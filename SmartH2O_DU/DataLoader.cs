using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using uPLibrary.Networking.M2Mqtt;

namespace SmartH2O_DU
{
    class DataLoader
    {
        SensorNodeDll.SensorNodeDll dll;
        string filePathXSD = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_Data\sensor.xsd";
        XmlDocument doc;
        bool isValid;
        String ValidationMessage;
        MqttClient m_cClient = new MqttClient(Properties.Resources.brokerIP);

        public void RetriveData(string str)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            String date = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss");
            string[] parts = str.Split(';');

            doc = new XmlDocument();
            XmlElement rootEl = doc.CreateElement("sensor");
            doc.AppendChild(rootEl);

            XmlNode root = doc.SelectSingleNode("/sensor");
            XmlElement data = doc.CreateElement("data");

            data.SetAttribute("id", parts[0]);
            data.SetAttribute("date", date);
            data.SetAttribute("type", parts[1]);
            data.SetAttribute("val", parts[2]);

            root.AppendChild(data);

            String xml = doc.OuterXml;
            try
            {
                isValid = true;
                ValidationEventHandler eventH = new ValidationEventHandler(MyEvent);
                doc.Schemas.Add(null, filePathXSD);
                doc.Validate(eventH);          
            }
            catch (XmlException ex)
            {
                isValid = false;
                ValidationMessage = String.Format("Invalid Document{0}", ex.Message);
            }

            if (isValid)
            {
                //call mosquito
                Console.WriteLine(xml);
                m_cClient.Publish("smartDU", Encoding.UTF8.GetBytes(xml));
               
            }
            else
            {
               Console.WriteLine(ValidationMessage);
            }

        }

        private void MyEvent(object sender, ValidationEventArgs e)
        {
            isValid = false;
            ValidationMessage = "Invalida Document! -->" + e.Message;
        }

        public void StartDLL()
        {
            Console.WriteLine("Connecting ....");
            try
            {
                m_cClient.Connect(Guid.NewGuid().ToString());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting");
            }else
            {
                Console.WriteLine("Connected");
            }
            dll = new SensorNodeDll.SensorNodeDll();
            dll.Initialize(RetriveData, 3000);
           
        }

        public void StopDll()
        {
            dll.Stop();
            m_cClient.Disconnect();
        }
    }
}

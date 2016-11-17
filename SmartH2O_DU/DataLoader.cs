using System;
using System.Globalization;
using System.Xml;

namespace SmartH2O_DU
{
    class DataLoader
    {
        SensorNodeDll.SensorNodeDll dll;
        string filePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_Data\sensors.xml";
        XmlDocument doc;

        public void RetriveData(string str)
        {
      
            String date = DateTime.Now.ToString("YYYY - MM - DD-hh:mm: ss");
            string[] parts = str.Split(';');

            Console.Write(str);
            Console.WriteLine(" " + date);

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
            //doc.Schemas.Add(new Xml)

            //

        }

        public void StartDLL()
        {     
            dll = new SensorNodeDll.SensorNodeDll();
            dll.Initialize(RetriveData, 250);
           
        }

        public void StopDll()
        {
            dll.Stop();
        }
    }
}

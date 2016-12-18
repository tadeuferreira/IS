
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SmartH2O_DLog
{
    class Program
    {
        private static SmartH20_Service.SmartH2O_ServiceClient serviceClient;

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine(e.Topic + ": \n" + Encoding.UTF8.GetString(e.Message));
            switch (e.Topic)
            {
                case "smartDU":
                    //saveParamData();
                    serviceClient.PutParam(Encoding.UTF8.GetString(e.Message));
                    //chamar metodo de gravar no ficheiro      
                    break;
                case "smartAlarm":
                    //chamar metodo de gravar no ficheiro
                    serviceClient.PutAlarm(Encoding.UTF8.GetString(e.Message));
                    break;
            }
        }
/*
        private static void saveParamData(string sensor)
        {
            string paramDataXML = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_Data\param-data.xml";
            string paramDataXSD = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_Data\param-data.xsd";

            XmlDocument docParamData = new XmlDocument();
            docParamData.Load(paramDataXML);

            XmlDocument docSensor = new XmlDocument();
            docSensor.LoadXml(sensor);

            //check if there is a root
            XmlNode root = docParamData.SelectSingleNode("/sensors");
            if(root == null)
            {
                XmlElement rootEl = docParamData.CreateElement("sensors");
                docParamData.AppendChild(rootEl);
                root = docParamData.SelectSingleNode("/sensors");
            }

            XmlNode sensorElement = docParamData.ImportNode(docSensor.SelectSingleNode("/sensor"), true);
            root.AppendChild(sensorElement);

           // serviceClient.PutParam();

           // docParamData.Save(paramDataXML);
        }*/

        static void Main(string[] args)
        {
            serviceClient = new SmartH20_Service.SmartH2O_ServiceClient();
            Console.WriteLine("Connecting ....");
            MqttClient m_cClient = new MqttClient(Properties.Resources.brokerIP);
            string[] m_strTopicsInfo = { "smartDU" , "smartAlarm" };

            
            m_cClient.Connect(Guid.NewGuid().ToString());
            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

            //Specify events we are interest on
            //New Msg Arrived
            m_cClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            //Subscribe to topics
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE};//QoS
            m_cClient.Subscribe(m_strTopicsInfo, qosLevels);

            Console.ReadKey();
            if (m_cClient.IsConnected)
            {
                m_cClient.Unsubscribe(m_strTopicsInfo); //Put this in a button to see notif!
                m_cClient.Disconnect(); //Free process and process's resources
            }
        }
    }
}

﻿
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Console.WriteLine(e.Topic + ": \n" + Encoding.UTF8.GetString(e.Message));
            switch (e.Topic)
            {
                case "smartDU":
                    serviceClient.PutParam(Encoding.UTF8.GetString(e.Message));    
                    break;
                case "smartAlarm":
                    serviceClient.PutAlarm(Encoding.UTF8.GetString(e.Message));

                    string alarmDataXML = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_Data\alarm-data.xml";
                    string paramDataXSD = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_Data\alarm-data.xsd";

                    XmlDocument docAlarmData = new XmlDocument();
                    docAlarmData.Load(alarmDataXML);

                    XmlDocument docTrigger = new XmlDocument();
                    docTrigger.LoadXml(Encoding.UTF8.GetString(e.Message));

                    //check if there is a root
                    XmlNode root = docAlarmData.SelectSingleNode("/alarms");
                    if (root == null)
                    {
                        XmlElement rootEl = docAlarmData.CreateElement("alarms");
                        docAlarmData.AppendChild(rootEl);
                        root = docAlarmData.SelectSingleNode("/alarms");
                    }

                    XmlNode triggerEl = docAlarmData.ImportNode(docTrigger.SelectSingleNode("/alarmTrigger"), true);
                    root.AppendChild(triggerEl);
                    docAlarmData.Save(alarmDataXML);

                    break;
            }
        }

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

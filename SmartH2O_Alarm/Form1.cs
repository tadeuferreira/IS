using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SmartH2O_Alarm
{
    public partial class Form1 : Form
    {
        string alarmChannel = "smartAlarm";
        Boolean activated = true;
        XmlDocument docRules = new XmlDocument();
        MqttClient ciClient = new MqttClient(Properties.Resources.bokerIP);
        string[] m_strTopicsInfo = {"smartDU"};
        string filePathRules = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_Data\trigger-rules.xml";

        public Form1()
        {
            InitializeComponent();

            ciClient.Connect(Guid.NewGuid().ToString());

            if (!ciClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            ciClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            ciClient.Subscribe(m_strTopicsInfo, qosLevels);
            loadRules();
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {

            if (activated)
            {
                string message = Encoding.UTF8.GetString(e.Message);
                Console.WriteLine(e.Topic + ": \n" + message);

                if (e.Topic == "smartDU")
                {
                    alarmTriggerCheck(Encoding.UTF8.GetString(e.Message));
                }
            }  
        }

        private void alarmTriggerCheck(string paramData)
        {
            XmlNodeList listAlarms = docRules.SelectNodes("/alarmcenter/alarm");

            XmlDocument docParamData = new XmlDocument();
            docParamData.LoadXml(paramData);
            XmlNode nodeData = docParamData.SelectSingleNode("/sensor/data");
            int count = listAlarms.Count;
            for(int i = 0; i < count; i++)
            {
                XmlNode nodeAlarm = listAlarms.Item(i);
                if (nodeAlarm.HasChildNodes)
                {
                    XmlNodeList listRules = nodeAlarm.SelectNodes("/rule");
                    int countrule = listRules.Count;
                    for(int j = 0; i< countrule; i++)
                    {
                        XmlNode nodeRule = listRules.Item(j);
                        if(nodeRule.Attributes["active"].Value == "true")
                        {
                            switch (nodeAlarm.Attributes["type"].Value)
                            {
                                case "ALARM_MIN":
                                    if (nodeRule.Attributes["type"].Value == nodeData.Attributes["type"].Value)
                                    {
                                        float ruleval = float.Parse(nodeRule.Attributes["value"].Value);
                                        float dataVal = float.Parse(nodeRule.Attributes["value"].Value);
                                        if (dataVal < ruleval)
                                            triggerAlarm(nodeRule, nodeData);
                                    }
                                    break;
                                case "ALARM_MAX":
                                    if (nodeRule.Attributes["type"].Value == nodeData.Attributes["type"].Value)
                                    {
                                        float ruleval = float.Parse(nodeRule.Attributes["value"].Value);
                                        float dataVal = float.Parse(nodeRule.Attributes["value"].Value);
                                        if (dataVal > ruleval)
                                            triggerAlarm(nodeRule, nodeData);
                                    }
                                    break;
                                case "ALARM_INTERVAL":
                                    if (nodeRule.Attributes["type"].Value == nodeData.Attributes["type"].Value)
                                    {
                                        float rulemin = float.Parse(nodeRule.Attributes["min"].Value);
                                        float rulemax = float.Parse(nodeRule.Attributes["max"].Value);
                                        float dataVal = float.Parse(nodeRule.Attributes["value"].Value);
                                        if (dataVal >= rulemin && dataVal <= rulemax)
                                            triggerAlarm(nodeRule, nodeData);
                                    }
                                    break;
                                case "ALARM_EQUALS":
                                    if (nodeRule.Attributes["type"].Value == nodeData.Attributes["type"].Value)
                                    {
                                        float ruleval = float.Parse(nodeRule.Attributes["value"].Value);
                                        float dataVal = float.Parse(nodeRule.Attributes["value"].Value);
                                        if (dataVal == ruleval)
                                            triggerAlarm(nodeRule, nodeData);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            



        }

        private void triggerAlarm(XmlNode nodeRule, XmlNode nodeData)
        {
            XmlDocument messageDoc = new XmlDocument();
            XmlNode root = messageDoc.SelectSingleNode("/alarmTrigger");
            if (root == null)
            {
                XmlElement rootEl = messageDoc.CreateElement("alarmTrigger");
                messageDoc.AppendChild(rootEl);
                root = messageDoc.SelectSingleNode("/alarmTrigger");
            }

            XmlElement messageEl = messageDoc.CreateElement("messageEl");
            messageEl.SetAttribute("alarmType", nodeRule.Attributes["type"].Value);
            messageEl.SetAttribute("sensorType", nodeData.Attributes["type"].Value);
            messageEl.SetAttribute("sensorid", nodeData.Attributes["id"].Value);
            messageEl.SetAttribute("date", nodeData.Attributes["date"].Value);
            messageEl.SetAttribute("val", nodeData.Attributes["val"].Value);
            messageEl.InnerText = nodeRule.InnerText;
            root.AppendChild(messageEl);

            publishToCI(messageDoc.OuterXml);


        }

        private void loadRules()
        {
            docRules.Load(filePathRules);
        }


        private void publishToCI(string message)
        {
            if (ciClient.IsConnected)
            {
                Console.WriteLine(message);
                ciClient.Publish(alarmChannel, Encoding.UTF8.GetBytes(message));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (activated)
            {
                case true:
                    button1.Text = "OFF";
                    activated = false;
                    break;
                case false:
                    button1.Text = "ON";
                    loadRules();
                    activated = true;
                    break;
            }
        }
    }
}

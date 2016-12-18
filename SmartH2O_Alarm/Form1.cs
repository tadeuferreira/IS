﻿using System;
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
        delegate void SetTextCallback(string text);
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
                //Console.WriteLine(e.Topic + ": \n" + message);

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
                    XmlNodeList listRules = nodeAlarm.SelectNodes("rule");
                    int countrule = listRules.Count;
                    for(int j = 0; j< countrule; j++)
                    {
                        XmlNode nodeRule = listRules.Item(j);                 
                        if(nodeRule.Attributes["active"].Value == "true")
                        {
                            string alarmType = nodeAlarm.Attributes["type"].Value;

                            switch (alarmType)
                            {
                                case "ALARM_MIN":
                                    if (nodeRule.Attributes["type"].Value == nodeData.Attributes["type"].Value)
                                    {
                                        float ruleval = float.Parse(nodeRule.Attributes["value"].Value);
                                        Console.WriteLine(CultureInfo.CurrentCulture);
                                        Console.WriteLine(CultureInfo.CurrentUICulture);

                                        float dataVal = float.Parse(nodeData.Attributes["val"].Value);
                                        if (dataVal < ruleval)
                                            triggerAlarm(nodeRule, nodeData, alarmType);
                                    }
                                    break;
                                case "ALARM_MAX":
                                    if (nodeRule.Attributes["type"].Value == nodeData.Attributes["type"].Value)
                                    {
                                        float ruleval = float.Parse(nodeRule.Attributes["value"].Value);
                                        float dataVal = float.Parse(nodeData.Attributes["val"].Value);
                                        if (dataVal > ruleval)
                                            triggerAlarm(nodeRule, nodeData, alarmType);
                                    }
                                    break;
                                case "ALARM_INTERVAL":
                                    if (nodeRule.Attributes["type"].Value == nodeData.Attributes["type"].Value)
                                    {
                                        float rulemin = float.Parse(nodeRule.Attributes["min"].Value);
                                        float rulemax = float.Parse(nodeRule.Attributes["max"].Value);
                                        float dataVal = float.Parse(nodeData.Attributes["val"].Value);
                                        if (dataVal >= rulemin && dataVal <= rulemax)
                                            triggerAlarm(nodeRule, nodeData, alarmType);
                                    }
                                    break;
                                case "ALARM_EQUALS":
                                    if (nodeRule.Attributes["type"].Value == nodeData.Attributes["type"].Value)
                                    {
                                        float ruleval = float.Parse(nodeRule.Attributes["value"].Value);
                                        float dataVal = float.Parse(nodeData.Attributes["val"].Value);
                                        if (dataVal == ruleval)
                                            triggerAlarm(nodeRule, nodeData, alarmType);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            



        }

        private void triggerAlarm(XmlNode nodeRule, XmlNode nodeData, string alarmType)
        {
            Console.WriteLine("triggering");
            XmlDocument messageDoc = new XmlDocument();
            XmlNode root = messageDoc.SelectSingleNode("/alarmTrigger");
            if (root == null)
            {
                XmlElement rootEl = messageDoc.CreateElement("alarmTrigger");
                messageDoc.AppendChild(rootEl);
                root = messageDoc.SelectSingleNode("/alarmTrigger");
            }

            XmlElement messageEl = messageDoc.CreateElement("message");
            messageEl.SetAttribute("alarmType", alarmType);
            messageEl.SetAttribute("sensorType", nodeData.Attributes["type"].Value);
            messageEl.SetAttribute("sensorid", nodeData.Attributes["id"].Value);
            messageEl.SetAttribute("date", nodeData.Attributes["date"].Value);
            messageEl.SetAttribute("val", nodeData.Attributes["val"].Value);
            if(alarmType == "ALARM_INTERVAL")
            {
                messageEl.SetAttribute("lowertriggerValue", nodeRule.Attributes["min"].Value);
                messageEl.SetAttribute("highertriggerValue", nodeRule.Attributes["max"].Value);
            }
            else
            {
                messageEl.SetAttribute("triggerValue", nodeRule.Attributes["value"].Value);
            }
            messageEl.InnerText = nodeRule.InnerText;
            root.AppendChild(messageEl);
            string outer = messageDoc.OuterXml;

            Console.WriteLine(outer);
            publishToCI(outer);

            string text = messageEl.InnerText + "@";
            text = text.Replace("@",Environment.NewLine);
            SetText(text);
        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = textBox1.Text + text;
            }
        }

        private void loadRules()
        {
            docRules.Load(filePathRules);
        }


        private void publishToCI(string message)
        {
            if (ciClient.IsConnected)
            {
              
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

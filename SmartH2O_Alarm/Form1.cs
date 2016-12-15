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
        static Boolean deactivated = true;
        static List<Parameter> rules = new List<Parameter>();
        static XmlDocument doc = new XmlDocument();
        static MqttClient ciClient = new MqttClient(Properties.Resources.bokerIP);
        static string[] m_strTopicsInfo = { "parameters", "alarms" };

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
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string data = Encoding.UTF8.GetString(e.Message);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(data);

            if (rules.Count != 0)
            {
                XmlNodeList nodeList = xml.GetElementsByTagName("parameter");
                foreach (XmlNode node in nodeList)
                {
                    foreach (Parameter p in rules)
                    {
                        Console.WriteLine("node :" + node.ChildNodes[0].InnerText.Trim());
                        Console.WriteLine("rule :" + p.Name.Trim());

                        string toCompare = node.ChildNodes[0].InnerText.Trim();
                        string rule = p.Name.Trim();

                        if (String.Compare(toCompare, rule) == 0)
                        {
                            Console.WriteLine(node.ChildNodes[1].InnerText);
                            float floatToCompare = float.Parse(node.ChildNodes[1].InnerText, CultureInfo.InvariantCulture);
                            float floatRule = float.Parse(p.Value, CultureInfo.InvariantCulture);

                            Console.WriteLine("Floats " + node.ChildNodes[1].InnerText + p.Value);

                            switch (p.Condition)
                            {
                                case "bigger":
                                    if (floatToCompare > floatRule)
                                    {
                                        appendAlarm(floatToCompare + " bigger " + floatRule, p.Name + "was bigger than " + floatRule);
                                    }
                                    break;
                                case "smaller":
                                    if (floatToCompare < floatRule)
                                    {
                                        appendAlarm(floatToCompare + " smaller " + floatRule, p.Name + "was smaller than " + floatRule);
                                    }
                                    break;
                                case "equals":
                                    if (floatToCompare == floatRule)
                                    {
                                        appendAlarm(floatToCompare + " bigger " + floatRule, p.Name + "was equals to " + floatRule);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void loadRules()
        {
            doc.Load("trigger-rules.xml");
            XmlNodeList nodeList = doc.GetElementsByTagName("parameter");
            if (nodeList != null)
            {
                foreach (XmlNode node in nodeList)
                {
                    Parameter param = new Parameter();
                    param.Name = node.ChildNodes[0].InnerText;
                    param.Value = node.ChildNodes[1].InnerText;
                    param.Condition = node.ChildNodes[2].InnerText;
                    rules.Add(param);

                }
            }

            foreach (Parameter p in rules)
            {
                Console.WriteLine(p.toString());
            }
        }

        private static void appendAlarm(string alarm, string message)
        {
            XmlDocumentFragment xfragment = doc.CreateDocumentFragment();
            xfragment.InnerXml = "<alarm>" + alarm + "</alarm><message>" + message + "</message>";
            doc.DocumentElement.FirstChild.AppendChild(xfragment);
            publicToCI(m_strTopicsInfo[1], doc.InnerText);
        }

        private static void publicToCI(string channel, string parameter)
        {
            if (ciClient.IsConnected)
            {
                ciClient.Publish(channel, Encoding.UTF8.GetBytes(parameter));
                Console.WriteLine(parameter);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           /*  DataReceiver data = new DataReceiver();

            data.startMosquitto(); */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (deactivated)
            {
                case false:
                    button1.Text = "OFF";
                    deactivated = true;
                    break;
                case true:
                    button1.Text = "ON";
                    rules.Clear();
                    loadRules();
                    deactivated = false;
                    break;
            }
        }
    }
}

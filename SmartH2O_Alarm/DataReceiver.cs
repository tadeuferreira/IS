using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SmartH2O_Alarm
{
    class DataReceiver
    {
        private TextBox textBox1;
        MqttClient m_cClient = new MqttClient("192.168.231.206");
      

        string[] strData = { "smartDU", "smartDU" };

        public DataReceiver()
        {

        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // MessageBox.Show("Received = " + Encoding.UTF8.GetString(e.Message) +
            // " on topic " + e.Topic);
            string filePathXML = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_Data\trigger-rules.xml";
            XmlDocument docRules = new XmlDocument();
            docRules.Load(filePathXML);

            XmlDocument docData = new XmlDocument();
            docData.LoadXml(Encoding.UTF8.GetString(e.Message));

            XmlNode data = docData.SelectSingleNode("/sensor/data");
            string type = data.Attributes["type"].Value;
            string val = data.Attributes["val"].Value;





        }

        public void startMosquitto(){

         try
            {
                m_cClient.Connect(Guid.NewGuid().ToString());

                m_cClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };

                m_cClient.Subscribe(strData, qosLevels);
                

            }
            catch (Exception e)
            {

                Console.Write(e.Message);
            }

            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

        }

       

    }
}

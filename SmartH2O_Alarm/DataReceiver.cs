using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SmartH2O_Alarm
{
    class DataReceiver
    {
        MqttClient m_cClient = new MqttClient("192.168.231.206");
        string[] strData = { "smartDU" };

        public DataReceiver()
        {

        }

        public void startMosquitto(){

         try
            {
                m_cClient.Connect(Guid.NewGuid().ToString());
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

            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
                                
            m_cClient.Subscribe(strData, qosLevels);
            Console.ReadKey();

            if (m_cClient.IsConnected)
            {
                m_cClient.Unsubscribe(strData);
                m_cClient.Disconnect(); 
            }

        }

    }
}

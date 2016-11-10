using System;
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
            Console.WriteLine(str);
            string[] parts = str.Split(';');
            if (parts[0] == "1")
            {
                doc = new XmlDocument();
                XmlElement rootEl = doc.CreateElement("sensors");
                doc.AppendChild(rootEl);
            }
            XmlNode root = doc.SelectSingleNode("/sensors");
            XmlElement data = doc.CreateElement("data");
            data.SetAttribute("number", parts[0]);
            data.SetAttribute("type", parts[1]);
            data.SetAttribute("val", parts[2]);

            root.AppendChild(data);
            if(parts[0] == "3")
                doc.Save(filePath);
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

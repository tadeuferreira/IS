using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SmartH2O_DU
{
    class DataLoader
    {
        SensorNodeDll.SensorNodeDll dll;
        string filePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_data\sensors.xml";
        XmlDocument doc;

        public void RetriveData(string str)
        {
            Console.WriteLine(str);
            string[] parts = str.Split(';');
            XmlNode root = doc.SelectSingleNode("/sensors");

            XmlElement data = doc.CreateElement("data");
            data.SetAttribute("number", parts[0]);
            data.SetAttribute("type", parts[1]);
            data.SetAttribute("val", parts[2]);

            root.AppendChild(data);
            doc.Save(filePath);



        }

        public void StartDLL()
        {
            doc = new XmlDocument();
            doc.Load(filePath);
            dll = new SensorNodeDll.SensorNodeDll();
            dll.Initialize(RetriveData, 500);
           
        }

        public void StopDll()
        {
            dll.Stop();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorNodeDll;


namespace SmartH2O_DU
{
    class Program
    {
        static void Main(string[] args)
        {
            DataLoader dataLoader = new DataLoader();
            dataLoader.StartDLL();
        }
    }
}

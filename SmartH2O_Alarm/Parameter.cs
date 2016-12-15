using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartH2O_Alarm
{
    public class Parameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Condition { get; set; }


        public string toString()
        {
            return Name + Value + Condition;
        }
    }
}
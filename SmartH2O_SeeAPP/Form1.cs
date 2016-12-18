using SmartH2O_SeeAPP.SmartH2O_Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartH2O_SeeAPP
{
    public partial class Form1 : Form
    {

        SmartH2O_ServiceClient service = new SmartH2O_ServiceClient();
        private ParamVals[] paramVals;
        private AlarmInfo[] alarmInfo;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void AlarmBetweenDates_Click(object sender, EventArgs e)
        {
            alarmInfo = service.GetAlarmInterval(dateTimePickerAlarmStart.Value.Date, dateTimePickerAlarmEnd.Value.Date);
            for (int i = 0; i < alarmInfo.Length; i++)
            {
                textBoxAlarmInfo.Text = alarmInfo[i].Type + alarmInfo[i].TriggerType + alarmInfo[i].TriggerValue.ToString() + alarmInfo[i].Datetime.ToString();
            }

        }
    }
}

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

        AlarmInfo alarm = new AlarmInfo();

        public Form1()
        {
            InitializeComponent();
            
        }

        private void AlarmBetweenDates_Click(object sender, EventArgs e)
        {
            DateTime dateStart = dateTimePickerAlarmStart.Value;
            DateTime dateEnd = dateTimePickerAlarmEnd.Value;

           // alarmInfo = service.GetAlarmInterval(dateStart, dateEnd);

            /*if (dateStart < dateEnd && !dateStart.Equals(dateEnd))
            {
                alarmInfo = service.GetAlarmInterval(dateStart, dateEnd);
                for (int i = 0; i < alarmInfo.Length; i++)
                {
                    textBoxAlarmInfo.Text = alarmInfo[i].Type + alarmInfo[i].TriggerType + alarmInfo[i].TriggerValue.ToString() + alarmInfo[i].Datetime.ToString();
                }
            }  */


           // textBoxAlarmInfo.Text = alarmInfo[0].Type + alarmInfo[0].TriggerType + alarmInfo[0].TriggerValue.ToString() + alarmInfo[0].Datetime.ToString();

            //textBoxAlarmInfo.Text = alarmInfo.Length.ToString()+ "     \n" + dateTimePickerAlarmStart.Value+ "\n" + dateTimePickerAlarmEnd.Value;
        }

        private void showWaterParams(string alarmType)
        {
            textBoxWaterQuality.Clear();
            paramVals = service.GetParamHourly(DateTime.Now);

            for (int i = 0; i < paramVals.Length; i++)
            {
                if (paramVals[i].Type == alarmType)
                {
                    textBoxWaterQuality.Text = alarm.Message;
                }
            }
        }

        private void checkBoxAllWater_CheckedChanged(object sender, EventArgs e)
        {
            bool isAllWater = checkBoxAllWater.Checked;
            string alarmType = alarm.Type;

            if (isAllWater)
            {
                showWaterParams(alarmType);
            }
        }

        private void checkBoxPHWater_CheckedChanged(object sender, EventArgs e)
        {   
            bool isPHWater = checkBoxPHWater.Checked;
            string alarmType = "PH";

            if (isPHWater)
            {
                showWaterParams(alarmType);
            }
        }

        private void checkBoxCL2Water_CheckedChanged(object sender, EventArgs e)
        {
            bool isCL2Water = checkBoxCL2Water.Checked;
            string alarmType = "CL2";

            if (isCL2Water)
            {
                showWaterParams(alarmType);
            }
        }

        private void checkBoxNH3Water_CheckedChanged(object sender, EventArgs e)
        {
            bool isNH3Water = checkBoxNH3Water.Checked;
            string alarmType = "NH3";

            if (isNH3Water)
            {
                showWaterParams(alarmType);
            }
        }
    }
}

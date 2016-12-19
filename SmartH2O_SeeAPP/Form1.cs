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
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartH2O_SeeAPP
{
    public partial class Form1 : Form
    {

        private SmartH2O_ServiceClient service = new SmartH2O_ServiceClient();
        private ParamVals[] paramVals;
        private AlarmInfo[] alarmInfo;

        private AlarmInfo alarm = new AlarmInfo();
        private ParamVals param = new ParamVals();

        public Form1()
        {
            InitializeComponent();
            listViewAlarmInfo.Clear();
            chart1.Series.Clear();
            chart1.Titles.Clear();
        }

        private void buttonAlarmInfoDaily_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePickerAlarmDaily.Value;
            if (date < DateTime.Now)
            {
                alarmInfo = service.GetAlarmDaily(date);
                showListViewAlarm(alarmInfo);
            }
            else
            {
                MessageBox.Show("Data Invalid !!!");
            }
        }

        private void buttonAlarmBetweenDates_Click(object sender, EventArgs e)
        {
            DateTime dateStart = dateTimePickerAlarmStart.Value;
            DateTime dateEnd = dateTimePickerAlarmEnd.Value;

            if (dateStart < dateEnd && !dateStart.Equals(dateEnd) && dateEnd <= DateTime.Now)
            {
                alarmInfo = service.GetAlarmInterval(dateStart, dateEnd);
                showListViewAlarm(alarmInfo);
            }
            else
            {
                MessageBox.Show("Data Invalid !!!");
            }

        }

        private void showListViewAlarm(AlarmInfo[] alarmInfo)
        {
            listViewAlarmInfo.Clear();

            listViewAlarmInfo.View = View.Details;
            listViewAlarmInfo.Columns.Add("PARAMETER", 50);
            listViewAlarmInfo.Columns.Add("TYPE", 100);
            listViewAlarmInfo.Columns.Add("VALUE", 40);
            listViewAlarmInfo.Columns.Add("TRIGGER VALUE", 40);
            listViewAlarmInfo.Columns.Add("LOWER BOUND", 50);
            listViewAlarmInfo.Columns.Add("UPPER BOUND", 50);
            listViewAlarmInfo.Columns.Add("DATE", 80);
            listViewAlarmInfo.Columns.Add("MESSAGE", 170);

            for (int i = 0; i < alarmInfo.Length; i++)
            {
                string[] row = { alarmInfo[i].Type,
                                 alarmInfo[i].TriggerType,
                                 alarmInfo[i].SensorVal.ToString(),
                                 alarmInfo[i].TriggerValue.ToString(),
                                 alarmInfo[i].LowerBound.ToString(),
                                 alarmInfo[i].UpperBound.ToString(),
                                 alarmInfo[i].Datetime.ToString(),
                                 alarmInfo[i].Message};

                ListViewItem item = new ListViewItem(row);
                listViewAlarmInfo.Items.Add(item);
            }
        }

        private void buttonGraphDay_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            DateTime date = dateTimePickerGraph.Value;

            if (date < DateTime.Now)
            {
                paramVals = service.GetParamHourly(date);

                Series[] max = new Series[3];
                Series[] min = new Series[3];
                Series[] avg = new Series[3];


                for (int i = 0; i < 3; i++)
                {
                    max[i] = new Series();
                    min[i] = new Series();
                    avg[i] = new Series();
                }

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 24; j++)
                    {
                        max[i].Points.AddXY(j+1, paramVals[i].Max[j]);
                        min[i].Points.AddXY(j+1, paramVals[i].Min[j]);
                        avg[i].Points.AddXY(j+1, paramVals[i].Average[j]);
                    }
                    chart1.Series.Add(max[i]);
                    chart1.Series.Add(min[i]);
                    chart1.Series.Add(avg[i]);
                }


                chart1.Series[0].Name = "PH: Max";
                chart1.Series[1].Name = "PH: Min";
                chart1.Series[2].Name = "PH: Avg";
                chart1.Series[3].Name = "NH3: Max";
                chart1.Series[4].Name = "NH3: Min";
                chart1.Series[5].Name = "NH3: Avg";
                chart1.Series[6].Name = "CL2: Max";
                chart1.Series[7].Name = "CL2: Min";
                chart1.Series[8].Name = "CL2: Avg";

                for (int i = 0; i < 9; i++)
                {
                    chart1.Series[i].ChartType = SeriesChartType.Spline;
                }

                chart1.ChartAreas[0].AxisX.Minimum = 1;
                chart1.ChartAreas[0].AxisX.Maximum = 24;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 20;
                chart1.ChartAreas[0].AxisX.Title = "Hours";
                chart1.ChartAreas[0].AxisY.Title = "Values";
                chart1.Titles.Add("Parameters Info " + date.ToString("dd/MM/yyyy"));

                checkGraphParams();


            }
            else
            {
                MessageBox.Show("Data Invalid !!!");
            }

        }

        private void buttonGraphWeek_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            DateTime date = dateTimePickerGraph.Value;

            if (date < DateTime.Now)
            {
                paramVals = service.GetParamWeekly(date);

                Series[] max = new Series[3];
                Series[] min = new Series[3];
                Series[] avg = new Series[3];


                for (int i = 0; i < 3; i++)
                {
                    max[i] = new Series();
                    min[i] = new Series();
                    avg[i] = new Series();
                }

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        max[i].Points.AddXY(j + 1, paramVals[i].Max[j]);
                        min[i].Points.AddXY(j + 1, paramVals[i].Min[j]);
                        avg[i].Points.AddXY(j + 1, paramVals[i].Average[j]);
                    }
                    chart1.Series.Add(max[i]);
                    chart1.Series.Add(min[i]);
                    chart1.Series.Add(avg[i]);
                }


                chart1.Series[0].Name = "PH: Max";
                chart1.Series[1].Name = "PH: Min";
                chart1.Series[2].Name = "PH: Avg";
                chart1.Series[3].Name = "NH3: Max";
                chart1.Series[4].Name = "NH3: Min";
                chart1.Series[5].Name = "NH3: Avg";
                chart1.Series[6].Name = "CL2: Max";
                chart1.Series[7].Name = "CL2: Min";
                chart1.Series[8].Name = "CL2: Avg";

                for (int i = 0; i < 9; i++)
                {
                    chart1.Series[i].ChartType = SeriesChartType.Spline;
                }

                chart1.ChartAreas[0].AxisX.Minimum = 1;
                chart1.ChartAreas[0].AxisX.Maximum = 7;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 20;
                chart1.ChartAreas[0].AxisX.Title = "Days";
                chart1.ChartAreas[0].AxisY.Title = "Values";
                chart1.Titles.Add("Parameters Info " + date.ToString("dd/MM/yyyy"));

                checkGraphParams();


            }
            else
            {
                MessageBox.Show("Data Invalid !!!");
            }

        }

        private void buttonGraphBetweenDates_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            DateTime dateStart = dateTimePickerGraphStart.Value;
            DateTime dateEnd = dateTimePickerGraphEnd.Value;

            if (dateStart < dateEnd && !dateStart.Equals(dateEnd) && dateEnd <= DateTime.Now)
            {
                paramVals = service.GetParamDaily(dateStart, dateEnd);

                Series[] max = new Series[3];
                Series[] min = new Series[3];
                Series[] avg = new Series[3];


                for (int i = 0; i < 3; i++)
                {
                    max[i] = new Series();
                    min[i] = new Series();
                    avg[i] = new Series();
                }

                int totalDays = (int) Math.Truncate((dateEnd - dateStart).TotalDays) + 1;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < totalDays; j++)
                    {
                        max[i].Points.AddXY(j + 1, paramVals[i].Max[j]);
                        min[i].Points.AddXY(j + 1, paramVals[i].Min[j]);
                        avg[i].Points.AddXY(j + 1, paramVals[i].Average[j]);
                    }
                    chart1.Series.Add(max[i]);
                    chart1.Series.Add(min[i]);
                    chart1.Series.Add(avg[i]);
                }


                chart1.Series[0].Name = "PH: Max";
                chart1.Series[1].Name = "PH: Min";
                chart1.Series[2].Name = "PH: Avg";
                chart1.Series[3].Name = "NH3: Max";
                chart1.Series[4].Name = "NH3: Min";
                chart1.Series[5].Name = "NH3: Avg";
                chart1.Series[6].Name = "CL2: Max";
                chart1.Series[7].Name = "CL2: Min";
                chart1.Series[8].Name = "CL2: Avg";

                for (int i = 0; i < 9; i++)
                {
                    chart1.Series[i].ChartType = SeriesChartType.Spline;
                }

                chart1.ChartAreas[0].AxisX.Minimum = 1;
                chart1.ChartAreas[0].AxisX.Maximum = totalDays;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 20;
                chart1.ChartAreas[0].AxisX.Title = "Days";
                chart1.ChartAreas[0].AxisY.Title = "Values";
                chart1.Titles.Add("Parameters Info " + dateStart.ToString("dd/MM/yyyy") + " to " + dateEnd.ToString("dd/MM/yyyy"));

                checkGraphParams();


            }
            else
            {
                MessageBox.Show("Data Invalid !!!");
            }

        }

        private void checkGraphParams()
        {
            try
            {
                chart1.Series[0].Enabled = checkBoxPH.Checked;
                chart1.Series[1].Enabled = checkBoxPH.Checked;
                chart1.Series[2].Enabled = checkBoxPH.Checked;

                chart1.Series[3].Enabled = checkBoxNH3.Checked;
                chart1.Series[4].Enabled = checkBoxNH3.Checked;
                chart1.Series[5].Enabled = checkBoxNH3.Checked;

                chart1.Series[6].Enabled = checkBoxCL2.Checked;
                chart1.Series[7].Enabled = checkBoxCL2.Checked;
                chart1.Series[8].Enabled = checkBoxCL2.Checked;
            }
            catch (Exception)
            {

            }
        }

        private void checkBoxPH_CheckedChanged(object sender, EventArgs e)
        {
            checkGraphParams();
        }

        private void checkBoxNH3_CheckedChanged(object sender, EventArgs e)
        {
            checkGraphParams();
        }

        private void checkBoxCL2_CheckedChanged(object sender, EventArgs e)
        {
            checkGraphParams();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxTypeGraphs.Items.Add("Column");
            comboBoxTypeGraphs.Items.Add("Line");
            comboBoxTypeGraphs.Items.Add("Spline");
            comboBoxTypeGraphs.Items.Add("Bar");
            comboBoxTypeGraphs.Items.Add("Point");

            //comboBoxTypeGraphs.SelectedIndex = 2;
        }

        private void comboBoxTypeGraphs_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxTypeGraphs.SelectedItem.ToString())
            {
                case "Column": changeGraphType(SeriesChartType.Column);
                    break;
                case "Line": changeGraphType(SeriesChartType.Line);
                    break;
                case "Spline": changeGraphType(SeriesChartType.Spline);
                    break;
                case "Bar": changeGraphType(SeriesChartType.Bar);
                    break;
                case "Point": changeGraphType(SeriesChartType.Point);
                    break;
                default: 
                    break;
            }
        }

        private void changeGraphType(SeriesChartType type)
        {
            for (int i = 0; i < 9; i++)
            {
                chart1.Series[i].ChartType = type;
            }
        }
    }
}

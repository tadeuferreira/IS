namespace SmartH2O_SeeAPP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePickerAlarmStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerAlarmEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerGraph = new System.Windows.Forms.DateTimePicker();
            this.AlarmBetweenDates = new System.Windows.Forms.Button();
            this.buttonGraphDay = new System.Windows.Forms.Button();
            this.buttonGraphWeek = new System.Windows.Forms.Button();
            this.listViewAlarmInfo = new System.Windows.Forms.ListView();
            this.dateTimePickerAlarmDaily = new System.Windows.Forms.DateTimePicker();
            this.buttonAlarmInfoDaily = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePickerGraphStart = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePickerGraphEnd = new System.Windows.Forms.DateTimePicker();
            this.buttonGraphBetweenDates = new System.Windows.Forms.Button();
            this.checkBoxPH = new System.Windows.Forms.CheckBox();
            this.checkBoxCL2 = new System.Windows.Forms.CheckBox();
            this.checkBoxNH3 = new System.Windows.Forms.CheckBox();
            this.comboBoxTypeGraphs = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alarm Information:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select Dates:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "to";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 616);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Select Date:";
            // 
            // dateTimePickerAlarmStart
            // 
            this.dateTimePickerAlarmStart.Location = new System.Drawing.Point(87, 166);
            this.dateTimePickerAlarmStart.Name = "dateTimePickerAlarmStart";
            this.dateTimePickerAlarmStart.Size = new System.Drawing.Size(156, 20);
            this.dateTimePickerAlarmStart.TabIndex = 32;
            // 
            // dateTimePickerAlarmEnd
            // 
            this.dateTimePickerAlarmEnd.Location = new System.Drawing.Point(271, 167);
            this.dateTimePickerAlarmEnd.Name = "dateTimePickerAlarmEnd";
            this.dateTimePickerAlarmEnd.Size = new System.Drawing.Size(156, 20);
            this.dateTimePickerAlarmEnd.TabIndex = 33;
            // 
            // dateTimePickerGraph
            // 
            this.dateTimePickerGraph.Location = new System.Drawing.Point(87, 610);
            this.dateTimePickerGraph.Name = "dateTimePickerGraph";
            this.dateTimePickerGraph.Size = new System.Drawing.Size(156, 20);
            this.dateTimePickerGraph.TabIndex = 34;
            // 
            // AlarmBetweenDates
            // 
            this.AlarmBetweenDates.Location = new System.Drawing.Point(446, 165);
            this.AlarmBetweenDates.Name = "AlarmBetweenDates";
            this.AlarmBetweenDates.Size = new System.Drawing.Size(156, 23);
            this.AlarmBetweenDates.TabIndex = 36;
            this.AlarmBetweenDates.Text = "Alarm Between Dates";
            this.AlarmBetweenDates.UseVisualStyleBackColor = true;
            this.AlarmBetweenDates.Click += new System.EventHandler(this.buttonAlarmBetweenDates_Click);
            // 
            // buttonGraphDay
            // 
            this.buttonGraphDay.Location = new System.Drawing.Point(271, 611);
            this.buttonGraphDay.Name = "buttonGraphDay";
            this.buttonGraphDay.Size = new System.Drawing.Size(156, 23);
            this.buttonGraphDay.TabIndex = 37;
            this.buttonGraphDay.Text = "Show Graph Day";
            this.buttonGraphDay.UseVisualStyleBackColor = true;
            this.buttonGraphDay.Click += new System.EventHandler(this.buttonGraphDay_Click);
            // 
            // buttonGraphWeek
            // 
            this.buttonGraphWeek.Location = new System.Drawing.Point(446, 613);
            this.buttonGraphWeek.Name = "buttonGraphWeek";
            this.buttonGraphWeek.Size = new System.Drawing.Size(156, 23);
            this.buttonGraphWeek.TabIndex = 38;
            this.buttonGraphWeek.Text = "Show Graph Week";
            this.buttonGraphWeek.UseVisualStyleBackColor = true;
            this.buttonGraphWeek.Click += new System.EventHandler(this.buttonGraphWeek_Click);
            // 
            // listViewAlarmInfo
            // 
            this.listViewAlarmInfo.Location = new System.Drawing.Point(15, 23);
            this.listViewAlarmInfo.Name = "listViewAlarmInfo";
            this.listViewAlarmInfo.Size = new System.Drawing.Size(587, 140);
            this.listViewAlarmInfo.TabIndex = 40;
            this.listViewAlarmInfo.UseCompatibleStateImageBehavior = false;
            // 
            // dateTimePickerAlarmDaily
            // 
            this.dateTimePickerAlarmDaily.Location = new System.Drawing.Point(87, 192);
            this.dateTimePickerAlarmDaily.Name = "dateTimePickerAlarmDaily";
            this.dateTimePickerAlarmDaily.Size = new System.Drawing.Size(156, 20);
            this.dateTimePickerAlarmDaily.TabIndex = 42;
            // 
            // buttonAlarmInfoDaily
            // 
            this.buttonAlarmInfoDaily.Location = new System.Drawing.Point(271, 193);
            this.buttonAlarmInfoDaily.Name = "buttonAlarmInfoDaily";
            this.buttonAlarmInfoDaily.Size = new System.Drawing.Size(156, 23);
            this.buttonAlarmInfoDaily.TabIndex = 43;
            this.buttonAlarmInfoDaily.Text = "Alarm Info Daily";
            this.buttonAlarmInfoDaily.UseVisualStyleBackColor = true;
            this.buttonAlarmInfoDaily.Click += new System.EventHandler(this.buttonAlarmInfoDaily_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 197);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 13);
            this.label12.TabIndex = 44;
            this.label12.Text = "Select Date:";
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(15, 254);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(587, 327);
            this.chart1.TabIndex = 45;
            this.chart1.Text = "chartGraph";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Graph Information:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 587);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Select Dates:";
            // 
            // dateTimePickerGraphStart
            // 
            this.dateTimePickerGraphStart.Location = new System.Drawing.Point(87, 584);
            this.dateTimePickerGraphStart.Name = "dateTimePickerGraphStart";
            this.dateTimePickerGraphStart.Size = new System.Drawing.Size(156, 20);
            this.dateTimePickerGraphStart.TabIndex = 48;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(249, 591);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 49;
            this.label6.Text = "to";
            // 
            // dateTimePickerGraphEnd
            // 
            this.dateTimePickerGraphEnd.Location = new System.Drawing.Point(271, 585);
            this.dateTimePickerGraphEnd.Name = "dateTimePickerGraphEnd";
            this.dateTimePickerGraphEnd.Size = new System.Drawing.Size(156, 20);
            this.dateTimePickerGraphEnd.TabIndex = 50;
            // 
            // buttonGraphBetweenDates
            // 
            this.buttonGraphBetweenDates.Location = new System.Drawing.Point(446, 584);
            this.buttonGraphBetweenDates.Name = "buttonGraphBetweenDates";
            this.buttonGraphBetweenDates.Size = new System.Drawing.Size(156, 23);
            this.buttonGraphBetweenDates.TabIndex = 51;
            this.buttonGraphBetweenDates.Text = "Show Graph Between Dates";
            this.buttonGraphBetweenDates.UseVisualStyleBackColor = true;
            this.buttonGraphBetweenDates.Click += new System.EventHandler(this.buttonGraphBetweenDates_Click);
            // 
            // checkBoxPH
            // 
            this.checkBoxPH.AutoSize = true;
            this.checkBoxPH.Checked = true;
            this.checkBoxPH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPH.Location = new System.Drawing.Point(456, 231);
            this.checkBoxPH.Name = "checkBoxPH";
            this.checkBoxPH.Size = new System.Drawing.Size(41, 17);
            this.checkBoxPH.TabIndex = 52;
            this.checkBoxPH.Text = "PH";
            this.checkBoxPH.UseVisualStyleBackColor = true;
            this.checkBoxPH.CheckedChanged += new System.EventHandler(this.checkBoxPH_CheckedChanged);
            // 
            // checkBoxCL2
            // 
            this.checkBoxCL2.AutoSize = true;
            this.checkBoxCL2.Checked = true;
            this.checkBoxCL2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCL2.Location = new System.Drawing.Point(557, 232);
            this.checkBoxCL2.Name = "checkBoxCL2";
            this.checkBoxCL2.Size = new System.Drawing.Size(45, 17);
            this.checkBoxCL2.TabIndex = 53;
            this.checkBoxCL2.Text = "CL2";
            this.checkBoxCL2.UseVisualStyleBackColor = true;
            this.checkBoxCL2.CheckedChanged += new System.EventHandler(this.checkBoxCL2_CheckedChanged);
            // 
            // checkBoxNH3
            // 
            this.checkBoxNH3.AutoSize = true;
            this.checkBoxNH3.Checked = true;
            this.checkBoxNH3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNH3.Location = new System.Drawing.Point(503, 231);
            this.checkBoxNH3.Name = "checkBoxNH3";
            this.checkBoxNH3.Size = new System.Drawing.Size(48, 17);
            this.checkBoxNH3.TabIndex = 54;
            this.checkBoxNH3.Text = "NH3";
            this.checkBoxNH3.UseVisualStyleBackColor = true;
            this.checkBoxNH3.CheckedChanged += new System.EventHandler(this.checkBoxNH3_CheckedChanged);
            // 
            // comboBoxTypeGraphs
            // 
            this.comboBoxTypeGraphs.FormattingEnabled = true;
            this.comboBoxTypeGraphs.Location = new System.Drawing.Point(329, 229);
            this.comboBoxTypeGraphs.Name = "comboBoxTypeGraphs";
            this.comboBoxTypeGraphs.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTypeGraphs.TabIndex = 55;
            this.comboBoxTypeGraphs.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypeGraphs_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(257, 233);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 56;
            this.label7.Text = "Graph Type:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 657);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBoxTypeGraphs);
            this.Controls.Add(this.checkBoxNH3);
            this.Controls.Add(this.checkBoxCL2);
            this.Controls.Add(this.checkBoxPH);
            this.Controls.Add(this.buttonGraphBetweenDates);
            this.Controls.Add(this.dateTimePickerGraphEnd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dateTimePickerGraphStart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.buttonAlarmInfoDaily);
            this.Controls.Add(this.dateTimePickerAlarmDaily);
            this.Controls.Add(this.listViewAlarmInfo);
            this.Controls.Add(this.buttonGraphWeek);
            this.Controls.Add(this.buttonGraphDay);
            this.Controls.Add(this.AlarmBetweenDates);
            this.Controls.Add(this.dateTimePickerGraph);
            this.Controls.Add(this.dateTimePickerAlarmEnd);
            this.Controls.Add(this.dateTimePickerAlarmStart);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateTimePickerAlarmStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerAlarmEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerGraph;
        private System.Windows.Forms.Button AlarmBetweenDates;
        private System.Windows.Forms.Button buttonGraphDay;
        private System.Windows.Forms.Button buttonGraphWeek;
        private System.Windows.Forms.ListView listViewAlarmInfo;
        private System.Windows.Forms.DateTimePicker dateTimePickerAlarmDaily;
        private System.Windows.Forms.Button buttonAlarmInfoDaily;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerGraphStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePickerGraphEnd;
        private System.Windows.Forms.Button buttonGraphBetweenDates;
        private System.Windows.Forms.CheckBox checkBoxPH;
        private System.Windows.Forms.CheckBox checkBoxCL2;
        private System.Windows.Forms.CheckBox checkBoxNH3;
        private System.Windows.Forms.ComboBox comboBoxTypeGraphs;
        private System.Windows.Forms.Label label7;
    }
}


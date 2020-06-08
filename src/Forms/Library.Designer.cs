namespace Trio.Forms
{
    partial class Library
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.grpboxConfig = new System.Windows.Forms.GroupBox();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.comboEnd = new System.Windows.Forms.ComboBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.comboNum = new System.Windows.Forms.ComboBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.comboStart = new System.Windows.Forms.ComboBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.comboRegion = new System.Windows.Forms.ComboBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.comboDate = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.comboLib = new System.Windows.Forms.ComboBox();
            this.lblLib = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.grpboxConfig.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.iconPictureBox3);
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Controls.Add(this.iconPictureBox2);
            this.panel1.Controls.Add(this.iconPictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 90);
            this.panel1.TabIndex = 0;
            // 
            // iconPictureBox3
            // 
            this.iconPictureBox3.BackColor = System.Drawing.Color.White;
            this.iconPictureBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.ArrowAltCircleLeft;
            this.iconPictureBox3.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox3.IconSize = 45;
            this.iconPictureBox3.Location = new System.Drawing.Point(89, 23);
            this.iconPictureBox3.Name = "iconPictureBox3";
            this.iconPictureBox3.Size = new System.Drawing.Size(45, 45);
            this.iconPictureBox3.TabIndex = 4;
            this.iconPictureBox3.TabStop = false;
            this.iconPictureBox3.Click += new System.EventHandler(this.IconPictureBox3_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWelcome.Location = new System.Drawing.Point(227, 38);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(681, 30);
            this.lblWelcome.TabIndex = 3;
            this.lblWelcome.Click += new System.EventHandler(this.LblWelcome_Click);
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.BackColor = System.Drawing.Color.White;
            this.iconPictureBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.UserClock;
            this.iconPictureBox2.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox2.IconSize = 45;
            this.iconPictureBox2.Location = new System.Drawing.Point(150, 23);
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.Size = new System.Drawing.Size(45, 45);
            this.iconPictureBox2.TabIndex = 2;
            this.iconPictureBox2.TabStop = false;
            this.iconPictureBox2.Click += new System.EventHandler(this.IconPictureBox2_Click);
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.White;
            this.iconPictureBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.iconPictureBox1.IconColor = System.Drawing.SystemColors.ControlText;
            this.iconPictureBox1.IconSize = 45;
            this.iconPictureBox1.Location = new System.Drawing.Point(28, 23);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(45, 45);
            this.iconPictureBox1.TabIndex = 1;
            this.iconPictureBox1.TabStop = false;
            this.iconPictureBox1.Click += new System.EventHandler(this.IconPictureBox1_Click);
            // 
            // grpboxConfig
            // 
            this.grpboxConfig.Controls.Add(this.pnlContainer);
            this.grpboxConfig.Controls.Add(this.btnExecute);
            this.grpboxConfig.Controls.Add(this.comboEnd);
            this.grpboxConfig.Controls.Add(this.lblEnd);
            this.grpboxConfig.Controls.Add(this.comboNum);
            this.grpboxConfig.Controls.Add(this.lblNum);
            this.grpboxConfig.Controls.Add(this.comboStart);
            this.grpboxConfig.Controls.Add(this.lblStart);
            this.grpboxConfig.Controls.Add(this.comboRegion);
            this.grpboxConfig.Controls.Add(this.lblRegion);
            this.grpboxConfig.Controls.Add(this.comboDate);
            this.grpboxConfig.Controls.Add(this.lblDate);
            this.grpboxConfig.Controls.Add(this.comboLib);
            this.grpboxConfig.Controls.Add(this.lblLib);
            this.grpboxConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpboxConfig.Location = new System.Drawing.Point(0, 90);
            this.grpboxConfig.Name = "grpboxConfig";
            this.grpboxConfig.Size = new System.Drawing.Size(934, 253);
            this.grpboxConfig.TabIndex = 1;
            this.grpboxConfig.TabStop = false;
            this.grpboxConfig.Text = "设置";
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContainer.Controls.Add(this.panel3);
            this.pnlContainer.Controls.Add(this.checkBox4);
            this.pnlContainer.Controls.Add(this.lblCondition);
            this.pnlContainer.Controls.Add(this.checkBox3);
            this.pnlContainer.Controls.Add(this.checkBox1);
            this.pnlContainer.Controls.Add(this.checkBox2);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlContainer.Location = new System.Drawing.Point(731, 25);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(200, 225);
            this.pnlContainer.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Location = new System.Drawing.Point(18, 160);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(120, 2);
            this.panel3.TabIndex = 18;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(35, 176);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(151, 27);
            this.checkBox4.TabIndex = 17;
            this.checkBox4.Text = "失败时自动续抢";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(14, 19);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(141, 31);
            this.lblCondition.TabIndex = 13;
            this.lblCondition.Text = "座位筛选条件: ";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(35, 121);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(66, 27);
            this.checkBox3.TabIndex = 16;
            this.checkBox3.Text = "电脑";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(35, 53);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 27);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "电源";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(35, 88);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(66, 27);
            this.checkBox2.TabIndex = 15;
            this.checkBox2.Text = "靠窗";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            this.btnExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnExecute.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Location = new System.Drawing.Point(81, 194);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(605, 39);
            this.btnExecute.TabIndex = 12;
            this.btnExecute.Text = "运行";
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.BtnExecute_Click);
            // 
            // comboEnd
            // 
            this.comboEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboEnd.FormattingEnabled = true;
            this.comboEnd.Location = new System.Drawing.Point(446, 142);
            this.comboEnd.Name = "comboEnd";
            this.comboEnd.Size = new System.Drawing.Size(240, 31);
            this.comboEnd.TabIndex = 11;
            // 
            // lblEnd
            // 
            this.lblEnd.Location = new System.Drawing.Point(353, 146);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(87, 23);
            this.lblEnd.TabIndex = 10;
            this.lblEnd.Text = "结束时间:";
            // 
            // comboNum
            // 
            this.comboNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboNum.FormattingEnabled = true;
            this.comboNum.Location = new System.Drawing.Point(81, 142);
            this.comboNum.Name = "comboNum";
            this.comboNum.Size = new System.Drawing.Size(240, 31);
            this.comboNum.TabIndex = 9;
            // 
            // lblNum
            // 
            this.lblNum.Location = new System.Drawing.Point(12, 145);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(66, 23);
            this.lblNum.TabIndex = 8;
            this.lblNum.Text = "座位号:";
            // 
            // comboStart
            // 
            this.comboStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboStart.FormattingEnabled = true;
            this.comboStart.Location = new System.Drawing.Point(446, 90);
            this.comboStart.Name = "comboStart";
            this.comboStart.Size = new System.Drawing.Size(240, 31);
            this.comboStart.TabIndex = 7;
            this.comboStart.SelectedIndexChanged += new System.EventHandler(this.ComboStart_SelectedIndexChanged);
            // 
            // lblStart
            // 
            this.lblStart.Location = new System.Drawing.Point(353, 93);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(87, 23);
            this.lblStart.TabIndex = 6;
            this.lblStart.Text = "开始时间:";
            // 
            // comboRegion
            // 
            this.comboRegion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRegion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboRegion.FormattingEnabled = true;
            this.comboRegion.Location = new System.Drawing.Point(81, 90);
            this.comboRegion.Name = "comboRegion";
            this.comboRegion.Size = new System.Drawing.Size(240, 31);
            this.comboRegion.TabIndex = 5;
            this.comboRegion.SelectedIndexChanged += new System.EventHandler(this.ComboRegion_SelectedIndexChanged);
            // 
            // lblRegion
            // 
            this.lblRegion.Location = new System.Drawing.Point(27, 93);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(50, 23);
            this.lblRegion.TabIndex = 4;
            this.lblRegion.Text = "区域:";
            // 
            // comboDate
            // 
            this.comboDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboDate.FormattingEnabled = true;
            this.comboDate.Location = new System.Drawing.Point(446, 40);
            this.comboDate.Name = "comboDate";
            this.comboDate.Size = new System.Drawing.Size(240, 31);
            this.comboDate.TabIndex = 3;
            this.comboDate.SelectedIndexChanged += new System.EventHandler(this.ComboDate_SelectedIndexChanged);
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(386, 44);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(48, 23);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "日期:";
            // 
            // comboLib
            // 
            this.comboLib.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboLib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLib.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboLib.FormattingEnabled = true;
            this.comboLib.Location = new System.Drawing.Point(81, 40);
            this.comboLib.Name = "comboLib";
            this.comboLib.Size = new System.Drawing.Size(240, 31);
            this.comboLib.TabIndex = 1;
            this.comboLib.SelectedIndexChanged += new System.EventHandler(this.ComboLib_SelectedIndexChanged);
            // 
            // lblLib
            // 
            this.lblLib.Location = new System.Drawing.Point(27, 44);
            this.lblLib.Name = "lblLib";
            this.lblLib.Size = new System.Drawing.Size(50, 23);
            this.lblLib.TabIndex = 0;
            this.lblLib.Text = "分馆:";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 343);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(934, 353);
            this.txtLog.TabIndex = 2;
            this.txtLog.Enter += new System.EventHandler(this.TxtLog_Enter);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker2_DoWork);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.WorkerSupportsCancellation = true;
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker3_DoWork);
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.WorkerSupportsCancellation = true;
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker4_DoWork);
            // 
            // Library
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(934, 696);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.grpboxConfig);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Library";
            this.Text = "Library";
            this.Load += new System.EventHandler(this.Library_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.grpboxConfig.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpboxConfig;
        public FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        public FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        public System.Windows.Forms.ComboBox comboLib;
        public System.Windows.Forms.Label lblLib;
        public System.Windows.Forms.Button btnExecute;
        public System.Windows.Forms.ComboBox comboEnd;
        public System.Windows.Forms.Label lblEnd;
        public System.Windows.Forms.ComboBox comboNum;
        public System.Windows.Forms.Label lblNum;
        public System.Windows.Forms.ComboBox comboStart;
        public System.Windows.Forms.Label lblStart;
        public System.Windows.Forms.ComboBox comboRegion;
        public System.Windows.Forms.Label lblRegion;
        public System.Windows.Forms.ComboBox comboDate;
        public System.Windows.Forms.Label lblDate;
        public System.Windows.Forms.CheckBox checkBox4;
        public System.Windows.Forms.CheckBox checkBox3;
        public System.Windows.Forms.CheckBox checkBox2;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.Label lblCondition;
        public System.Windows.Forms.Panel pnlContainer;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.TextBox txtLog;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        public System.ComponentModel.BackgroundWorker backgroundWorker4;
        public System.Windows.Forms.Label lblWelcome;
        public FontAwesome.Sharp.IconPictureBox iconPictureBox3;
    }
}
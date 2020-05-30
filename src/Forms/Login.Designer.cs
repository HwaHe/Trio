namespace Trio.Forms
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.picUsr = new System.Windows.Forms.PictureBox();
            this.comboUsr = new System.Windows.Forms.ComboBox();
            this.picPwd = new System.Windows.Forms.PictureBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblStateHint = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.iconPicState = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUsr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPicState)).BeginInit();
            this.SuspendLayout();
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(401, 114);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(120, 120);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // picUsr
            // 
            this.picUsr.Image = ((System.Drawing.Image)(resources.GetObject("picUsr.Image")));
            this.picUsr.Location = new System.Drawing.Point(304, 261);
            this.picUsr.Name = "picUsr";
            this.picUsr.Size = new System.Drawing.Size(32, 32);
            this.picUsr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUsr.TabIndex = 1;
            this.picUsr.TabStop = false;
            // 
            // comboUsr
            // 
            this.comboUsr.BackColor = System.Drawing.Color.White;
            this.comboUsr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboUsr.FormattingEnabled = true;
            this.comboUsr.Location = new System.Drawing.Point(342, 262);
            this.comboUsr.Name = "comboUsr";
            this.comboUsr.Size = new System.Drawing.Size(300, 31);
            this.comboUsr.TabIndex = 2;
            this.comboUsr.SelectedIndexChanged += new System.EventHandler(this.ComboUsr_SelectedIndexChanged);
            // 
            // picPwd
            // 
            this.picPwd.Image = ((System.Drawing.Image)(resources.GetObject("picPwd.Image")));
            this.picPwd.Location = new System.Drawing.Point(304, 320);
            this.picPwd.Name = "picPwd";
            this.picPwd.Size = new System.Drawing.Size(32, 32);
            this.picPwd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPwd.TabIndex = 3;
            this.picPwd.TabStop = false;
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.Location = new System.Drawing.Point(342, 320);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(300, 22);
            this.txtPwd.TabIndex = 5;
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Checked = true;
            this.chkRemember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemember.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRemember.Location = new System.Drawing.Point(313, 396);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(78, 22);
            this.chkRemember.TabIndex = 6;
            this.chkRemember.Text = "记住我";
            this.chkRemember.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Location = new System.Drawing.Point(304, 424);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(338, 43);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // lblHint
            // 
            this.lblHint.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHint.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.Location = new System.Drawing.Point(417, 396);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(225, 22);
            this.lblHint.TabIndex = 8;
            this.lblHint.Text = "不在自己的电脑上请不要勾选";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(304, 359);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 4);
            this.panel1.TabIndex = 9;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1_ProgressChanged);
            // 
            // lblStateHint
            // 
            this.lblStateHint.AutoSize = true;
            this.lblStateHint.Location = new System.Drawing.Point(23, 650);
            this.lblStateHint.Name = "lblStateHint";
            this.lblStateHint.Size = new System.Drawing.Size(184, 23);
            this.lblStateHint.TabIndex = 10;
            this.lblStateHint.Text = "图书馆服务器连接状态:";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblState.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblState.Location = new System.Drawing.Point(243, 649);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(74, 20);
            this.lblState.TabIndex = 11;
            this.lblState.Text = "Unknown";
            // 
            // iconPicState
            // 
            this.iconPicState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.iconPicState.ForeColor = System.Drawing.Color.Silver;
            this.iconPicState.IconChar = FontAwesome.Sharp.IconChar.Circle;
            this.iconPicState.IconColor = System.Drawing.Color.Silver;
            this.iconPicState.IconSize = 24;
            this.iconPicState.Location = new System.Drawing.Point(213, 649);
            this.iconPicState.Name = "iconPicState";
            this.iconPicState.Size = new System.Drawing.Size(24, 24);
            this.iconPicState.TabIndex = 12;
            this.iconPicState.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(934, 694);
            this.Controls.Add(this.iconPicState);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblStateHint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.chkRemember);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.picPwd);
            this.Controls.Add(this.comboUsr);
            this.Controls.Add(this.picUsr);
            this.Controls.Add(this.picLogo);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Login";
            this.Text = "Login";
            this.Activated += new System.EventHandler(this.Login_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUsr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPicState)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox picUsr;
        private System.Windows.Forms.ComboBox comboUsr;
        private System.Windows.Forms.PictureBox picPwd;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblStateHint;
        private System.Windows.Forms.Label lblState;
        public FontAwesome.Sharp.IconPictureBox iconPicState;
    }
}
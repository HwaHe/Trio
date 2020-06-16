namespace Trio.Forms
{
    partial class News
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
            this.picHome = new FontAwesome.Sharp.IconPictureBox();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            this.pnlOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // picHome
            // 
            this.picHome.BackColor = System.Drawing.Color.White;
            this.picHome.ForeColor = System.Drawing.SystemColors.ControlText;
            this.picHome.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.picHome.IconColor = System.Drawing.SystemColors.ControlText;
            this.picHome.IconSize = 36;
            this.picHome.Location = new System.Drawing.Point(22, 18);
            this.picHome.Name = "picHome";
            this.picHome.Size = new System.Drawing.Size(36, 36);
            this.picHome.TabIndex = 0;
            this.picHome.TabStop = false;
            this.picHome.Click += new System.EventHandler(this.PicHome_Click);
            // 
            // pnlOptions
            // 
            this.pnlOptions.BackColor = System.Drawing.Color.LightGray;
            this.pnlOptions.Controls.Add(this.picHome);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOptions.Location = new System.Drawing.Point(0, 0);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(934, 110);
            this.pnlOptions.TabIndex = 0;
            this.pnlOptions.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlOptions_Paint);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 110);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(934, 586);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // News
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(934, 696);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.pnlOptions);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "News";
            this.Text = "News";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.News_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            this.pnlOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconPictureBox picHome;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
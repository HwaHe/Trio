namespace Trio
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.picItem = new FontAwesome.Sharp.IconPictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlSidemenu = new System.Windows.Forms.Panel();
            this.btnAbout = new FontAwesome.Sharp.IconButton();
            this.btnSettings = new FontAwesome.Sharp.IconButton();
            this.btnSeat = new FontAwesome.Sharp.IconButton();
            this.pnlWallpaper = new System.Windows.Forms.Panel();
            this.btnBA = new System.Windows.Forms.Button();
            this.btnSW = new System.Windows.Forms.Button();
            this.btnBing = new System.Windows.Forms.Button();
            this.btnWallpaper = new FontAwesome.Sharp.IconButton();
            this.pnlNews = new System.Windows.Forms.Panel();
            this.btnReadingList = new System.Windows.Forms.Button();
            this.btnCs = new System.Windows.Forms.Button();
            this.btnBkjw = new System.Windows.Forms.Button();
            this.btnWhu = new System.Windows.Forms.Button();
            this.btnNews = new FontAwesome.Sharp.IconButton();
            this.pnlStatusBar = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlChildForm = new System.Windows.Forms.Panel();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).BeginInit();
            this.pnlSidemenu.SuspendLayout();
            this.pnlWallpaper.SuspendLayout();
            this.pnlNews.SuspendLayout();
            this.pnlStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlChildForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.pnlTitle.Controls.Add(this.picItem);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Controls.Add(this.btnMax);
            this.pnlTitle.Controls.Add(this.btnRestore);
            this.pnlTitle.Controls.Add(this.btnMin);
            this.pnlTitle.Controls.Add(this.btnExit);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1184, 38);
            this.pnlTitle.TabIndex = 0;
            this.pnlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PnlTitle_MouseDown);
            // 
            // picItem
            // 
            this.picItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.picItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picItem.BackgroundImage")));
            this.picItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.picItem.IconChar = FontAwesome.Sharp.IconChar.None;
            this.picItem.IconColor = System.Drawing.SystemColors.ControlText;
            this.picItem.IconSize = 36;
            this.picItem.Location = new System.Drawing.Point(24, 2);
            this.picItem.Name = "picItem";
            this.picItem.Size = new System.Drawing.Size(36, 36);
            this.picItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picItem.TabIndex = 6;
            this.picItem.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(50, 1);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(72, 36);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Trio";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMax.BackgroundImage")));
            this.btnMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMax.FlatAppearance.BorderSize = 0;
            this.btnMax.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMax.Location = new System.Drawing.Point(1076, 0);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(54, 36);
            this.btnMax.TabIndex = 1;
            this.btnMax.UseVisualStyleBackColor = true;
            this.btnMax.Click += new System.EventHandler(this.BtnMax_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRestore.BackgroundImage")));
            this.btnRestore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRestore.FlatAppearance.BorderSize = 0;
            this.btnRestore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Location = new System.Drawing.Point(1076, 0);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(54, 36);
            this.btnRestore.TabIndex = 3;
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMin.BackgroundImage")));
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Location = new System.Drawing.Point(1022, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(54, 36);
            this.btnMin.TabIndex = 2;
            this.btnMin.UseVisualStyleBackColor = true;
            this.btnMin.Click += new System.EventHandler(this.BtnMin_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(1130, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 38);
            this.btnExit.TabIndex = 0;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // pnlSidemenu
            // 
            this.pnlSidemenu.AutoScroll = true;
            this.pnlSidemenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlSidemenu.Controls.Add(this.btnAbout);
            this.pnlSidemenu.Controls.Add(this.btnSettings);
            this.pnlSidemenu.Controls.Add(this.btnSeat);
            this.pnlSidemenu.Controls.Add(this.pnlWallpaper);
            this.pnlSidemenu.Controls.Add(this.btnWallpaper);
            this.pnlSidemenu.Controls.Add(this.pnlNews);
            this.pnlSidemenu.Controls.Add(this.btnNews);
            this.pnlSidemenu.Controls.Add(this.pnlStatusBar);
            this.pnlSidemenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidemenu.Location = new System.Drawing.Point(0, 38);
            this.pnlSidemenu.Name = "pnlSidemenu";
            this.pnlSidemenu.Size = new System.Drawing.Size(250, 694);
            this.pnlSidemenu.TabIndex = 1;
            // 
            // btnAbout
            // 
            this.btnAbout.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAbout.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(136)))));
            this.btnAbout.IconChar = FontAwesome.Sharp.IconChar.Newspaper;
            this.btnAbout.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(136)))));
            this.btnAbout.IconSize = 45;
            this.btnAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbout.Location = new System.Drawing.Point(0, 792);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Padding = new System.Windows.Forms.Padding(15, 0, 30, 0);
            this.btnAbout.Rotation = 0D;
            this.btnAbout.Size = new System.Drawing.Size(229, 70);
            this.btnAbout.TabIndex = 7;
            this.btnAbout.Text = "About";
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.BtnAbout_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnSettings.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(136)))));
            this.btnSettings.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.btnSettings.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(136)))));
            this.btnSettings.IconSize = 45;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.Location = new System.Drawing.Point(0, 722);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(15, 0, 30, 0);
            this.btnSettings.Rotation = 0D;
            this.btnSettings.Size = new System.Drawing.Size(229, 70);
            this.btnSettings.TabIndex = 6;
            this.btnSettings.Text = "Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // btnSeat
            // 
            this.btnSeat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSeat.FlatAppearance.BorderSize = 0;
            this.btnSeat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeat.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnSeat.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSeat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(136)))));
            this.btnSeat.IconChar = FontAwesome.Sharp.IconChar.BookReader;
            this.btnSeat.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(136)))));
            this.btnSeat.IconSize = 45;
            this.btnSeat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeat.Location = new System.Drawing.Point(0, 652);
            this.btnSeat.Name = "btnSeat";
            this.btnSeat.Padding = new System.Windows.Forms.Padding(15, 0, 30, 0);
            this.btnSeat.Rotation = 0D;
            this.btnSeat.Size = new System.Drawing.Size(229, 70);
            this.btnSeat.TabIndex = 5;
            this.btnSeat.Text = "Library Seat";
            this.btnSeat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSeat.UseVisualStyleBackColor = true;
            this.btnSeat.Click += new System.EventHandler(this.BtnSeat_Click);
            // 
            // pnlWallpaper
            // 
            this.pnlWallpaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlWallpaper.Controls.Add(this.btnBA);
            this.pnlWallpaper.Controls.Add(this.btnSW);
            this.pnlWallpaper.Controls.Add(this.btnBing);
            this.pnlWallpaper.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWallpaper.Location = new System.Drawing.Point(0, 484);
            this.pnlWallpaper.Name = "pnlWallpaper";
            this.pnlWallpaper.Size = new System.Drawing.Size(229, 168);
            this.pnlWallpaper.TabIndex = 4;
            // 
            // btnBA
            // 
            this.btnBA.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBA.FlatAppearance.BorderSize = 0;
            this.btnBA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnBA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBA.ForeColor = System.Drawing.Color.Black;
            this.btnBA.Location = new System.Drawing.Point(0, 112);
            this.btnBA.Name = "btnBA";
            this.btnBA.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnBA.Size = new System.Drawing.Size(229, 56);
            this.btnBA.TabIndex = 2;
            this.btnBA.Text = "彼岸图网";
            this.btnBA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBA.UseVisualStyleBackColor = true;
            this.btnBA.Click += new System.EventHandler(this.BtnBA_Click);
            // 
            // btnSW
            // 
            this.btnSW.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSW.FlatAppearance.BorderSize = 0;
            this.btnSW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSW.ForeColor = System.Drawing.Color.Black;
            this.btnSW.Location = new System.Drawing.Point(0, 56);
            this.btnSW.Name = "btnSW";
            this.btnSW.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnSW.Size = new System.Drawing.Size(229, 56);
            this.btnSW.TabIndex = 1;
            this.btnSW.Text = "StreetWill";
            this.btnSW.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSW.UseVisualStyleBackColor = true;
            this.btnSW.Click += new System.EventHandler(this.BtnSW_Click);
            // 
            // btnBing
            // 
            this.btnBing.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBing.FlatAppearance.BorderSize = 0;
            this.btnBing.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnBing.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBing.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBing.ForeColor = System.Drawing.Color.Black;
            this.btnBing.Location = new System.Drawing.Point(0, 0);
            this.btnBing.Name = "btnBing";
            this.btnBing.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnBing.Size = new System.Drawing.Size(229, 56);
            this.btnBing.TabIndex = 0;
            this.btnBing.Text = "Bing";
            this.btnBing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBing.UseVisualStyleBackColor = true;
            this.btnBing.Click += new System.EventHandler(this.BtnBing_Click);
            // 
            // btnWallpaper
            // 
            this.btnWallpaper.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnWallpaper.FlatAppearance.BorderSize = 0;
            this.btnWallpaper.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnWallpaper.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWallpaper.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnWallpaper.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWallpaper.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(136)))));
            this.btnWallpaper.IconChar = FontAwesome.Sharp.IconChar.PhotoVideo;
            this.btnWallpaper.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(136)))));
            this.btnWallpaper.IconSize = 45;
            this.btnWallpaper.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWallpaper.Location = new System.Drawing.Point(0, 414);
            this.btnWallpaper.Name = "btnWallpaper";
            this.btnWallpaper.Padding = new System.Windows.Forms.Padding(15, 0, 30, 0);
            this.btnWallpaper.Rotation = 0D;
            this.btnWallpaper.Size = new System.Drawing.Size(229, 70);
            this.btnWallpaper.TabIndex = 3;
            this.btnWallpaper.Text = "Wallpaper";
            this.btnWallpaper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWallpaper.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnWallpaper.UseVisualStyleBackColor = true;
            this.btnWallpaper.Click += new System.EventHandler(this.BtnWallpaper_Click);
            // 
            // pnlNews
            // 
            this.pnlNews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlNews.Controls.Add(this.btnReadingList);
            this.pnlNews.Controls.Add(this.btnCs);
            this.pnlNews.Controls.Add(this.btnBkjw);
            this.pnlNews.Controls.Add(this.btnWhu);
            this.pnlNews.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNews.Location = new System.Drawing.Point(0, 190);
            this.pnlNews.Name = "pnlNews";
            this.pnlNews.Size = new System.Drawing.Size(229, 224);
            this.pnlNews.TabIndex = 2;
            // 
            // btnReadingList
            // 
            this.btnReadingList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReadingList.FlatAppearance.BorderSize = 0;
            this.btnReadingList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnReadingList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReadingList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadingList.ForeColor = System.Drawing.Color.Black;
            this.btnReadingList.Location = new System.Drawing.Point(0, 168);
            this.btnReadingList.Name = "btnReadingList";
            this.btnReadingList.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnReadingList.Size = new System.Drawing.Size(229, 56);
            this.btnReadingList.TabIndex = 3;
            this.btnReadingList.Text = "Reading List";
            this.btnReadingList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReadingList.UseVisualStyleBackColor = true;
            this.btnReadingList.Click += new System.EventHandler(this.BtnReadingList_Click);
            // 
            // btnCs
            // 
            this.btnCs.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCs.FlatAppearance.BorderSize = 0;
            this.btnCs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCs.ForeColor = System.Drawing.Color.Black;
            this.btnCs.Location = new System.Drawing.Point(0, 112);
            this.btnCs.Name = "btnCs";
            this.btnCs.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnCs.Size = new System.Drawing.Size(229, 56);
            this.btnCs.TabIndex = 2;
            this.btnCs.Text = "Faculty";
            this.btnCs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCs.UseVisualStyleBackColor = true;
            this.btnCs.Click += new System.EventHandler(this.BtnCs_Click);
            // 
            // btnBkjw
            // 
            this.btnBkjw.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBkjw.FlatAppearance.BorderSize = 0;
            this.btnBkjw.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnBkjw.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBkjw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBkjw.ForeColor = System.Drawing.Color.Black;
            this.btnBkjw.Location = new System.Drawing.Point(0, 56);
            this.btnBkjw.Name = "btnBkjw";
            this.btnBkjw.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnBkjw.Size = new System.Drawing.Size(229, 56);
            this.btnBkjw.TabIndex = 1;
            this.btnBkjw.Text = "BKJW";
            this.btnBkjw.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBkjw.UseVisualStyleBackColor = true;
            this.btnBkjw.Click += new System.EventHandler(this.BtnBkjw_Click);
            // 
            // btnWhu
            // 
            this.btnWhu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnWhu.FlatAppearance.BorderSize = 0;
            this.btnWhu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnWhu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnWhu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWhu.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWhu.ForeColor = System.Drawing.Color.Black;
            this.btnWhu.Location = new System.Drawing.Point(0, 0);
            this.btnWhu.Name = "btnWhu";
            this.btnWhu.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.btnWhu.Size = new System.Drawing.Size(229, 56);
            this.btnWhu.TabIndex = 0;
            this.btnWhu.Text = "WHU";
            this.btnWhu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWhu.UseVisualStyleBackColor = true;
            this.btnWhu.Click += new System.EventHandler(this.BtnWhu_Click);
            // 
            // btnNews
            // 
            this.btnNews.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNews.FlatAppearance.BorderSize = 0;
            this.btnNews.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnNews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNews.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnNews.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNews.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(136)))));
            this.btnNews.IconChar = FontAwesome.Sharp.IconChar.Newspaper;
            this.btnNews.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(136)))));
            this.btnNews.IconSize = 45;
            this.btnNews.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNews.Location = new System.Drawing.Point(0, 120);
            this.btnNews.Name = "btnNews";
            this.btnNews.Padding = new System.Windows.Forms.Padding(15, 0, 30, 0);
            this.btnNews.Rotation = 0D;
            this.btnNews.Size = new System.Drawing.Size(229, 70);
            this.btnNews.TabIndex = 1;
            this.btnNews.Text = "News";
            this.btnNews.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNews.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNews.UseVisualStyleBackColor = true;
            this.btnNews.Click += new System.EventHandler(this.BtnNews_Click);
            // 
            // pnlStatusBar
            // 
            this.pnlStatusBar.Controls.Add(this.picLogo);
            this.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatusBar.Location = new System.Drawing.Point(0, 0);
            this.pnlStatusBar.Name = "pnlStatusBar";
            this.pnlStatusBar.Size = new System.Drawing.Size(229, 120);
            this.pnlStatusBar.TabIndex = 0;
            this.pnlStatusBar.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlStatusBar_Paint);
            // 
            // picLogo
            // 
            this.picLogo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(27, 21);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(176, 79);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            this.picLogo.Click += new System.EventHandler(this.PicLogo_Click);
            // 
            // pnlChildForm
            // 
            this.pnlChildForm.Controls.Add(this.picHome);
            this.pnlChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChildForm.Location = new System.Drawing.Point(250, 38);
            this.pnlChildForm.Name = "pnlChildForm";
            this.pnlChildForm.Size = new System.Drawing.Size(934, 694);
            this.pnlChildForm.TabIndex = 2;
            // 
            // picHome
            // 
            this.picHome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picHome.BackColor = System.Drawing.Color.White;
            this.picHome.Image = ((System.Drawing.Image)(resources.GetObject("picHome.Image")));
            this.picHome.Location = new System.Drawing.Point(202, 213);
            this.picHome.Name = "picHome";
            this.picHome.Size = new System.Drawing.Size(575, 216);
            this.picHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHome.TabIndex = 0;
            this.picHome.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 732);
            this.Controls.Add(this.pnlChildForm);
            this.Controls.Add(this.pnlSidemenu);
            this.Controls.Add(this.pnlTitle);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1184, 732);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trio";
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).EndInit();
            this.pnlSidemenu.ResumeLayout(false);
            this.pnlWallpaper.ResumeLayout(false);
            this.pnlNews.ResumeLayout(false);
            this.pnlStatusBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlChildForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlSidemenu;
        private System.Windows.Forms.Panel pnlChildForm;
        private FontAwesome.Sharp.IconButton btnNews;
        private System.Windows.Forms.Panel pnlStatusBar;
        private FontAwesome.Sharp.IconPictureBox picItem;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel pnlNews;
        private System.Windows.Forms.Button btnWhu;
        private System.Windows.Forms.Button btnReadingList;
        private System.Windows.Forms.Button btnCs;
        private System.Windows.Forms.Button btnBkjw;
        private System.Windows.Forms.Panel pnlWallpaper;
        private System.Windows.Forms.Button btnBA;
        private System.Windows.Forms.Button btnSW;
        private System.Windows.Forms.Button btnBing;
        private FontAwesome.Sharp.IconButton btnWallpaper;
        private FontAwesome.Sharp.IconButton btnAbout;
        private FontAwesome.Sharp.IconButton btnSeat;
        private FontAwesome.Sharp.IconButton btnSettings;
        private System.Windows.Forms.PictureBox picHome;
    }
}


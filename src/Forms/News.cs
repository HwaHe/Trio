using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.IO;
using System.Reflection;


namespace Trio.Forms
{
    public partial class News : Form
    {
        public static Main main;
        public static News news;
        //private List<IconButton> buttons = new List<IconButton>();
        //private List<IconButton> WLButtons = new List<IconButton>();  //watch later 按钮

        public List<(string, string)> newsTitle = new List<(string, string)>();
        public Dictionary<string, string> newsUrl = new Dictionary<string, string>();  //title,url

        CircularButton circularButton1 = new CircularButton();
        CircularButton circularButton2 = new CircularButton();
        CircularButton circularButton3 = new CircularButton();
        ToolTip toolTip = new ToolTip();

        public News() 
        {
            InitializeComponent();
        }
        public News(List<(string, string)> newsTitle, Dictionary<string, string> newsUrl)   //新闻列表  从三个按钮进入
        {
            
            //字典初始化
            this.newsTitle = newsTitle;
            this.newsUrl = newsUrl;

            InitializeComponent();
            addCircularButton();
            circularButton1Click();  //初始显示新闻栏

            /*
            //提示框
            ToolTip toolTip = new ToolTip();
            //toolTip.IsBalloon = true;

            foreach (var title in newsTitle)
            {
                IconButton button = new IconButton();
                button.Size = new Size(flowLayoutPanel1.Size.Width - 100, 50);
                
                button.Text = title;
                button.MouseDown += buttonClick;
                toolTip.SetToolTip(button, "左键进入新闻，右键加入待看列表");
                flowLayoutPanel1.Controls.Add(button);
                //buttons.Add(button);
            }*/
        }
        public News(List<string> newsTitle, Dictionary<string, string> newsUrl, bool WL)   //新闻列表  从待看列表中进入
        {
            //字典初始化
            //this.newsTitle = newsTitle;
            this.newsUrl = newsUrl;

            InitializeComponent();

            //提示框
            //ToolTip toolTip = new ToolTip();
            //toolTip.IsBalloon = true;

            tableLayoutPanel1.RowCount = newsTitle.Count;
            foreach (var title in newsTitle)
            {
                string url = newsUrl[title];
                IconButton button = new IconButton();
                //button.Size = new Size(tableLayoutPanel1.Size.Width - 10, 50);
                button.Size = new Size(930, 50);
                button.Text = title;
                button.Anchor = AnchorStyles.Top;
                button.MouseDown += buttonClick;
                toolTip.SetToolTip(button, "左键进入新闻，右键从待看列表中删除");

                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                button.ContextMenuStrip = contextMenuStrip;

                ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem();
                toolStripMenuItem1.Text = "用浏览器打开";
                toolStripMenuItem1.Click += delegate { openWithWebb(url); };

                ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
                toolStripMenuItem2.Click += delegate { delWL(title, url); };
                toolStripMenuItem2.Text = "从待看列表中删除";

                contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                toolStripMenuItem1,
                toolStripMenuItem2,});

                tableLayoutPanel1.Controls.Add(button);
                //buttons.Add(button);
            }
            
            if(newsTitle.Count == 0)
            {
                tableLayoutPanel1.RowCount = 1;
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                try
                {
                    pictureBox.Load(@"./assets/404.jpg");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    //throw;
                }
                pictureBox.Anchor = AnchorStyles.None;
                tableLayoutPanel1.Controls.Add(pictureBox);
                
            }
        }
        public News(string url)   //打开新闻内容页
        {
            InitializeComponent();
            //添加前进后退按钮


            tableLayoutPanel1.Hide();
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Size = tableLayoutPanel1.Size;
            webBrowser.Dock = DockStyle.Fill;
            //webBrowser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.Controls.Add(webBrowser);
            try
            {
                webBrowser.Navigate(url);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void addCircularButton()   //三个圆角按钮
        {
            int width = 150;
            
            circularButton1.Text = "新闻";
            circularButton1.Size = new Size(90, 45);
            circularButton1.Location = new Point(90, 23);
            circularButton1.Click += delegate { circularButton1Click(); };
            this.pnlOptions.Controls.Add(circularButton1);

            circularButton2.Text = "通知";
            circularButton2.Size = new Size(90, 45);
            circularButton2.Location = new Point(90 + width, 23);
            circularButton2.Click += delegate { circularButton2Click(); };
            this.pnlOptions.Controls.Add(circularButton2);

            circularButton3.Text = "学术";
            circularButton3.Size = new Size(90, 45);
            circularButton3.Location = new Point(90 + 2 * width, 23);
            circularButton3.Click += delegate { circularButton3Click(); };
            this.pnlOptions.Controls.Add(circularButton3);
        }

        
        private void circularButton1Click()
        {
            tableLayoutPanel1.Controls.Clear();
            int cnt = 0;
            foreach (var title in newsTitle)
            {
                if (title.Item2 == "新闻")
                {
                    cnt++;
                }
            }
            if(cnt > 0 )
            {
                tableLayoutPanel1.RowCount = cnt;
            }
            foreach (var title in newsTitle)
            {
                if(title.Item2 == "新闻")
                {
                    addButton(title.Item1, newsUrl[title.Item1]);
                }
            }
        }
        private void circularButton2Click()
        {
            tableLayoutPanel1.Controls.Clear();
            
            int cnt = 0;
            foreach (var title in newsTitle)
            {
                if (title.Item2 == "通知")
                {
                    cnt++;
                }
            }
            if (cnt > 0)
            {
                tableLayoutPanel1.RowCount = cnt;
            }
            foreach (var title in newsTitle)
            {
                if (title.Item2 == "通知")
                {
                    addButton(title.Item1, newsUrl[title.Item1]);
                }
            }
        }
        private void circularButton3Click()
        {
            tableLayoutPanel1.Controls.Clear();
            int cnt = 0;
            foreach (var title in newsTitle)
            {
                if (title.Item2 == "学术")
                {
                    cnt++;
                }
            }
            if (cnt > 0)
            {
                tableLayoutPanel1.RowCount = cnt;
            }
            foreach (var title in newsTitle)
            {
                if (title.Item2 == "学术")
                {
                    addButton(title.Item1, newsUrl[title.Item1]);                  
                }
            }
            //学术按钮要特判
            if(cnt == 0)
            {
                tableLayoutPanel1.RowCount = 1;
                //tableLayoutPanel1.Hide();
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                try
                {
                    pictureBox.Load(@"./assets/404.jpg");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    //throw;
                }
                pictureBox.Anchor = AnchorStyles.Top;
                tableLayoutPanel1.Controls.Add(pictureBox);
            }
        }
        private void buttonClick(object sender, MouseEventArgs e)  //点击标题进入新闻页
        {

            Button b = (Button)sender;
            if(e.Button == MouseButtons.Left)  //左键开新闻页
            {
                Forms.News news = new Forms.News(newsUrl[b.Text]);
                main.OpenChildForm(news);
            }
            else   ////加入待看列表
            {
                //addWL(b.Text, newsUrl[b.Text]);
            }
                
        }
        private void WLButtonClick(object sender, MouseEventArgs e)  //点击标题进入新闻页  待看列表
        {
            Button b = (Button)sender;
            if (e.Button == MouseButtons.Left)  //左键开新闻页
            {
                Forms.News news = new Forms.News(newsUrl[b.Text]);
                main.OpenChildForm(news);
            }
            else   ////从待看列表中删除
            {
                //delWL(b.Text, newsUrl[b.Text]);
            }

        }
        private void addButton(string title, string url)   //添加标题的按钮
        {
            IconButton button = new IconButton();
            button.Size = new Size(930, 50);
            button.Text = title;
            button.Anchor = AnchorStyles.Top;
            button.MouseDown += buttonClick;

            //ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(button, "左键进入新闻，右键加入待看列表");

            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            button.ContextMenuStrip = contextMenuStrip;

            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem1.Text = "用浏览器打开";
            toolStripMenuItem1.Click += delegate { openWithWebb(url); };

            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem2.Click += delegate { addWL(title, url); };
            toolStripMenuItem2.Text = "加入待看列表";

            contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripMenuItem1,
            toolStripMenuItem2,});

            tableLayoutPanel1.Controls.Add(button);
            
            //button.Anchor = AnchorStyles.Left | AnchorStyles.Right; 
        }
        private void addWL(string title, string url)  //加入待看列表
        {
            string connstr = "Data Source=newstb.sqlite;Version=3;";
            //插入到数据库
            using (SQLiteConnection connection = new SQLiteConnection(connstr))
            {
                string command = "INSERT INTO newstable (title,url)" +
                    "VALUES('" + title + "','" + url + "')";
                SQLiteCommand cmd = new SQLiteCommand(command, connection);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        private void delWL(string title, string url)   //从待看列表中删除
        {
            string connstr = "Data Source=newstb.sqlite;Version=3;";
            //删除数据库记录
            using (SQLiteConnection connection = new SQLiteConnection(connstr))
            {
                string command = "DELETE " + 
                    "FROM newstable " +
                    "WHERE url='" + url + "'";
                SQLiteCommand cmd = new SQLiteCommand(command, connection);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            List<string> titles = new List<string>();
            Dictionary<string, string> urls = new Dictionary<string, string>();


            //读数据库
            using (SQLiteConnection connection = new SQLiteConnection(connstr))
            {
                string command = "select * from newstable";
                SQLiteCommand cmd = new SQLiteCommand(command, connection);
                connection.Open();
                SQLiteDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    titles.Add(dataReader["title"].ToString());
                    urls.Add(dataReader["title"].ToString(), dataReader["url"].ToString());
                }
                dataReader.Close();
            }
            Forms.News news = new Forms.News(titles, urls, true);
            main.OpenChildForm(news);

        }
        private void News_Paint(object sender, PaintEventArgs e)
        {
            /*     
            Graphics pnlLine = pnlOptions.CreateGraphics();
            Pen blackPen = new Pen(Color.FromArgb(220, 220, 220), 1.5f);
            pnlLine.DrawLine(blackPen, 0, 119, 934, 119);*/
        }

        private void openWithWebb(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
        private void PicHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pnlOptions_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void 用浏览器打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 加入待看列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

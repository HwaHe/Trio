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


namespace Trio.Forms
{
    public partial class News : Form
    {
        public static Main main;
        public static News news;
        //private List<IconButton> buttons = new List<IconButton>();
        //private List<IconButton> WLButtons = new List<IconButton>();  //watch later 按钮

        public List<string> newsTitle = new List<string>();
        public Dictionary<string, string> newsUrl = new Dictionary<string, string>();  //title,url

        public News() 
        {
            InitializeComponent();
        }
        public News(List<string> newsTitle, Dictionary<string, string> newsUrl)   //新闻列表  从三个按钮进入
        {
            //字典初始化
            this.newsTitle = newsTitle;
            this.newsUrl = newsUrl;

            InitializeComponent();
            webBrowser.Hide();
            //提示框
            ToolTip toolTip = new ToolTip();
            toolTip.IsBalloon = true;

            foreach (var title in newsTitle)
            {
                IconButton button = new IconButton();
                button.Size = new Size(flowLayoutPanel1.Size.Width - 50, 50);
                button.Text = title;
                button.MouseDown += buttonClick;
                toolTip.SetToolTip(button, "左键进入新闻，右键加入待看列表");
                flowLayoutPanel1.Controls.Add(button);
                //buttons.Add(button);
            }
        }
        public News(List<string> newsTitle, Dictionary<string, string> newsUrl, bool WL)   //新闻列表  从待看列表中进入
        {
            //字典初始化
            this.newsTitle = newsTitle;
            this.newsUrl = newsUrl;

            InitializeComponent();
            webBrowser.Hide();
            //提示框
            ToolTip toolTip = new ToolTip();
            toolTip.IsBalloon = true;

            foreach (var title in newsTitle)
            {
                IconButton button = new IconButton();
                button.Size = new Size(flowLayoutPanel1.Size.Width - 50, 50);
                button.Text = title;
                button.MouseDown += WLButtonClick;
                toolTip.SetToolTip(button, "左键进入新闻，右键从待看列表中删除");
                flowLayoutPanel1.Controls.Add(button);
                //buttons.Add(button);
            }
        }
        public News(string url)   //打开新闻内容页
        {
            InitializeComponent();
            
            flowLayoutPanel1.Hide();
            label1.Hide();

            try
            {
                webBrowser.Navigate(url);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void mouseEnter(object sender, MouseEventArgs e)
        {

        }
        private void buttonClick(object sender, MouseEventArgs e)  //进入新闻页
        {

            Button b = (Button)sender;
            if(e.Button == MouseButtons.Left)  //左键开新闻页
            {
                Forms.News news = new Forms.News(newsUrl[b.Text]);
                main.OpenChildForm(news);
            }
            else   ////加入待看列表
            {
                addWL(b.Text, newsUrl[b.Text]);
            }
                
        }
        private void WLButtonClick(object sender, MouseEventArgs e)  //进入新闻页
        {
            Button b = (Button)sender;
            if (e.Button == MouseButtons.Left)  //左键开新闻页
            {
                Forms.News news = new Forms.News(newsUrl[b.Text]);
                main.OpenChildForm(news);
            }
            else   ////从待看列表中删除
            {
                delWL(b.Text, newsUrl[b.Text]);
            }

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
            Graphics pnlLine = pnlOptions.CreateGraphics();
            Pen blackPen = new Pen(Color.FromArgb(220, 220, 220), 1.5f);
            pnlLine.DrawLine(blackPen, 0, 119, 934, 119);
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
    }
}

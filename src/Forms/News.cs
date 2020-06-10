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


namespace Trio.Forms
{
    public partial class News : Form
    {
        public static Main main;
        public static News news;
        private List<IconButton> buttons = new List<IconButton>();
        private List<IconButton> WLButtons = new List<IconButton>();  //watch later 按钮

        public List<string> newsTitle = new List<string>();
        public Dictionary<string, string> newsUrl = new Dictionary<string, string>();

        public News()
        {
            InitializeComponent();
        }
        public News(List<string> newsTitle, Dictionary<string, string> newsUrl)   //新闻列表
        {
            InitializeComponent();
            flowLayoutPanel3.Hide();
            foreach (var title in newsTitle)
            {
                IconButton button = new IconButton();
                button.Size = new Size(flowLayoutPanel1.Size.Width - 100, 50);
                button.Text = title;
                button.Click += delegate { buttonClick(newsUrl[title]); };
                flowLayoutPanel1.Controls.Add(button);
                //buttons.Add(button);

                button = new IconButton();
                button.Size = new Size(50, 50);
                button.Click += delegate { WLButtonClick(title, newsUrl[title]); };
                flowLayoutPanel1.Controls.Add(button);
            }
        }
        public News(List<string> newsTitle, Dictionary<string, string> newsUrl, bool WL)
        {
            InitializeComponent();
            flowLayoutPanel3.Hide();
            label2.Text = "取消稍后再看";
            foreach (var title in newsTitle)
            {
                IconButton button = new IconButton();
                button.Size = new Size(flowLayoutPanel1.Size.Width - 100, 50);
                button.Text = title;
                button.Click += delegate { buttonClick(newsUrl[title]); };
                flowLayoutPanel1.Controls.Add(button);
                //buttons.Add(button);

                button = new IconButton();
                button.Size = new Size(50, 50);
                button.Click += delegate { DWLButtonClick(title, newsUrl[title]); };
                flowLayoutPanel1.Controls.Add(button);
            }
        }
        public News(string url)   //打开新闻内容页
        {
            InitializeComponent();
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Size = flowLayoutPanel3.Size;
            flowLayoutPanel1.Hide();
            //flowLayoutPanel2.Hide();

            flowLayoutPanel3.Controls.Add(webBrowser);
            try
            {
                webBrowser.Navigate(url);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void buttonClick(string url)  //进入新闻页
        {
            Forms.News news = new Forms.News(url);
            main.OpenChildForm(news);
        }
        private void WLButtonClick(string title, string url)  //加入待看列表
        {
            string connstr = "data source=localhost; " +
                "database=newsTB; user id=root;password=1011;" +
                "pooling=false;charset=utf8";//pooling代表是否使用连接池

            //插入到数据库
            using (MySqlConnection connection = new MySqlConnection(connstr))
            {
                string command = "INSERT INTO newstable (title,url)" +
                    "VALUES('" + title + "','" + url + "')";
                MySqlCommand cmd = new MySqlCommand(command, connection);
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
        private void DWLButtonClick(string title, string url)   //从待看列表中删除
        {
            string connstr = "data source=localhost; " +
                "database=newsTB; user id=root;password=1011;" +
                "pooling=false;charset=utf8";//pooling代表是否使用连接池

            //删除数据库记录
            using (MySqlConnection connection = new MySqlConnection(connstr))
            {
                string command = "DELETE " + 
                    "FROM newstable " +
                    "WHERE url='" + url + "'";
                MySqlCommand cmd = new MySqlCommand(command, connection);
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
            

            //插入到数据库
            using (MySqlConnection connection = new MySqlConnection(connstr))
            {
                string command = "select * from newstable";
                MySqlCommand cmd = new MySqlCommand(command, connection);
                connection.Open();
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    titles.Add(mySqlDataReader["title"].ToString());
                    urls.Add(mySqlDataReader["title"].ToString(), mySqlDataReader["url"].ToString());
                }
                mySqlDataReader.Close();
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
    }
}

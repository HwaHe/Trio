﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Net;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using MySql.Data.MySqlClient;
using Trio.Forms;



namespace Trio
{
    public partial class Main : Form
    {
        public static Main main;
        private static readonly List<Button> subMenuButtonList = new List<Button>();
        public bool logged = false;
        private Forms.Library lib;
        private IconButton currentBtn;
        private Form activeForm = null; /// <summary>
        /// /
        /// </summary>
        private Panel leftBorderBtn;
        private string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Trio", wallPaperDir;
        

        public string HomeDir{
            get => homeDir;
            set
            {
                homeDir = value;
                CreateDir();
            }
        }

        public string WallPaperDir
        {
            get => wallPaperDir;
        }

        private Thread ldn;
        public List<(string, string)> whuNewsTitle = new List<(string, string)>();
        public Dictionary<string, string> whuNewsUrl = new Dictionary<string, string>();
        public List<(string, string)> bkNewsTitle = new List<(string, string)>();
        public Dictionary<string, string> bkNewsUrl = new Dictionary<string, string>();
        public List<(string, string)> csNewsTitle = new List<(string, string)>();
        public Dictionary<string, string> csNewsUrl = new Dictionary<string, string>();



        private struct RGBColors
        {
            public static Color colorNews = Color.FromArgb(172, 126, 241);
            public static Color colorWallpaper = Color.FromArgb(249, 118, 176);
            public static Color colorLibrarySeat = Color.FromArgb(253, 138, 114);
            public static Color colorSettings = Color.FromArgb(95, 77, 221);
            //public static Color colorAbout = Color.FromArgb(249, 88, 155);
            public static Color colorAbout = Color.FromArgb(24, 161, 251);
        }

        public void CreateDir()
        {
            try
            {
                if (!Directory.Exists(homeDir))
                {
                    Directory.CreateDirectory(homeDir);
                }

                wallPaperDir = homeDir + @"\WallPapers";
                if (!Directory.Exists(wallPaperDir))
                {
                    Directory.CreateDirectory(wallPaperDir);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Warning", MessageBoxButtons.OK);
            }
        }

        public Main()
        {
            InitializeComponent();

            main = this;
            CheckForIllegalCrossThreadCalls = false;
            //customize default design
            CustomizeDesign();

            // add all sidemenu buttons to the list
            subMenuButtonList.Add(btnWhu);
            subMenuButtonList.Add(btnBkjw);
            subMenuButtonList.Add(btnCs);
            subMenuButtonList.Add(btnReadingList);
            subMenuButtonList.Add(btnBing);
            subMenuButtonList.Add(btnSW);
            subMenuButtonList.Add(btnBA);

            // customize left border shadow
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, btnNews.Size.Height);
            pnlSidemenu.Controls.Add(leftBorderBtn);

            //// remove title bar
            //Text = String.Empty;
            //ControlBox = false;
            //DoubleBuffered = true;

            //limit Maximized bounds to the working area
            MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;  //防止最大化覆盖任务栏

            //先爬新闻标题
            ldn = new Thread(loadNews);
            ldn.Start();


            //每次打开看一下数据库文件在不在
            if (!File.Exists("newstb.sqlite"))
            {
                SQLiteConnection.CreateFile("newstb.sqlite");
                string connstr = "Data Source=newstb.sqlite;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connstr))
                {
                    connection.Open();
                    string command = "create table newstable(title varchar(500) NOT NULL,url varchar(1000) NOT NULL,primary key(url))";
                    SQLiteCommand cmd = new SQLiteCommand(command, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            // 创建目录
            CreateDir();
        }

        /// <summary>
        /// 绘制侧边菜单logo和按钮的分隔线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PnlStatusBar_Paint(object sender, PaintEventArgs e)
        {
            Graphics pnlLine = pnlStatusBar.CreateGraphics();
            Pen blackPen = new Pen(Color.FromArgb(220, 220, 220), 1.5f);
            pnlLine.DrawLine(blackPen, 0, 119, 250, 119);
        }

        #region 自定义标题栏
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BtnExit_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Environment.Exit(0);
        }

        private void BtnMax_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            btnRestore.BringToFront();
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnMax.BringToFront();
        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void PnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region 控制子菜单的展开和关闭
        private void CustomizeDesign()
        {
            pnlNews.Visible = false;
            pnlWallpaper.Visible = false;
        }

        private void HideSubMenu()
        {
            if (pnlNews.Visible == true)
                pnlNews.Visible = false;
            if (pnlWallpaper.Visible == true)
                pnlWallpaper.Visible = false;
        }

        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        #endregion

        #region 控制选择按钮后其余子菜单按钮背景色
        private void CustomizeButton(Button button)
        {
            button.FlatAppearance.MouseOverBackColor = Color.White;
            button.BackColor = Color.White;
        }

        private void ResetButton(Button button)
        {
            foreach (var btn in subMenuButtonList)
                if (btn != button)
                {
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 224, 224);
                    btn.BackColor = Color.FromArgb(224, 224, 224);
                }
        }
        #endregion

        #region 新闻菜单的响应事件
        void loadNews()
        {
            GetNews getNews = new GetNews();
            whuNewsTitle = getNews.whuNewsTitle;
            whuNewsUrl = getNews.whuNewsUrl;
            bkNewsTitle = getNews.bkNewsTitle;
            bkNewsUrl = getNews.bkNewsUrl;
            csNewsTitle = getNews.csNewsTitle;
            csNewsUrl = getNews.csNewsUrl;
        }
        private void BtnNews_Click(object sender, EventArgs e)
        {
            
            ResetButton(null);
            ShowSubMenu(pnlNews);
            CloseActiveForm(true);
            ActivateButton(sender, RGBColors.colorNews);



        }

        private void BtnWhu_Click(object sender, EventArgs e)   //武大官网  1
        {
            ResetButton(btnWhu);
            //TODO
            /*GetNews getNews = new GetNews();
            getNews.getWhuTitle();
            whuNewsTitle = getNews.newsTitle;
            whuNewsUrl = getNews.newsUrl;*/

            ldn.Join();  //阻塞

            Forms.News news = new Forms.News(whuNewsTitle, whuNewsUrl);
            Forms.News.main = main;
            OpenChildForm(news);
            CustomizeButton(btnWhu);
        }

        private void BtnBkjw_Click(object sender, EventArgs e)   //本科生院官网  2
        {
            ResetButton(btnBkjw);
            //TODO
            /*GetNews getNews = new GetNews();
            getNews.getBkTitle();
            bkNewsTitle = getNews.newsTitle;
            bkNewsUrl = getNews.newsUrl;*/

            ldn.Join();  //阻塞

            Forms.News news = new Forms.News(bkNewsTitle, bkNewsUrl);
            Forms.News.main = main;
            OpenChildForm(news);

            CustomizeButton(btnBkjw);
        }

        private void BtnCs_Click(object sender, EventArgs e)   //计算机学院官网   3
        {
            ResetButton(btnCs);
            //TODO
            /*GetNews getNews = new GetNews();
            getNews.getCsTitle();
            csNewsTitle = getNews.newsTitle;
            csNewsUrl = getNews.newsUrl;*/

            ldn.Join();  //阻塞

            Forms.News news = new Forms.News(csNewsTitle, csNewsUrl);
            Forms.News.main = main;
            OpenChildForm(news);

            CustomizeButton(btnCs);
        }

        private void BtnReadingList_Click(object sender, EventArgs e)   //待读列表
        {
            ResetButton(btnReadingList);
            //TODO
            List<string> titles = new List<string>();
            Dictionary<string, string> urls = new Dictionary<string, string>();
            /*string connstr = "data source=localhost; " +
                "database=newstb; user id=root;password=1011;" +
                "pooling=false;charset=utf8";//pooling代表是否使用连接池*/

            //读数据库
            string connstr = "Data Source=newstb.sqlite;Version=3;";
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
            Forms.News.main = main;
            OpenChildForm(news);

            CustomizeButton(btnReadingList);
        }
        #endregion

        #region 壁纸菜单的响应事件

        //提取网页html原码(公用)
        static string GetHTMLDoc(WebClient webClient, string webURL)
        {
            try
            {
                webClient.Headers.Add("User-Agent", "Microsoft Internet Explorer");
                string html = webClient.DownloadString(webURL);
                return html;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "error";
            }

        }

        //解析传入的图片信息，并下载图片(公用)
        static void DownloadPic(string wallPaperDir, string imgInfo)
        {
            string imgId = imgInfo.Split(' ')[1];
            string imgURL = imgInfo.Split(' ')[0];
            string dirPath = wallPaperDir + "\\" + imgId + ".jpg";
            if (File.Exists(dirPath))
            {
                //do nothing
            }
            else
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(imgURL, dirPath);
            }
        
        }

        //声明wallpaper子窗体(公用)
        //static Wallpaper wallpaper = new Wallpaper();

        //利用正则表达式，从原码中解析出图片信息
        //用字符串存储信息，并用空格分隔
        //字符串前半部分是地址，后半部分是id,id是当前年月日(Bing)
        static string[] GetPicInfoBing(string html)
        {
            string pattern = "a class=\"mark\" href=\"(.+?)\"";
            MatchCollection matchCollection = Regex.Matches(html, pattern);
            string[] urls = new string[9];

            //解析一级链接
            for (int i = 0; i < 9; i++)
            {
                string url = @"https://bing.ioliu.cn" + matchCollection[i].Groups[1].Value;
                urls[i] = url;
            }

            string[] picInfos = new string[9];
            string patternNew = "data-progressive=\"(.+?)\\?imageslim\"";

            List<Task> tasks = new List<Task>();
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (int i in arr)
            {
                var task = new Task(() =>
                {
                    WebClient webClient = new WebClient();
                    string htmlOld = GetHTMLDoc(webClient, urls[i]);
                    Match match = Regex.Match(htmlOld, patternNew);
                    string picUrl = match.Groups[1].Value;
                    string id = DateTime.Now.ToString("yyyy-MM-dd").Replace('-', '_') + '_' + "Bing" + '_' + i.ToString();
                    string picInfo = picUrl + ' ' + id;
                    picInfos[i] = picInfo;
                });
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());

            return picInfos;
        }

        //把图片以blob形式存进MySQL数据库(Bing)
        static void SavePicBing(string imgInfo, string wallPaperDir)
        {
            string imgId = imgInfo.Split(' ')[1];
            string dirPath = wallPaperDir + "\\" + imgId + ".jpg";

            //把图片转换为byte格式
            byte[] bytes = null;
            bytes = File.ReadAllBytes(dirPath);

            //如果数据库文件不存在则创建一个数据库文件
            if (File.Exists(@"PicDB.sqlite"))
            {
                //do nothing
            }
            else
            {
                SQLiteConnection.CreateFile("PicDB.sqlite");
            }

            //创建连接
            SQLiteConnection liteConnection;
            liteConnection = new SQLiteConnection("Data Source=PicDB.sqlite;Version=3;");
            //string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            //MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:尝试建表,判断唯一性,插入数据
            string createCommand = "create table if not exists myPicBing(imgId char(20) primary key, pic mediumblob not null);";
            string checkCommand = "select count(imgId) from myPicBing where imgId = @imgId;";
            string insertCommand = "insert into myPicBing(imgId, pic) values(@imgId, @pic);";
            SQLiteCommand commandCreate = new SQLiteCommand(createCommand, liteConnection);
            SQLiteCommand commandCheck = new SQLiteCommand(checkCommand, liteConnection);
            SQLiteCommand commandInsert = new SQLiteCommand(insertCommand, liteConnection);
            //MySqlCommand mySqlCommandCreate = new MySqlCommand(createCommand, mySqlConnection);
            //MySqlCommand mySqlCommandCheck = new MySqlCommand(checkCommand, mySqlConnection);
            //MySqlCommand mySqlCommandInsert = new MySqlCommand(insertCommand, mySqlConnection);
            commandCheck.Parameters.AddWithValue("@imgId", imgId);
            commandInsert.Parameters.AddWithValue("@imgId", imgId);
            commandInsert.Parameters.AddWithValue("@pic", bytes);
            //mySqlCommandCheck.Parameters.AddWithValue("@imgId", imgId);
            //mySqlCommandInsert.Parameters.AddWithValue("@imgId", imgId);
            //mySqlCommandInsert.Parameters.AddWithValue("@pic", bytes);

            //打开连接
            liteConnection.Open();
            //mySqlConnection.Open();

            //执行create
            commandCreate.ExecuteNonQuery();
            //mySqlCommandCreate.ExecuteNonQuery();
            //关闭create
            commandCreate.Dispose();
            //mySqlCommandCreate.Dispose();

            //执行check
            Object check = commandCheck.ExecuteScalar();
            //Object check = mySqlCommandCheck.ExecuteScalar();
            int checknum = Convert.ToInt32(check);
            if (checknum == 0)
            {
                //执行insert
                commandInsert.ExecuteNonQuery();
                //mySqlCommandInsert.ExecuteNonQuery();
                //关闭insert
                commandInsert.Dispose();
                //mySqlCommandInsert.Dispose();
            }
            //关闭check
            commandCheck.Dispose();
            //mySqlCommandCheck.Dispose();

            //关闭连接
            liteConnection.Close();
            //mySqlConnection.Close();
        }

        //读出最新的九张图片，并显示在picturebox1-9中(Bing)
        static void ReadPicBing(Wallpaper wallpaper)
        {

            //创建连接
            SQLiteConnection liteConnection;
            liteConnection = new SQLiteConnection("Data Source=PicDB.sqlite;Version=3;");
            //string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            //MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:读取图片
            //string readCommand = "select imgId, pic from myPicBing order by strftime(imgId, '%Y_%m_%d') desc limit 9;";
            string readCommand = "select imgId, pic from myPicBing order by imgId desc limit 9;";
            SQLiteCommand commandRead = new SQLiteCommand(readCommand, liteConnection);
            //MySqlCommand mySqlCommandRead = new MySqlCommand(readCommand, mySqlConnection);

            //打开连接
            liteConnection.Open();
            //mySqlConnection.Open();

            //执行read
            SQLiteDataReader liteDataReader = commandRead.ExecuteReader();
            //MySqlDataReader mySqlDataReader = mySqlCommandRead.ExecuteReader();

            //关闭read
            commandRead.Dispose();
            //mySqlCommandRead.Dispose();

            PictureBox[] pictureBoxes = new PictureBox[9];
            pictureBoxes[0] = wallpaper.pictureBox1;
            pictureBoxes[1] = wallpaper.pictureBox2;
            pictureBoxes[2] = wallpaper.pictureBox3;
            pictureBoxes[3] = wallpaper.pictureBox4;
            pictureBoxes[4] = wallpaper.pictureBox5;
            pictureBoxes[5] = wallpaper.pictureBox6;
            pictureBoxes[6] = wallpaper.pictureBox7;
            pictureBoxes[7] = wallpaper.pictureBox8;
            pictureBoxes[8] = wallpaper.pictureBox9;

            TextBox[] textBoxes = new TextBox[9];
            textBoxes[0] = wallpaper.textBox1;
            textBoxes[1] = wallpaper.textBox2;
            textBoxes[2] = wallpaper.textBox3;
            textBoxes[3] = wallpaper.textBox4;
            textBoxes[4] = wallpaper.textBox5;
            textBoxes[5] = wallpaper.textBox6;
            textBoxes[6] = wallpaper.textBox7;
            textBoxes[7] = wallpaper.textBox8;
            textBoxes[8] = wallpaper.textBox9;

            if (liteDataReader.HasRows)
            {
                int i = 0;
                byte[] buffer = null;
                while (liteDataReader.Read()) 
                {
                    long len = liteDataReader.GetBytes(1, 0, null, 0, 0);
                    buffer = new byte[len];
                    len = liteDataReader.GetBytes(1, 0, buffer, 0, (int)len);

                    MemoryStream memoryStream = new MemoryStream(buffer);
                    Image image = Image.FromStream(memoryStream);
                    pictureBoxes[i].Image = image;
                    textBoxes[i].Text = liteDataReader[0].ToString();

                    i++;

                }

            }

            //关闭reader
            liteDataReader.Close();
            //mySqlDataReader.Close();

            //关闭连接
            liteConnection.Close();
            //mySqlConnection.Close();

        }

        //利用正则表达式，从原码中解析出图片信息
        //用字符串存储信息，并用空格分隔
        //字符串前半部分是地址，后半部分是id,id是当前年月日(SW)
        static string[] GetPicInfoSW(string html)
        {
            string pattern = "img alt=\"3x2\" class=\"unveil aphoto\" data-src-full-retina=\"(.+?)\"";
            MatchCollection matchCollection = Regex.Matches(html, pattern);
            string[] Infos = new string[9];

            List<Task> tasks = new List<Task>();
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (int i in arr)
            {
                var task = new Task(() =>
                {
                    string url = @"http://streetwill.co" + matchCollection[i].Groups[1].Value;
                    string id = DateTime.Now.ToString("yyyy-MM-dd").Replace('-', '_') + '_' + "SW" + '_' + i.ToString();
                    Infos[i] = url + ' ' + id;
                });
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());

            return Infos;
        }

        //把图片以blob形式存进MySQL数据库(SW)
        static void SavePicSW(string Info, string wallPaperDir)
        {
            string imgId = Info.Split(' ')[1];
            string dirPath = wallPaperDir + "\\" + imgId + ".jpg";

            //把图片转换为byte格式
            byte[] bytes = null;
            bytes = File.ReadAllBytes(dirPath);

            //如果数据库文件不存在则创建一个数据库文件
            if (File.Exists(@"PicDB.sqlite"))
            {
                //do nothing
            }
            else
            {
                SQLiteConnection.CreateFile("PicDB.sqlite");
            }

            //创建连接
            SQLiteConnection liteConnection;
            liteConnection = new SQLiteConnection("Data Source=PicDB.sqlite;Version=3;");
            //string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            //MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:尝试建表,判断唯一性,插入数据
            string createCommand = "create table if not exists myPicSW(imgId char(20) primary key, pic mediumblob not null);";
            string checkCommand = "select count(imgId) from myPicSW where imgId = @imgId;";
            string insertCommand = "insert into myPicSW(imgId, pic) values(@imgId, @pic);";
            SQLiteCommand commandCreate = new SQLiteCommand(createCommand, liteConnection);
            SQLiteCommand commandCheck = new SQLiteCommand(checkCommand, liteConnection);
            SQLiteCommand commandInsert = new SQLiteCommand(insertCommand, liteConnection);
            //MySqlCommand mySqlCommandCreate = new MySqlCommand(createCommand, mySqlConnection);
            //MySqlCommand mySqlCommandCheck = new MySqlCommand(checkCommand, mySqlConnection);
            //MySqlCommand mySqlCommandInsert = new MySqlCommand(insertCommand, mySqlConnection);
            commandCheck.Parameters.AddWithValue("@imgId", imgId);
            commandInsert.Parameters.AddWithValue("@imgId", imgId);
            commandInsert.Parameters.AddWithValue("@pic", bytes);
            //mySqlCommandCheck.Parameters.AddWithValue("@imgId", imgId);
            //mySqlCommandInsert.Parameters.AddWithValue("@imgId", imgId);
            //mySqlCommandInsert.Parameters.AddWithValue("@pic", bytes);

            //打开连接
            liteConnection.Open();
            //mySqlConnection.Open();

            //执行create
            commandCreate.ExecuteNonQuery();
            //mySqlCommandCreate.ExecuteNonQuery();

            //关闭create
            commandCreate.Dispose();
            //mySqlCommandCreate.Dispose();

            //执行check
            Object check = commandCheck.ExecuteScalar();
            //Object check = mySqlCommandCheck.ExecuteScalar();
            int checknum = Convert.ToInt32(check);
            if (checknum == 0)
            {
                //执行insert
                commandInsert.ExecuteNonQuery();
                //mySqlCommandInsert.ExecuteNonQuery();
                //关闭insert
                commandInsert.Dispose();
                //mySqlCommandInsert.Dispose();
            }
            //关闭check
            commandCheck.Dispose();
            //mySqlCommandCheck.Dispose();

            //关闭连接
            liteConnection.Close();
            //mySqlConnection.Close();
        }

        //读出最新的九张图片，并显示在picturebox1-9中(SW)
        static void ReadPicSW(Wallpaper wallpaper)
        {

            //创建连接
            SQLiteConnection liteConnection;
            liteConnection = new SQLiteConnection("Data Source=PicDB.sqlite;Version=3;");
            //string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            //MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:读取图片
            //string readCommand = "select imgId, pic from myPicSW order by strftime(imgId, '%Y_%m_%d') desc limit 9;";
            string readCommand = "select imgId, pic from myPicSW order by imgId desc limit 9;";
            SQLiteCommand commandRead = new SQLiteCommand(readCommand, liteConnection);
            //MySqlCommand mySqlCommandRead = new MySqlCommand(readCommand, mySqlConnection);

            //打开连接
            liteConnection.Open();
            //mySqlConnection.Open();

            //执行read
            SQLiteDataReader liteDataReader = commandRead.ExecuteReader();
            //MySqlDataReader mySqlDataReader = mySqlCommandRead.ExecuteReader();

            //关闭read
            commandRead.Dispose();
            //mySqlCommandRead.Dispose();

            PictureBox[] pictureBoxes = new PictureBox[9];
            pictureBoxes[0] = wallpaper.pictureBox1;
            pictureBoxes[1] = wallpaper.pictureBox2;
            pictureBoxes[2] = wallpaper.pictureBox3;
            pictureBoxes[3] = wallpaper.pictureBox4;
            pictureBoxes[4] = wallpaper.pictureBox5;
            pictureBoxes[5] = wallpaper.pictureBox6;
            pictureBoxes[6] = wallpaper.pictureBox7;
            pictureBoxes[7] = wallpaper.pictureBox8;
            pictureBoxes[8] = wallpaper.pictureBox9;

            TextBox[] textBoxes = new TextBox[9];
            textBoxes[0] = wallpaper.textBox1;
            textBoxes[1] = wallpaper.textBox2;
            textBoxes[2] = wallpaper.textBox3;
            textBoxes[3] = wallpaper.textBox4;
            textBoxes[4] = wallpaper.textBox5;
            textBoxes[5] = wallpaper.textBox6;
            textBoxes[6] = wallpaper.textBox7;
            textBoxes[7] = wallpaper.textBox8;
            textBoxes[8] = wallpaper.textBox9;

            


            if (liteDataReader.HasRows)
            {
                int i = 0;
                byte[] buffer = null;
                while (liteDataReader.Read())
                {
                    long len = liteDataReader.GetBytes(1, 0, null, 0, 0);
                    buffer = new byte[len];
                    len = liteDataReader.GetBytes(1, 0, buffer, 0, (int)len);

                    MemoryStream memoryStream = new MemoryStream(buffer);
                    Image image = Image.FromStream(memoryStream);
                    pictureBoxes[i].Image = image;
                    textBoxes[i].Text = liteDataReader[0].ToString();
                    i++;
                }

            }

            //关闭reader
            liteDataReader.Close();
            //mySqlDataReader.Close();

            //关闭连接
            liteConnection.Close();
            //mySqlConnection.Close();

        }

        //解析出图片链接(BA)
        static string[] GetUrlsBA(string html)
        {
            string pattern = "a href=\"(.+?)\" title=";
            MatchCollection matchCollection = Regex.Matches(html, pattern);
            string[] urls = new string[9];

            int m = 29;
            //解析一级链接
            for (int i = 0; i < 9; i++)
            {
                while (matchCollection[m].Groups[1].Value == @"http://pic.netbian.com")
                {
                    m++;
                }
                string url = @"http://pic.netbian.com" + matchCollection[m].Groups[1].Value;
                urls[i] = url;
                m++;
            }

            string[] picInfos = new string[9];
            string patternNew = "img src=\"(.+?)\" data-pic=";

            List<Task> tasks = new List<Task>();
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach(int i in arr)
            {
                var task = new Task(() =>
                {
                    WebClient webClient = new WebClient();
                    string htmlOld = GetHTMLDoc(webClient, urls[i]);
                    Match match = Regex.Match(htmlOld, patternNew);
                    string picUrl = @"http://pic.netbian.com/" + match.Groups[1].Value;
                    string id = DateTime.Now.ToString("yyyy-MM-dd").Replace('-', '_') + '_' + "BA" + '_' + i.ToString();
                    string picInfo = picUrl + ' ' + id;
                    picInfos[i] = picInfo;
                });
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());

            return picInfos;

        }

        //把图片以blob形式存进MySQL数据库(BA)
        static void SavePicBA(string Info, string wallPaperDir)
        {
            string imgId = Info.Split(' ')[1];
            string dirPath = wallPaperDir + "\\" + imgId + ".jpg";

            //把图片转换为byte格式
            byte[] bytes = null;
            bytes = File.ReadAllBytes(dirPath);

            //如果数据库文件不存在则创建一个数据库文件
            if (File.Exists(@"PicDB.sqlite"))
            {
                //do nothing
            }
            else
            {
                SQLiteConnection.CreateFile("PicDB.sqlite");
            }

            //创建连接
            SQLiteConnection liteConnection;
            liteConnection = new SQLiteConnection("Data Source=PicDB.sqlite;Version=3;");
            //string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            //MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:尝试建表,判断唯一性,插入数据
            string createCommand = "create table if not exists myPicBA(imgId char(20) primary key, pic mediumblob not null);";
            string checkCommand = "select count(imgId) from myPicBA where imgId = @imgId;";
            string insertCommand = "insert into myPicBA(imgId, pic) values(@imgId, @pic);";
            SQLiteCommand commandCreate = new SQLiteCommand(createCommand, liteConnection);
            SQLiteCommand commandCheck = new SQLiteCommand(checkCommand, liteConnection);
            SQLiteCommand commandInsert = new SQLiteCommand(insertCommand, liteConnection);
            //MySqlCommand mySqlCommandCreate = new MySqlCommand(createCommand, mySqlConnection);
            //MySqlCommand mySqlCommandCheck = new MySqlCommand(checkCommand, mySqlConnection);
            //MySqlCommand mySqlCommandInsert = new MySqlCommand(insertCommand, mySqlConnection);
            commandCheck.Parameters.AddWithValue("@imgId", imgId);
            commandInsert.Parameters.AddWithValue("@imgId", imgId);
            commandInsert.Parameters.AddWithValue("@pic", bytes);
            //mySqlCommandCheck.Parameters.AddWithValue("@imgId", imgId);
            //mySqlCommandInsert.Parameters.AddWithValue("@imgId", imgId);
            //mySqlCommandInsert.Parameters.AddWithValue("@pic", bytes);

            //打开连接
            liteConnection.Open();
            //mySqlConnection.Open();

            //执行create
            commandCreate.ExecuteNonQuery();
            //mySqlCommandCreate.ExecuteNonQuery();
            //关闭create
            commandCreate.Dispose();
            //mySqlCommandCreate.Dispose();

            //执行check
            Object check = commandCheck.ExecuteScalar();
            //Object check = mySqlCommandCheck.ExecuteScalar();
            int checknum = Convert.ToInt32(check);
            if (checknum == 0)
            {
                //执行insert
                commandInsert.ExecuteNonQuery();
                //mySqlCommandInsert.ExecuteNonQuery();
                //关闭insert
                commandInsert.Dispose();
                //mySqlCommandInsert.Dispose();
            }
            //关闭check
            commandCheck.Dispose();
            //mySqlCommandCheck.Dispose();

            //关闭连接
            liteConnection.Close();
            //mySqlConnection.Close();
        }

        //读出最新的九张图片，并显示在picturebox1-9中(SW)
        static void ReadPicBA(Wallpaper wallpaper)
        {


            //创建连接
            SQLiteConnection liteConnection;
            liteConnection = new SQLiteConnection("Data Source=PicDB.sqlite;Version=3;");
            //string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            //MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:读取图片
            //string readCommand = "select imgId, pic from myPicBA order by strftime(imgId, '%Y_%m_%d') desc limit 9;";
            string readCommand = "select imgId, pic from myPicBA order by imgId desc limit 9;";
            SQLiteCommand commandRead = new SQLiteCommand(readCommand, liteConnection);
            //MySqlCommand mySqlCommandRead = new MySqlCommand(readCommand, mySqlConnection);

            //打开连接
            liteConnection.Open();
            //mySqlConnection.Open();

            //执行read
            SQLiteDataReader liteDataReader = commandRead.ExecuteReader();
            //MySqlDataReader mySqlDataReader = mySqlCommandRead.ExecuteReader();

            //关闭read
            commandRead.Dispose();
            //mySqlCommandRead.Dispose();

            PictureBox[] pictureBoxes = new PictureBox[9];
            pictureBoxes[0] = wallpaper.pictureBox1;
            pictureBoxes[1] = wallpaper.pictureBox2;
            pictureBoxes[2] = wallpaper.pictureBox3;
            pictureBoxes[3] = wallpaper.pictureBox4;
            pictureBoxes[4] = wallpaper.pictureBox5;
            pictureBoxes[5] = wallpaper.pictureBox6;
            pictureBoxes[6] = wallpaper.pictureBox7;
            pictureBoxes[7] = wallpaper.pictureBox8;
            pictureBoxes[8] = wallpaper.pictureBox9;

            TextBox[] textBoxes = new TextBox[9];
            textBoxes[0] = wallpaper.textBox1;
            textBoxes[1] = wallpaper.textBox2;
            textBoxes[2] = wallpaper.textBox3;
            textBoxes[3] = wallpaper.textBox4;
            textBoxes[4] = wallpaper.textBox5;
            textBoxes[5] = wallpaper.textBox6;
            textBoxes[6] = wallpaper.textBox7;
            textBoxes[7] = wallpaper.textBox8;
            textBoxes[8] = wallpaper.textBox9;

            if (liteDataReader.HasRows)
            {
                int i = 0;
                byte[] buffer = null;
                while (liteDataReader.Read())
                {
                    long len = liteDataReader.GetBytes(1, 0, null, 0, 0);
                    buffer = new byte[len];
                    len = liteDataReader.GetBytes(1, 0, buffer, 0, (int)len);

                    MemoryStream memoryStream = new MemoryStream(buffer);
                    Image image = Image.FromStream(memoryStream);
                    pictureBoxes[i].Image = image;
                    textBoxes[i].Text = liteDataReader[0].ToString();

                    i++;

                }

            }

            //关闭reader
            liteDataReader.Close();
            //mySqlDataReader.Close();

            //关闭连接
            liteConnection.Close();
            //mySqlConnection.Close();

        }


        private void BtnWallpaper_Click(object sender, EventArgs e)
        {
            ResetButton(null);
            ShowSubMenu(pnlWallpaper);
            CloseActiveForm(true);
            ActivateButton(sender, RGBColors.colorWallpaper);
            //判断路径是否存在
            if (!System.IO.Directory.Exists(wallPaperDir))
            {
                System.IO.Directory.CreateDirectory(wallPaperDir);//不存在就创建目录
            }
        }

        private void BtnBing_Click(object sender, EventArgs e)
        {
            ResetButton(btnBing);

            Wallpaper wallpaper = new Wallpaper();
            Wallpaper.main = main;

            //准备右侧窗体
            pnlChildForm.Controls.Clear();
            wallpaper.pictureBox1.Image = null;
            wallpaper.pictureBox2.Image = null;
            wallpaper.pictureBox3.Image = null;
            wallpaper.pictureBox4.Image = null;
            wallpaper.pictureBox5.Image = null;
            wallpaper.pictureBox6.Image = null;
            wallpaper.pictureBox8.Image = null;
            wallpaper.pictureBox7.Image = null;
            wallpaper.pictureBox9.Image = null;
            wallpaper.textBox1.Text = "";
            wallpaper.textBox2.Text = "";
            wallpaper.textBox3.Text = "";
            wallpaper.textBox4.Text = "";
            wallpaper.textBox5.Text = "";
            wallpaper.textBox6.Text = "";
            wallpaper.textBox7.Text = "";
            wallpaper.textBox8.Text = "";
            wallpaper.textBox9.Text = "";

            OpenChildForm(wallpaper);

            //wallpaper.TopLevel = false;
            //wallpaper.Dock = DockStyle.Fill;
            //wallpaper.FormBorderStyle = FormBorderStyle.None;
            //pnlChildForm.Controls.Add(wallpaper);
            //wallpaper.Show();


            //获得今日Bing图片并存入数据库
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string webURL = @"https://bing.ioliu.cn/";
            string html = GetHTMLDoc(webClient, webURL);
            string[] imgInfos = GetPicInfoBing(html);

            wallpaper.savepth = wallPaperDir;

            List<Task> tasks = new List<Task>();
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (int i in arr)
            {
                var task = new Task(() => {
                    DownloadPic(wallPaperDir, imgInfos[i]);
                    SavePicBing(imgInfos[i], wallPaperDir);
                });
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());

            //在右侧展示最新的九张图片
            ReadPicBing(wallpaper);


            CustomizeButton(btnBing);
        }

        private void BtnSW_Click(object sender, EventArgs e)
        {
            ResetButton(btnSW);

            Wallpaper wallpaper = new Wallpaper();
            Wallpaper.main = main;

            //准备右侧窗体
            pnlChildForm.Controls.Clear();
            wallpaper.pictureBox1.Image = null;
            wallpaper.pictureBox2.Image = null;
            wallpaper.pictureBox3.Image = null;
            wallpaper.pictureBox4.Image = null;
            wallpaper.pictureBox5.Image = null;
            wallpaper.pictureBox6.Image = null;
            wallpaper.pictureBox8.Image = null;
            wallpaper.pictureBox7.Image = null;
            wallpaper.pictureBox9.Image = null;
            wallpaper.textBox1.Text = "";
            wallpaper.textBox2.Text = "";
            wallpaper.textBox3.Text = "";
            wallpaper.textBox4.Text = "";
            wallpaper.textBox5.Text = "";
            wallpaper.textBox6.Text = "";
            wallpaper.textBox7.Text = "";
            wallpaper.textBox8.Text = "";
            wallpaper.textBox9.Text = "";

            OpenChildForm(wallpaper);

            //wallpaper.TopLevel = false;
            //wallpaper.Dock = DockStyle.Fill;
            //wallpaper.FormBorderStyle = FormBorderStyle.None;
            //pnlChildForm.Controls.Add(wallpaper);
            //wallpaper.Show();

            //获取最新的九张图片
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string webURL = @"http://streetwill.co/";
            string html = GetHTMLDoc(webClient, webURL);
            string[] Infos = GetPicInfoSW(html);
            
            wallpaper.savepth = wallPaperDir;

            List<Task> tasks = new List<Task>();
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach(int i in arr)
            {
                var task = new Task(() => {
                    DownloadPic(wallPaperDir, Infos[i]);
                    SavePicSW(Infos[i], wallPaperDir);
                });
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());


            //在右侧展示获得的图片
            ReadPicSW(wallpaper);

            CustomizeButton(btnSW);
        }

        private void BtnBA_Click(object sender, EventArgs e)
        {
            ResetButton(btnBA);

            Wallpaper wallpaper = new Wallpaper();
            Wallpaper.main = main;

            //准备右侧窗体
            pnlChildForm.Controls.Clear();
            wallpaper.pictureBox1.Image = null;
            wallpaper.pictureBox2.Image = null;
            wallpaper.pictureBox3.Image = null;
            wallpaper.pictureBox4.Image = null;
            wallpaper.pictureBox5.Image = null;
            wallpaper.pictureBox6.Image = null;
            wallpaper.pictureBox8.Image = null;
            wallpaper.pictureBox7.Image = null;
            wallpaper.pictureBox9.Image = null;
            wallpaper.textBox1.Text = "";
            wallpaper.textBox2.Text = "";
            wallpaper.textBox3.Text = "";
            wallpaper.textBox4.Text = "";
            wallpaper.textBox5.Text = "";
            wallpaper.textBox6.Text = "";
            wallpaper.textBox7.Text = "";
            wallpaper.textBox8.Text = "";
            wallpaper.textBox9.Text = "";

            OpenChildForm(wallpaper);

            //wallpaper.TopLevel = false;
            //wallpaper.Dock = DockStyle.Fill;
            //wallpaper.FormBorderStyle = FormBorderStyle.None;
            //pnlChildForm.Controls.Add(wallpaper);
            //wallpaper.Show();

            //获取最新的九张图片
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string webURL = @"http://pic.netbian.com/";
            string html = GetHTMLDoc(webClient, webURL);
            string[] picInfos = GetUrlsBA(html);
            
            wallpaper.savepth = wallPaperDir;

            List<Task> tasks = new List<Task>();
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (int i in arr)
            {
                var task = new Task(() => {
                    DownloadPic(wallPaperDir, picInfos[i]);
                    SavePicBA(picInfos[i], wallPaperDir);
                });
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());

            //在右侧展示获得的图片
            ReadPicBA(wallpaper);

            CustomizeButton(btnBA);
        }

        #endregion

        /// <summary>
        /// 图书馆抢座按钮响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSeat_Click(object sender, EventArgs e)
        {
            //if (activeForm!=null&&activeForm.GetType() == typeof(Trio.Forms.Library))
            //    return;

            //Open child form
            ResetButton(null);
            HideSubMenu();
            ActivateButton(sender, RGBColors.colorLibrarySeat);
            CloseActiveForm(true);

            Forms.Login login = new Forms.Login();
            Forms.Login.main = main;
            OpenChildForm(login);
        }

        /// <summary>
        /// 设置按钮响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ResetButton(null);
            HideSubMenu();
            ActivateButton(sender, RGBColors.colorSettings);
            CloseActiveForm(true);
            Forms.Settings settings = new Forms.Settings();
            Forms.Settings.main = main;
            OpenChildForm(settings);
        }

        /// <summary>
        /// 关于按钮响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAbout_Click(object sender, EventArgs e)
        {
            ResetButton(null);
            HideSubMenu();
            ActivateButton(sender, RGBColors.colorAbout);
            CloseActiveForm(true);
            OpenChildForm(new Forms.About());
        }

        public void CloseActiveForm(bool keepAlive)
        { 
            if (activeForm != null)
                if (activeForm.GetType() == typeof(Trio.Forms.Library) && logged&&keepAlive)  // 维持登录图书馆后不退出的状态
                {
                    lib = activeForm as Forms.Library;
                    lib.SendToBack();
                    lib.Visible = false;
                }
                else
                    activeForm.Close();
        }

        /// <summary>
        /// 打开按钮对应form函数
        /// </summary>
        /// <param name="childForm"></param>
        public void OpenChildForm(Form childForm)
        {
            // 如果已经登录过，直接打开登录后的界面
            if (childForm.GetType() == typeof(Trio.Forms.Login)&&logged)
            {
                lib.BringToFront();
                lib.Visible = true;
                return;
            }    
            activeForm = childForm;
            childForm.TopLevel = false;  // make it to behave like a control, not a form
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlChildForm.Controls.Add(childForm);
            pnlChildForm.Tag = childForm;  // to associate the child form and the main form
            childForm.BringToFront();
            childForm.Show();
        }

        /// <summary>
        /// 高亮选中的按钮
        /// </summary>
        /// <param name="senderBtn"></param>
        /// <param name="color"></param>
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                //Disactivate the previous button
                DisableButton();

                //activate button
                currentBtn = (IconButton)senderBtn;
                //currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                //currentBtn.Padding = new Padding(18, 0, 20, 0);
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                //picItem
                if(picItem.BackgroundImage != null)
                {
                    picItem.BackgroundImage.Dispose();  // release memory
                    picItem.BackgroundImage = null;
                }
                picItem.IconChar = currentBtn.IconChar;
                picItem.IconColor = color;

            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.ForeColor = Color.FromArgb(116, 125, 136);
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                //currentBtn.Padding = new Padding(18, 0, 20, 0);
                currentBtn.IconColor = Color.FromArgb(116, 125, 136);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void PicLogo_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            CloseActiveForm(false);
            picItem.IconChar = IconChar.None;
            picItem.BackgroundImage = Image.FromFile(@"assets/logo/猫咪.png");
        }
    }
}


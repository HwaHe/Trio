using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using FontAwesome.Sharp;
using MySql.Data.MySqlClient;
using Trio.Forms;

namespace Trio
{
    public partial class Main : Form
    {
        private static readonly List<Button> subMenuButtonList = new List<Button>();
        private IconButton currentBtn;
        private Form activeForm = null;
        private Panel leftBorderBtn;

        private struct RGBColors
        {
            public static Color colorNews = Color.FromArgb(172, 126, 241);
            public static Color colorWallpaper = Color.FromArgb(249, 118, 176);
            public static Color colorLibrarySeat = Color.FromArgb(253, 138, 114);
            public static Color colorSettings = Color.FromArgb(95, 77, 221);
            //public static Color colorAbout = Color.FromArgb(249, 88, 155);
            public static Color colorAbout = Color.FromArgb(24, 161, 251);
        }

        public Main()
        {
            InitializeComponent();
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
            leftBorderBtn.Size = new Size(7, 70);
            pnlSidemenu.Controls.Add(leftBorderBtn);

            //// remove title bar
            //Text = String.Empty;
            //ControlBox = false;
            //DoubleBuffered = true;

            //limit Maximized bounds to the working area
            MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;  //防止最大化覆盖任务栏
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
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            SendMessage(this.Handle, 0x112, 0xf012, 0);
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
        private void BtnNews_Click(object sender, EventArgs e)
        {
            ResetButton(null);
            ShowSubMenu(pnlNews);
            ActivateButton(sender, RGBColors.colorNews);
        }

        private void BtnWhu_Click(object sender, EventArgs e)
        {
            ResetButton(btnWhu);
            //TODO
            OpenChildForm(new Forms.News());
            CustomizeButton(btnWhu);
        }

        private void BtnBkjw_Click(object sender, EventArgs e)
        {
            ResetButton(btnBkjw);
            //TODO
            CustomizeButton(btnBkjw);
        }

        private void BtnCs_Click(object sender, EventArgs e)
        {
            ResetButton(btnCs);
            //TODO
            CustomizeButton(btnCs);
        }

        private void BtnReadingList_Click(object sender, EventArgs e)
        {
            ResetButton(btnReadingList);
            //TODO
            CustomizeButton(btnReadingList);
        }
        #endregion

        #region 壁纸菜单的响应事件

        //提取网页html原码(公用)
        static string GetHTMLDoc(WebClient webClient, string webURL)
        {
            string html = webClient.DownloadString(webURL);
            return html;
        }

        //解析传入的图片信息，并下载图片(公用)
        static void DownloadPic(string savePath, string imgInfo)
        {
            string imgId = imgInfo.Split(' ')[1];
            string imgURL = imgInfo.Split(' ')[0];
            string dirPath = savePath + "\\" + imgId + ".jpg";
            WebClient webClient = new WebClient();
            webClient.DownloadFile(imgURL, dirPath);
        }

        //声明wallpaper子窗体(公用)
        static Wallpaper wallpaper = new Wallpaper();

        //利用正则表达式，从原码中解析出图片信息
        //用字符串存储信息，并用空格分隔
        //字符串前半部分是地址，后半部分是id,id是当前年月日(Bing)
        static string GetPicInfoBing(string html)
        {
            Match match = Regex.Match(html, "id=\"bgLink\".*?href=\"(.+?)\"");
            string imgURL = @"https://cn.bing.com/" + match.Groups[1].Value;
            string imgId = DateTime.Now.ToString("yyyy-MM-dd").Replace('-', '_') + '_' + "Bing";
            string imgInfo = imgURL + ' ' + imgId;
            return imgInfo;
        }

        //把图片以blob形式存进MySQL数据库(Bing)
        static void SavePicBing(string imgInfo, string savePath)
        {
            string imgId = imgInfo.Split(' ')[1];
            string dirPath = savePath + "\\" + imgId + ".jpg";

            //把图片转换为byte格式
            byte[] bytes = null;
            bytes = File.ReadAllBytes(dirPath);

            //创建连接
            string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:尝试建表,判断唯一性,插入数据
            string createCommand = "create table if not exists myPicBing(imgId char(20) primary key, pic mediumblob not null);";
            string checkCommand = "select count(imgId) from myPicBing where imgId = @imgId;";
            string insertCommand = "insert into myPicBing(imgId, pic) values(@imgId, @pic);";
            MySqlCommand mySqlCommandCreate = new MySqlCommand(createCommand, mySqlConnection);
            MySqlCommand mySqlCommandCheck = new MySqlCommand(checkCommand, mySqlConnection);
            MySqlCommand mySqlCommandInsert = new MySqlCommand(insertCommand, mySqlConnection);
            mySqlCommandCheck.Parameters.AddWithValue("@imgId", imgId);
            mySqlCommandInsert.Parameters.AddWithValue("@imgId", imgId);
            mySqlCommandInsert.Parameters.AddWithValue("@pic", bytes);

            //打开连接
            mySqlConnection.Open();

            //执行create
            mySqlCommandCreate.ExecuteNonQuery();
            //关闭create
            mySqlCommandCreate.Dispose();

            //执行check
            Object check = mySqlCommandCheck.ExecuteScalar();
            int checknum = Convert.ToInt32(check);
            if (checknum == 0)
            {
                //执行insert
                mySqlCommandInsert.ExecuteNonQuery();
                //关闭insert
                mySqlCommandInsert.Dispose();
            }
            //关闭check
            mySqlCommandCheck.Dispose();

            //关闭连接
            mySqlConnection.Close();
        }

        //读出最新的九张图片，并显示在picturebox1-9中(Bing)
        static void ReadPicBing()
        {

            //创建连接
            string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:读取图片
            string readCommand = "select imgId, pic from myPicBing order by str_to_date(imgId, '%Y_%m_%d') desc limit 9;";
            MySqlCommand mySqlCommandRead = new MySqlCommand(readCommand, mySqlConnection);

            //打开连接
            mySqlConnection.Open();

            //执行read
            MySqlDataReader mySqlDataReader = mySqlCommandRead.ExecuteReader();

            //关闭read
            mySqlCommandRead.Dispose();

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

            if (mySqlDataReader.HasRows)
            {
                int i = 0;
                byte[] buffer = null;
                while (mySqlDataReader.Read())
                {
                    long len = mySqlDataReader.GetBytes(1, 0, null, 0, 0);
                    buffer = new byte[len];
                    len = mySqlDataReader.GetBytes(1, 0, buffer, 0, (int)len);

                    MemoryStream memoryStream = new MemoryStream(buffer);
                    Image image = Image.FromStream(memoryStream);
                    pictureBoxes[i].Image = image;
                    textBoxes[i].Text = mySqlDataReader[0].ToString();

                    i++;

                }

            }

            //关闭reader
            mySqlDataReader.Close();

            //关闭连接
            mySqlConnection.Close();

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
        static void SavePicSW(string Info, string savePath)
        {
            string imgId = Info.Split(' ')[1];
            string dirPath = savePath + "\\" + imgId + ".jpg";

            //把图片转换为byte格式
            byte[] bytes = null;
            bytes = File.ReadAllBytes(dirPath);

            //创建连接
            string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:尝试建表,判断唯一性,插入数据
            string createCommand = "create table if not exists myPicSW(imgId char(20) primary key, pic mediumblob not null);";
            string checkCommand = "select count(imgId) from myPicSW where imgId = @imgId;";
            string insertCommand = "insert into myPicSW(imgId, pic) values(@imgId, @pic);";
            MySqlCommand mySqlCommandCreate = new MySqlCommand(createCommand, mySqlConnection);
            MySqlCommand mySqlCommandCheck = new MySqlCommand(checkCommand, mySqlConnection);
            MySqlCommand mySqlCommandInsert = new MySqlCommand(insertCommand, mySqlConnection);
            mySqlCommandCheck.Parameters.AddWithValue("@imgId", imgId);
            mySqlCommandInsert.Parameters.AddWithValue("@imgId", imgId);
            mySqlCommandInsert.Parameters.AddWithValue("@pic", bytes);

            //打开连接
            mySqlConnection.Open();

            //执行create
            mySqlCommandCreate.ExecuteNonQuery();
            //关闭create
            mySqlCommandCreate.Dispose();

            //执行check
            Object check = mySqlCommandCheck.ExecuteScalar();
            int checknum = Convert.ToInt32(check);
            if (checknum == 0)
            {
                //执行insert
                mySqlCommandInsert.ExecuteNonQuery();
                //关闭insert
                mySqlCommandInsert.Dispose();
            }
            //关闭check
            mySqlCommandCheck.Dispose();

            //关闭连接
            mySqlConnection.Close();
        }

        //读出最新的九张图片，并显示在picturebox1-9中(SW)
        static void ReadPicSW()
        {

            //创建连接
            string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:读取图片
            string readCommand = "select imgId, pic from myPicSW order by str_to_date(imgId, '%Y_%m_%d') desc limit 9;";
            MySqlCommand mySqlCommandRead = new MySqlCommand(readCommand, mySqlConnection);

            //打开连接
            mySqlConnection.Open();

            //执行read
            MySqlDataReader mySqlDataReader = mySqlCommandRead.ExecuteReader();

            //关闭read
            mySqlCommandRead.Dispose();

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

            if (mySqlDataReader.HasRows)
            {
                int i = 0;
                byte[] buffer = null;
                while (mySqlDataReader.Read())
                {
                    long len = mySqlDataReader.GetBytes(1, 0, null, 0, 0);
                    buffer = new byte[len];
                    len = mySqlDataReader.GetBytes(1, 0, buffer, 0, (int)len);

                    MemoryStream memoryStream = new MemoryStream(buffer);
                    Image image = Image.FromStream(memoryStream);
                    pictureBoxes[i].Image = image;
                    textBoxes[i].Text = mySqlDataReader[0].ToString();

                    i++;

                }

            }

            //关闭reader
            mySqlDataReader.Close();

            //关闭连接
            mySqlConnection.Close();

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
        static void SavePicBA(string Info, string savePath)
        {
            string imgId = Info.Split(' ')[1];
            string dirPath = savePath + "\\" + imgId + ".jpg";

            //把图片转换为byte格式
            byte[] bytes = null;
            bytes = File.ReadAllBytes(dirPath);

            //创建连接
            string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:尝试建表,判断唯一性,插入数据
            string createCommand = "create table if not exists myPicBA(imgId char(20) primary key, pic mediumblob not null);";
            string checkCommand = "select count(imgId) from myPicBA where imgId = @imgId;";
            string insertCommand = "insert into myPicBA(imgId, pic) values(@imgId, @pic);";
            MySqlCommand mySqlCommandCreate = new MySqlCommand(createCommand, mySqlConnection);
            MySqlCommand mySqlCommandCheck = new MySqlCommand(checkCommand, mySqlConnection);
            MySqlCommand mySqlCommandInsert = new MySqlCommand(insertCommand, mySqlConnection);
            mySqlCommandCheck.Parameters.AddWithValue("@imgId", imgId);
            mySqlCommandInsert.Parameters.AddWithValue("@imgId", imgId);
            mySqlCommandInsert.Parameters.AddWithValue("@pic", bytes);

            //打开连接
            mySqlConnection.Open();

            //执行create
            mySqlCommandCreate.ExecuteNonQuery();
            //关闭create
            mySqlCommandCreate.Dispose();

            //执行check
            Object check = mySqlCommandCheck.ExecuteScalar();
            int checknum = Convert.ToInt32(check);
            if (checknum == 0)
            {
                //执行insert
                mySqlCommandInsert.ExecuteNonQuery();
                //关闭insert
                mySqlCommandInsert.Dispose();
            }
            //关闭check
            mySqlCommandCheck.Dispose();

            //关闭连接
            mySqlConnection.Close();
        }

        //读出最新的九张图片，并显示在picturebox1-9中(SW)
        static void ReadPicBA()
        {

            //创建连接
            string connString = "server=localhost;user id=root;pwd=200087;database=BlobPic;charset=utf8;Allow User Variables=True;";
            MySqlConnection mySqlConnection = new MySqlConnection(connString);

            //Command:读取图片
            string readCommand = "select imgId, pic from myPicBA order by str_to_date(imgId, '%Y_%m_%d') desc limit 9;";
            MySqlCommand mySqlCommandRead = new MySqlCommand(readCommand, mySqlConnection);

            //打开连接
            mySqlConnection.Open();

            //执行read
            MySqlDataReader mySqlDataReader = mySqlCommandRead.ExecuteReader();

            //关闭read
            mySqlCommandRead.Dispose();

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

            if (mySqlDataReader.HasRows)
            {
                int i = 0;
                byte[] buffer = null;
                while (mySqlDataReader.Read())
                {
                    long len = mySqlDataReader.GetBytes(1, 0, null, 0, 0);
                    buffer = new byte[len];
                    len = mySqlDataReader.GetBytes(1, 0, buffer, 0, (int)len);

                    MemoryStream memoryStream = new MemoryStream(buffer);
                    Image image = Image.FromStream(memoryStream);
                    pictureBoxes[i].Image = image;
                    textBoxes[i].Text = mySqlDataReader[0].ToString();

                    i++;

                }

            }

            //关闭reader
            mySqlDataReader.Close();

            //关闭连接
            mySqlConnection.Close();

        }

        private void BtnWallpaper_Click(object sender, EventArgs e)
        {
            ResetButton(null);
            ShowSubMenu(pnlWallpaper);
            ActivateButton(sender, RGBColors.colorWallpaper);
        }

        private void BtnBing_Click(object sender, EventArgs e)
        {
            ResetButton(btnBing);

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
            wallpaper.TopLevel = false;
            wallpaper.Dock = DockStyle.Fill;
            wallpaper.FormBorderStyle = FormBorderStyle.None;
            pnlChildForm.Controls.Add(wallpaper);
            wallpaper.Show();

            //获得今日Bing图片并存入数据库
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string webURL = @"https://cn.bing.com/";
            string html = GetHTMLDoc(webClient, webURL);
            string imgInfo = GetPicInfoBing(html);
            string savePath = "D:\\a";
            wallpaper.savepth = savePath;
            DownloadPic(savePath, imgInfo);
            SavePicBing(imgInfo, savePath);

            //在右侧展示最新的九张图片
            ReadPicBing();

            CustomizeButton(btnBing);
        }

        private void BtnSW_Click(object sender, EventArgs e)
        {
            ResetButton(btnSW);

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
            wallpaper.TopLevel = false;
            wallpaper.Dock = DockStyle.Fill;
            wallpaper.FormBorderStyle = FormBorderStyle.None;
            pnlChildForm.Controls.Add(wallpaper);
            wallpaper.Show();

            //获取最新的九张图片
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string webURL = @"http://streetwill.co/";
            string html = GetHTMLDoc(webClient, webURL);
            string[] Infos = GetPicInfoSW(html);
            string savePath = "D:\\a";
            wallpaper.savepth = savePath;

            List<Task> tasks = new List<Task>();
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach(int i in arr)
            {
                var task = new Task(() => {
                    DownloadPic(savePath, Infos[i]);
                    SavePicSW(Infos[i], savePath);
                });
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());


            //在右侧展示获得的图片
            ReadPicSW();

            CustomizeButton(btnSW);
        }

        private void BtnBA_Click(object sender, EventArgs e)
        {
            ResetButton(btnBA);

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
            wallpaper.TopLevel = false;
            wallpaper.Dock = DockStyle.Fill;
            wallpaper.FormBorderStyle = FormBorderStyle.None;
            pnlChildForm.Controls.Add(wallpaper);
            wallpaper.Show();

            //获取最新的九张图片
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string webURL = @"http://pic.netbian.com/";
            string html = GetHTMLDoc(webClient, webURL);
            string[] picInfos = GetUrlsBA(html);
            string savePath = "D:\\a";
            wallpaper.savepth = savePath;

            List<Task> tasks = new List<Task>();
            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (int i in arr)
            {
                var task = new Task(() => {
                    DownloadPic(savePath, picInfos[i]);
                    SavePicBA(picInfos[i], savePath);
                });
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());

            //在右侧展示获得的图片
            ReadPicBA();

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
            ResetButton(null);
            HideSubMenu();
            ActivateButton(sender, RGBColors.colorLibrarySeat);
            //TODO
            //Open child form
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
            //TODO
            //Open child form
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
            //TODO
            //Open child form
        }

        /// <summary>
        /// 打开按钮对应form函数
        /// </summary>
        /// <param name="childForm"></param>
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
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
            picItem.IconChar = IconChar.None;
            picItem.BackgroundImage = Image.FromFile(@"assets/logo/猫咪.png");


        }
    }
}


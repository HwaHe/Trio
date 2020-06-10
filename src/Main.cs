using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace Trio
{
    public partial class Main : Form
    {
        public static Main main;
        private static readonly List<Button> subMenuButtonList = new List<Button>();
        public bool logged = false;
        private Forms.Library lib;
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
            subMenuButtonList.Add(btnAnime);
            subMenuButtonList.Add(btnWallpaperEngine);

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
        private void BtnNews_Click(object sender, EventArgs e)
        {
            ResetButton(null);
            ShowSubMenu(pnlNews);
            CloseActiveForm(true);
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
        private void BtnWallpaper_Click(object sender, EventArgs e)
        {
            ResetButton(null);
            ShowSubMenu(pnlWallpaper);
            CloseActiveForm(true);
            ActivateButton(sender, RGBColors.colorWallpaper);
        }

        private void BtnBing_Click(object sender, EventArgs e)
        {
            ResetButton(btnBing);
            //TODO
            CustomizeButton(btnBing);
        }

        private void BtnAnime_Click(object sender, EventArgs e)
        {
            ResetButton(btnAnime);
            //TODO
            CustomizeButton(btnAnime);
        }

        private void BtnWallpaperEngine_Click(object sender, EventArgs e)
        {
            ResetButton(btnWallpaperEngine);
            //TODO
            CustomizeButton(btnWallpaperEngine);
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
            //TODO
            OpenChildForm(new Forms.Settings());
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
            //TODO
            //Open child form
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


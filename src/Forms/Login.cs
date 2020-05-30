using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Trio.Forms
{
    public partial class Login : Form
    {
        public static Main main;
        public static Login login;
        BindingSource bs = new BindingSource();
        bool exitFlag = false;

        public Login()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            login = this;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPwd.AutoSize = false;
            txtPwd.Height = 32;

            backgroundWorker1.RunWorkerAsync();
            Pwd.CreateSubKey();
            Pwd.GetKey();

            bs.DataSource = Pwd.users;
            comboUsr.DataSource = bs;

            if (comboUsr.Items.Count > 0)
            {
                Pwd.users.Add("清空存储的登录信息");
                bs.ResetBindings(false);
            }

        }

        private void Login_Activated(object sender, EventArgs e)
        {
            comboUsr.Focus();
        }

        private void ComboUsr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboUsr.Text== "清空存储的登录信息")
            {
                Pwd.DeleteValue();
                bs.ResetBindings(false);
            }
            else
            {
                txtPwd.Text = Pwd.GetValue(comboUsr.Text);
            }
        }

        //[DllImport("kernel32.dll")]
        //private static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        //private static void FlushMemory()
        //{
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        //    {
        //        SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        //    }
        //}

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            Book.username = comboUsr.Text;
            Book.password = txtPwd.Text;
            string response = Book.GetToken(false);
            if (response == "Success")
            {
                exitFlag = false;
                if (chkRemember.Checked)
                {
                    Pwd.SetValue(comboUsr.Text, txtPwd.Text);
                }

                Hide();
                Book.CheckResInf(); 
                Library lib = new Library();
                Library.main = main;
                main.OpenChildForm(lib);
                Close();
                //FlushMemory();
            }
            else if(response=="System Maintenance")
            {
                Forms.Warning warn = new Forms.Warning();
                warn.lblMsg.Text = "系统维护中(23:45-00:15), 登录失败";
                warn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                warn.Show();
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            else if(response=="Connection lost")
            {
                Forms.Warning warn = new Forms.Warning();
                warn.lblMsg.Text = "连接丢失, 请稍后再试, 登录失败";
                warn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                warn.Show();
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            else
            {
                Forms.Warning warn = new Forms.Warning();
                warn.lblMsg.Text = response;
                warn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Dispose();
            }
            catch
            {

            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                return;
            }
            else if (exitFlag)
            {
                Environment.Exit(0);
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Book.username = "";
            Book.password = "";
            while (!backgroundWorker1.CancellationPending)
            {
                if(Book.GetToken(false)== "登录失败: 用户名或密码不正确")
                {
                    backgroundWorker1.ReportProgress(100);
                }
                else
                {
                    backgroundWorker1.ReportProgress(0);
                }
                Thread.Sleep(200);
            }
            e.Cancel = true;
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                try
                {
                    lblState.Text = "Avaiable";
                    lblState.ForeColor = Color.ForestGreen;
                    iconPicState.IconColor = Color.ForestGreen;
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    lblState.Text = "Unavailable";
                    lblState.ForeColor = Color.Red;
                    iconPicState.IconColor = Color.Red;
                }
                catch
                {

                }
            }
        }
    }
}

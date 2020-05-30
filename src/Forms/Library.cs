using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;

namespace Trio.Forms
{
    public partial class Library : Form
    {
        public static Main main;
        public static Library lib;
        ArrayList displayStartTimeList;

        // 图书馆list
        readonly ArrayList building_list = new ArrayList
        {
            new DictionaryEntry("1", "信图"),
            new DictionaryEntry("2", "总图"),
            new DictionaryEntry("3", "工图"),
            new DictionaryEntry("4", "医学部图书馆")
        };

        // 开始时间list
        readonly ArrayList startTimeList = new ArrayList
        {
            new DictionaryEntry("480", "8:00"),
            new DictionaryEntry("510", "8:30"),
            new DictionaryEntry("540", "9:00"),
            new DictionaryEntry("570", "9:30"),
            new DictionaryEntry("600", "10:00"),
            new DictionaryEntry("630", "10:30"),
            new DictionaryEntry("660", "11:00"),
            new DictionaryEntry("690", "11:30"),
            new DictionaryEntry("720", "12:00"),
            new DictionaryEntry("750", "12:30"),
            new DictionaryEntry("780", "13:00"),
            new DictionaryEntry("810", "13:30"),
            new DictionaryEntry("840", "14:00"),
            new DictionaryEntry("870", "14:30"),
            new DictionaryEntry("900", "15:00"),
            new DictionaryEntry("930", "15:30"),
            new DictionaryEntry("960", "16:00"),
            new DictionaryEntry("990", "16:30"),
            new DictionaryEntry("1020", "17:00"),
            new DictionaryEntry("1050", "17:30"),
            new DictionaryEntry("1080", "18:00"),
            new DictionaryEntry("1110", "18:30"),
            new DictionaryEntry("1140", "19:00"),
            new DictionaryEntry("1170", "19:30"),
            new DictionaryEntry("1200", "20:00"),
            new DictionaryEntry("1230", "20:30"),
            new DictionaryEntry("1260", "21:00"),
            new DictionaryEntry("1290", "21:30"),
            new DictionaryEntry("1320", "22:00")
        };

        public Library()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            lib = this;
        }

        private void IconPictureBox1_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void Library_Load(object sender, EventArgs e)
        {
            txtLog.AppendText("Requesting for token.....success");
            txtLog.AppendText("\r\nFetching user information.....success");

            comboLib.DataSource = building_list;
            comboLib.DisplayMember = "Value";  // 显示的值
            comboLib.ValueMember = "Key";  // 绑定的值

            comboRegion.DisplayMember = "Value";
            comboRegion.ValueMember = "Key";

            comboNum.DisplayMember = "Value";
            comboNum.ValueMember = "Key";

            comboDate.DisplayMember = "Value";
            comboDate.ValueMember = "Key";

            comboStart.DisplayMember = "Value";
            comboStart.ValueMember = "Key";

            comboEnd.DisplayMember = "Value";
            comboEnd.ValueMember = "Key";

            backgroundWorker2.RunWorkerAsync();
            backgroundWorker3.RunWorkerAsync();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!IsHandleCreated)
            {
                Close();
            }
        }


        private void ComboLib_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboLib.SelectedIndex)
            {
                case 0:  //信图
                    comboRegion.DataSource = new ArrayList
                    {
                        new DictionaryEntry("0", "自动选择"),
                        new DictionaryEntry("1", "只包含2~4楼和1楼云桌面"),
                        new DictionaryEntry("5", "一楼创新学习讨论区"),
                        new DictionaryEntry("4", "一楼3C创客空间"),
                        new DictionaryEntry("14", "3C创客-双屏电脑（20台）"),
                        new DictionaryEntry("15", "创新学习-MAC电脑（12台）"),
                        new DictionaryEntry("16", "创新学习-云桌面（42台）"),
                        new DictionaryEntry("6", "二楼西自然科学图书借阅区"),
                        new DictionaryEntry("7", "二楼东自然科学图书借阅区"),
                        new DictionaryEntry("8", "三楼西社会科学图书借阅区"),
                        new DictionaryEntry("10", "三楼东社会科学图书借阅区"),
                        new DictionaryEntry("12", "三楼自主学习区"),
                        new DictionaryEntry("9", "四楼西图书阅览区"),
                        new DictionaryEntry("11", "四楼东图书阅览区")
                    };
                    break;
                case 1:  // 总图
                    comboRegion.DataSource = new ArrayList
                    {
                        new DictionaryEntry("0", "自动选择"),
                        new DictionaryEntry("39", "A1-座位区"),
                        new DictionaryEntry("62", "A1-沙发区"),
                        new DictionaryEntry("66", "A1-苹果区"),
                        new DictionaryEntry("51", "A2"),
                        new DictionaryEntry("52", "A3"),
                        new DictionaryEntry("60", "A4"),
                        new DictionaryEntry("61", "A5"),
                        new DictionaryEntry("65", "B1"),
                        new DictionaryEntry("59", "B2"),
                        new DictionaryEntry("56", "B3"),
                        new DictionaryEntry("40", "C1自习区"),
                        new DictionaryEntry("92", "E1信息共享空间双屏云桌面区"),
                        new DictionaryEntry("84", "E2报刊阅览区"),
                        new DictionaryEntry("89", "E2大厅"),
                        new DictionaryEntry("85", "E3学位论文阅览区"),
                        new DictionaryEntry("86", "E4港台文献阅览区"),
                        new DictionaryEntry("87", "E5地方文献阅览区"),
                        new DictionaryEntry("88", "E6影印文献阅览区"),
                    };
                    break;
                case 2:  // 工图
                    comboRegion.DataSource = new ArrayList
                    {
                        new DictionaryEntry("0", "自动选择"),
                        new DictionaryEntry("19", "201室-东部自科图书借阅区"),
                        new DictionaryEntry("29", "2楼-中部走廊"),
                        new DictionaryEntry("31", "205室-中部电子阅览室笔记本区"),
                        new DictionaryEntry("32", "301室-东部自科图书借阅区"),
                        new DictionaryEntry("33", "305室-中部自科图书借阅区"),
                        new DictionaryEntry("34", "401室-东部自科图书借阅区"),
                        new DictionaryEntry("35", "405室中部期刊阅览区"),
                        new DictionaryEntry("37", "501室-东部外文图书借阅区"),
                        new DictionaryEntry("38", "505室-中部自科图书借阅区")
                    };
                    break;
                case 3:  // 医学部图书馆
                    comboRegion.DataSource = new ArrayList
                    {
                        new DictionaryEntry("0", "自动选择"),
                        new DictionaryEntry("20", "204教学参考书借阅区"),
                        new DictionaryEntry("21", "302中文科技图书借阅B区"),
                        new DictionaryEntry("23", "305科技期刊阅览区"),
                        new DictionaryEntry("24", "402中文文科图书借阅区"),
                        new DictionaryEntry("26", "502外文图书借阅区"),
                        new DictionaryEntry("27", "506医学人文阅览区")
                    };
                    break;
            }
        }

        private void ComboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList seats = new ArrayList();
            if (comboRegion.SelectedValue.GetType() == typeof(string))
            {
                if (int.Parse(comboRegion.SelectedValue.ToString()) > 1)
                {
                    Book.GetSeats(comboRegion.SelectedValue.ToString(), seats);
                }
            }
            seats.Insert(0, new DictionaryEntry("0", "自动选择"));
            comboNum.DataSource = seats;
        }

        private void ComboDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayStartTimeList = new ArrayList();
            if (comboDate.SelectedIndex == 0 && DateTime.Now.TimeOfDay.TotalMinutes > 480)
            {
                displayStartTimeList.Add(new DictionaryEntry("-1", "现在"));
                for (int i = 0; i < startTimeList.Count; i++)
                {
                    if (int.Parse(((DictionaryEntry)startTimeList[i]).Key.ToString()) > (int)DateTime.Now.TimeOfDay.TotalMinutes)
                    {
                        for (int j = i; j < startTimeList.Count; j++)
                        {
                            displayStartTimeList.Add((DictionaryEntry)startTimeList[j]);
                        }
                        break;
                    }
                }
            }
            else
            {
                displayStartTimeList = startTimeList;
            }
            comboStart.DataSource = displayStartTimeList;
        }

        private void ComboStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList endTimeList = new ArrayList();
            if (comboStart.SelectedIndex == 0 && displayStartTimeList.Count == 30)
            {
                for (int i = comboStart.SelectedIndex + 2; i <= 29; i++)
                {
                    endTimeList.Add(displayStartTimeList[i]);
                }
            }
            else
            {
                for (int i = comboStart.SelectedIndex + 1; i <= displayStartTimeList.Count - 1; i++)
                {
                    endTimeList.Add(displayStartTimeList[i]);
                }
            }
            endTimeList.Add(new DictionaryEntry("1350", "22:30"));
            comboEnd.DataSource = endTimeList;
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            if (btnExecute.Text == "运行")
            {
                KillSeat.buildingId = comboLib.SelectedValue.ToString();
                KillSeat.roomId = comboRegion.SelectedValue.ToString() == "1" ? "0" : comboRegion.SelectedValue.ToString();
                KillSeat.seatId = comboNum.SelectedValue.ToString();
                KillSeat.date = comboDate.SelectedValue.ToString();
                KillSeat.startTime = comboStart.SelectedValue.ToString();
                KillSeat.endTime = comboEnd.SelectedValue.ToString();

                Book.onlyPower = checkBox1.Checked;
                Book.onlyWindow = checkBox2.Checked;
                Book.onlyComputer = checkBox3.Checked;

                switch (KillSeat.buildingId)
                {
                    case "1":
                        KillSeat.rooms = (((IList)Book.xt_lite).Contains(KillSeat.roomId) || comboRegion.SelectedValue.ToString() == "1") ? Book.xt_lite : Book.xt;
                        break;
                    case "2":
                        KillSeat.rooms = Book.gt;
                        break;
                    case "3":
                        KillSeat.rooms = Book.yt;
                        break;
                    case "4":
                        KillSeat.rooms = Book.zt;
                        break;
                }

                lib.lblLib.Enabled = false;
                lib.lblRegion.Enabled = false;
                lib.lblNum.Enabled = false;
                lib.lblDate.Enabled = false;
                lib.lblStart.Enabled = false;
                lib.lblEnd.Enabled = false;
                lib.lblWelcome.Enabled = false;
                lib.comboLib.Enabled = false;
                lib.comboRegion.Enabled = false;
                lib.comboNum.Enabled = false;
                lib.comboDate.Enabled = false;
                lib.comboStart.Enabled = false;
                lib.comboEnd.Enabled = false;
                lib.checkBox1.Enabled = false;
                lib.checkBox2.Enabled = false;
                lib.checkBox3.Enabled = false;
                lib.checkBox4.Enabled = false;
                lib.iconPictureBox2.Enabled = false;
                lib.btnExecute.Text = "停止";

                KillSeat.Start();
            }
            else
            {
                KillSeat.Stop();

                lib.lblLib.Enabled = true;
                lib.lblRegion.Enabled = true;
                lib.lblNum.Enabled = true;
                lib.lblDate.Enabled = true;
                lib.lblStart.Enabled = true;
                lib.lblEnd.Enabled = true;
                lib.lblWelcome.Enabled = true;
                lib.comboLib.Enabled = true;
                lib.comboRegion.Enabled = true;
                lib.comboNum.Enabled = true;
                lib.comboDate.Enabled = true;
                lib.comboStart.Enabled = true;
                lib.comboEnd.Enabled = true;
                lib.checkBox1.Enabled = true;
                lib.checkBox2.Enabled = true;
                lib.checkBox3.Enabled = true;
                lib.checkBox4.Enabled = true;
                lib.iconPictureBox2.Enabled = true;
                lib.btnExecute.Text = "运行";
            }
        }

        private void TxtLog_Enter(object sender, EventArgs e)
        {
            lblWelcome.Focus();
        }

        private void LblWelcome_Click(object sender, EventArgs e)
        {
            if (Book.GetUsrInf(false))
            {
                lblWelcome.Text = "Hello, " + Book.name + " 你上次网页登录时间为: " + Book.last_login_time 
                    + " 状态: " + Book.state + " 违约记录: " + Book.violationCount + " 次";
            }
        }

        private void IconPictureBox2_Click(object sender, EventArgs e)
        {
            if(!Book.CheckResInf(true, true))
            {
                Warning warn = new Warning();
                warn.lblMsg.Text = "暂无有效预约";
                warn.TopLevel = true;
                //warn.Parent = lib;
                warn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                warn.ShowDialog();
            }
        }

        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int index = txtLog.GetFirstCharIndexOfCurrentLine();
            bool first = true;
            while (!backgroundWorker1.CancellationPending)
            {
                TimeSpan delta = Book.time.Subtract(DateTime.Now);
                if ((bool)(e.Argument))
                {
                    if (first)
                    {
                        txtLog.AppendText("\r\n\r\n正在等待系统开放，剩余" + ((int)delta.TotalSeconds).ToString() + "秒\r\n");
                        first = false;
                    }
                    else
                    {
                        txtLog.Select(index, txtLog.TextLength - index);
                        txtLog.SelectedText = "\r\n\r\n正在等待系统开放，剩余" + ((int)delta.TotalSeconds).ToString() + "秒\r\n";
                    }
                }
                else
                {
                    if (first)
                    {
                        txtLog.AppendText("\r\n正在等待系统开放，剩余" + ((int)delta.TotalSeconds).ToString() + "秒\r\n");
                    }
                    else
                    {
                        txtLog.Select(index, txtLog.TextLength - index);
                        txtLog.SelectedText= "\r\n正在等待系统开放，剩余" + ((int)delta.TotalSeconds).ToString() + "秒\r\n";
                    }
                }

                Thread.Sleep(1000);
            }
            e.Cancel = true;
        }

        private void BackgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            while (true)
            {
                if (Book.GetUsrInf(false))
                {
                    lblWelcome.Text = "你好 , " + Book.name + "  上次网页登录时间 : " + Book.last_login_time + "  状态 : " + Book.state + "  违约记录 : " + Book.violationCount + "次";
                }
                else
                {
                    Thread.Sleep(30000);
                    Book.GetToken(false);
                }
                Thread.Sleep(60000);
            }
        }

        private void BackgroundWorker3_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true)
            {
                comboDate.DataSource = new ArrayList
                {
                    new DictionaryEntry(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd") + " (今天)"),
                    new DictionaryEntry(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " (明天)")
                };
                comboDate.SelectedIndex = 0;

                while (true)
                {
                    if (DateTime.Now.ToString("yyyy-MM-dd") != ((DictionaryEntry)comboDate.Items[0]).Key.ToString())
                    {
                        break;
                    }
                    Thread.Sleep(10000);
                }
            }
        }

        private void BackgroundWorker4_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            txtLog.AppendText("\r\n\r\n正在等待下一次循环...\r\n");
            while (true)
            {
                if (backgroundWorker4.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                if (DateTime.Now.TimeOfDay.TotalMinutes >= 1360 && DateTime.Now.TimeOfDay.TotalMinutes <= 1365)
                {
                    break;
                }
                Thread.Sleep(1000);
            }
            comboDate.SelectedIndex = 0;
            KillSeat.date = comboDate.SelectedValue.ToString();
            KillSeat.startTime = comboStart.SelectedValue.ToString() == "-1" ? "480" : comboStart.SelectedValue.ToString();
            txtLog.Text = "";
            KillSeat.Start(true);
        }
    }
}

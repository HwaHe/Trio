using System;
using Trio.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trio
{
    class KillSeat
    {
        public static string buildingId, roomId, seatId, date, startTime, endTime;
        public static string[] rooms;
        public static bool enter;
        private static Thread thread;

        public static void Start(bool didClear = false)
        {
            enter = !didClear;
            if (Library.lib.backgroundWorker4.IsBusy)
            {
                Library.lib.backgroundWorker4.CancelAsync();
            }
            thread = new Thread(Run)
            {
                IsBackground = true
            };
            thread.Start();
        }

        public static void Stop()
        {
            bool workerBusy = false;
            if (Library.lib.backgroundWorker1.IsBusy)
            {
                workerBusy = true;
                Library.lib.backgroundWorker1.CancelAsync();
            }
            if (Library.lib.backgroundWorker4.IsBusy)
            {
                workerBusy = true;
                Library.lib.backgroundWorker4.CancelAsync();
            }
            while (Library.lib.backgroundWorker1.IsBusy || Library.lib.backgroundWorker4.IsBusy)
            {
                Thread.Sleep(100);
            }
            thread.Abort();
            Library.lib.txtLog.AppendText((workerBusy ? "" : "\r\n") + "\r\n-----------------------------运行中断------------------------------\r\n");
        }

        public static void Run()
        {
            bool cancelled = false, exchange = false;
            if (Library.lib.comboDate.SelectedIndex == 1)
            {
                Library.lib.txtLog.AppendText((enter ? "\r\n" : "") + "\r\n---------------------------进入抢座模式---------------------------\r\n");

                if (DateTime.Now.TimeOfDay.TotalSeconds < 81880)
                {
                    Book.Wait("22", "44", "40", false);
                }

                if (Book.GetToken() == "Success")
                {
                    if (Book.CheckResInf(false))
                    {
                        Library.lib.txtLog.AppendText("\r\n已检测到有效预约，将自动改签预约信息\r\n");
                        exchange = true;
                    }
                    else
                    {
                        exchange = false;
                    }

                    if (DateTime.Now.TimeOfDay.TotalMinutes < 1365)
                    {
                        Book.GetRooms(buildingId);
                        Book.Wait("22", "45", "00");
                    }
                    else if (DateTime.Now.TimeOfDay.TotalMinutes > 1420)
                    {
                        Library.lib.txtLog.AppendText("\r\n预约系统已关闭");

                        if (exchange)
                        {
                            if (Book.ExchangeLoop(buildingId, rooms, startTime, endTime, roomId, seatId))
                            {
                                Book.LockSeat(Book.bookedSeatId);
                            }
                        }
                        else
                        {
                            if (Book.Loop(buildingId, rooms, startTime, endTime, roomId, seatId))
                            {
                                Book.LockSeat(Book.bookedSeatId);
                            }
                        }

                        EnableControls();
                        return;
                    }

                    while (true)
                    {
                        if (DateTime.Now.TimeOfDay.TotalMinutes > 1420)
                        {
                            Library.lib.txtLog.AppendText("\r\n\r\n抢座失败，座位预约系统已关闭");

                            if (exchange)
                            {
                                if (Book.ExchangeLoop(buildingId, rooms, startTime, endTime, roomId, seatId))
                                {
                                    Book.LockSeat(Book.bookedSeatId);
                                }

                                else
                                {
                                    EnableControls();
                                }
                            }
                            else
                            {
                                if (Book.Loop(buildingId, rooms, startTime, endTime, roomId, seatId))
                                {
                                    Book.LockSeat(Book.bookedSeatId);
                                }

                                else
                                {
                                    EnableControls();
                                }
                            }

                            return;
                        }

                        if (seatId != "0")
                        {
                            if (exchange && !Book.reserving && !cancelled)
                            {
                                if (Book.StopUsing())
                                {
                                    cancelled = true;
                                }
                                else
                                {
                                    Library.lib.txtLog.AppendText("\r\n\r\n释放座位失败，请稍后重试\r\n");
                                    Library.lib.txtLog.AppendText("\r\n---------------------------退出抢座模式---------------------------\r\n");
                                    EnableControls();
                                    return;
                                }
                            }
                            else if (exchange && Book.reserving && !cancelled)
                            {
                                if (Book.CancelReservation(Book.res_id))
                                {
                                    cancelled = true;
                                }
                                else
                                {
                                    Library.lib.txtLog.AppendText("\r\n\r\n取消预约失败，请稍后重试\r\n");
                                    Library.lib.txtLog.AppendText("\r\n---------------------------退出抢座模式---------------------------\r\n");
                                    EnableControls();
                                    return;
                                }
                            }

                            string res = Book.BookSeat(seatId, date, startTime, endTime);
                            if (res == "Success")
                            {
                                Library.lib.txtLog.AppendText("\r\n\r\n---------------------------退出抢座模式---------------------------\r\n");
                                Book.LockSeat(Book.bookedSeatId);
                                EnableControls();
                                return;
                            }
                            else if (res == "Connection lost")
                            {
                                Library.lib.txtLog.AppendText("\r\n\r\n连接丢失，30秒后尝试继续预约空位\r\n");
                                Thread.Sleep(30000);
                                continue;
                            }
                            else if (Library.lib.checkBox4.Checked)
                            {
                                Library.lib.txtLog.AppendText("\r\n\r\n指定座位预约失败，尝试检索其他空位.....\r\n");
                                seatId = "0";
                                continue;
                            }
                        }
                        else
                        {
                            Book.freeSeats.Clear();

                            if (roomId == "0")
                            {
                                foreach (var room in rooms)
                                {
                                    string res = Book.SearchFreeSeat(buildingId, room, date, startTime, endTime);
                                    if (res == "Success")
                                    {
                                        break;
                                    }
                                    else if (res == "Connection lost")
                                    {
                                        Library.lib.txtLog.AppendText("\r\n\r\n连接丢失，30秒后尝试继续检索空位\r\n");
                                        Thread.Sleep(30000);
                                        continue;
                                    }
                                    Thread.Sleep(1500);
                                }
                            }
                            else
                            {
                                string res = Book.SearchFreeSeat(buildingId, roomId, date, startTime, endTime);
                                if (res == "Connection lost")
                                {
                                    Library.lib.txtLog.AppendText("\r\n\r\n连接丢失，30秒后尝试继续检索空位\r\n");
                                    Thread.Sleep(30000);
                                    continue;
                                }
                                else if (res == "Failed" && Library.lib.checkBox4.Checked)
                                {
                                    Library.lib.txtLog.AppendText("\r\n\r\n当前区域暂无空位，尝试全馆检索空位.....\r\n");
                                    foreach (var room in rooms)
                                    {
                                        string result = Book.SearchFreeSeat(buildingId, room, date, startTime, endTime);
                                        if (result == "Success")
                                        {
                                            break;
                                        }
                                        else if (result == "Connection lost")
                                        {
                                            Library.lib.txtLog.AppendText("\r\n\r\n连接丢失，30秒后尝试继续检索空位\r\n");
                                            Thread.Sleep(30000);
                                            continue;
                                        }
                                        Thread.Sleep(1500);
                                    }
                                }
                            }

                            foreach (var freeSeat in Book.freeSeats)
                            {
                                if (exchange && !Book.reserving && !cancelled)
                                {
                                    if (Book.StopUsing())
                                    {
                                        cancelled = true;
                                    }
                                    else
                                    {
                                        Library.lib.txtLog.AppendText("\r\n\r\n释放座位失败，请稍后重试\r\n");
                                        Library.lib.txtLog.AppendText("\r\n---------------------------退出抢座模式---------------------------\r\n");
                                        EnableControls();
                                        return;
                                    }
                                }
                                else if (exchange && Book.reserving && !cancelled)
                                {
                                    if (Book.CancelReservation(Book.res_id))
                                    {
                                        cancelled = true;
                                    }
                                    else
                                    {
                                        Library.lib.txtLog.AppendText("\r\n\r\n取消预约失败，请稍后重试\r\n");
                                        Library.lib.txtLog.AppendText("\r\n---------------------------退出抢座模式---------------------------\r\n");
                                        EnableControls();
                                        return;
                                    }
                                }

                                switch (Book.BookSeat(freeSeat.ToString(), date, startTime, endTime))
                                {
                                    case "Success":
                                        Book.bookedSeatId = freeSeat.ToString();
                                        Library.lib.txtLog.AppendText("\r\n\r\n---------------------------退出抢座模式---------------------------\r\n");
                                        Book.LockSeat(Book.bookedSeatId);
                                        EnableControls();
                                        return;
                                    case "Failed":
                                        Thread.Sleep(1500);
                                        break;
                                    case "Connection lost":
                                        Library.lib.txtLog.AppendText("\r\n\r\n连接丢失，30秒后重新尝试抢座，系统开放时间剩余" + (85500 - (int)DateTime.Now.TimeOfDay.TotalSeconds).ToString() + "秒\r\n");
                                        Thread.Sleep(30000);
                                        break;
                                }
                            }
                        }

                        Library.lib.txtLog.AppendText("\r\n\r\n暂无可用座位，系统开放时间剩余" + (85200 - (int)DateTime.Now.TimeOfDay.TotalSeconds).ToString() + "秒\r\n");
                        Thread.Sleep(1500);
                    }
                }
                else
                {
                    Library.lib.txtLog.AppendText("\r\n\r\n获取token失败，请检查网络后重试\r\n");
                    Library.lib.txtLog.AppendText("\r\n---------------------------退出抢座模式---------------------------\r\n");
                    EnableControls();
                    return;
                }
            }
            else
            {
                if (Book.GetToken(false) == "Success")
                {
                    if (Book.CheckResInf(false))
                    {
                        Library.lib.txtLog.AppendText("\r\n\r\n已检测到有效预约，将自动改签预约信息");
                        if (Book.ExchangeLoop(buildingId, rooms, startTime, endTime, roomId, seatId))
                        {
                            Book.LockSeat(Book.bookedSeatId);
                        }

                        EnableControls();
                    }
                    else
                    {
                        if (Book.Loop(buildingId, rooms, startTime, endTime, roomId, seatId))
                        {
                            Book.LockSeat(Book.bookedSeatId);
                        }

                        EnableControls();
                    }
                }
                else
                {
                    Library.lib.txtLog.AppendText("\r\n\r\n获取token失败，请检查网络后重试\r\n");
                    EnableControls();
                }

                return;
            }
        }

        public static void EnableControls()
        {
            Library.lib.lblLib.Enabled = true;
            Library.lib.lblRegion.Enabled = true;
            Library.lib.lblNum.Enabled = true;
            Library.lib.lblDate.Enabled = true;
            Library.lib.lblStart.Enabled = true;
            Library.lib.lblEnd.Enabled = true;
            Library.lib.lblWelcome.Enabled = true;
            Library.lib.comboLib.Enabled = true;
            Library.lib.comboRegion.Enabled = true;
            Library.lib.comboNum.Enabled = true;
            Library.lib.comboDate.Enabled = true;
            Library.lib.comboStart.Enabled = true;
            Library.lib.comboEnd.Enabled = true;
            Library.lib.checkBox1.Enabled = true;
            Library.lib.checkBox2.Enabled = true;
            Library.lib.checkBox3.Enabled = true;
            Library.lib.checkBox4.Enabled = true;
            Library.lib.iconPictureBox2.Enabled = true;
            Library.lib.btnExecute.Text = "运行";
        }
    }
}

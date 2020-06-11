using System;
using System.Collections;
using Trio.Forms;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace Trio
{
    class Book
    {
        private const string API_ROOT = "https://seat.lib.whu.edu.cn:8443/rest/";
        private const string API_V2_ROOT = "https://seat.lib.whu.edu.cn:8443/rest/v2/";

        public static readonly string[] xt = { "6", "7", "8", "9", "10", "11", "12", "16", "4", "5", "14", "15" };
        public static readonly string[] xt_lite = { "9", "11", "6", "7", "8", "10", "16" };
        public static readonly string[] gt = { "19", "29", "31", "32", "33", "34", "35", "37", "38" };
        public static readonly string[] yt = { "20", "21", "23", "24", "26", "27" };
        public static readonly string[] zt = { "39", "40", "51", "52", "56", "59", "60", "61", "62", "65", "66", "84", "85", "86", "87", "88", "89", "92" };

        public static ArrayList freeSeats = new ArrayList();
        private static ArrayList startTimes = new ArrayList(), endTimes = new ArrayList();
        public static string res_id, username, password, status, bookedSeatId, historyDate, historyStartTime, historyEndTime, historyAwayStartTime, token, name, last_login_time, state, violationCount;
        public static bool checkedIn, reserving, onlyPower, onlyWindow, onlyComputer;
        public static DateTime time;

        public static void Wait(string hour, string minute, string second, bool enter = true)
        {
            time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + hour + ":" + minute + ":" + second);
            if (DateTime.Compare(DateTime.Now, time) > 0)
            {
                time = time.AddDays(1);
            }
            Library.lib.backgroundWorker1.RunWorkerAsync(enter);
            while (true)
            {
                TimeSpan delta = time.Subtract(DateTime.Now);
                if (delta.TotalSeconds < 0)
                {
                    Library.lib.backgroundWorker1.CancelAsync();
                    while (true)
                    {
                        if (!Library.lib.backgroundWorker1.IsBusy)
                        {
                            break;
                        }
                    }
                    break;
                }
                Thread.Sleep(5);
            }
            return;
        }

        public static string GetToken(bool alert = true)
        {
            string url = API_ROOT + "auth?username=" + username + "&password=" + password;

            if (alert)
            {
                Library.lib.txtLog.AppendText("\r\nRequesting for token.....");
            }

            try
            {
                JObject res = HTTPRequest.HttpGetRequest(url, token);

                if (alert)
                {
                    Library.lib.txtLog.AppendText(res["status"].ToString());
                }

                if (res["status"].ToString() == "success")
                {
                    token = res["data"]["token"].ToString();
                    return "Success";
                }
                else
                {
                    if (alert)
                    {
                        Library.lib.txtLog.AppendText("\r\n" + res.ToString());
                    }
                    return res["message"].ToString();
                }
            }
            catch
            {
                if (alert)
                {
                    Library.lib.txtLog.AppendText("Connection lost");
                }
                return "Connection lost";
            }
        }

        public static bool CheckResInf(bool alert = true, bool modal = false)
        {
            string url = API_V2_ROOT + "history/1/30";
            string[] probableStatus = { "RESERVE", "CHECK_IN", "AWAY" };

            try
            {
                JObject res = HTTPRequest.HttpGetRequest(url, token, 2000);

                if (res["status"].ToString() == "success")
                {
                    foreach (JToken reservations in res["data"]["reservations"])
                    {
                        if (probableStatus.Contains(reservations["stat"].ToString()))
                        {
                            res_id = reservations["id"].ToString();
                            if (alert)
                            {
                                Reservation reservation = new Reservation(modal);
                                reservation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                reservation.label2.Text = " ID: " + reservations["id"] + "\r\n 时间: " + reservations["date"] + " " + reservations["begin"] + "~" + reservations["end"];
                                status = reservations["stat"].ToString();
                                switch (status)
                                {
                                    case "RESERVE":
                                        reservation.label2.Text += "\r\n 状态: 预约";
                                        reservation.label3.Text = "是否取消此预约？";
                                        reserving = true;
                                        break;
                                    case "CHECK_IN":
                                        if (reservations["awayEnd"].ToString() != "")
                                        {
                                            reservation.label2.Text += "\r\n 暂离时间: " + reservations["awayBegin"].ToString() + "~" + reservations["awayEnd"].ToString();
                                            reservation.label3.Location = new Point(140, 150);
                                        }
                                        reservation.label2.Text += "\r\n 状态: 履约中";
                                        reservation.label3.Text = "是否释放此座位？";
                                        reserving = false;
                                        break;
                                    case "AWAY":
                                        reservation.label2.Text += "\r\n 暂离时间: " + reservations["awayBegin"].ToString();
                                        reservation.label2.Text += "\r\n 状态: 暂离";
                                        reservation.label3.Text = "是否释放此座位？";
                                        reservation.label3.Location = new Point(140, 150);
                                        reserving = false;
                                        break;
                                }
                                reservation.label2.Text = reservation.label2.Text + "\r\n 地址: " + reservations["loc"].ToString() + "\r\n----------------------------------------------------------------";
                                if (modal)
                                {
                                    reservation.ShowDialog();
                                }
                                else
                                {
                                    reservation.Show();
                                }
                            }
                            else
                            {
                                status = reservations["stat"].ToString();
                                historyDate = reservations["date"].ToString();
                                historyStartTime = reservations["begin"].ToString();
                                historyEndTime = reservations["end"].ToString();
                                reserving = (status == "RESERVE") ? true : false;
                                if (status == "AWAY")
                                {
                                    historyAwayStartTime = reservations["awayBegin"].ToString();
                                }
                            }

                            return true;
                        }
                    }

                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool GetUsrInf(bool alert = true)
        {
            string url = API_V2_ROOT + "user";

            if (alert)
            {
                Library.lib.txtLog.AppendText("\r\nFetching user info.....");
            }

            try
            {
                JObject res = HTTPRequest.HttpGetRequest(url, token);

                if (alert)
                {
                    Library.lib.txtLog.AppendText(res["status"].ToString());
                }

                if (res["status"].ToString() == "success")
                {
                    name = res["data"]["name"].ToString();
                    last_login_time = res["data"]["lastLogin"].ToString();
                    if (res["data"]["checkedIn"].ToString() == "True")
                    {
                        checkedIn = true;
                        state = "已进入" + res["data"]["lastInBuildingName"].ToString();
                    }
                    else
                    {
                        checkedIn = false;
                        state = "未入馆";
                    }

                    violationCount = res["data"]["violationCount"].ToString();

                    return true;
                }
                else
                {
                    if (alert)
                    {
                        Library.lib.txtLog.AppendText("\r\n" + res.ToString());
                    }
                    return false;
                }
            }
            catch
            {
                if (alert)
                {
                    Library.lib.txtLog.AppendText("Connection lost");
                }
                return false;
            }
        }

        public static bool GetRooms(string buildingId)
        {
            string url = API_V2_ROOT + "room/stats2/" + buildingId;
            Library.lib.txtLog.AppendText("\r\nFetching room info.....");

            try
            {
                JObject res = HTTPRequest.HttpGetRequest(url, token);
                Library.lib.txtLog.AppendText(res["status"].ToString());

                if (res["status"].ToString() == "success")
                {
                    JToken jToken = res["data"];
                    Library.lib.txtLog.AppendText("\r\n\r\n当前座位状态：");

                    foreach (var room in jToken)
                    {
                        Library.lib.txtLog.AppendText("\r\n\r\n" + room["room"].ToString() + "\r\n楼层：" + room["floor"].ToString() + "\r\n总座位数：" + room["totalSeats"].ToString() + "\r\n已预约：" + room["reserved"].ToString() + "\r\n正在使用：" + room["inUse"].ToString() + "\r\n暂离：" + room["away"].ToString() + "\r\n空闲：" + room["free"].ToString());
                    }
                    Library.lib.txtLog.AppendText("\r\n");

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Library.lib.txtLog.AppendText("Connection lost");
                return false;
            }
        }

        public static bool GetSeats(string roomId, ArrayList seats)
        {
            string url = API_V2_ROOT + "room/layoutByDate/" + roomId + "/" + DateTime.Now.ToString("yyyy-MM-dd");
            Library.lib.txtLog.AppendText("\r\nFetching seat info in room " + roomId + ".....");

            try
            {
                JObject res = HTTPRequest.HttpGetRequest(url, token);
                Library.lib.txtLog.AppendText(res["status"].ToString());

                if (res["status"].ToString() == "success")
                {
                    JToken layout = res["data"]["layout"];
                    foreach (var num in layout)
                    {
                        if (num.First["type"].ToString() == "seat")
                        {
                            string seatInfo = num.First["name"].ToString();
                            if (num.First["power"].ToString() == "True")
                                seatInfo += " (电源)";
                            if (num.First["window"].ToString() == "True")
                                seatInfo += " (靠窗)";
                            if (num.First["computer"].ToString() == "True")
                                seatInfo += " (电脑)";
                            seats.Add(new DictionaryEntry(num.First["id"].ToString(), seatInfo));
                        }
                    }
                    NewComparer newComparer = new NewComparer();
                    seats.Sort(newComparer);

                    return true;
                }
                else
                {
                    Library.lib.txtLog.AppendText("\r\n" + res.ToString());
                    return false;
                }
            }
            catch
            {
                Library.lib.txtLog.AppendText("Connection lost");
                return false;
            }
        }

        public static bool CancelReservation(string id, bool alert = true)
        {
            string url = API_V2_ROOT + "cancel/" + id;

            if (alert)
            {
                Library.lib.txtLog.AppendText("\r\nCancelling reservation.....");
            }

            try
            {
                JObject res = HTTPRequest.HttpGetRequest(url, token);

                if (alert)
                {
                    Library.lib.txtLog.AppendText(res["status"].ToString());
                }

                if (res["status"].ToString() == "success")
                {
                    return true;
                }
                else
                {
                    if (alert)
                    {
                        Library.lib.txtLog.AppendText("\r\n\r\n取消预约失败，原因：" + res["message"].ToString());
                    }
                    return false;
                }
            }
            catch
            {
                if (alert)
                {
                    Library.lib.txtLog.AppendText("Connection lost");
                }
                return false;
            }
        }

        public static bool StopUsing(bool alert = true)
        {
            string url = API_V2_ROOT + "stop";

            if (alert)
            {
                Library.lib.txtLog.AppendText("\r\nReleasing seat.....");
            }

            try
            {
                JObject res = HTTPRequest.HttpGetRequest(url, token);

                if (alert)
                {
                    Library.lib.txtLog.AppendText(res["status"].ToString());
                }

                if (res["status"].ToString() == "success")
                {
                    return true;
                }
                else
                {
                    if (alert)
                    {
                        Library.lib.txtLog.AppendText("\r\n\r\n释放座位失败，原因：" + res["message"].ToString());
                    }
                    return false;
                }
            }
            catch
            {
                if (alert)
                {
                    Library.lib.txtLog.AppendText("Connection lost");
                }
                return false;
            }
        }

        public static string SearchFreeSeat(string buildingId, string roomId, string date, string startTime, string endTime)
        {
            if (startTime == "-1")
            {
                startTime = ((int)DateTime.Now.TimeOfDay.TotalMinutes).ToString();
            }

            string url = API_V2_ROOT + "searchSeats/" + date + "/" + startTime + "/" + endTime;
            Library.lib.txtLog.AppendText("\r\nFetching free seats in room " + roomId + ".....");

            StringBuilder buffer = new StringBuilder();
            buffer.AppendFormat("{0}={1}", "t", "1");
            buffer.AppendFormat("&{0}={1}", "roomId", roomId);
            buffer.AppendFormat("&{0}={1}", "buildingId", buildingId);
            buffer.AppendFormat("&{0}={1}", "batch", "9999");
            buffer.AppendFormat("&{0}={1}", "page", "1");
            buffer.AppendFormat("&{0}={1}", "t2", "2");
            byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());

            try
            {
                JObject res = HTTPRequest.HttpPostRequest(url, token, data);

                if (res["data"]["seats"].ToString() != "{}")
                {
                    JToken seats = res["data"]["seats"];
                    foreach (var num in seats)
                    {
                        if (onlyPower && num.First["power"].ToString() == "False")
                        {
                            continue;
                        }
                        if (onlyWindow && num.First["window"].ToString() == "False")
                        {
                            continue;
                        }
                        if (onlyComputer && num.First["computer"].ToString() == "False")
                        {
                            continue;
                        }
                        freeSeats.Add(num.First["id"].ToString());
                    }
                    if (freeSeats.Count > 0)
                    {
                        Library.lib.txtLog.AppendText("success");
                        return "Success";
                    }
                    else
                    {
                        Library.lib.txtLog.AppendText("fail");
                        return "Failed";
                    }
                }
                else
                {
                    Library.lib.txtLog.AppendText("fail");
                    return "Failed";
                }
            }
            catch
            {
                Library.lib.txtLog.AppendText("Connection lost");
                return "Connection lost";
            }
        }

        public static bool CheckStartTime(string seatId, string date, string startTime)
        {
            if (startTime == "-1")
            {
                startTime = "now";
            }

            string url = API_V2_ROOT + "startTimesForSeat/" + seatId + "/" + date;
            Library.lib.txtLog.AppendText("\r\nChecking start time of seat No." + seatId + ".....");

            try
            {
                JObject res = HTTPRequest.HttpGetRequest(url, token);

                if (res["status"].ToString() == "success")
                {
                    startTimes.Clear();
                    JToken getStartTimes = res["data"]["startTimes"];
                    foreach (var time in getStartTimes)
                    {
                        startTimes.Add((time["id"].ToString()));
                    }

                    if (startTimes.Contains(startTime))
                    {
                        Library.lib.txtLog.AppendText("success");
                        return true;
                    }
                    else
                    {
                        Library.lib.txtLog.AppendText("fail");
                        return false;
                    }
                }
                else
                {
                    Library.lib.txtLog.AppendText("fail");
                    return false;
                }
            }
            catch
            {
                Library.lib.txtLog.AppendText("Connection lost");
                return false;
            }
        }

        public static bool CheckEndTime(string seatId, string date, string startTime, string endTime)
        {
            if (startTime == "-1")
            {
                startTime = ((int)DateTime.Now.TimeOfDay.TotalMinutes).ToString();
            }

            string url = API_V2_ROOT + "endTimesForSeat/" + seatId + "/" + date + "/" + startTime;
            Library.lib.txtLog.AppendText("\r\nChecking end time of seat No." + seatId + ".....");

            try
            {
                JObject res = HTTPRequest.HttpGetRequest(url, token);

                if (res["status"].ToString() == "success")
                {
                    endTimes.Clear();
                    JToken getEndTimes = res["data"]["endTimes"];
                    foreach (var time in getEndTimes)
                    {
                        endTimes.Add((time["id"].ToString()));
                    }

                    if (endTimes.Contains(endTime))
                    {
                        Library.lib.txtLog.AppendText("success");
                        return true;
                    }
                    else
                    {
                        Library.lib.txtLog.AppendText("fail");
                        return false;
                    }
                }
                else
                {
                    Library.lib.txtLog.AppendText("fail");
                    return false;
                }
            }
            catch
            {
                Library.lib.txtLog.AppendText("Connection lost");
                return false;
            }
        }

        public static string BookSeat(string seatId, string date, string startTime, string endTime, bool alert = true)
        {
            string url = API_V2_ROOT + "freeBook";

            StringBuilder buffer = new StringBuilder();
            buffer.AppendFormat("{0}={1}", "t", "1");
            buffer.AppendFormat("&{0}={1}", "startTime", startTime);
            buffer.AppendFormat("&{0}={1}", "endTime", endTime);
            buffer.AppendFormat("&{0}={1}", "seat", seatId);
            buffer.AppendFormat("&{0}={1}", "date", date);
            buffer.AppendFormat("&{0}={1}", "t2", "2");
            byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());

            if (alert)
            {
                Library.lib.txtLog.AppendText("\r\n\r\nBooking seat No." + seatId + ".....");
            }

            try
            {
                JObject res = HTTPRequest.HttpPostRequest(url, token, data);

                if (alert)
                {
                    Library.lib.txtLog.AppendText(res["status"].ToString() + "\r\n");
                }

                if (res["status"].ToString() == "success")
                {
                    bookedSeatId = seatId;
                    if (alert)
                    {
                        res.Remove("status");
                        res.Remove("message");
                        res.Remove("code");

                        PrintBookInf(res);

                    }
                    return "Success";
                }
                else
                {
                    if (alert)
                    {
                        Library.lib.txtLog.AppendText("\r\n" + res["message"].ToString() + "\r\n");
                    }
                    return "Failed";
                }
            }
            catch
            {
                if (alert)
                {
                    Library.lib.txtLog.AppendText("Connection lost");
                }
                return "Connection lost";
            }
        }

        public static void PrintBookInf(JObject info)
        {
            Library.lib.txtLog.AppendText("\r\n---------------------------------座位预约成功-------------------------------------");
            Library.lib.txtLog.AppendText("\r\nID：" + info["data"]["id"]);
            Library.lib.txtLog.AppendText("\r\n凭证号码：" + info["data"]["receipt"]);
            Library.lib.txtLog.AppendText("\r\n时间：" + info["data"]["onDate"] + " " + info["data"]["begin"] + " ~ " + info["data"]["end"]);
            Library.lib.txtLog.AppendText("\r\n状态：" + (info["data"]["checkedIn"].ToString() == "True" ? "已签到" : "预约"));
            Library.lib.txtLog.AppendText("\r\n地址：" + info["data"]["location"]);
            Library.lib.txtLog.AppendText("\r\n----------------------------------------------------------------------------------------");
        }

        public static void LockSeat(string seatId)
        {
            int index, /*linesCount, */count = 0;
            bool doClear = false, reBook = false;
            Library.lib.txtLog.AppendText("\r\n正在锁定座位，ID: " + seatId + "\r\n");
            if (!CheckResInf(false))
            {
                Library.lib.txtLog.AppendText("\r\n\r\n预约信息获取失败");
                return;
            }
            while (true)
            {
                if (count >= 50)
                {
                    Library.lib.txtLog.AppendText("\r\n\r\n座位锁定失败");
                    break;
                }

                if (historyDate == DateTime.Now.ToString("yyyy-M-d") && DateTime.Now.TimeOfDay.TotalMinutes > 400 && DateTime.Now.TimeOfDay.TotalMinutes < 1320)
                {
                    if (GetToken(false) == "Success")
                    {
                        if (CheckResInf(false) || reBook)
                        {
                            int historyEndTimeInt = int.Parse(historyEndTime.Substring(0, 2)) * 60 + int.Parse(historyEndTime.Substring(3, 2));

                            if (historyEndTimeInt - (int)DateTime.Now.TimeOfDay.TotalMinutes < 2)
                            {
                                if (reserving)
                                {
                                    Library.lib.txtLog.AppendText("\r\n\r\n座位预约时间已过，自动取消预约");
                                    CancelReservation(res_id);
                                }
                                else
                                {
                                    Library.lib.txtLog.AppendText("\r\n\r\n座位预约时间已过，自动释放座位");
                                    StopUsing();
                                }
                                break;
                            }

                            if (reserving && !checkedIn)
                            {
                                int historyStartTimeInt = int.Parse(historyStartTime.Substring(0, 2)) * 60 + int.Parse(historyStartTime.Substring(3, 2));

                                if ((int)DateTime.Now.TimeOfDay.TotalMinutes - historyStartTimeInt >= 25)
                                {
                                    if (CancelReservation(res_id, false) || reBook)
                                    {
                                        if (BookSeat(seatId, DateTime.Now.ToString("yyyy-MM-dd"), "-1", historyEndTimeInt.ToString(), false) != "Success")
                                        {
                                            if (doClear)
                                            {
                                                index = Library.lib.txtLog.GetFirstCharIndexOfCurrentLine();
                                                Library.lib.txtLog.Select(index, Library.lib.txtLog.TextLength - index);
                                                Library.lib.txtLog.SelectedText = "重新预约座位失败，重试次数: " + count;
                                            }
                                            else
                                            {
                                                Library.lib.txtLog.AppendText("\r\n重新预约座位失败，重试次数: " + count);
                                                doClear = true;
                                            }
                                            Thread.Sleep(5000);
                                            reBook = true;
                                            count += 1;
                                            continue;
                                        }
                                        else
                                        {
                                            reBook = false;
                                        }
                                    }
                                    else
                                    {
                                        if (doClear)
                                        {
                                            index = Library.lib.txtLog.GetFirstCharIndexOfCurrentLine();
                                            Library.lib.txtLog.Select(index, Library.lib.txtLog.TextLength - index);
                                            Library.lib.txtLog.SelectedText = "取消预约失败，重试次数: " + count;
                                        }
                                        else
                                        {
                                            Library.lib.txtLog.AppendText("\r\n取消预约失败，重试次数: " + count);
                                            doClear = true;
                                        }
                                        Thread.Sleep(5000);
                                        count += 1;
                                        continue;
                                    }
                                }
                            }
                            else if (status == "AWAY")
                            {
                                int historyAwayStartTimeInt = int.Parse(historyAwayStartTime.Substring(0, 2)) * 60 + int.Parse(historyAwayStartTime.Substring(3, 2));

                                if ((int)DateTime.Now.TimeOfDay.TotalMinutes - historyAwayStartTimeInt >= 25)
                                {
                                    if (StopUsing(false) || reBook)
                                    {
                                        if (BookSeat(seatId, DateTime.Now.ToString("yyyy-MM-dd"), "-1", historyEndTimeInt.ToString(), false) != "Success")
                                        {
                                            if (doClear)
                                            {
                                                index = Library.lib.txtLog.GetFirstCharIndexOfCurrentLine();
                                                Library.lib.txtLog.Select(index, Library.lib.txtLog.TextLength - index);
                                                Library.lib.txtLog.SelectedText = "重新预约座位失败，重试次数: " + count;
                                            }
                                            else
                                            {
                                                Library.lib.txtLog.AppendText("\r\n重新预约座位失败，重试次数: " + count);
                                                doClear = true;
                                            }
                                            Thread.Sleep(5000);
                                            reBook = true;
                                            count += 1;
                                            continue;
                                        }
                                        else
                                        {
                                            reBook = false;
                                        }
                                    }
                                    else
                                    {
                                        if (doClear)
                                        {
                                            index = Library.lib.txtLog.GetFirstCharIndexOfCurrentLine();
                                            Library.lib.txtLog.Select(index, Library.lib.txtLog.TextLength - index);
                                            Library.lib.txtLog.SelectedText = "释放座位失败，重试次数: " + count;
                                        }
                                        else
                                        {
                                            Library.lib.txtLog.AppendText("\r\n释放座位失败，重试次数: " + count);
                                            doClear = true;
                                        }
                                        Thread.Sleep(5000);
                                        count += 1;
                                        continue;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (doClear)
                            {
                                index = Library.lib.txtLog.GetFirstCharIndexOfCurrentLine();
                                Library.lib.txtLog.Select(index, Library.lib.txtLog.TextLength - index);
                                Library.lib.txtLog.SelectedText = "获取预约信息失败，重试次数: " + count;
                            }
                            else
                            {
                                Library.lib.txtLog.AppendText("\r\n获取预约信息失败，重试次数: " + count);
                                doClear = true;
                            }
                            Thread.Sleep(5000);
                            count += 1;
                            continue;
                        }
                    }
                    else
                    {
                        if (doClear)
                        {
                            index = Library.lib.txtLog.GetFirstCharIndexOfCurrentLine();
                            Library.lib.txtLog.Select(index, Library.lib.txtLog.TextLength - index);
                            Library.lib.txtLog.SelectedText = "获取token失败，重试次数: " + count;
                        }
                        else
                        {
                            Library.lib.txtLog.AppendText("\r\n获取token失败，重试次数: " + count);
                            doClear = true;
                        }
                        Thread.Sleep(5000);
                        count += 1;
                        continue;
                    }
                }
                else if (historyDate == DateTime.Now.ToString("yyyy-M-d") && DateTime.Now.TimeOfDay.TotalMinutes > 1320)
                {
                    return;
                }
                //count = 0;
                //linesCount = Library.lib.txtLog.Lines.Count();
                //index = Library.lib.txtLog.GetFirstCharIndexFromLine(linesCount - (doClear ? 2 : 1));
                //Library.lib.txtLog.Select(index, Library.lib.txtLog.TextLength - index);
                //Library.lib.txtLog.SelectedText = "当前有效" + (reserving ? "预约" : "使用") + "时间: " + historyDate + " " + historyStartTime + "~" + historyEndTime;
                doClear = false;
                Thread.Sleep(30000);
            }
        }

        public static bool Loop(string buildingId, string[] rooms, string startTime, string endTime, string roomId = "0", string seatId = "0")
        {
            Library.lib.txtLog.AppendText("\r\n\r\n---------------------------------进入捡漏模式---------------------------------\r\n");

            if (DateTime.Now.TimeOfDay.TotalMinutes < 60 || DateTime.Now.TimeOfDay.TotalMinutes > 1420)
            {
                Wait("01", "00", "00", false);
            }
            else if (DateTime.Now.TimeOfDay.TotalMinutes > 1320)
            {
                Library.lib.txtLog.AppendText("\r\n捡漏失败，超出系统开放时间\r\n");
                Library.lib.txtLog.AppendText("\r\n---------------------------------退出捡漏模式---------------------------------\r\n");
                return false;
            }

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            GetRooms(buildingId);

            if (seatId != "0" && !Library.lib.checkBox4.Checked)
            {
                Library.lib.txtLog.AppendText("\r\n正在监控座位，ID: " + seatId + "\r\n");
            }
            else if (roomId != "0" && !Library.lib.checkBox4.Checked)
            {
                Library.lib.txtLog.AppendText("\r\n正在监控区域，ID: " + roomId + "\r\n");
            }

            while (true)
            {
                if (DateTime.Now.TimeOfDay.TotalMinutes > 1320)
                {
                    Library.lib.txtLog.AppendText("\r\n\r\n捡漏失败，超出系统开放时间\r\n");
                    Library.lib.txtLog.AppendText("\r\n---------------------------------退出捡漏模式---------------------------------\r\n");
                    return false;
                }

                if (startTime != "-1" && (int)DateTime.Now.TimeOfDay.TotalMinutes > int.Parse(startTime))
                {
                    startTime = "-1";
                }

                if (seatId != "0")
                {
                    string res = BookSeat(seatId, date, startTime, endTime);

                    if (res == "Success")
                    {
                        Library.lib.txtLog.AppendText("\r\n\r\n捡漏成功\r\n");
                        Library.lib.txtLog.AppendText("\r\n---------------------------------退出捡漏模式---------------------------------\r\n");
                        return true;
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
                    freeSeats.Clear();

                    if (roomId == "0")
                    {
                        foreach (var room in rooms)
                        {
                            string res = SearchFreeSeat(buildingId, room, date, startTime, endTime);
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
                        string res = SearchFreeSeat(buildingId, roomId, date, startTime, endTime);
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
                                string result = SearchFreeSeat(buildingId, room, date, startTime, endTime);
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

                    foreach (var freeSeatId in freeSeats)
                    {
                        switch (BookSeat(freeSeatId.ToString(), date, startTime, endTime))
                        {
                            case "Success":
                                Library.lib.txtLog.AppendText("\r\n\r\n捡漏成功\r\n");
                                Library.lib.txtLog.AppendText("\r\n---------------------------------退出捡漏模式---------------------------------\r\n");
                                return true;
                            case "Failed":
                                Thread.Sleep(1500);
                                break;
                            case "Connection lost":
                                Library.lib.txtLog.AppendText("\r\n\r\n连接丢失，30秒后尝试继续预约空位\r\n");
                                Thread.Sleep(30000);
                                break;
                        }
                    }
                }

                Library.lib.txtLog.AppendText("\r\n\r\n暂无可用座位，系统开放时间剩余" + (79200 - (int)DateTime.Now.TimeOfDay.TotalSeconds).ToString() + "秒\r\n");
                Thread.Sleep(1500);
            }
        }

        public static bool ExchangeLoop(string buildingId, string[] rooms, string startTime, string endTime, string roomId = "0", string seatId = "0")
        {
            Library.lib.txtLog.AppendText("\r\n\r\n---------------------------------进入改签模式---------------------------------\r\n");

            if (DateTime.Now.TimeOfDay.TotalMinutes < 60 || DateTime.Now.TimeOfDay.TotalMinutes > 1420)
            {
                Wait("01", "00", "00", false);
            }
            else if (DateTime.Now.TimeOfDay.TotalMinutes > 1320)
            {
                Library.lib.txtLog.AppendText("\r\n改签失败，超出系统开放时间\r\n");
                Library.lib.txtLog.AppendText("\r\n---------------------------------退出改签模式---------------------------------\r\n");
                return false;
            }

            bool cancelled = false;
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            GetRooms(buildingId);

            if (seatId != "0" && !Library.lib.checkBox4.Checked)
            {
                Library.lib.txtLog.AppendText("\r\n正在监控座位，ID: " + seatId + "\r\n");
            }
            else if (roomId != "0" && !Library.lib.checkBox4.Checked)
            {
                Library.lib.txtLog.AppendText("\r\n正在监控区域，ID: " + roomId + "\r\n");
            }

            while (true)
            {
                if (DateTime.Now.TimeOfDay.TotalMinutes > 1320)
                {
                    Library.lib.txtLog.AppendText("\r\n\r\n改签失败，超出系统开放时间\r\n");
                    Library.lib.txtLog.AppendText("\r\n---------------------------------退出改签模式---------------------------------\r\n");
                    return false;
                }

                if (startTime != "-1" && (int)DateTime.Now.TimeOfDay.TotalMinutes > int.Parse(startTime))
                {
                    startTime = "-1";
                }

                if (seatId != "0")
                {
                    if (CheckStartTime(seatId, date, startTime) && CheckEndTime(seatId, date, startTime, endTime))
                    {
                        GetUsrInf(true);
                        if (!reserving)
                        {
                            if (StopUsing())
                            {
                                if (BookSeat(seatId, date, startTime, endTime) == "Success")
                                {
                                    Library.lib.txtLog.AppendText("\r\n\r\n改签成功\r\n");
                                    Library.lib.txtLog.AppendText("\r\n---------------------------------退出改签模式---------------------------------\r\n");
                                    return true;
                                }
                                else
                                {
                                    Library.lib.txtLog.AppendText("\r\n\r\n改签失败，原座位已丢失\r\n");
                                    Library.lib.txtLog.AppendText("\r\n---------------------------------退出改签模式--------------------------------\r\n");
                                    return false;
                                }
                            }
                            else
                            {
                                Library.lib.txtLog.AppendText("\r\n\r\n---------------------------------退出改签模式---------------------------------\r\n");
                                return false;
                            }
                        }
                        else
                        {
                            if (CancelReservation(res_id))
                            {
                                if (BookSeat(seatId, date, startTime, endTime) == "Success")
                                {
                                    Library.lib.txtLog.AppendText("\r\n\r\n改签成功\r\n");
                                    Library.lib.txtLog.AppendText("\r\n---------------------------------退出改签模式---------------------------------\r\n");
                                    return true;
                                }
                                else
                                {
                                    Library.lib.txtLog.AppendText("\r\n\r\n改签失败，原座位已丢失\r\n");
                                    Library.lib.txtLog.AppendText("\r\n---------------------------------退出改签模式---------------------------------\r\n");
                                    return false;
                                }
                            }
                            else
                            {
                                Library.lib.txtLog.AppendText("\r\n\r\n---------------------------------退出改签模式--------------------------------\r\n");
                                return false;
                            }
                        }
                    }
                    else if (Library.lib.checkBox4.Checked)
                    {
                        Library.lib.txtLog.AppendText("\r\n\r\n指定座位改签失败，尝试检索其他空位.....\r\n");
                        seatId = "0";
                        continue;
                    }
                }
                else
                {
                    freeSeats.Clear();

                    if (roomId == "0")
                    {
                        foreach (var room in rooms)
                        {
                            string res = SearchFreeSeat(buildingId, room, date, startTime, endTime);
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
                        string res = SearchFreeSeat(buildingId, roomId, date, startTime, endTime);
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
                                string result = SearchFreeSeat(buildingId, room, date, startTime, endTime);
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

                    foreach (var freeSeatId in freeSeats)
                    {
                        if (!cancelled)
                        {
                            if (CheckStartTime(freeSeatId.ToString(), date, startTime) && CheckEndTime(freeSeatId.ToString(), date, startTime, endTime))
                            {
                                GetUsrInf(true);
                                if (!reserving)
                                {
                                    if (StopUsing())
                                    {
                                        cancelled = true;
                                    }
                                    else
                                    {
                                        Library.lib.txtLog.AppendText("\r\n\r\n---------------------------------退出改签模式---------------------------------\r\n");
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (CancelReservation(res_id))
                                    {
                                        cancelled = true;
                                    }
                                    else
                                    {
                                        Library.lib.txtLog.AppendText("\r\n\r\n---------------------------------退出改签模式---------------------------------\r\n");
                                        return false;
                                    }
                                }

                                switch (BookSeat(freeSeatId.ToString(), date, startTime, endTime))
                                {
                                    case "Success":
                                        Library.lib.txtLog.AppendText("\r\n\r\n改签成功\r\n");
                                        Library.lib.txtLog.AppendText("\r\n---------------------------------退出改签模式---------------------------------\r\n");
                                        return true;
                                    case "Failed":
                                        Thread.Sleep(1500);
                                        break;
                                    case "Connection lost":
                                        Library.lib.txtLog.AppendText("\r\n\r\n连接丢失，30秒后尝试继续预约空位\r\n");
                                        Thread.Sleep(30000);
                                        break;
                                }
                            }
                        }
                    }
                }

                Library.lib.txtLog.AppendText("\r\n\r\n暂无可用座位，系统开放时间剩余" + (79200 - (int)DateTime.Now.TimeOfDay.TotalSeconds).ToString() + "秒\r\n");
                Thread.Sleep(1500);
            }
        }


    }
}

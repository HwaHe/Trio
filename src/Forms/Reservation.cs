using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trio.Forms
{
    public partial class Reservation : Form
    {
        bool modal, exitFlag = true;
        public Reservation(bool modal)
        {
            this.modal = modal;
            InitializeComponent();
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            if (Book.reserving)
            {
                if (!Book.CancelReservation(Book.res_id, false))
                {

                    MessageBox.Show("取消预约失败，请稍后重试", "提示");
                }
            }
            else
            {
                if (!Book.StopUsing(false))
                {
                    MessageBox.Show("释放座位失败，请稍后重试", "提示");
                }
            }

            exitFlag = false;
            Close();
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Reservation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!modal && exitFlag)
            {
                this.Close();
            }
        }
    }
}


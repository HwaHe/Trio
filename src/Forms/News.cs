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
    public partial class News : Form
    {
        public News()
        {
            InitializeComponent();
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
    }
}

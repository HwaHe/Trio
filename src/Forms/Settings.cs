using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trio.Forms
{
    public partial class Settings : Form
    {
        public static Main main;
        public Settings()
        {
            InitializeComponent();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", main.HomeDir);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    main.HomeDir = fbd.SelectedPath;
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", main.WallPaperDir);
        }

        private void MyCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            return;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            return;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            return;
        }


        //private void PnlTitle_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics pnlLine = pnlTitle.CreateGraphics();
        //    Pen blackPen = new Pen(Color.FromArgb(220, 220, 220), 1.5f);
        //    pnlLine.DrawLine(blackPen, 0, 119, 934, 119);
        //}
    }
}

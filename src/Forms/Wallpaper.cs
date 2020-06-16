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

namespace Trio.Forms
{
    public partial class Wallpaper : Form
    {
        public Wallpaper()
        {
            InitializeComponent();
        }

        public string savepth;

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
          int uAction,
          int uParam,
          string lpvParam,
          int fuWinIni
);

        private void 设为壁纸ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dirPath = savepth + "\\" + this.textBox1.Text + ".jpg";
            SystemParametersInfo(20,1,dirPath,1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.Image == null)
            {
                //不显示窗口
            }
            else
            {
                //在新窗口显示大图
                Form formnew = new Form();
                formnew.Width = 1920;
                formnew.Height = 1080;
                formnew.Text = "pic1";
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 1080;
                pictureBox.Width = 1920;
                formnew.Controls.Add(pictureBox);
                pictureBox.Image = this.pictureBox1.Image;
                formnew.Show();
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (this.pictureBox2.Image == null)
            {

            }
            else
            {
                Form formnew = new Form();
                formnew.Width = 1920;
                formnew.Height = 1080;
                formnew.Text = "pic2";
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 1080;
                pictureBox.Width = 1920;
                formnew.Controls.Add(pictureBox);
                pictureBox.Image = this.pictureBox2.Image;
                formnew.Show();
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (this.pictureBox3.Image == null)
            {

            }
            else
            {
                Form formnew = new Form();
                formnew.Width = 1920;
                formnew.Height = 1080;
                formnew.Text = "pic3";
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 1080;
                pictureBox.Width = 1920;
                formnew.Controls.Add(pictureBox);
                pictureBox.Image = this.pictureBox3.Image;
                formnew.Show();
            }

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (this.pictureBox4.Image == null)
            {

            }
            else
            {
                Form formnew = new Form();
                formnew.Width = 1920;
                formnew.Height = 1080;
                formnew.Text = "pic4";
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 1080;
                pictureBox.Width = 1920;
                formnew.Controls.Add(pictureBox);
                pictureBox.Image = this.pictureBox4.Image;
                formnew.Show();
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (this.pictureBox5.Image == null)
            {

            }
            else
            {
                Form formnew = new Form();
                formnew.Width = 1920;
                formnew.Height = 1080;
                formnew.Text = "pic5";
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 1080;
                pictureBox.Width = 1920;
                formnew.Controls.Add(pictureBox);
                pictureBox.Image = this.pictureBox5.Image;
                formnew.Show();
            }

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (this.pictureBox6.Image == null)
            {

            }
            else
            {
                Form formnew = new Form();
                formnew.Width = 1920;
                formnew.Height = 1080;
                formnew.Text = "pic6";
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 1080;
                pictureBox.Width = 1920;
                formnew.Controls.Add(pictureBox);
                pictureBox.Image = this.pictureBox6.Image;
                formnew.Show();
            }

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (this.pictureBox8.Image == null)
            {

            }
            else
            {
                Form formnew = new Form();
                formnew.Width = 1920;
                formnew.Height = 1080;
                formnew.Text = "pic8";
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 1080;
                pictureBox.Width = 1920;
                formnew.Controls.Add(pictureBox);
                pictureBox.Image = this.pictureBox8.Image;
                formnew.Show();
            }

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (this.pictureBox7.Image == null)
            {

            }
            else
            {
                Form formnew = new Form();
                formnew.Width = 1920;
                formnew.Height = 1080;
                formnew.Text = "pic7";
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 1080;
                pictureBox.Width = 1920;
                formnew.Controls.Add(pictureBox);
                pictureBox.Image = this.pictureBox7.Image;
                formnew.Show();
            }

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (this.pictureBox9.Image == null)
            {

            }
            else
            {
                Form formnew = new Form();
                formnew.Width = 1920;
                formnew.Height = 1080;
                formnew.Text = "pic9";
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 1080;
                pictureBox.Width = 1920;
                formnew.Controls.Add(pictureBox);
                pictureBox.Image = this.pictureBox9.Image;
                formnew.Show();
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string dirPath = savepth + "\\" + this.textBox2.Text + ".jpg";
            SystemParametersInfo(20, 1, dirPath, 1);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string dirPath = savepth + "\\" + this.textBox3.Text + ".jpg";
            SystemParametersInfo(20, 1, dirPath, 1);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string dirPath = savepth + "\\" + this.textBox4.Text + ".jpg";
            SystemParametersInfo(20, 1, dirPath, 1);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string dirPath = savepth + "\\" + this.textBox5.Text + ".jpg";
            SystemParametersInfo(20, 1, dirPath, 1);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string dirPath = savepth + "\\" + this.textBox6.Text + ".jpg";
            SystemParametersInfo(20, 1, dirPath, 1);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            string dirPath = savepth + "\\" + this.textBox7.Text + ".jpg";
            SystemParametersInfo(20, 1, dirPath, 1);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            string dirPath = savepth + "\\" + this.textBox8.Text + ".jpg";
            SystemParametersInfo(20, 1, dirPath, 1);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            string dirPath = savepth + "\\" + this.textBox9.Text + ".jpg";
            SystemParametersInfo(20, 1, dirPath, 1);
        }

    }
}

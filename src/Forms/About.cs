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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/HwaHe");
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/qichengit");
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/FlowWind1999");
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/HwaHe/Trio");
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Fndroid/clash_for_windows_pkg");
        }

        private void Label9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCrOMiLLn857KqOzZYIqO-hQ");
        }

        private void Label10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/c1aris/SeatKiller_UI");
        }

        private void Label11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/awesome-inc/FontAwesome.Sharp");
        }
    }
}

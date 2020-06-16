using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace Trio
{
    [ToolboxItem(true)]
    public class CircularButton : IconButton
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,  // x-coordinate of upper-left cornere
            int nTorRect,  // y-coordinate of upper-left corner
            int nRightRect,  // x-coordinate of lower-right corner
            int nBottomRect,  // y-coordinate of lower-right corner
            int nWidthEllipse,  // width of ellipse
            int nHeightEllipse  // height of ellipse
            );

        private int _Radius=20;
        /// <summary>
        /// set round radius for the panel
        /// </summary>
        [Description("Set round radius for the panel")]
        public int RoundRadius
        {
            get
            {
                return _Radius;
            }
            set
            {
                if (value < 0) { _Radius = 0; }
                else { _Radius = value; }
                base.Refresh();
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            base.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, _Radius, _Radius));
            base.OnPaint(e);
        }
    }
}

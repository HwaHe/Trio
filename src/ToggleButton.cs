using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trio
{
    public class ToggleButton:Control
    {
        private float diameter;
        private Rect rect;
        private RectangleF circle;
        private bool isOn;
        private float artis;
        private Color borderColor;
        private bool textEnabled;
        private string onText = "";
        private string offText = "";
        private Color onColor;
        private Color offColor;
        private Timer painTicker = new Timer();
        public event SliderChangedEventHandler SliderValueChanged;

        public ToggleButton()
        {
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
            this.artis = 4f;
            this.diameter = 30f;
            this.textEnabled = true;
            this.rect = new Rect(2f * this.diameter, this.diameter, this.diameter / 2f, 1f, 1f);
            this.circle = new RectangleF(1f, 1f, this.diameter, this.diameter);
            this.isOn = false;
            this.borderColor = Color.LightGray;
            this.painTicker.Tick += new EventHandler(this.paintTicker_Tick);
            this.painTicker.Interval = 1;
            this.onColor = Color.FromArgb(94, 148, 255);
            this.offColor = Color.White;
            this.onText = "ON";
            this.offText = "OFF";
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.Invalidate();
            base.OnEnabledChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.isOn = !this.isOn;
                this.IsON = this.isOn;
                base.OnMouseClick(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = (SmoothingMode)SmoothingMode.HighQuality;
            if (base.Enabled)
            {
                Pen pen;
                using (SolidBrush brush=new SolidBrush(this.isOn ? this.onColor : this.offColor))
                {
                    e.Graphics.FillPath((Brush)brush, this.rect.Path);
                }
                using (pen = new Pen(this.borderColor, 2f))
                {
                    e.Graphics.DrawPath(pen, this.rect.Path);
                }
                if (this.textEnabled)
                {
                    using (Font font = new Font("Century Gothic", (8.2f * this.diameter) / 48f, (FontStyle)FontStyle.Bold))
                    {
                        SolidBrush b = new SolidBrush(this.ForeColor);
                        int height = TextRenderer.MeasureText(this.onText, font).Height;
                        float num2 = (this.diameter - height) / 2f;
                        e.Graphics.DrawString(this.onText, font, b, 5f, num2 + 1f);
                        height = TextRenderer.MeasureText(this.offText, font).Height;
                        num2 = (this.diameter - height) / 2f;
                        e.Graphics.DrawString(this.offText, font, b, this.diameter + 2f, num2 + 1f);
                    }
                    using(SolidBrush brush2= new SolidBrush(Color.FromArgb(255, 255, 255)))
                    {
                        e.Graphics.FillEllipse((Brush)brush2, this.circle);
                    }
                    using (pen = new Pen(Color.LightGray, 1.2f))
                    {
                        e.Graphics.DrawEllipse(pen, this.circle);
                    }
                }
                else
                {
                    using (SolidBrush brush3=new SolidBrush(Color.FromArgb(255, 255, 255)))
                    {
                        using(SolidBrush brush4=new SolidBrush(Color.FromArgb(255, 255, 255)))
                        {
                            e.Graphics.FillPath((Brush)brush3, this.rect.Path);
                            e.Graphics.FillEllipse((Brush)brush4, this.circle);
                            e.Graphics.DrawEllipse(Pens.DarkGray, this.circle);
                        }
                    }
                }
            }
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.Width = (base.Height - 2) * 2;
            this.diameter = base.Width / 2;
            this.artis = (4f * this.diameter) * 30f;
            this.rect = new Rect(2f * this.diameter, this.diameter + 2f, this.diameter / 2f, 1f, 1f);
            this.circle = new RectangleF(!this.isOn ? 1f : base.Width - this.diameter - 1f, 1f, this.diameter, this.diameter);
            base.OnResize(e);
        }

        private void paintTicker_Tick(object sender, EventArgs e)
        {
            float x = this.circle.X;
            if (this.isOn)
            {
                if ((x + this.artis) <= (base.Width - this.diameter - 1f))
                {
                    x += this.artis;
                    this.circle = new RectangleF(x, 1f, this.diameter, this.diameter);
                }
                else
                {
                    x = base.Width - this.diameter - 1f;
                    this.circle = new RectangleF(x, 1f, this.diameter, this.diameter);
                    base.Invalidate();
                    this.painTicker.Stop();
                }
            }
            else if ((x - this.artis) >= 1f)
            {
                x -= this.artis;
                this.circle = new RectangleF(x, 1f, this.diameter, this.diameter);
            }
            else
            {
                x = 1f;
                this.circle = new RectangleF(x, 1f, this.diameter, this.diameter);
                base.Invalidate();
                this.painTicker.Stop();
            }
        }


        public bool TextEnabled
        {
            get => this.textEnabled;
            set
            {
                this.textEnabled = value;
                base.Invalidate();
            }
        }

        public bool IsON
        {
            get => this.isOn;
            set
            {
                this.painTicker.Stop();
                this.isOn = value;
                this.painTicker.Start();
                if (this.SliderValueChanged != null)
                {
                    this.SliderValueChanged(this, EventArgs.Empty);
                }
            }
        }

        public Color BorderColor
        {
            get => this.borderColor;
            set
            {
                this.borderColor = value;
                base.Invalidate();
            }
        }
        protected override Size DefaultSize => new Size(60, 35);
        public delegate void SliderChangedEventHandler(object sender, EventArgs e);

        public string OnText
        {
            get => this.onText;
            set
            {
                this.onText = value;
                base.Invalidate();
            }
        }

        public string OffText
        {
            get => this.offText;
            set
            {
                this.offText = value;
                base.Invalidate();
            }
        }

        public Color OnColor
        {
            get => this.onColor;
            set
            {
                this.onColor = value;
                base.Invalidate();
            }
        }

        public Color OffColor
        {
            get => this.offColor;
            set
            {
                this.offColor = value;
                base.Invalidate();
            }
        }
    }
}

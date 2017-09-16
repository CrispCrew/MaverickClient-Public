using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaverickClient.Theme
{
    public class FlatClose : Control
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();

        private int x;

        private Color _BaseColor;
        private Color _TextColor;

        [Category("Colors")]
        public Color BaseColor
        {
            get
            {
                return this._BaseColor;
            }
            set
            {
                this._BaseColor = value;
            }
        }

        [Category("Colors")]
        public Color TextColor
        {
            get
            {
                return this._TextColor;
            }
            set
            {
                this._TextColor = value;
            }
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object value)
        {
            List<WeakReference> _ENCList = FlatClose.__ENCList;
            bool flag = false;
            checked
            {
                try
                {
                    Monitor.Enter(_ENCList, ref flag);
                    bool flag2 = FlatClose.__ENCList.Count == FlatClose.__ENCList.Capacity;
                    if (flag2)
                    {
                        int num = 0;
                        int arg_44_0 = 0;
                        int num2 = FlatClose.__ENCList.Count - 1;
                        int num3 = arg_44_0;
                        while (true)
                        {
                            int arg_95_0 = num3;
                            int num4 = num2;
                            if (arg_95_0 > num4)
                            {
                                break;
                            }
                            WeakReference weakReference = FlatClose.__ENCList[num3];
                            flag2 = weakReference.IsAlive;
                            if (flag2)
                            {
                                bool flag3 = num3 != num;
                                if (flag3)
                                {
                                    FlatClose.__ENCList[num] = FlatClose.__ENCList[num3];
                                }
                                num++;
                            }
                            num3++;
                        }
                        FlatClose.__ENCList.RemoveRange(num, FlatClose.__ENCList.Count - num);
                        FlatClose.__ENCList.Capacity = FlatClose.__ENCList.Count;
                    }
                    FlatClose.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
                }
                finally
                {
                    bool flag3 = flag;
                    if (flag3)
                    {
                        Monitor.Exit(_ENCList);
                    }
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Console.WriteLine("Overridden?");
        }

        public FlatClose()
        {
            FlatClose.__ENCAddToList(this);
            this._BaseColor = Color.FromArgb(168, 35, 35);
            this._TextColor = Color.FromArgb(243, 243, 243);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            Size size = new Size(18, 18);
            this.Size = size;
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.Font = new Font("Marlett", 10f);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Graphics graphics2 = graphics;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics2.Clear(this.BackColor);
            graphics2.FillRectangle(new SolidBrush(this._BaseColor), rect);
            Graphics arg_A3_0 = graphics2;
            string arg_A3_1 = "r";
            Font arg_A3_2 = this.Font;
            Brush arg_A3_3 = new SolidBrush(this.TextColor);
            Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
            arg_A3_0.DrawString(arg_A3_1, arg_A3_2, arg_A3_3, r, Helpers.CenterSF);
            base.OnPaint(e);
            graphics.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
            bitmap.Dispose();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }

    [StandardModule]
	internal sealed class Helpers
    {
        internal static Graphics G;

        internal static Bitmap B;

        internal static Color _FlatColor = Color.FromArgb(35, 168, 109);

        internal static StringFormat NearSF = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Near
        };

        internal static StringFormat CenterSF = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public static GraphicsPath RoundRec(Rectangle Rectangle, int Curve)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            checked
            {
                int num = Curve * 2;
                GraphicsPath arg_2F_0 = graphicsPath;
                Rectangle rect = new Rectangle(Rectangle.X, Rectangle.Y, num, num);
                arg_2F_0.AddArc(rect, -180f, 90f);
                GraphicsPath arg_63_0 = graphicsPath;
                rect = new Rectangle(Rectangle.Width - num + Rectangle.X, Rectangle.Y, num, num);
                arg_63_0.AddArc(rect, -90f, 90f);
                GraphicsPath arg_A1_0 = graphicsPath;
                rect = new Rectangle(Rectangle.Width - num + Rectangle.X, Rectangle.Height - num + Rectangle.Y, num, num);
                arg_A1_0.AddArc(rect, 0f, 90f);
                GraphicsPath arg_D5_0 = graphicsPath;
                rect = new Rectangle(Rectangle.X, Rectangle.Height - num + Rectangle.Y, num, num);
                arg_D5_0.AddArc(rect, 90f, 90f);
                GraphicsPath arg_118_0 = graphicsPath;
                Point point = new Point(Rectangle.X, Rectangle.Height - num + Rectangle.Y);
                Point arg_118_1 = point;
                Point pt = new Point(Rectangle.X, Curve + Rectangle.Y);
                arg_118_0.AddLine(arg_118_1, pt);
                return graphicsPath;
            }
        }

        public static GraphicsPath RoundRect(float x, float y, float w, float h, float r = 0.3f, bool TL = true, bool TR = true, bool BR = true, bool BL = true)
        {
            float num = Math.Min(w, h) * r;
            float num2 = x + w;
            float num3 = y + h;
            GraphicsPath graphicsPath = new GraphicsPath();
            GraphicsPath graphicsPath2 = graphicsPath;
            if (TL)
            {
                graphicsPath2.AddArc(x, y, num, num, 180f, 90f);
            }
            else
            {
                graphicsPath2.AddLine(x, y, x, y);
            }
            if (TR)
            {
                graphicsPath2.AddArc(num2 - num, y, num, num, 270f, 90f);
            }
            else
            {
                graphicsPath2.AddLine(num2, y, num2, y);
            }
            if (BR)
            {
                graphicsPath2.AddArc(num2 - num, num3 - num, num, num, 0f, 90f);
            }
            else
            {
                graphicsPath2.AddLine(num2, num3, num2, num3);
            }
            if (BL)
            {
                graphicsPath2.AddArc(x, num3 - num, num, num, 90f, 90f);
            }
            else
            {
                graphicsPath2.AddLine(x, num3, x, num3);
            }
            graphicsPath2.CloseFigure();
            return graphicsPath;
        }

        public static GraphicsPath DrawArrow(int x, int y, bool flip)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            int num = 12;
            int num2 = 6;
            checked
            {
                if (flip)
                {
                    graphicsPath.AddLine(x + 1, y, x + num + 1, y);
                    graphicsPath.AddLine(x + num, y, x + num2, y + num2 - 1);
                }
                else
                {
                    graphicsPath.AddLine(x, y + num2, x + num, y + num2);
                    graphicsPath.AddLine(x + num, y + num2, x + num2, y);
                }
                graphicsPath.CloseFigure();
                return graphicsPath;
            }
        }
    }
}

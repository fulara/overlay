using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace overlay
{
    class HudControl
    {
        public struct Drawer : IDisposable
        {
            private static Font font = new Font("Arial", 36);
            private SizeF templateSize;

            internal Drawer(Bitmap image, PictureBox target)
            {
                image_ = image;
                target_ = target;

                g = Graphics.FromImage(image_);
                g.Clear(Color.White);

                templateSize = g.MeasureString("100", font);
            }

            PointF Position(PointF offset)
            {
                var center = new PointF((image_.Width - templateSize.Width) / 2, (image_.Height - templateSize.Width) / 2);
                return new PointF(center.X + templateSize.Width * offset.X, center.Y + templateSize.Height * offset.Y);
            }

            public void Draw(String text, Brush brush, PointF offset)
            {
                g.DrawString(text, font, brush, Position(offset));
            }

            public void Dispose()
            {
                target_.Invalidate();
            }

            private PictureBox target_;
            Bitmap image_;
            Graphics g;
        }

        private PictureBox target_;
        private Bitmap image_;

        public HudControl(PictureBox target)
        {
            target_ = target;
            image_ = new Bitmap(target_.Width, target_.Height);

            Graphics g = Graphics.FromImage(image_);
        }

        public Drawer StartDrawing()
        {
            target_.BackgroundImage = image_;
            return new Drawer(image_, target_);
        }
    }
}

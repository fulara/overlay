using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static overlay.HudControl;

namespace overlay
{
    public partial class Form1 : Form
    {
        Keyboard kb;
        HudControl control;
        Stopwatch stopwatchPots = Stopwatch.StartNew();
        Stopwatch stopwatchAdrenaline = Stopwatch.StartNew();
        Timer t = new Timer();

        bool enabled = false;

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();

            kb = new Keyboard(this);
            control = new HudControl(mainPictureBox);


            t.Tick += TimerTick;
            t.Interval = 400;
            t.Start();

            kb.KeyDown += (sender, arg) => {
                if(arg.Key == Keys.X && kb.IsPressed(Keys.LMenu))
                {
                    enabled = !enabled;
                }
                if(!enabled)
                {
                    return;
                }
                if (arg.Key == Keys.A)
                {
                    kb.Send(Keys.D3);
                    kb.Send(Keys.D4);
                    kb.Send(Keys.D5);

                    stopwatchPots.Restart();
                }

                if(arg.Key == Keys.W)
                {
                    stopwatchAdrenaline.Restart();
                }
                
                if(arg.Key == Keys.Space)
                {
                    kb.SendDown(Keys.LShiftKey);
                }

                if(arg.Key == Keys.B)
                {
                    if(FormBorderStyle == FormBorderStyle.None)
                    {
                        FormBorderStyle = FormBorderStyle.Fixed3D;
                    } else
                    {
                        FormBorderStyle = FormBorderStyle.None;
                    }
                    
                }
            };

        }

        private void TimerTick(object sender, EventArgs e)
        {
            using (var drawer = control.StartDrawing())
            {
                if(!enabled)
                {
                    drawer.Draw("disabled", Brushes.Black, new PointF(0, 0));
                    return;
                }

                DrawPots(drawer);
                DrawAdrenaline(drawer);
            }
        }

        void DrawPots(Drawer drawer)
        {
            var elapsed = Math.Min(stopwatchPots.Elapsed.TotalSeconds, 60);

            var color = Brushes.Black;
            if(elapsed > 5.5)
            {
                color = Brushes.Red;
            }

            var text = String.Format("{0:0.0}", elapsed);

            drawer.Draw(text, color, new PointF(-0.5f, 0));
        }

        void DrawAdrenaline(Drawer drawer)
        {
            var elapsed = Math.Min(stopwatchAdrenaline.Elapsed.TotalSeconds, 60);

            var color = Brushes.Black;
            if (elapsed > 20)
            {
                color = Brushes.Red;
            }

            var text = String.Format("{0:0.0}", elapsed);

            drawer.Draw(text, color, new PointF(0.5f, 0));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TopMost = true;
            this.TransparencyKey = Color.White;
            this.BackColor = Color.White;
        }
    }
}

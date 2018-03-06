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

namespace overlay
{
    public partial class Form1 : Form
    {
        Keyboard kb;
        Stopwatch stopwatch = Stopwatch.StartNew();
        Timer t = new Timer();

        bool enabled = false;

        public Form1()
        {
            InitializeComponent();

            kb = new Keyboard(this);
            t.Tick += TimerTick;
            t.Interval = 100;
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

                    stopwatch.Restart();
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
            var secs = Math.Min((int)stopwatch.Elapsed.TotalSeconds, 60);

            if(enabled)
            {
                label1.ForeColor = Color.Red;
            } else
            {
                label1.ForeColor = Color.Black;
            }
            label1.Text = secs.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TopMost = true;
            this.TransparencyKey = Color.White;
            this.BackColor = Color.White;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Eryan
{
    public class Utils : Form
    {
        //Plexiglass
        private delegate void setPlexiglass(Form form);
        private delegate void closeFormDelegate();
        private delegate void bringToFrontDelegate();
        private delegate void setOwnerDelegate(Form form);
        private delegate void hideFormDelegate();
        private delegate void sizeDelegate(Size sz);
        private delegate void locationDelegate(Point loc);
        private delegate void colorDelegate(Color color);
        private delegate void opacityDelegate(double opacity);
        private delegate void booleanDelegate(bool show);
        private delegate void FormStartPositionDelegate(FormStartPosition pos);
        private delegate void AutoScaleModeDelegate(AutoScaleMode mode);
        private delegate void FormBorderStyleDelegate(FormBorderStyle style);


        //public BackgroundWorker bw = new BackgroundWorker();



        //private Form m_form = new Form();

        public Utils()
        {
           int hello = 10;  
        }

        public void drawString(String str, Font f, Point loc, bool antialiasing)
        {
            Graphics g = this.CreateGraphics();
            if(antialiasing)
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(str, f, Brushes.Green, loc);
        }

        public void drawString(String str, Font f, Point loc)
        {
            Graphics g = this.CreateGraphics();
            g.DrawString(str, f, Brushes.Green, loc);
        }

        public void drawLine(Pen pen, Point point1, Point point2)
        {
            Graphics g = this.CreateGraphics();
            g.DrawLine(pen, point1, point2);
        }
            

        /*
        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
        }
         */

        public Boolean IsVisible()
        {
            foreach (Screen scrn in Screen.AllScreens)
                // You may prefer Intersects(), rather than Contains() <br/>
                if (scrn.Bounds.IntersectsWith(this.Bounds))
                    return true;

            return false;
        }


        public void setOwner(Form form)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new setOwnerDelegate(setOwner), new object[] { form });
                return;
            }

           
            this.Owner = form;
        }

        public void closeForm()
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new closeFormDelegate(closeForm), new object[]{});
                return ;
            }

    

            this.Close();
        }

        public void hideForm()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new hideFormDelegate(hideForm), new object[] { });
                return;
            }

       

            this.Hide();
        }

        public void setSize(Size sz)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new sizeDelegate(setSize), new object[] {sz});
                return;
            }

            
            this.Size = sz;
        }

        public void setTransparencyKey()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new hideFormDelegate(setTransparencyKey), new object[] { });
                return;
            }


            this.TransparencyKey = this.BackColor;
        }


        public void bwSetLocation(object sender, DoWorkEventArgs e)
        {
            System.Console.WriteLine("BW set location" + e.Argument);
            this.Location = (Point)e.Argument;
        }


        public void setLocation(Point loc)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new locationDelegate(setLocation), new object[] { loc });
                return;
            }

            this.Location = loc;
        }

        public void setBackColor(Color color)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new colorDelegate(setBackColor), new object[] { color });
                return;
            }

            System.Console.WriteLine("COlor = :" + color);
            this.BackColor = color;
        }

        public void setOpacity(double opacity)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new opacityDelegate(setOpacity), new object[] { opacity });
                return;
            }


            System.Console.WriteLine("Opacity = :" + opacity);
            this.Opacity = opacity;
        }

        public void setControlBox(bool show)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new booleanDelegate(setControlBox), new object[] { show });
                return;
            }

            System.Console.WriteLine("ControlBox = :" + ControlBox);
            this.ControlBox = show;
        }
        
        public void showInTaskbar(bool show)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new booleanDelegate(setControlBox), new object[] { show });
                return;
            }


            this.ShowInTaskbar = show;
        }

        public void setStartPosition(FormStartPosition position)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new FormStartPositionDelegate(setStartPosition), new object[] { position });
                return;
            }

            this.StartPosition = position;
        }
        
        public void setAutoScaleMode(AutoScaleMode mode)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new AutoScaleModeDelegate(setAutoScaleMode), new object[] { mode });
                return;
            }

   

            this.AutoScaleMode = mode;
        }

        public void setFormBorderStyle(FormBorderStyle style)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new FormBorderStyleDelegate(setFormBorderStyle), new object[] { style });
                return;
            }

  

            this.FormBorderStyle = style;
        }


        public void bringToFront()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new bringToFrontDelegate(bringToFront), new object[] { });
                return;
            }

            this.BringToFront();
        }

        public void showForm()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new bringToFrontDelegate(showForm), new object[] { });
                return;
            }

           
            this.Show();

        }


        public void updatePlexiglass(Form form)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new setPlexiglass(updatePlexiglass), new object[] { form });
                return;
            }

            if (this == null)
                return;

            this.BackColor = Color.DarkGray;
            this.Opacity = 0.30;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScaleMode = AutoScaleMode.None;
            System.Console.WriteLine(form.Size.ToString());
        //    this.Location = form.Location;
        //    this.Size = form.Size;
        //    this.Show();
        //    this.BringToFront();

            //form.safeShow(frm);

            

        }



        public Form Plexiglass(Form tocover)
        {
            var frm = new Form();
            frm.BackColor = Color.DarkGray;
            frm.Opacity = 0.30;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.ControlBox = false;
            frm.ShowInTaskbar = false;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.AutoScaleMode = AutoScaleMode.None;
            System.Console.WriteLine(tocover.Size.ToString());
            frm.Location = tocover.Location;
            frm.Size = tocover.Size;
            //frm.Show(tocover);
            frm.Show();
            return frm;
        }
    }

}

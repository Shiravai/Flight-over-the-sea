using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenGL;

namespace myOpenGL
{
    public partial class Form1 : Form
    {
        cOGL cGL;

        public Form1()
        {
            InitializeComponent();
            cGL = new cOGL(panel1);

            // read the starting values of the camera / sun bars into the scene
            hScrollBarScroll(hScrollBar1, null);
            hScrollBarScroll(hScrollBar2, null);
            hScrollBarScroll(hScrollBar3, null);
            hScrollBarScroll(hScrollBar4, null);
            hScrollBarScroll(hScrollBar5, null);
            hScrollBarScroll(hScrollBar6, null);
            hScrollBarScroll(hScrollBar7, null);
            hScrollBarScroll(hScrollBar8, null);
            hScrollBarScroll(hScrollBar9, null);
            hScrollBarScroll(hScrollBar11, null);
            hScrollBarScroll(hScrollBar12, null);
            hScrollBarScroll(hScrollBar13, null);

            cGL.flightSpeed = trackBarSpeed.Value / 10.0f;
            ApplyZoom();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            cGL.Draw();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            cGL.OnResize();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cGL.AdvanceAnimation();
        }

        // ---- camera + sun scrollbars (9 for gluLookAt, 3 for the light) ----
        private void hScrollBarScroll(object sender, ScrollEventArgs e)
        {
            cGL.intOptionC = 0;
            HScrollBar hb = (HScrollBar)sender;
            int n = int.Parse(hb.Name.Substring(10));
            cGL.ScrollValue[n - 1] = (hb.Value - 100) / 10.0f;
            if (e != null)
                cGL.Draw();
        }

        // ---- projection ----
        private void radioProjection_CheckedChanged(object sender, EventArgs e)
        {
            cGL.isPerspective = radioPersp.Checked;
            cGL.SetProjection();
            cGL.Draw();
        }

        private void ApplyZoom()
        {
            int v = trackBarZoom.Value;
            cGL.fovy = v;              // field of view in perspective mode
            cGL.orthoScale = v / 6.0f; // half-height in orthographic mode
            cGL.SetProjection();
            cGL.Draw();
        }

        private void trackBarZoom_Scroll(object sender, EventArgs e)
        {
            ApplyZoom();
        }

        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            cGL.flightSpeed = trackBarSpeed.Value / 10.0f;
        }

        // ---- check boxes ----
        private void checkBoxAnim_CheckedChanged(object sender, EventArgs e)
        {
            cGL.animate = checkBoxAnim.Checked;
            timer1.Enabled = checkBoxAnim.Checked;
            cGL.Draw();
        }

        private void checkBoxToggle_CheckedChanged(object sender, EventArgs e)
        {
            cGL.showReflection = checkBoxReflect.Checked;
            cGL.showShadow = checkBoxShadow.Checked;
            cGL.showTextures = checkBoxTex.Checked;
            cGL.showAxes = checkBoxAxes.Checked;
            cGL.showSun = checkBoxSun.Checked;
            cGL.dayNight = checkBoxDayNight.Checked;
            cGL.showBeam = checkBoxBeam.Checked;
            cGL.showBoat = checkBoxBoat.Checked;
            cGL.showBirds = checkBoxBirds.Checked;
            cGL.Draw();
        }

        // ---- mouse : drag to rotate the whole scene ----
        bool dragging = false;
        int lastX, lastY;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            lastX = e.X;
            lastY = e.Y;
            panel1.Focus();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragging)
                return;
            int dx = e.X - lastX;
            int dy = e.Y - lastY;
            if (Math.Abs(dx) < 3 && Math.Abs(dy) < 3)
                return;

            if (Math.Abs(dx) > Math.Abs(dy))
                cGL.intOptionC = (dx > 0) ? 2 : -2;   // around the vertical axis
            else
                cGL.intOptionC = (dy > 0) ? 1 : -1;   // around the horizontal axis

            cGL.Draw();
            cGL.intOptionC = 0;
            lastX = e.X;
            lastY = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        // ---- keyboard ----
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: cGL.intOptionC = -3; cGL.Draw(); cGL.intOptionC = 0; break;
                case Keys.Right: cGL.intOptionC = 3; cGL.Draw(); cGL.intOptionC = 0; break;
                case Keys.Up: cGL.intOptionC = 1; cGL.Draw(); cGL.intOptionC = 0; break;
                case Keys.Down: cGL.intOptionC = -1; cGL.Draw(); cGL.intOptionC = 0; break;
                case Keys.Oemplus:
                case Keys.Add:
                    if (trackBarZoom.Value - 4 >= trackBarZoom.Minimum) trackBarZoom.Value -= 4;
                    ApplyZoom();
                    break;
                case Keys.OemMinus:
                case Keys.Subtract:
                    if (trackBarZoom.Value + 4 <= trackBarZoom.Maximum) trackBarZoom.Value += 4;
                    ApplyZoom();
                    break;
                case Keys.P:
                    radioOrtho.Checked = !radioOrtho.Checked;
                    break;
                case Keys.Space:
                    checkBoxAnim.Checked = !checkBoxAnim.Checked;
                    break;
            }
            e.Handled = true;
        }
    }
}

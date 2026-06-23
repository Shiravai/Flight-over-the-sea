namespace myOpenGL
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxCam = new System.Windows.Forms.GroupBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar3 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar4 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar5 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar6 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar7 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar8 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar9 = new System.Windows.Forms.HScrollBar();
            this.lblc1 = new System.Windows.Forms.Label();
            this.lblc2 = new System.Windows.Forms.Label();
            this.lblc3 = new System.Windows.Forms.Label();
            this.lblc4 = new System.Windows.Forms.Label();
            this.lblc5 = new System.Windows.Forms.Label();
            this.lblc6 = new System.Windows.Forms.Label();
            this.lblc7 = new System.Windows.Forms.Label();
            this.lblc8 = new System.Windows.Forms.Label();
            this.lblc9 = new System.Windows.Forms.Label();
            this.groupBoxSun = new System.Windows.Forms.GroupBox();
            this.hScrollBar11 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar12 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar13 = new System.Windows.Forms.HScrollBar();
            this.lbls1 = new System.Windows.Forms.Label();
            this.lbls2 = new System.Windows.Forms.Label();
            this.lbls3 = new System.Windows.Forms.Label();
            this.groupBoxProj = new System.Windows.Forms.GroupBox();
            this.radioPersp = new System.Windows.Forms.RadioButton();
            this.radioOrtho = new System.Windows.Forms.RadioButton();
            this.lblZoom = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.trackBarZoom = new System.Windows.Forms.TrackBar();
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.groupBoxShow = new System.Windows.Forms.GroupBox();
            this.checkBoxAnim = new System.Windows.Forms.CheckBox();
            this.checkBoxReflect = new System.Windows.Forms.CheckBox();
            this.checkBoxShadow = new System.Windows.Forms.CheckBox();
            this.checkBoxTex = new System.Windows.Forms.CheckBox();
            this.checkBoxAxes = new System.Windows.Forms.CheckBox();
            this.checkBoxSun = new System.Windows.Forms.CheckBox();
            this.lblHelp = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBoxCam.SuspendLayout();
            this.groupBoxSun.SuspendLayout();
            this.groupBoxProj.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            this.groupBoxShow.SuspendLayout();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 628);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            //
            // groupBoxCam
            //
            this.groupBoxCam.Controls.Add(this.lblc1);
            this.groupBoxCam.Controls.Add(this.lblc2);
            this.groupBoxCam.Controls.Add(this.lblc3);
            this.groupBoxCam.Controls.Add(this.lblc4);
            this.groupBoxCam.Controls.Add(this.lblc5);
            this.groupBoxCam.Controls.Add(this.lblc6);
            this.groupBoxCam.Controls.Add(this.lblc7);
            this.groupBoxCam.Controls.Add(this.lblc8);
            this.groupBoxCam.Controls.Add(this.lblc9);
            this.groupBoxCam.Controls.Add(this.hScrollBar1);
            this.groupBoxCam.Controls.Add(this.hScrollBar2);
            this.groupBoxCam.Controls.Add(this.hScrollBar3);
            this.groupBoxCam.Controls.Add(this.hScrollBar4);
            this.groupBoxCam.Controls.Add(this.hScrollBar5);
            this.groupBoxCam.Controls.Add(this.hScrollBar6);
            this.groupBoxCam.Controls.Add(this.hScrollBar7);
            this.groupBoxCam.Controls.Add(this.hScrollBar8);
            this.groupBoxCam.Controls.Add(this.hScrollBar9);
            this.groupBoxCam.Location = new System.Drawing.Point(580, 12);
            this.groupBoxCam.Name = "groupBoxCam";
            this.groupBoxCam.Size = new System.Drawing.Size(350, 250);
            this.groupBoxCam.TabIndex = 1;
            this.groupBoxCam.TabStop = false;
            this.groupBoxCam.Text = "Camera  (eye / center / up)";
            //
            // hScrollBar1
            //
            this.hScrollBar1.Location = new System.Drawing.Point(95, 22);
            this.hScrollBar1.Maximum = 200;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Value = 100;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // hScrollBar2
            //
            this.hScrollBar2.Location = new System.Drawing.Point(95, 46);
            this.hScrollBar2.Maximum = 200;
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar2.TabIndex = 1;
            this.hScrollBar2.Value = 30;
            this.hScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // hScrollBar3
            //
            this.hScrollBar3.Location = new System.Drawing.Point(95, 70);
            this.hScrollBar3.Maximum = 200;
            this.hScrollBar3.Name = "hScrollBar3";
            this.hScrollBar3.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar3.TabIndex = 2;
            this.hScrollBar3.Value = 145;
            this.hScrollBar3.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // hScrollBar4
            //
            this.hScrollBar4.Location = new System.Drawing.Point(95, 100);
            this.hScrollBar4.Maximum = 200;
            this.hScrollBar4.Name = "hScrollBar4";
            this.hScrollBar4.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar4.TabIndex = 3;
            this.hScrollBar4.Value = 100;
            this.hScrollBar4.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // hScrollBar5
            //
            this.hScrollBar5.Location = new System.Drawing.Point(95, 124);
            this.hScrollBar5.Maximum = 200;
            this.hScrollBar5.Name = "hScrollBar5";
            this.hScrollBar5.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar5.TabIndex = 4;
            this.hScrollBar5.Value = 100;
            this.hScrollBar5.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // hScrollBar6
            //
            this.hScrollBar6.Location = new System.Drawing.Point(95, 148);
            this.hScrollBar6.Maximum = 200;
            this.hScrollBar6.Name = "hScrollBar6";
            this.hScrollBar6.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar6.TabIndex = 5;
            this.hScrollBar6.Value = 110;
            this.hScrollBar6.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // hScrollBar7
            //
            this.hScrollBar7.Location = new System.Drawing.Point(95, 178);
            this.hScrollBar7.Maximum = 200;
            this.hScrollBar7.Name = "hScrollBar7";
            this.hScrollBar7.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar7.TabIndex = 6;
            this.hScrollBar7.Value = 100;
            this.hScrollBar7.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // hScrollBar8
            //
            this.hScrollBar8.Location = new System.Drawing.Point(95, 202);
            this.hScrollBar8.Maximum = 200;
            this.hScrollBar8.Name = "hScrollBar8";
            this.hScrollBar8.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar8.TabIndex = 7;
            this.hScrollBar8.Value = 100;
            this.hScrollBar8.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // hScrollBar9
            //
            this.hScrollBar9.Location = new System.Drawing.Point(95, 226);
            this.hScrollBar9.Maximum = 200;
            this.hScrollBar9.Name = "hScrollBar9";
            this.hScrollBar9.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar9.TabIndex = 8;
            this.hScrollBar9.Value = 110;
            this.hScrollBar9.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // lblc1
            //
            this.lblc1.AutoSize = true;
            this.lblc1.Location = new System.Drawing.Point(10, 24);
            this.lblc1.Name = "lblc1";
            this.lblc1.Size = new System.Drawing.Size(33, 13);
            this.lblc1.TabIndex = 0;
            this.lblc1.Text = "eye x";
            //
            // lblc2
            //
            this.lblc2.AutoSize = true;
            this.lblc2.Location = new System.Drawing.Point(10, 48);
            this.lblc2.Name = "lblc2";
            this.lblc2.Size = new System.Drawing.Size(33, 13);
            this.lblc2.TabIndex = 0;
            this.lblc2.Text = "eye y";
            //
            // lblc3
            //
            this.lblc3.AutoSize = true;
            this.lblc3.Location = new System.Drawing.Point(10, 72);
            this.lblc3.Name = "lblc3";
            this.lblc3.Size = new System.Drawing.Size(33, 13);
            this.lblc3.TabIndex = 0;
            this.lblc3.Text = "eye z";
            //
            // lblc4
            //
            this.lblc4.AutoSize = true;
            this.lblc4.Location = new System.Drawing.Point(10, 102);
            this.lblc4.Name = "lblc4";
            this.lblc4.Size = new System.Drawing.Size(48, 13);
            this.lblc4.TabIndex = 0;
            this.lblc4.Text = "center x";
            //
            // lblc5
            //
            this.lblc5.AutoSize = true;
            this.lblc5.Location = new System.Drawing.Point(10, 126);
            this.lblc5.Name = "lblc5";
            this.lblc5.Size = new System.Drawing.Size(48, 13);
            this.lblc5.TabIndex = 0;
            this.lblc5.Text = "center y";
            //
            // lblc6
            //
            this.lblc6.AutoSize = true;
            this.lblc6.Location = new System.Drawing.Point(10, 150);
            this.lblc6.Name = "lblc6";
            this.lblc6.Size = new System.Drawing.Size(48, 13);
            this.lblc6.TabIndex = 0;
            this.lblc6.Text = "center z";
            //
            // lblc7
            //
            this.lblc7.AutoSize = true;
            this.lblc7.Location = new System.Drawing.Point(10, 180);
            this.lblc7.Name = "lblc7";
            this.lblc7.Size = new System.Drawing.Size(30, 13);
            this.lblc7.TabIndex = 0;
            this.lblc7.Text = "up x";
            //
            // lblc8
            //
            this.lblc8.AutoSize = true;
            this.lblc8.Location = new System.Drawing.Point(10, 204);
            this.lblc8.Name = "lblc8";
            this.lblc8.Size = new System.Drawing.Size(30, 13);
            this.lblc8.TabIndex = 0;
            this.lblc8.Text = "up y";
            //
            // lblc9
            //
            this.lblc9.AutoSize = true;
            this.lblc9.Location = new System.Drawing.Point(10, 228);
            this.lblc9.Name = "lblc9";
            this.lblc9.Size = new System.Drawing.Size(30, 13);
            this.lblc9.TabIndex = 0;
            this.lblc9.Text = "up z";
            //
            // groupBoxSun
            //
            this.groupBoxSun.Controls.Add(this.lbls1);
            this.groupBoxSun.Controls.Add(this.lbls2);
            this.groupBoxSun.Controls.Add(this.lbls3);
            this.groupBoxSun.Controls.Add(this.hScrollBar11);
            this.groupBoxSun.Controls.Add(this.hScrollBar12);
            this.groupBoxSun.Controls.Add(this.hScrollBar13);
            this.groupBoxSun.Location = new System.Drawing.Point(580, 268);
            this.groupBoxSun.Name = "groupBoxSun";
            this.groupBoxSun.Size = new System.Drawing.Size(350, 95);
            this.groupBoxSun.TabIndex = 2;
            this.groupBoxSun.TabStop = false;
            this.groupBoxSun.Text = "Sun position";
            //
            // hScrollBar11
            //
            this.hScrollBar11.Location = new System.Drawing.Point(95, 22);
            this.hScrollBar11.Maximum = 200;
            this.hScrollBar11.Name = "hScrollBar11";
            this.hScrollBar11.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar11.TabIndex = 0;
            this.hScrollBar11.Value = 130;
            this.hScrollBar11.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // hScrollBar12
            //
            this.hScrollBar12.Location = new System.Drawing.Point(95, 46);
            this.hScrollBar12.Maximum = 200;
            this.hScrollBar12.Name = "hScrollBar12";
            this.hScrollBar12.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar12.TabIndex = 1;
            this.hScrollBar12.Value = 70;
            this.hScrollBar12.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // hScrollBar13
            //
            this.hScrollBar13.Location = new System.Drawing.Point(95, 70);
            this.hScrollBar13.Maximum = 200;
            this.hScrollBar13.Name = "hScrollBar13";
            this.hScrollBar13.Size = new System.Drawing.Size(245, 17);
            this.hScrollBar13.TabIndex = 2;
            this.hScrollBar13.Value = 190;
            this.hScrollBar13.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarScroll);
            //
            // lbls1
            //
            this.lbls1.AutoSize = true;
            this.lbls1.Location = new System.Drawing.Point(10, 24);
            this.lbls1.Name = "lbls1";
            this.lbls1.Size = new System.Drawing.Size(14, 13);
            this.lbls1.TabIndex = 0;
            this.lbls1.Text = "x";
            //
            // lbls2
            //
            this.lbls2.AutoSize = true;
            this.lbls2.Location = new System.Drawing.Point(10, 48);
            this.lbls2.Name = "lbls2";
            this.lbls2.Size = new System.Drawing.Size(14, 13);
            this.lbls2.TabIndex = 0;
            this.lbls2.Text = "y";
            //
            // lbls3
            //
            this.lbls3.AutoSize = true;
            this.lbls3.Location = new System.Drawing.Point(10, 72);
            this.lbls3.Name = "lbls3";
            this.lbls3.Size = new System.Drawing.Size(14, 13);
            this.lbls3.TabIndex = 0;
            this.lbls3.Text = "z";
            //
            // groupBoxProj
            //
            this.groupBoxProj.Controls.Add(this.radioPersp);
            this.groupBoxProj.Controls.Add(this.radioOrtho);
            this.groupBoxProj.Controls.Add(this.lblZoom);
            this.groupBoxProj.Controls.Add(this.lblSpeed);
            this.groupBoxProj.Controls.Add(this.trackBarZoom);
            this.groupBoxProj.Controls.Add(this.trackBarSpeed);
            this.groupBoxProj.Location = new System.Drawing.Point(580, 369);
            this.groupBoxProj.Name = "groupBoxProj";
            this.groupBoxProj.Size = new System.Drawing.Size(350, 150);
            this.groupBoxProj.TabIndex = 3;
            this.groupBoxProj.TabStop = false;
            this.groupBoxProj.Text = "Projection";
            //
            // radioPersp
            //
            this.radioPersp.AutoSize = true;
            this.radioPersp.Checked = true;
            this.radioPersp.Location = new System.Drawing.Point(12, 22);
            this.radioPersp.Name = "radioPersp";
            this.radioPersp.Size = new System.Drawing.Size(85, 17);
            this.radioPersp.TabIndex = 0;
            this.radioPersp.TabStop = true;
            this.radioPersp.Text = "Perspective";
            this.radioPersp.UseVisualStyleBackColor = true;
            this.radioPersp.CheckedChanged += new System.EventHandler(this.radioProjection_CheckedChanged);
            //
            // radioOrtho
            //
            this.radioOrtho.AutoSize = true;
            this.radioOrtho.Location = new System.Drawing.Point(160, 22);
            this.radioOrtho.Name = "radioOrtho";
            this.radioOrtho.Size = new System.Drawing.Size(94, 17);
            this.radioOrtho.TabIndex = 1;
            this.radioOrtho.Text = "Orthographic";
            this.radioOrtho.UseVisualStyleBackColor = true;
            this.radioOrtho.CheckedChanged += new System.EventHandler(this.radioProjection_CheckedChanged);
            //
            // lblZoom
            //
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(12, 58);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(34, 13);
            this.lblZoom.TabIndex = 0;
            this.lblZoom.Text = "zoom";
            //
            // lblSpeed
            //
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(12, 105);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(38, 13);
            this.lblSpeed.TabIndex = 0;
            this.lblSpeed.Text = "speed";
            //
            // trackBarZoom
            //
            this.trackBarZoom.Location = new System.Drawing.Point(60, 52);
            this.trackBarZoom.Maximum = 80;
            this.trackBarZoom.Minimum = 10;
            this.trackBarZoom.Name = "trackBarZoom";
            this.trackBarZoom.Size = new System.Drawing.Size(280, 45);
            this.trackBarZoom.TabIndex = 2;
            this.trackBarZoom.TickFrequency = 5;
            this.trackBarZoom.Value = 45;
            this.trackBarZoom.Scroll += new System.EventHandler(this.trackBarZoom_Scroll);
            //
            // trackBarSpeed
            //
            this.trackBarSpeed.Location = new System.Drawing.Point(60, 99);
            this.trackBarSpeed.Maximum = 30;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(280, 45);
            this.trackBarSpeed.TabIndex = 3;
            this.trackBarSpeed.Value = 10;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBarSpeed_Scroll);
            //
            // groupBoxShow
            //
            this.groupBoxShow.Controls.Add(this.checkBoxAnim);
            this.groupBoxShow.Controls.Add(this.checkBoxReflect);
            this.groupBoxShow.Controls.Add(this.checkBoxShadow);
            this.groupBoxShow.Controls.Add(this.checkBoxTex);
            this.groupBoxShow.Controls.Add(this.checkBoxAxes);
            this.groupBoxShow.Controls.Add(this.checkBoxSun);
            this.groupBoxShow.Location = new System.Drawing.Point(580, 525);
            this.groupBoxShow.Name = "groupBoxShow";
            this.groupBoxShow.Size = new System.Drawing.Size(350, 90);
            this.groupBoxShow.TabIndex = 4;
            this.groupBoxShow.TabStop = false;
            this.groupBoxShow.Text = "Show";
            //
            // checkBoxAnim
            //
            this.checkBoxAnim.AutoSize = true;
            this.checkBoxAnim.Checked = true;
            this.checkBoxAnim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnim.Location = new System.Drawing.Point(12, 22);
            this.checkBoxAnim.Name = "checkBoxAnim";
            this.checkBoxAnim.Size = new System.Drawing.Size(74, 17);
            this.checkBoxAnim.TabIndex = 0;
            this.checkBoxAnim.Text = "Animation";
            this.checkBoxAnim.UseVisualStyleBackColor = true;
            this.checkBoxAnim.CheckedChanged += new System.EventHandler(this.checkBoxAnim_CheckedChanged);
            //
            // checkBoxReflect
            //
            this.checkBoxReflect.AutoSize = true;
            this.checkBoxReflect.Checked = true;
            this.checkBoxReflect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReflect.Location = new System.Drawing.Point(130, 22);
            this.checkBoxReflect.Name = "checkBoxReflect";
            this.checkBoxReflect.Size = new System.Drawing.Size(76, 17);
            this.checkBoxReflect.TabIndex = 1;
            this.checkBoxReflect.Text = "Reflection";
            this.checkBoxReflect.UseVisualStyleBackColor = true;
            this.checkBoxReflect.CheckedChanged += new System.EventHandler(this.checkBoxToggle_CheckedChanged);
            //
            // checkBoxShadow
            //
            this.checkBoxShadow.AutoSize = true;
            this.checkBoxShadow.Checked = true;
            this.checkBoxShadow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShadow.Location = new System.Drawing.Point(245, 22);
            this.checkBoxShadow.Name = "checkBoxShadow";
            this.checkBoxShadow.Size = new System.Drawing.Size(66, 17);
            this.checkBoxShadow.TabIndex = 2;
            this.checkBoxShadow.Text = "Shadow";
            this.checkBoxShadow.UseVisualStyleBackColor = true;
            this.checkBoxShadow.CheckedChanged += new System.EventHandler(this.checkBoxToggle_CheckedChanged);
            //
            // checkBoxTex
            //
            this.checkBoxTex.AutoSize = true;
            this.checkBoxTex.Checked = true;
            this.checkBoxTex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTex.Location = new System.Drawing.Point(12, 50);
            this.checkBoxTex.Name = "checkBoxTex";
            this.checkBoxTex.Size = new System.Drawing.Size(71, 17);
            this.checkBoxTex.TabIndex = 3;
            this.checkBoxTex.Text = "Textures";
            this.checkBoxTex.UseVisualStyleBackColor = true;
            this.checkBoxTex.CheckedChanged += new System.EventHandler(this.checkBoxToggle_CheckedChanged);
            //
            // checkBoxAxes
            //
            this.checkBoxAxes.AutoSize = true;
            this.checkBoxAxes.Location = new System.Drawing.Point(130, 50);
            this.checkBoxAxes.Name = "checkBoxAxes";
            this.checkBoxAxes.Size = new System.Drawing.Size(49, 17);
            this.checkBoxAxes.TabIndex = 4;
            this.checkBoxAxes.Text = "Axes";
            this.checkBoxAxes.UseVisualStyleBackColor = true;
            this.checkBoxAxes.CheckedChanged += new System.EventHandler(this.checkBoxToggle_CheckedChanged);
            //
            // checkBoxSun
            //
            this.checkBoxSun.AutoSize = true;
            this.checkBoxSun.Checked = true;
            this.checkBoxSun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSun.Location = new System.Drawing.Point(245, 50);
            this.checkBoxSun.Name = "checkBoxSun";
            this.checkBoxSun.Size = new System.Drawing.Size(44, 17);
            this.checkBoxSun.TabIndex = 5;
            this.checkBoxSun.Text = "Sun";
            this.checkBoxSun.UseVisualStyleBackColor = true;
            this.checkBoxSun.CheckedChanged += new System.EventHandler(this.checkBoxToggle_CheckedChanged);
            //
            // lblHelp
            //
            this.lblHelp.AutoSize = true;
            this.lblHelp.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblHelp.Location = new System.Drawing.Point(580, 622);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(326, 13);
            this.lblHelp.TabIndex = 5;
            this.lblHelp.Text = "drag with the mouse to rotate  -  arrows turn  -  +/- zoom  -  space";
            //
            // timer1
            //
            this.timer1.Enabled = true;
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 650);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.groupBoxShow);
            this.Controls.Add(this.groupBoxProj);
            this.Controls.Add(this.groupBoxSun);
            this.Controls.Add(this.groupBoxCam);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Flight over the sea";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.groupBoxCam.ResumeLayout(false);
            this.groupBoxCam.PerformLayout();
            this.groupBoxSun.ResumeLayout(false);
            this.groupBoxSun.PerformLayout();
            this.groupBoxProj.ResumeLayout(false);
            this.groupBoxProj.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            this.groupBoxShow.ResumeLayout(false);
            this.groupBoxShow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxCam;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.HScrollBar hScrollBar3;
        private System.Windows.Forms.HScrollBar hScrollBar4;
        private System.Windows.Forms.HScrollBar hScrollBar5;
        private System.Windows.Forms.HScrollBar hScrollBar6;
        private System.Windows.Forms.HScrollBar hScrollBar7;
        private System.Windows.Forms.HScrollBar hScrollBar8;
        private System.Windows.Forms.HScrollBar hScrollBar9;
        private System.Windows.Forms.Label lblc1;
        private System.Windows.Forms.Label lblc2;
        private System.Windows.Forms.Label lblc3;
        private System.Windows.Forms.Label lblc4;
        private System.Windows.Forms.Label lblc5;
        private System.Windows.Forms.Label lblc6;
        private System.Windows.Forms.Label lblc7;
        private System.Windows.Forms.Label lblc8;
        private System.Windows.Forms.Label lblc9;
        private System.Windows.Forms.GroupBox groupBoxSun;
        private System.Windows.Forms.HScrollBar hScrollBar11;
        private System.Windows.Forms.HScrollBar hScrollBar12;
        private System.Windows.Forms.HScrollBar hScrollBar13;
        private System.Windows.Forms.Label lbls1;
        private System.Windows.Forms.Label lbls2;
        private System.Windows.Forms.Label lbls3;
        private System.Windows.Forms.GroupBox groupBoxProj;
        private System.Windows.Forms.RadioButton radioPersp;
        private System.Windows.Forms.RadioButton radioOrtho;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.TrackBar trackBarZoom;
        private System.Windows.Forms.TrackBar trackBarSpeed;
        private System.Windows.Forms.GroupBox groupBoxShow;
        private System.Windows.Forms.CheckBox checkBoxAnim;
        private System.Windows.Forms.CheckBox checkBoxReflect;
        private System.Windows.Forms.CheckBox checkBoxShadow;
        private System.Windows.Forms.CheckBox checkBoxTex;
        private System.Windows.Forms.CheckBox checkBoxAxes;
        private System.Windows.Forms.CheckBox checkBoxSun;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Timer timer1;
    }
}

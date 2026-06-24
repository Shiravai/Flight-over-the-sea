using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace OpenGL
{
    // Final project - a small propeller plane flying over the sea.
    // The propeller keeps spinning while the plane circles around the island,
    // the plane is reflected in the water and drops a shadow on it.
    // A day/night cycle, a turning lighthouse beam, a sailing boat and a flock
    // of birds bring the scene to life.
    class cOGL
    {
        Control p;
        int Width;
        int Height;

        GLUquadric obj;

        public cOGL(Control pb)
        {
            p = pb;
            Width = p.Width;
            Height = p.Height;

            // some defaults so the scene is already visible at start-up
            ScrollValue[0] = 0;   ScrollValue[1] = -7;  ScrollValue[2] = 4.5f; // eye
            ScrollValue[3] = 0;   ScrollValue[4] = 0;   ScrollValue[5] = 1.0f;  // center
            ScrollValue[6] = 0;   ScrollValue[7] = 0;   ScrollValue[8] = 1.0f;  // up
            ScrollValue[10] = 3;  ScrollValue[11] = -3; ScrollValue[12] = 9;    // light/sun

            InitializeGL();
            obj = GLU.gluNewQuadric();
            GLU.gluQuadricNormals(obj, GLU.GLU_SMOOTH);
            GenerateTextures();
            MakeStars();
        }

        ~cOGL()
        {
            GLU.gluDeleteQuadric(obj);
            WGL.wglDeleteContext(m_uint_RC);
        }

        uint m_uint_HWND = 0;
        public uint HWND { get { return m_uint_HWND; } }
        uint m_uint_DC = 0;
        public uint DC { get { return m_uint_DC; } }
        uint m_uint_RC = 0;
        public uint RC { get { return m_uint_RC; } }

        //--------------------------------------------------------------------
        //  state controlled from the form
        //--------------------------------------------------------------------
        public float[] ScrollValue = new float[14];
        public float zShift = 0.0f, yShift = 0.0f, xShift = 0.0f;
        public float zAngle = 0.0f, yAngle = 0.0f, xAngle = 0.0f;
        public int intOptionC = 0;
        double[] AccumulatedRotationsTraslations = new double[16];

        public bool showTextures = true;
        public bool showReflection = true;
        public bool showShadow = true;
        public bool showAxes = false;
        public bool showSun = true;
        public bool animate = true;

        // extra scene features (the "upgrade")
        public bool dayNight = true;   // moving sun, changing sky, stars
        public bool showBeam = true;   // turning lighthouse beam
        public bool showBoat = true;   // sailing boat on the water
        public bool showBirds = true;  // flock crossing the sky
        public bool showKeeper = true; // keeper walking around the lighthouse

        public bool isPerspective = true;
        public float fovy = 45.0f;       // perspective zoom
        public float orthoScale = 7.0f;  // ortho zoom
        public float flightSpeed = 1.0f;

        float propAngle = 0.0f;          // propeller rotation
        float flightAngle = 30.0f;       // position along the circle

        float sunAngle = 60.0f;          // position along the day arc (degrees)
        float beamAngle = 0.0f;          // lighthouse beam rotation
        float boatPhase = 0.0f;          // boat rocking
        float boatTravel = 0.0f;         // boat sailing around the island
        float keeperAngle = 0.0f;        // keeper position around the lighthouse
        float keeperWalk = 0.0f;         // keeper leg / arm swing
        float birdCross = -13.0f;        // birds crossing the sky
        float wingFlap = 0.0f;
        float waterScroll = 0.0f;        // moving water texture
        float daylight = 1.0f;           // 0 = night .. 1 = noon

        float skyR = 0.53f, skyG = 0.70f, skyB = 0.92f;

        public float[] pos = new float[4];

        // a fixed dome of stars
        const int STAR_COUNT = 120;
        float[] starX = new float[STAR_COUNT];
        float[] starY = new float[STAR_COUNT];
        float[] starZ = new float[STAR_COUNT];

        static float Lerp(float a, float b, float t) { return a + (b - a) * t; }

        void MakeStars()
        {
            Random r = new Random(7);
            for (int i = 0; i < STAR_COUNT; i++)
            {
                double ang = r.NextDouble() * 2 * Math.PI;
                double rad = 16 + r.NextDouble() * 14;
                starX[i] = (float)(Math.Cos(ang) * rad);
                starY[i] = (float)(Math.Sin(ang) * rad);
                starZ[i] = (float)(4 + r.NextDouble() * 24);
            }
        }

        //--------------------------------------------------------------------
        //  textures
        //--------------------------------------------------------------------
        uint texWater, texSand, texMetal;
        bool texturesLoaded = false;

        void GenerateTextures()
        {
            try
            {
                uint[] ids = new uint[3];
                GL.glGenTextures(3, ids);
                texWater = ids[0]; texSand = ids[1]; texMetal = ids[2];
                LoadTexture("water.bmp", texWater);
                LoadTexture("sand.bmp", texSand);
                LoadTexture("metal.bmp", texMetal);
                texturesLoaded = true;
            }
            catch
            {
                // if the bmp files are missing we simply run without textures
                texturesLoaded = false;
            }
        }

        void LoadTexture(string fileName, uint id)
        {
            // look for the bmp next to the exe, so it is found no matter the working folder
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (!System.IO.File.Exists(path)) path = fileName;
            Bitmap image = new Bitmap(path);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY); // GL has Y upwards
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            System.Drawing.Imaging.BitmapData data =
                image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                               System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            GL.glBindTexture(GL.GL_TEXTURE_2D, id);
            GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
                            0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, data.Scan0);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, (int)GL.GL_REPEAT);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, (int)GL.GL_REPEAT);

            image.UnlockBits(data);
            image.Dispose();
        }

        void EnableTexture(uint id)
        {
            if (showTextures && texturesLoaded)
            {
                GL.glEnable(GL.GL_TEXTURE_2D);
                GL.glBindTexture(GL.GL_TEXTURE_2D, id);
                GLU.gluQuadricTexture(obj, (byte)1);
            }
        }

        void DisableTexture()
        {
            GL.glDisable(GL.GL_TEXTURE_2D);
            GLU.gluQuadricTexture(obj, (byte)0);
        }

        //--------------------------------------------------------------------
        //  shadow projection matrix (classic flatten-onto-a-plane trick)
        //--------------------------------------------------------------------
        float[,] ground = new float[3, 3];
        float[] cubeXform = new float[16];

        void ReduceToUnit(float[] vector)
        {
            float length = (float)Math.Sqrt(vector[0] * vector[0] +
                                            vector[1] * vector[1] +
                                            vector[2] * vector[2]);
            if (length == 0.0f) length = 1.0f;
            vector[0] /= length; vector[1] /= length; vector[2] /= length;
        }

        void calcNormal(float[,] v, float[] outp)
        {
            float[] v1 = new float[3];
            float[] v2 = new float[3];
            v1[0] = v[0, 0] - v[1, 0]; v1[1] = v[0, 1] - v[1, 1]; v1[2] = v[0, 2] - v[1, 2];
            v2[0] = v[1, 0] - v[2, 0]; v2[1] = v[1, 1] - v[2, 1]; v2[2] = v[1, 2] - v[2, 2];
            outp[0] = v1[1] * v2[2] - v1[2] * v2[1];
            outp[1] = v1[2] * v2[0] - v1[0] * v2[2];
            outp[2] = v1[0] * v2[1] - v1[1] * v2[0];
            ReduceToUnit(outp);
        }

        void MakeShadowMatrix(float[,] points)
        {
            float[] planeCoeff = new float[4];
            float dot;
            calcNormal(points, planeCoeff);
            planeCoeff[3] = -((planeCoeff[0] * points[2, 0]) +
                              (planeCoeff[1] * points[2, 1]) +
                              (planeCoeff[2] * points[2, 2]));

            dot = planeCoeff[0] * pos[0] + planeCoeff[1] * pos[1] +
                  planeCoeff[2] * pos[2] + planeCoeff[3];

            cubeXform[0] = dot - pos[0] * planeCoeff[0];
            cubeXform[4] = 0.0f - pos[0] * planeCoeff[1];
            cubeXform[8] = 0.0f - pos[0] * planeCoeff[2];
            cubeXform[12] = 0.0f - pos[0] * planeCoeff[3];

            cubeXform[1] = 0.0f - pos[1] * planeCoeff[0];
            cubeXform[5] = dot - pos[1] * planeCoeff[1];
            cubeXform[9] = 0.0f - pos[1] * planeCoeff[2];
            cubeXform[13] = 0.0f - pos[1] * planeCoeff[3];

            cubeXform[2] = 0.0f - pos[2] * planeCoeff[0];
            cubeXform[6] = 0.0f - pos[2] * planeCoeff[1];
            cubeXform[10] = dot - pos[2] * planeCoeff[2];
            cubeXform[14] = 0.0f - pos[2] * planeCoeff[3];

            cubeXform[3] = 0.0f - pos[3] * planeCoeff[0];
            cubeXform[7] = 0.0f - pos[3] * planeCoeff[1];
            cubeXform[11] = 0.0f - pos[3] * planeCoeff[2];
            cubeXform[15] = dot - pos[3] * planeCoeff[3];
        }

        //--------------------------------------------------------------------
        //  small geometry helpers
        //--------------------------------------------------------------------
        // axis aligned box, centred at (cx,cy,cz), half sizes hx,hy,hz, with normals
        void DrawBox(float cx, float cy, float cz, float hx, float hy, float hz)
        {
            float x0 = cx - hx, x1 = cx + hx;
            float y0 = cy - hy, y1 = cy + hy;
            float z0 = cz - hz, z1 = cz + hz;

            GL.glBegin(GL.GL_QUADS);
            GL.glNormal3f(0, 0, 1);
            GL.glTexCoord2f(0, 0); GL.glVertex3f(x0, y0, z1);
            GL.glTexCoord2f(1, 0); GL.glVertex3f(x1, y0, z1);
            GL.glTexCoord2f(1, 1); GL.glVertex3f(x1, y1, z1);
            GL.glTexCoord2f(0, 1); GL.glVertex3f(x0, y1, z1);

            GL.glNormal3f(0, 0, -1);
            GL.glTexCoord2f(0, 0); GL.glVertex3f(x0, y1, z0);
            GL.glTexCoord2f(1, 0); GL.glVertex3f(x1, y1, z0);
            GL.glTexCoord2f(1, 1); GL.glVertex3f(x1, y0, z0);
            GL.glTexCoord2f(0, 1); GL.glVertex3f(x0, y0, z0);

            GL.glNormal3f(1, 0, 0);
            GL.glTexCoord2f(0, 0); GL.glVertex3f(x1, y0, z0);
            GL.glTexCoord2f(1, 0); GL.glVertex3f(x1, y1, z0);
            GL.glTexCoord2f(1, 1); GL.glVertex3f(x1, y1, z1);
            GL.glTexCoord2f(0, 1); GL.glVertex3f(x1, y0, z1);

            GL.glNormal3f(-1, 0, 0);
            GL.glTexCoord2f(0, 0); GL.glVertex3f(x0, y1, z0);
            GL.glTexCoord2f(1, 0); GL.glVertex3f(x0, y0, z0);
            GL.glTexCoord2f(1, 1); GL.glVertex3f(x0, y0, z1);
            GL.glTexCoord2f(0, 1); GL.glVertex3f(x0, y1, z1);

            GL.glNormal3f(0, 1, 0);
            GL.glTexCoord2f(0, 0); GL.glVertex3f(x1, y1, z0);
            GL.glTexCoord2f(1, 0); GL.glVertex3f(x0, y1, z0);
            GL.glTexCoord2f(1, 1); GL.glVertex3f(x0, y1, z1);
            GL.glTexCoord2f(0, 1); GL.glVertex3f(x1, y1, z1);

            GL.glNormal3f(0, -1, 0);
            GL.glTexCoord2f(0, 0); GL.glVertex3f(x0, y0, z0);
            GL.glTexCoord2f(1, 0); GL.glVertex3f(x1, y0, z0);
            GL.glTexCoord2f(1, 1); GL.glVertex3f(x1, y0, z1);
            GL.glTexCoord2f(0, 1); GL.glVertex3f(x0, y0, z1);
            GL.glEnd();
        }

        //--------------------------------------------------------------------
        //  the airplane (hierarchical model). nose looks towards +X.
        //--------------------------------------------------------------------
        void DrawPlane(bool asShadow)
        {
            // fuselage
            if (!asShadow)
            {
                GL.glEnable(GL.GL_LIGHTING);
                GL.glColor3f(0.85f, 0.87f, 0.92f);
                EnableTexture(texMetal);
            }
            GL.glPushMatrix();
            GL.glTranslatef(-1.2f, 0, 0);
            GL.glRotatef(90, 0, 1, 0);           // aim the quadric (+Z) along +X
            GLU.gluCylinder(obj, 0.28, 0.20, 2.2, 24, 4);
            GLU.gluDisk(obj, 0, 0.28, 24, 2);    // tail cap
            GL.glPopMatrix();
            if (!asShadow) DisableTexture();

            // nose spinner
            GL.glPushMatrix();
            GL.glTranslatef(1.0f, 0, 0);
            GL.glRotatef(90, 0, 1, 0);
            if (!asShadow) GL.glColor3f(0.65f, 0.12f, 0.12f);
            GLU.gluCylinder(obj, 0.20, 0.0, 0.35, 24, 3);
            GL.glPopMatrix();

            // main wing
            if (!asShadow) GL.glColor3f(0.80f, 0.20f, 0.20f);
            DrawBox(0.0f, 0.0f, -0.06f, 0.45f, 1.6f, 0.05f);

            // horizontal stabilizer
            DrawBox(-1.05f, 0.0f, 0.04f, 0.30f, 0.65f, 0.04f);
            // vertical fin
            DrawBox(-1.05f, 0.0f, 0.30f, 0.30f, 0.05f, 0.32f);

            // cockpit glass
            if (!asShadow) GL.glColor3f(0.20f, 0.32f, 0.48f);
            GL.glPushMatrix();
            GL.glTranslatef(0.25f, 0, 0.22f);
            GL.glScalef(0.6f, 0.34f, 0.30f);
            GLU.gluSphere(obj, 0.5, 16, 16);
            GL.glPopMatrix();

            // propeller - this is the part that keeps spinning
            GL.glPushMatrix();
            GL.glTranslatef(1.07f, 0, 0);
            GL.glRotatef(propAngle, 1, 0, 0);
            if (!asShadow) GL.glColor3f(0.15f, 0.15f, 0.15f);
            GL.glPushMatrix();
            GL.glRotatef(90, 0, 1, 0);
            GLU.gluCylinder(obj, 0.06, 0.06, 0.13, 12, 2);
            GL.glPopMatrix();
            for (int i = 0; i < 3; i++)
            {
                GL.glPushMatrix();
                GL.glRotatef(i * 120, 1, 0, 0);
                if (!asShadow) GL.glColor3f(0.10f, 0.10f, 0.10f);
                DrawBox(0.02f, 0.0f, 0.34f, 0.025f, 0.06f, 0.34f);
                GL.glPopMatrix();
            }
            GL.glPopMatrix();
        }

        // place the plane on its circular flight path and draw it
        void DrawAirplaneInScene(bool asShadow)
        {
            float a = flightAngle * (float)Math.PI / 180.0f;
            float R = 4.0f, H = 2.2f;
            float px = R * (float)Math.Cos(a);
            float py = R * (float)Math.Sin(a);

            GL.glPushMatrix();
            GL.glTranslatef(px, py, H);
            GL.glRotatef(flightAngle + 90.0f, 0, 0, 1); // turn the nose along the path
            GL.glRotatef(18.0f, 1, 0, 0);               // bank into the turn
            GL.glScalef(0.65f, 0.65f, 0.65f);
            DrawPlane(asShadow);
            GL.glPopMatrix();
        }

        //--------------------------------------------------------------------
        //  the sailing boat (second hierarchical model, rocks with the waves)
        //--------------------------------------------------------------------
        void DrawBoat(bool asShadow)
        {
            // hull
            if (!asShadow) { GL.glEnable(GL.GL_LIGHTING); GL.glColor3f(0.45f, 0.27f, 0.13f); }
            DrawBox(0.0f, 0.0f, 0.10f, 0.7f, 0.28f, 0.12f);

            // cabin
            if (!asShadow) GL.glColor3f(0.85f, 0.85f, 0.88f);
            DrawBox(-0.25f, 0.0f, 0.26f, 0.22f, 0.20f, 0.10f);

            // mast
            if (!asShadow) GL.glColor3f(0.35f, 0.22f, 0.12f);
            GL.glPushMatrix();
            GL.glTranslatef(0.15f, 0, 0.20f);
            GL.glRotatef(0, 0, 1, 0);
            GLU.gluCylinder(obj, 0.035, 0.02, 0.95, 8, 2);
            GL.glPopMatrix();

            // triangular sail
            if (!asShadow) GL.glColor3f(0.96f, 0.96f, 0.96f);
            GL.glBegin(GL.GL_TRIANGLES);
            GL.glNormal3f(0, 1, 0);
            GL.glVertex3f(0.17f, 0.0f, 0.24f);
            GL.glVertex3f(0.17f, 0.0f, 1.05f);
            GL.glVertex3f(0.62f, 0.0f, 0.30f);
            GL.glNormal3f(0, -1, 0);
            GL.glVertex3f(0.17f, 0.0f, 0.24f);
            GL.glVertex3f(0.62f, 0.0f, 0.30f);
            GL.glVertex3f(0.17f, 0.0f, 1.05f);
            GL.glEnd();
        }

        // the boat sails on a wide circle around the island
        void DrawBoatInScene(bool asShadow)
        {
            float a = boatTravel * (float)Math.PI / 180.0f;
            float R = 7.0f;
            float bx = R * (float)Math.Cos(a);
            float by = R * (float)Math.Sin(a);
            float bob = 0.06f * (float)Math.Sin(boatPhase);
            float roll = 5.0f * (float)Math.Sin(boatPhase * 0.8f);

            GL.glPushMatrix();
            GL.glTranslatef(bx, by, bob);
            GL.glRotatef(boatTravel + 90.0f, 0, 0, 1);  // sail along the path
            GL.glRotatef(roll, 1, 0, 0);                // rock on the waves
            GL.glScalef(0.85f, 0.85f, 0.85f);
            DrawBoat(asShadow);
            GL.glPopMatrix();
        }

        // everything that moves and should be reflected / shadowed
        void DrawMovers(bool asShadow)
        {
            DrawAirplaneInScene(asShadow);
            if (showBoat) DrawBoatInScene(asShadow);
        }

        //--------------------------------------------------------------------
        //  the lighthouse keeper - a little walking man (hierarchical model).
        //  he faces +X, his feet are at z = 0.
        //--------------------------------------------------------------------
        // one swinging limb, pivoting at (x,y,zHip) and hanging down
        void DrawLimb(float x, float y, float zHip, float swing, float ht, float len)
        {
            GL.glPushMatrix();
            GL.glTranslatef(x, y, zHip);
            GL.glRotatef(swing, 0, 1, 0);   // swing forward / back
            DrawBox(0, 0, -len * 0.5f, ht, ht, len * 0.5f);
            GL.glPopMatrix();
        }

        void DrawKeeper()
        {
            GL.glEnable(GL.GL_LIGHTING);
            float sw = 28.0f * (float)Math.Sin(keeperWalk);

            // legs
            GL.glColor3f(0.20f, 0.20f, 0.32f);
            DrawLimb(0.0f, 0.07f, 0.18f, sw, 0.05f, 0.18f);
            DrawLimb(0.0f, -0.07f, 0.18f, -sw, 0.05f, 0.18f);

            // body
            GL.glColor3f(0.15f, 0.35f, 0.65f);
            DrawBox(0.0f, 0.0f, 0.30f, 0.09f, 0.13f, 0.12f);

            // arms (swing opposite to the legs)
            DrawLimb(0.0f, 0.15f, 0.40f, -sw, 0.04f, 0.15f);
            DrawLimb(0.0f, -0.15f, 0.40f, sw, 0.04f, 0.15f);

            // head
            GL.glColor3f(0.85f, 0.68f, 0.55f);
            GL.glPushMatrix();
            GL.glTranslatef(0, 0, 0.50f);
            GLU.gluSphere(obj, 0.07, 12, 12);
            // a red keeper's cap
            GL.glColor3f(0.75f, 0.12f, 0.12f);
            GL.glTranslatef(0, 0, 0.03f);
            GLU.gluCylinder(obj, 0.075, 0.0, 0.08, 12, 2);
            GL.glPopMatrix();
        }

        // place the keeper on a path that circles the lighthouse on the island
        void DrawKeeperInScene()
        {
            float r = 1.35f;
            float a = keeperAngle * (float)Math.PI / 180.0f;
            float kx = r * (float)Math.Cos(a);
            float ky = r * (float)Math.Sin(a);
            float kz = 0.9f * (2.2f - r) / 1.5f;   // sit on the island slope

            GL.glPushMatrix();
            GL.glTranslatef(kx, ky, kz);
            GL.glRotatef(keeperAngle + 90.0f, 0, 0, 1); // face the walking direction
            GL.glScalef(0.85f, 0.85f, 0.85f);
            DrawKeeper();
            GL.glPopMatrix();
        }

        //--------------------------------------------------------------------
        //  the static world
        //--------------------------------------------------------------------
        void DrawSea(bool translucent)
        {
            GL.glEnable(GL.GL_LIGHTING);
            if (translucent)
            {
                GL.glEnable(GL.GL_BLEND);
                GL.glColor4f(0.62f, 0.78f, 0.96f, 0.48f);
            }
            else
                GL.glColor4f(0.55f, 0.72f, 0.92f, 1.0f);

            EnableTexture(texWater);
            bool scroll = showTextures && texturesLoaded;
            if (scroll)
            {
                // slide the texture so the sea looks alive (texture matrix)
                GL.glMatrixMode(GL.GL_TEXTURE);
                GL.glPushMatrix();
                GL.glLoadIdentity();
                GL.glTranslatef(waterScroll, waterScroll * 0.4f, 0);
                GL.glMatrixMode(GL.GL_MODELVIEW);
            }

            GL.glNormal3f(0, 0, 1);
            float s = 13.0f, t = 6.0f;
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0, 0); GL.glVertex3f(-s, -s, 0);
            GL.glTexCoord2f(t, 0); GL.glVertex3f(s, -s, 0);
            GL.glTexCoord2f(t, t); GL.glVertex3f(s, s, 0);
            GL.glTexCoord2f(0, t); GL.glVertex3f(-s, s, 0);
            GL.glEnd();

            if (scroll)
            {
                GL.glMatrixMode(GL.GL_TEXTURE);
                GL.glPopMatrix();
                GL.glMatrixMode(GL.GL_MODELVIEW);
            }
            DisableTexture();
        }

        void DrawIsland()
        {
            GL.glEnable(GL.GL_LIGHTING);
            GL.glColor3f(1, 1, 1);
            EnableTexture(texSand);
            GL.glPushMatrix();
            GLU.gluCylinder(obj, 2.2, 0.7, 0.9, 28, 6); // sandy hill
            GL.glTranslatef(0, 0, 0.9f);
            GLU.gluDisk(obj, 0, 0.7, 28, 3);
            GL.glPopMatrix();
            DisableTexture();

            // a little red & white lighthouse on top of the island
            GL.glPushMatrix();
            GL.glTranslatef(0, 0, 0.9f);
            GL.glColor3f(0.92f, 0.92f, 0.92f);
            GLU.gluCylinder(obj, 0.20, 0.14, 0.85, 16, 3);
            GL.glTranslatef(0, 0, 0.85f);

            // the lamp - glows stronger at night
            GL.glDisable(GL.GL_LIGHTING);
            float glow = 0.35f + 0.65f * (1.0f - daylight);
            GL.glColor3f(glow, glow * 0.9f, 0.30f * glow);
            GLU.gluSphere(obj, 0.13, 12, 12);
            GL.glEnable(GL.GL_LIGHTING);

            GL.glColor3f(0.80f, 0.12f, 0.12f);
            GLU.gluDisk(obj, 0, 0.22, 16, 2);
            GLU.gluCylinder(obj, 0.22, 0.0, 0.28, 16, 2);
            GL.glPopMatrix();
        }

        // soft, additive light beam turning around the lighthouse
        void DrawBeam()
        {
            float a = 0.05f + 0.22f * (1.0f - daylight);
            GL.glDisable(GL.GL_LIGHTING);
            GL.glDisable(GL.GL_TEXTURE_2D);
            GL.glEnable(GL.GL_BLEND);
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE);   // additive glow
            GL.glDepthMask((byte)GL.GL_FALSE);
            GL.glColor4f(1.0f, 0.95f, 0.6f, a);
            for (int k = 0; k < 2; k++)
            {
                GL.glPushMatrix();
                GL.glTranslatef(0, 0, 1.78f);             // lamp height
                GL.glRotatef(beamAngle + k * 180.0f, 0, 0, 1);
                GL.glRotatef(95.0f, 0, 1, 0);             // outward and a touch down
                GLU.gluCylinder(obj, 0.04, 0.8, 7.0, 18, 1);
                GL.glPopMatrix();
            }
            GL.glDepthMask((byte)GL.GL_TRUE);
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
            GL.glDisable(GL.GL_BLEND);
            GL.glEnable(GL.GL_LIGHTING);
        }

        void DrawStars()
        {
            float a = (0.55f - daylight) * 2.2f;
            if (a <= 0) return;
            if (a > 1) a = 1;
            GL.glDisable(GL.GL_LIGHTING);
            GL.glDisable(GL.GL_TEXTURE_2D);
            GL.glDisable(GL.GL_DEPTH_TEST);
            GL.glEnable(GL.GL_BLEND);
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
            // each star is a tiny quad so it is easy to see and twinkles a bit
            for (int i = 0; i < STAR_COUNT; i++)
            {
                float tw = 0.6f + 0.4f * (float)Math.Sin(beamAngle * 0.1f + i);
                GL.glColor4f(1, 1, 0.9f, a * tw);
                float sx = starX[i], sy = starY[i], sz = starZ[i];
                float d = 0.12f;
                GL.glBegin(GL.GL_QUADS);
                GL.glVertex3f(sx - d, sy, sz - d);
                GL.glVertex3f(sx + d, sy, sz - d);
                GL.glVertex3f(sx + d, sy, sz + d);
                GL.glVertex3f(sx - d, sy, sz + d);
                GL.glEnd();
            }
            GL.glDisable(GL.GL_BLEND);
            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glEnable(GL.GL_LIGHTING);
        }

        void DrawBirds()
        {
            GL.glDisable(GL.GL_LIGHTING);
            GL.glDisable(GL.GL_TEXTURE_2D);
            GL.glColor3f(0.13f, 0.13f, 0.16f);
            float flap = 0.45f * (float)Math.Sin(wingFlap * 2.0f);
            float[,] off = { { 0, 0 }, { -0.7f, 0.55f }, { -0.7f, -0.55f } };
            GL.glLineWidth(2.0f);
            for (int i = 0; i < 3; i++)
            {
                GL.glPushMatrix();
                GL.glTranslatef(birdCross + off[i, 0], 5.0f + off[i, 1], 6.5f);
                GL.glBegin(GL.GL_LINE_STRIP);
                GL.glVertex3f(-0.35f, 0, -flap);
                GL.glVertex3f(0, 0, 0.12f);
                GL.glVertex3f(0.35f, 0, -flap);
                GL.glEnd();
                GL.glPopMatrix();
            }
            GL.glLineWidth(1.0f);
            GL.glEnable(GL.GL_LIGHTING);
        }

        void DrawSun()
        {
            // below the horizon at night - nothing to show
            if (pos[2] < -0.5f) return;
            GL.glDisable(GL.GL_LIGHTING);
            GL.glColor3f(1.0f, 0.94f, 0.40f);
            GL.glPushMatrix();
            GL.glTranslatef(pos[0], pos[1], pos[2]);
            GLU.gluSphere(obj, 0.4, 16, 16);
            GL.glPopMatrix();
            GL.glEnable(GL.GL_LIGHTING);
        }

        void DrawAxes()
        {
            GL.glDisable(GL.GL_LIGHTING);
            GL.glLineWidth(1);
            GL.glBegin(GL.GL_LINES);
            GL.glColor3f(1, 0, 0); GL.glVertex3f(0, 0, 0); GL.glVertex3f(5, 0, 0);
            GL.glColor3f(0, 1, 0); GL.glVertex3f(0, 0, 0); GL.glVertex3f(0, 5, 0);
            GL.glColor3f(0, 0, 1); GL.glVertex3f(0, 0, 0); GL.glVertex3f(0, 0, 5);
            GL.glEnd();
            GL.glEnable(GL.GL_LIGHTING);
        }

        //--------------------------------------------------------------------
        //  sun position, sky colour and light colour for the current frame
        //--------------------------------------------------------------------
        void UpdateDayNight()
        {
            if (dayNight)
            {
                float rad = sunAngle * (float)Math.PI / 180.0f;
                float elev = (float)Math.Sin(rad);
                pos[0] = 9.0f * (float)Math.Cos(rad);
                pos[1] = -2.5f;
                pos[2] = 9.0f * elev;
                pos[3] = 1.0f;

                daylight = elev * 1.25f + 0.15f;
                if (daylight < 0) daylight = 0;
                if (daylight > 1) daylight = 1;

                // warm glow while the sun is near the horizon
                float dusk = 1.0f - Math.Abs(elev) / 0.35f;
                if (dusk < 0 || elev < -0.25f) dusk = 0;

                skyR = Lerp(0.03f, 0.53f, daylight);
                skyG = Lerp(0.04f, 0.70f, daylight);
                skyB = Lerp(0.10f, 0.92f, daylight);
                skyR = Lerp(skyR, 0.97f, dusk * 0.6f);
                skyG = Lerp(skyG, 0.52f, dusk * 0.6f);
                skyB = Lerp(skyB, 0.26f, dusk * 0.6f);

                float[] amb = { Lerp(0.06f, 0.32f, daylight), Lerp(0.06f, 0.32f, daylight),
                                Lerp(0.11f, 0.32f, daylight), 1 };
                float[] dif = { Lerp(0.12f, 1.00f, daylight) + dusk * 0.25f,
                                Lerp(0.12f, 0.96f, daylight),
                                Lerp(0.22f, 0.88f, daylight) - dusk * 0.12f, 1 };
                GL.glLightfv(GL.GL_LIGHT0, GL.GL_AMBIENT, amb);
                GL.glLightfv(GL.GL_LIGHT0, GL.GL_DIFFUSE, dif);
            }
            else
            {
                pos[0] = ScrollValue[10];
                pos[1] = ScrollValue[11];
                pos[2] = ScrollValue[12];
                pos[3] = 1.0f;
                daylight = 1.0f;
                skyR = 0.53f; skyG = 0.70f; skyB = 0.92f;
                float[] amb = { 0.35f, 0.35f, 0.35f, 1 };
                float[] dif = { 0.95f, 0.95f, 0.88f, 1 };
                GL.glLightfv(GL.GL_LIGHT0, GL.GL_AMBIENT, amb);
                GL.glLightfv(GL.GL_LIGHT0, GL.GL_DIFFUSE, dif);
            }
        }

        //--------------------------------------------------------------------
        //  per frame
        //--------------------------------------------------------------------
        public void AdvanceAnimation()
        {
            if (animate)
            {
                propAngle -= 14.0f * flightSpeed;   // propeller is fast
                flightAngle += 0.7f * flightSpeed;  // plane is slow
                if (flightAngle > 360.0f) flightAngle -= 360.0f;
                if (propAngle < -360.0f) propAngle += 360.0f;

                beamAngle += 2.4f * flightSpeed;
                if (beamAngle > 360.0f) beamAngle -= 360.0f;
                boatPhase += 0.035f * flightSpeed;
                wingFlap += 0.25f * flightSpeed;
                birdCross += 0.03f * flightSpeed;
                if (birdCross > 13.0f) birdCross = -13.0f;
                waterScroll += 0.0016f * flightSpeed;
                boatTravel += 0.25f * flightSpeed;
                if (boatTravel > 360.0f) boatTravel -= 360.0f;
                keeperAngle += 0.5f * flightSpeed;
                if (keeperAngle > 360.0f) keeperAngle -= 360.0f;
                keeperWalk += 0.35f * flightSpeed;

                if (dayNight)
                {
                    sunAngle += 0.30f * flightSpeed;
                    if (sunAngle > 360.0f) sunAngle -= 360.0f;
                }
            }
            Draw();
        }

        public void Draw()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            UpdateDayNight();
            GL.glClearColor(skyR, skyG, skyB, 1.0f);
            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);
            GL.glLoadIdentity();

            double[] ModelVievMatrixBeforeSpecificTransforms = new double[16];
            double[] CurrentRotationTraslation = new double[16];

            GLU.gluLookAt(ScrollValue[0], ScrollValue[1], ScrollValue[2],
                          ScrollValue[3], ScrollValue[4], ScrollValue[5],
                          ScrollValue[6], ScrollValue[7], ScrollValue[8]);

            // ---- accumulated rotation / translation of the whole scene (mouse + spinners) ----
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            GL.glLoadIdentity();

            float delta;
            if (intOptionC != 0)
            {
                delta = 5.0f * Math.Abs(intOptionC) / intOptionC;
                switch (Math.Abs(intOptionC))
                {
                    case 1: GL.glRotatef(delta, 1, 0, 0); break;
                    case 2: GL.glRotatef(delta, 0, 1, 0); break;
                    case 3: GL.glRotatef(delta, 0, 0, 1); break;
                    case 4: GL.glTranslatef(delta / 20, 0, 0); break;
                    case 5: GL.glTranslatef(0, delta / 20, 0); break;
                    case 6: GL.glTranslatef(0, 0, delta / 20); break;
                }
            }
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);
            GL.glLoadMatrixd(AccumulatedRotationsTraslations);
            GL.glMultMatrixd(CurrentRotationTraslation);
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            GL.glMultMatrixd(AccumulatedRotationsTraslations);

            // light/sun position (placed in the rotated world)
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, pos);

            if (dayNight) DrawStars();
            if (showAxes) DrawAxes();

            // ---------------- water + reflection ----------------
            if (showReflection)
            {
                GL.glEnable(GL.GL_BLEND);
                GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);

                // mark the sea area in the stencil buffer only
                GL.glEnable(GL.GL_STENCIL_TEST);
                GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE);
                GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF);
                GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE);
                GL.glDisable(GL.GL_DEPTH_TEST);
                DrawSea(false);
                GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE);
                GL.glEnable(GL.GL_DEPTH_TEST);

                // draw the mirrored movers only inside the sea area
                GL.glStencilFunc(GL.GL_EQUAL, 1, 0xFFFFFFFF);
                GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP);
                GL.glPushMatrix();
                GL.glScalef(1, 1, -1);          // mirror through the water plane
                GL.glEnable(GL.GL_NORMALIZE);
                DrawMovers(false);
                GL.glDisable(GL.GL_NORMALIZE);
                GL.glPopMatrix();
                GL.glDisable(GL.GL_STENCIL_TEST);

                // the real (semi transparent) water, so the reflection shows through
                GL.glDepthMask((byte)GL.GL_FALSE);
                DrawSea(true);
                GL.glDepthMask((byte)GL.GL_TRUE);
                GL.glDisable(GL.GL_BLEND);
            }
            else
            {
                DrawSea(false);
            }

            // ---------------- island ----------------
            DrawIsland();

            // ---------------- shadow on the water ----------------
            if (showShadow)
            {
                ground[0, 0] = 1; ground[0, 1] = 1; ground[0, 2] = 0.02f;
                ground[1, 0] = 0; ground[1, 1] = 1; ground[1, 2] = 0.02f;
                ground[2, 0] = 1; ground[2, 1] = 0; ground[2, 2] = 0.02f;

                GL.glDisable(GL.GL_LIGHTING);
                GL.glEnable(GL.GL_BLEND);
                GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
                GL.glColor4f(0.0f, 0.0f, 0.1f, 0.35f);
                GL.glPushMatrix();
                MakeShadowMatrix(ground);
                GL.glMultMatrixf(cubeXform);
                DrawMovers(true);
                GL.glPopMatrix();
                GL.glDisable(GL.GL_BLEND);
                GL.glEnable(GL.GL_LIGHTING);
            }

            // ---------------- the real movers ----------------
            DrawMovers(false);
            if (showKeeper) DrawKeeperInScene();

            if (showBirds) DrawBirds();
            if (showBeam) DrawBeam();

            // ---------------- the sun ----------------
            if (showSun) DrawSun();

            GL.glFlush();
            WGL.wglSwapBuffers(m_uint_DC);
        }

        //--------------------------------------------------------------------
        //  projection (perspective <-> orthographic, plus zoom)
        //--------------------------------------------------------------------
        public void SetProjection()
        {
            if (Height == 0) Height = 1;
            float aspect = (float)Width / (float)Height;

            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();
            if (isPerspective)
                GLU.gluPerspective(fovy, aspect, 0.4, 100.0);
            else
                GL.glOrtho(-orthoScale * aspect, orthoScale * aspect,
                           -orthoScale, orthoScale, -100.0, 100.0);
            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();
        }

        protected virtual void InitializeGL()
        {
            m_uint_HWND = (uint)p.Handle.ToInt32();
            m_uint_DC = WGL.GetDC(m_uint_HWND);
            WGL.wglSwapBuffers(m_uint_DC);

            WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
            WGL.ZeroPixelDescriptor(ref pfd);
            pfd.nVersion = 1;
            pfd.dwFlags = (WGL.PFD_DRAW_TO_WINDOW | WGL.PFD_SUPPORT_OPENGL | WGL.PFD_DOUBLEBUFFER);
            pfd.iPixelType = (byte)(WGL.PFD_TYPE_RGBA);
            pfd.cColorBits = 32;
            pfd.cDepthBits = 32;
            pfd.cStencilBits = 32;   // needed for the reflection
            pfd.iLayerType = (byte)(WGL.PFD_MAIN_PLANE);

            int pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pfd);
            if (pixelFormatIndex == 0) { MessageBox.Show("Unable to retrieve pixel format"); return; }
            if (WGL.SetPixelFormat(m_uint_DC, pixelFormatIndex, ref pfd) == 0)
            { MessageBox.Show("Unable to set pixel format"); return; }

            m_uint_RC = WGL.wglCreateContext(m_uint_DC);
            if (m_uint_RC == 0) { MessageBox.Show("Unable to get rendering context"); return; }
            if (WGL.wglMakeCurrent(m_uint_DC, m_uint_RC) == 0)
            { MessageBox.Show("Unable to make rendering context current"); return; }

            initRenderingGL();
        }

        public void OnResize()
        {
            Width = p.Width;
            Height = p.Height;
            GL.glViewport(0, 0, Width, Height);
            SetProjection();
            Draw();
        }

        protected virtual void initRenderingGL()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0) return;
            if (this.Width == 0 || this.Height == 0) return;

            GL.glShadeModel(GL.GL_SMOOTH);
            GL.glClearColor(0.53f, 0.70f, 0.92f, 1.0f); // sky
            GL.glClearDepth(1.0f);
            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);
            GL.glHint(GL.GL_PERSPECTIVE_CORRECTION_Hint, GL.GL_NICEST);

            float[] amb = { 0.35f, 0.35f, 0.35f, 1.0f };
            float[] dif = { 0.95f, 0.95f, 0.88f, 1.0f };
            float[] spe = { 1.0f, 1.0f, 1.0f, 1.0f };
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_AMBIENT, amb);
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_DIFFUSE, dif);
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_SPECULAR, spe);
            float[] globalAmb = { 0.25f, 0.25f, 0.25f, 1.0f };
            GL.glLightModelfv(GL.GL_LIGHT_MODEL_AMBIENT, globalAmb);

            GL.glEnable(GL.GL_LIGHT0);
            GL.glEnable(GL.GL_LIGHTING);
            GL.glEnable(GL.GL_COLOR_MATERIAL);
            GL.glColorMaterial(GL.GL_FRONT_AND_BACK, GL.GL_AMBIENT_AND_DIFFUSE);

            float[] matSpec = { 0.6f, 0.6f, 0.6f, 1.0f };
            GL.glMaterialfv(GL.GL_FRONT_AND_BACK, GL.GL_SPECULAR, matSpec);
            GL.glMaterialf(GL.GL_FRONT_AND_BACK, GL.GL_SHININESS, 40.0f);

            GL.glEnable(GL.GL_NORMALIZE);

            GL.glViewport(0, 0, this.Width, this.Height);
            SetProjection();

            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);
        }
    }
}

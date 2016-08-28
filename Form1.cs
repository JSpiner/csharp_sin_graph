using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp_sin
{
    public partial class Form1 : Form
    {
        float angle = 0;
        int r = 100;
        int type = 1;

        List<Pos> posList1;             // for graph 1
        List<Pos> posList2;             // for graph 2

        class Pos
        {
            public int x;
            public int y;

            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            angle += 0.05f;
            if ((180 / Math.PI * angle) > 360 * 2)
            {
                angle = 0;
                posList1.Clear();
                posList2.Clear();
            }
            label1.Text = "angle : " + (180 / Math.PI * angle);

            /*
            draw graph 1 
            */
            Graphics g1 = Graphics.FromImage(pictureBox1.Image);
            g1.Clear(Color.Transparent);

            //draw circle frame
            g1.DrawLine(Pens.Black, new Point(r, r), new Point(r * 2 + r / 10, r));
            g1.DrawEllipse(Pens.Black, new Rectangle(r - 5, r - 5, 10, 10));

            //draw circle line
            g1.DrawLine(Pens.Blue, new Point(r - (int)(r * cos(angle)), r - (int)(r * sin(angle))),
                new Point(r + (int)(r*cos(angle)), r + (int)(r * sin(angle))));

            float length = getLength(angle);
            g1.DrawEllipse(Pens.Red,
                new Rectangle(
                    r + (int)(length * cos(angle)) - 5,
                    r + (int)(length * sin(angle)) - 5,
                    10, 10));

            posList1.Add(new Pos(r + (int)(length * cos(angle)), r + (int)(length * sin(angle))));

            for (int i = 0; i < posList1.Count - 1; i++)
            {
                Pos prevPos = posList1[i];
                Pos nextPos = posList1[i + 1];

                g1.DrawLine(Pens.Green, new Point(prevPos.x, prevPos.y), new Point(nextPos.x, nextPos.y));
            }

            /*
            draw graph 2
            */
            Graphics g2 = Graphics.FromImage(pictureBox2.Image);
            g2.Clear(Color.Transparent);

            g2.DrawLine(Pens.Black, new Point(10, r * 2 + 15), new Point(r * 5, r * 2 + 15));
            g2.DrawLine(Pens.Black, new Point(10, 10), new Point(10, r * 2 + 15));

            int value = (int)getValue(angle);
            posList2.Add(new Pos((int)(180 / Math.PI * angle / 2) + 15, value ));
            g2.DrawEllipse(Pens.Red, 
                new Rectangle(
                    (int)(180 / Math.PI * angle / 2) + 15 - 5, 
                    value - 5, 
                    10, 10));

            for(int i = 0; i < posList2.Count - 1; i++)
            {
                Pos prevPos = posList2[i];
                Pos nextPos = posList2[i + 1];

                g2.DrawLine(Pens.Green, new Point(prevPos.x, prevPos.y), new Point(nextPos.x, nextPos.y));
            }


            //update
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
        }

        private float getValue(float angle)
        {
            switch (type)
            {
                case 1:
                    return 10;
                    break;
                case 2:
                    return 60 + r / 2 + (int)(r * cos(angle));
                    break;
                case 3:
                    return 60 + r / 2 + (int)(r * cos(angle * 3));
                    break;
                case 4:
                    return 60 + r / 2 + (int)(r * cos(angle * 5));
                    break;
            }
            return 0;
        }

        private float getLength(float angle)
        {
            switch (type)
            {
                case 1:
                    return r;
                    break;
                case 2:
                    return (float)(r * cos(angle));
                    break;
                case 3:
                    return (float)(r * cos(angle * 3));
                    break;
                case 4:
                    return (float)(r * cos(angle * 5));
                    break;
            }
            return r;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            /*
            init
            */
            pictureBox1.Image = new Bitmap(500, 500);
            pictureBox2.Image = new Bitmap(500, 500);

            posList1 = new List<Pos>();
            posList2 = new List<Pos>();

            pictureBox1.Invalidate();
        }
        
        private double sin(float angle)
        {
            return Math.Sin((double)angle);
        }

        private double cos(float angle)
        {
            return Math.Cos((double)angle);
        }

        private void clearGraph()
        {
            angle = 0;
            posList1.Clear();
            posList2.Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            type = 1;
            clearGraph();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            type = 2;
            clearGraph();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            type = 3;
            clearGraph();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            type = 4;
            clearGraph();
        }
    }
}

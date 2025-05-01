using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab7CSharp
{
    public partial class Form1 : Form
    {
        private Timer timer;
        private int currentXIndex = 0;
        private float[] xValues;
        private float[] yValues;
        private Pen graphPen;
        private Font axisFont;
        private Brush axisBrush;

        public Form1()
        {
            InitializeComponent();
            InitializeGraph();
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            infoBox.Text = this.DesktopLocation.ToString();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            infoBox.Text = "No";
        }

        private void ExitButton_MouseHover(object sender, MouseEventArgs e)
        {
            exitButton.Location = new Point(exitButton.Location.X, (exitButton.Location.Y + 100) % 500);
        }

        private void ExitButton_MouseHover(object sender, EventArgs e)
        {
            exitButton.Location = new Point(exitButton.Location.X, (exitButton.Location.Y + 100) % 500);
        }
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red, 2);

            // Scaling factors
            float scaleX = 20f; // pixels per unit in X
            float scaleY = 50f; // pixels per unit in Y
            float from = -2;
            float to = 2;
            int numberOfPoints = (int)Math.Ceiling(to - from * scaleX);

            // Origin point in screen coordinates
            float originX = this.ClientSize.Width / 2;
            float originY = this.ClientSize.Height / 2;

            // axes
            g.DrawLine(Pens.Black, 0, originY, this.ClientSize.Width, originY); // X-axis
            g.DrawLine(Pens.Black, originX, 0, originX, this.ClientSize.Height); // Y-axis

            PointF[] points = new PointF[numberOfPoints];
            int centralPoint = (int)Math.Floor((double)numberOfPoints / 2.0);
            for (int i = 0; i < points.Length; i++)
            {
                float x = (i - centralPoint) / scaleX;  // from -12.5 to +12.5
                float y = Graph(x);
                points[i] = new PointF(originX + x * scaleX, originY - y * scaleY);
            }

            g.DrawLines(pen, points);
        }

        private float Graph(float x)
        {
            return (1 - x * x) * (x - 2);
        }
        private void InitializeGraph()
        {
            int points = 200;
            xValues = new float[points];
            yValues = new float[points];
            float step = 4f / points; // від -2 до 2
            for (int i = 0; i < points; i++)
            {
                float x = -2f + i * step;
                xValues[i] = x;
                yValues[i] = (1 - x * x) * (x - 2);
            }

            timer = new Timer();
            timer.Interval = 30;
            timer.Tick += Timer_Tick;

            graphPen = new Pen(Color.Red, 2);
            axisFont = new Font("Arial", 12, FontStyle.Regular);
            axisBrush = Brushes.Black;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioRed.Checked) graphPen.Color = Color.Red;
            else if (radioBlue.Checked) graphPen.Color = Color.Blue;
            else if (radioGreen.Checked) graphPen.Color = Color.Green;

            FontStyle style = FontStyle.Regular;
            if (radioBold.Checked) style = FontStyle.Bold;
            else if (radioItalic.Checked) style = FontStyle.Italic;

            int fontSize = 12;
            if (radioSmall.Checked) fontSize = 8;
            else if (radioLarge.Checked) fontSize = 16;

            axisFont = new Font("Arial", fontSize, style);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            currentXIndex = 0;

            DrawAxes();
            timer.Start();
        }

        private void DrawAxes()
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;

            int centerX = w / 2;
            int centerY = h / 2;

            g.DrawLine(Pens.Black, 0, centerY, w, centerY); // X
            g.DrawLine(Pens.Black, centerX, 0, centerX, h); // Y

            g.DrawString("X", axisFont, axisBrush, w - 20, centerY + 5);
            g.DrawString("Y", axisFont, axisBrush, centerX + 5, 5);

            pictureBox1.Refresh();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentXIndex >= xValues.Length - 1)
            {
                timer.Stop();
                return;
            }

            Graphics g = Graphics.FromImage(pictureBox1.Image);
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;
            int centerX = w / 2;
            int centerY = h / 2;

            float scaleX = w / 4f;
            float scaleY = h / 16f;

            float x1 = centerX + xValues[currentXIndex] * scaleX;
            float y1 = centerY - yValues[currentXIndex] * scaleY;
            float x2 = centerX + xValues[currentXIndex + 1] * scaleX;
            float y2 = centerY - yValues[currentXIndex + 1] * scaleY;

            g.DrawLine(graphPen, x1, y1, x2, y2);
            pictureBox1.Refresh();

            currentXIndex++;
        }

        private Figure[] figures;
        private Random rand = new Random();
        private Bitmap bmp;
        
        
        
        
        private void buttonDraw_Click(object sender, EventArgs e)
        {
            int n = int.Parse(textBox1.Text);
            figures = new Figure[n];

            if (bmp == null)
                bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            Graphics g = Graphics.FromImage(bmp);

            for (int i = 0; i < n; i++)
            {
                int x = rand.Next(20, pictureBox2.Width - 40);
                int y = rand.Next(20, pictureBox2.Height - 40);
                Color color = Color.FromName(comboBox2.SelectedItem.ToString());
                string text = textBox2.Text;

                switch (comboBox1.SelectedItem.ToString())
                {
                    case "Square":
                        figures[i] = new Square(x, y, color, text, int.Parse(textBox4.Text));
                        break;
                    case "Star":
                        figures[i] = new Star(x, y, color, text, int.Parse(textBox6.Text));
                        break;
                    case "Triangle":
                        figures[i] = new Triangle(x, y, color, text, int.Parse(textBox5.Text));
                        break;
                    case "Circle":
                        figures[i] = new Circle(x, y, color, text, int.Parse(textBox3.Text));
                        break;
                }
                figures[i].Draw(g);
            }
            pictureBox2.Image = bmp;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            pictureBox2.Image = null;
        }
    }
}

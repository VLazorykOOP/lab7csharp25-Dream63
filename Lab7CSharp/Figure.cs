using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7CSharp
{
    abstract class Figure
    {
        public int X, Y;
        public Color Color;
        public string Text;
        public abstract void Draw(Graphics g);
        public abstract void Move(int dx, int dy);
    }

    class Square : Figure
    {
        public int Size;

        public Square(int x, int y, Color color, string text, int size)
        {
            X = x;
            Y = y;
            Color = color;
            Text = text;
            Size = size;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, 2);
            g.DrawRectangle(pen, X, Y, Size, Size);
            g.DrawString(Text, SystemFonts.DefaultFont, Brushes.Black, X + Size / 4, Y + Size / 4);
        }

        public override void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }

    class Star : Figure
    {
        public int Size;

        public Star(int x, int y, Color color, string text, int size)
        {
            X = x;
            Y = y;
            Color = color;
            Text = text;
            Size = size;
        }
        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, 2);
            Point[] points1 = GetEquilateralTrianglePoints(X, Y, Size, 0);
            Point[] points2 = GetEquilateralTrianglePoints(X, Y, Size, 180);
            g.DrawPolygon(pen, points1);
            g.DrawPolygon(pen, points2);
            g.DrawString(Text, SystemFonts.DefaultFont, Brushes.Black, X - 10, Y);
        }

        Point[] GetEquilateralTrianglePoints(float centerX, float centerY, float size, float degreeOffset)
        {
            Point[] points = new Point[3];
            float radius = size / (float)Math.Sqrt(3);

            for (int i = 0; i < 3; i++)
            {
                double angleDeg = -90 + degreeOffset + i * 120;
                double angleRad = angleDeg * Math.PI / 180;
                float x = centerX + radius * (float)Math.Cos(angleRad);
                float y = centerY + radius * (float)Math.Sin(angleRad);
                points[i] = new Point((int)x, (int)y);
            }

            return points;
        }


        public override void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }

    class Triangle : Figure
    {
        public int Size;

        public Triangle(int x, int y, Color color, string text, int size)
        {
            X = x;
            Y = y;
            Color = color;
            Text = text;
            Size = size;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, 2);
            Point[] points = GetEquilateralTrianglePoints(X, Y, Size, 0);
            g.DrawPolygon(pen, points);
            g.DrawString(Text, SystemFonts.DefaultFont, Brushes.Black, X - 10, Y);
        }

        Point[] GetEquilateralTrianglePoints(float centerX, float centerY, float size, float degreeOffset)
        {
            Point[] points = new Point[3];
            float radius = size / (float)Math.Sqrt(3);

            for (int i = 0; i < 3; i++)
            {
                double angleDeg = (-90 + i * 120) + degreeOffset;
                double angleRad = angleDeg * Math.PI / 180;
                float x = centerX + radius * (float)Math.Cos(angleRad);
                float y = centerY + radius * (float)Math.Sin(angleRad);
                points[i] = new Point((int)x, (int)y);
            }

            return points;
        }

        public override void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }

    class Circle : Figure
    {
        public int Radius;

        public Circle(int x, int y, Color color, string text, int radius)
        {
            X = x;
            Y = y;
            Color = color;
            Text = text;
            Radius = radius;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, 2);
            g.DrawEllipse(pen, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
            g.DrawString(Text, SystemFonts.DefaultFont, Brushes.Black, X - Radius / 2, Y - Radius / 2);
        }

        public override void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace LAB78_OOP.Mod
{
    public class Polygon : Circle
    {
        private int n, rotate = 0;
        List<PointF> lst;
        public Polygon() : base()
        {
            name = "Polygon";
        }

        public Polygon(int x, int y, int r, int n, Color c, int Width, int Height) : base(x, y, r, c, Width, Height)
        {
            this.n = n;
            //контроль выхода за границы при инициализации
            if (r > x) r = x;
            if (x + r > width) r = width - x;
            if (r > y) r = y;
            if (y + r > height) r = height - y;
            Resize();
            name = "Polygon";
        }

        public override void DrawObj(Graphics e)
        {
            e.DrawPolygon(new Pen(Color.Black, 2), lst.ToArray());
            e.FillPolygon(new SolidBrush(col), lst.ToArray());
        }

        public override void DrawRectangle(Graphics e, Pen pen)
        {
            e.DrawRectangle(pen, rect);
        }

        public override bool Find(int _x, int _y)
        {
            if (rect.X < _x && _x < rect.Right && rect.Y < _y && _y < rect.Bottom) return true; else return false;
        }

        public override Rectangle GetRectangle()
        {
            return rect;
        }

        public override void Grow(int gr)
        {
            if (radius + gr < x && x + radius + gr < width && radius + gr < y && y + radius + gr < height && radius + gr > 0) radius += gr;
            Resize();
        }

        public override void Load(StreamReader stream)
        {
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            x = Convert.ToInt32(data[0]);
            y = Convert.ToInt32(data[1]);
            radius = Convert.ToInt32(data[2]);
            n = Convert.ToInt32(data[3]);
            rotate = Convert.ToInt32(data[4]);
            col = Color.FromArgb(Convert.ToInt32(data[5]), Convert.ToInt32(data[6]), Convert.ToInt32(data[7]));
            width = Convert.ToInt32(data[8]);
            height = Convert.ToInt32(data[9]);
            Resize();
        }

        public override void OffsetXY(int _x, int _y)
        {            
            if (x + _x > radius && x + _x + radius < width) x += _x;
            if (y + _y > radius && y + _y + radius < height) y += _y;
            Resize();
        }

        public override void Resize()
        {
            lst = null;
            lst = new List<PointF>();
            for (int i = rotate; i < rotate + 360; i += 360 / n)
            {
                double radiani = (double)(i * 3.14) / 180;
                float xx = x + (int)(radius * Math.Cos(radiani));
                float yy = y + (int)(radius * Math.Sin(radiani));
                lst.Add(new PointF(xx, yy));
            }
            rect = new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius);
        }

        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("Polygon");
            stream.WriteLine(x + " " + y + " " + radius + " " + n + " " + rotate + " " + col.R + " " + col.G + " " + col.B + " " + width + " " + height);
        }

        public override void SetColor(Color c)
        {
            col = c;
        }

        public override string GetInfo()
        {
            return name + "  X: " + x + " Y: " + y + " R: " + radius + " N: " + n + " " + col.ToString();
        }

        public override void Rotate(int gr)
        {
            rotate += gr;
            Resize();
        }

        public override void GrowN(int gr)
        {
            if (n + gr > 2) n += gr;
            Resize();
        }
    }

    public class Star : Circle
    {
        private int n, rotate = 0;
        List<PointF> lst;
        public Star() : base()
        {
            name = "Star";
        }

        public Star(int x, int y, int r, int n, Color c, int Width, int Height) : base(x, y, r, c, Width, Height)
        {
            this.n = n;
            //контроль выхода за границы при инициализации
            if (r > x) r = x;
            if (x + r > width) r = width - x;
            if (r > y) r = y;
            if (y + r > height) r = height - y;
            Resize();
            name = "Star";
        }

        public override void DrawObj(Graphics e)
        {
            e.DrawPolygon(new Pen(Color.Black, 2), lst.ToArray());
            e.FillPolygon(new SolidBrush(col), lst.ToArray());
        }

        public override void DrawRectangle(Graphics e, Pen pen)
        {
            e.DrawRectangle(pen, rect);
        }

        public override bool Find(int _x, int _y)
        {
            if (rect.X < _x && _x < rect.Right && rect.Y < _y && _y < rect.Bottom) return true; else return false;
        }

        public override Rectangle GetRectangle()
        {
            return rect;
        }

        public override void Grow(int gr)
        {
            if (radius + gr < x && x + radius + gr < width && radius + gr < y && y + radius + gr < height && radius + gr > 0) radius += gr;
            Resize();
        }

        public override void Load(StreamReader stream)
        {
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            x = Convert.ToInt32(data[0]);
            y = Convert.ToInt32(data[1]);
            radius = Convert.ToInt32(data[2]);
            n = Convert.ToInt32(data[3]);
            rotate = Convert.ToInt32(data[4]);
            col = Color.FromArgb(Convert.ToInt32(data[5]), Convert.ToInt32(data[6]), Convert.ToInt32(data[7]));
            width = Convert.ToInt32(data[8]);
            height = Convert.ToInt32(data[9]);

            Resize();
        }

        public override void Rotate(int gr)
        {
            rotate += gr;
            Resize();
        }

        public override void OffsetXY(int _x, int _y)
        {            
            if (x + _x > radius && x + _x + radius < width) x += _x;
            if (y + _y > radius && y + _y + radius < height) y += _y;
            Resize();
        }

        public override void Resize()
        {
            lst = null;
            lst = new List<PointF>();
            double a = rotate * Math.PI / 180, da = Math.PI / n, l;
            for (int k = 0; k < 2 * n + 1; k++)
            {
                l = k % 2 == 0 ? radius : 0.37 * radius;
                lst.Add(new PointF((float)(x + l * Math.Cos(a)), (float)(y + l * Math.Sin(a))));
                a += da;
            }
            rect = new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius);
        }

        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("Star");
            stream.WriteLine(x + " " + y + " " + radius + " " + n + " " + rotate + " " + col.R + " " + col.G + " " + col.B + " " + width + " " + height);
        }

        public override void SetColor(Color c)
        {
            col = c;
        }

        public override string GetInfo()
        {
            return name + "  X: " + x + " Y: " + y + " R: " + radius + " N: " + n + " " + col.ToString();
        }

        public override void GrowN(int gr)
        {
            if (n + gr > 3) n += gr;
            Resize();
        }
    }
}

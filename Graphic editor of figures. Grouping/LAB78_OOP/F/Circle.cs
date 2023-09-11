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
    public class Circle : Shape
    {
        protected int radius;
        public Circle()
        {
            x = 0;
            y = 0;
            radius = 0;
            name = "Circle";
        }
        public Circle(int x, int y, int r, Color c, int Width, int Height)
        {
            width = Width;
            height = Height;
            name = "Circle";
            this.x = x;
            this.y = y;
            col = c;

            
            if (r > x) r = x;  //контроль выхода за границы 
            if (x + r > width) r = width - x;
            if (r > y) r = y;
            if (y + r > height) r = height - y;
            radius = r;
            rect = new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius);
        }

        public override void DrawObj(Graphics e)
        {
            e.DrawEllipse(new Pen(Color.Black, 2), rect);
            e.FillEllipse(new SolidBrush(col), rect);
        }

        public override void DrawRectangle(Graphics e, Pen pen)
        {
            e.DrawRectangle(pen, rect);
        }

        public override void OffsetXY(int _x, int _y)
        {           
            if (x + _x > radius && x + _x + radius < width) x += _x;
            if (y + _y > radius && y + _y + radius < height) y += _y;
            Resize();
        }

        public override void Resize()
        {
            rect = new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius);
        }

        public override void Grow(int gr)
        {
            if (radius + gr < x && x + radius + gr < width && radius + gr < y && y + radius + gr < height && radius + gr > 0) radius += gr;
            Resize();
        }

        public override void SetColor(Color c)
        {
            col = c;
        }
        public override Rectangle GetRectangle()
        {
            return rect;
        }

        public override bool Find(int _x, int _y)
        {
            if (Math.Pow(x - _x, 2) + Math.Pow(y - _y, 2) <= radius * radius) return true; else return false;
        }

        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("Circle");
            stream.WriteLine((rect.X + radius) + " " + (rect.Y + radius) + " " + radius + " " + col.R + " " + col.G + " " + col.B + " " + width + " " + height);
        }

        public override void Load(StreamReader stream)
        {
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            x = Convert.ToInt32(data[0]);
            y = Convert.ToInt32(data[1]);
            radius = Convert.ToInt32(data[2]);
            col = Color.FromArgb(Convert.ToInt32(data[3]), Convert.ToInt32(data[4]), Convert.ToInt32(data[5]));
            width = Convert.ToInt32(data[6]);
            height = Convert.ToInt32(data[7]);
            Resize();
        }

        public override string GetInfo()
        {
            return name + "  X: " + x + " Y: " + y + " R: " + radius + " " + col.ToString();
        }

        public override void Rotate(int gr)
        {

        }

        public override bool Find(Shape obj)
        {
            string[] data = obj.GetInfo().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (data[0] != "Group")
            {
                int _x = Convert.ToInt32(data[2]);
                int _y = Convert.ToInt32(data[4]);
                int _rad = Convert.ToInt32(data[6]);
                if (Math.Pow(x - _x, 2) + Math.Pow(y - _y, 2) <= Math.Pow(radius + _rad, 2)) return true;
            }
            else return obj.Find(this); //просим группу искать объект
            return false;
        }

        public override void GrowN(int gr)
        {
        }
    }
}

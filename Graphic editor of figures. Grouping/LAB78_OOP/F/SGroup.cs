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
    public class SGroup : Shape
    {
        public Storage<Shape> sto;

        public SGroup()
        {
            sto = new Storage<Shape>();
            name = "Group";
        }

        public SGroup(int Width, int Height)
        {
            width = Width;
            height = Height;
            sto = new Storage<Shape>();
            name = "Group";
        }

        public void Add(Shape s)
        {
            sto.add(s);
            sto.get().sticky = false;
            if (sto.Size() == 1) rect = new Rectangle(s.GetRectangle().X, s.GetRectangle().Y, s.GetRectangle().Width, s.GetRectangle().Height);
            else
            {
                if (s.GetRectangle().Left < rect.Left)
                {
                    int tmp = rect.Right;
                    rect.X = s.GetRectangle().Left;
                    rect.Width = tmp - rect.X;
                }
                if (s.GetRectangle().Right > rect.Right) rect.Width = s.GetRectangle().Right - rect.X;
                if (s.GetRectangle().Top < rect.Top)
                {
                    int tmp = rect.Bottom;
                    rect.Y = s.GetRectangle().Top;
                    rect.Height = tmp - rect.Y;
                }
                if (s.GetRectangle().Bottom > rect.Bottom) rect.Height = s.GetRectangle().Bottom - rect.Y;
            }
        }

        public Shape Out()
        {
            if (sto.Size() != 0)
            {
                Shape tmp = sto.get();
                sto.del();
                Resize();
                return tmp;
            }
            return null;
        }

        public int size()
        {
            return sto.Size();
        }

        public override void Resize()
        {
            if (sto.Size() != 0)
            {
                sto.toFirst();
                rect = sto.GetIterator().GetRectangle();
                for (int i = 0; i < sto.Size(); i++, sto.next())
                {
                    if (sto.GetIterator().GetRectangle().Left < rect.Left)
                    {
                        int tmp = rect.Right;
                        rect.X = sto.GetIterator().GetRectangle().Left;
                        rect.Width = tmp - rect.X;
                    }
                    if (sto.GetIterator().GetRectangle().Right > rect.Right) rect.Width = sto.GetIterator().GetRectangle().Right - rect.X;
                    if (sto.GetIterator().GetRectangle().Top < rect.Top)
                    {
                        int tmp = rect.Bottom;
                        rect.Y = sto.GetIterator().GetRectangle().Top;
                        rect.Height = tmp - rect.Y;
                    }
                    if (sto.GetIterator().GetRectangle().Bottom > rect.Bottom) rect.Height = sto.GetIterator().GetRectangle().Bottom - rect.Y;
                }
            }
        }

        public override void DrawObj(Graphics e)
        {
            if (sto.Size() != 0)
            {
                sto.toFirst();
                for (int i = 0; i < sto.Size(); i++, sto.next()) sto.GetIterator().DrawObj(e);
            }
        }

        public override void Grow(int gr)
        {
            if (sto.Size() != 0)
            {
                if (gr > 0 && rect.X + gr > 1 && gr + rect.Right < width - 1 && rect.Y + gr > 1 && gr + rect.Bottom < height - 1)
                {
                    rect.X -= gr;
                    rect.Y -= gr;
                    rect.Width += 2 * gr;
                    rect.Height += 2 * gr;
                    sto.toFirst();
                    for (int i = 0; i < sto.Size(); i++, sto.next()) sto.GetIterator().Grow(gr);
                }
                if (gr < 0)
                {
                    sto.toFirst();
                    for (int i = 0; i < sto.Size(); i++, sto.next()) sto.GetIterator().Grow(gr);
                    if (gr < 0) Resize();
                }
            }
        }

        public override void OffsetXY(int _x, int _y)
        {
            if (sto.Size() != 0)
            {
                if (rect.X + _x > 0 && _x + rect.Right < width)
                {
                    rect.X += _x;
                    sto.toFirst();
                    for (int i = 0; i < sto.Size(); i++, sto.next()) sto.GetIterator().OffsetXY(_x, 0);
                }
                if (rect.Y + _y > 0 && _y + rect.Bottom < height)
                {
                    rect.Y += _y;
                    sto.toFirst();
                    for (int i = 0; i < sto.Size(); i++, sto.next()) sto.GetIterator().OffsetXY(0, _y);
                }
            }
        }

        public override void SetColor(Color c)
        {
            if (sto.Size() != 0)
            {
                sto.toFirst();
                for (int i = 0; i < sto.Size(); i++, sto.next()) sto.GetIterator().SetColor(c);
            }
        }

        public override Rectangle GetRectangle()
        {
            return rect;
        }

        public override bool Find(int _x, int _y)
        {
            if (rect.X < _x && _x < rect.Right && rect.Y < _y && _y < rect.Bottom) return true; else return false;
        }

        public override void DrawRectangle(Graphics e, Pen pen)
        {
            e.DrawRectangle(pen, rect);
        }

        public override void Save(StreamWriter stream)
        {
            stream.WriteLine("Group");
            stream.WriteLine(sto.Size() + " " + width + " " + height);
            if (sto.Size() != 0)
            {
                sto.toFirst();
                for (int i = 0; i < sto.Size(); i++, sto.next()) sto.GetIterator().Save(stream);
            }
        }

        public override void Load(StreamReader stream)
        {
            ShapeFactory factory = new ShapeFactory();
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int i = Convert.ToInt32(data[0]);
            width = Convert.ToInt32(data[1]);
            height = Convert.ToInt32(data[2]);
            for (; i > 0; i--)
            {
                Shape tmp = factory.createShape(stream.ReadLine());
                tmp.Load(stream);
                Add(tmp);
            }
        }

        public override string GetInfo()
        {
            return name + "    Size : " + sto.Size().ToString();
        }

        public override void Rotate(int gr)
        {
            if (sto.Size() != 0)
            {
                sto.toFirst();
                for (int i = 0; i < sto.Size(); i++, sto.next()) sto.GetIterator().Rotate(gr);
            }
        }

        public override bool Find(Shape obj)
        {
            if (sto.Size() != 0)
            {
                sto.toFirst();
                for (int i = 0; i < sto.Size(); i++, sto.next()) if (sto.GetIterator().Find(obj) == true) return true;
            }
            return false;
        }

        public override void GrowN(int gr)
        {
            throw new NotImplementedException();
        }
    }
}

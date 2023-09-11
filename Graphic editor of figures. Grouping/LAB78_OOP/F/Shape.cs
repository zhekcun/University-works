using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace LAB78_OOP.Mod
{
    public abstract class Shape : ObjObserved
    {
        public string name; //имена для всех объектов и групп
        public Color col;
        protected Rectangle rect; //область объекта для его отрисовки и выделения
        protected int x, y, width, height; //x, y - позиции объектов и групп; w и h - границы отрисовки
        public bool sticky = false;
        abstract public void Resize();
        abstract public void OffsetXY(int _x, int _y);
        abstract public void SetColor(Color c);
        abstract public void Grow(int gr);
        abstract public void GrowN(int gr);
        abstract public void Rotate(int gr);
        abstract public void DrawObj(System.Drawing.Graphics e);
        abstract public void DrawRectangle(System.Drawing.Graphics e, Pen pen);
        abstract public bool Find(int _x, int _y);
        abstract public bool Find(Shape obj);
        abstract public Rectangle GetRectangle();  //границы фигуры для контроля выхода за пределы
        abstract public void Save(StreamWriter stream);
        abstract public void Load(StreamReader stream);
        abstract public string GetInfo();
    }
}

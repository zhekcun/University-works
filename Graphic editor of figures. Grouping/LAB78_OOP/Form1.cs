using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using LAB78_OOP.Mod;

namespace LAB78_OOP
{
    public partial class Form1 : Form
    {
        Storage<Shape> stor = new Storage<Shape>();
        Tree tree;

        Receiver receiver = new Receiver(); 

        public Form1()
        {

            InitializeComponent();
            (pictureBox1 as Control).KeyPress += new KeyPressEventHandler(PressEventHandler);
            tree = new Tree(stor, treeView1);
            stor.AddObserver(tree);
            treeView1.CheckBoxes = true;

        }


        public void PressEventHandler(object sender, KeyPressEventArgs e)
        {
            if (stor.Size() != 0)
            {
                if (e.KeyChar == 119) receiver.OffestXY(stor.get(), 0, -1);
                if (e.KeyChar == 115) receiver.OffestXY(stor.get(), 0, 1);
                if (e.KeyChar == 97) receiver.OffestXY(stor.get(), -1, 0);
                if (e.KeyChar == 100) receiver.OffestXY(stor.get(), 1, 0);
                if (e.KeyChar == 98)
                {
                    receiver.Grow(stor.get(),1);
                }
                if (e.KeyChar == 118)
                {
                    receiver.Grow(stor.get(), -1);
                }
                if (e.KeyChar == 110)
                {
                    if (stor.get() is Polygon) receiver.GrowN(stor.get(), 1);
                    if (stor.get() is Star) receiver.GrowN(stor.get(), 2);
                }
                if (e.KeyChar == 109)
                {
                    if (stor.get() is Polygon) receiver.GrowN(stor.get(), -1);
                    if (stor.get() is Star) receiver.GrowN(stor.get(), -2);
                }
                if (e.KeyChar == 46)
                {
                    receiver.Rotate(stor.get(), 2);
                }
                if (e.KeyChar == 44)
                {
                    receiver.Rotate(stor.get(),-2);
                }
                if (e.KeyChar == 52)
                {
                    receiver.Undo();
                    pictureBox1.Invalidate();
                }
                if (e.KeyChar == 122) stor.prevCur();
                if (e.KeyChar == 120) stor.nextCur();
                if (e.KeyChar == 99) receiver.Delete(stor.get(), stor);
                if (e.KeyChar == 107)
                {
                    if (colorDialog1.ShowDialog() == DialogResult.OK) receiver.SetColor(stor.get(), colorDialog1.Color);
                }
              
                    Random rnd = new Random();
                    int obj = rnd.Next(1, 3);
                    if (e.KeyChar==49)
                    {
                        int rad = rnd.Next(10, 100);
                    receiver.Add(new Circle(rnd.Next(4, pictureBox1.Width - 50), rnd.Next(4, pictureBox1.Height - 50), rad, Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width - 50, pictureBox1.Height - 50), stor);
                    //stor.add(new Circle(rnd.Next(4, pictureBox1.Width - 50), rnd.Next(4, pictureBox1.Height - 50), rad, Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width - 50, pictureBox1.Height - 50));
                    }
                if (e.KeyChar == 51)
                {
                        int rad = rnd.Next(10, 100);
                    receiver.Add(new Polygon(rnd.Next(4, pictureBox1.Width - 50), rnd.Next(4, pictureBox1.Height - 50), rad, rnd.Next(3, 9), Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width - 50, pictureBox1.Height - 50), stor);
                    //stor.add(new Polygon(rnd.Next(4, pictureBox1.Width - 50), rnd.Next(4, pictureBox1.Height - 50), rad, rnd.Next(3, 9), Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width-50, pictureBox1.Height - 50));
                        ((Polygon)stor.get()).Rotate(rnd.Next(0, 180));
                    }
                  if(e.KeyChar==50)
                    {
                        int rad = rnd.Next(10, 100);
                    receiver.Add(new Star(rnd.Next(4, pictureBox1.Width - 50), rnd.Next(4, pictureBox1.Height - 50), rad, rnd.Next(5, 9), Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width - 50, pictureBox1.Height - 50), stor);
                    //stor.add(new Star(rnd.Next(4, pictureBox1.Width - 50), rnd.Next(4, pictureBox1.Height - 50), rad, rnd.Next(5, 9), Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width - 50, pictureBox1.Height - 50));
                        ((Star)stor.get()).Rotate(rnd.Next(0, 180));
                    }
                
            }
            tree.Print();
            pictureBox1.Invalidate();
        }        

        //сохранение
        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream f = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                StreamWriter stream = new StreamWriter(f);
                stream.WriteLine(stor.Size());
                if (stor.Size() != 0)
                {
                    stor.toFirst();
                    for (int i = 0; i < stor.Size(); i++, stor.next())
                        stor.GetIterator().Save(stream);
                }
                stream.Close();
                f.Close();
            }
        }

        //группировка
        private void button4_Click(object sender, EventArgs e)
        {
            int f = 1;
            receiver.DelAllCom();
            if (stor.Size() != 0)
            {
                SGroup group = new SGroup(pictureBox1.Width, pictureBox1.Height);
                stor.toFirst();
                int cnt = 0;
                for (int i = 0; i < stor.Size(); i++, stor.next()) //считаем количество отмеченных элементов
                    if (stor.IsChecked() == true) cnt++;
                if (cnt == 0) f = 0;
                while (cnt != 0)
                {
                    if (stor.IsChecked() == true)
                    {
                        group.Add(stor.GetIterator());
                        stor.DelIterator();
                        cnt--;
                    }
                    if (stor.Size() != 0) stor.next();
                }
                if(f == 1) stor.add(group);
            }
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            receiver.DelAllCom(); // удаление комманд
            while (stor.Size() != 0) stor.del(); //receiver.Delete(stor.get(), stor);
            pictureBox1.Invalidate();
        }

     

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            bool isFinded = false;
            if (stor.Size() != 0)
            {
                stor.toFirst();
                for (int i = 0; i < stor.Size(); i++, stor.next())
                {
                    if (stor.GetIterator().Find(e.X, e.Y) == true && e.Button == MouseButtons.Left)
                    {
                        isFinded = true;
                        stor.SetCurPTR();
                        break;
                    }
                    if (stor.GetIterator().Find(e.X, e.Y) == true && e.Button == MouseButtons.Right)
                    {
                        isFinded = true;
                        stor.Check();
                        break;
                    }
                }
            }
            if (isFinded == false)
            {
                if (radioButton3.Checked == true)
                {
                    Random rnd = new Random();
                    int rad = rnd.Next(30, 60);
                    receiver.Add(new Circle(e.X, e.Y, rad, Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width, pictureBox1.Height), stor);
                    //stor.add(new Circle(e.X, e.Y, rad, Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width, pictureBox1.Height));
                }
                else
                if (radioButton2.Checked == true)
                {
                    Random rnd = new Random();
                    int rad = rnd.Next(30, 60);
                    receiver.Add(new Polygon(e.X, e.Y, rad, rnd.Next(3, 9), Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width, pictureBox1.Height), stor);
                    //stor.add(new Polygon(e.X, e.Y, rad, rnd.Next(3, 9), Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width, pictureBox1.Height));
                    ((Polygon)stor.get()).Rotate(rnd.Next(0, 180));
                }
                else
                if (radioButton1.Checked == true)
                {
            
                    Random rnd = new Random();
                    int n = rnd.Next(5, 27);
                    if (n % 2 == 0) n += 1;
                    int rad = rnd.Next(30, 60);
                    receiver.Add(new Star(e.X, e.Y, rad, n, Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width, pictureBox1.Height), stor);
                    //stor.add(new Star(e.X, e.Y, rad, n, Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), pictureBox1.Width, pictureBox1.Height));
                    ((Star)stor.get()).Rotate(rnd.Next(0, 180));
                }
                
            }           
           pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (stor.Size() != 0)
            {
                stor.toFirst();
                for (int i = 0; i < stor.Size(); i++, stor.next())
                {
                    stor.GetIterator().DrawObj(e.Graphics);
                    if (stor.IsChecked() == true) stor.GetIterator().DrawRectangle(e.Graphics, new Pen(Color.Gray, 2));
                }
                stor.get().DrawRectangle(e.Graphics, new Pen(Color.Red, 1));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            receiver.DelAllCom();
            if (stor.Size() != 0 && stor.get() is SGroup)
            {
                while (((SGroup)stor.get()).size() != 0)
                {
                    stor.addAfter(((SGroup)stor.get()).Out());
                    stor.prevCur();
                }
                receiver.Delete(stor.get(), stor);
            }
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream f = new FileStream(openFileDialog1.FileName, FileMode.Open);
                StreamReader stream = new StreamReader(f);
                int i = Convert.ToInt32(stream.ReadLine());
                Factory shapeFactory = new ShapeFactory();  //фабрика КОНКРЕТНЫХ объектов
                for (; i > 0; i--)
                {
                    string tmp = stream.ReadLine();
                    stor.add(shapeFactory.createShape(tmp));
                    stor.get().Load(stream);
                }
                stream.Close();
                f.Close();
            }
            pictureBox1.Invalidate();
            tree.Print();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {


            if ((e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse) && e.Node.Text != "Shapes")
            {

                TreeNode tmp = e.Node;

                while (tmp.Parent.Text != "Shapes") tmp = tmp.Parent;
                treeView1.SelectedNode = tmp;
                stor.toFirst();
                stor.SetCurPTR();
                for (int i = 0; i < tmp.Index; i++)
                {

                    //stor.nextCur();
                }
                stor.SetCurPTR();
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Focus();
        }

   

        private void Button6_Click(object sender, EventArgs e)
        {
                  if(stor.Size()!=0)
                receiver.Delete(stor.get(), stor);
            pictureBox1.Invalidate();
            
        }
        

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode tmp = e.Node;

            if (e.Button == MouseButtons.Right)
            {
                stor.toFirst();
                stor.SetCurPTR();

                for (int i = 0; i < tmp.Index; i++)
                {
                    stor.nextCur();
                }
                if (stor.Size() != 0 && stor.get() is SGroup)
                {
                    while (((SGroup)stor.get()).size() != 0)
                    {
                        stor.addAfter(((SGroup)stor.get()).Out());
                        stor.prevCur();
                    }
                    stor.del();

                }
            }
            if (e.Button == MouseButtons.Left)
            {

                treeView1.SelectedNode = tmp;
                stor.toFirst();

                for (int i = 0; i < treeView1.SelectedNode.Index; i++)
                {
                    stor.next();
                }
               stor.Check();
                           
            }            
                pictureBox1.Invalidate();
          

        }
            

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode tmp = e.Node;
            treeView1.SelectedNode = tmp;
           
                stor.toFirst();

                for (int i = 0; i < treeView1.SelectedNode.Index; i++)
                {
                    stor.next();
                }
                stor.SetCurPTR();
                receiver.Delete(stor.get(), stor);


            pictureBox1.Invalidate();

        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void Label13_Click(object sender, EventArgs e)
        {

        }

        private void Label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
       
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            receiver.Undo();
            pictureBox1.Invalidate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        //сгруппировать
        private void Uhe_Click(object sender, EventArgs e)
        {
            int f = 1;
            receiver.DelAllCom();
            if (stor.Size() != 0)
            {
                SGroup group = new SGroup(pictureBox1.Width, pictureBox1.Height);
                stor.toFirst();
                int cnt = 0;
                for (int i = 0; i < stor.Size(); i++, stor.next()) //считаем количество отмеченных элементов
                    if (stor.IsChecked() == true) cnt++;
                if (cnt == 0) f = 0;
                while (cnt != 0)
                {
                    if (stor.IsChecked() == true)
                    {
                        group.Add(stor.GetIterator());
                        stor.DelIterator();
                        cnt--;
                    }
                    if (stor.Size() != 0) stor.next();
                }
                if (f == 1) stor.add(group);
            }
            pictureBox1.Invalidate();
        }

        //разгруп
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            receiver.DelAllCom();
            if (stor.Size() != 0 && stor.get() is SGroup)
            {
                while (((SGroup)stor.get()).size() != 0)
                {
                    stor.addAfter(((SGroup)stor.get()).Out());
                    stor.prevCur();
                }
                receiver.Delete(stor.get(), stor);
            }
            pictureBox1.Invalidate();

        }
    }
}


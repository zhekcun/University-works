using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace LAB5_Algoritmh
{
    public partial class Form1 : Form
    {
        List<Point> Points = new List<Point>();
        int[,] Mat = new int[40, 40];
        int pressed = -1;
        bool[] used;
        string outp;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            if (me.Button == MouseButtons.Left)
                Points.Add(new Point(me.X - 10, me.Y - 10));
            else
            {
                int newPressed = -1;
                for (int i = 0; i < Points.Count; i++)
                    if (me.X > Points[i].X && me.Y > Points[i].Y && me.X < Points[i].X + 20 && me.Y < Points[i].Y + 20)
                        newPressed = i;
                if (newPressed >= 0)
                {
                    if (newPressed == pressed)
                        pressed = -1;
                    else
                    {
                        if (pressed == -1)
                            pressed = newPressed;
                        else
                        {
                            Mat[pressed, newPressed] = 1;
                            Mat[newPressed, pressed] = 1;
                            pressed = -1;
                        }
                    }
                }
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < Points.Count; ++i)
            {
                Pen pp = new Pen(Color.Red, 5);
                //pp.Color = Color.Red;
                //pp.Width = 5;
                var p = Points[i];
                for (int j = i; j < Points.Count; ++j)
                {
                    if (Mat[i, j] == 1)
                        e.Graphics.DrawLine(Pens.Black, Points[i].X + 10, Points[i].Y + 10, Points[j].X + 10, Points[j].Y + 10);
                    if (Mat[i, j] == 2)
                        e.Graphics.DrawLine(pp, Points[i].X + 10, Points[i].Y + 10, Points[j].X + 10, Points[j].Y + 10);
                }
                e.Graphics.FillEllipse(i == pressed ? Brushes.DarkCyan : Brushes.BurlyWood, p.X, p.Y, 30, 30);
                e.Graphics.DrawEllipse(Pens.Black, p.X, p.Y, 30, 30);
                e.Graphics.DrawString((i + 1).ToString(), new Font(FontFamily.GenericSansSerif, 10f), Brushes.White, p.X + 4, p.Y);
            }
            pictureBox2.Refresh();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                e.Graphics.DrawString((i + 1).ToString(), new Font("Arial", 10F, FontStyle.Bold), Brushes.Black, i * 20 + 20, 0);
                e.Graphics.DrawString((i + 1).ToString(), new Font("Arial", 10F, FontStyle.Bold), Brushes.Black, 0, i * 20 + 20);
                for (int j = 0; j < Points.Count; j++)
                {
                    if (Mat[i, j] == 0)
                        e.Graphics.DrawString(Mat[i, j].ToString(), new Font("Arial", 10F), Brushes.Black, i * 20 + 20, j * 20 + 20);
                    else e.Graphics.DrawString(Mat[i, j].ToString(), new Font("Arial", 10F, FontStyle.Bold), Brushes.Black, i * 20 + 20, j * 20 + 20);
                }
            }
        }

        private void but_clear_Click(object sender, EventArgs e)
        {
            Points = new List<Point>();
            Mat = new int[40, 40];
            textBox1.Text = "";
            pictureBox1.Refresh();
            
        }

        private void dfs(int k)
        {
            used[k] = true;
            for (int i = 0; i < Points.Count; i++)
                for (int j= 0; j< Points.Count; j++)
                    if (Mat[i,j] == 1 && used[i] == false) dfs(i);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[,] matr = new int[40, 40];
            for (int i = 0; i < Points.Count; i++)
                for (int j = 0; j < Points.Count; j++)
                    matr[i, j] = Mat[i, j];
            if (pressed >= 0)
            {
                if (Points.Count > 0)
                {
                    for (int i = 0; i < Points.Count; i++)
                    {
                        int deg = 0;
                        for (int j = 0; j < Points.Count; j++)
                        {
                            if (matr[i, j] != 0) deg++;
                        }
                        if (deg % 2 != 0)
                        {
                            MessageBox.Show("Нечетная вершина №  " + (i + 1), "Ошибка");
                            return;
                        }
                    }
                }

               used = null;
                used = new bool[Points.Count];
                dfs(0);
              
                for (int i = 0; i < Points.Count; i++)
                {
                    if(used[i]==false)
                    {
                        MessageBox.Show("Граф несвязный", "Ошибка");
                        return;
                    }
                }
                Stack<int> st = new Stack<int>(); 
                List<int> outv = new List<int>(); 
          
                st.Push(pressed);
                while (st.Count > 0)
                {
                    int v = st.Peek();
                    int i = 0;
                    for (; i < Points.Count; i++)
                        if (matr[v, i] != 0) break;
                    if (i < Points.Count)
                    {
                        st.Push(i);
                        matr[v, i] = 0;
                        matr[i, v] = 0;

                        // выделение ребра в красный
                       // Mat[v, i] = 2;
                       // Mat[i, v] = 2;
                       // pictureBox1.Refresh();
                       // Thread.Sleep(1000);
                    }
                    else
                    {
                       outv.Add(v);
                        st.Pop();
                        if(st.Count > 0)
                        {
                            // откат выделения ребра
                           // Mat[v, st.Peek()] = 1;
                           // Mat[st.Peek(), v] = 1;
                        }

                    }
                }
              
                textBox1.Text = "";
                for (int i = outv.Count - 1; i >= 0; i--)
                {
                    if (i != 0)
                    {
                        Mat[outv[i] , outv[i-1] ] = 2;
                        Mat[outv[i-1], outv[i] ] = 2;
                        pictureBox1.Refresh();
                        Thread.Sleep(1000);
                    }
                    else
                    {
                       
                    }

                    if (i != 0) 
                        textBox1.Text += (outv[i] + 1) + "->";
                    
                    else textBox1.Text += (outv[i] + 1) + ";";
                        }

            }
            else MessageBox.Show("Не выбрана начальная вершина", "Ошибка");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pressed = -1;           
            for (int i = 0; i < Points.Count; i++)
                for (int j = 0; j < Points.Count; j++)
                    if (Mat[i, j] == 2) Mat[i, j] = 1;


            textBox1.Text = "";
            pictureBox1.Refresh();
        }
    }
}

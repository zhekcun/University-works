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

namespace LAB4_Algoritmh
{
    public partial class Form1 : Form
    {
        List<Point> Points = new List<Point>();
        int[,] Mat = new int[40, 40];
        int[,] Mat2 = new int[40, 40];
        int pressed = -1;
        List<int> allocated = new List<int>();
        int[] col = new int[40];
        int[] a = new int[20];
        int[] b = new int[20];
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Points = new List<Point>();
            Mat = new int[40, 40];
             pressed = -1;
             col= new int[40];            
            textBox1.Text = "";
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < Points.Count; ++i)
            {
                var p = Points[i];
                Pen pp = new Pen(Color.Red, 5);
                Pen ppp = new Pen(Color.Black, 5);
                for (int j = 0; j < Points.Count; ++j)
                {                    
                    if (Mat[i, j] != 0)
                    {
                        e.Graphics.DrawLine(Pens.Black, Points[i].X + 10, Points[i].Y + 10, Points[j].X + 10, Points[j].Y + 10);
                        e.Graphics.DrawLine(ppp, (Points[i].X  + Points[j].X + 20)/2, (Points[i].Y + Points[j].Y+20)/2, Points[j].X + 10, Points[j].Y + 10);
                    }
                }
                if (col[i] == 0)
                {
                    if(allocated.Any(x => x == i))                        
                        e.Graphics.FillEllipse(i == pressed ? Brushes.DarkRed : Brushes.IndianRed, p.X, p.Y, 30, 30);
                    else e.Graphics.FillEllipse(i == pressed ? Brushes.DarkCyan : Brushes.BurlyWood, p.X, p.Y, 30, 30);
                }              

                if (col[i]==1)   e.Graphics.FillEllipse( Brushes.MediumOrchid , p.X, p.Y, 30, 30);
   
                if (col[i] == 2) e.Graphics.FillEllipse(Brushes.Coral, p.X, p.Y, 30, 30);    

                e.Graphics.DrawEllipse(Pens.Black, p.X, p.Y, 30, 30);
                e.Graphics.DrawString((i + 1).ToString(), new Font(FontFamily.GenericSansSerif, 10f), Brushes.White, p.X + 4, p.Y);
            }
            pictureBox2.Refresh();
        }

        private void but__Click(object sender, EventArgs e)
        {
            if (pressed >= 0)
            {
                List<int> Result = new List<int>();
                Queue<int> Queue = new Queue<int>();
               
                for (int i = 0; i < Points.Count; i++) 
                Queue.Enqueue(pressed); //Добавить эл в конец очереди
                int z = pressed;
                col[z] = 1;
                a[z] = pressed;
                while (Queue.Count != 0)
                {
                    if (!Result.Contains(Queue.Peek()))
                    {
                        Result.Add(Queue.Peek());
                        for (int i = 0; i < Points.Count; ++i)
                            if (Mat[i, Queue.Peek()] == 1)
                                Queue.Enqueue(i);
                         
                    }
                    Queue.Dequeue();
                  
                }
                for (int i = 0; i < Result.Count; i++)
                    textBox1.Text += (Result[i] + 1) + " ";
                pictureBox1.Refresh();
              
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            if (me.Button == MouseButtons.Left)
            {
                if (Points.Count > 0)
                {
                    int newPressedLeft = -1;
                    for (int i = 0; i < Points.Count; i++)
                        if (me.X > Points[i].X && me.Y > Points[i].Y && me.X < Points[i].X + 20 && me.Y < Points[i].Y + 20)// если клик в кург
                        {
                            newPressedLeft = i; // то запоминаем в какой   
                        }
                    if (newPressedLeft >= 0)// если попали то
                    {  
                        
                        if (allocated.Any(x => x == newPressedLeft)) // если дважды то
                        {
                            allocated.Remove(newPressedLeft); // удаляем
                        }
                        else allocated.Add(newPressedLeft);   // иначе добавляем                                              
                    }
                    else Points.Add(new Point(me.X - 10, me.Y - 10));
                }
                else Points.Add(new Point(me.X - 10, me.Y - 10));
            }
            else
            {
                int newPressed = -1;
                for (int i = 0; i < Points.Count; i++)
                    if (me.X > Points[i].X && me.Y > Points[i].Y && me.X < Points[i].X + 20 && me.Y < Points[i].Y + 20)// если клик в кург
                        newPressed = i; // то запоминаем в какой   
                if (newPressed >= 0)// если попали то
                {
                    if (newPressed == pressed) // если дважды попали, то снимаем выделение
                        pressed = -1;
                    else
                    {
                        if (pressed == -1)// если первый раз попали, то выдеяем его
                            pressed = newPressed;
                        else // иначе связываем их
                        {
                            Mat[pressed, newPressed] = (int)numericUpDown2.Value > 0 ? (int)numericUpDown2.Value : 1;
                            // Mat[newPressed, pressed] = 1;
                            pressed = -1;
                        }
                    }
                }
            }
            pictureBox1.Refresh();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            for(int i=0;i<Points.Count;i++)
            {
                e.Graphics.DrawString((i+1).ToString(), new Font("Arial", 10F, FontStyle.Bold), Brushes.Black, i * 20+20 ,0);
                e.Graphics.DrawString((i+1).ToString(), new Font("Arial", 10F, FontStyle.Bold), Brushes.Black, 0, i * 20+20 );
                for (int j = 0; j < Points.Count; j++)
                {
                    if(Mat[i,j]==0)
                        e.Graphics.DrawString(Mat[j, i].ToString(), new Font("Arial", 10F),Brushes.Black,i*20+20,j*20+20);
                    else e.Graphics.DrawString(Mat[j, i].ToString(), new Font("Arial", 10F, FontStyle.Bold), Brushes.Black, i * 20 + 20, j * 20 + 20);
                }
            }
        }

        
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void But__Click_1(object sender, EventArgs e)
        {
            int last = (int)numericUpDown1.Value - 1;
            if ((last >= Points.Count) || (last < 0))  // если нет вершины
            {
                MessageBox.Show("Нет вершины " + (last + 1));
            }
            else 
            if (pressed >= 0) // если есть начальная вершина
            {
                int k = pressed;
                
                
                int[,] MatD = new int[40, 40];
                int[] A = new int[Points.Count]; // метки
                int[] B = new int[Points.Count]; // кратчайшие расстояния 
                int[] C = new int[Points.Count]; // вершины
                int E = 50000;

                for (int i = 0; i < Points.Count; i++)  //инициализация
                {
                    A[i] = 0;
                    B[i] = E;
                    C[i] = pressed;
                    for (int j = 0; j < Points.Count; j++)
                    {
                        if (i == j)
                        {
                            MatD[i, j] = E;
                        }
                        else
                        if (Mat[i, j] == 0)
                        {
                            MatD[i, j] = E;
                        }
                        else MatD[i, j] = Mat[i, j];
                    }
                }                

                foreach (var item in allocated)  // убираем заданное множество вершин
                {
                    for (int i = 0; i < Points.Count; i++)
                    {
                        if (Mat[i,item] != 0)  //если есть путь из вершины в allocated
                        {                            
                            MatD[i, item] = E;  // убираем путь
                        }
                    }
                }
                
                A[pressed] = 1; C[pressed] = 0; B[pressed] = 0;
                int i_min = 0, min = E, f=0;

                while (A[last] == 0) // пока последняя вершина не помечена
                {
                    i_min = 0; min = E; f = 0;
                    for (int i = 0; i < Points.Count; i++)
                    {
                        if (A[i] == 0) // если помечена
                        {
                            if (B[i] > B[k] + MatD[k, i]) // если путь через вершину k меньше
                            {
                                B[i] = B[k] + MatD[k, i]; // то изменяем путь на путь через вершину k
                                C[i] = k;                 // запоминаем вершину
                            }
                        }
                    }

                    for (int i = 0; i < Points.Count; i++)
                    {
                        if (A[i] == 0) // если помечена
                        {
                            if (B[i] < min)  // находим min
                            {
                                min = B[i];
                                i_min = i;  // запоминаем вершину min
                                f = 1;     
                            }
                        }
                    }
                    if (f == 0) break;  // если нет пути в last
                    A[i_min] = 1; // помечаем вершину min
                    k = i_min;    // дальше рассматриваем вершину min
                }                
                k = last;
                if (f != 0)
                {
                    textBox1.Text = " (" + B[last] + ") "; // вывод стоимости пути

                    // вывод пути
                    while (k != pressed)    
                    {
                        textBox1.Text = "-" + (k + 1) + textBox1.Text;
                        k = C[k];
                    }
                    textBox1.Text = "" + (pressed + 1) + textBox1.Text;
                }
                else MessageBox.Show("До " + (last + 1) + " нет пути");

                pictureBox1.Refresh();


            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pressed = -1;
            for (int i = 0; i < Points.Count; i++)
                col[i] = 0;
            allocated.Clear();


            textBox1.Text = "";
            pictureBox1.Refresh();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void заданиеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Задана система односторонних дорог. Найти путь, соединяющий города А и В не проходящий через заданное множество городов. ");
        }
        
    }
}

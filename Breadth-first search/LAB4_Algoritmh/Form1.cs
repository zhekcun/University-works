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
        int pressed = -1;
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
                for (int j = i; j < Points.Count; ++j)
                {
                    if (Mat[i, j] == 2)
                        e.Graphics.DrawLine(pp, Points[i].X + 10, Points[i].Y + 10, Points[j].X + 10, Points[j].Y + 10);
                    if (Mat[i, j] == 1)
                        e.Graphics.DrawLine(Pens.Black, Points[i].X + 10, Points[i].Y + 10, Points[j].X + 10, Points[j].Y + 10);
                }
                if (col[i] == 0)
           
                    e.Graphics.FillEllipse(i == pressed ? Brushes.DarkCyan : Brushes.BurlyWood, p.X, p.Y, 30, 30);
           
              

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
                Points.Add(new Point(me.X - 10, me.Y - 10));
            else
            {
                int newPressed = -1;
                for (int i = 0; i < Points.Count; i++) // если клик в кург
                    if (me.X > Points[i].X && me.Y > Points[i].Y && me.X < Points[i].X + 20 && me.Y < Points[i].Y + 20)
                        newPressed = i; // если попали, то запоминаем в какой   
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
                            Mat[pressed, newPressed] = 1;
                            Mat[newPressed, pressed] = 1;
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
                    e.Graphics.DrawString(Mat[i, j].ToString(), new Font("Arial", 10F),Brushes.Black,i*20+20,j*20+20);
                    else e.Graphics.DrawString(Mat[i, j].ToString(), new Font("Arial", 10F, FontStyle.Bold), Brushes.Black, i * 20 + 20, j * 20 + 20);
                }
            }
        }

        
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void But__Click_1(object sender, EventArgs e)
        {
            if (pressed >= 0)
            {
                List<int> Result = new List<int>();
                Queue<int> Queue = new Queue<int>();

                for (int i = 0; i < Points.Count; i++) col[i] = 0;
                Queue.Enqueue(pressed);
                int z = pressed;
                col[z] = 1;
                a[z] = pressed;
                int mes = 0;
                while (Queue.Count != 0) // Пока очередь не пуста
                {
                    if (!Result.Contains(Queue.Peek()))                 
                    {
                        foreach (var item in Result)
                        {
                            if (Mat[item, Queue.Peek()] == 1)
                            {
                                Mat[item, Queue.Peek()] = 2;
                                Mat[Queue.Peek(), item] = 2;
                                pictureBox1.Refresh();
                                Thread.Sleep(1000);
                                break;
                            }
                        }
                        Result.Add(Queue.Peek()); // Вершина заносится в очередь
                        for (int i = 0; i < Points.Count; ++i)
                            if (Mat[i, Queue.Peek()] == 1)
                            {
                                if (col[i] == 0) // Если выделена красным
                                {
                                    if (col[Queue.Peek()] == 1) col[i] = 2;  // Красим в зеленый если вершина выделена                           
                                    else col[i] = 1; // Красим в золотой, если нет
                                }
                                
                                Queue.Enqueue(i); // В конец очереди
                                pictureBox1.Refresh();
                                Thread.Sleep(1000);
                            }
                    }                  
                    Queue.Dequeue(); // Возвращает первый элемент очереди и удаляет его                  

                }
                                
                textBox1.Text = "";
                for (int i = 0; i < Result.Count; i++)
                    if (i != Result.Count - 1) textBox1.Text += (Result[i] + 1) + " - ";
                else textBox1.Text += (Result[i] + 1);

                pictureBox1.Refresh();

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pressed = -1;
            for (int i = 0; i < Points.Count; i++)
                col[i] = 0;
            for (int i = 0; i < Points.Count; i++)
                for (int j = 0; j < Points.Count; j++)
                    if (Mat[i, j] == 2) Mat[i, j] = 1;

            
            textBox1.Text = "";
            pictureBox1.Refresh();
        }
    }
}

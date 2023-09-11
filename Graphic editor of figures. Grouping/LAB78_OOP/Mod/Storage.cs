using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB78_OOP.Mod
{
    public class Storage<T> : Observed
    {
        public class list
        {
            public T data { get; set; }
            public list right { get; set; }
            public list left { get; set; }
            public bool isChecked = false;
        };
        private list first;
        private list last;
        private list current;
        private list iterator;

        private int rate;
        public Storage()
        {
            first = null;
            rate = 0;
        }
        public void add(T obj)
        {
            list tmp = new list();
            tmp.data = obj;
            //if (first != null)
            //{
            //    tmp.left = last;
            //    last.right = tmp;
            //    last = tmp;
            //}
            //else
            //{
            //    first = tmp;
            //    last = first;
            //    current = first;
            //}
            //last.right = first;  
            //first.left = last;
            if (first != null)
            {
                tmp.left = last;
                last.right = tmp;
                last = tmp;
            }
            else
            {
                first = tmp;
                last = first;
                current = first;
            }

            last.right = first;            
            first.left = last;
            current = tmp;


            rate++;
            Notify(); // если добавили объект, то сообщаем это
        }
        public void addBefore(T obj)
        {
            list tmp = new list();
            tmp.data = obj;
            if (first != null)
            {
                tmp.left = (current.left);
                (current.left).right = tmp;
                current.left = tmp;
                tmp.right = current;
                if (current == first) first = current.left;
            }
            else
            {
                first = tmp;
                last = first;
                current = first;
                first.right = first;
                first.left = first;
            }
            current = tmp;
            rate++;
        }
        public void addAfter(T obj)
        {
            list tmp = new list();
            tmp.data = obj;
            if (first != null)
            {
                tmp.left = current;
                tmp.right = current.right;
                (current.right).left = tmp;
                current.right = tmp;
                if (current == last) last = current.right;
            }
            else
            {
                first = tmp;
                last = first;
                current = first;
                first.right = first;
                first.left = first;
            }
            current = tmp;
            rate++;
        }
        public void toFirst()
        {
            iterator = first;
        }
        public void toLast()
        {
            iterator = last;
        }
        public void next()
        {
            iterator = iterator.right;
        }
        public void prev()
        {
            iterator = iterator.left;
        }
        public void nextCur()
        {
            current = current.right;
            Notify();
        }
        public void prevCur()
        {
            current = current.left;
            Notify();
        }
        public void del()
        {
            if (rate == 1)
            {
                first = null;
                last = null;
                current = null;
            }
            else
            {
                (current.left).right = current.right;
                (current.right).left = current.left;
                list tmp = current;
                if (current == last)
                {
                    current = current.left;
                    last = current;
                }
                else
                {
                    if (current == first) first = current.right;
                    current = current.right;
                }
            }
            rate--;
            Notify();
        }
        public void DelIterator()
        {
            if (rate == 1)
            {
                first = null;
                last = null;
                iterator = null;
            }
            else
            {
                (iterator.left).right = iterator.right;
                (iterator.right).left = iterator.left;
                if (iterator == last)
                {
                    iterator = iterator.left;
                    last = iterator;
                }
                else
                {
                    if (iterator == first) first = iterator.right;
                    iterator = iterator.left;
                }
            }
            rate--;
            Notify();
        }
        public int Size()
        {
            return rate;
        }
        public list GetIteratorPTR()
        {
            return iterator;
        }
        public list GetCurPTR()
        {
            return current;
        }
        public void SetCurPTR()
        {
            current = iterator;
            Notify();
        }
        public bool IsChecked()
        {
            if (iterator.isChecked == true) return true; else return false;
        }
        public void Check()
        {
            iterator.isChecked = !iterator.isChecked;
            Notify();
        }
        public T GetIterator()
        {
            return (iterator.data);
        }
        public T get()
        {
            return (current.data);
        }
    }
}

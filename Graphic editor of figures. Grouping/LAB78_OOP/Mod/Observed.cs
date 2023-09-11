using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB78_OOP.Mod
{
    public class Observed
    {
        private List<Observer> observers;
        public Observed()
        {
            observers = new List<Observer>();
        }
        public void AddObserver(Observer o)
        {
            observers.Add(o);
        }
        public void Notify()
        {
            foreach (Observer observer in observers) observer.SubjectChanged();
        }
    }
}

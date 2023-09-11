using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB78_OOP.Mod
{
    public abstract class Factory
    {
        public abstract Shape createShape(string name);
    }
}

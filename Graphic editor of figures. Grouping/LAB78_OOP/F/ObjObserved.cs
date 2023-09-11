using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB78_OOP.Mod
{
    public class ObjObserved
    {
        public Storage<Shape> storage;
        public void AddStorage(Storage<Shape> sto)
        {
            storage = sto;
        }
    }
}

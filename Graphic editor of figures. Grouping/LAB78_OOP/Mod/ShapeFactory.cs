﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB78_OOP.Mod
{
    public class ShapeFactory : Factory
    {
        public override Shape createShape(string name)
        {
            Shape shape;
            switch (name)
            {
                case "Circle":
                    shape = new Circle();
                    break;
                case "Group":
                    shape = new SGroup();
                    break;
                case "Polygon":
                    shape = new Polygon();
                    break;
                case "Star":
                    shape = new Star();
                    break;
                default:
                    shape = null;
                    break;
            }
            return shape;
        }
    }
}

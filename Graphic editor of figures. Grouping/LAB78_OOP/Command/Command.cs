using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB78_OOP.Mod
{
    public abstract class Command
    {
        protected Shape _shape;
        public Command(Shape shape)
        {
            _shape = shape;
        }
        public virtual void Execute() { }
        public virtual void Undo() { }
    }

    public class RotateCommand : Command
    {
        private int _gr;
        public RotateCommand(Shape shape, int gr) : base(shape)
        {
            _gr = gr;
        }
        public override void Execute()
        {
            _shape.Rotate(_gr);
        }
        public override void Undo()
        {
            _shape.Rotate(-_gr);
        }
    }
    public class GrowCommand : Command
    {
        private int _gr;
        public GrowCommand(Shape shape, int gr) : base(shape)
        {
            _gr = gr;
        }
        public override void Execute()
        {
            _shape.Grow(_gr);
        }
        public override void Undo()
        {
            _shape.Grow(-_gr);
        }
    }
    public class OffsetCommand : Command
    {
        private int _x;
        private int _y;
        public OffsetCommand(Shape shape, int x, int y) : base(shape)
        {
            _x = x;
            _y = y;
        }
        public override void Execute()
        {
            _shape.OffsetXY(_x, _y);
        }
        public override void Undo()
        {
            _shape.OffsetXY(-_x, -_y);
        }
    }
    public class SetColorCommand : Command
    {
        private Color _c;
        private Color _LastC;
        public SetColorCommand(Shape shape, Color c) : base(shape)
        {
            _c = c;
        }
        public override void Execute()
        {
            _LastC = _shape.col;
            _shape.SetColor(_c);
        }
        public override void Undo()
        {
            _shape.SetColor(_LastC);
        }
    }
    public class GrowNCommand : Command
    {
        private int _gr;
        public GrowNCommand(Shape shape, int gr) : base(shape)
        {
            _gr = gr;
        }
        public override void Execute()
        {
            _shape.GrowN(_gr);
        }
        public override void Undo()
        {
            _shape.GrowN(-_gr);
        }
    }
    public class DeleteCommand : Command
    {
        private Storage<Shape> _storage;
        public DeleteCommand(Shape shape, Storage<Shape> storage) : base(shape)
        {
            _storage = storage;
        }
        public override void Execute()
        {
            _storage.del();
        }
        public override void Undo()
        {
            _storage.add(_shape);
        }
    }
    public class AddCommand : Command
    {
        private Storage<Shape> _storage;
        public AddCommand(Shape shape, Storage<Shape> storage) : base(shape)
        {
            _storage = storage;
        }
        public override void Execute()
        {
            _storage.add(_shape);
            
        }
        public override void Undo()
        {
            _storage.del();
        }
    }
}

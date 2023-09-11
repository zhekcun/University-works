using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB78_OOP.Mod
{
    public class Receiver
    {
        private Stack<Command> _commands = new Stack<Command>();
        public void DelAllCom()
        {
            _commands.Clear();
        }
        public void Rotate(Shape shape, int gr)
        {
            var command = new RotateCommand(shape, gr);
            command.Execute();
            _commands.Push(command);
        }
        public void Grow(Shape shape, int gr)
        {
            var command = new GrowCommand(shape, gr);
            command.Execute();
            _commands.Push(command);
        }
        public void GrowN(Shape shape, int gr)
        {
            var command = new GrowNCommand(shape, gr);
            command.Execute();
            _commands.Push(command);
        }
        public void OffestXY(Shape shape, int x, int y)
        {
            var command = new OffsetCommand(shape, x, y);
            command.Execute();
            _commands.Push(command);            
        }
        public void SetColor(Shape shape, Color c)
        {
            var command = new SetColorCommand(shape, c);
            command.Execute();
            _commands.Push(command);
        }
        public void Delete(Shape shape, Storage<Shape> storage)
        {
            var command = new DeleteCommand(shape, storage);
            command.Execute();
            _commands.Push(command);
        }
        public void Add(Shape shape, Storage<Shape> storage)
        {
            var command = new AddCommand(shape, storage);
            command.Execute();
            _commands.Push(command);
        }
        public void Undo()
        {
            if(_commands.Any())
            {
                var command = _commands.Pop();
                command.Undo();
            }
        }
    }
}

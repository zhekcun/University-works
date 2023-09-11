using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB78_OOP.Mod
{
    class Tree : Observer
    {

        private Storage<Shape> sto;
        private TreeView tree;
        public Tree(Storage<Shape> sto, TreeView tree)
        {
            this.sto = sto;
            this.tree = tree;
        }

        public void Print()
        {
            tree.Nodes.Clear();
            if (sto.Size() != 0)
            {
                int SelectedIndex = 0;
                TreeNode start = new TreeNode("Shapes");
                sto.toFirst();
                for (int i = 0; i < sto.Size(); i++, sto.next())
                {
                    if (sto.GetCurPTR() == sto.GetIteratorPTR()) SelectedIndex = i;
                    PrintNode(start, sto.GetIterator());
                }
                tree.Nodes.Add(start);               

                for (int i = 0; i < sto.Size(); i++)
                {                   
                   // stor.next(); 
                    tree.SelectedNode = tree.Nodes[0].Nodes[i];
                    
                    if (sto.IsChecked() == true && SelectedIndex == i)
                        tree.SelectedNode.ForeColor = Color.Violet;
                    else if (SelectedIndex == i)
                        tree.SelectedNode.ForeColor = Color.Blue;
                    else if (sto.IsChecked() == true)
                        tree.SelectedNode.ForeColor = Color.Red;
                    else tree.SelectedNode.ForeColor = Color.Black;
                    sto.next();
                }
            }
            tree.ExpandAll();

        }

        private void PrintNode(TreeNode node, Shape shape)
        {
            if (shape is SGroup)
            {
                TreeNode tn = new TreeNode(shape.GetInfo());
                if (((SGroup)shape).sto.Size() != 0)
                {
                    ((SGroup)shape).sto.toFirst();
                    for (int i = 0; i < ((SGroup)shape).sto.Size(); i++, ((SGroup)shape).sto.next())
                        PrintNode(tn, ((SGroup)shape).sto.GetIterator());
                }
                node.Nodes.Add(tn);
            }
            else
            {

                node.Nodes.Add(shape.GetInfo());
            }
        }

        public override void SubjectChanged()
        {
            Print();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreeImplementation
{
    public class Node
    {
        public Node Parent = null;
        public List<Node> children = new List<Node>();

        public string Name;
        public float Depth;

        public void displayParent()
        {
            if(Parent != null)
            {
                Console.WriteLine("This Node's name is " + this.Name + " and its parent is " + this.Parent.Name);
                this.Parent.displayParent();
            }
            else
            {
                Console.WriteLine("This Node's name is " + this.Name + " and it has no parent.");
            }
        }
    }
}

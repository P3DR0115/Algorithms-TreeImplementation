using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreePart2
{
    public class Node : INode
    {
        string _Id;
        string _Content;
        float _Depth;
        List<Node> _Children;
        Node _Parent;
        bool _IsReady;

        public string Id { get => this._Id; set => this._Id = value; }
        public string Content { get => this._Content; set => this._Content = value; }
        public float Depth { get => this._Depth; set => this._Depth = value; }
        public List<Node> Children { get => this._Children; set => this._Children = value; }
        public Node Parent { get => this._Parent; set => this._Parent = value; }
        public bool IsReady { get =>  this._IsReady; set => this._IsReady = value; }

        public Node()
        {
            this.Id = "";
            this.Content = "";
            this.Depth = 0f;
            this.Children = new List<Node>();
            this.Parent = null;
            this.IsReady = false;
        }

        public void displayAncestors()
        {
            if (Parent != null)
            {
                Console.WriteLine("Node's name is " + this.Content + " and its parent is " + this.Parent.Content);
                this.Parent.displayAncestors();
            }
            else
            {
                Console.WriteLine("Node's name is " + this.Content + " and it has no parent.");
            }
        }

        public void displayDescendants()
        {
            if(Children.Count > 0)
            {
                Console.Write("Node's name is " + this.Content + "and its chilren are ");

                foreach(Node n in Children)
                {
                    Console.Write(n.Content + "   ");
                    if(n.Children.Count > 0)
                    {
                        n.displayDescendants();
                    }
                }
            }
            else
            {
                Console.WriteLine("Node has no descendants");
            }
        }

    }
}

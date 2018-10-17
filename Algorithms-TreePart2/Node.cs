using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreePart2
{
    public class Node : INode
    {
        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Depth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Node> children { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Node Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsReady { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Get(string Id, bool shouldGetBranch)
        {
            if(!shouldGetBranch)
                return this.Id;
            else
            {
                try
                {
                    Get(Parent.Id, true);
                }
                catch(Exception e)
                {
                    // Parent doesn't exist. Continue.
                }
                
                return this.Id;
            }
        }

        public void AddNode(Node Child, string ParentId)
        {
            
        }

        public void MoveNode(string Id, string ParentId)
        {

        }

        public void DeleteNode(string Id)
        {
            
        }

        public void FindNodeId(string Id)
        {

        }

        public void FindNodeContent(string Content)
        {

        }

    }
}

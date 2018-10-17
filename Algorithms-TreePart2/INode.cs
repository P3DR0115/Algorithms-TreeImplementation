using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreePart2
{
    public interface INode
    {
        string Id { get; set; }
        string Content { get; set; }
        int Depth { get; set; }
        List<Node> children { get; set; }
        Node Parent { get; set; }
        bool IsReady { get; set; }

        string Get(string Id, bool shouldGetBranch);
        void AddNode(Node Child, string ParentId);
        void DeleteNode(string Id);
        void MoveNode(string Id, string ParentId);
        void FindNodeId(string Id);
        void FindNodeContent(string Content);
    }
}

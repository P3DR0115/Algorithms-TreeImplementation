using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreeImplementation
{
    public interface INode
    {
        string Id { get; set; }
        string Content { get; set; }
        bool IsReady { get; set; }

        void Get(string Id, bool shouldGetBranch);
        void AddNode();
        void DeleteNode(string Id);
        void MoveNode(string Id, string ParentId);
        void FindNodeId(string Id);
        void FindNodeContent(string Content);
    }
}

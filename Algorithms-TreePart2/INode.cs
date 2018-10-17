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
        float Depth { get; set; }
        List<Node> children { get; set; }
        Node Parent { get; set; }
        bool IsReady { get; set; }
        
    }
}

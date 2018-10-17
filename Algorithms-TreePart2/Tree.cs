using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreePart2
{
    public class Tree
    {
        List<Node> Root = new List<Node>();
        
        public void Get(string Id, bool shouldGetBranch)
        {
            Node searchResult = FindNodeId(Root, Id);
            if (!shouldGetBranch)
            {
                Console.WriteLine(searchResult.Content);
            }
            else
            {
                try
                {
                    // Show all the parents

                    //Console.WriteLine(searchResult.Content);
                }
                catch (Exception e)
                {
                    // Parent doesn't exist.
                }

            }
        }

        public void AddNode(Node Child, string ParentId)
        {
            Node searchResult = new Node();

            searchResult = FindNodeId(Root, ParentId);
            foreach (Node n in Root)
            {
                if(ParentId == "Command: None")
                {
                    this.Root.Add(Child);
                }
                else if (ParentId == n.Id)
                {
                    n.children.Add(Child);
                }
                else
                {
                    try
                    {

                    }
                    catch (Exception e)
                    {

                    } // catch
                } // else
            } // foreach
        } // method

        public void MoveNode(string Id, string ParentId)
        {

        }

        public void DeleteNode(string Id)
        {

        }

        public Node FindNodeId(List<Node> Root, string Id)
        {
            Node searchResult = new Node
            {
                Id = "DELETETHIS" // In case the search fails, easily find node for extermination >:3
            };

            foreach (Node n in Root)
            {
                if (Id == n.Id)
                {
                    searchResult = n;
                }
                else
                {
                    // Check n's children
                    try
                    {
                        searchResult = FindNodeId(n.children, Id);
                    }
                    catch (Exception e)
                    {
                        // Node n has no children
                    }
                }
            }

            return searchResult;
        }

        public void FindNodeContent(string Content)
        {

        }
    }
}

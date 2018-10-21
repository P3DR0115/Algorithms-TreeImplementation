﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreePart2
{
    public class Tree
    {
        public List<Node> Root = new List<Node>();
        
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

            // Search to see if parent exists 
            Node searchResult = FindNodeId(Root, ParentId);
            if(searchResult != null)
            {
                foreach (Node n in Root)
                {
                    if (ParentId == "None")
                    {
                        // Child will have no parent
                        this.Root.Add(Child);
                    }
                    else if (ParentId == n.Id)
                    {
                        n.Children.Add(Child);
                    }
                    else
                    {
                        try
                        {
                            foreach(Node o in n.Children)
                            {
                                if (ParentId == n.Id)
                                {
                                    n.Children.Add(Child);
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        } // catch
                    } // else
                } // foreach
            } // if node exists
            
        } // method

        public void MoveNode(string Id, string ParentId)
        {

        }

        public void DeleteNode(string Id)
        {

        }

        public Node FindNodeId(List<Node> query, string Id)
        {
            Node searchResult = new Node();
            foreach (Node n in query)
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
                        searchResult = FindNodeId(n.Children, Id);
                    }
                    catch (Exception e)
                    {
                        // Node n has no children
                        searchResult = null;
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
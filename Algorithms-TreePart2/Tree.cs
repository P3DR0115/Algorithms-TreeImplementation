using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreePart2
{
    public class Tree
    {
        public List<Node> Root = new List<Node>();
        
        public string menu()
        {
            bool validChoice;
            string choice = "";

            do
            {
                validChoice = false;
                Console.Clear();
                Console.WriteLine("Please select an option:");
                Console.WriteLine("Press [1] to Get a node by ID");
                Console.WriteLine("Press [2] to Add a node to the Tree");
                Console.WriteLine("Press [3] to Move a node within the Tree");
                Console.WriteLine("Press [4] to Delete a node within the Tree");
                Console.WriteLine("Press [5] to Find a node by ID");
                Console.WriteLine("Press [6] to Find a node by Content");
                Console.WriteLine("Press [7] to Save and Quit");

                choice = Console.ReadLine();

                try
                {
                    if(Convert.ToInt32(choice) >= 1 && Convert.ToInt32(choice) <= 7)
                        validChoice = true;
                    else
                    {
                        Console.WriteLine("ERROR 4QB17: Invalid input");
                        Console.WriteLine("Please try again: Press a key to continue...");
                        Console.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR 4QB17: Invalid input");
                    Console.WriteLine("Please try again: Press a key to continue...");
                    Console.ReadLine();
                }
            } while (!validChoice);

            switch (Convert.ToInt32(choice))
            {
                case 1:
                    {
                        Console.WriteLine("Please enter the ID of the Node:");
                        string IdSearch = Console.ReadLine();
                        Console.WriteLine("Please enter [1] to retrieve the Node's branch \nOr enter [0] to not retrieve the Node's branch");
                        string getBranch = Console.ReadLine();
                        bool branchControl;
                        if(getBranch == "1")
                        {
                            branchControl = true;
                            Get(IdSearch, branchControl);
                            Console.ReadLine();
                        }
                        else
                        {
                            branchControl = false;
                            Get(IdSearch, branchControl);

                            Console.ReadLine();
                        }
                        break;
                    }
                case 2:
                    {
                        // Get new node's name and parent from input
                        Console.WriteLine("Please enter the name of your new Node:");
                        string newName = Console.ReadLine();
                        Console.WriteLine("Please enter the desired parent's ID:\n(type 'None' if you want the Node to be in root)");
                        string parentID = Console.ReadLine();

                        // Create a new ID for the new node
                        Random rnd = new Random();
                        string newID = newName + rnd.Next(0, 100);

                        // Add a new node and apply to it the new ID, name, and parent
                        Node newNode = new Node
                        {
                            Id = newID,
                            Content = newName,
                            Parent = FindNodeId(Root, parentID)
                        };
                        newNode.Depth = newNode.Parent.Depth + 1;

                        // Try to AddNode with the node and its parent. If no parent,
                        // it will add to root
                        try
                        {
                            AddNode(newNode, newNode.Parent.Id);
                        }
                        catch (Exception e)
                        {
                            AddNode(newNode, null);
                        }

                        // Also try to add the new node to its parent's list of children
                        try { newNode.Parent.Children.Add(newNode); }
                        catch { }

                        Console.Beep();
                        Console.Beep();
                        Console.Beep(900, 400);

                        // Reflect the new node's addition in console
                        try { Console.WriteLine("New node '" + newNode.Id + "' added to parent " + newNode.Parent.Id); }
                        catch (Exception e) { Console.WriteLine("New node '" + newNode.Id + "' added to root."); }


                        Console.ReadKey();

                        break;
                    }
                case 3: // Move Node
                    {
                        Console.WriteLine("Please enter the ID of the Node to move:");
                        string moveNodeID = Console.ReadLine();
                        string newNodeId = "";
                        string parentOfMoveID = "";

                        Node movingNode = FindNodeId(Root, moveNodeID);

                        if (movingNode.Id != null)
                        {
                            Console.WriteLine("Please enter the parent's ID to move Node " + movingNode.Id + " to:");
                            parentOfMoveID = Console.ReadLine();

                            newNodeId = MoveNode(movingNode.Id, parentOfMoveID);
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid Node ID. Cannot move.");
                        }

                        Console.WriteLine("Node " + newNodeId + " moved under Node " + parentOfMoveID + ".");

                        Console.ReadKey();

                        break;
                    }
                case 4: // Delete Node
                    {
                        Console.WriteLine("Please enter the ID of the Node to delete: ");
                        string IDtoDelete = Console.ReadLine();

                        bool nodeDeleted = DeleteNode(this.Root, IDtoDelete);


                        if (nodeDeleted)
                        {
                            Console.WriteLine("Node " + IDtoDelete + " successfuly deleted.");
                        }
                        else
                        {
                            Console.WriteLine("Node " + IDtoDelete + " was not found.");
                        }

                        Console.ReadKey();

                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Please enter the ID of the Node: ");
                        string contentIDSearch = Console.ReadLine();
                        Node searchResult = FindNodeId(this.Root, contentIDSearch);

                        if (searchResult != null)
                        {
                            Console.Beep();
                            Console.Beep();
                            Console.Beep(1000, 400);
                            Console.WriteLine("Item Found: " + searchResult.Content + ", ID: " + searchResult.Id);
                            searchResult.displayAncestors();
                            searchResult.displayDescendants();
                            Console.WriteLine("\nPress [Enter] to continue...");
                        }
                        else
                        {
                            Console.Beep();
                            Console.Beep();
                            Console.Beep(400, 400);
                            Console.WriteLine("Item Not Found.");
                        }
                        Console.ReadLine();
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("Please enter the name of the Node: ");
                        string contentSearch = Console.ReadLine();
                        Node searchResult = FindNodeContent(this.Root, contentSearch);

                        if(searchResult != null)
                        {
                            Console.Beep();
                            Console.Beep();
                            Console.Beep(1000, 400);
                            Console.WriteLine("Item Found: " + searchResult.Content + ", ID: " + searchResult.Id);
                            Console.WriteLine("Press [Enter] to continue...");
                        }
                        else
                        {
                            Console.Beep();
                            Console.Beep();
                            Console.Beep(400, 400);
                            Console.WriteLine("Item Not Found.");
                        }
                        Console.ReadLine();
                        break;
                    }
            }

            return choice;
        }

        public void Get(string Id, bool shouldGetBranch)
        {
            Node searchResult = new Node();
            try
            {
                searchResult = FindNodeId(Root, Id);
            }
            catch(Exception e)
            {
                // search fail
                searchResult = null;
            }

            if (searchResult != null && !shouldGetBranch)
            {
                Console.WriteLine(searchResult.Content);
            }
            else
            {
                try
                {
                    // Show all the descendants
                    searchResult.displayDescendants();
                }
                catch (Exception e)
                {
                    // descendants don't exist.
                }

            }
        }

        public void AddNode(Node Child, string ParentId)
        {
            // Search to see if parent exists 
            Node searchResult = FindNodeId(Root, ParentId);
            if (searchResult != null)
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
                            foreach (Node o in n.Children)
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

        } // AddNode()

        public string MoveNode(string Id, string ParentId)
        {
            Node newChild = new Node();

            Random rnd = new Random();

            if (ParentId != null)
            {
                Node newParent = FindNodeId(this.Root, ParentId);

                newChild = FindNodeId(this.Root, Id);

                newChild.Id = newChild.Content + rnd.Next(0, 100);

                // Add new node with the moving node's values to the new place
                AddNode(newChild, ParentId);
            }
            else
            {
                newChild = FindNodeId(this.Root, Id);

                newChild.Id = newChild.Content + rnd.Next(0, 100);

                // Add new node with the moving node's values to root
                AddNode(newChild, "None");
            }

            // Delete the original node
            DeleteNode(this.Root, Id);

            // Return the new node
            return newChild.Id;

        } // MoveNode()

        public bool DeleteNode(List<Node> query, string Id)
        {
            bool nodeDeleted = false;

            for (int i = 0; i < query.Count && nodeDeleted == false; i++)
            {
                if (nodeDeleted)
                {
                    break;
                }
                else if (Id == query[i].Id)
                {   // Set all values of the Node to null and remove it from the list
                    query[i].Id = null;
                    query[i].Content = null;
                    query[i].Depth = 0f;
                    query[i].Children = null;
                    query[i].Parent = null;

                    query.RemoveAt(i);

                    nodeDeleted = true;
                }
                else // Check node's children
                {
                    try { nodeDeleted = DeleteNode(query[i].Children, Id); }
                    catch
                    { // Node has no children
                    }

                }
            }

            return nodeDeleted;
        } // DeleteNode()

        public Node FindNodeId(List<Node> query, string Id)
        {
            Node searchResult = new Node();
            foreach (Node n in query)
            {
                if (searchResult.IsReady)
                {
                    break;
                }
                else if (Id == n.Id)
                {
                    n.IsReady = true;
                    searchResult = n;
                    break;
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
                    }
                }
            }

            return searchResult;
        } // FindNodeId()

        public Node FindNodeContent(List<Node> query, string Content)
        {
            Node searchResult = new Node();
            foreach (Node n in query)
            {
                if (searchResult.IsReady)
                {
                    break;
                }                
                else if (Content.ToUpper() == n.Content.ToUpper())
                {
                    n.IsReady = true;
                    searchResult = n;
                    break;
                }
                else
                {
                    // Check n's children
                    try
                    {
                        searchResult = FindNodeContent(n.Children, Content);
                    }
                    catch (Exception e)
                    {
                        // Node n has no children and doesn't exist in context
                    }

                }
            }

            return searchResult;
        } // FindNodeContent()
    }
}

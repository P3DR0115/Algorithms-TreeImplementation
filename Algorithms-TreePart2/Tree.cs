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
                        if(getBranch == "1")
                        {
                            Get(IdSearch, true);
                            Console.ReadLine();
                        }
                        else
                        {
                            Get(IdSearch, false);
                            Console.ReadLine();
                        }
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("Please enter the name of the Node: ");
                        string contentSearch = Console.ReadLine();
                        Node searchResult = FindNodeContent(Root, contentSearch);

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
            Node searchResult = null;
            try
            {
                searchResult = FindNodeId(Root, Id);
            }
            catch(Exception e)
            {
                // search fail
            }

            if (searchResult != null && !shouldGetBranch)
            {
                Console.WriteLine(searchResult.Content);
            }
            else
            {
                try
                {
                    // Show all the parents
                    searchResult.displayDescendants();
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
            
        } // AddNode()

        public void MoveNode(string Id, string ParentId)
        {

        } // MoveNode()

        public void DeleteNode(string Id)
        {

        } // DeleteNode()

        public Node FindNodeId(List<Node> query, string Id)
        {
            Node searchResult = null;
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
                        break;
                    }
                    catch (Exception e)
                    {
                        // Node n has no children
                        searchResult = null;
                        break;
                    }
                }
            }

            return searchResult;
        } // FindNodeId()

        public Node FindNodeContent(List<Node> query, string Content)
        {
            Node searchResult = null; // Defaults to null to display that it couldn't be found
            foreach (Node n in query)
            {
                if (Content.ToUpper() == n.Content.ToUpper())
                {
                    searchResult = n;
                }
                else
                {
                    // Check n's children
                    try
                    {
                        searchResult = FindNodeContent(n.Children, Content);
                        break;
                    }
                    catch (Exception e)
                    {
                        // Node n has no children and doesn't exist in context
                    }

                }
            }

            return searchResult;
        }
    }
}

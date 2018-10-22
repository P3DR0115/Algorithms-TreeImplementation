using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreePart2
{
    public class Run
    {
        static string FileLocation = AppDomain.CurrentDomain.BaseDirectory + "names.tab";

        //public static List<string> FileDataStrings = new List<string>();
        public static string[] FileDataStrings;
        public bool isReady = false;
        Tree baseTree = new Tree();
        string choice;

        public Run()
        {
            LoadText();
            DetermineDepth(baseTree);
            DetermineFamily(baseTree);
            List<string> FileSaveStrings = new List<string>();
            PreDisplayElements();
            DisplayElements(baseTree.Root, FileSaveStrings);
            PreInnerElements();
            InnerElements(baseTree.Root, FileSaveStrings);
            SaveFile(FileSaveStrings);
            Console.WriteLine("Press [Enter] key to continue to the Tree Menu...");
            Console.ReadLine();

            do
            {
                choice = baseTree.menu();
            } while (choice != "7");

            FileSaveStrings.Clear();
            FileSaveStrings = PrepSave(baseTree.Root, FileSaveStrings);
            SaveFile(FileSaveStrings);
            //Console.ReadLine();
        }
        
        static void LoadText()
        {
            //string[] FileStrings = System.IO.File.ReadAllLines(FileLocation);
            FileDataStrings = System.IO.File.ReadAllLines(FileLocation);

            /*
            foreach(string s in FileStrings)
            {
                FileDataStrings.Add(s);
            }*/
        }// LoadText()

        static List<string> PrepSave(List<Node> query, List<string> FileSaveStrings)
        {

            foreach(Node n in query)
            {
                if(n.Children.Count > 0)
                {
                    FileSaveStrings = PrepSave(n.Children, FileSaveStrings);
                }
                string tSaveString = "";
                for (int d = 0; d < n.Depth; d++)
                {
                    tSaveString += "\t";
                }
                tSaveString += n.Content;
                //FileSaveStrings.Add(tSaveString);
                FileSaveStrings.Insert(0, tSaveString);
            }

            return FileSaveStrings;
        }

        static void SaveFile(List<string> FileSaveStrings)
        {
            System.IO.File.WriteAllLines(FileLocation, FileSaveStrings.ToArray());
        }// SaveText()
        
        static void PreDisplayElements()
        {
            Console.WriteLine("All Elements:\n");
        }

        static void PreInnerElements()
        {
            Console.WriteLine("Inner Elements:\n");
        }

        static Tree DetermineDepth(Tree baseTree)
        {
            Random rnd = new Random();
            for (int i = 0; i < FileDataStrings.Length; i++)
            {
                int tDepth = 0;
                float spaceDepth = 0;
                for (int j = 0; j < FileDataStrings[i].Count(); j++)
                {
                    //Console.WriteLine(FileDataStrings[i]);
                    char[] temp = FileDataStrings[i].ToCharArray();
                    if (temp[j] == '\t')
                    {
                        tDepth += 1;
                    }
                    else if (temp[j] == ' ')
                    {
                        spaceDepth += 1;
                    }
                    else
                    {
                        //FileDataStrings[i] = FileDataStrings[i].Substring(tDepth, temp.Length);
                        Node tNode = new Node
                        {
                            Content = FileDataStrings[i].Substring((tDepth + (int)spaceDepth), (temp.Count() - (tDepth + (int)spaceDepth)))
                        };

                        float tempD = spaceDepth % 8;
                        spaceDepth -= tempD;

                        if (i > 0)
                            tNode.Depth = (tDepth + (spaceDepth / 8));
                        else
                            tNode.Depth = 0; //(tDepth + (spaceDepth / 8));

                        tNode.Id = tNode.Content + i + rnd.Next(0, 100);
                        baseTree.Root.Add(tNode);
                        break;
                    }
                } // inner loop. For the characters of the string element

            } // outer loop. For the string elements within the array

            /*bool noTab = true;
            foreach(Node w in baseTree.Root)
            {
                if(w.Depth == 0)
                {
                    noTab = false;
                    break;
                }
            }

            if (!noTab)
            {
                foreach (Node y in baseTree.Root)
                {
                    y.Depth -= 1;
                }
            }*/
            return baseTree;
        } // DetermineDepth();

        static Tree DetermineFamily(Tree baseTree)
        {
            int deepestDepth = 0;

            // Determine how deep the tree is
            foreach (Node n in baseTree.Root)
            {
                if (n.Depth > deepestDepth)
                {
                    deepestDepth = (int)n.Depth;
                }
            }

            List<Node> deepest = new List<Node>();

            for (int i = 0; i < baseTree.Root.Count(); i++)
            {
                if (baseTree.Root[i].Depth == deepestDepth)
                {
                    deepest.Add(baseTree.Root[i]);

                    for (int w = i; w >= 0; w--)
                    {
                        if (baseTree.Root[w].Depth < baseTree.Root[i].Depth)
                        {
                            // The previous is the parent
                            try
                            {
                                baseTree.Root[i].Parent = baseTree.Root[w];
                                baseTree.Root[w].Children.Add(baseTree.Root[i]);
                                baseTree.Root.RemoveAt(i);
                            }
                            catch(Exception e)
                            {
                                baseTree.Root[i].Parent = null;
                                baseTree.Root[i].Depth = 0;
                                //baseTree.Root[w].Children.Add(baseTree.Root[i]);
                                baseTree.Root.RemoveAt(i);
                            }
                            break;
                        }
                    }
                }
            }
            /*
            if(baseTree.Root[0].Depth > 0)
            {

            }
            else */if (deepestDepth > 0)
            {
                try
                {
                    DetermineFamily(baseTree);
                }
                catch(Exception w)
                {

                }
            }
            
            return baseTree;
        } // DetermineFamily();
        
        static void DisplayElements(List<Node> Root, List<string> FileSaveStrings)
        {
            foreach (Node r in Root)
            {
                if (r.Parent != null)
                {
                    r.Depth = r.Parent.Depth + 1;
                }
            }

            foreach (Node n in Root)
            {
                string tSaveString = "";
                if (n.Parent != null)
                {
                    // Yes parent
                    //Console.WriteLine("Depth: " + n.Depth + "    Name: " + n.Name + "    Parent: " + n.Parent.Name);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Depth: " + n.Depth);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("    Name: " + n.Content);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("    Parent: " + n.Parent.Content + "\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    // No parent
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Depth: " + n.Depth);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("    Name: " + n.Content);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("    Parent: None\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                for (int d = 0; d < n.Depth; d++)
                {
                    tSaveString += "\t";
                }
                tSaveString += n.Content;
                FileSaveStrings.Add(tSaveString);

                try
                {
                    // Try to display children
                    DisplayElements(n.Children, FileSaveStrings);
                }
                catch (Exception e)
                {
                    // No Children, Continue
                }
            }

        } // DisplayElements();

        static void InnerElements(List<Node> Root, List<string> FileSaveStrings)
        {
            foreach (Node n in Root)
            {
                if (n.Parent != null && n.Children.Count > 0)
                {
                    // Yes parent and has children
                    //Console.WriteLine("Depth: " + n.Depth + "    Name: " + n.Name + "    Parent: " + n.Parent.Name);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Depth: " + n.Depth);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("    Name: " + n.Content);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("    Parent: " + n.Parent.Content + "\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                // else
                // No parent, Can't display.

                try
                {
                    // Try to display children
                    InnerElements(n.Children, FileSaveStrings);
                }
                catch (Exception e)
                {
                    // No Children, Continue
                }
            }
        } // InnerElements();

    }
}

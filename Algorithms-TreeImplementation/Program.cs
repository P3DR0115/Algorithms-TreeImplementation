using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreeImplementation
{
    class Program
    {
        static string FileLocation = AppDomain.CurrentDomain.BaseDirectory + "people.txt";
        static string[] FileDataStrings;
        public string[] SaveDataStrings;
        public bool isReady = false;

        static void Main()
        {
        List<Node> Root = new List<Node>();

            LoadText();
            DetermineDepth(Root);
            DetermineFamily(Root);
            DisplayElements(Root, 0);
            Console.ReadLine();

        } // main()

        static void LoadText()
        {
            FileDataStrings = System.IO.File.ReadAllLines(FileLocation);
        }// LoadText()

        static void SaveText(string[] SaveDataStrings)
        {
            System.IO.File.WriteAllLines(FileLocation, SaveDataStrings);
        }

        static List<Node> DetermineDepth(List<Node> Root)
        {
            for(int i = 0; i < FileDataStrings.Length; i++)
            {
                int tDepth = 0;
                float spaceDepth = 0;
                for(int j = 0; j < FileDataStrings[i].Count(); j++)
                {
                    //Console.WriteLine(FileDataStrings[i]);
                    char[] temp = FileDataStrings[i].ToCharArray();
                    if(temp[j] == '\t')
                    {
                        tDepth += 1;
                    }
                    else if(temp[j] == ' ')
                    {
                        spaceDepth += 1;
                    }
                    else
                    {
                        //FileDataStrings[i] = FileDataStrings[i].Substring(tDepth, temp.Length);
                        Node tNode = new Node();
                        tNode.Name = FileDataStrings[i].Substring((tDepth + (int)spaceDepth), (temp.Count() - (tDepth + (int)spaceDepth)));

                        float tempD = spaceDepth % 8;
                        spaceDepth -= tempD;

                        tNode.Depth = (tDepth + (spaceDepth / 8));
                        Root.Add(tNode);
                        break;
                    }
                } // inner loop. For the characters of the string element

            } // outer loop. For the string elements within the array

            return Root;
        } // DetermineDepth();

        static List<Node> DetermineFamily(List<Node> Root)
        {
            List<Node> deepest = new List<Node>();
            int deepestDepth = 0;

            // Determine how deep the tree is
            foreach(Node n in Root)
            {
                if(n.Depth > deepestDepth)
                {
                    deepestDepth = (int)n.Depth;
                }
            }

            
            for(int i = 0; i < Root.Count(); i++)
            {
                if(Root[i].Depth == deepestDepth)
                {
                    deepest.Add(Root[i]);

                    for(int w = i; w >= 0; w--)
                    {
                        if(Root[w].Depth < Root[i].Depth)
                        {
                            // The previous is the parent
                            Root[i].Parent = Root[w];
                            Root[w].children.Add(Root[i]);
                            Root.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            if(deepestDepth > 0)
                DetermineFamily(Root);

            return Root;
        } // DetermineFamily();

        static void DisplayElements(List<Node> Root, int depth)
        {
            foreach (Node n in Root)
            {
                if(n.Parent != null)
                {
                    // Yes parent
                    //Console.WriteLine("Depth: " + n.Depth + "    Name: " + n.Name + "    Parent: " + n.Parent.Name);

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Depth: " + n.Depth);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("    Name: " + n.Name);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("    Parent: " + n.Parent.Name + "\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    // No parent
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Depth: " + n.Depth);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("    Name: " + n.Name);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("    Parent: None\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                try
                {
                    // Try to display children
                    DisplayElements(n.children, (depth + 1));
                }
                catch(Exception e)
                {
                    // No Children, Continue
                }
            }

        } // DisplayElements();

        //static string[] 

    }
}

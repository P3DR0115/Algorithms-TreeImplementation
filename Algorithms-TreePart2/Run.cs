using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_TreePart2
{
    public class Run
    {
        static string FileLocation = AppDomain.CurrentDomain.BaseDirectory + "people.txt";
        static string[] FileDataStrings;
        public bool isReady = false;
        Tree baseTree = new Tree();

        public Run()
        {
            LoadText();
            

            List<string> FileSaveStrings = new List<string>();

            SaveFile(FileSaveStrings);
        }
        
        static void LoadText()
        {
            FileDataStrings = System.IO.File.ReadAllLines(FileLocation);
        }// LoadText()

        static void SaveFile(List<string> FileSaveStrings)
        {
            System.IO.File.WriteAllLines(FileLocation, FileSaveStrings.ToArray());
        }// SaveText()

        static Tree DetermineDepth(Tree Root)
        {
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
                        Node tNode = new Node();
                        tNode.Content = FileDataStrings[i].Substring((tDepth + (int)spaceDepth), (temp.Count() - (tDepth + (int)spaceDepth)));

                        float tempD = spaceDepth % 8;
                        spaceDepth -= tempD;

                        tNode.Depth = (tDepth + (spaceDepth / 8));
                        Root.AddNode(tNode);
                        break;
                    }
                } // inner loop. For the characters of the string element

            } // outer loop. For the string elements within the array

            return Root;
        } // DetermineDepth();
    }
}

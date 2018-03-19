using System;
using System.IO;
using System.Collections.Generic;

class ViewerSample
{
    public static void Main()
    {
        /* Baca File */
        ReadFile R = new ReadFile("test.txt");
        /* Print Graph */
        Graph G = new Graph();
        G = R.OpenFile();
        G.printGraph();
    }

    public class Node
    {
        private string Value;
        private List<string> Prereq = new List<string>();
        private int FirstVisit;
        private int LastVisit;
        public Node(string S)
        {
            Value = S;
            List<string> Prereq = new List<string>();
            FirstVisit = 0;
            LastVisit = 0;
        }
        public string getVal()
        {
            return Value;
        }
        public int getFirstVisit()
        {
            return FirstVisit;
        }
        public int getLastVisit()
        {
            return LastVisit;
        }
        public string getPrereq(int Idx)
        {
            /* Elemen list dimulai dari 0 */
            return Prereq[Idx].ToString();
        }
        public int getPrereqIdx(string Search)
        {
            return Prereq.IndexOf(Search);
        }
        public void setFirstVisit(int F)
        {
            FirstVisit = F;
        }
        public void setLastVisit(int L)
        {
            LastVisit = L;
        }
        public void addPrereq(string S)
        {
            Prereq.Add(S);
        }
        public void deletePrereq(string S)
        {
            Prereq.Remove(S);
        }
        public void printAllPrereq()
        {
            foreach (var node in Prereq)
            {
                Console.Write(node);
                if (Convert.ToBoolean(string.Compare(node, Prereq[Prereq.Count - 1])))
                {
                    Console.Write(", ");
                }
            }
        }
    }
    public class Graph
    {
        public List<Node> Nodes;
        public Graph()
        {
            Nodes = new List<Node>();
        }
        public Node getNodes(int Idx)
        {
            return Nodes[Idx];
        }
        public int getNodesIdx(string S)
        {
            Node N = new Node(S);
            return Nodes.IndexOf(N);
        }
        public void setNodes(int idx, Node N)
        {
            Nodes[idx] = N;
        }
        public void addNodes(string S)
        {
            Node N = new Node(S);
            Nodes.Add(N);
        }
        public void addPrereqNodes(int Idx, string S)
        {
            Nodes[Idx].addPrereq(S);
        }
        public void printGraph()
        {
            Console.WriteLine("====== GRAPH ======\n");
            for (int i = 0; i < Nodes.Count; i++)
            {
                Console.WriteLine("\nNodes ke-{0} dengan nilai {1}", i, getNodes(i).getVal());
                Console.WriteLine("Prerequisite : ");
                Nodes[i].printAllPrereq();
            }
            Console.WriteLine();
        }
    }

    public class ReadFile
    {
        private string FilePath;
        public ReadFile(string S)
        {
            FilePath = S;
        }
        public Graph OpenFile()
        {
            Graph G = new Graph();
            StreamReader sr = new StreamReader(FilePath);
            string stringText = File.ReadAllText(FilePath);
            string[] stringPerLineDashN = stringText.Split('\n');
            for (int i = 0; i < stringPerLineDashN.Length; i++)
            {
                string[] stringPerLineDot = stringPerLineDashN[i].Split('.');
                string[] stringPerLineComma = stringPerLineDot[0].Split(',');
                for (int k = 0; k < stringPerLineComma.Length; k++)
                {
                    //string s = stringPerLineComma[k].Trim();
                    //Console.WriteLine("Array ke-{0}, indeks node ke-{1}, valuenya {2}", k, i, s);
                    if (k == 0)
                    {
                        G.addNodes(stringPerLineComma[k].Trim());
                    }
                    else
                    {
                        G.addPrereqNodes(i, stringPerLineComma[k].Trim());
                    }
                }
            }
            sr.Close();
            return G;
        }
    }
}
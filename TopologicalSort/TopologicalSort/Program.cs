using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace TopologicalSort
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Baca File */
            ReadFile R = new ReadFile("test.txt");
            /* Print Graph */
            Graph G = new Graph();
            G = R.OpenFile();
            G.GeneratePostReq();
            G.PrintGraph();
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
                        G.AddNodes(stringPerLineComma[k].Trim());
                    }
                    else
                    {
                        G.AddPrereqNodes(i, stringPerLineComma[k].Trim());
                    }
                }
            }
            sr.Close();
            return G;
        }
    }

    public class Node
    {
        private string Value;
        private List<string> Prereq = new List<string>();
        private List<string> Postreq = new List<string>();
        private bool FirstVisit;
        private bool LastVisit;
        public Node(string S)
        {
            Value = S;
            List<string> Prereq = new List<string>();
            List<string> Postreq = new List<string>();
            FirstVisit = false;
            LastVisit = false;
        }
        public string GetVal()
        {
            return Value;
        }
        public bool GetFirstVisit()
        {
            return FirstVisit;
        }
        public bool GetLastVisit()
        {
            return LastVisit;
        }
        public string GetPrereq(int Idx)
        {
            /* Elemen list dimulai dari 0 */
            return Prereq[Idx].ToString();
        }
        public int GetPrereqIdx(string Search)
        {
            return Prereq.IndexOf(Search);
        }
        public int GetPrereqCount()
        {
            return Prereq.Count;
        }
        public string GetPostreq(int Idx)
        {
            /* Elemen list dimulai dari 0 */
            return Postreq[Idx].ToString();
        }
        public int GetPostreqIdx(string Search)
        {
            return Postreq.IndexOf(Search);
        }
        public int GetPostreqCount()
        {
            return Postreq.Count;
        }
        public void SetFirstVisit(bool F)
        {
            FirstVisit = F;
        }
        public void SetLastVisit(bool L)
        {
            LastVisit = L;
        }
        public void AddPrereq(string S)
        {
            Prereq.Add(S);
        }
        public void DeletePrereq(string S)
        {
            Prereq.Remove(S);
        }
        public void AddPostreq(string S)
        {
            Postreq.Add(S);
        }
        public void DeletePostreq(string S)
        {
            Postreq.Remove(S);
        }
        public void PrintAllPrereq()
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
        public void printAllPostreq()
        {
            foreach (var node in Postreq)
            {
                Console.Write(node);
                if (Convert.ToBoolean(string.Compare(node, Postreq[Postreq.Count - 1])))
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
        public Node GetNodes(int Idx)
        {
            return Nodes[Idx];
        }
        public int GetNodesIdx(string S)
        {
            Node N = new Node(S);
            return Nodes.IndexOf(N);
        }
        public void SetNodes(int idx, Node N)
        {
            Nodes[idx] = N;
        }
        public void AddNodes(string S)
        {
            Node N = new Node(S);
            Nodes.Add(N);
        }
        public void AddPrereqNodes(int Idx, string S)
        {
            Nodes[Idx].AddPrereq(S);
        }
        public void AddPostreqNodes(int Idx, string S)
        {
            Nodes[Idx].AddPostreq(S);
        }
        public void PrintGraph()
        {
            Console.WriteLine("====== GRAPH ======\n");
            for (int i = 0; i < Nodes.Count; i++)
            {
                Console.WriteLine("\nNodes ke-{0} dengan nilai {1}", i, GetNodes(i).GetVal());
                Console.WriteLine("Prerequisite : ");
                Nodes[i].PrintAllPrereq();
                Console.WriteLine("Postrequisite : ");
                Nodes[i].printAllPostreq();
            }
            Console.WriteLine("");
        }
    }
}
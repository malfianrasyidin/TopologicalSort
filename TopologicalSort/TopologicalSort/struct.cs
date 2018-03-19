/* Tugas Besar Strategi Algoritma II */
/* Topological Sort */
/* Rifqi Rifaldi Utomo (13516
 * M Alfian 
 * Deborah Aprilia Josephine (13516152 */
using System;
using System;
using System.Collections.Generic;

namespace TopologicalSort
{
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
            Console.WriteLine("");
        }
    }
}

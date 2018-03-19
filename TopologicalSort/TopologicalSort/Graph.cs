using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace TopologicalSort
{
    internal class Graph
    {
        private List<Node> Nodes;

        public Graph()
        {
            Nodes = new List<Node>();
        }

        public static Graph DeepClone<Graph>(Graph obj)
        {
            Console.WriteLine("DEEP");
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (Graph)formatter.Deserialize(ms);
            }
        }

        public Node GetNodes(int Idx)
        {
            return Nodes[Idx];
        }

        public void AddPostreqOnNodeWithString(int Idx, string s)
        {
            //intinya adalah menambah postreq dengan nilai s pada node ke idx
            Nodes[Idx].AddPostreq(s);
        }

        public void DeletePrereqOnNodeWithString(int Idx, string s)
        {
            //intinya adalah menghapus prereq dengan nilai s pada node ke idx
            Nodes[Idx].DeletePrereq(s);
        }

        public int GetNodesIdx(string S)
        {
            for (int i = 0; i < Nodes.Count(); ++i)
            {
                if (Nodes[i].GetVal().Equals(S, StringComparison.Ordinal))
                {
                    return i;
                }
            }
            return -1;
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

        public int GetNodesCount()
        {
            return Nodes.Count;
        }

        public void PrintGraph()
        {
            Console.WriteLine("====== GRAPH ======\n");
            for (int i = 0; i < Nodes.Count; i++)
            {
                Console.WriteLine("\nNodes ke-{0} dengan nilai {1}", i, GetNodes(i).GetVal());
                Console.WriteLine("Prerequisite : ");
                Nodes[i].PrintAllPrereq();
                Console.WriteLine("\nPostrequisite : ");
                Nodes[i].PrintAllPostreq();
            }
            Console.WriteLine("");
        }

        public void NodesRemoveAt(int Idx)
        {
            Nodes.RemoveAt(Idx);
        }

        public int Count()
        {
            return Nodes.Count;
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Drawing;

namespace TopologicalSort
{
    internal class Graph
    {
        private List<Node> Nodes;

        public Graph()
        {
            Nodes = new List<Node>();
        }

        public Graph(Graph _Graph)
        {
            Nodes = new List<Node>(_Graph.Nodes);
        }

        public static Graph DeepClone<Graph>(Graph obj)
        {
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
            Nodes[Idx].AddPostreq(s);
        }

        public void DeletePrereqOnNodeWithString(int Idx, string s)
        {
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

        public void Draw()
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            for (int i=0; i< Nodes.Count; i++)
            {
                for (int j=0; j<Nodes[i].GetPostreqCount(); j++)
                {
                    graph.AddEdge(Nodes[i].GetVal(), Nodes[i].GetPostreq(j));
                }
            }
            viewer.Graph = graph; 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.WindowState = FormWindowState.Maximized;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();
        }

        public void NodesRemoveAt(int Idx)
        {
            Nodes.RemoveAt(Idx);
        }
    }
}
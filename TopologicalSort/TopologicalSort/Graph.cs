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

        public void Draw()
        {
            //create a form 
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            
            //create the graph content 
            //graph.AddEdge(timestamps[0], timestamps[1]);
            //graph.AddEdge("B", "C");
            //graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            //graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            //graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            //Microsoft.Msagl.Drawing.Node c = graph.FindNode(timestamps[0]);
            //c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            //c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            
            for (int i=0; i< Nodes.Count; i++)
            {
                for (int j=0; j<Nodes[i].GetPostreqCount(); i++)
                {
                    graph.AddEdge(Nodes[i].GetVal(), Nodes[i].GetPostreq(j));
                }
            }

            //bind the graph to the viewer 
            viewer.Graph = graph;

            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            Button btn1 = new Button();
            btn1.Width = 170;
            btn1.Height = 30;
            btn1.Text = "Next";
            btn1.ForeColor = Color.White;
            form.WindowState = FormWindowState.Maximized;
            form.Controls.Add(btn1);
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            //form.ShowDialog();
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
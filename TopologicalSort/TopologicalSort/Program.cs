using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

class ViewerSample
{
    public static void Main()
    {
        //create a form 
        Form form = new Form();
        //create a viewer object 
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        //create a graph object 
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        //create the graph content 
        graph.AddEdge("A", "B");
        graph.AddEdge("B", "C");
        graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
        graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
        graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
        Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
        c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
        c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
        //bind the graph to the viewer 
        viewer.Graph = graph;
        //associate the viewer with the form 
        form.SuspendLayout();
        viewer.Dock = System.Windows.Forms.DockStyle.Fill;
        form.Controls.Add(viewer);
        form.ResumeLayout();
        //show the form 
        form.ShowDialog();
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
                if (Convert.ToBoolean(string.Compare(node,Prereq[Prereq.Count - 1]))){
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
                Console.WriteLine("Nodes ke-{0} dengan nilai {1}", i, getNodes(i).getVal());
                Console.WriteLine("Prerequisite : ");
                Nodes[i].printAllPrereq();
            }
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
            while (!sr.EndOfStream)
            {
                string line = File.ReadAllText(FilePath);
                string[] linePerLine = line.Split('.');
                int i = 0 , idx = 0;
                while (i < linePerLine.Length)
                {
                    string[] stringPerLine = linePerLine[i].Split(',');
                    for (int j = 0; j < stringPerLine.Length; j++)
                    {
                        //Console.WriteLine("Array ke-{0}, traversal ke-{1} dengan value {2}", j, idx, stringPerLine[j]);
                        if (stringPerLine[j] != null)
                        {
                            if (j == 0)
                            {
                                G.addNodes(stringPerLine[j].Trim());
                            }
                            else
                            {
                                G.addPrereqNodes(idx, stringPerLine[j].Trim());
                            }
                        }
                    }
                    if (linePerLine[i] == null)
                    {
                        idx++;
                    }
                    i++;
                }
            }
            sr.Close();
            return G;
        }
    }
}
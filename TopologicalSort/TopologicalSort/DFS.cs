using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TopologicalSort
{
    internal class DFS
    {
        private int number_of_nodes;
        List<string> timestamps, first_semester;
        private Graph graph;

        public DFS(Graph _graph)
        {
            graph = _graph;
            number_of_nodes = graph.Count();
            timestamps = new List<string>();
            first_semester = new List<string>();
        }

        public void InitFirstSemester()
        {
            for (int i = 0; i < graph.Count(); i++)
            {
                if (graph.GetNodes(i).GetPrereqCount() == 0)
                {
                    first_semester.Add(graph.GetNodes(i).GetVal());
                }
            }
        }

        public void Execute()
        {
            InitFirstSemester();
            for (int i = 0; i < first_semester.Count; i++)
            {
                string current = first_semester[i];
                //Console.WriteLine(current);
                ExecuteDFS(graph, current);
            }
            Print();
            Draw();
            
        }

        public void ExecuteDFS(Graph graph, string current)
        {
            timestamps.Add(current);
            int idx = graph.GetNodesIdx(current);
            //Console.WriteLine("idx" + idx.ToString());
            Node x = graph.GetNodes(idx);
            if (x.GetPostreqCount() != 0)
            {
                //Console.WriteLine(x.GetNumberPostReq());
                for (int i = 0; i < x.GetPostreqCount(); i++)
                {
                    //Console.WriteLine(i);
                    if (!HasVisited(x.GetPostreq(i)))
                    {
                        //Console.WriteLine("yes");
                        ExecuteDFS(graph, x.GetPostreq(i));
                    }
                }
                timestamps.Add(current);
            }
            else
            {
                timestamps.Add(current);
            }
        }

        public void Print()
        {
            for (int i = 0; i < timestamps.Count; i++)
            {
                Console.WriteLine(timestamps[i]);
            }
        }

        public bool HasVisited(string current)
        {
            bool found = false;
            int i = 0;
            //Console.WriteLine("here");
            while (!found && i < timestamps.Count)
            {
                if (timestamps[i].Equals(current, StringComparison.Ordinal))
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }
            return found;
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
            int i = 0;
            while(i<timestamps.Count-1)
            {
                graph.AddEdge(timestamps[i], timestamps[i+1]);
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
                form.ShowDialog();
                i++;
            }
            //graph.AddEdge("B", "C");
            //graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            //graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            //graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            //Microsoft.Msagl.Drawing.Node c = graph.FindNode(timestamps[0]);
            //c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            //c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            
        }
    }
}
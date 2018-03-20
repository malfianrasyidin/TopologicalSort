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

        public void Execute(Graph _graph)
        {
            InitFirstSemester();
            for (int i = 0; i < first_semester.Count; i++)
            {
                //Console.WriteLine(first_semester.ToString());
                string current = first_semester[i];
                //Console.WriteLine(current);
                ExecuteDFS(_graph, current);
            }
            Print();
            Draw(_graph);
            FinalDraw(_graph);
        }

        public void ExecuteDFS(Graph graph, string current)
        {
            timestamps.Add(current);
            int idx = graph.GetNodesIdx(current);
            Console.WriteLine("idx" + idx.ToString());
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

        public void Draw(Graph _graph)
        {
            int i = 0;
            while(i<timestamps.Count-1)
            {
                //create a form 
                System.Windows.Forms.Form form = new System.Windows.Forms.Form();
                //create a viewer object 
                Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
                //create a graph object 
                Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

                //create the graph content 
                for (int idx = 0; idx < _graph.GetNodesCount(); idx++)
                {
                    for (int j = 0; j < _graph.GetNodes(idx).GetPostreqCount(); j++)
                    {
                        graph.AddEdge(_graph.GetNodes(idx).GetVal(), _graph.GetNodes(idx).GetPostreq(j));
                    }
                }
                //graph.AddEdge(timestamps[i], timestamps[i+1]);
                //bind the graph to the viewer 
                viewer.Graph = graph;
                //associate the viewer with the form 
                form.SuspendLayout();
                viewer.Dock = System.Windows.Forms.DockStyle.Fill;
                form.WindowState = FormWindowState.Maximized;
                form.Controls.Add(viewer);
                form.ResumeLayout();
                graph.FindNode(timestamps[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
                graph.FindNode(timestamps[i+1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                //show the form 
                form.ShowDialog();
                graph.FindNode(timestamps[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                graph.FindNode(timestamps[i+1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                i++;
            }
        }

        public void FinalDraw(Graph _graph)
        {
            //create a form 
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            int i = timestamps.Count - 1;
            while (i > 0)
            {
                if (!(timestamps[i].Equals(timestamps[i - 1], StringComparison.Ordinal)))
                {
                    graph.AddEdge(timestamps[i], timestamps[i - 1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                }
                i--;
            }
            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.WindowState = FormWindowState.Maximized;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
        }
    }
}
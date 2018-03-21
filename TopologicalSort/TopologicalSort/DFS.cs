using System;
using System.Collections.Generic;
using System.Linq;
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
            number_of_nodes = graph.GetNodesCount();
            timestamps = new List<string>();
            first_semester = new List<string>();
        }

        public void InitFirstSemester()
        {
            for (int i = 0; i < graph.GetNodesCount(); i++)
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
                string current = first_semester[i];
                ExecuteDFS(_graph, current);
            }
            Console.WriteLine("Hasil Penyusunan Mata Kuliah dengan DFS");
            Console.WriteLine("=======================================");
            Draw(_graph);
            DrawBox("Hasil Topological Sort dengan DFS");
            DrawResult(_graph);
            DrawBox("BFS - Breadth First Search");
        }

        public void ExecuteDFS(Graph graph, string current)
        {
            timestamps.Add(current);
            int idx = graph.GetNodesIdx(current);
            Node x = graph.GetNodes(idx);
            if (x.GetPostreqCount() != 0)
            {
                for (int i = 0; i < x.GetPostreqCount(); i++)
                {
                    if (!HasVisited(x.GetPostreq(i)))
                    {
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
            DrawBox("DFS - Depth First Search");
            int i = 0;
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            for (int idx = 0; idx < _graph.GetNodesCount(); idx++)
            {
                if (_graph.GetNodes(idx).GetPostreqCount() == 0)
                {
                    graph.AddNode(_graph.GetNodes(idx).GetVal());
                }
                else
                {
                    for (int j = 0; j < _graph.GetNodes(idx).GetPostreqCount(); j++)
                    {
                        graph.AddEdge(_graph.GetNodes(idx).GetVal(), _graph.GetNodes(idx).GetPostreq(j));
                    }
                }
            }

            viewer.Graph = graph;
            form.SuspendLayout();
            form.Controls.Add(viewer);
            form.ResumeLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.WindowState = FormWindowState.Maximized;

            while (i<timestamps.Count)
            {   
                if (i!=0)
                {
                    graph.FindNode(timestamps[i - 1]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                    graph.FindNode(timestamps[i - 1]).LabelText = timestamps[i - 1];
                }
                graph.FindNode(timestamps[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                graph.FindNode(timestamps[i]).LabelText = (i+1).ToString();
                form.ShowDialog();
                i++;
            }
            
        }

        public void DrawResult(Graph _graph)
        {
            timestamps.Reverse();
            List<string> distinct = timestamps.Distinct().ToList();
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            for (int i = 0; i < distinct.Count; i++)
            {
                Console.WriteLine("\nSemester {0} :\n{1}", i + 1, distinct[i]);
            }
            for (int idx = 0; idx < distinct.Count-1 ; idx++)
            {
                graph.AddEdge(distinct[idx], distinct[idx+1]).Attr.Color = Microsoft.Msagl.Drawing.Color.Green; ;
                int index = _graph.GetNodesIdx(distinct[idx]);
                for (int j = 0; j < _graph.GetNodes(index).GetPostreqCount(); j++)
                {
                    if (!(distinct[idx+1].Equals(_graph.GetNodes(index).GetPostreq(j), StringComparison.Ordinal)))
                    {
                        graph.AddEdge(_graph.GetNodes(index).GetVal(), _graph.GetNodes(index).GetPostreq(j));
                    }
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

        public void DrawBox(string str)
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            graph.AddNode(str).Attr.Shape = Microsoft.Msagl.Drawing.Shape.Box;
            viewer.Graph = graph;
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.WindowState = FormWindowState.Maximized;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();
        }
    }
}
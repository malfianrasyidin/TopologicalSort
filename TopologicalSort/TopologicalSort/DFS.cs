using System;
using System.Collections.Generic;
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
            Draw(_graph);
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
            int i = 0;
            while(i<timestamps.Count)
            {
                System.Windows.Forms.Form form = new System.Windows.Forms.Form();
                Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
                Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

                for (int idx = 0; idx < _graph.GetNodesCount(); idx++)
                {
                    if (_graph.GetNodes(idx).GetPostreqCount() == 0)
                    {
                        graph.AddNode(_graph.GetNodes(idx).GetVal());
                    } else
                    {
                        for (int j = 0; j < _graph.GetNodes(idx).GetPostreqCount(); j++)
                        {
                            graph.AddEdge(_graph.GetNodes(idx).GetVal(), _graph.GetNodes(idx).GetPostreq(j));
                        }
                    }
                }
                viewer.Graph = graph;
                form.SuspendLayout();
                viewer.Dock = System.Windows.Forms.DockStyle.Fill;
                form.WindowState = FormWindowState.Maximized;
                form.Controls.Add(viewer);
                form.ResumeLayout();
                graph.FindNode(timestamps[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                graph.FindNode(timestamps[i]).LabelText = (i+1).ToString();
                form.ShowDialog();
                i++;
            }
        }
    }
}
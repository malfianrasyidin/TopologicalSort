using System;
using System.Collections.Generic;

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
    }
}
using System;
using System.Linq;
using System.Collections.Generic;

namespace TopologicalSort
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Baca File */
            ReadFile R = new ReadFile("test.txt");
            /* Print Graph */
            Graph G = new Graph();
            G = R.OpenFile();
            R.GeneratePostReq(G);
            G.PrintGraph();
            BFS bfs = new BFS(G);
            DFS dfs = new DFS(G);
            dfs.Execute();
            Console.WriteLine("--------");
            if (G.GetNodesCount()>0)
            {
                G.PrintGraph();
            }
            else
            {
                Console.WriteLine("Graf kosong");
            }
        }
    }  
}
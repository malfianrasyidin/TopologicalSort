using System;
using System.Linq;
using System.Collections.Generic;

namespace TopologicalSort
{
    class Program
    {
        static void Main(string[] args)
        {
            //Membaca File dan Inisialisasi Graph
            ReadFile R = new ReadFile("test.txt");
            Graph G = new Graph();
            G = R.OpenFile();
            R.GeneratePostReq(G);

            //Menggambar DFS
            DFS dfs = new DFS(G);
            dfs.Execute(G);

            //Menggambar BFS
            BFS bfs = new BFS(G);
            bfs.Execute(G);
        }
    }  
}
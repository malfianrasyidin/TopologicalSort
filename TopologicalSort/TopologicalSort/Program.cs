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
            Console.WriteLine("============================== Penyusunan Mata Kuliah dengan DFS dan BFS ==============================");
            Console.WriteLine("================================== Oleh 13516098, 13516104, 13516152 ==================================");
            Console.WriteLine("");
            Console.WriteLine("Sebelum memasukan nama file, pastikan file sudah disimpan di dalam folder \nTubesDuaStima/TopologicalSort/TopologicalSort/bin/Debug");
            Console.Write("Nama file : ");
            string s = Console.ReadLine();
            ReadFile R = new ReadFile(s.Trim());
            Graph G = new Graph();
            G = R.OpenFile();
            R.GeneratePostReq(G);
            G.Draw();

            Console.WriteLine("HASIL TOPOLOGICAL SORTING");
            Console.WriteLine("=========================");

            //Menggambar DFS
            DFS dfs = new DFS(G);
            dfs.Execute(G);

            //Menggambar BFS
            BFS bfs = new BFS(G);
            bfs.Execute(G);

            Console.WriteLine("\n=====================");
            Console.WriteLine("Press any key to exit ");
            Console.ReadLine();
        }
    }  
}
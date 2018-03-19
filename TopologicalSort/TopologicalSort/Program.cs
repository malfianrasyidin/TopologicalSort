using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Linq;

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
            Graph GDFS = new Graph();
            Graph GBFS = new Graph();
            G = R.OpenFile();
            GDFS = R.OpenFile();
            GBFS = R.OpenFile();
            R.GeneratePostReq(G);
            R.GeneratePostReq(GDFS);
            R.GeneratePostReq(GBFS);
            G.PrintGraph();
            BFS bfs = new BFS(GBFS);
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
            string stringText = File.ReadAllText(FilePath);
            string[] stringPerLineDashN = stringText.Split('\n');
            for (int i = 0; i < stringPerLineDashN.Length; i++)
            {
                string[] stringPerLineDot = stringPerLineDashN[i].Split('.');
                string[] stringPerLineComma = stringPerLineDot[0].Split(',');
                for (int k = 0; k < stringPerLineComma.Length; k++)
                {
                    //string s = stringPerLineComma[k].Trim();
                    //Console.WriteLine("Array ke-{0}, indeks node ke-{1}, valuenya {2}", k, i, s);
                    if (k == 0)
                    {
                        G.AddNodes(stringPerLineComma[k].Trim());
                    }
                    else
                    {
                        G.AddPrereqNodes(i, stringPerLineComma[k].Trim());
                    }
                }
            }
            sr.Close();
            return G;
        }
        public void GeneratePostReq(Graph G)
        {
            //iterasi dari awal graph
            for (int i = 0; i < G.GetNodesCount(); ++i)
            {
                //untuk setiap graf, diiterasi prereqnya.
                for (int j = 0; j < G.GetNodes(i).GetPrereqCount(); ++j)
                {
                    //untuk setiap prereq, dimasukkin si node ke postreqnya
                    G.AddPostreqNodes(G.GetNodesIdx(G.GetNodes(i).GetPrereq(j)) , (G.GetNodes(i).GetVal()));
                    //Console.WriteLine("NYAMPE PLZZ");
                }
                //caranya pertama lu harus 
            }
        }
    }
    public class BFS
    {
        public BFS(Graph _G)
        {
            Console.WriteLine("YAHUT");
            Graph G = _G;
            List<string> mkTanpaPrereq = new List<string>();
            int semesterSaatIni = 1;
            bool belumSelesai = true; //belum selesai = masih ada yang listnya gak kosong
            do
            {
                //cari ada kah yang matkulnya gak ada prereq
                for (int i = 0; i < G.GetNodesCount(); ++i)
                {
                    if ((G.GetNodes(i).GetPrereqCount()) == 0)
                    {
                        mkTanpaPrereq.Add(G.GetNodes(i).GetVal());
                    }
                } // didapatkan semua matkul yang gak ada prereq nya
                // mengeluarkan isi dari mkTanpaPrereq
                Console.WriteLine("Semester " + semesterSaatIni.ToString() + " :");
                for (int i = 0; i < mkTanpaPrereq.Count(); ++i)
                {
                    Console.WriteLine(mkTanpaPrereq[i]);
                } // semua prereq sudah dihapus
                  //menghapus semua preReq yang mengandung mkTanpaPrereq
                for (int i = 0; i < mkTanpaPrereq.Count(); ++i)
                {
                    for (int j = 0; j < G.GetNodesCount(); ++j)
                    {
                        G.DeletePrereqNodes(j, mkTanpaPrereq[i]);
                    }
                } // semua prereq sudah dihapus

                // menghapus mkTanpaPrereq dari Graph
                for (int i = 0; i < mkTanpaPrereq.Count(); ++i)
                {
                    int j = G.GetNodesIdx(mkTanpaPrereq[i]);
                    if (j != -1)
                    {
                        G.NodesRemoveAt(j);
                    }
                } // mkTanpaPrereq sudah dihapus dari Graph
                  // menghapus isi dari mkTanpaPrereq (biar ntar terisi yang baru lagi)
                mkTanpaPrereq.Clear();
                // mengecek apakah masih ada node pada graph G
                if (G.GetNodesCount() == 0)
                {
                    belumSelesai = false;
                }
                // menambah semester saat ini jadi semester selanjutnya
                ++semesterSaatIni;
            } while (belumSelesai);
        }
    }
    public class DFS
    {
        private static int timestamp;
        private List<string> reverseorder = new List<string>();
        public static int GetTimestamp()
        {
            return timestamp;
        }
        public static void SetTimestamp(int s)
        {
            timestamp = s;
        }
        public DFS(Graph _G)
        {
            Graph G = _G;

        }
    }

    public class Node
    {
        private string Value;
        private List<string> Prereq = new List<string>();
        private List<string> Postreq = new List<string>();
        private bool FirstVisit;
        private bool LastVisit;
        public Node(string S)
        {
            Value = S;
            List<string> Prereq = new List<string>();
            List<string> Postreq = new List<string>();
            FirstVisit = false;
            LastVisit = false;
        }
        public string GetVal()
        {
            return Value;
        }
        public bool GetFirstVisit()
        {
            return FirstVisit;
        }
        public bool GetLastVisit()
        {
            return LastVisit;
        }
        public int GetPrereqCount()
        {
            return Prereq.Count();
        }
        public string GetPrereq(int Idx)
        {
            /* Elemen list dimulai dari 0 */
            return Prereq[Idx];
        }
        public int GetPrereqIdx(string Search)
        {
            return Prereq.IndexOf(Search);
        }
        public string GetPostreq(int Idx)
        {
            /* Elemen list dimulai dari 0 */
            return Postreq[Idx];
        }
        public int GetPostreqIdx(string Search)
        {
            return Postreq.IndexOf(Search);
        }
        public void SetFirstVisit(bool F)
        {
            FirstVisit = F;
        }
        public void SetLastVisit(bool L)
        {
            LastVisit = L;
        }
        public void AddPrereq(string S)
        {
            Prereq.Add(S);
        }
        public void DeletePrereq(string S)
        {
            Prereq.Remove(S);
        }
        public void AddPostreq(string S)
        {
            Postreq.Add(S);
        }
        public void DeletePostreq(string S)
        {
            Postreq.Remove(S);
        }
        public void PrintAllPrereq()
        {
            foreach (var node in Prereq)
            {
                Console.Write(node);
                if (Convert.ToBoolean(string.Compare(node, Prereq[Prereq.Count - 1])))
                {
                    Console.Write(", ");
                }
            }
        }
        public void printAllPostreq()
        {
            foreach (var node in Postreq)
            {
                Console.Write(node);
                if (Convert.ToBoolean(string.Compare(node, Postreq[Postreq.Count - 1])))
                {
                    Console.Write(", ");
                }
            }
        }
    }

    public class Graph
    {
        private List<Node> Nodes;
        public Graph()
        {
            Nodes = new List<Node>();
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
        public void DeletePrereqNodes(int Idx, string s)
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
                Nodes[i].printAllPostreq();
            }
            Console.WriteLine("");
        }
        public void NodesRemoveAt(int Idx)
        {
            Nodes.RemoveAt(Idx);
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Linq;

namespace TopologicalSort
{
    class Program
    {
<<<<<<< HEAD
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
        BFS(G);
    }

    public static void BFS(Graph G)
    {
        List<string> mkTanpaPrereq = new List<string>();
        int semesterSaatIni = 1;
        bool belumSelesai = true; //belum selesai = masih ada yang listnya gak kosong
	    do {
		    //cari ada kah yang matkulnya gak ada prereq
    	    for (int i = 0; i< G.Nodes.Count; ++i) {
    		    if (!(G.Nodes[i].Prereq.Any())) {
    			    mkTanpaPrereq.Add(G.Nodes[i].getVal());
    		    }
            } // didapatkan semua matkul yang gak ada prereq nya
    	    //menghapus semua preReq yang mengandung mkTanpaPrereq
    	    for (int i = 0; i<mkTanpaPrereq.Count(); ++i) {
    		    for (int j = 0; j<G.Nodes.Count(); ++j) {
    			    G.Nodes[j].deletePrereq(mkTanpaPrereq[i]);
    		    }
    	    } // semua prereq sudah dihapus
    	    // mengeluarkan isi dari mkTanpaPrereq
    	    Console.WriteLine("Semester " + semesterSaatIni.ToString() + " :");
    	    for (int i = 0; i<mkTanpaPrereq.Count(); ++i) {
    		    Console.WriteLine(mkTanpaPrereq[i]);
    	    } // semua prereq sudah dihapus
    	
    	    // menghapus mkTanpaPrereq dari Graph
    	    for (int i = 0; i<mkTanpaPrereq.Count(); ++i) {
                int j = G.getNodesIdx(mkTanpaPrereq[i]);
                if (j != -1)
                {
                    G.Nodes.RemoveAt(j);
                }
    	    } // mkTanpaPrereq sudah dihapus dari Graph
    	    // menghapus isi dari mkTanpaPrereq (biar ntar terisi yang baru lagi)
    	    mkTanpaPrereq.Clear();
    	    // mengecek apakah masih ada node pada graph G
    	    if (!(G.Nodes.Any())) {
    		    belumSelesai = false;
    	    }
    	    // menambah semester saat ini jadi semester selanjutnya
    	    ++semesterSaatIni;
        } while (belumSelesai);
=======
        static void Main(string[] args)
        {
            /* Baca File */
            ReadFile R = new ReadFile("test.txt");
            /* Print Graph */
            Graph G = new Graph();
            G = R.OpenFile();
            G.PrintGraph();
        }
>>>>>>> e2709feb43f1f25457c1ed36f4a03de2bef228ba
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
        public void GeneratePostReq()
        {

        }
    }
    public class Node
    {
        private string Value;
<<<<<<< HEAD
        public List<string> Prereq = new List<string>();
        private int FirstVisit;
        private int LastVisit;
=======
        private List<string> Prereq = new List<string>();
        private List<string> Postreq = new List<string>();
        private bool FirstVisit;
        private bool LastVisit;
>>>>>>> e2709feb43f1f25457c1ed36f4a03de2bef228ba
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
        public string GetPrereq(int Idx)
        {
            /* Elemen list dimulai dari 0 */
            return Prereq[Idx].ToString();
        }
        public int getPrereqIdx(string Search)
        {
            return Prereq.IndexOf(Search);
        }
        public string getPostreq(int Idx)
        {
            /* Elemen list dimulai dari 0 */
            return Postreq[Idx].ToString();
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
        public List<Node> Nodes;
        public Graph()
        {
            Nodes = new List<Node>();
        }
        public Node GetNodes(int Idx)
        {
            return Nodes[Idx];
        }
        public int GetNodesIdx(string S)
        {
            Node N = new Node(S);
            return Nodes.IndexOf(N);
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
        public void PrintGraph()
        {
            Console.WriteLine("====== GRAPH ======\n");
            for (int i = 0; i < Nodes.Count; i++)
            {
                Console.WriteLine("\nNodes ke-{0} dengan nilai {1}", i, GetNodes(i).GetVal());
                Console.WriteLine("Prerequisite : ");
                Nodes[i].PrintAllPrereq();
            }
            Console.WriteLine("");
        }
    }
}
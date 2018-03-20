using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TopologicalSort
{
    internal class BFS
    {
        public BFS (Graph _G)
        {
            Graph G = _G;
            Execute(G);
        }

        public void Execute(Graph _G)
        {
            //Graph G = _G;
            Graph G = new Graph(_G);
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
                }
                Draw(G, mkTanpaPrereq);
                // semua prereq sudah dihapus
                  //menghapus semua preReq yang mengandung mkTanpaPrereq
                for (int i = 0; i < mkTanpaPrereq.Count(); ++i)
                {
                    for (int j = 0; j < G.GetNodesCount(); ++j)
                    {
                        G.DeletePrereqOnNodeWithString(j, mkTanpaPrereq[i]);
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

        public void Draw(Graph G, List<string>mkTanpaPrereq)
        {
            //create a form 
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            
            for (int i = 0; i < mkTanpaPrereq.Count(); ++i)
            {
                graph.AddNode(mkTanpaPrereq[i]);
                graph.FindNode(mkTanpaPrereq[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Gray;
            }
            for (int i = 0; i < G.GetNodesCount(); ++i)
            {
                for (int j = 0; j < G.GetNodes(i).GetPrereqCount(); ++j)
                {
                    graph.AddEdge(G.GetNodes(i).GetPrereq(j), G.GetNodes(i).GetVal());
                }
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
using System;
using System.Collections.Generic;
using System.Linq;

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
                } // semua prereq sudah dihapus
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
    }
}
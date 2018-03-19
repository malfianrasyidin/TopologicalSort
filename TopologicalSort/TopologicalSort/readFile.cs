using System.IO;

namespace TopologicalSort
{
    internal class ReadFile
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
                    G.AddPostreqNodes(G.GetNodesIdx(G.GetNodes(i).GetPrereq(j)), (G.GetNodes(i).GetVal()));
                    //Console.WriteLine("NYAMPE PLZZ");
                }
            }
        }
    }
}
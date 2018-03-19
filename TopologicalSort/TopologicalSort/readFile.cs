/* Tugas Besar Strategi Algoritma II */
/* Topological Sort */
/* Rifqi Rifaldi Utomo (13516) */
/* M Alfian */ 
/* Deborah Aprilia Josephine (13516152 */

using System;
using System.IO;
using System.Linq;

namespace TopologicalSort
{
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
                        G.addNodes(stringPerLineComma[k].Trim());
                    }
                    else
                    {
                        G.addPrereqNodes(i, stringPerLineComma[k].Trim());
                    }
                }
            }
            sr.Close();
            return G;
        }
        public void generatePostReq()
        {

        }
    }
}

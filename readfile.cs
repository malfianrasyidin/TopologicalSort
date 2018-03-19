/* Tugas Besar Stima II */
/* Rifqi Rifaldi Utomo 13516098 */
/* Muhammad Alfian Rasyidin 13516104 */
/* Deborah Aprilia Josephine 13516152 */

using System ;
using System.IO.file ;
namespace DirectedAcyclicGraph
{
    public class ReadFile{
        private string FilePath;
        public ReadFile(string S){
            FilePath = S;
        }
        public Graph OpenFile(){
            Graph G = new Graph();
            StreamReader sr = new StreamReader(FilePath);
            while(!sr.EndOfStream){
                string line = sr.ReadLine();
                string[] linePerLine = line.Split('.');
                for (int i=0 ; i < linePerLine.Length ; i++){
                    string[] stringPerLine = linePerLine[i].Split(',');
                    for (int j =0; j < stringPerLine.Length ; j++){
                        if (j==0){
                            G.addNodes(stringPerLine[j].Trim());
                        }
                        else {
                            G.addPrereqNodes(i,stringPerLine[j].Trim());
                        }
                    }
                }
            }  
            sr.Close(); 
            return G; 
        }
    }
}

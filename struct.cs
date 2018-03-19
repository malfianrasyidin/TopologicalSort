/* Tugas Besar Stima II */
/* Rifqi Rifaldi Utomo 13516098 */
/* Muhammad Alfian Rasyidin 13516104 */
/* Deborah Aprilia Josephine 13516152 */

using System ;
using System.Collections.Generic;
namespace DirectedAcyclicGraph
{
    public class Node {
        private string Value ;
        private List<string>[] Prereq;
        private int FirstVisit ;
        private int LastVisit ;
        public Node (string S) {
            Value = S;
            List<string> Prereq = new List<string>();
            FirstVisit = 0 ;
            LastVisit = 0 ;
        }
        public string getVal(){
            return Value ;
        }
        public int getFirstVisit(){
            return FirstVisit;
        }
        public int getLastVisit(){
            return LastVisit;
        }
        public string getPrereq(int Idx){
            /* Elemen list dimulai dari 0 */
            return Prereq[Idx];
        }
        public int getPrereqIdx(string Search){
            return Prereq.IndexOf(Search);
        }
        public void setFirstVisit(int F){
            FirstVisit = F;
        }
        public void setLastVisit(int L){
            LastVisit = L ;
        }
        public void addPrereq(string S){
            Prereq.add(S);
        }
        public void deletePrereq(string S){
            Prereq.remove(S);
        }
        public void printAllPrereq(){
            foreach (var node in Prereq){
                Console.WriteLine(node);
                if (node != Prereq.Last()){
                    Console.WriteLine(", ")
                }
            }
        }
    }
    public class Graph {
        private List<Node> Nodes;
        public Graph(){
            Nodes = new List<Node>();
        }
        public Nodes getNodes(int Idx){
            return Nodes[idx];
        }
        public int getNodes(string S){
            return Nodes.IndexOf(S);
        }
        public void setNodes(int idx, Nodes N){
            Nodes[i] = N;
        }
        public void addNodes(string S){
            Nodes N = new Node(S);
            Nodes.add(N);
        }
        public void addPrereqNodes(int Idx, string S){
            Nodes[Idx].addPrereq(S);
        }
    }
}
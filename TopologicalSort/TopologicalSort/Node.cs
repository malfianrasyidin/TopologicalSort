using System;
using System.Collections.Generic;
using System.Linq;

namespace TopologicalSort
{
    internal class Node
    {
        private string Value;
        private List<string> Prereq = new List<string>();
        private List<string> Postreq = new List<string>();

        public Node(string S)
        {
            Value = S;
            List<string> Prereq = new List<string>();
            List<string> Postreq = new List<string>();
        }

        public Node(Node _Node)
        {
            Value = _Node.Value;
            List<string> Prereq = new List<string>();
            List<string> Postreq = new List<string>();
            for (int i = 0; i < _Node.GetPrereqCount(); ++i)
            {
                AddPrereq(_Node.GetPrereq(i));
            }
            for (int i = 0; i < _Node.GetPostreqCount(); ++i)
            {
                AddPostreq(_Node.GetPostreq(i));
            }

        }

        public string GetVal()
        {
            return Value;
        }

        public int GetPrereqCount()
        {
            return Prereq.Count();
        }

        public string GetPrereq(int Idx)
        {
            return Prereq[Idx];
        }

        public int GetPrereqIdx(string Search)
        {
            return Prereq.IndexOf(Search);
        }

        public string GetPostreq(int Idx)
        {
            return Postreq[Idx];
        }

        public int GetPostreqIdx(string Search)
        {
            return Postreq.IndexOf(Search);
        }

        public int GetPostreqCount()
        {
            return Postreq.Count;
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

        public void PrintAllPostreq()
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
}
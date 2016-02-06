using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_project
{
    class Graph
    {
        public int Size,removed=0;
        public List<List<int>> adj = new List<List<int>>();
        public Graph(int v)
        {
            this.Size = v;
            for (int i = 0; i < Size; i++)
                adj.Add(new List<int>(Size));
        }
        public void AddEdge(int v,int w)
        {
            adj[v].Add(w);
            adj[w].Add(v);
            

        }
        public bool isComplete()
        {
            for (int i = 0; i < this.Size; i++)
            {
                if (adj[i].Count == 0)
                    continue;
                else if (adj[i].Count()!=(this.Size-1-this.removed))
                    return false;
            }
            return true;
        }
        public bool Cover(int A, int B)
        {
            if (adj[B].Count== 0)//B is already removed
                return false;
            if (adj[A].Contains(B) || adj[B].Contains(A))//there is an edge
                return false;
            if (adj[A].Count <= adj[B].Count)//A has more edges than B
                return false;
            for(int i=0;i<adj[B].Count;i++)// both have same edges
                if (!(adj[A].Contains(adj[B][i]))) 
                    return false;
            return true;
        }
        public bool Psuedo_Cover(int A, int B)
        {
            if (adj[A].Count == 0 || adj[B].Count == 0)//A or B is already removed
                return false;
            if (adj[A].Contains(B) || adj[B].Contains(A))// there is an edge between them
                return false;
            if (adj[A].Count != adj[B].Count)//dont have equal number of edges
                return false;
            for (int i = 0; i < adj[B].Count; i++)// both have same edges
                if (!(adj[A].Contains(adj[B][i])))
                    return false;
            return true;
        }
        public void Remove(int A)
        {
            adj[A].Clear();
            for(int i=0;i<this.Size;i++)
                if(adj[i].Contains(A))
                    adj[i].RemoveAt(adj[i].FindIndex(s=>s.Equals(A)));
            removed++;
        }
        public bool Non_Reducible()
        {
            if (this.isComplete())
                return false;
            for(int i=0;i<Size;i++)
            {
                for(int j=0;j<Size;j++)
                {
                    if (i!=j && (Cover(i, j) || Psuedo_Cover(i, j)))
                        return false;
                }
            }
            return true;
        }
    }
}

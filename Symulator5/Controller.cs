using Symulator5.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator5
{
    class Controller
    {
        List<Process> masterlist = new();
        List<CPU> CPUs = new();
        int processnum;
        int cpunum;
        int p, r, z, n;
        int maxcpuneed;
        int maxtimeentry;
        int maxtimeneeded;

        public int Processnum { get => processnum; set => processnum = value; }
        public int P { get => p; set => p = value; }
        public int R { get => r; set => r = value; }
        public int Z { get => z; set => z = value; }
        public int N { get => n; set => n = value; }
        public int Maxcpuneed { get => maxcpuneed; set => maxcpuneed = value; }
        public int Maxtimeentry { get => maxtimeentry; set => maxtimeentry = value; }
        public int Maxtimeneeded { get => maxtimeneeded; set => maxtimeneeded = value; }
        public int Cpunum { get => cpunum; set => cpunum = value; }

        public void populate()
        {
            Random rand = new();
            masterlist.Clear();
            for (int i = 0; i < processnum; i++)
            {
                masterlist.Add(new Process(rand.Next(maxcpuneed)+1, rand.Next(maxtimeentry)+1, rand.Next(maxtimeneeded)+1));
            }
            CPUs.Clear();
            for (int i = 0; i < cpunum; i++)
            {
                CPUs.Add(new CPU(i));
            }
        }
        public void debugpop()
        {
            masterlist.Clear();
            for (int i = 0; i < processnum; i++)
            {
                masterlist.Add(new Process(maxcpuneed,maxtimeentry, maxtimeneeded));
            }
            CPUs.Clear();
            for (int i = 0; i < cpunum; i++)
            {
                CPUs.Add(new CPU(i));
            }
        }
        public List<Result> Evaluate(List<Strategy> strategies)
        {
            List<Result> ret = new();
            foreach (var item in strategies)
            {
                item.Processes = masterlist.ToList() ;
                item.Processes.ForEach(e => e.reset());
                item.CPUs = CPUs;
                item.Controller = this;
                ret.Add(item.Evaluate());
                masterlist.ForEach(e => e.reset());
            }
            return ret;
        }
        
    }
}

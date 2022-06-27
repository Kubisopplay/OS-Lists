using LiveCharts;
using LiveCharts.Wpf;
using Symulator2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2
{
    class Controller
    {
        int reqnum;
        int pageNum;
        int processNum;
        int frameNum;
        List<Process> processes = new();
        public int Reqnum { get => reqnum; set => reqnum = value; }
        public int FrameNum { get => frameNum; set => frameNum = value; }
        public int PageNum { get => pageNum; set => pageNum = value; }
        public int ProcessNum { get => processNum; set => processNum = value; }

        public void populate()
        {
            processes.Clear();
            for (int i = 0; i < processNum; i++)
            {
                processes.Add(new());
            }
            Random rand = new();
            foreach (var item in processes)
            {
                //item.Requiredframes = rand.Next(frameNum / processNum)+1;
                var temp = TailoredRandom.LocalRandom(reqnum, pageNum);
                foreach (var item2 in temp)
                {
                    item.RequestList.Add(new Request(item2));
                }
                item.Masterlist = item.RequestList; //a magic
                
                var temp2 = item.RequestList.Distinct(new requestcomparer()); //more magic
                item.Requiredframes = temp2.Count();
            }
            
        }
        public List<Result> EvaluateAlgs(Algorithm[] alglist)
        {
            var ret = new List<Result>();
            
            foreach (var item in alglist)
            {
                processes.ForEach(e => e.Pagefaults = 0);
                item.Processes = processes;
                item.Maxframes = frameNum;
                item.Reqnum = reqnum;
                item.Processes.ForEach(e => e.ResetLists());
                ret.Add(item.Evaluate());
            }
            
            return ret;
        } 

    }
    class Result {
        int val;
        string name;

        public Result(int val, string name)
        {
            this.val = val;
            this.name = name;
        }

        public int Val { get => val; set => val = value; }
        public string Name { get => name; set => name = value; }
    }
    class requestcomparer : IEqualityComparer<Request>
    {
        public bool Equals(Request x, Request y)
        {
            return x.Equals(y);
        }

        public int GetHashCode([DisallowNull] Request obj)
        {
            return obj.GetHashCode();
        }
    }

}

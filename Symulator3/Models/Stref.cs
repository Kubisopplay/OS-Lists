using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class Stref : Algorithm
    {
        public Stref(string name) : base(name)
        {
        }

        public override Result Evaluate()
        {
            int framesleft = maxframes;
            foreach (var item in processes)
            {
                item.Framelenght++;
                framesleft--;
            }
            Dictionary<Process,HashSet<Request>> timewindow = new();
            foreach (var item in processes)
            {
                timewindow.Add(item, new());
            }
            reqnum = processes[0].RequestList.Count;
            for (int i = 0; i < reqnum; i++)
            {
                foreach (var item in processes)
                {
                    timewindow[item].Add(item.RequestList[0]);
                    item.EvaluateTick();
                }
                framesleft = maxframes - processes.Count;
                foreach (var item in processes)
                {
                    if (framesleft > timewindow[item].Count)
                    {
                        item.Framelenght = timewindow[item].Count;
                    }
                    else
                    {
                        item.Framelenght = Math.Clamp(timewindow[item].Count, 0, framesleft>0?framesleft-1:0)+1;
                    }
                    framesleft = Math.Clamp(framesleft - timewindow[item].Count, 0, framesleft);
                }
            }

            return new(processes.Sum(e => e.Pagefaults), this.name);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class Error : Algorithm
    {
        //na 10 tickow
        const int maxerrors = 7;
        const int minerrors = 2;
        int freeframes;
        public Error(string name) : base(name)
        {
        }
        public override Result Evaluate()
        {
            freeframes = maxframes;
            foreach (var item in processes)
            {
                item.Framelenght = (int)Math.Floor((double)maxframes / processes.Count);
            }
            reqnum = processes[0].RequestList.Count;
            for (int i = 0; i < reqnum; i++)
            {
                foreach (var item in processes)
                {
                    item.EvaluateTick();
                }
                if (i % 10 == 0)
                {

                    processes.FindAll(e => e.Pagefaults - faults[e] > maxerrors).ForEach(e =>
                    {
                        if (freeframes > 0)
                        {
                            freeframes--;
                            e.Framelenght++;
                        }
                    });
                    processes.FindAll(e => e.Pagefaults - faults[e] < minerrors).ForEach(e =>
                    {
                        if (e.Framelenght > 0)
                        {
                            freeframes++;
                            e.Framelenght--;
                        }
                    });
                    foreach (var item in processes)
                    {
                        faults[item] = item.Pagefaults;
                    }
}
}
            return new(processes.Sum(e => e.Pagefaults), this.name);
        }

    }
}

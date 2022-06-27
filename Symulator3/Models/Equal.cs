using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class Equal : Algorithm
    {
        public Equal(string name) : base(name)
        {
        }

        public override Result Evaluate()
        {
            foreach (var item in Processes)
            {
                item.Framelenght = (int)Math.Floor((double) maxframes / processes.Count);
            }
            reqnum = processes[0].RequestList.Count;
            for (int i = 0; i < reqnum; i++)
            {
                foreach (var item in Processes)
                {
                    item.EvaluateTick();
                }
            }
            return new(processes.Sum(e => e.Pagefaults), this.name);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class Proportional : Algorithm
    {
        public Proportional(string name) : base(name)
        {

        }
        public override Result Evaluate()
        {
            int framesleft = maxframes - processes.Count;
            foreach (var item in processes)
            {
                item.Framelenght = (item.Requiredframes-1 < framesleft ? item.Requiredframes-1 : framesleft)+1;
                framesleft = Math.Clamp(framesleft - item.Requiredframes, 0, maxframes);
            }
            reqnum = processes[0].RequestList.Count;
            for (int i = 0; i < reqnum; i++)
            {
                foreach (var item in processes)
                {
                    item.EvaluateTick();
                }
            }
            return new(processes.Sum(e => e.Pagefaults), this.name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator5.Helper
{
    class Strat2 : Strategy
    {
        public Strat2(string name) : base(name)
        {
        }

        public override Result Evaluate()
        {
            Random rand = new();
            return Eval(delegate (Process incoming)
            {
                var temp = CPUs[rand.Next(CPUs.Count)];
                if (temp.filledpercent < Controller.P || CPUs.Count == 1)
                {
                    temp.Activeprocesses.Add(incoming);
                    return new Migrationstats(0, 1);
                }
                if (CPUs.All(e => e.filledpercent > Controller.P))
                { //omijam nieskończone pętle
                    temp.Activeprocesses.Add(incoming);
                    return new Migrationstats(0, CPUs.Count);
                }
                int i = 0;
                int failsafe = 0;
                while (temp.filledpercent > Controller.P)
                {
                    temp = CPUs[rand.Next(CPUs.Count)];
                    i++;
                    if (failsafe > CPUs.Count * 10) break; //nie dziękuję, 
                }
                temp.Activeprocesses.Add(incoming);
                return new Migrationstats(1, i);

            });
        }
    }
}

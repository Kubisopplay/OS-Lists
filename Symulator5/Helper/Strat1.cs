using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator5.Helper
{
    class Strat1 : Strategy
    {
        public Strat1(string name) : base(name)
        {
        }

        public override Result Evaluate()
        {
            Random rand = new();
            return base.Eval(delegate(Process incoming) {
                var temp = CPUs[rand.Next(CPUs.Count)];
                if (temp.filledpercent < Controller.P||CPUs.Count==1)
                {
                    temp.Activeprocesses.Add(incoming);
                    return new Migrationstats(0, 1);
                }
                var temp2 = temp;
                for (int i = 0; i < Controller.Z; i++)
                {
                    if (temp2.filledpercent < Controller.P)
                    {
                        temp2.Activeprocesses.Add(incoming);
                        return new Migrationstats(1, i + 2);
                    }
                    temp2 = CPUs[rand.Next(CPUs.Count)];
                }
                temp.Activeprocesses.Add(incoming);
                return new Migrationstats(1, Controller.Z + 2);
            }
                );
        }
    }
}

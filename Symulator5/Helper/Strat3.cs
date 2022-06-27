using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator5.Helper
{
    class Strat3 : Strategy
    {
        public Strat3(string name) : base(name)
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
                int j = 1;
                int failsafe = 0;
                while (temp.filledpercent > Controller.P)
                {
                    failsafe++;
                    temp = CPUs[rand.Next(CPUs.Count)];
                    i++;
                    //litosci nad moim biednym procesorem
                   /* if (failsafe < CPUs.Count * 10) Parallel.ForEach(CPUs.FindAll(e => e.filledpercent < Controller.R), item =>
                    {
                        var temp3 = CPUs.FindAll(e => e.filledpercent > Controller.P);
                        if (temp3.Count != 0)
                        {
                            i += temp3.Count;
                            var temp4 = temp3[rand.Next(temp3.Count)];
                            j += (int)temp4.Activeprocesses.Count / 2;
                            i += (int)temp4.Activeprocesses.Count / 2;
                            item.Activeprocesses.AddRange(temp4.Activeprocesses.GetRange(0, (int)temp4.Activeprocesses.Count / 2));
                            temp4.Activeprocesses.RemoveRange(0, (int)temp4.Activeprocesses.Count / 2);
                        }
                    });
                   */
                    foreach (var item in CPUs.FindAll(e=>e.filledpercent<Controller.R))
                    {
                        var temp3 = CPUs.FindAll(e => e.filledpercent > Controller.P);
                        if (temp3.Count == 0) continue;
                        i += temp3.Count;
                        var temp4 = temp3[rand.Next(temp3.Count)];
                        j += (int)temp4.Activeprocesses.Count / 2;
                        i += (int)temp4.Activeprocesses.Count / 2;
                        item.Activeprocesses.AddRange(temp4.Activeprocesses.GetRange(0, (int)temp4.Activeprocesses.Count / 2));
                        temp4.Activeprocesses.RemoveRange(0, (int)temp4.Activeprocesses.Count / 2);

                    }
                    if (failsafe > CPUs.Count * 100) break; //nieszczególnie mnie cieszy program zacinający mi komputer na 10 minut próbując wylosować właściwe cpu
                }
                temp.Activeprocesses.Add(incoming);
                return new Migrationstats(j, i);

            });
        }
    }
}

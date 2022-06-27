using Symulator1.Models.Comparators;
using Symulator1.Models.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Symulator1.Models.Helper.HddAlg;

namespace Symulator1.Models.HDDscheduling
{
    class EDF : HddAlg
    {
        public EDF(string name, int maxlen) : base(name, maxlen)
{
}
        
public override int evaluate()
{
            List<HDDRequest> prioritylist = new List<HDDRequest>();
            Dropped = 0;
            return AdvEvaluate(delegate (HddAlg alg, List<HDDRequest> queue)
            {
                prioritylist.AddRange(List.FindAll(e => e.Realtime && e.EntryTime == CurrentTime));
                prioritylist.Sort(Comparer<HDDRequest>.Create((o1, o2) =>
                {
                    return (o1.Deadline+o1.EntryTime).CompareTo(o2.Deadline+ o2.EntryTime);
                }));
                queue.Sort(new HddEntryComparator());//powinno działać
                //można zmienić to na sstf zmieniając tą lambdę, mogłoby to na przykład działać

                if (prioritylist.Count > 0)
                {
                    if (prioritylist[0].Cylinder == HeadLocation)
                    {
                        if ( prioritylist[0].Duration==0)
                        {
                            prioritylist.RemoveAt(0);
                        }
                        else
                        {
                            prioritylist[0].Duration--;
                        }
                    }
                    else
                    {
                        HeadLocation += Math.Sign(prioritylist[0].Cylinder - HeadLocation);
                        HeadTravel++;
                    }
                    Dropped += prioritylist.RemoveAll(e => e.EntryTime + e.Deadline < CurrentTime);
                }
                else if (queue.Count > 0)
                {
                    if (queue[0].Cylinder == HeadLocation)
                    {
                        if (queue[0].Duration == 0)
                        {
                            queue.RemoveAt(0);
                        }
                        else
                        {
                           queue[0].Duration--;
                        }
                    }
                    else
                    {
                        HeadLocation += Math.Sign(queue[0].Cylinder - HeadLocation);
                        HeadTravel++;
                    }
                }
                
            });
        }
    }
    
}

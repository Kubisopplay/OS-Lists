using Symulator1.Models.Comparators;
using Symulator1.Models.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.HDDscheduling
{
    class SSTF : HddAlg
    {
        public SSTF(string name, int maxlen) : base(name, maxlen)
        {
        }

        public override int evaluate()
        {
            List.Sort(new HddEntryComparator());
            int headTravel = 0;
            int headLocation = maxblock / 2;
            int currentTime = 0;
            HDDRequest headLoc = new HDDRequest(-1, headLocation, -1); //brudny sposób żeby zmusić comparator do działania
            List<HDDRequest> queue = new List<HDDRequest>();
            var sstfcomp = Comparer<HDDRequest>.Create((head, o2) =>
            {
                return (head.Cylinder - headLoc.Cylinder).CompareTo(o2.Cylinder - headLoc.Cylinder);
            });
            do
            {
                bool done = List.TrueForAll((e) =>
                {
                    return e.EntryTime < currentTime;
                });
                for (int i = 0; i < List.Count; i++)
                {

                    if (currentTime == List[i].EntryTime)
                    {
                        queue.Add(List[i]);
                    }
                }
                queue.Sort(sstfcomp);
                if (queue.Count > 0)
                {
                    var top = queue[0];
                    if (headLocation == top.Cylinder)
                    {
                        queue.RemoveAt(0);
                    }
                    else
                    {
                        headLocation += Math.Sign(top.Cylinder - headLocation);
                        headTravel++;
                    }
                }
                headLoc.Cylinder = headLocation;
                currentTime++;
                if (done && queue.Count == 0)
                {
                    break;
                }
            } while (true);
            return headTravel;
            //return base.evaluate();
        }
    }
}

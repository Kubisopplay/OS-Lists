using Symulator1.Models.Comparators;
using Symulator1.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.HDDscheduling
{
    class FCFS : HddAlg
    {
        public FCFS(string name) : base(name)
        {
            maxblock = 200;
        }
        public FCFS(string name, int maxlen) : base(name, maxlen)
        {
            maxblock = maxlen;
        }
        public override int evaluate()
        {
            List.Sort(new HddEntryComparator());
            int headTravel = 0;
            int headLocation = maxblock / 2;
            foreach (var item in List)
            {
                headTravel += Math.Abs(headLocation - item.Cylinder);
                headLocation = item.Cylinder;
            }
            return headTravel;
        }

    }
}

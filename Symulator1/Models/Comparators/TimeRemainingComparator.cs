using Symulator1.Models.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.Comparators
{
    class TimeRemainingComparator : IComparer<Helper.Process>
    {
        public int Compare(Process x, Process y)
        {
            return x.TimeLeft - y.TimeLeft;
        }
    }
}

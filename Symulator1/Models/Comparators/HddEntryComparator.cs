using Symulator1.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.Comparators
{
    class HddEntryComparator : IComparer<HDDRequest>
    {
        public int Compare(HDDRequest x, HDDRequest y)
        {
            return x.EntryTime.CompareTo(y.EntryTime);
        }
    }
}

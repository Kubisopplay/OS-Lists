using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.Comparators
{ class EntryPointComparator : IComparer<Helper.Process>
    {

        public int Compare(Helper.Process x, Helper.Process y)
        {
            return x.EntryMoment - y.EntryMoment;
        }
    }
    
}

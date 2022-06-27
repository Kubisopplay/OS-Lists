using Symulator1.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.Comparators
{
    class DurationComparator : IComparer<Helper.Process>
    {

        int IComparer<Process>.Compare(Process x, Process y)
        {
            return x.Duration - y.Duration;
        }
    }
}

using Symulator1.Models.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.HDDscheduling
{
    class CSCAN : HddAlg
    {
        public CSCAN(string name, int maxlen) : base(name, maxlen)
        {

        }
        public override int evaluate()
        {
            return AdvEvaluate(delegate (HddAlg alg, List<HDDRequest> queue)
            {
                alg.HeadLocation++;
                alg.HeadTravel++;
                if (HeadLocation > maxblock)
                {
                    HeadLocation = 0;
                }
                var temp = queue.RemoveAll(e => e.Cylinder == HeadLocation);
            });
            return base.evaluate();
        }
    }
}

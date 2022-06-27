using Symulator1.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.HDDscheduling
{
    internal class SCAN : HddAlg
    {
        bool Right = true;
        public SCAN(string name, int maxlen) : base(name, maxlen)
        {
        }
        public override int evaluate()
        {
            /*
            int currentTime = 0;
            int headTravel = 0;
            int headLocation = maxblock / 2;
            List<HDDRequest> queue = new List<HDDRequest>();
            do
            {

            } while (true);
            return base.evaluate();
            */
            return AdvEvaluate(delegate (HddAlg alg, List<HDDRequest> queue)
            {
                if (Right)
                {
                    this.HeadLocation++;
                    this.HeadTravel++;
                    if (this.HeadLocation == this.maxblock)
                    {
                        Right = false;
                    }
                }
                else
                {
                    this.HeadLocation--;
                    this.HeadTravel++;
                    if (this.HeadLocation == 0)
                    {
                        Right = true;
                    }
                }
                var temp = queue.FindAll((e) => e.Cylinder == this.HeadLocation);
                foreach (var item in temp)
                {
                    queue.Remove(item);
                }

            });
        }
    }
}

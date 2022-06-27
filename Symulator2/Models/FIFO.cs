using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class FIFO : Algorithm
    {
        public FIFO(string name) : base( name)
        {
        }

        public override int Eval()
        {
            int firstloc = 0;
            var queue = new Queue<int>();
            return Evaluate(delegate (int needed)
            {
                if (queue.Contains(needed)) return false;
                else
                {
                    if(queue.Count>=Frames.Length) queue.Dequeue();
                    queue.Enqueue(needed);
                    return true;
                }
                
            });
        }
    }
}

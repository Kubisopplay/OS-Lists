using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class ALRU : Algorithm
    {
        public ALRU(string name) : base(name)
        {
        }
        public override int Eval()
        {
            int firstloc = 0;
            var queue = new Queue<KVPair>();
            return Evaluate(delegate (int needed)
            {
                if (queue.Contains(new KVPair(needed, false))|| queue.Contains(new KVPair(needed, true)))
                {
                    queue.Enqueue(queue.Dequeue());
                    return false;
                }
                else
                {
                    while (queue.Count >= Frames.Length)
                    {
                        if(queue.Peek().Bit)
                        {
                            queue.Peek().Bit = false;
                            queue.Enqueue(queue.Dequeue());
                        }
                        else
                        {
                            queue.Dequeue();
                        }
                    }
                    queue.Enqueue(new KVPair(needed, true));
                    return true;
                }

            });
        }
        
        class KVPair
        {
            int key;
            bool bit;

            public KVPair(int key, bool bit)
            {
                this.key = key;
                this.bit = bit;
            }

            public int Key { get => key; set => key = value; }
            public bool Bit { get => bit; set => bit = value; }

            public override bool Equals(object obj)
            {
                return obj is KVPair pair &&
                       key == pair.key;
            }
        }
    }
}

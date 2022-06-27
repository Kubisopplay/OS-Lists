using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class LRU : Algorithm
    {
        public LRU(string name) : base(name)
        {
        }
        public override int Eval()
        {
            List<int> framescheck = new List<int>(Frames.Length);
            for (int i = 0; i < Frames.Length; i++)
            {
                Frames[i] = 0;
                framescheck.Add(-1 );
            }
            int lenght = Frames.Length;
            return Evaluate(delegate (int needed)
            {
                if (framescheck.Contains(needed)) {
                    Frames[framescheck.IndexOf(needed)] = 0;
                    return false;
                } 
                else
                {
                    if (framescheck.Contains(-1))
                    {
                        for (int i = 0; i < framescheck.Count; i++)
                        {
                            if (framescheck[i] == -1)
                            {
                                framescheck[i] = needed;
                                break;
                            }
                        }
                        for (int i = 0; i < framescheck.Count; i++) if (framescheck[i] != -1) Frames[i]++;
                        return true;
                    }

                    for (int i = 0; i < framescheck.Count; i++)
                    {
                        if (Frames[i] == Frames.Max())
                        {
                            framescheck[i] = needed;
                            Frames[i] = 0;
                            break;
                        }
                    }
                    for (int i = 0; i < framescheck.Count; i++) if(framescheck[i]!=-1) Frames[i]++;
                    return true;
                }
            });
        }
    }
}

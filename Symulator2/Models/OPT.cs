using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Symulator2.Models
{
    class OPT : Algorithm
    {
        
        public OPT(string name) : base(name)
        {
        }
        public override int Eval()
        {
            int reqind = -1;
            List<int> ram = new();
            return Evaluate(delegate (int needed)
            {
                reqind++;
                if (ram.Contains(needed)) return false;
                else
                {
                    if (ram.Count < Frames.Length)
                    {
                        ram.Add(needed);
                        return true;
                    }
                    else
                    {
                        Dictionary<int, int> temp = new();
                        for (int i = reqind; i < Requests1.Length; i++)
                        {
                            if (ram.Contains(Requests1[i])&&!temp.Values.Contains(Requests1[i])) temp.Add(i-reqind, Requests1[i]);
                            if (temp.Count == ram.Count) break;
                        }
                        if (temp.Count < ram.Count)
                        {
                            int e = 0;
                            foreach (var item in ram)
                            {
                                if (!temp.Values.Contains(item)) temp.Add(e--, item);
                            }
                        }
                        ram.Remove(temp[temp.Keys.Max<int>()]);
                        ram.Add(needed);
                        return true;
                    }
                }
               
            });
        }
    }
}

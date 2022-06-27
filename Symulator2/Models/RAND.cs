using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class RAND : Algorithm
    {
        public RAND(string name) : base(name)
        {
        }
        public override int Eval()
        {
            List<int> e = new();
            Random r = new();
            return Evaluate(delegate (int needed)
            {
                if (e.Contains(needed)) return false;
                else
                {
                    if (e.Count > Frames.Length) e.RemoveAt(r.Next(0, e.Count));
                    e.Add(needed);
                    return true;
                }
            });
        }
    }
}

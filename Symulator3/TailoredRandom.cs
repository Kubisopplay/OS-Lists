using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2
{
    class TailoredRandom
    {
        public static List<int> LocalRandom(int length, int maxdisc)
        {
            List<int> hotspots = new();

            Random rand = new(2137);
            List<int> ret = new();
            for (int i = 0; i <(int)Math.Floor( Math.Sqrt(length))*2; i++)
            {
                hotspots.Add(rand.Next(maxdisc));
            }
            for (int i = 0; i < length; i++)
            {
                ret.Add(Math.Clamp(hotspots[rand.Next(hotspots.Count-1)] + rand.Next((int)((int)Math.Floor(Math.Sqrt(maxdisc)) - Math.Floor(Math.Sqrt(maxdisc)) / 2)),0,maxdisc));
            }
            return ret;
        }
    }
}

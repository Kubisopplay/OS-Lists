using Symulator1.Models.Comparators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.Helper
{
    class HddAlg
    {
        List<HDDRequest> list;
        String name;
        protected int maxblock;
         int dropped = 0;
        public HddAlg(string name)
        {
            this.name = name;
        }
        public HddAlg (string name, int maxlen)
        {
            this.name = name;
            maxblock = maxlen;
            HeadLocation = maxblock / 2;
        }
        public string Name { get => name; set => name = value; }
        internal List<HDDRequest> List { get => list; set => list = value; }
        public int HeadTravel { get => headTravel; set => headTravel = value; }
        public int HeadLocation { get => headLocation; set => headLocation = value; }
        public int CurrentTime { get => currentTime; set => currentTime = value; }
        public int Dropped { get => dropped; set => dropped = value; }

        int headTravel = 0;
        int headLocation = 0;
        int currentTime = 0;
        public virtual int evaluate()
        {
            return 0;
        }
        public delegate void Alg(HddAlg hdd, List<HDDRequest> queue);

        public int AdvEvaluate(Alg delegated)
        {
            List.Sort(new HddEntryComparator());
            
            //HDDRequest headLoc = new HDDRequest(-1, headLocation, -1); //brudny sposób żeby zmusić comparator do działania
            List<HDDRequest> queue = new List<HDDRequest>();
            do
            {
                bool done = List.TrueForAll((e) =>
                {
                    return e.EntryTime < currentTime;
                });
                for (int i = 0; i < List.Count; i++)
                {

                    if (currentTime == List[i].EntryTime && (list[i].Realtime == null || !list[i].Realtime))
                    {
                        queue.Add(List[i]);
                    }
                }
                delegated(this, queue);
               // headLoc.Cylinder = headLocation;
                currentTime++;
                if (done && queue.Count == 0)
                {
                    break;
                }
            } while (true);
            return headTravel;

        }
    }
}

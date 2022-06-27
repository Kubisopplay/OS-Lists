using Symulator1.Models;
using Symulator1.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1
{
    class HddController
    {
        int procAmount;
        List<HDDRequest> list;
        Dictionary<string,RngControl> rngControls;
        int realtimePercent = 20;


        public int ProcAmount { get => procAmount; set => procAmount = value; }
        public int RealtimePercent { get => realtimePercent; set => realtimePercent = value; }

        public HddController(Dictionary<string, RngControl> rng_dict,int proc)
        {
            procAmount = proc;
            rngControls = rng_dict;
        }
        public void populateRandom()
        {
            Random rand = new Random(2137);
            list = new List<HDDRequest>();
            var cylinders = rngControls["cylinder"].AdvancedRandom(procAmount);
            var entry = rngControls["entry"].AdvancedRandom(procAmount);
            var deadline = rngControls["deadline"].AdvancedRandom(procAmount);
            for (int i = 0; i < procAmount; i++)
            {
                HDDRequest temp = new HDDRequest(i, cylinders[i], entry[i],rand.Next(0,10),deadline[i],rand.Next(0,100)<realtimePercent);
                list.Add(temp);
            }

        }
        public List<HDDResult> evaluateAlgorithm(HddAlg[] algs)
        {
            List < HDDResult > results = new List<HDDResult>();
            foreach (var item in algs)
            {
                item.List = copylist(list);
                var temp = item.evaluate();
                if (item.Dropped > 0) {
                    results.Add(new HDDResult(temp, item.Name,item.Dropped));
                }
                else
                {
                    results.Add(new HDDResult(temp, item.Name));
                }
            }
            return results;
        }
        internal class HDDResult
        {
            int value;
            string name;
            int dropped;

            public HDDResult(int value, string name)
            {
                this.value = value;
                this.name = name;
                this.dropped = 0;
            }
                public HDDResult(int value, string name, int dropped)
            {
                this.value = value;
                this.name = name;
                this.dropped = dropped;
            }

            public int Value { get => value; set => this.value = value; }
            public string Name { get => name; set => name = value; }
            public int Dropped { get => dropped; set => dropped = value; }
        }
        List<HDDRequest> copylist(List<HDDRequest> copy)
        {
            list = new List<HDDRequest>();
            if (copy == null) return null;
            foreach (var item in copy)
            {
                list.Add(item);
            }
            return list;
        }
        
    }
}

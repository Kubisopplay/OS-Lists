using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class Algorithm
    {
        protected String name;
        protected List<Process> processes = new();
        protected int reqnum;
        protected int maxframes;
        internal List<Process> Processes { get => processes; set { processes = value;
                faults.Clear();
                foreach (var item in processes)
                {
                    faults.Add(item, 0);
                }
            } }

        public int Reqnum { get => reqnum; set => reqnum = value; }
        public int Maxframes { get => maxframes; set => maxframes = value; }

        protected Dictionary<Process, int> faults = new();

        public Algorithm(string name)
        {
            this.name = name;
        }

        public virtual Result Evaluate()
        {
            return new Result(0,name);
        }
    }
}

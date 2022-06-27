using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator5.Helper
{
    class CPU
    {
        List<Process> activeprocesses = new();
        bool changed = true;
        private int fill = 0;
        public int filledpercent { get {
                if (changed) { fill = activeprocesses.Sum(e => e.NeededCPU);
                    changed = false;
                }
                return fill;
            } }
        internal List<Process> Activeprocesses { get {
                changed = true;
                return activeprocesses; 
            }
            set {
                activeprocesses = value;
                changed = true;
                    } 
        }
        int id;

        public CPU(int id)
        {
            this.id = id;
        }

        public void Tick()
        {
            changed = true;
            if (filledpercent <= 100)
            {
                foreach (var item in activeprocesses)
                {
                    item.Timeleft--;
                }
                activeprocesses.RemoveAll(e => e.Timeleft <= 0);
            } else if (filledpercent > 100)
            {
                int temp = 0;
                foreach (var item in activeprocesses)
                {
                    if (temp <= 100)
                    {
                        temp += item.NeededCPU;
                        item.Timeleft--;
                    }
                }
                activeprocesses.RemoveAll(e => e.Timeleft <= 0);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator5.Helper
{
    class Process
    {
        int neededCPU;
        int entrytime;
        int timeneeded;
        int timeleft;

        public Process(int neededCPU, int entrytime, int timeneeded)
        {
            this.neededCPU = neededCPU;
            this.entrytime = entrytime;
            this.timeneeded = timeneeded;
            this.timeleft = timeneeded;
        }
        public void reset()
        {
            timeleft = timeneeded;
        }
        public int NeededCPU { get => neededCPU; set => neededCPU = value; }
        public int Entrytime { get => entrytime; set => entrytime = value; }
        public int Timeneeded { get => timeneeded; set => timeneeded = value; }
        public int Timeleft { get => timeleft; set => timeleft = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.Helper
{
    class Process
    {
       private int processNum;
       private int duration;
       private int entryMoment;
       private int timeLeft;

        public Process(int process_num, int duration, int entry_moment, int time_left)
        {
            this.ProcessNum = process_num;
            this.Duration = duration;
            this.EntryMoment = entry_moment;
            this.TimeLeft = time_left;
            WaitingTime = 0;
        }
        public Process(Process proc)
        {
            this.ProcessNum = proc.ProcessNum;
            this.Duration = proc.Duration;
            this.EntryMoment = proc.EntryMoment;
            this.TimeLeft = proc.TimeLeft;
            this.WaitingTime = proc.WaitingTime;
        }
       
        public int WaitingTime { get; set; }
        public int ProcessNum { get => processNum; set => processNum = value; }
        public int Duration { get => duration; set => duration = value; }
        public int EntryMoment { get => entryMoment; set => entryMoment = value; }
        public int TimeLeft { get => timeLeft; set => timeLeft = value; }

        public override bool Equals(object obj)
        {
            return obj is Process process &&
                   processNum == process.processNum;
        }
    }
}

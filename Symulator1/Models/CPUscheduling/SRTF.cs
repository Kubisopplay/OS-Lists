using Symulator1.Models.Comparators;
using Symulator1.Models.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.Scheduling
{
    class SRTF : Algorithm
    {
        public SRTF(string name) : base(name)
        {
        }

        public override Result evaluate()
        {
            ProcessList.Sort(new EntryPointComparator());
            List<Process> activeprocesses = new List<Process>();
            int currentTime = 0;
            do
            {
                bool done = false;
                for (int i = 0; i < ProcessList.Count; i++)
                {
                    done = false;
                    if (currentTime == ProcessList[i].EntryMoment)
                    {
                        activeprocesses.Add(ProcessList[i]);
                        activeprocesses.Sort(new TimeRemainingComparator());
                    }
                    done = currentTime > ProcessList[i].EntryMoment;

                }
                if(activeprocesses.Count>0)activeprocesses[0].TimeLeft -= 1;
                currentTime++;
                if (activeprocesses.Count > 0&&activeprocesses[0].TimeLeft == 0)
                {
                    Process temp = ProcessList.Find((e) =>
                    {
                        return e.Equals(activeprocesses[0]);
                    });
                    ProcessList.Find((e) =>
                    {
                        return e.Equals(activeprocesses[0]);
                    }).WaitingTime = currentTime - temp.Duration- temp.EntryMoment;
                    activeprocesses.RemoveAt(0);
                    activeprocesses.Sort(new TimeRemainingComparator());
                }
                if (activeprocesses.Count == 0 && done)
                {
                    break;
                }
            } while (currentTime < 1000000);
            List<int> wait = new List<int>();
            foreach (Process i in ProcessList)
            {
                wait.Add(i.WaitingTime);
            }
            return new Result(wait, Name);
        }
    }
}

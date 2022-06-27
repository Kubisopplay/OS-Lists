using Symulator1.Models.Comparators;
using Symulator1.Models.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Process = Symulator1.Models.Helper.Process;

namespace Symulator1.Models.Scheduling
{
    class FCFS : Algorithm
    {
        public FCFS(string name) : base(name)
        {
            this.Name = name;
        }

      public override Result evaluate()
       {
            ProcessList.Sort(new EntryPointComparator());
            int finishtime = ProcessList[0].Duration;
            for (int i = 1; i < ProcessList.Count; i++)
            {
                Process process = ProcessList[i];
                if (finishtime <= process.EntryMoment)
                {
                    finishtime = process.EntryMoment + process.Duration;
                }
                else
                {
                    finishtime += process.Duration;
                }
                //czas oczekiwania to suma oczekiwania i trwania poprzedniego, minus różnica pomiędzy czasami wejścia
                ProcessList[i].WaitingTime = ProcessList[i - 1].WaitingTime + ProcessList[i - 1].Duration - (process.EntryMoment - ProcessList[i - 1].EntryMoment);
            }
            List<int> wait = new List<int>();
            foreach (Process i in ProcessList) {
                wait.Add(i.WaitingTime);
            }
            return new Result(wait, Name);

            
            
        }

    }
    
}

using Symulator1.Models.Comparators;
using Symulator1.Models.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Process = Symulator1.Models.Helper.Process;

namespace Symulator1.Models.Scheduling
{
    class RR : Algorithm
    {
int maxq;


        public RR(string name) : base(name)
        {
            
        }
        public RR(string name, int q) : base(name)
        {
            this.maxq = q;
        }

        public override Result evaluate()
        {
            int max_quant = maxq;
            int currentTime = 0;
            int quant = max_quant;
            int totaltime = 0;
            Queue<Process> queue = new Queue<Process>();
            ProcessList.Sort(new EntryPointComparator());
                do
                {
                bool done = false;
                for (int i = 0; i < ProcessList.Count; i++)
                {
                    done = false;
                    if (currentTime == ProcessList[i].EntryMoment)
                    {
                        queue.Enqueue(ProcessList[i]);
                    }
                    done = currentTime > ProcessList[i].EntryMoment;
                }
                currentTime++;
                quant--;
                if (queue.Count > 0) queue.Peek().TimeLeft -= 1;
                if (queue.Count>0&&queue.Peek().TimeLeft == 0)
                {
                    queue.Peek().WaitingTime = currentTime-queue.Peek().Duration - queue.Peek().EntryMoment;
                    queue.Dequeue().TimeLeft = 0;
                    quant = max_quant;
                }
                if (queue.Count > 0 && quant == 0)
                {
                    queue.Enqueue(queue.Dequeue());
                    quant = max_quant;

                }
                
                if (queue.Count == 0 && done)
                {
                    break;
                }
                } while (currentTime < 100000);
            List<int> wait = new List<int>();
            foreach (Process i in ProcessList)
            {
                wait.Add(i.WaitingTime);
            }
            return new Result(wait, Name);
        }

    }
}

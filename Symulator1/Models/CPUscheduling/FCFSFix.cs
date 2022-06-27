using Symulator1.Models.Comparators;
using Symulator1.Models.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Symulator1.Models.Scheduling
{
    class FCFSFix : Algorithm
    {
        public FCFSFix(string name) : base(name)
        {
            this.Name = name;
        }

        public override Result evaluate()
        {
            ProcessList.Sort(new EntryPointComparator());
            int currentTime = 0;
            Queue<Process> queue = new Queue<Process>();

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
                if (queue.Count > 0) queue.Peek().TimeLeft -= 1;
                currentTime++;
                if (queue.Count > 0 && queue.Peek().TimeLeft == 0)
                {
                    queue.Peek().WaitingTime = currentTime - queue.Peek().Duration - queue.Peek().EntryMoment;
                    queue.Dequeue().TimeLeft = 0;
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

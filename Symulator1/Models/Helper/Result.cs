using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.Helper
{
    public class Result 
    {
        List<int> waitingTimes = null;
        float max, min, avg;
        String name;

        public Result(string name)
        {
            this.name = name;
            waitingTimes =  new List<int>();
        }

        public float Max { get => max; set => max = value; }
        public float Min { get => min; set => min = value; }
        public float Avg { get => avg; set => avg = value; }
        public string Name { get => name; set => name = value; }
        public List<int> WaitingTimes { get => waitingTimes; set => waitingTimes = value; }

        public Result()
        {
            waitingTimes = new List<int>();
        }
        public Result(List<int> waitingTimes, String name)
        {
            this.waitingTimes = waitingTimes;
            this.name = name;
        }
       
        public void AddResult(int i)
        {
            waitingTimes.Add(i);
        }
        public void Evaluate()//liczy średnie, i inne rzeczy
        {
            var temp = 0;
            if (waitingTimes == null||waitingTimes.Count==0)
            {
                Max = 0;
                Min = 0;
                Avg = 0;
            }
            else if(waitingTimes.Count>0)
            {
                Max = 0;
                Min = waitingTimes[0]; 
                foreach (int i in waitingTimes)
                {
                    if (Max < i) Max = i;
                    if (Min > i) Min = i;
                    temp += i;
                }
                Avg = (float)temp / (float)waitingTimes.Count;
            }
            
        }
    }
}

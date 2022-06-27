using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class Algorithm
    {
        int[] Requests;
        int[] frames;
        string name;
         object algSpecificData;
        public string Name { get => name; set => name = value; }
        public object AlgSpecificData { get => algSpecificData; set => algSpecificData = value; }
        public int[] Frames { get => frames; set => frames = value; }
        public int[] Requests1 { get => Requests; set => Requests = value; }

        public Algorithm(int framelen, string name)
        {
            this.name = name;
            frames = new int[framelen];
        }

        public Algorithm(string name)
        {
            this.name = name;
        }

        public void ResetFrames(int num)
        {
            frames = new int[num];
        }
        public delegate bool Alg(int needed);

        protected int Evaluate(Alg e)
        {
            int pagefaults = 0;
            foreach (var item in Requests)
            {
                pagefaults += e(item) ? 1 : 0;
            }
            return pagefaults;
        }
        public virtual int Eval()
        {
            return 1;
        }
    }
}

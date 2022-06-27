using LiveCharts;
using LiveCharts.Wpf;
using Symulator2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2
{
    class Controller
    {
        int[] Requests;
        int reqnum;
        int[] frameNum;
        int pageNum;


        public int Reqnum { get => reqnum; set => reqnum = value; }
        public int[] FrameNum { get => frameNum; set => frameNum = value; }
        public int PageNum { get => pageNum; set => pageNum = value; }

        public void populate()
        {
            Requests = new int[reqnum];
            Random random = new(2137);
            for (int i = 0; i < reqnum; i++)
            {
                Requests[i] = random.Next(0, pageNum);
            }
        }
        public List<Result> EvaluateAlgs(Algorithm[] alglist)
        {
            var ret = new List<Result>();
            
            foreach (var item in alglist)
            {

                foreach (var num in frameNum)
                {
                    item.Requests1 = Requests;
                    item.ResetFrames(num);
                    ret.Add(new Result(item.Eval(), item.Name + " " + num + " Frames"));

                }
               
            }
            
            return ret;
        } 

    }
    class Result {
        int val;
        string name;

        public Result(int val, string name)
        {
            this.val = val;
            this.name = name;
        }

        public int Val { get => val; set => val = value; }
        public string Name { get => name; set => name = value; }
    }


}

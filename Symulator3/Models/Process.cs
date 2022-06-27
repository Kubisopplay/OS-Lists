using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Symulator2.Models.Algorithm;

namespace Symulator2.Models
{
    class Process
    {
        List<Request> requestList = new();
        List<Request> masterlist = new();
        List<Request> frames = new();
        int pagefaults = 0;
        int framelenght = 3;
        int requiredframes = 1;
  
        internal List<Request> RequestList { get => requestList; set {
                requestList = value;
            } 
        }

        public void ResetLists()
        {
            requestList = new List<Request>(masterlist.ToArray());
            foreach (var item in requestList)
            {
                item.Lastuse = 0;
            }
            frames.Clear();
            pagefaults = 0;

        }
        public int Framelenght { get => framelenght; set => framelenght = value; }
        public int Requiredframes { get => requiredframes; set => requiredframes = value; }
        public int Pagefaults { get => pagefaults; set => pagefaults = value; }
        public  List<Request> Masterlist { get => masterlist; set {
                masterlist.Clear();
                foreach (var item in value)
                {
                    masterlist.Add(new(item.Path));
                }
            } }

        public Request GetNext()
        {
            var temp = requestList[0];
            requestList.RemoveAt(0);
            return temp;
        }

        public void EvaluateTick()
        {
            Request needed = this.GetNext();
            foreach (var item in frames)
            {
                item.Lastuse++;
            }
            while (frames.Count > framelenght) frames.Remove(frames.Find(e => e.Lastuse == frames.Max(e => e.Lastuse)));
            if (frames.Contains(new Request(needed.Path)))
            {
                frames.Find(e => e.Path == needed.Path).Lastuse = 0;
                return;
            }
            else if (frames.Count < framelenght) {
                frames.Add(needed);
                pagefaults++;
                return;
            }
            else if(frames.Count==framelenght) {
                frames.Remove(frames.Find(e => e.Lastuse == frames.Max(e => e.Lastuse)));
                frames.Add(needed);
                pagefaults++;
            }

            
        }

        public override bool Equals(object obj)
        {
            return obj is Process process &&
                   EqualityComparer<List<Request>>.Default.Equals(masterlist, process.masterlist) &&
                   framelenght == process.framelenght &&
                   requiredframes == process.requiredframes;
        }
    }
}

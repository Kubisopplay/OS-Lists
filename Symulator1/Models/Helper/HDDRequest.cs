using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models.Helper
{
    class HDDRequest
    {
        private int requestnum;
        private int cylinder;
        private int entryTime;
        private int duration;
        private int deadline;
        private bool realtime;

        public HDDRequest(int requestnum, int cylinder,int entry)
        {
            this.requestnum = requestnum;
            this.cylinder = cylinder;
            this.entryTime = entry;
            this.realtime = false;
            this.duration = 0;
            this.deadline = 0;
        }

        public HDDRequest(int requestnum, int cylinder,int entry, int duration, int deadline, bool realtime)
        {
            this.requestnum = requestnum;
            this.cylinder = cylinder;
            this.duration = duration;
            this.deadline = deadline;
            this.entryTime = entry;
            this.realtime = realtime;
        }
        public HDDRequest(HDDRequest req)
        {
            this.requestnum = req.requestnum;
            this.cylinder = req.cylinder;
            this.duration = req.duration;
            this.deadline = req.deadline;
            this.entryTime = req.entryTime;
            this.realtime = req.realtime;
        }

        public int Requestnum { get => requestnum; set => requestnum = value; }
        public int Cylinder { get => cylinder; set => cylinder = value; }
        public int EntryTime { get => entryTime; set => entryTime = value; }
        public int Duration { get => duration; set => duration = value; }
        public int Deadline { get => deadline; set => deadline = value; }
        public bool Realtime { get => realtime; set => realtime = value; }

        public override bool Equals(object obj)
        {
            return obj is HDDRequest request &&
                   requestnum == request.requestnum;
        }

    }
}

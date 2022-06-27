using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator2.Models
{
    class Request
    {
        int path;
        int lastuse;

        public Request(int path)
        {
            this.path = path;
            this.lastuse = 0;
        }

        public int Path { get => path; set => path = value; }
        public int Lastuse { get => lastuse; set => lastuse = value; }

        public override bool Equals(object obj)
        {
            return obj is Request request &&
                   path == request.path;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(path);
        }
    }
}

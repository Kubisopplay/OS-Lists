using Symulator1.Models.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator1.Models
{
    public class Algorithm
    {
        List<Process> processList;
        String name;

        public Algorithm(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        internal List<Process> ProcessList { get => processList; set => processList = value; }

        public virtual Result evaluate()
        {
            return new Result(name);
        }
    }
}

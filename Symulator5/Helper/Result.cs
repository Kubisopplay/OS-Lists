using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Symulator5.Helper
{
    class Result
    {
        float averageCPUusage;
        float cPUusageamplitude;
        int usagequeries;
        int migrations;
    String name;
        public Result(float averageCPUusage, float cPUusageamplitude, int usagequeries, int migrations, String n)
        {
            this.averageCPUusage = averageCPUusage;
            CPUusageamplitude = cPUusageamplitude;
            this.usagequeries = usagequeries;
            this.migrations = migrations;
            this.name = n;
        }

        public float AverageCPUusage { get => averageCPUusage; set => averageCPUusage = value; }
        public float CPUusageamplitude { get => cPUusageamplitude; set => cPUusageamplitude = value; }
        public int Usagequeries { get => usagequeries; set => usagequeries = value; }
        public int Migrations { get => migrations; set => migrations = value; }
        public string Name { get => name; set => name = value; }
    }
}

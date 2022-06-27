using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulator5.Helper
{
    class Strategy
    {
        List<CPU> cPUs = new();
        List<Process> processes = new();
        string name;
        int timestamp = 1;
        Controller controller;


        internal Controller Controller { get => controller; set => controller = value; }
        internal List<CPU> CPUs { get => cPUs; set => cPUs = value; }
        internal List<Process> Processes { get => processes; set => processes = value; }

        public Strategy(string name)
        {
            this.name = name;
        }

        public delegate Migrationstats evalbody(Process incoming);
        public virtual Result Evaluate()
        {
            return new Result(0,0,0,0,name);
        }

        public Result Eval(evalbody e)
        {
            bool done = false;
            
            Result temp = new(0, 0, 0, 0,name);
            while (!done)
            {
                foreach (var item in processes.FindAll(e=>e.Entrytime==timestamp))
                {
                    var t = e(item); //tutaj jest efekt że przemieszczenia odbywają się tylko gdy pojawi się nowy proces
                    temp.Migrations += t.Migrations;
                    temp.Usagequeries += t.Queries;
                    processes.Remove(item);
                }
                //liczenie średniego obciążenia natomiast na każdym ticku czasu
                temp.AverageCPUusage += (float)cPUs.Average(e=>e.filledpercent);
                temp.CPUusageamplitude += (float)cPUs.Average(e => Math.Abs(e.filledpercent - cPUs.Average(e => e.filledpercent)));
                timestamp++;
                Parallel.ForEach(cPUs, e => e.Tick()); //god have mercy on me, mam nadzieję że w microsofcie widzieli co robią
                //cPUs.ForEach(e => e.Tick());
                done = processes.Count==0 && cPUs.All(e => e.filledpercent == 0);
            }
            temp.CPUusageamplitude /= timestamp;
            temp.AverageCPUusage /= timestamp;
            return temp;
        }
        
    }
    class Migrationstats
    {
        int migrations;
        int queries;

        public Migrationstats(int migrations, int queries)
        {
            this.migrations = migrations;
            this.queries = queries;
        }

        public int Migrations { get => migrations; set => migrations = value; }
        public int Queries { get => queries; set => queries = value; }
    }
}

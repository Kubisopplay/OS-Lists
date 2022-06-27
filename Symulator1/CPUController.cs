using Symulator1.Models;
using Symulator1.Models.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Symulator1
{

    public class CPUController
    {
         int seed = 2137;
        int procAmount = 50;
        int maxRandomValue = 100;
       
        Random r;
        List<Process> mainProcess;

        public int Seed { get => seed; set => seed = value; }
        public int ProcAmount { get => procAmount; set => procAmount = value; }
        public int MaxRandomValue { get => maxRandomValue; set => maxRandomValue = value; }

        public CPUController()
        {
            changeseed();
            populateProcesses();
        }
	public void populateProcesses(){
		mainProcess = new List<Process>();
            
            for (int i = 0; i < procAmount; i++)
            {
                int d = r.Next(r.Next(maxRandomValue));
                int t = r.Next(maxRandomValue) + 1;
                mainProcess.Add(new Process(i,t,d,t));
            }
            /*
             for (int i = 0; i < procAmount; i++)
            {
                int d = r.Next(maxRandomValue);
		        int t = r.Next(maxRandomValue) +1;
                mainProcess.Add(new Process(i,t,d,t));
            }
             */
        }
        //Zwraca liste obiektów result, do wykorzystania w listview
        public List<Result> evaluateAlgorithm(List<Algorithm> algs){
            List<Result> lr = new List<Result>();
            foreach(Algorithm alg in algs)
            {
                List<Process> temp = new List<Process>();
                foreach(Process p in mainProcess)
                {
                    temp.Add(new Process(p));
                }
                alg.ProcessList = temp;
                lr.Add(alg.evaluate());
            }
            return lr;
            
	}
    public void changeseed()
            // Regeneruje Randoma
        {
            r = new Random(seed);
        }
    int AdvRandom(int low, int med, int high) 
        {
            low = low / (low + med + high);
            med = low / (low + med + high);
            high = low / (low + med + high);
            float temp = low * r.Next((int)Math.Round(maxRandomValue*0.05)) + med * r.Next((int)Math.Round((double)maxRandomValue/2) + high * r.Next(maxRandomValue));
            return (int)Math.Round(temp/ (low + med + high));
        }
    }
    
}

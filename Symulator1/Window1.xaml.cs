using Symulator1.Models.HDDscheduling;
using Symulator1.Models.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Symulator1.HddController;

namespace Symulator1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Button b;
        Grid g;
        Slider[] sliders;
        HddController controller;
        public Window1()
        {
            InitializeComponent();
            b = (Button)FindName("button");
            g = (Grid)FindName("Grid");
            sliders = new Slider[3];
            sliders[0] = (Slider)FindName("s1");
            sliders[1] = (Slider)FindName("s2");
            sliders[2] = (Slider)FindName("s3");

            rng_entry.InitSliders();
            rng_cylinder.InitSliders();
            deadline.InitSliders();
            Dictionary<string, RngControl> temp = new Dictionary<string, RngControl>();
            temp.Add("entry", rng_entry);
            temp.Add("cylinder", rng_cylinder);
            temp.Add("deadline", deadline);
            controller = new HddController(temp,int.Parse(process_amount.Text));

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
         
        private void displayresults(List<HDDResult> ps)

        {
            this.Wyniki.ItemsSource = ps;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            controller.populateRandom();
            var maxlen = int.Parse(DiskWidth.Text);
            HddAlg[] algs =
            {
                new HddAlg("Empty"),
                new FCFS("FCFS",maxlen ),
                new SSTF("sstf",maxlen),
                new SCAN("scan",maxlen),
                new CSCAN("CSCAN",maxlen),
                new EDF("EDF",maxlen),
                new FDSCAN("fdscam",maxlen),
            };
            displayresults(controller.evaluateAlgorithm(algs));
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
             List<HDDResult> resultlistlist = new List<HDDResult>();
            var maxlen = int.Parse(DiskWidth.Text);
            HddAlg[] algs =
                {
                new HddAlg("Empty"),
                new FCFS("FCFS",maxlen ),
                new SSTF("sstf",maxlen),
                new SCAN("scan",maxlen),
                new CSCAN("CSCAN",maxlen),
                new EDF("EDF",maxlen),
                new FDSCAN("fdscam",maxlen),
            };
            for (int i = 0; i < 10; i++)
            {
                controller.populateRandom(); 
                resultlistlist.AddRange(controller.evaluateAlgorithm(algs));
            }
            List<HDDResult> results = new List<HDDResult>();
            foreach (var item in algs)
            {
                var temp = new HDDResult(0, item.Name);
                foreach(var i in resultlistlist.FindAll(ee => ee.Name == item.Name))
                {
                    temp.Value += i.Value;
                }
                temp.Value = temp.Value / 10;
                results.Add(temp);
            }
            displayresults(results);
        }
        void DiskWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( rng_cylinder != null) { 
            try
            {
                rng_cylinder.max.Text = int.Parse(DiskWidth.Text).ToString();
                rng_cylinder.MaxRange = int.Parse(DiskWidth.Text);
            }
            catch (System.Exception)
            {
                DiskWidth.Text = rng_cylinder.MaxRange.ToString();

            }
        }
        }

        private void realtime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (controller != null)
            {
                controller.RealtimePercent = int.Parse(realtime.Text);
            }
        }

        private void process_amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(process_amount!=null&&controller!=null) controller.ProcAmount = int.Parse(process_amount.Text);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using System.IO;
using System.Timers;
using System.Threading;

namespace Symulator1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RngControl : UserControl
    {
        Slider[] sliders;
        int[] values;
        int maxRange;
        int minRange;
        System.Timers.Timer serializationTimer;

        public int MaxRange { get => maxRange; set => maxRange = value; }

        public RngControl()
        {
            InitializeComponent();
            sliders = new Slider[3];
            sliders[0] = (Slider)FindName("s1");
            sliders[1] = (Slider)FindName("s2");
            sliders[2] = (Slider)FindName("s3");
            
        }
        public void InitSliders()
        {
            values = new int[3];
            DeserlializeSettings();
            SetSliders();
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            const int pool = 100;
            values = new int[3];
            int total = 0;
            for (int i = 0; i < sliders.Length; i++)
            {
                values[i] = (int)sliders[i].Value;
                total += values[i];
            }
            var changed = (Slider)sender;
            var reminder = (pool - total)/2;
            foreach (Slider s in sliders)
            {
                if (s.Equals(changed)) continue;
                var temp = s.Value;
                if (temp + reminder > 100)
                {
                    s.Value = 100;
                    reminder += (int)temp + reminder - 100;
                }
                else if (temp + reminder < 0)
                {
                    s.Value = 0;
                    reminder += (int)temp + reminder;
                }
                else
                {
                    s.Value = temp + reminder;
                }
            }
            SerializeSettings();
        }
        public List<int> AdvancedRandom(int lenght)
        {
            Random random = new Random(2137);
            // values x 0,50,100
            //formula 1
            List<int> ret = new List<int>();
            int av = 0; // debug, anyone?
           while(ret.Count< lenght)
                {
                int x = random.Next(minRange, maxRange);
                int y = random.Next(0, 100);
                //we do some magic, funkcja z dwóch punktów dla metody monte carlo
                // sorry przyszły ja, będziesz cierpiał za moje lenistwo
                 int xhalf = ((maxRange - minRange) / 2) + minRange;
                 bool isQualified = x < xhalf ? (((values[0] - values[1]) / (minRange - xhalf)) * x) +
                    (values[0] - (((values[0] - values[1]) / (minRange - xhalf) * minRange))) > y
                    : (((((values[1] - values[2]) / (xhalf-maxRange)) * x) +
                    (values[1] - ((values[1] - values[2]) / (xhalf - maxRange)) * (xhalf)))) > y;
                //jeżeli coś się zepsuje wołaj egzorcystę, nie ma ratunku
                if (isQualified)
                {
                    ret.Add(x);
                  // av += x;
                }
                
           }
            //av = av / lenght;
           // Console.Out.WriteLine(av);

            return ret; 
        }

        private void max_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (max.Text == "TextBox") return;
            try
            {
                maxRange = int.Parse(max.Text);
                SerializeSettings();
            }
            catch(Exception exe)
            {
                max.Text = maxRange.ToString();
            }
            if (maxRange < minRange)
            {
                maxRange = minRange + 1;
            }
        }

        private void min_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(min.Text == "TextBox") return;
            try
            {
                minRange = int.Parse(min.Text);
                SerializeSettings();
            }
            catch (Exception exe)
            {
                min.Text = minRange.ToString();
            }
           
        }
        private void SerializeSettings()
        {

            //tutaj nawet egzorcyzmy nie pomogą
            if (serializationTimer != null && serializationTimer.Enabled)
            {
                serializationTimer.Interval = 1000;
                serializationTimer.Start();

            }
            else
            {
                serializationTimer = new System.Timers.Timer();
                serializationTimer.Interval = 1000;
                serializationTimer.Elapsed += (sender, ee) =>
                {
                    //magic
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        SerializedSettings settings = new SerializedSettings(maxRange, minRange, values);

                        //  FileStream fileStream = new FileStream("rng" + this.Name, FileMode.Create);
                        // JsonSerializer.SerializeAsync(fileStream, settings);
                        string e = JsonSerializer.Serialize<SerializedSettings>(settings);
                        File.WriteAllText("rng" + this.Name, e);

                    });
                };
                serializationTimer.Start();

            }
            
        }
        
        private void DeserlializeSettings()
        {
            try
            {
                string e = File.ReadAllText("rng" + this.Name);
                SerializedSettings temp = JsonSerializer.Deserialize<SerializedSettings>(e);
                minRange = temp.MinR;
                maxRange = temp.MaxR;
                values = temp.Values;

            }
            catch (FileNotFoundException)
            {

            }
            
        }
        private void SetSliders()
        {
            for (int i = 0; i < 3; i++)
            {
                sliders[i].Value = values[i];
            }
            max.Text = maxRange.ToString();
            min.Text = minRange.ToString();
        }

    }
    class SerializedSettings
    {
        private int maxR;
        private int minR;
        private int[] values;

        public int MaxR { get => maxR; set => maxR = value; }
        public int MinR { get => minR; set => minR = value; }
        public int[] Values { get => values; set => values = value; }

        public SerializedSettings(int MaxR, int MinR, int[] values)
        {
            this.MaxR = MaxR;
            this.MinR = MinR;
            this.Values = values;
        }
    }
}

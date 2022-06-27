using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
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
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json.Linq;
using Symulator2.Models;

namespace Symulator2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] framenum;
        int reqnum;
        int pagenum;
        System.Timers.Timer serializationTimer;
        Controller controller;
        List<string> labels;
        public SeriesCollection SeriesCollection { get; private set; }
        public List<string> Labels { get => labels; set => labels = value; }

        public MainWindow()
        {
            InitializeComponent();
            framenum = new int[3];
            controller = new Controller();
            ReloadSettings();
        }
        void SaveSettings()
        {
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
                        SerializedSettings2 settings = new SerializedSettings2(reqnum, framenum[0], framenum[1], framenum[2], pagenum);

                        //  FileStream fileStream = new FileStream("rng" + this.Name, FileMode.Create);
                        // JsonSerializer.SerializeAsync(fileStream, settings);
                        string e = JsonSerializer.Serialize<SerializedSettings2>(settings);
                        File.WriteAllText("mainsetting", e);

                    });
                };
                serializationTimer.Start();

            }
        }
        void ReloadSettings()
        {
            try
            {
                string e = File.ReadAllText("mainsetting");
                SerializedSettings2 temp = JsonSerializer.Deserialize<SerializedSettings2>(e);
                request_number.Text = temp.Request_number.ToString(); ;
                frame_number.Text = temp.Framenumber1.ToString();
                frame_number2.Text = temp.Framenumber2.ToString();
                frame_number3.Text = temp.Framenumber3.ToString();
                page_number.Text = temp.Page_number.ToString();
                SomethingChanged(null, null);


            }
            catch (FileNotFoundException)
            {

            }
        }

        void SomethingChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                reqnum = int.Parse(request_number.Text);
                framenum[0] = int.Parse(frame_number.Text);
                framenum[1] = int.Parse(frame_number2.Text);
                framenum[2] = int.Parse(frame_number3.Text);
                pagenum = int.Parse(page_number.Text);
                SaveSettings();
                UpdateController();
            }
            catch (Exception)
            {
            }
        }
        void showResults(List<Result> srsl)
        {
            /*  Labels = new List<string>();
              foreach (var item in framenum)
              {
                  Labels.Add(item.ToString());
              }
              SeriesCollection = srsl;
            //  SeriesColl.*/
            this.Wyniki.ItemsSource = srsl;

        }
        void UpdateController()
        {
            controller.FrameNum = framenum;
            controller.Reqnum = reqnum;
            controller.PageNum = pagenum;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            controller.populate();
            Algorithm[] algs =
            {
                //new Algorithm(5,"Blank"),
                new FIFO("FIFO"),
                new LRU("LRU"),
                new RAND("RAND"),
                new OPT("OPT"),
                new ALRU("ALRU")
            };
            showResults(controller.EvaluateAlgs(algs));
        }
    }
    class SerializedSettings2
        {
            int request_number;
            int framenumber1;
            int framenumber2;
            int framenumber3;
            int page_number;
            public SerializedSettings2(int request_number, int framenumber1, int framenumber2, int framenumber3, int page_number)
            {
                this.request_number = request_number;
                this.framenumber1 = framenumber1;
                this.framenumber2 = framenumber2;
                this.framenumber3 = framenumber3;
                this.page_number = page_number;
            }

            public int Request_number { get => request_number; set => request_number = value; }
            public int Framenumber1 { get => framenumber1; set => framenumber1 = value; }
            public int Framenumber2 { get => framenumber2; set => framenumber2 = value; }
            public int Framenumber3 { get => framenumber3; set => framenumber3 = value; }
            public int Page_number { get => page_number; set => page_number = value; }
        }



    
}

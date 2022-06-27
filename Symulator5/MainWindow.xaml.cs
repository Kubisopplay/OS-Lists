using Symulator5.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
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

namespace Symulator5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        System.Timers.Timer serializationTimer = null;
        Controller controller = new();

        public MainWindow()
        {
            InitializeComponent();
            ReloadSettings();
            controller = new();
            UpdateController();
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
                        SerializedSettings2 settings = new SerializedSettings2();
                        try
                        {
                            settings.Processnum = int.Parse(processnum.Text);
                            settings.Cpunum = int.Parse(cpunum.Text);
                            settings.Maxcpuneed = int.Parse(Maxcpuneed.Text);
                            settings.Maxtimeentry = int.Parse(maxtimeentry.Text);
                            settings.Maxtimeneeded = int.Parse(maxtimeneeded.Text);
                            settings.N = int.Parse(Nnum.Text);
                            settings.P = int.Parse(Pnum.Text);
                            settings.R = int.Parse(Rnum.Text);
                            settings.Z = int.Parse(Znum.Text);
                        } catch (Exception eeee)
                        {
                            //lol
                        }
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
                processnum.Text = temp.Processnum.ToString();
                Rnum.Text = temp.R.ToString();
                Nnum.Text = temp.N.ToString();
                Pnum.Text = temp.P.ToString();
                Znum.Text = temp.Z.ToString();
                cpunum.Text = temp.Cpunum.ToString();
                maxtimeentry.Text = temp.Maxtimeentry.ToString();
                maxtimeneeded.Text = temp.Maxtimeneeded.ToString();
                Maxcpuneed.Text = temp.Maxcpuneed.ToString();
                SaveSettings();
                UpdateController();


            }
            catch (FileNotFoundException)
            {
                //honk
            }
        }

        void SomethingChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SaveSettings();
                UpdateController();
            }
            catch (Exception)
            {
            }
        }

        private void UpdateController()
        {
            controller.Processnum = int.Parse(processnum.Text);
            controller.Cpunum = int.Parse(cpunum.Text);
            controller.Maxcpuneed = int.Parse(Maxcpuneed.Text);
            controller.Maxtimeentry = int.Parse(maxtimeentry.Text);
            controller.Maxtimeneeded = int.Parse(maxtimeneeded.Text);
            controller.N = int.Parse(Nnum.Text);
            controller.P = int.Parse(Pnum.Text);
            controller.R = int.Parse(Rnum.Text);
            controller.Z = int.Parse(Znum.Text);
        }

        class SerializedSettings2
        {
            int processnum;
            int cpunum;
            int p, r, z, n;
            int maxcpuneed;
            int maxtimeentry;
            int maxtimeneeded;

            public SerializedSettings2()
            {
            }

            public SerializedSettings2(int processnum, int cpunum, int p, int r, int z, int n, int maxcpuneed, int maxtimeentry, int maxtimeneeded)
            {
                this.processnum = processnum;
                this.cpunum = cpunum;
                this.p = p;
                this.r = r;
                this.z = z;
                this.n = n;
                this.maxcpuneed = maxcpuneed;
                this.maxtimeentry = maxtimeentry;
                this.maxtimeneeded = maxtimeneeded;
            }

            public int Processnum { get => processnum; set => processnum = value; }
            public int Cpunum { get => cpunum; set => cpunum = value; }
            public int P { get => p; set => p = value; }
            public int R { get => r; set => r = value; }
            public int Z { get => z; set => z = value; }
            public int N { get => n; set => n = value; }
            public int Maxcpuneed { get => maxcpuneed; set => maxcpuneed = value; }
            public int Maxtimeentry { get => maxtimeentry; set => maxtimeentry = value; }
            public int Maxtimeneeded { get => maxtimeneeded; set => maxtimeneeded = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            controller.populate();
            List<Strategy> ee = new();
            ee.Add(new Strategy("blank"));
            
            ee.Add(new Strat1("strat 1"));
            ee.Add(new Strat2("strat 2"));
            ee.Add(new Strat3("strat 3"));
            Wyniki.ItemsSource = controller.Evaluate(ee);
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            controller.debugpop();
            List<Strategy> ee = new();
            ee.Add(new Strat3("strat 3"));
            ee.Add(new Strat1("strat 1"));
            ee.Add(new Strat2("strat 2"));
            Wyniki.ItemsSource = controller.Evaluate(ee);
        }
    }
}

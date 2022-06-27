using Symulator1.Models;
using Symulator1.Models.Helper;
using Symulator1.Models.Scheduling;
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

namespace Symulator1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListView results;
        public CPUController controller = null;
        List<Result> ViewList;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            results = (ListView)FindName("Wyniki");
            controller = new CPUController();

        }
        void ShowResults(List<Result> resultLists)
        {
            //Przypisuje listę jako nowe źródło danych do listview, wyświetla to
            ViewList = new List<Result>(resultLists);
            results.ItemsSource = ViewList;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //CLickme
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Algorithm> temp = new List<Algorithm>();

            temp.Add(new Algorithm("Empty"));
            temp.Add(new FCFSFix("FCFS"));
            temp.Add(new RR("RR", int.Parse(((TextBox)FindName("Quant")).Text)));
            temp.Add(new SRTF("SRTF"));
            temp.Add(new SJF("SJF"));
            var resultLists = controller.evaluateAlgorithm(temp);
            foreach (Result r in resultLists)
            {
                r.Evaluate();
            }
            ShowResults(resultLists);
           // controller.populateProcesses();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (controller != null) controller.Seed = int.Parse(((TextBox)FindName("seedbox")).Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            controller.changeseed();
            controller.populateProcesses();
        }
        //batch test
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<Result> batchlist = new List<Result>();
            List<Algorithm> temp = new List<Algorithm>();
            temp.Add(new Algorithm("Empty"));
            temp.Add(new FCFSFix("FCFS"));
            temp.Add(new RR("RR", int.Parse(((TextBox)FindName("Quant")).Text)));
            temp.Add(new SRTF("SRTF"));
            temp.Add(new SJF("SJF"));
            for (int i = 0; i < 20; i++)
            {
                var controllerResponse = controller.evaluateAlgorithm(temp);
                foreach (Result item in controllerResponse)
                {
                    batchlist.Add(item);
                }
                controller.populateProcesses();
            }
            List<Result> response = new List<Result>();
            foreach (var item in temp)
            {
                List<Result> batch = batchlist.FindAll((r) =>
                {
                    return r.Name.Equals(item.Name);
                });
                Result average = new Result(item.Name);
                foreach (var res in batch)
                {
                    if (res.WaitingTimes == null) break;
                    foreach (var time in res.WaitingTimes)
                    {
                        average.WaitingTimes.Add(time);
                    }
                }
                average.Evaluate();
                response.Add(average);
            }
            ShowResults(response);
        }

        private void Quant_Copy1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (controller != null) controller.MaxRandomValue = int.Parse(((TextBox)FindName("MaxRand")).Text);
        }

        private void ProcNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (controller != null) controller.ProcAmount = int.Parse(((TextBox)FindName("ProcNum")).Text);
        }
    }
}
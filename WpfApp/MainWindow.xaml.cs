using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    StatusMsg.Text = "DOING WORK!";
        //    foreach (var number in Enumerable.Range(0, 100))
        //    {
        //        Thread.Sleep(100);
        //        AddResult($"the result is {number}");
        //    }
        //    ;
        //    StatusMsg.Text = "WORK DONE!";
        //}

        ////private void Button_Click(object sender, RoutedEventArgs e)
        ////{
        ////    StatusMsg.Text = "DOING WORK!";
        ////    //Parallel.ForEach(Enumerable.Range(0, 100), (number) =>
        ////    //{
        ////    //    Thread.Sleep(100);
        ////    //    Dispatcher.Invoke(() => AddResult($"the result is {number}"));
        ////    //})
        ////    //;
        ////    Parallel.For(0, 100, 
        ////        new ParallelOptions()
        ////        {
        ////            //TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext(),
        ////            CancellationToken = cts.Token,
        ////            MaxDegreeOfParallelism = 4
        ////        }, (number) =>
        ////    {
        ////        Thread.Sleep(100);
        ////        AddResult($"the result is {number}");
        ////    });
        ////    StatusMsg.Text = "WORK DONE!";
        //                    Thread.Sleep(100);
        //        }, cts.Token)
        //        //.ContinueWith((result) =>
        //        //{
        //        //    AddResult($"the result is {result}");
        //        //})
        //        //.ContinueWith((result) =>
        //        //{
        //        //    Dispatcher.Invoke(() => AddResult($"the result is {number}"));
        //        //})
        //        //.ContinueWith((result) =>
        //        //{
        //        //    syncContext.Post((state) => AddResult($"the result is {state}"), number);
        //        //})
        //        //.ContinueWith((result) =>
        //        //{
        //        //    AddResult($"the result is {number}");
        //        //}, TaskScheduler.FromCurrentSynchronizationContext())
        //        .ContinueWith((thePreviousTask) =>
        //        {
        //            AddResult($"the result is {number}");
        //        }, CancellationToken.None, TaskContinuationOptions.NotOnCanceled, TaskScheduler.FromCurrentSynchronizationContext());
        //    }
        //    StatusMsg.Text = "WORK DONE!";
        //}//}


        private CancellationTokenSource cts = new CancellationTokenSource();
        //// usint TPL
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var syncContext = SynchronizationContext.Current;
        //    StatusMsg.Text = "DOING WORK!";
        //    foreach (var number in Enumerable.Range(0, 100))
        //    {

        //        //var theTask = Task.Factory.StartNew(() =>
        //        //{
        //        //    Thread.Sleep(300);
        //        //    return number;
        //        //}, cts.Token)
        //        // equivalent for
        //        Task.Factory.StartNew(someAction, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        //        // see https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcreationoptions?view=netcore-3.1
        //        Task.Run(() =>
        //        {


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            Stack.Children.Clear();
            StatusMsg.Text = "DOING WORK!";
            foreach (var number in Enumerable.Range(0, 100))
            {
                await Task.Delay(100);
                AddResult($"the result is {number}");
            }
            StatusMsg.Text = "WORK DONE!";
            StartButton.IsEnabled = true;
        }

        private void AddResult(string result)
        {
            Stack.Children.Add(new TextBlock
            {
                Text = result
            }); ;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}

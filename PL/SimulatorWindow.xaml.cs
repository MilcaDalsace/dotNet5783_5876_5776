using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;
using static Simulator.SimulatorClass;
//using Simulator;
namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        BackgroundWorker worker;
        private Stopwatch stopwatch;
        private Thread timerThread;
        private bool isRunning = false;
        BO.Order? order;
        bool flagClose;
        public SimulatorWindow()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
            if (!isRunning)
            {
                stopwatch.Restart();
                isRunning = true;
            }
        }
        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                Simulator.SimulatorClass.startSimulator();
                Simulator.SimulatorClass.StatusChangedEvent += StatusChange;
                Simulator.SimulatorClass.FinishSimulatorEvent += finishSimulator;
                while (isRunning)
                {
                    worker.ReportProgress(1);
                    Thread.Sleep(1000);
                }
            }
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            string timerText = stopwatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            timerTxtBlock.Text = timerText;
            if (order is null)
                lblStatus.Content = "Loading order for update...";
            else
                lblStatus.Content = "The order currently being processed: " + order.ID.ToString();
        }
        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.SimulatorClass.StatusChangedEvent -= StatusChange;
            Simulator.SimulatorClass.FinishSimulatorEvent -= finishSimulator;
            isRunning = false;
        }
        public void StatusChange(BO.Order order1, string CurStatus, DateTime prev, DateTime next)
        {
            order = order1;
            Dispatcher.Invoke(() =>
            {
                txtsimulator.Text = $"The result for this order: " + order?.ID.ToString() + "\n" +
                $"Last status: " + order?.OrderStatus.ToString() + "\n" +
                $"Current status: " + CurStatus + "\n" +
                    $"begin to change at: " + prev + "\n" +
                    $"end to change at: " + next;
                Txtorder.Text = order?.ID.ToString();
                Txtstatus.Text = order?.OrderStatus.ToString();
            });
        }
        public void finishSimulator(DateTime end, string reason = "")
        {
            Dispatcher.Invoke(() =>
            {
                if (reason != "")
                {
                    MessageBox.Show("finished the simulator: " + end.ToString() + "\n" + "Cause: " + reason);
                    StopSimulator_Click();
                }
            });
        }
        private void StopSimulator_Click(object? sender = null, RoutedEventArgs? e = null)
        {
            Simulator.SimulatorClass.Stop();
            flagClose = false;
            worker.CancelAsync();
            Close();
        }
        void setTextInvok(string text)
        {
            if (!CheckAccess())
            {
                Action<string> d = setTextInvok;
                Dispatcher.BeginInvoke(d, new object[] { text });
            }
            else
                timerTxtBlock.Text = text;
        }
        public void runTimer()
        {
            while (isRunning)
            {
                string timerText = stopwatch.Elapsed.ToString();
                timerText = timerText.Substring(0, 8);

                setTextInvok(timerText);
                Thread.Sleep(1000);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Simulator.SimulatorClass.Stop();
            flagClose = false;
            worker.CancelAsync();
            //Close();
            if (isRunning)
            {
                stopwatch.Stop();
                isRunning = false;
            }

        }
    }
}















//private void startSimulatorWindow()
//{
//    if (!isRunning)
//    {
//        stopwatch.Restart();
//        isRunning = true;

//        //timerWorker.RunWorkerAsync();

//        timerThread = new Thread(runTimer);
//        timerThread.Start();

//    }
//}
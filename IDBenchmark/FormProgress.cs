using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace IDBenchmark
{
    public partial class FormProgress : Form
    {
        public const int TotalTests = 5;
        public static long TimeElapsed;
        private int _currentTest;

        public FormProgress()
        {
            InitializeComponent();
        }

        private void IDBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            var worker = sender as BackgroundWorker;

            var percentComplete = 0;
            var n = 100 / TotalTests;
            for (var test = 1; test <= TotalTests; test++)
            {
                _currentTest = test;
                worker?.ReportProgress(++percentComplete * n);
                IDRunTest(_currentTest, worker, ref e);
                if (e.Cancel)
                {
                    break;
                }
            }
        }

        private void IDRunTest(int currentTest, BackgroundWorker worker, ref DoWorkEventArgs doWorkEventArgs)
        {
            if (worker.CancellationPending) {
                doWorkEventArgs.Cancel = true;
                return;
            }
            GetTestName(currentTest, true);
            Thread.Sleep(1000);
        }

        private void IDBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBenchmark.Value = e.ProgressPercentage;
            labelWorkload.Text = GetTestName(_currentTest);
        }

        private void IDBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            timerElapsed.Stop();
            // First, handle the case where an exception was thrown. 
            if (e.Error != null) {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                labelWorkload.Text = "Stopping benchmarks...";
                this.Close();
            }
            else
            {
                // @TODO Run a new form with graph of CPU usage.
                this.Close();
            
                var resultForm = new FormResult { Location = this.Location };
                resultForm.Location = new Point(this.Location.X + this.Width / 2 - resultForm.Width / 2, this.Location.Y + this.Height / 2 - resultForm.Height / 2);
                resultForm.Show();
            }
        }
       
        private void FormProgress_Load(object sender, EventArgs e)
        {
            // Reset the text in the result label.
            var date = new DateTime(0);
            labelTime.Text = date.ToString("HH:mm:ss");
            // Run timer.
            TimeElapsed = 0;
            timerElapsed.Start();
            // Reset the variable for percentage tracking.
            progressBenchmark.Value = 0;
            // Start the asynchronous operation.
            IDBackgroundWorker.RunWorkerAsync();
        }

        private void TimerElapsed_Tick(object sender, EventArgs e)
        {
            TimeElapsed += timerElapsed.Interval;
            var date = new DateTime(TimeElapsed * 10000);
            labelTime.Text = date.ToString("HH:mm:ss");
        }

        protected string GetTestName(int testID, bool run = false)
        {
            string testName;
            switch (testID)
            {
                case 1:
                    testName = "Twofish";
                    if (run) {
                        BenchmarkTest.Test1();
                    }
                    break;
                case 2:
                    testName = "SHA1/SHA2";
                    if (run) {
                        BenchmarkTest.Test2();
                    }
                    break;
                case 3:
                    testName = "DateTime";
                    if (run) {
                        BenchmarkTest.Test3();
                    }
                    break;
                case 4:
                    testName = "Arithmetic operations";
                    if (run) {
                        BenchmarkTest.Test4();
                    }
                    break;
                case 5:
                    testName = "List";
                    if (run) {
                        BenchmarkTest.Test5();
                    }
                    break;
                default:
                    testName = "Unknown";
                    break;
            }
            return testName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Cancel the asynchronous operation.
            IDBackgroundWorker.CancelAsync();
            this.Close();
        }
    }
}

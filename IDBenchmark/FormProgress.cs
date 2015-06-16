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
        private const int TotalTests = 10;
        private long _timeElapsed;
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
            getTestName(currentTest, true);
        }

        private void IDBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBenchmark.Value = e.ProgressPercentage;
            labelWorkload.Text = getTestName(_currentTest);
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
                Close();
            }
            else
            {
                // @TODO Run a new form with graph of CPU usage and Score.
                MessageBox.Show(string.Format("Done!!! Time Elapsed: {0} seconds.", _timeElapsed / 1000));
            }
        }

        private void FormProgress_Load(object sender, EventArgs e)
        {
            // Reset the text in the result label.
            var date = new DateTime(0);
            labelTime.Text = date.ToString("HH:mm:ss");
            // Run timer.
            _timeElapsed = 0;
            timerElapsed.Start();
            // Reset the variable for percentage tracking.
            progressBenchmark.Value = 0;
            // Start the asynchronous operation.
            IDBackgroundWorker.RunWorkerAsync();
        }

        private void TimerElapsed_Tick(object sender, EventArgs e)
        {
            _timeElapsed += timerElapsed.Interval;
            var date = new DateTime(_timeElapsed * 10000);
            labelTime.Text = date.ToString("HH:mm:ss");
        }

        protected string getTestName(int testID, bool run = false)
        {
            string testName;
            switch (testID)
            {
                case 1:
                    testName = "Test1";
                    if (run) {
                        BenchmarkTest.Test1();
                    }
                    break;
                case 2:
                    testName = "Test2";
                    if (run) {
                        BenchmarkTest.Test2();
                    }
                    break;
                case 3:
                    testName = "Test3";
                    if (run) {
                        BenchmarkTest.Test3();
                    }
                    break;
                case 4:
                    testName = "Test4";
                    if (run) {
                        BenchmarkTest.Test4();
                    }
                    break;
                case 5:
                    testName = "Test5";
                    if (run) {
                        BenchmarkTest.Test5();
                    }
                    break;
                case 6:
                    testName = "Test6";
                    if (run) {
                        BenchmarkTest.Test6();
                    }
                    break;
                case 7:
                    testName = "Test7";
                    if (run) {
                        BenchmarkTest.Test7();
                    }
                    break;
                case 8:
                    testName = "Test8";
                    if (run) {
                        BenchmarkTest.Test8();
                    }
                    break;
                case 9:
                    testName = "Test9";
                    if (run) {
                        BenchmarkTest.Test9();
                    }
                    break;
                case 10:
                    testName = "Test10";
                    if (run) {
                        BenchmarkTest.Test10();
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
        }
    }
}

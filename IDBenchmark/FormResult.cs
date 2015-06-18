using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDBenchmark
{
    public partial class FormResult : Form
    {
        public FormResult()
        {
            InitializeComponent();
        }

        private void FormResult_Load(object sender, EventArgs e)
        {
            labelResult.Text = string.Format("You got {0} points.", BenchmarkTest.GetScore(FormProgress.TimeElapsed / 1000));
            labelTestTime.Text = string.Format("Testing time: {0} sec.", FormProgress.TimeElapsed / 1000);
        }
    }
}

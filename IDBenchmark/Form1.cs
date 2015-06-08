using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IDBenchmark
{
    public partial class MainForm : Form
    {
        // Init handler.
        public MainForm()
        {
            InitializeComponent();
        }

        // Load handler.
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set OS name.
            labelOS.Text = OSInfo.GetOsFullName();
            // Set Processor name.
            labelProcessor.Text = OSInfo.GetProcessorName();
        }
    }
}

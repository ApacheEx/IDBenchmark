using System;
using System.Drawing;
using System.Windows.Forms;

namespace IDBenchmark
{
    public partial class MainForm : Form
    {
        // Init handler.
        public MainForm()
        {
            InitializeComponent();
        }

        // Loads handler.
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set OS name.
            labelOS.Text = OSInfo.GetOsFullName();
            // Set Processor name.
            labelProcessor.Text = OSInfo.GetProcessorName();
            // Set GPU name.
            labelGPU.Text = OSInfo.GetGpuName();
            // Set Memory name.
            labelMemory.Text = OSInfo.GetMemoryName();
            // Set Model name.
            labelModel.Text = OSInfo.GetModelName();
        }

        // Runs benchmark handler.
        private void btnRun_Click(object sender, EventArgs e)
        {
            var progressForm = new FormProgress {Location = this.Location};
            progressForm.Location = new Point(this.Location.X + this.Width / 2 - progressForm.Width / 2, this.Location.Y + this.Height / 2 - progressForm.Height / 2);
            progressForm.Show();
        }
    }
}

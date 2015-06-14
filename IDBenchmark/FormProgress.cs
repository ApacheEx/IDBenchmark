using System;
using System.Windows.Forms;

namespace IDBenchmark
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            labelWorkload.Text = "Stopping benchmarks...";
            // @TODO.. Check after tests implementation.
            this.Close();
        }
    }
}

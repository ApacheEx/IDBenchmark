namespace IDBenchmark
{
    partial class FormProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelWorkload = new System.Windows.Forms.Label();
            this.progressBenchmark = new System.Windows.Forms.ProgressBar();
            this.labelTime = new System.Windows.Forms.Label();
            this.IDBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.timerElapsed = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(292, 74);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Workload:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Progress:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Elapsed:";
            // 
            // labelWorkload
            // 
            this.labelWorkload.AutoSize = true;
            this.labelWorkload.Location = new System.Drawing.Point(114, 9);
            this.labelWorkload.Name = "labelWorkload";
            this.labelWorkload.Size = new System.Drawing.Size(58, 13);
            this.labelWorkload.TabIndex = 4;
            this.labelWorkload.Text = "Checking..";
            // 
            // progressBenchmark
            // 
            this.progressBenchmark.Location = new System.Drawing.Point(117, 29);
            this.progressBenchmark.Name = "progressBenchmark";
            this.progressBenchmark.Size = new System.Drawing.Size(250, 20);
            this.progressBenchmark.TabIndex = 5;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(114, 55);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(49, 13);
            this.labelTime.TabIndex = 6;
            this.labelTime.Text = "00:00:00";
            // 
            // IDBackgroundWorker
            // 
            this.IDBackgroundWorker.WorkerReportsProgress = true;
            this.IDBackgroundWorker.WorkerSupportsCancellation = true;
            this.IDBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.IDBackgroundWorker_DoWork);
            this.IDBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.IDBackgroundWorker_ProgressChanged);
            this.IDBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.IDBackgroundWorker_RunWorkerCompleted);
            // 
            // timerElapsed
            // 
            this.timerElapsed.Enabled = true;
            this.timerElapsed.Interval = 1000;
            this.timerElapsed.Tick += new System.EventHandler(this.TimerElapsed_Tick);
            // 
            // FormProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(379, 109);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.progressBenchmark);
            this.Controls.Add(this.labelWorkload);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProgress";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Benchmark Progress";
            this.Load += new System.EventHandler(this.FormProgress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelWorkload;
        private System.Windows.Forms.ProgressBar progressBenchmark;
        private System.Windows.Forms.Label labelTime;
        private System.ComponentModel.BackgroundWorker IDBackgroundWorker;
        private System.Windows.Forms.Timer timerElapsed;
    }
}
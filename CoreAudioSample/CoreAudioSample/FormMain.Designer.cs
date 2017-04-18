namespace CoreAudioSample
{
    partial class FormMain
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
            if (disposing && (components != null))
            {
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
            this.TrackbarMaster = new System.Windows.Forms.TrackBar();
            this.Label1 = new System.Windows.Forms.Label();
            this.ProgressBarMaster = new System.Windows.Forms.ProgressBar();
            this.LabelMasterPeak = new System.Windows.Forms.Label();
            this.ProgressBarLeft = new System.Windows.Forms.ProgressBar();
            this.LabelLeftPeak = new System.Windows.Forms.Label();
            this.ProgressBarRight = new System.Windows.Forms.ProgressBar();
            this.LabelRightPeak = new System.Windows.Forms.Label();
            this.TimerUpdate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TrackbarMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // TrackbarMaster
            // 
            this.TrackbarMaster.LargeChange = 20;
            this.TrackbarMaster.Location = new System.Drawing.Point(35, 14);
            this.TrackbarMaster.Maximum = 100;
            this.TrackbarMaster.Name = "TrackbarMaster";
            this.TrackbarMaster.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackbarMaster.Size = new System.Drawing.Size(45, 102);
            this.TrackbarMaster.SmallChange = 5;
            this.TrackbarMaster.TabIndex = 0;
            this.TrackbarMaster.TickFrequency = 10;
            this.TrackbarMaster.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TrackbarMaster.Scroll += new System.EventHandler(this.TrackbarMaster_Scroll);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(14, 119);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(86, 15);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Master Volume";
            // 
            // ProgressBarMaster
            // 
            this.ProgressBarMaster.Location = new System.Drawing.Point(203, 21);
            this.ProgressBarMaster.Name = "ProgressBarMaster";
            this.ProgressBarMaster.Size = new System.Drawing.Size(187, 15);
            this.ProgressBarMaster.Step = 1;
            this.ProgressBarMaster.TabIndex = 2;
            // 
            // LabelMasterPeak
            // 
            this.LabelMasterPeak.AutoSize = true;
            this.LabelMasterPeak.Location = new System.Drawing.Point(118, 21);
            this.LabelMasterPeak.Name = "LabelMasterPeak";
            this.LabelMasterPeak.Size = new System.Drawing.Size(71, 15);
            this.LabelMasterPeak.TabIndex = 3;
            this.LabelMasterPeak.Text = "Master Peak";
            // 
            // ProgressBarLeft
            // 
            this.ProgressBarLeft.Location = new System.Drawing.Point(203, 52);
            this.ProgressBarLeft.Name = "ProgressBarLeft";
            this.ProgressBarLeft.Size = new System.Drawing.Size(187, 15);
            this.ProgressBarLeft.Step = 1;
            this.ProgressBarLeft.TabIndex = 2;
            // 
            // LabelLeftPeak
            // 
            this.LabelLeftPeak.AutoSize = true;
            this.LabelLeftPeak.Location = new System.Drawing.Point(118, 52);
            this.LabelLeftPeak.Name = "LabelLeftPeak";
            this.LabelLeftPeak.Size = new System.Drawing.Size(55, 15);
            this.LabelLeftPeak.TabIndex = 3;
            this.LabelLeftPeak.Text = "Left Peak";
            // 
            // ProgressBarRight
            // 
            this.ProgressBarRight.Location = new System.Drawing.Point(203, 76);
            this.ProgressBarRight.Name = "ProgressBarRight";
            this.ProgressBarRight.Size = new System.Drawing.Size(187, 15);
            this.ProgressBarRight.Step = 1;
            this.ProgressBarRight.TabIndex = 2;
            // 
            // LabelRightPeak
            // 
            this.LabelRightPeak.AutoSize = true;
            this.LabelRightPeak.Location = new System.Drawing.Point(118, 76);
            this.LabelRightPeak.Name = "LabelRightPeak";
            this.LabelRightPeak.Size = new System.Drawing.Size(63, 15);
            this.LabelRightPeak.TabIndex = 3;
            this.LabelRightPeak.Text = "Right Peak";
            // 
            // TimerUpdate
            // 
            this.TimerUpdate.Interval = 150;
            this.TimerUpdate.Tick += new System.EventHandler(this.TimerUpdate_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 158);
            this.Controls.Add(this.LabelRightPeak);
            this.Controls.Add(this.LabelLeftPeak);
            this.Controls.Add(this.LabelMasterPeak);
            this.Controls.Add(this.ProgressBarRight);
            this.Controls.Add(this.ProgressBarLeft);
            this.Controls.Add(this.ProgressBarMaster);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TrackbarMaster);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Core Audio Volume Tools";
            ((System.ComponentModel.ISupportInitialize)(this.TrackbarMaster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar TrackbarMaster;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ProgressBar ProgressBarMaster;
        private System.Windows.Forms.Label LabelMasterPeak;
        private System.Windows.Forms.ProgressBar ProgressBarLeft;
        private System.Windows.Forms.Label LabelLeftPeak;
        private System.Windows.Forms.ProgressBar ProgressBarRight;
        private System.Windows.Forms.Label LabelRightPeak;
        private System.Windows.Forms.Timer TimerUpdate;
    }
}
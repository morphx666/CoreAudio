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
            this.TrackbarMaster.Location = new System.Drawing.Point(30, 12);
            this.TrackbarMaster.Maximum = 100;
            this.TrackbarMaster.Name = "TrackbarMaster";
            this.TrackbarMaster.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackbarMaster.Size = new System.Drawing.Size(45, 88);
            this.TrackbarMaster.SmallChange = 5;
            this.TrackbarMaster.TabIndex = 0;
            this.TrackbarMaster.TickFrequency = 10;
            this.TrackbarMaster.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TrackbarMaster.Scroll += new System.EventHandler(this.TrackbarMaster_Scroll);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 103);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(77, 13);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Master Volume";
            // 
            // ProgressBarMaster
            // 
            this.ProgressBarMaster.Location = new System.Drawing.Point(174, 18);
            this.ProgressBarMaster.Name = "ProgressBarMaster";
            this.ProgressBarMaster.Size = new System.Drawing.Size(160, 13);
            this.ProgressBarMaster.Step = 1;
            this.ProgressBarMaster.TabIndex = 2;
            // 
            // LabelMasterPeak
            // 
            this.LabelMasterPeak.AutoSize = true;
            this.LabelMasterPeak.Location = new System.Drawing.Point(101, 18);
            this.LabelMasterPeak.Name = "LabelMasterPeak";
            this.LabelMasterPeak.Size = new System.Drawing.Size(67, 13);
            this.LabelMasterPeak.TabIndex = 3;
            this.LabelMasterPeak.Text = "Master Peak";
            // 
            // ProgressBarLeft
            // 
            this.ProgressBarLeft.Location = new System.Drawing.Point(174, 52);
            this.ProgressBarLeft.Name = "ProgressBarLeft";
            this.ProgressBarLeft.Size = new System.Drawing.Size(160, 13);
            this.ProgressBarLeft.Step = 1;
            this.ProgressBarLeft.TabIndex = 2;
            // 
            // LabelLeftPeak
            // 
            this.LabelLeftPeak.AutoSize = true;
            this.LabelLeftPeak.Location = new System.Drawing.Point(101, 52);
            this.LabelLeftPeak.Name = "LabelLeftPeak";
            this.LabelLeftPeak.Size = new System.Drawing.Size(53, 13);
            this.LabelLeftPeak.TabIndex = 3;
            this.LabelLeftPeak.Text = "Left Peak";
            // 
            // ProgressBarRight
            // 
            this.ProgressBarRight.Location = new System.Drawing.Point(174, 87);
            this.ProgressBarRight.Name = "ProgressBarRight";
            this.ProgressBarRight.Size = new System.Drawing.Size(160, 13);
            this.ProgressBarRight.Step = 1;
            this.ProgressBarRight.TabIndex = 2;
            // 
            // LabelRightPeak
            // 
            this.LabelRightPeak.AutoSize = true;
            this.LabelRightPeak.Location = new System.Drawing.Point(101, 87);
            this.LabelRightPeak.Name = "LabelRightPeak";
            this.LabelRightPeak.Size = new System.Drawing.Size(60, 13);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 137);
            this.Controls.Add(this.LabelRightPeak);
            this.Controls.Add(this.LabelLeftPeak);
            this.Controls.Add(this.LabelMasterPeak);
            this.Controls.Add(this.ProgressBarRight);
            this.Controls.Add(this.ProgressBarLeft);
            this.Controls.Add(this.ProgressBarMaster);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TrackbarMaster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
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
using System.ComponentModel;

namespace CoreAudioForms.Core.Sample {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.LabelRight = new System.Windows.Forms.Label();
            this.LabelLeft = new System.Windows.Forms.Label();
            this.LabelMaster = new System.Windows.Forms.Label();
            this.ProgressBarRight = new System.Windows.Forms.ProgressBar();
            this.ProgressBarLeft = new System.Windows.Forms.ProgressBar();
            this.ProgressBarMaster = new System.Windows.Forms.ProgressBar();
            this.LabelVolume = new System.Windows.Forms.Label();
            this.TrackBarMaster = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelRight
            // 
            this.LabelRight.AutoSize = true;
            this.LabelRight.Location = new System.Drawing.Point(118, 89);
            this.LabelRight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRight.Name = "LabelRight";
            this.LabelRight.Size = new System.Drawing.Size(63, 15);
            this.LabelRight.TabIndex = 9;
            this.LabelRight.Text = "Right Peak";
            // 
            // LabelLeft
            // 
            this.LabelLeft.AutoSize = true;
            this.LabelLeft.Location = new System.Drawing.Point(118, 48);
            this.LabelLeft.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelLeft.Name = "LabelLeft";
            this.LabelLeft.Size = new System.Drawing.Size(55, 15);
            this.LabelLeft.TabIndex = 10;
            this.LabelLeft.Text = "Left Peak";
            // 
            // LabelMaster
            // 
            this.LabelMaster.AutoSize = true;
            this.LabelMaster.Location = new System.Drawing.Point(118, 9);
            this.LabelMaster.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelMaster.Name = "LabelMaster";
            this.LabelMaster.Size = new System.Drawing.Size(71, 15);
            this.LabelMaster.TabIndex = 11;
            this.LabelMaster.Text = "Master Peak";
            // 
            // ProgressBarRight
            // 
            this.ProgressBarRight.Location = new System.Drawing.Point(203, 89);
            this.ProgressBarRight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProgressBarRight.Name = "ProgressBarRight";
            this.ProgressBarRight.Size = new System.Drawing.Size(187, 15);
            this.ProgressBarRight.Step = 1;
            this.ProgressBarRight.TabIndex = 6;
            // 
            // ProgressBarLeft
            // 
            this.ProgressBarLeft.Location = new System.Drawing.Point(203, 48);
            this.ProgressBarLeft.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProgressBarLeft.Name = "ProgressBarLeft";
            this.ProgressBarLeft.Size = new System.Drawing.Size(187, 15);
            this.ProgressBarLeft.Step = 1;
            this.ProgressBarLeft.TabIndex = 7;
            // 
            // ProgressBarMaster
            // 
            this.ProgressBarMaster.Location = new System.Drawing.Point(203, 9);
            this.ProgressBarMaster.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProgressBarMaster.Name = "ProgressBarMaster";
            this.ProgressBarMaster.Size = new System.Drawing.Size(187, 15);
            this.ProgressBarMaster.Step = 1;
            this.ProgressBarMaster.TabIndex = 8;
            // 
            // LabelVolume
            // 
            this.LabelVolume.AutoSize = true;
            this.LabelVolume.Location = new System.Drawing.Point(14, 107);
            this.LabelVolume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelVolume.Name = "LabelVolume";
            this.LabelVolume.Size = new System.Drawing.Size(86, 15);
            this.LabelVolume.TabIndex = 5;
            this.LabelVolume.Text = "Master Volume";
            // 
            // TrackBarMaster
            // 
            this.TrackBarMaster.LargeChange = 20;
            this.TrackBarMaster.Location = new System.Drawing.Point(35, 2);
            this.TrackBarMaster.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TrackBarMaster.Maximum = 100;
            this.TrackBarMaster.Name = "TrackBarMaster";
            this.TrackBarMaster.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackBarMaster.Size = new System.Drawing.Size(45, 102);
            this.TrackBarMaster.SmallChange = 5;
            this.TrackBarMaster.TabIndex = 4;
            this.TrackBarMaster.TickFrequency = 10;
            this.TrackBarMaster.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 135);
            this.Controls.Add(this.LabelRight);
            this.Controls.Add(this.LabelLeft);
            this.Controls.Add(this.LabelMaster);
            this.Controls.Add(this.ProgressBarRight);
            this.Controls.Add(this.ProgressBarLeft);
            this.Controls.Add(this.ProgressBarMaster);
            this.Controls.Add(this.LabelVolume);
            this.Controls.Add(this.TrackBarMaster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoreAudio Volume Sample";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarMaster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LabelRight;
        private System.Windows.Forms.Label LabelLeft;
        private System.Windows.Forms.Label LabelMaster;
        private System.Windows.Forms.ProgressBar ProgressBarRight;
        private System.Windows.Forms.ProgressBar ProgressBarLeft;
        private System.Windows.Forms.ProgressBar ProgressBarMaster;
        private System.Windows.Forms.Label LabelVolume;
        private System.Windows.Forms.TrackBar TrackBarMaster;
    }
}
using System.ComponentModel;

namespace CoreAudioForms.Core.Sample {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            LabelRightPeak = new System.Windows.Forms.Label();
            LabelLeftPeak = new System.Windows.Forms.Label();
            LabelMasterPeak = new System.Windows.Forms.Label();
            ProgressBarRight = new System.Windows.Forms.ProgressBar();
            ProgressBarLeft = new System.Windows.Forms.ProgressBar();
            ProgressBarMaster = new System.Windows.Forms.ProgressBar();
            LabelMasterVolume = new System.Windows.Forms.Label();
            TrackBarMaster = new System.Windows.Forms.TrackBar();
            CheckBoxMute = new System.Windows.Forms.CheckBox();
            ((ISupportInitialize)TrackBarMaster).BeginInit();
            SuspendLayout();
            // 
            // LabelRightPeak
            // 
            LabelRightPeak.AutoSize = true;
            LabelRightPeak.Location = new System.Drawing.Point(152, 84);
            LabelRightPeak.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelRightPeak.Name = "LabelRightPeak";
            LabelRightPeak.Size = new System.Drawing.Size(73, 19);
            LabelRightPeak.TabIndex = 9;
            LabelRightPeak.Text = "Right Peak";
            // 
            // LabelLeftPeak
            // 
            LabelLeftPeak.AutoSize = true;
            LabelLeftPeak.Location = new System.Drawing.Point(152, 49);
            LabelLeftPeak.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelLeftPeak.Name = "LabelLeftPeak";
            LabelLeftPeak.Size = new System.Drawing.Size(64, 19);
            LabelLeftPeak.TabIndex = 10;
            LabelLeftPeak.Text = "Left Peak";
            // 
            // LabelMasterPeak
            // 
            LabelMasterPeak.AutoSize = true;
            LabelMasterPeak.Location = new System.Drawing.Point(152, 14);
            LabelMasterPeak.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelMasterPeak.Name = "LabelMasterPeak";
            LabelMasterPeak.Size = new System.Drawing.Size(84, 19);
            LabelMasterPeak.TabIndex = 11;
            LabelMasterPeak.Text = "Master Peak";
            // 
            // ProgressBarRight
            // 
            ProgressBarRight.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ProgressBarRight.Location = new System.Drawing.Point(261, 84);
            ProgressBarRight.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            ProgressBarRight.Name = "ProgressBarRight";
            ProgressBarRight.Size = new System.Drawing.Size(240, 23);
            ProgressBarRight.Step = 1;
            ProgressBarRight.TabIndex = 6;
            // 
            // ProgressBarLeft
            // 
            ProgressBarLeft.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ProgressBarLeft.Location = new System.Drawing.Point(261, 49);
            ProgressBarLeft.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            ProgressBarLeft.Name = "ProgressBarLeft";
            ProgressBarLeft.Size = new System.Drawing.Size(240, 23);
            ProgressBarLeft.Step = 1;
            ProgressBarLeft.TabIndex = 7;
            // 
            // ProgressBarMaster
            // 
            ProgressBarMaster.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            ProgressBarMaster.Location = new System.Drawing.Point(261, 14);
            ProgressBarMaster.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            ProgressBarMaster.Name = "ProgressBarMaster";
            ProgressBarMaster.Size = new System.Drawing.Size(240, 23);
            ProgressBarMaster.Step = 1;
            ProgressBarMaster.TabIndex = 8;
            // 
            // LabelMasterVolume
            // 
            LabelMasterVolume.AutoSize = true;
            LabelMasterVolume.Location = new System.Drawing.Point(13, 175);
            LabelMasterVolume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelMasterVolume.Name = "LabelMasterVolume";
            LabelMasterVolume.Size = new System.Drawing.Size(102, 19);
            LabelMasterVolume.TabIndex = 5;
            LabelMasterVolume.Text = "Master Volume";
            // 
            // TrackBarMaster
            // 
            TrackBarMaster.LargeChange = 20;
            TrackBarMaster.Location = new System.Drawing.Point(45, 11);
            TrackBarMaster.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            TrackBarMaster.Maximum = 100;
            TrackBarMaster.Name = "TrackBarMaster";
            TrackBarMaster.Orientation = System.Windows.Forms.Orientation.Vertical;
            TrackBarMaster.Size = new System.Drawing.Size(45, 135);
            TrackBarMaster.SmallChange = 5;
            TrackBarMaster.TabIndex = 4;
            TrackBarMaster.TickFrequency = 10;
            TrackBarMaster.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // CheckBoxMute
            // 
            CheckBoxMute.AutoSize = true;
            CheckBoxMute.Location = new System.Drawing.Point(60, 149);
            CheckBoxMute.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            CheckBoxMute.Name = "CheckBoxMute";
            CheckBoxMute.Size = new System.Drawing.Size(15, 14);
            CheckBoxMute.TabIndex = 12;
            CheckBoxMute.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(524, 217);
            Controls.Add(CheckBoxMute);
            Controls.Add(LabelRightPeak);
            Controls.Add(LabelLeftPeak);
            Controls.Add(LabelMasterPeak);
            Controls.Add(ProgressBarRight);
            Controls.Add(ProgressBarLeft);
            Controls.Add(ProgressBarMaster);
            Controls.Add(LabelMasterVolume);
            Controls.Add(TrackBarMaster);
            Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(2000, 256);
            MinimumSize = new System.Drawing.Size(540, 256);
            Name = "FormMain";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "CoreAudio Volume Sample";
            ((ISupportInitialize)TrackBarMaster).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label LabelRightPeak;
        private System.Windows.Forms.Label LabelLeftPeak;
        private System.Windows.Forms.Label LabelMasterPeak;
        private System.Windows.Forms.ProgressBar ProgressBarRight;
        private System.Windows.Forms.ProgressBar ProgressBarLeft;
        private System.Windows.Forms.ProgressBar ProgressBarMaster;
        private System.Windows.Forms.Label LabelMasterVolume;
        private System.Windows.Forms.TrackBar TrackBarMaster;
        private System.Windows.Forms.CheckBox CheckBoxMute;
    }
}
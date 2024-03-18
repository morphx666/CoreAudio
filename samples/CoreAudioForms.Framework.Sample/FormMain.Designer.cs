namespace CoreAudioForms.Framework.Sample
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
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.LabelRightPeak = new System.Windows.Forms.Label();
            this.LabelLeftPeak = new System.Windows.Forms.Label();
            this.LabelMasterPeak = new System.Windows.Forms.Label();
            this.ProgressBarRight = new System.Windows.Forms.ProgressBar();
            this.ProgressBarLeft = new System.Windows.Forms.ProgressBar();
            this.ProgressBarMaster = new System.Windows.Forms.ProgressBar();
            this.LabelMasterVolume = new System.Windows.Forms.Label();
            this.TrackBarMaster = new System.Windows.Forms.TrackBar();
            this.CheckBoxMute = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // LabelRightPeak
            // 
            this.LabelRightPeak.AutoSize = true;
            this.LabelRightPeak.Location = new System.Drawing.Point(152, 84);
            this.LabelRightPeak.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRightPeak.Name = "LabelRightPeak";
            this.LabelRightPeak.Size = new System.Drawing.Size(90, 23);
            this.LabelRightPeak.TabIndex = 9;
            this.LabelRightPeak.Text = "Right Peak";
            // 
            // LabelLeftPeak
            // 
            this.LabelLeftPeak.AutoSize = true;
            this.LabelLeftPeak.Location = new System.Drawing.Point(152, 49);
            this.LabelLeftPeak.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelLeftPeak.Name = "LabelLeftPeak";
            this.LabelLeftPeak.Size = new System.Drawing.Size(78, 23);
            this.LabelLeftPeak.TabIndex = 10;
            this.LabelLeftPeak.Text = "Left Peak";
            // 
            // LabelMasterPeak
            // 
            this.LabelMasterPeak.AutoSize = true;
            this.LabelMasterPeak.Location = new System.Drawing.Point(152, 14);
            this.LabelMasterPeak.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelMasterPeak.Name = "LabelMasterPeak";
            this.LabelMasterPeak.Size = new System.Drawing.Size(102, 23);
            this.LabelMasterPeak.TabIndex = 11;
            this.LabelMasterPeak.Text = "Master Peak";
            // 
            // ProgressBarRight
            // 
            this.ProgressBarRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBarRight.Location = new System.Drawing.Point(261, 84);
            this.ProgressBarRight.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ProgressBarRight.Name = "ProgressBarRight";
            this.ProgressBarRight.Size = new System.Drawing.Size(240, 23);
            this.ProgressBarRight.Step = 1;
            this.ProgressBarRight.TabIndex = 6;
            // 
            // ProgressBarLeft
            // 
            this.ProgressBarLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBarLeft.Location = new System.Drawing.Point(261, 49);
            this.ProgressBarLeft.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ProgressBarLeft.Name = "ProgressBarLeft";
            this.ProgressBarLeft.Size = new System.Drawing.Size(240, 23);
            this.ProgressBarLeft.Step = 1;
            this.ProgressBarLeft.TabIndex = 7;
            // 
            // ProgressBarMaster
            // 
            this.ProgressBarMaster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBarMaster.Location = new System.Drawing.Point(261, 14);
            this.ProgressBarMaster.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ProgressBarMaster.Name = "ProgressBarMaster";
            this.ProgressBarMaster.Size = new System.Drawing.Size(240, 23);
            this.ProgressBarMaster.Step = 1;
            this.ProgressBarMaster.TabIndex = 8;
            // 
            // LabelMasterVolume
            // 
            this.LabelMasterVolume.AutoSize = true;
            this.LabelMasterVolume.Location = new System.Drawing.Point(13, 175);
            this.LabelMasterVolume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelMasterVolume.Name = "LabelMasterVolume";
            this.LabelMasterVolume.Size = new System.Drawing.Size(125, 23);
            this.LabelMasterVolume.TabIndex = 5;
            this.LabelMasterVolume.Text = "Master Volume";
            // 
            // TrackBarMaster
            // 
            this.TrackBarMaster.LargeChange = 20;
            this.TrackBarMaster.Location = new System.Drawing.Point(45, 11);
            this.TrackBarMaster.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.TrackBarMaster.Maximum = 100;
            this.TrackBarMaster.Name = "TrackBarMaster";
            this.TrackBarMaster.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackBarMaster.Size = new System.Drawing.Size(56, 135);
            this.TrackBarMaster.SmallChange = 5;
            this.TrackBarMaster.TabIndex = 4;
            this.TrackBarMaster.TickFrequency = 10;
            this.TrackBarMaster.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TrackBarMaster.Scroll += new System.EventHandler(this.TrackBarMaster_Scroll);
            // 
            // CheckBoxMute
            // 
            this.CheckBoxMute.AutoSize = true;
            this.CheckBoxMute.Location = new System.Drawing.Point(60, 149);
            this.CheckBoxMute.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CheckBoxMute.Name = "CheckBoxMute";
            this.CheckBoxMute.Size = new System.Drawing.Size(18, 17);
            this.CheckBoxMute.TabIndex = 12;
            this.CheckBoxMute.UseVisualStyleBackColor = true;
            this.CheckBoxMute.CheckedChanged += new System.EventHandler(this.CheckBoxMute_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 209);
            this.Controls.Add(this.CheckBoxMute);
            this.Controls.Add(this.LabelRightPeak);
            this.Controls.Add(this.LabelLeftPeak);
            this.Controls.Add(this.LabelMasterPeak);
            this.Controls.Add(this.ProgressBarRight);
            this.Controls.Add(this.ProgressBarLeft);
            this.Controls.Add(this.ProgressBarMaster);
            this.Controls.Add(this.LabelMasterVolume);
            this.Controls.Add(this.TrackBarMaster);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2000, 256);
            this.MinimumSize = new System.Drawing.Size(540, 256);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoreAudio Volume Sample";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarMaster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer UpdateTimer;
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


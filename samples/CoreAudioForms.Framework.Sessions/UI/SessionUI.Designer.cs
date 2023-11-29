namespace CoreAudioForms.Framework.Sessions {
    partial class SessionUI {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.PictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.LabelName = new System.Windows.Forms.Label();
            this.TrackBarVol = new XComponent.SliderBar.MACTrackBar();
            this.LabelSessionInfo = new System.Windows.Forms.Label();
            this.VUDisplay = new CoreAudioForms.Framework.Sessions.VU();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxIcon
            // 
            this.PictureBoxIcon.Location = new System.Drawing.Point(10, 7);
            this.PictureBoxIcon.Margin = new System.Windows.Forms.Padding(4);
            this.PictureBoxIcon.Name = "PictureBoxIcon";
            this.PictureBoxIcon.Size = new System.Drawing.Size(85, 79);
            this.PictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxIcon.TabIndex = 0;
            this.PictureBoxIcon.TabStop = false;
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Location = new System.Drawing.Point(105, 4);
            this.LabelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(96, 16);
            this.LabelName.TabIndex = 1;
            this.LabelName.Text = "Session Name";
            // 
            // TrackBarVol
            // 
            this.TrackBarVol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackBarVol.BackColor = System.Drawing.Color.Transparent;
            this.TrackBarVol.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.TrackBarVol.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrackBarVol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.TrackBarVol.IndentHeight = 6;
            this.TrackBarVol.Location = new System.Drawing.Point(107, 71);
            this.TrackBarVol.Margin = new System.Windows.Forms.Padding(4);
            this.TrackBarVol.Maximum = 100;
            this.TrackBarVol.Minimum = 0;
            this.TrackBarVol.Name = "TrackBarVol";
            this.TrackBarVol.Size = new System.Drawing.Size(283, 28);
            this.TrackBarVol.TabIndex = 3;
            this.TrackBarVol.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.TrackBarVol.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.TrackBarVol.TickFrequency = 10;
            this.TrackBarVol.TickHeight = 4;
            this.TrackBarVol.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrackBarVol.TrackerColor = System.Drawing.Color.DarkSlateBlue;
            this.TrackBarVol.TrackerSize = new System.Drawing.Size(16, 16);
            this.TrackBarVol.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.TrackBarVol.TrackLineHeight = 3;
            this.TrackBarVol.Value = 0;
            // 
            // LabelSessionInfo
            // 
            this.LabelSessionInfo.AutoSize = true;
            this.LabelSessionInfo.Location = new System.Drawing.Point(105, 20);
            this.LabelSessionInfo.Name = "LabelSessionInfo";
            this.LabelSessionInfo.Size = new System.Drawing.Size(80, 16);
            this.LabelSessionInfo.TabIndex = 5;
            this.LabelSessionInfo.Text = "Session Info";
            // 
            // VUDisplay
            // 
            this.VUDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VUDisplay.BarBackColor = System.Drawing.SystemColors.Control;
            this.VUDisplay.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.VUDisplay.Channels = 2;
            this.VUDisplay.ForeColor = System.Drawing.SystemColors.Highlight;
            this.VUDisplay.LedsColorsOff = new System.Drawing.Color[] {
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.DarkGoldenrod,
        System.Drawing.Color.DarkRed};
            this.VUDisplay.LedsColorsOn = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Red};
            this.VUDisplay.LedsRanges = new float[] {
        50F,
        35F,
        15F};
            this.VUDisplay.Location = new System.Drawing.Point(107, 39);
            this.VUDisplay.Mode = CoreAudioForms.Framework.Sessions.VU.Modes.Bar;
            this.VUDisplay.Name = "VUDisplay";
            this.VUDisplay.Size = new System.Drawing.Size(283, 25);
            this.VUDisplay.TabIndex = 4;
            this.VUDisplay.Values = new int[] {
        0,
        0};
            // 
            // SessionUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LabelSessionInfo);
            this.Controls.Add(this.VUDisplay);
            this.Controls.Add(this.TrackBarVol);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.PictureBoxIcon);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SessionUI";
            this.Size = new System.Drawing.Size(400, 104);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxIcon;
        private System.Windows.Forms.Label LabelName;
        private XComponent.SliderBar.MACTrackBar TrackBarVol;
        internal VU VUDisplay;
        private System.Windows.Forms.Label LabelSessionInfo;
    }
}

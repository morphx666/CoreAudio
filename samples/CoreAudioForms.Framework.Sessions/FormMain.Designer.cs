namespace CoreAudioForms.Framework.Sessions {
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
            this.LabelDevice = new System.Windows.Forms.Label();
            this.ComboBoxDevices = new System.Windows.Forms.ComboBox();
            this.FlowLayoutPanelSessions = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // LabelDevice
            // 
            this.LabelDevice.AutoSize = true;
            this.LabelDevice.Location = new System.Drawing.Point(12, 17);
            this.LabelDevice.Name = "LabelDevice";
            this.LabelDevice.Size = new System.Drawing.Size(60, 23);
            this.LabelDevice.TabIndex = 0;
            this.LabelDevice.Text = "Device";
            // 
            // ComboBoxDevices
            // 
            this.ComboBoxDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDevices.FormattingEnabled = true;
            this.ComboBoxDevices.Location = new System.Drawing.Point(76, 14);
            this.ComboBoxDevices.Name = "ComboBoxDevices";
            this.ComboBoxDevices.Size = new System.Drawing.Size(643, 29);
            this.ComboBoxDevices.TabIndex = 1;
            // 
            // FlowLayoutPanelSessions
            // 
            this.FlowLayoutPanelSessions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowLayoutPanelSessions.AutoScroll = true;
            this.FlowLayoutPanelSessions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanelSessions.Location = new System.Drawing.Point(16, 49);
            this.FlowLayoutPanelSessions.Name = "FlowLayoutPanelSessions";
            this.FlowLayoutPanelSessions.Padding = new System.Windows.Forms.Padding(8);
            this.FlowLayoutPanelSessions.Size = new System.Drawing.Size(703, 489);
            this.FlowLayoutPanelSessions.TabIndex = 2;
            this.FlowLayoutPanelSessions.WrapContents = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 550);
            this.Controls.Add(this.FlowLayoutPanelSessions);
            this.Controls.Add(this.ComboBoxDevices);
            this.Controls.Add(this.LabelDevice);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoreAudio Sessions Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelDevice;
        private System.Windows.Forms.ComboBox ComboBoxDevices;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanelSessions;
    }
}


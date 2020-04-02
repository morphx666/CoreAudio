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
            this.lblRight = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblMaster = new System.Windows.Forms.Label();
            this.pbRight = new System.Windows.Forms.ProgressBar();
            this.pbLeft = new System.Windows.Forms.ProgressBar();
            this.pbMaster = new System.Windows.Forms.ProgressBar();
            this.lblVolume = new System.Windows.Forms.Label();
            this.tbMaster = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tbMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Tick += new System.EventHandler(this.Update_Timer_Tick);
            // 
            // lblRight
            // 
            this.lblRight.AutoSize = true;
            this.lblRight.Location = new System.Drawing.Point(101, 77);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(60, 13);
            this.lblRight.TabIndex = 9;
            this.lblRight.Text = "Right Peak";
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Location = new System.Drawing.Point(101, 42);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(53, 13);
            this.lblLeft.TabIndex = 10;
            this.lblLeft.Text = "Left Peak";
            // 
            // lblMaster
            // 
            this.lblMaster.AutoSize = true;
            this.lblMaster.Location = new System.Drawing.Point(101, 8);
            this.lblMaster.Name = "lblMaster";
            this.lblMaster.Size = new System.Drawing.Size(67, 13);
            this.lblMaster.TabIndex = 11;
            this.lblMaster.Text = "Master Peak";
            // 
            // pbRight
            // 
            this.pbRight.Location = new System.Drawing.Point(174, 77);
            this.pbRight.Name = "pbRight";
            this.pbRight.Size = new System.Drawing.Size(160, 13);
            this.pbRight.Step = 1;
            this.pbRight.TabIndex = 6;
            // 
            // pbLeft
            // 
            this.pbLeft.Location = new System.Drawing.Point(174, 42);
            this.pbLeft.Name = "pbLeft";
            this.pbLeft.Size = new System.Drawing.Size(160, 13);
            this.pbLeft.Step = 1;
            this.pbLeft.TabIndex = 7;
            // 
            // pbMaster
            // 
            this.pbMaster.Location = new System.Drawing.Point(174, 8);
            this.pbMaster.Name = "pbMaster";
            this.pbMaster.Size = new System.Drawing.Size(160, 13);
            this.pbMaster.Step = 1;
            this.pbMaster.TabIndex = 8;
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(12, 93);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(77, 13);
            this.lblVolume.TabIndex = 5;
            this.lblVolume.Text = "Master Volume";
            // 
            // tbMaster
            // 
            this.tbMaster.LargeChange = 20;
            this.tbMaster.Location = new System.Drawing.Point(30, 2);
            this.tbMaster.Maximum = 100;
            this.tbMaster.Name = "tbMaster";
            this.tbMaster.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbMaster.Size = new System.Drawing.Size(45, 88);
            this.tbMaster.SmallChange = 5;
            this.tbMaster.TabIndex = 4;
            this.tbMaster.TickFrequency = 10;
            this.tbMaster.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbMaster.Scroll += new System.EventHandler(this.Master_Scroll);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 117);
            this.Controls.Add(this.lblRight);
            this.Controls.Add(this.lblLeft);
            this.Controls.Add(this.lblMaster);
            this.Controls.Add(this.pbRight);
            this.Controls.Add(this.pbLeft);
            this.Controls.Add(this.pbMaster);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.tbMaster);
            this.Name = "FormMain";
            this.Text = "CoreAudio Volume Sample";
            ((System.ComponentModel.ISupportInitialize)(this.tbMaster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblMaster;
        private System.Windows.Forms.ProgressBar pbRight;
        private System.Windows.Forms.ProgressBar pbLeft;
        private System.Windows.Forms.ProgressBar pbMaster;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TrackBar tbMaster;
    }
}


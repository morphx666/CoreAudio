namespace CoreAudioSample
{
    partial class Form1
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
            this.tbMaster = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.pkMaster = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.pkLeft = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.pkRight = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tbMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // tbMaster
            // 
            this.tbMaster.LargeChange = 20;
            this.tbMaster.Location = new System.Drawing.Point(30, 12);
            this.tbMaster.Maximum = 100;
            this.tbMaster.Name = "tbMaster";
            this.tbMaster.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbMaster.Size = new System.Drawing.Size(45, 88);
            this.tbMaster.SmallChange = 5;
            this.tbMaster.TabIndex = 0;
            this.tbMaster.TickFrequency = 10;
            this.tbMaster.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbMaster.Scroll += new System.EventHandler(this.tbMaster_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Master Volume";
            // 
            // pkMaster
            // 
            this.pkMaster.Location = new System.Drawing.Point(174, 18);
            this.pkMaster.Name = "pkMaster";
            this.pkMaster.Size = new System.Drawing.Size(160, 13);
            this.pkMaster.Step = 1;
            this.pkMaster.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Master Peak";
            // 
            // pkLeft
            // 
            this.pkLeft.Location = new System.Drawing.Point(174, 52);
            this.pkLeft.Name = "pkLeft";
            this.pkLeft.Size = new System.Drawing.Size(160, 13);
            this.pkLeft.Step = 1;
            this.pkLeft.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Left Peak";
            // 
            // pkRight
            // 
            this.pkRight.Location = new System.Drawing.Point(174, 87);
            this.pkRight.Name = "pkRight";
            this.pkRight.Size = new System.Drawing.Size(160, 13);
            this.pkRight.Step = 1;
            this.pkRight.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Right Peak";
            // 
            // timer1
            // 
            this.timer1.Interval = 150;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 137);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pkRight);
            this.Controls.Add(this.pkLeft);
            this.Controls.Add(this.pkMaster);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbMaster);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Core Audio Volume Tools";
            ((System.ComponentModel.ISupportInitialize)(this.tbMaster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbMaster;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pkMaster;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pkLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pkRight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
    }
}
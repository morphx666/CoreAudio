using System.Drawing;
using System.Windows.Forms;

namespace CoreAudioConsole.Framework.Discover.Tester {
    partial class FomMain {
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
            this.TextBoxOutput = new System.Windows.Forms.TextBox();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.CheckBoxAutoRefresh = new System.Windows.Forms.CheckBox();
            this.ButtonRefresh = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBoxOutput
            // 
            this.TextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.TextBoxOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxOutput.Font = new System.Drawing.Font("Consolas", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxOutput.ForeColor = System.Drawing.Color.Gainsboro;
            this.TextBoxOutput.Location = new System.Drawing.Point(16, 19);
            this.TextBoxOutput.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TextBoxOutput.Multiline = true;
            this.TextBoxOutput.Name = "TextBoxOutput";
            this.TextBoxOutput.ReadOnly = true;
            this.TextBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxOutput.Size = new System.Drawing.Size(1173, 604);
            this.TextBoxOutput.TabIndex = 0;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonSave.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.ButtonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonSave.Location = new System.Drawing.Point(16, 635);
            this.ButtonSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(143, 36);
            this.ButtonSave.TabIndex = 3;
            this.ButtonSave.Text = "Save...";
            this.ButtonSave.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAutoRefresh
            // 
            this.CheckBoxAutoRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxAutoRefresh.Appearance = System.Windows.Forms.Appearance.Button;
            this.CheckBoxAutoRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.CheckBoxAutoRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckBoxAutoRefresh.Location = new System.Drawing.Point(746, 634);
            this.CheckBoxAutoRefresh.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.CheckBoxAutoRefresh.Name = "CheckBoxAutoRefresh";
            this.CheckBoxAutoRefresh.Size = new System.Drawing.Size(143, 36);
            this.CheckBoxAutoRefresh.TabIndex = 2;
            this.CheckBoxAutoRefresh.Text = "Auto Refresh";
            this.CheckBoxAutoRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxAutoRefresh.UseVisualStyleBackColor = true;
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.ButtonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonRefresh.Location = new System.Drawing.Point(896, 634);
            this.ButtonRefresh.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Size = new System.Drawing.Size(143, 36);
            this.ButtonRefresh.TabIndex = 1;
            this.ButtonRefresh.Text = "Refresh";
            this.ButtonRefresh.UseVisualStyleBackColor = true;
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonClose.Location = new System.Drawing.Point(1046, 635);
            this.ButtonClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(143, 36);
            this.ButtonClose.TabIndex = 1;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            // 
            // FomMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(1203, 685);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.CheckBoxAutoRefresh);
            this.Controls.Add(this.ButtonRefresh);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.TextBoxOutput);
            this.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FomMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoreAudioNET Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxOutput;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.CheckBox CheckBoxAutoRefresh;
        private System.Windows.Forms.Button ButtonRefresh;
        private System.Windows.Forms.Button ButtonClose;
    }
}


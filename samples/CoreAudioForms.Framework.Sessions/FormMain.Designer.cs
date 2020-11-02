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
            this.Label1 = new System.Windows.Forms.Label();
            this.ComboBoxDevices = new System.Windows.Forms.ComboBox();
            this.TableLayoutPanelSessions = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 17);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(46, 17);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Device";
            // 
            // ComboBoxDevices
            // 
            this.ComboBoxDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDevices.FormattingEnabled = true;
            this.ComboBoxDevices.Location = new System.Drawing.Point(76, 14);
            this.ComboBoxDevices.Name = "ComboBoxDevices";
            this.ComboBoxDevices.Size = new System.Drawing.Size(479, 25);
            this.ComboBoxDevices.TabIndex = 1;
            // 
            // TableLayoutPanelSessions
            // 
            this.TableLayoutPanelSessions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanelSessions.AutoScroll = true;
            this.TableLayoutPanelSessions.ColumnCount = 1;
            this.TableLayoutPanelSessions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelSessions.Location = new System.Drawing.Point(12, 45);
            this.TableLayoutPanelSessions.Name = "TableLayoutPanelSessions";
            this.TableLayoutPanelSessions.RowCount = 1;
            this.TableLayoutPanelSessions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelSessions.Size = new System.Drawing.Size(543, 493);
            this.TableLayoutPanelSessions.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 550);
            this.Controls.Add(this.TableLayoutPanelSessions);
            this.Controls.Add(this.ComboBoxDevices);
            this.Controls.Add(this.Label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoreAudio Sessions Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ComboBox ComboBoxDevices;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelSessions;
    }
}


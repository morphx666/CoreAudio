<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBoxOutput = New System.Windows.Forms.TextBox()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ButtonRefresh = New System.Windows.Forms.Button()
        Me.CheckBoxAutoRefresh = New System.Windows.Forms.CheckBox()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBoxOutput
        '
        Me.TextBoxOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxOutput.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxOutput.Location = New System.Drawing.Point(14, 16)
        Me.TextBoxOutput.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBoxOutput.Multiline = True
        Me.TextBoxOutput.Name = "TextBoxOutput"
        Me.TextBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxOutput.Size = New System.Drawing.Size(896, 460)
        Me.TextBoxOutput.TabIndex = 0
        '
        'ButtonClose
        '
        Me.ButtonClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClose.Location = New System.Drawing.Point(806, 484)
        Me.ButtonClose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(104, 30)
        Me.ButtonClose.TabIndex = 1
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ButtonRefresh
        '
        Me.ButtonRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRefresh.Location = New System.Drawing.Point(696, 484)
        Me.ButtonRefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonRefresh.Name = "ButtonRefresh"
        Me.ButtonRefresh.Size = New System.Drawing.Size(104, 30)
        Me.ButtonRefresh.TabIndex = 1
        Me.ButtonRefresh.Text = "Refresh"
        Me.ButtonRefresh.UseVisualStyleBackColor = True
        '
        'CheckBoxAutoRefresh
        '
        Me.CheckBoxAutoRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxAutoRefresh.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBoxAutoRefresh.Location = New System.Drawing.Point(586, 484)
        Me.CheckBoxAutoRefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CheckBoxAutoRefresh.Name = "CheckBoxAutoRefresh"
        Me.CheckBoxAutoRefresh.Size = New System.Drawing.Size(104, 30)
        Me.CheckBoxAutoRefresh.TabIndex = 2
        Me.CheckBoxAutoRefresh.Text = "Auto Refresh"
        Me.CheckBoxAutoRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CheckBoxAutoRefresh.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonSave.Location = New System.Drawing.Point(14, 485)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(104, 30)
        Me.ButtonSave.TabIndex = 3
        Me.ButtonSave.Text = "Save..."
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(922, 527)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.CheckBoxAutoRefresh)
        Me.Controls.Add(Me.ButtonRefresh)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.TextBoxOutput)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FormMain"
        Me.Text = "CoreAudioNET Tester"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxOutput As System.Windows.Forms.TextBox
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
    Friend WithEvents ButtonRefresh As System.Windows.Forms.Button
    Friend WithEvents CheckBoxAutoRefresh As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonSave As System.Windows.Forms.Button

End Class

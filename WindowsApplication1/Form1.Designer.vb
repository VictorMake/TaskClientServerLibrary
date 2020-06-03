<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.ButtonLoad = New System.Windows.Forms.Button
        Me.ListBoxTask = New System.Windows.Forms.ListBox
        Me.ButtonChowSelecttion = New System.Windows.Forms.Button
        Me.ButtonChowSelecttionByIndex = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ButtonLoad
        '
        Me.ButtonLoad.Location = New System.Drawing.Point(12, 12)
        Me.ButtonLoad.Name = "ButtonLoad"
        Me.ButtonLoad.Size = New System.Drawing.Size(75, 23)
        Me.ButtonLoad.TabIndex = 0
        Me.ButtonLoad.Text = "Загрузить"
        Me.ButtonLoad.UseVisualStyleBackColor = True
        '
        'ListBoxTask
        '
        Me.ListBoxTask.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxTask.FormattingEnabled = True
        Me.ListBoxTask.Location = New System.Drawing.Point(12, 41)
        Me.ListBoxTask.Name = "ListBoxTask"
        Me.ListBoxTask.Size = New System.Drawing.Size(385, 212)
        Me.ListBoxTask.TabIndex = 1
        '
        'ButtonChowSelecttion
        '
        Me.ButtonChowSelecttion.Location = New System.Drawing.Point(93, 12)
        Me.ButtonChowSelecttion.Name = "ButtonChowSelecttion"
        Me.ButtonChowSelecttion.Size = New System.Drawing.Size(75, 23)
        Me.ButtonChowSelecttion.TabIndex = 2
        Me.ButtonChowSelecttion.Text = "Параметры"
        Me.ButtonChowSelecttion.UseVisualStyleBackColor = True
        '
        'ButtonChowSelecttionByIndex
        '
        Me.ButtonChowSelecttionByIndex.Location = New System.Drawing.Point(174, 12)
        Me.ButtonChowSelecttionByIndex.Name = "ButtonChowSelecttionByIndex"
        Me.ButtonChowSelecttionByIndex.Size = New System.Drawing.Size(134, 23)
        Me.ButtonChowSelecttionByIndex.TabIndex = 3
        Me.ButtonChowSelecttionByIndex.Text = "Параметры индекс"
        Me.ButtonChowSelecttionByIndex.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(409, 262)
        Me.Controls.Add(Me.ButtonChowSelecttionByIndex)
        Me.Controls.Add(Me.ButtonChowSelecttion)
        Me.Controls.Add(Me.ListBoxTask)
        Me.Controls.Add(Me.ButtonLoad)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonLoad As System.Windows.Forms.Button
    Friend WithEvents ListBoxTask As System.Windows.Forms.ListBox
    Friend WithEvents ButtonChowSelecttion As System.Windows.Forms.Button
    Friend WithEvents ButtonChowSelecttionByIndex As System.Windows.Forms.Button

End Class

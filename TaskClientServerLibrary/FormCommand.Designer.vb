Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormCommand
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCommand))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControlCommandShassis = New System.Windows.Forms.TabControl()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "netcenter_22.ico")
        Me.ImageList1.Images.SetKeyName(1, "netcenter_23.ico")
        Me.ImageList1.Images.SetKeyName(2, "signal-1.png")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "gg_connecting.png")
        '
        'TabControlCommandShassis
        '
        Me.TabControlCommandShassis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlCommandShassis.ImageList = Me.ImageList1
        Me.TabControlCommandShassis.Location = New System.Drawing.Point(0, 0)
        Me.TabControlCommandShassis.Name = "TabControlCommandShassis"
        Me.TabControlCommandShassis.SelectedIndex = 0
        Me.TabControlCommandShassis.Size = New System.Drawing.Size(800, 661)
        Me.TabControlCommandShassis.TabIndex = 1
        '
        'FormCommandTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 661)
        Me.Controls.Add(Me.TabControlCommandShassis)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(700, 700)
        Me.Name = "FormCommandTest"
        Me.Tag = "Обмен командами управления"
        Me.Text = "Обмен командами управления"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TabControlCommandShassis As TabControl
End Class

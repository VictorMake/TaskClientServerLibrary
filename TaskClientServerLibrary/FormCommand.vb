Imports System.Drawing
Imports System.Windows.Forms

Public Class FormCommand
    Private parentReaderWriterCommand As ReaderWriterCommand

    Public Sub New(inReaderWriterCommandClass As ReaderWriterCommand)
        MyBase.New

        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()

        ' Добавить код инициализации после вызова InitializeComponent().
        parentReaderWriterCommand = inReaderWriterCommandClass
    End Sub

    Private Sub FormCommandTest_Load(sender As Object, e As EventArgs) Handles Me.Load
        'RegistrationEventLog.EventLog_AUDIT_SUCCESS("Згрузка окна " & Me.Text)
        Me.Text += " - " & parentReaderWriterCommand.Caption
        PopulateTabPage()
        Dim mFutureClass As New FutureClass("Первый", FutureClass.TypeProject.Removed)
    End Sub

    Private Sub PopulateTabPage()
        Dim I As Integer = 0

        For Each itemTarget As Target In parentReaderWriterCommand.ManagerAllTargets.Targets.Values
            Dim mTabPageTargets As TabPage = New TabPage()
            Dim mUserControlCommandTarget As UserControlCommand = itemTarget.GetUserControlCommandTarget

            mTabPageTargets.SuspendLayout()
            TabControlCommandShassis.Controls.Add(mTabPageTargets)
            '
            'TabPageTargets
            '
            mTabPageTargets.Controls.Add(mUserControlCommandTarget)

            mTabPageTargets.BackColor = SystemColors.Control
            mTabPageTargets.BorderStyle = BorderStyle.Fixed3D
            mTabPageTargets.Font = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(204, Byte))
            mTabPageTargets.ImageIndex = 1
            mTabPageTargets.Location = New Point(4, 23)
            mTabPageTargets.Name = "TabPageTarget" & I
            mTabPageTargets.Tag = I
            mTabPageTargets.Padding = New Padding(3)
            mTabPageTargets.Size = New Size(770, 611)
            'mTabPage.TabIndex = 0
            mTabPageTargets.Text = itemTarget.HostName
            mTabPageTargets.UseVisualStyleBackColor = True
            '
            'UserControlCommandTarget
            '
            mUserControlCommandTarget.Dock = DockStyle.Fill
            mUserControlCommandTarget.Location = New Point(3, 3)
            mUserControlCommandTarget.Name = "UserControlCommandTarget" & I
            mUserControlCommandTarget.Size = New Size(764, 605)

            mTabPageTargets.ResumeLayout(False)
            I += 1
        Next
    End Sub

    Private Sub FormCommand_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        For Each itemTarget As Target In parentReaderWriterCommand.ManagerAllTargets.Targets.Values
            itemTarget.IsControlCommadVisible = False
        Next

        parentReaderWriterCommand.UcheckMenuCommandClientServer(False)
        parentReaderWriterCommand = Nothing
        'RegistrationEventLog.EventLog_AUDIT_SUCCESS("Закрытие окна " & Me.Text)
    End Sub
End Class
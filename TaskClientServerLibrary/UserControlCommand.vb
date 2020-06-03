Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports TaskClientServerLibrary.Clobal

Public Class UserControlCommand
    Private mTarget As Target ' связанный поставщик для отображения его данных
    Private mIsServer As Boolean
    Private isLoaded As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(inTarget As Target)
        MyBase.New()

        InitializeComponent()
        mTarget = inTarget
        mIsServer = inTarget.ParrentManagerTargets.ParrentReaderWriterCommand.IsServer

        If mIsServer Then
            SetToolTip(mTarget.HostName)
        Else
            SetToolTip(SERVER)
        End If
    End Sub

    Private Sub UserControlCommand_Load(sender As Object, e As EventArgs) Handles Me.Load
        InitializeDataSource()

        With mTarget
            TextBoxPipeServerStatus.Text = .PipeServerStatus
            TextBoxURLSend.Text = .URLSendLocation
            TextBoxURLReceive.Text = .URLReceiveLocation
            TextBoxError.Text = .ErrorCommand
            TimeStampTextBox.Text = .TimeStampTarget
            SendTextBox.Text = .SendText
            ReceiveTextBox.Text = ConvertStringToXML(.CommandValueReader)

            InitializeListView(ListTaskSend)
            InitializeListView(ListTaskReceive)
            RefreshListCommandsReceive()
            RefreshListCommandsSend()
        End With

        'наполнить ComboBoxTasks и назначить обработчик
        ComboBoxTasks.Items.AddRange(mTarget.ConfigSend.ManagerTasks.Tasks.Values.ToArray)
        RadioButtonIsServer.Checked = mIsServer
        ButtonListen.Enabled = False
        isLoaded = True
    End Sub

    Public Sub Close()
        mTarget = Nothing
    End Sub

    Private Sub SetToolTip(toReceive As String)
        ToolTip1.SetToolTip(ComboBoxTasks, $"Выбор команды для редактирования и отправки на {toReceive}")
        ToolTip1.SetToolTip(TextBoxURLSend, $"Сетевой адрес контейнера команды посылаемой на {toReceive}")
        ToolTip1.SetToolTip(TextBoxURLReceive, $"Сетевой адрес контейнера команды пришедшей от {toReceive}")

        ToolTip1.SetToolTip(TextBoxPipeServerStatus, $"Статус контейнера для команды посылаемой на {toReceive}")
        ToolTip1.SetToolTip(SendTextBox, $"Текст команды посылаемой на {toReceive} в формате XML")
        ToolTip1.SetToolTip(ListTaskSend, $"Список всех команд посылаемых на {toReceive} в текущем сеансе работы")
        ToolTip1.SetToolTip(DataGridSend, $"Значения параметров команды посылаемой {toReceive}")
        ToolTip1.SetToolTip(ButtonSend, $"Послать {toReceive} выбранную команду для исполнения")
        ToolTip1.SetToolTip(TimeStampTextBox, $"Время получения команды от {toReceive}")

        ToolTip1.SetToolTip(ReceiveTextBox, $"Текст команды пришедшей от {toReceive} в формате XML")
        ToolTip1.SetToolTip(DataGridReceive, $"Значения параметров команды принятой от {toReceive}")
        ToolTip1.SetToolTip(ListTaskReceive, $"Список всех пришедших команд от {toReceive} в текущем сеансе работы")
        ToolTip1.SetToolTip(TextBoxError, $"Текст ошибки соединения с {toReceive} по сети")
    End Sub

    ''' <summary>
    ''' Преобразование линейной строки команды XML в форматированную строку
    ''' </summary>
    ''' <param name="inString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConvertStringToXML(inString As String) As String
        If inString Is Nothing OrElse inString = COMMAND_NOTHING Then Return COMMAND_NOTHING

        Dim tr As TextReader = New StringReader(inString)
        Dim doc As XDocument = XDocument.Load(tr)
        Dim sb As StringBuilder = New StringBuilder()

        Using sr1 = New StringWriter(sb)
            doc.Save(sr1, SaveOptions.None)
            Return sb.ToString
        End Using
    End Function

#Region "DataGrid"
    ''' <summary>
    ''' Связать таблицы контрола с поставщиками данных
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDataSource()
        DataGridSend.DataSource = mTarget.ConfigSend.GetDataTable()
        DataGridReceive.DataSource = mTarget.ConfigReceive.GetDataTable()
        SetDataGridSendColumnsSizeMode()
        SetDataGridReceiveColumnsSizeMode()
    End Sub

    ''' <summary>
    ''' Настройка внешнего вида таблицы
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataGridSendColumnsSizeMode()
        For I As Integer = 0 To DataGridSend.Columns.Count - 1
            DataGridSend.Columns(I).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridSend.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
            DataGridSend.Columns(I).ReadOnly = True
        Next
    End Sub

    ''' <summary>
    ''' Настройка внешнего вида таблицы
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataGridReceiveColumnsSizeMode()
        For I As Integer = 0 To DataGridReceive.Columns.Count - 1
            DataGridReceive.Columns(I).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridReceive.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
            DataGridReceive.Columns(I).ReadOnly = True
        Next
    End Sub

    ''' <summary>
    ''' Настройка внешнего вида листа пришедших и отправляемых команд
    ''' </summary>
    ''' <param name="lView"></param>
    ''' <remarks></remarks>
    Private Sub InitializeListView(ByRef lView As ListView)
        Dim width As Integer = lView.Width

        lView.Items.Clear()
        lView.Columns.Clear()
        lView.Columns.Add(ID_COMMAND_LV, ID_COMMAND_LV, CInt(width * 0.5 / 4) - 2, HorizontalAlignment.Left, 0)
        lView.Columns.Add(COMMAND_DESCRIPTION_LV, COMMAND_DESCRIPTION_LV, CInt(width * 2 / 4) - 2, HorizontalAlignment.Left, 0)
        lView.Columns.Add(COMMANDER_ID_LV, COMMANDER_ID_LV, CInt(width * 0.5 / 4) - 2, HorizontalAlignment.Left, 0)
        lView.Columns.Add(INDEX_COMMAND_LV, INDEX_COMMAND_LV, CInt(width * 1 / 4) - 2, HorizontalAlignment.Left, 0)
    End Sub

    Private Sub UserControlCommandShassis_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If isLoaded Then
            ResizeListView(ListTaskSend)
            ResizeListView(ListTaskReceive)
        End If
    End Sub

    Private Sub ResizeListView(ByRef List As ListView)
        Dim listViewWidth As Integer = List.Width

        List.Columns(ID_COMMAND_LV).Width = CInt(listViewWidth * 0.5 / 4) - 2
        List.Columns(COMMAND_DESCRIPTION_LV).Width = CInt(listViewWidth * 2 / 4) - 2
        List.Columns(COMMANDER_ID_LV).Width = CInt(listViewWidth * 0.5 / 4) - 2
        List.Columns(INDEX_COMMAND_LV).Width = CInt(listViewWidth * 1 / 4) - 2
    End Sub
#End Region

    ''' <summary>
    ''' Нужно в какой-то таблице установить свойства выделенного из списка и клонорованной задачи, 
    ''' а затем ее послать на выполнениею
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonSend_Click(sender As Object, e As EventArgs) Handles ButtonSend.Click
        SendQueryByHand()
    End Sub

    ''' <summary>
    ''' После установки желаемых значений параметров команды
    ''' запись их в класс XMLConfigSend, а затем посылка на выполнение
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SendQueryByHand()
        If ComboBoxTasks.SelectedIndex <> -1 Then
            Dim runingTask As ManagerTaskApplication.TaskApplication = CType(ComboBoxTasks.SelectedItem, ManagerTaskApplication.TaskApplication).Clone

            With mTarget.ConfigSend
                .Clear()
                .AddRow(COMMAND_NAME, runingTask.Name, TypeParam.String)
                .AddRow(COMMAND_DESCRIPTION, runingTask.Description, TypeParam.String)

                If runingTask.Parameters.Values.Count > 0 Then
                    dgvParameters.Rows(0).Cells(0).Selected = True
                    For Each itemParameter As ManagerTaskApplication.TaskApplication.Parameter In runingTask.Parameters.Values
                        .AddRow($"{itemParameter.Number.ToString} {COMMAND_PARAMETER}",
                                 dgvParameters.Rows(itemParameter.Number - 1).Cells(1).Value, itemParameter.Type)
                    Next
                End If

                'Dim _random As Random = New Random(Convert.ToInt32((DateTime.Now.Millisecond >> 32)))
                .AddRow(INDEX, Convert.ToInt32(RandomProvider.GetThreadRandom().NextDouble * (2 ^ 31)).ToString, TypeParam.String)
                .AddRow(COMMAND_COMMANDER_ID, $"Компьютер:<{My.Computer.Name}> соединение:<{mTarget.HostName}> канал:<{mTarget.NamePiepSend}>", TypeParam.String)
            End With

            mTarget.ParrentManagerTargets.ParrentReaderWriterCommand.SendXMLCommand(mTarget)
        End If
    End Sub

    ''' <summary>
    ''' Выделенная из списка задача клонируется.
    ''' Выведенные в таблице строки параметров позволяют установить желаемые значения.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBoxTasks_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxTasks.SelectedIndexChanged
        If ComboBoxTasks.SelectedIndex <> -1 Then
            Dim selectedTask As ManagerTaskApplication.TaskApplication = CType(ComboBoxTasks.SelectedItem, ManagerTaskApplication.TaskApplication)
            ButtonSend.Select()
            dgvParameters.Rows.Clear()

            If selectedTask.Parameters.Values.Count > 0 Then
                dgvParameters.Rows.Add(selectedTask.Parameters.Count)

                For Each itemParameter As ManagerTaskApplication.TaskApplication.Parameter In selectedTask.Parameters.Values
                    dgvParameters.Rows(itemParameter.Number - 1).Cells(0).Value = CType(itemParameter.Number, Object)
                    dgvParameters.Rows(itemParameter.Number - 1).Cells(1).Value = CType(itemParameter.Value, Object)
                    dgvParameters.Rows(itemParameter.Number - 1).Cells(2).Value = CType(itemParameter.Type, Object)
                    dgvParameters.Rows(itemParameter.Number - 1).Cells(3).Value = CType(itemParameter.Description, Object)
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' В интерактивном режиме обновление из сервисного класса mReaderWriterCommandClass.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateDataGridReceive()
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() UpdateDataGridReceive()))
        Else
            DataGridReceive.DataSource = mTarget.ConfigReceive.GetDataTable()
            SetDataGridReceiveColumnsSizeMode()
        End If
    End Sub

    ''' <summary>
    ''' В интерактивном режиме обновление из сервисного класса mReaderWriterCommandClass.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateDataGridSend()
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() UpdateDataGridSend()))
        Else
            DataGridSend.DataSource = mTarget.ConfigSend.GetDataTable()
            SetDataGridSendColumnsSizeMode()
        End If
    End Sub

    ''' <summary>
    ''' Сбросить таблицу
    ''' </summary>
    Public Sub SetNothigDataSourceForDataGridSend()
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() SetNothigDataSourceForDataGridSend()))
        Else
            DataGridSend.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' В интерактивном режиме обновление из сервисного класса mReaderWriterCommandClass.
    ''' При загрузке контрола восстановить кешированные данные.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshListCommandsReceive()
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() RefreshListCommandsReceive()))
        Else
            Dim itmX As ListViewItem

            ListTaskReceive.Items.Clear()

            For Each itemCommand As CommandForListViewItem In mTarget.ListCommandsReceive
                itmX = New ListViewItem(itemCommand.IDCommamd) With {
                    .ForeColor = itemCommand.Color
                }
                itmX.SubItems.Add(itemCommand.Description)
                itmX.SubItems.Add(itemCommand.CommanderID)
                itmX.SubItems.Add(itemCommand.IndexCommamd)
                ListTaskReceive.Items.Add(itmX)
                itmX.Selected = True
                itmX.EnsureVisible()
            Next
        End If
    End Sub

    ''' <summary>
    ''' В интерактивном режиме обновление из сервисного класса mReaderWriterCommandClass.
    ''' При загрузке контрола восстановить кешированные данные.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshListCommandsSend()
        If InvokeRequired Then
            'Если вызов не из UI thread, продолжить рекурсивный вызов,
            'пока не достигнут UI thread
            'Invoke(New EventHandler(Of EventArgs)(AddressOf _data_LoadStarted), sender, e)
            'Invoke(New MethodInvoker(Sub() UpdateStatus(status, percent)))

            Invoke(New MethodInvoker(Sub() RefreshListCommandsSend()))
        Else
            'textBoxLog.AppendText("Load started" & Environment.NewLine)

            Dim itmX As ListViewItem
            ListTaskSend.Items.Clear()

            For Each itemCommand As CommandForListViewItem In mTarget.ListCommandsSend
                itmX = New ListViewItem(itemCommand.IDCommamd) With {
                    .ForeColor = itemCommand.Color
                }
                itmX.SubItems.Add(itemCommand.Description)
                itmX.SubItems.Add(itemCommand.CommanderID)
                itmX.SubItems.Add(itemCommand.IndexCommamd)
                ListTaskSend.Items.Add(itmX)
                itmX.Selected = True
                itmX.EnsureVisible()
            Next
        End If
    End Sub

    Public Sub UpdateSendTextBox(text As String)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() UpdateSendTextBox(text)))
        Else
            SendTextBox.Text = text
        End If
    End Sub

    Public Sub UpdateStatusCommandPipeServer(text As String)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() UpdateStatusCommandPipeServer(text)))
        Else
            TextBoxPipeServerStatus.Text = text
        End If
    End Sub

    Public Sub UpdateTimeStamp(text As String)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() UpdateTimeStamp(text)))
        Else
            TimeStampTextBox.Text = text
        End If
    End Sub

    Public Sub UpdateError(text As String)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() UpdateError(text)))
        Else
            TextBoxError.Text = text
        End If
    End Sub

    Public Sub SetButtonSendEnabled(isEnabled As Boolean)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() SetButtonSendEnabled(isEnabled)))
        Else
            ButtonSend.Enabled = isEnabled
        End If
    End Sub

    'Public Sub Update(text As String)
    '    If InvokeRequired Then
    '        Invoke(New MethodInvoker(Sub() (text)))
    '    Else
    '        .Text = text
    '    End If
    'End Sub

    ''обработчик события главной формы, вызываемый из другого потока
    'Private Sub _data_LoadStarted(sender As Object, e As EventArgs)
    '    If InvokeRequired Then
    '        'Если вызов не из UI thread, продолжить рекурсивный вызов,
    '        'пока не достигнут UI thread
    '        Invoke(New EventHandler(Of EventArgs)(AddressOf _data_LoadStarted), sender, e)
    '    Else
    '        'textBoxLog.AppendText("Load started" & Environment.NewLine)
    '    End If
    'End Sub
End Class
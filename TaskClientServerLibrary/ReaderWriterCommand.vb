Imports System.Drawing
Imports System.Reflection
Imports System.Timers
Imports System.Windows.Forms
Imports TaskClientServerLibrary.Clobal

' 1. Созданы отдельные асинхронные события "обмена командами".
' На сосновании количества подключённых target создаются 2 по два сетевых каналов типа Read и Write
' для двухстороннего обмена командами XML с индивидуальной упаковкой.
' На события каналов созданы подписки с уведомлением. 
' В случае прихода новой команды вызвается событие приёма команды в подписчике.
' Каналы созданы в ассинхронной манере с уведомлением о результате операции для обработки ошибок.
' Каждому target присваивается класс парсер работы с XML командами (XMLConfigCommand), а им в свою
' очередь присваиваются классы менеджеров команд (ManagerTaskApplication), которые работают только с записанными 
' в файлы "TasksClientServer.xml" шаблонами (сигнатурами) разрешённых команд.

' 2. Подписчик ReaderSubscriber target по свойству LastCommand (кешировать и анализировать на повтор) определяет новую пришедшую команду
' и в случае прихода новой команды она расшифровывается и исполняется. 

' В отличии от команды посылаемой вручную путём выбора из списка и заполнения атрибутов в таблице вручную, 
' программная команда заносится в очередь target.CommandWriterQueue и событии таймера извлекается и посылается на target.
' По таймеру OnTimedEvent 1 сек производится запись команд на исполнение для всех target,
' участвующих в работе из очереди команд (в случае автоматической генерации команд) и вызывается в методе RunTask(TaskReceive).
' При получении <ответа> от target ищется совпадение ИндексКоманды и изменяется цвет отправленной команды в листе отправленных команд.
' Через промежуток времени 10 сек производится запись Nothing, чтобы target не тратило время на долгий разбор команды для анализа, а сразу выходило из цикла.

' 3. Результаты ответов от target или принятые команды отображаются в сообщениях на вкладке окна FormCommand, а при загрузке окна "Обмена командами"
' принятые команды отображаются и там.
' target имеет поля кеширующие все свойства контрола и истории вызова команд,
' UserControlCommand при его отображении на закладке формы, элементы контрола заполняются из этих свойств
' при загрузке формы обмена командами истории всех вызовов восстанавливалась в листах контрола свзанного с target.
' Контрол служит только интерфейсом отображения и командным вызовом методов target.

' 4. На target:
' При получении на target анализирует поля <Name>Command ID</Name> и поля <Name>Commander ID</Name> на повторы для определения новой команды
' Command ID для команд подверждения берётся из пришедших команд от Сервера.
' Для вновь вводимых команд Command ID генерируется программно.

' <Task Name="Сообщение" Description="Послать сообщение на другой компьютер" ProcedureName="Сообщение" WhatModule="FormMain" Index="123">
'     <Parameter Key="1" Value="0" Type="String" Description="Текст посылаемого сообщеня" />
' </Task>
' Можно в файле TasksClientServer.xml описать любую задачу которая вызывает открытую процедуру
' в основной форме FormMain или в основной форме frmBaseKT.vb
' процедуры или должны быть или их надо добавить

''' <summary>
''' Сервисный класс обслуживания командного обмена между target
''' </summary>
''' <remarks></remarks>
Public Class ReaderWriterCommand
    ''' <summary>
    ''' Управляющий класс коллекцией компьютеров на стенде 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ManagerAllTargets As ManagerTargets
    ''' <summary>
    ''' Имя удаленного компьютера, к которому нужно подключиться, или значение ".", чтобы указать локальный компьютер. 
    ''' </summary>
    Public Property ServerName As String = "." ' "DESKTOP-6SPCMGA"

    ''' <summary>
    ''' Метод в основном окне для вывода сообщения
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AppendMethod As Action(Of String, Integer, MessageBoxIcon)

    Public ReadOnly Property IsServer As Boolean = False
    Private Const LIM_COUNT_TIMED_EVENT As Integer = 10 ' через 10 сек. пошлётся всем target команда Nothing
    Private counterTimedEvent As Integer ' счётчик 10 секунд для посылки команды Nothing

    Private Const TIMER_INTERVAL As Integer = 1000
    Private ReadOnly aTimer As Timers.Timer

    ' управление коллекцией разрешённых задач
    'Private ReadOnly mTasksReceiveManager As ManagerTaskApplication' если задачи на target отличаются, например на cRio
    Private ReadOnly mTasksSendManager As ManagerTaskApplication
    ''' <summary>
    ''' вызывающая основная форма, в которая содержит вызываемые командой продедуры
    ''' </summary>
    Private mParentForm As Form
    ''' <summary>
    ''' Отображение формы производится из меню вызывающей основной формы
    ''' </summary>
    Public FormCommander As FormCommand
    ''' <summary>
    ''' заголовок окна клиентской вкладки
    ''' </summary>
    Public ReadOnly Property Caption As String
    ''' <summary>
    ''' Число клиентов для регистратора запущенного как Сервер или
    ''' номер клиента для регистратора запущенного как Клиент.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CountClientOrNumberClient As Integer

    Public Sub New(inParentForm As Form,
                   inPathРесурсы As String,
                   inIsServer As Boolean,
                   inCountClientOrNumberClient As Integer,
                   inServerWorkingFolder As String,
                   inClientWorkingFolder As String,
                   inAction As Action(Of String, Integer, MessageBoxIcon))
        mParentForm = inParentForm
        IsServer = inIsServer
        CountClientOrNumberClient = inCountClientOrNumberClient

        If IsServer Then
            ' образец: "\\006-stend21\System\doublearray" ' "\\localhost\system\doublearray"
            'sendAddressURL = "\\localhost\system\task1" ' было "dstp://localhost/task1"
            ServerName = GetReceiveAddressURL(inClientWorkingFolder) ' Клиент Рабочий Каталог
            Caption = SERVER
        Else ' клиент' просмотр снимка
            'sendAddressURL = "\\localhost\system\task2"
            ' только для случая при запуске просмотра снимков
            If inServerWorkingFolder Is Nothing Then inServerWorkingFolder = inClientWorkingFolder

            ServerName = GetReceiveAddressURL(inServerWorkingFolder) ' Server Рабочий Каталог
            Caption = "Клиент"
        End If

        ' надо считать из конфигурационного файла или ещё как-то создать
        ManagerAllTargets = New ManagerTargets(Me)

        If ManagerAllTargets.LoadTargets() AndAlso ManagerAllTargets.Count = 0 Then
            ManagerAllTargets = Nothing
            MessageBox.Show("Отсутствуют Клиент-Серверные задачи.", "Создание задач Клиент-Серверного обмена", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'mTasksReceiveManager = New ManagerTaskApplication(inPathРесурсы)
        mTasksSendManager = New ManagerTaskApplication(inPathРесурсы)

        AppendMethod = inAction
        CreateCollectionReceiverSender()
        AddHandlerDataUpdatedWriteCompleted()
        AddHandlerDataUpdatedWriteCompleted()

        aTimer = New Timers.Timer(TIMER_INTERVAL)
        AddHandler aTimer.Elapsed, AddressOf OnTimedEvent
        aTimer.Enabled = True

        'For Each itemTarget As Target In ManagerAllTargets.Targets.Values
        '    itemTarget.RunListenAsync()
        '    ' послать на очистку после подключения какого-либо клиента
        '    SendRequestProgrammed(itemTarget, "Очистка линии")
        'Next
    End Sub

    ''' <summary>
    ''' 1 этап Close
    ''' </summary>
    Public Sub Close()
        aTimer.Enabled = False

        Try
            If FormCommander IsNot Nothing Then FormCommander.Close()

            For Each itemTarget As Target In ManagerAllTargets.Targets.Values
                'Await itemTarget.WriteStopAsync
                'Await Task.Delay(100)
                itemTarget.Close()
            Next
        Catch ex As Exception
            Debug.WriteLine("ERROR: {0}", ex.ToString)
            'Await Task.Run(Function() Console.WriteLine("ERROR: {0}", ex.ToString))
        End Try
    End Sub

    Private Function GetReceiveAddressURL(workingFolder As String) As String
        If InStr(1, workingFolder, "\\") Then ' клиент на другом компьютере \\Stend_NN\c\Registration\Store\Channels.mdb
            ' вырезать имя компьютера
            Return $"{Mid(workingFolder, 3, InStr(3, workingFolder, "\") - 3)}"
        Else ' клиент на локальном компьютере D:\ПрограммыVBNET\Регистратор.NET\bin\Ресурсы\Channels.mdb
            Return "."
        End If
    End Function

    Public Sub ShowFormCommand()
        FormCommander = New FormCommand(Me)
        FormCommander.Show()
        FormCommander.Activate()
    End Sub

    Public Sub HideFormCommand()
        FormCommander.Close()
    End Sub

#Region "Сетевые переменные контейнеры команд обмена"
    ''' <summary>
    ''' Создать Коллекции Читателей Писателей.
    ''' Коллекция Read содержит имена сетевых переменных строкового типа Location: \\IP Address target\RT Variables\Invoke_SSDVariable.
    ''' Коллекция Write содержит имена сетевых переменных строкового типа Location: \\IP Address target\RT Variables\Invoke_cRIO.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateCollectionReceiverSender()
        For Each itemTarget As Target In ManagerAllTargets.Targets.Values
            ' т.к. задачи исполнения процедур в модулях(формах) программы одинаковы для исходящих и входящих команд, то из манеджеры одинаковы
            itemTarget.ConfigSend.ManagerTasks = mTasksSendManager
            itemTarget.ConfigReceive.ManagerTasks = mTasksSendManager ' mTasksReceiveManager
        Next
    End Sub

    ''' <summary>
    ''' Подписаться на уведомление получения команды от Target.
    ''' Присвоить делегат окончания отправки команды для Target.
    ''' </summary>
    Private Sub AddHandlerDataUpdatedWriteCompleted()
        ' подписаться на события уведомления
        For Each itemTarget As Target In ManagerAllTargets.ListTargets
            AddHandler itemTarget.DataUpdated, AddressOf OnSubscriber_DataUpdated
            AddHandler itemTarget.WriteCompleted, AddressOf Writer_WriteCompleted
        Next
    End Sub

    ''' <summary>
    ''' Делегат завершения асинхронной операции записи
    ''' одинаков для типов NetworkVariableWriter(Of String) и NetworkVariableWriter(Of Boolean)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Writer_WriteCompleted(sender As Object, e As WriteCompletedEventArgs)
        Dim targetWriter As Target = CType(e.UserState, Target)
        Dim errorText As String = String.Empty

        If e.Error IsNot Nothing Then
            ' обработать ошибку      
            errorText = Convert.ToString(e.Error)
            AppendMethod.Invoke($"Ошибка при записи для: {targetWriter.URLSendLocation}{Environment.NewLine}{errorText}",
                                targetWriter.IndexTab, MessageBoxIcon.Error)
        End If

        If targetWriter.IsControlCommadVisible Then
            targetWriter.UserControlCommandTarget.UpdateError(errorText)
        End If
    End Sub

    ''' <summary>
    ''' Отслеживание события изменения данных
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnSubscriber_DataUpdated(ByVal sender As Object, ByVal e As DataUpdatedEventArgs(Of String))
        Dim messageReceive As String
        Dim targetReader As Target = Nothing

        Try
            'targetReader = ManagerAllTargets.GetTargetReaderFromURL(CType(sender, Target).URLSendLocation)
            targetReader = CType(sender, Target)

            If targetReader IsNot Nothing Then
                targetReader.TimeStampTarget = e.Data.TimeStamp.ToLocalTime().ToString()
                targetReader.PipeServerStatus = e.Data.Quality
                messageReceive = e.Data.GetValue() ' содержат все сведения о данных

                ' получить только отличающиеся от старых данные
                If messageReceive = "" OrElse messageReceive = COMMAND_NOTHING Then Exit Sub
                If targetReader.LastCommand = e.Data.GetValue Then
                    Exit Sub
                Else
                    ' обновить поля target
                    targetReader.CommandValueReader = e.Data.GetValue()
                    targetReader.LastCommand = e.Data.GetValue
                    targetReader.ConfigReceive.LoadXMLfromString(messageReceive)
                    PopulateReader(targetReader)
                    RunTask(targetReader)
                End If
            End If
        Catch ex As TimeoutException
            Const CAPTION As String = "Проблема с сетью:"
            Dim text As String = $"Получение данных коммандного обмена в <{NameOf(OnSubscriber_DataUpdated)}>: "
            If targetReader IsNot Nothing Then
                text &= targetReader.URLReceiveLocation
                AppendMethod.Invoke(text & Environment.NewLine & Convert.ToString(ex),
                                    KEY_RICH_TEXT_SERVER, MessageBoxIcon.Error)
            End If
            MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'RegistrationEventLog.EventLog_MSG_APPLICATION_MESSAGE(String.Format("<{0}> {1}", CAPTION, text))
        End Try
    End Sub

    ''' <summary>
    ''' Заполнить лист и таблицу Слушателя
    ''' </summary>
    Private Sub PopulateReader(targetReader As Target)
        Dim strIndex As String = targetReader.ConfigReceive.GetRowValue(INDEX)(0)

        ' поиск в листе посылки для отметки команды, требующей подтверждения
        For Each itemCommand As CommandForListViewItem In targetReader.ListCommandsSend
            If itemCommand.IndexCommamd = strIndex Then
                itemCommand.Color = Color.Green
                ' команда Найдена - пришел ответ о выполнении задачи, ее надо снять
                If targetReader.IsControlCommadVisible Then targetReader.UserControlCommandTarget.RefreshListCommandsSend()
                Exit For
            End If
        Next

        ' пришел запрос поставить задачу на выполнение
        With targetReader
            .ListCommandsReceive.Add(New CommandForListViewItem(
                                                    .ConfigReceive.GetRowValue(COMMAND_NAME)(0),
                                                    .ConfigReceive.GetRowValue(COMMAND_DESCRIPTION)(0),
                                                    .ConfigReceive.GetRowValue(COMMAND_COMMANDER_ID)(0),
                                                    .ConfigReceive.GetRowValue(INDEX)(0)))

            If .IsControlCommadVisible Then
                .UserControlCommandTarget.UpdateDataGridReceive()
                .UserControlCommandTarget.UpdateTimeStamp(.TimeStampTarget)
                .UserControlCommandTarget.UpdateStatusCommandPipeServer(.PipeServerStatus)
            End If

            AppendMethod.Invoke($"Получена команда: { .ConfigReceive.GetRowValue(COMMAND_DESCRIPTION)(0)} индекс: { .ConfigReceive.GetRowValue(INDEX)(0)}{Environment.NewLine} от компьютера: { .HostName}",
                                KEY_RICH_TEXT_SERVER, MessageBoxIcon.Information)
        End With

        'RegistrationEventLog.EventLog_AUDIT_SUCCESS($"Получена команда {readValue} от компьютера: {targetReader.HostName}")
    End Sub

    ''' <summary>
    ''' Запуск метода содержащегося в атрибутах RuningTask 
    ''' </summary>
    ''' <param name="inReader"></param>
    Private Sub RunTask(ByVal inReader As Target)
        ' Получить Type и MethodInfo
        ' Diagnostics.Process.GetCurrentProcess.ProcessName дает в среде строку "Registration.vshost"
        ' надо очистить
        'Dim MyType As Type = Type.GetType(Diagnostics.Process.GetCurrentProcess.ProcessName & "." & RuningTask.WhatModule) '("Registration.frmMain") 
        Try
            'Dim processName As String = Process.GetCurrentProcess.ProcessName
            'processName = Left(processName, If(InStr(1, processName, ".") = 0, Len(processName), InStr(1, processName, ".") - 1))
            'Dim MyType As Type = Type.GetType($"{processName}.{RuningTask.WhatModule}") '("Registration.frmMain") "Registration.FormSnapshotViewingDiagram"
            'Dim Mymethodinfo As MethodInfo = MyType.GetMethod(RuningTask.ProcedureName)

            ''RegistrationEventLog.EventLog_AUDIT_SUCCESS("RunTask " & RuningTask.ProcedureName)
            ''Dim parameters As Object() = {True, Модуль1.enuЗапросы.enuПоставитьМеткуКТ, "Пример"} 'ConfigReceive.GetValue(conПараметр)(0)}

            'If RuningTask.Parameters.Values.Count > 0 Then
            '    Dim parameters(RuningTask.Parameters.Values.Count - 1) As Object

            '    For Each itemParameter As TasksReceiveManager.TaskApplication.Parameter In RuningTask.Parameters.Values
            '        parameters(itemParameter.Number - 1) = Convert.ChangeType(itemParameter.Value, Type.GetType("System." & itemParameter.Type))
            '    Next

            '    Mymethodinfo.Invoke(mParentForm, parameters)
            'Else
            '    Mymethodinfo.Invoke(mParentForm, Nothing)
            'End If

            Dim hostName As String = inReader.HostName
            Dim indexResponse As String = inReader.ConfigReceive.GetRowValue(INDEX)(0)
            Dim runingTask As ManagerTaskApplication.TaskApplication = inReader.ConfigReceive.GetTask ' десериализация задачи с реальными параметрами
            Dim myType2 As Type = mParentForm.GetType
            Dim myMethodinfo2 As MethodInfo = myType2.GetMethod(runingTask.ProcedureName)

            If runingTask.Parameters.Values.Count > 0 Then
                Dim parameters(runingTask.Parameters.Values.Count) As Object

                For Each itemParameter As ManagerTaskApplication.TaskApplication.Parameter In runingTask.Parameters.Values
                    parameters(itemParameter.Number - 1) = Convert.ChangeType(itemParameter.Value, Type.GetType("System." & itemParameter.Type.ToString))
                Next

                parameters(runingTask.Parameters.Values.Count) = New String() {hostName, indexResponse}
                myMethodinfo2.Invoke(mParentForm, parameters)
            Else
                'Dim parameters As Object() = {inReader.HostName}
                myMethodinfo2.Invoke(mParentForm, {New String() {hostName, indexResponse}})
            End If
        Catch ex As Exception
            Dim caption As String = $"Процедура <{NameOf(RunTask)}>"
            Dim text As String = ex.ToString
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'RegistrationEventLog.EventLog_MSG_EXCEPTION($"<{CAPTION}> {text}")
        End Try
    End Sub
#End Region

#Region "Послать Команду"
    Private Sub OnTimedEvent(source As Object, e As ElapsedEventArgs)
        SendCommandOutOfQueueFromAllTargets()
        counterTimedEvent += 1
    End Sub

    'Private syncPointDoMonitor As Integer = 0 'для синхронизации

    ''' <summary>
    ''' Отправить команды на все Targets.
    ''' Вызыватся из одного обработчика таймера,
    ''' если будут вызываться из разных потоков,
    ''' то могут мешать друг другу, поэтому следует применять очередь команд в каждом target
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SendCommandOutOfQueueFromAllTargets()
        ' не стал применять синхронизацию (syncPointDoMonitor), т.к. при выводе окна из родительской формы события таймера теряются
        '<MethodImplAttribute(MethodImplOptions.Synchronized)>

        For Each itemTarget As Target In ManagerAllTargets.ListTargets
            ' '' Должна быть очередь команд для каждого компьютера. Какой-то сервис кладёт туда команду, а здесь происходит извлечение
            ' ''--- Тест ---------------------------------------------------------
            'Dim random As Random = New Random(Convert.ToInt32((DateTime.Now.Millisecond >> 32)))
            'itemTarget.CommandWriterQueue.Enqueue(New NetCommandForTask("Clear Polynomial Channel",
            '                                                                  New String() {((random.NextDouble) * 100).ToString,
            '                                                                                "2",
            '                                                                                "3",
            '                                                                                "4",
            '                                                                                "5",
            '                                                                                "6",
            '                                                                                "7"})) 'TODO: где-то занести значение в очередь
            ' ''--- Тест ---------------------------------------------------------

            'Dim sync As Integer = Interlocked.CompareExchange(syncPointDoMonitor, 1, 0)
            'If sync = 0 Then
            If itemTarget.CommandWriterQueue.Count > 0 Then
                While itemTarget.CommandWriterQueue.Count > 0
                    Dim mNetCommandForTask As NetCommandForTask = itemTarget.CommandWriterQueue.Dequeue()
                    SendRequestProgrammed(itemTarget, mNetCommandForTask)
                    ' задача ни чего не дала
                    'Dim tsk As Task = Task.Factory.StartNew(Sub() SendRequestProgrammed(itemTarget, mNetCommandForTask.NameCommand, mNetCommandForTask.Parameters))
                    'tsk.Wait()
                End While
            End If
            'syncPointDoMonitor = 0 ' освободить
        Next

        'TODO: проверить не пришло ли время очистить буфера команд для target
        'If countTimedEvent > LIM_COUNT_TIMED_EVENT Then
        '    countTimedEvent = 0
        '    For  itemTarget As Target In ManagerAllTargets.ListTargets
        '        SendRequestProgrammed(itemTarget, COMMAND_NOTHING)
        '    Next
        'End If
    End Sub

    ''' <summary>
    ''' Послать Запрос Программно
    ''' Необходимо занести значения в параметры
    ''' -> они заносятся в таблицу
    ''' -> таблица сереализуется в XML (было)
    ''' </summary>
    ''' <param name="targetWriter"></param>
    ''' <param name="inNetCommandForTask"></param>
    Private Sub SendRequestProgrammed(ByVal targetWriter As Target, inNetCommandForTask As NetCommandForTask)
        Dim procedureName As String = inNetCommandForTask.ProcedureName
        Dim querytask = mTasksSendManager.Tasks.Where(Function(task) task.Value.ProcedureName = procedureName).First.Value
        If querytask Is Nothing Then Throw New ArgumentNullException($"<{procedureName}> отсутствует в колекции <mTasksSendManager.Tasks> в процедуре <{NameOf(SendRequestProgrammed)}>")

        Dim taskSend As ManagerTaskApplication.TaskApplication = mTasksSendManager.Tasks(querytask.Name).Clone() ' десериализация задачи

        With targetWriter.ConfigSend
            If targetWriter.IsControlCommadVisible Then targetWriter.UserControlCommandTarget.SetNothigDataSourceForDataGridSend()

            .Clear()
            .AddRow(COMMAND_NAME, taskSend.Name, TypeParam.String)
            .AddRow(COMMAND_DESCRIPTION, taskSend.Description, TypeParam.String)

            If Not IsNothing(inNetCommandForTask.Parameters) Then
                If inNetCommandForTask.Parameters.Length = taskSend.Parameters.Values.Count Then
                    ' скопировать в itemParameter значение parameters(itemParameter.Number - 1)
                    For Each itemParameter As ManagerTaskApplication.TaskApplication.Parameter In taskSend.Parameters.Values
                        .AddRow($"{itemParameter.Number.ToString} {COMMAND_PARAMETER}", inNetCommandForTask.Parameters(itemParameter.Number - 1), itemParameter.Type)
                    Next
                Else
                    MessageBox.Show($"Число переданных параметров для исполнения не соответствует{Environment.NewLine}числу параметров в конфигурационном описании задачи.",
                                    $"Процедура <{NameOf(SendRequestProgrammed)}>", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1,
                                    Nothing)
                    Exit Sub
                End If
            End If

            If inNetCommandForTask.IsResponse Then
                .AddRow(INDEX, inNetCommandForTask.IndexResponse, TypeParam.String)
            Else
                'Dim random As Random = RandomProvider.GetThreadRandom()
                'Dim random As Random = New Random(Convert.ToInt32((DateTime.Now.Millisecond >> 32)))
                .AddRow(INDEX, Convert.ToInt32(RandomProvider.GetThreadRandom().NextDouble * (2 ^ 31)).ToString, TypeParam.String)
            End If

            .AddRow(COMMAND_COMMANDER_ID, $"Компьютер:<{My.Computer.Name}> соединение:<{targetWriter.HostName}> канал:<{targetWriter.NamePiepSend}>", TypeParam.String)
        End With

        SendXMLCommand(targetWriter)

        If targetWriter.IsControlCommadVisible Then targetWriter.UserControlCommandTarget.UpdateDataGridSend()
    End Sub

    '    <MethodImplAttribute(MethodImplOptions.Synchronized)>
    ''' <summary>
    ''' Сформировать XML Команду Отправить к Target.
    ''' Вызов из дежурного таймера по цепочке.
    ''' </summary>
    ''' <param name="targetWriter"></param>
    Public Sub SendXMLCommand(ByVal targetWriter As Target)
        Dim strIndex As String = targetWriter.ConfigSend.GetRowValue(INDEX)(0)

        ' поиск в листе приема
        If targetWriter.ListCommandsReceive.Count > 0 Then
            For Each itemCommand As CommandForListViewItem In targetWriter.ListCommandsReceive
                If itemCommand.IndexCommamd = strIndex Then
                    itemCommand.Color = Color.Green
                    ' задача Найдена - отвечаем на запрос после выполнения, ее надо снять
                    If targetWriter.IsControlCommadVisible Then targetWriter.UserControlCommandTarget.RefreshListCommandsReceive()
                    Exit For
                End If
            Next
        End If

        ' отметить и послать задачу на выполнение
        targetWriter.ListCommandsSend.Add(New CommandForListViewItem(targetWriter.ConfigSend.GetRowValue(COMMAND_NAME)(0),
                                                                    targetWriter.ConfigSend.GetRowValue(COMMAND_DESCRIPTION)(0),
                                                                    targetWriter.ConfigSend.GetRowValue(COMMAND_COMMANDER_ID)(0),
                                                                    targetWriter.ConfigSend.GetRowValue(INDEX)(0)))

        '' для команды Stop нет необходимости в упаковке в XML, поэтому посылается простой текст
        'If targetWriter.ConfigSend.GetRowValue(NAME_COMMAND) = COMMAND_STOP Then
        '    commandValueWriter = COMMAND_STOP
        'Else
        Dim commandXMLValueWriter As String = targetWriter.ConfigSend.ToString
        'End If

        targetWriter.SendText = commandXMLValueWriter

        If targetWriter.IsControlCommadVisible Then targetWriter.UserControlCommandTarget.UpdateSendTextBox(commandXMLValueWriter)

        SendCommandConcreteTargetAsync(targetWriter, commandXMLValueWriter)
        AppendMethod.Invoke($"Послана команда: {targetWriter.ConfigSend.GetRowValue(COMMAND_DESCRIPTION)} Индекс:{targetWriter.ConfigSend.GetRowValue(INDEX)}",
                            targetWriter.IndexTab, MessageBoxIcon.Information)
        'RegistrationEventLog.EventLog_AUDIT_SUCCESS($"Послана команда {commandValueWriter} на target: {targetWriter.HostName}")
    End Sub

    ''' <summary>
    ''' Отправить сформированную XML Команду к Target.
    ''' Вызов из дежурного таймера по цепочке.
    ''' </summary>
    ''' <param name="targetWriter"></param>
    ''' <remarks></remarks>
    Private Async Sub SendCommandConcreteTargetAsync(ByVal targetWriter As Target, commandValueWriter As String)
        ' при отсутствии связи ни чего не делать 
        counterTimedEvent = 0 ' сбросить счётчик времени
        targetWriter.CommandValueWriter = commandValueWriter
        ' передать в сеть
        Await targetWriter.WriteDataAsync(commandValueWriter)
    End Sub
#End Region

#Region "RaiseEvent FormCommandClosed"
    Public Sub UcheckMenuCommandClientServer(isVisible As Boolean)
        'CType(mParentForm, FormMain).MenuCommandClientServer.Checked = False
        OnFormCommandClosed(New FormCommandVisibleClosedEventArg(False))
    End Sub

    Public Delegate Sub FormCommandVisibleClosedEventHandler(ByVal sender As Object, ByVal e As FormCommandVisibleClosedEventArg)
    Public Event FormCommandClosed As FormCommandVisibleClosedEventHandler

    Private Sub OnFormCommandClosed(ByVal e As FormCommandVisibleClosedEventArg)
        'If FormCommandChanged IsNot Nothing Then
        RaiseEvent FormCommandClosed(Me, e)
        'End If
    End Sub
#End Region
End Class

Public Class FormCommandVisibleClosedEventArg
    Inherits EventArgs
    'Private eventAction As System.Action

    'Public Sub New(ByVal Message As String, ByVal ex As Exception, targetNumber As Integer) ', ByVal action As System.Action)
    '    MyBase.New()
    '    Me.Message = Message
    '    Me.ex = ex
    '    Me.targetNumber = targetNumber
    '    'Me.eventAction = action
    'End Sub

    'Public ReadOnly Property Message() As String
    'Public ReadOnly Property ex As Exception
    'Public ReadOnly Property targetNumber As Integer

    ''Public ReadOnly Property Action() As System.Action
    ''    Get
    ''        Return Me.eventAction
    ''    End Get
    ''End Property

    Public ReadOnly Property IsVisible As Boolean

    Public Sub New(ByVal inIsVisible As Boolean)
        MyBase.New()

        IsVisible = inIsVisible
    End Sub
End Class

'Private Sub GetEnum()
'    Console.WriteLine("**** Очень простой генератор подключений *****" & vbLf)
'    Dim ClassName As String = System.Configuration.ConfigurationManager.AppSettings("DiagramClassName")

'    ' Чтение ключа поставщика. 
'    Dim replyString As String = ConfigurationManager.AppSettings("provider")
'    ' Преобразование строки в перечисление. 
'    Dim dp As Reply = Reply.Unknown
'    If [Enum].IsDefined(GetType(Reply), replyString) Then
'        dp = DirectCast([Enum].Parse(GetType(Reply), replyString), Reply)
'    Else
'        Console.WriteLine("К сожалению, поставщик отсутствует.")
'    End If

'    ' Получение конкретного подключения. 
'    Dim myCn As IDbConnection = GetConnection(dp)
'    If myCn IsNot Nothing Then
'        Console.WriteLine("Ваше подключение — {О}", myCn.[GetType]().Name)
'    End If
'    ' Открытие, использование и закрытие подключения . . . 
'    Console.ReadLine()
'End Sub
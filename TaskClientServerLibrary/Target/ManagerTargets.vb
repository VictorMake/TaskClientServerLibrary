Imports System.Windows.Forms
Imports TaskClientServerLibrary.Clobal

''' <summary>
''' Управляющий класс коллекцией компьютеров на стенде 
''' </summary>
''' <remarks></remarks>
Public Class ManagerTargets
    Implements IEnumerable

    ' В классе конфигураторе стартовой формы определяются target содержащие каналы и производится их конфигурирование
    ' Далее производится создание менеджера Target(ов), где на основании включённых target создаётся коллекция Target
    ' каждый Target содержит сетевые переменные каналы

    ''' <summary>
    ''' число созданных target
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Count() As Integer
        Get
            Return mDictionaryTargets.Count
        End Get
    End Property

    ''' <summary>
    ''' Оболочка коллекции target
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Targets() As Dictionary(Of String, Target)
        Get
            Return mDictionaryTargets
        End Get
    End Property

    ''' <summary>
    ''' Оболочка коллекции target
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ListTargets() As List(Of Target)
        Get
            Return mListTargets
        End Get
    End Property

    ''' <summary>
    ''' элемент коллекции
    ''' </summary>
    ''' <param name="indexKey"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Public ReadOnly Property Item(ByVal indexKey As String) As Target
        Get
            Return mDictionaryTargets.Item(indexKey)
        End Get
    End Property

    Public ReadOnly Property ParrentReaderWriterCommand As ReaderWriterCommand

    Private mDictionaryTargets As New Dictionary(Of String, Target) ' внутренняя коллекция для управления target
    Private mListTargets As List(Of Target)
    Private mTargetCreated As Integer = 0 ' внутренний счетчик для подсчета созданных target можно использовать в заголовке

    Public Sub New(parrentReaderWriterCommandClass As ReaderWriterCommand)
        Me.ParrentReaderWriterCommand = parrentReaderWriterCommandClass
    End Sub

    ''' <summary>
    ''' перечислитель
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return mDictionaryTargets.GetEnumerator
    End Function

    ''' <summary>
    ''' удаление по номеру или имени или объекту?
    ''' </summary>
    ''' <param name="indexKey"></param>
    ''' <remarks></remarks>
    Public Sub Remove(ByRef indexKey As String) '
        ' удаление по номеру или имени или объекту?
        ' если целый тип то по плавающему индексу, а если строковый то по ключу
        mDictionaryTargets.Remove(indexKey)
        mTargetCreated -= 1
    End Sub

    Public Sub Clear()
        mDictionaryTargets.Clear()
    End Sub

    Protected Overrides Sub Finalize()
        mDictionaryTargets = Nothing
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' Если клиент создаётся только одна вкладка с конкретнной парой задач,
    ''' если сервер, то создаются вкладки по числу установленных клиентов.
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadTargets() As Boolean
        Dim success As Boolean = False

        Try
            If ParrentReaderWriterCommand.IsServer Then
                ' при создании автоматом добавляется в коллекцию
                ' там проверка на корректность
                Dim numberPair As Integer = 1

                For I As Integer = 1 To ParrentReaderWriterCommand.CountClientOrNumberClient
                    Dim clientName As String = CLIENT & I
                    If Me.NewTarget(New Target(clientName, Me, NamePipe & numberPair, NamePipe & (numberPair + 1))) Then
                        mDictionaryTargets(clientName).IndexRow = mDictionaryTargets.Count - 1 ' индекс в таблице которая уже создана
                        mDictionaryTargets(clientName).RunListenAsync()
                    End If

                    numberPair += 2
                Next
            Else ' клиент' просмотр снимка
                Dim clientName As String = CLIENT & ParrentReaderWriterCommand.CountClientOrNumberClient
                If Me.NewTarget(New Target(clientName,
                                                  Me,
                                                  NamePipe & (ParrentReaderWriterCommand.CountClientOrNumberClient * 2),
                                                  NamePipe & ParrentReaderWriterCommand.CountClientOrNumberClient * 2 - 1)) Then
                    mDictionaryTargets(clientName).IndexRow = mDictionaryTargets.Count - 1 ' индекс в таблице которая уже создана
                    mDictionaryTargets(clientName).RunListenAsync()
                End If
            End If

            mListTargets = mDictionaryTargets.Values.ToList
            success = True
        Catch ex As Exception
            Dim text As String = ex.ToString
            MessageBox.Show(text, NameOf(LoadTargets), MessageBoxButtons.OK, MessageBoxIcon.Error)
            'RegistrationEventLog.EventLog_MSG_EXCEPTION(String.Format("<{0}> {1}", CAPTION, text))
            success = False
        End Try

        Return success
    End Function

#Region "GetTarget"
    ''' <summary>
    ''' Поиск target по URLReceiveLocation в коллекции
    ''' </summary>
    ''' <param name="url"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTargetReaderFromURL(url As String) As Target
        Dim foundTarget As Target = Nothing

        For Each itemTarget As Target In Targets.Values
            If itemTarget.URLReceiveLocation = url Then
                foundTarget = itemTarget
                Exit For
            End If
        Next

        Return foundTarget
    End Function

    ''' <summary>
    ''' Поиск target по URLSendLocation в коллекции
    ''' </summary>
    ''' <param name="url"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTargetWriterFromURL(url As String) As Target
        Dim foundTarget As Target = Nothing

        For Each itemTarget As Target In Targets.Values
            If itemTarget.URLSendLocation = url Then
                foundTarget = itemTarget
                Exit For
            End If
        Next

        Return foundTarget
    End Function

    ''' <summary>
    ''' Поиск target по HostName в коллекции
    ''' </summary>
    ''' <param name="inHostName"></param>
    ''' <returns></returns>
    Public Function FindTargetInManager(inHostName As String) As Target
        Dim foundTarget As Target = Nothing

        For Each itemTarget As Target In mDictionaryTargets.Values
            If itemTarget.HostName = inHostName Then
                foundTarget = itemTarget
                Exit For
            End If
        Next

        If foundTarget Is Nothing Then
            Const CAPTION As String = NameOf(FindTargetInManager)
            Dim text As String = String.Format("Для компьютера с адресом {0} не найден соответствующий компьютер в коллекции.", inHostName)
            MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'RegistrationEventLog.EventLog_MSG_EXCEPTION(String.Format("<{0}> {1}", CAPTION, text))
        End If

        Return foundTarget
    End Function
#End Region

    ''' <summary>
    ''' Создание нового target
    ''' </summary>
    ''' <param name="inNewTarget"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NewTarget(ByVal inNewTarget As Target) As Boolean
        Dim success As Boolean = False

        If mDictionaryTargets.ContainsKey(inNewTarget.HostName) Then
            Const CAPTION As String = "Добавление нового компьютера"
            Dim text As String = String.Format("Компьютер с именем {0} уже загружен!", inNewTarget.HostName)
            MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'RegistrationEventLog.EventLog_MSG_APPLICATION_MESSAGE(String.Format("<{0}> {1}", CAPTION, text))
            Return success
        End If

        Try
            mDictionaryTargets.Add(inNewTarget.HostName, inNewTarget)
            mTargetCreated += 1
            'RegistrationEventLog.EventLog_AUDIT_SUCCESS("Загрузка нового компьютера " & inNewTarget.HostName)

            If mDictionaryTargets.ContainsKey(inNewTarget.HostName) Then
                success = True
            Else
                success = False
            End If

        Catch exp As Exception
            Dim CAPTION As String = exp.Source
            Dim text As String = exp.Message
            MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'RegistrationEventLog.EventLog_MSG_EXCEPTION(String.Format("<{0}> {1}", CAPTION, text))
            success = False
        End Try

        Return success
    End Function
End Class
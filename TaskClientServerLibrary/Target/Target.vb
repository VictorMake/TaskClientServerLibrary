Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports System.Threading.Tasks
Imports TaskClientServerLibrary.Clobal

''' <summary>
''' Класс описания клиент-сервера в межпрограммном командном обмене
''' </summary>
''' <remarks></remarks>
Public Class Target
    'Public Event PropertyChanged As PropertyChangedEventHandler
    ''' <summary>
    ''' Событие генерируемое при приёме новой команды
    ''' </summary>
    Public Event DataUpdated As EventHandler(Of DataUpdatedEventArgs(Of String))
    ''' <summary>
    ''' Событие генериремоу при удачной отправке команды или при ошибке
    ''' </summary>
    Public Event WriteCompleted As EventHandler(Of WriteCompletedEventArgs)
    ''' <summary>
    ''' Имя слушающего канала. 
    ''' </summary>
    Public Property NamePiepListen As String
    ''' <summary>
    ''' Имя пишущего канала. 
    ''' </summary>
    Public Property NamePiepSend As String
    ''' <summary>
    ''' Имя Target
    ''' </summary>
    ''' <returns></returns>
    Public Property HostName As String
    ''' <summary>
    ''' Родительский управляющий класс коллекцией компьютеров на стенде 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ParrentManagerTargets As ManagerTargets
    ''' <summary>
    ''' индекс для связи со строкой таблицы
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IndexRow As Integer
    ''' <summary>
    ''' URL канала получателя
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property URLReceiveLocation As String
    ''' <summary>
    ''' URL канала отправителя
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property URLSendLocation As String

    ''' <summary>
    ''' Статус соединения канала получателя
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PipeServerStatus As String

    ''' <summary>
    ''' Ошибка отправки команды
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrorCommand As String

    ''' <summary>
    ''' Временная метка команды от target
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TimeStampTarget As String

    ''' <summary>
    ''' XML текст команды, отправляемой на target
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SendText As String

    ''' <summary>
    ''' Содержит текст последней команды для сравнения
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LastCommand As String = Nothing

    Private mCommandValueReader As String
    ''' <summary>
    ''' Прочитанное значение из транспорта команды канала получателя
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>       
    Public Property CommandValueReader As String
        Get
            Return mCommandValueReader
        End Get
        Set(ByVal value As String)
            ' в фоне запускаются потоки ассинхронного чтения значений команд и они передаются на расшифровку и исполнение в методе Set свойства CommandValue для target
            ' там в случае прихода новой команды вызвать событие приёма команды для подписчиков - формы просмотра и отправки команд
            If IsControlCommadVisible Then
                UserControlCommandTarget.ReceiveTextBox.Text = UserControlCommandTarget.ConvertStringToXML(value)
            End If

            mCommandValueReader = value
        End Set
    End Property

    ''' <summary>
    ''' Значение для записи в транспорт команды канала отправителя
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>       
    Public Property CommandValueWriter As String

    ''' <summary>
    ''' Индекс target для связи с закладкой при логировании
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IndexTab As Integer

    ''' <summary>
    ''' Упаковка команды отправляемой на target
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ConfigSend As XMLConfigCommand

    ''' <summary>
    ''' Работа по расшифровке команды полученной от target
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ConfigReceive As XMLConfigCommand

    ''' <summary>
    ''' Флаг видимости пользовательского контрола устанавливается при загрузке 
    ''' и сбрасывается при выгрузке Окна просмотра обмена командами.
    ''' При True в ReaderWriterCommand должен через данный класс
    ''' управлять отображением состояний данного класса  
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsControlCommadVisible As Boolean

    ''' <summary>
    ''' Пользовательский контрол отображающий состояние обмена командами между всеми target
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserControlCommandTarget As UserControlCommand
    ''' <summary>
    ''' Очередь команд для отправки добавленных программно или вручную
    ''' </summary>
    ''' <returns></returns>
    Public Property CommandWriterQueue As Queue(Of NetCommandForTask)
    Private Const CAPACITY As Integer = 10 ' ёмкость очереди

    ' Коллекции, хранящие состояние командного обмена. При визуализации формы, 
    ' эти коллекции отображают своё состояние на контролах на закладке, связанной с target
    Public WithEvents ListCommandsSend As CommandSendReceiveCollection
    Public WithEvents ListCommandsReceive As CommandSendReceiveCollection

    Private myPipeClientServer As PipeClientServer
    Private isRunListen As Boolean = True ' бесконечный цикл прослушивания нового соединения для получения команды

    Public Sub New(ByVal inHostName As String, inParrentManagerTargets As ManagerTargets, inNamePiepListen As String, inNamePiepSend As String)
        HostName = inHostName
        ParrentManagerTargets = inParrentManagerTargets
        NamePiepListen = inNamePiepListen
        NamePiepSend = inNamePiepSend
        ' сформировать сетевые адреса для сетевых команд
        URLReceiveLocation = NamePiepListen
        URLSendLocation = $"\\{inParrentManagerTargets.ParrentReaderWriterCommand.ServerName}\pipe\{NamePiepSend}"
        PipeServerStatus = "Listening - OK" ' TextBoxSendStatus.Text
        CommandWriterQueue = New Queue(Of NetCommandForTask)(CAPACITY)
        InitializeCollections()
        myPipeClientServer = New PipeClientServer(inNamePiepListen, inNamePiepSend, inParrentManagerTargets.ParrentReaderWriterCommand.ServerName)

        ConfigSend = New XMLConfigCommand(WhoIsUpdate.DataView)
        ConfigReceive = New XMLConfigCommand(WhoIsUpdate.DataView)
    End Sub

    Public Sub InitializeCollections()
        ListCommandsSend = New CommandSendReceiveCollection
        ListCommandsReceive = New CommandSendReceiveCollection
    End Sub
    ''' <summary>
    ''' Получить контрол для вкладки связанный с target
    ''' </summary>
    ''' <returns></returns>
    Public Function GetUserControlCommandTarget() As UserControlCommand
        UserControlCommandTarget = New UserControlCommand(Me)
        IsControlCommadVisible = True
        Return UserControlCommandTarget
    End Function

    Private Sub ListCommandsReceive_Added(sender As Object, e As AlarmChangeCommandEventArgs) Handles ListCommandsReceive.Added
        If IsControlCommadVisible Then UserControlCommandTarget.RefreshListCommandsReceive()
    End Sub

    Private Sub ListCommandsSend_Added(sender As Object, e As AlarmChangeCommandEventArgs) Handles ListCommandsSend.Added
        If IsControlCommadVisible Then UserControlCommandTarget.RefreshListCommandsSend()
    End Sub

    ''' <summary>
    ''' Асинхронная отправка команды
    ''' </summary>
    ''' <param name="commandValueWriter"></param>
    ''' <returns></returns>
    Public Async Function WriteDataAsync(commandValueWriter As String) As Task
        If IsControlCommadVisible Then
            UserControlCommandTarget.SetButtonSendEnabled(False)
            UserControlCommandTarget.UpdateError("")
        End If

        Try
            Await myPipeClientServer.SendAsync(commandValueWriter, 1000)
        Catch ex As Exception
            Debug.WriteLine("ERROR: {0}", ex.ToString)
            'Await Task.Run(Function() Console.WriteLine("ERROR: {0}", ex.ToString))
            If IsControlCommadVisible Then UserControlCommandTarget.UpdateError(ex.ToString)
            ErrorCommand = ex.ToString
            RaiseEvent WriteCompleted(Me, New WriteCompletedEventArgs(ex, False, Me))
        End Try

        If IsControlCommadVisible Then
            UserControlCommandTarget.UpdateSendTextBox(commandValueWriter)
            UserControlCommandTarget.SetButtonSendEnabled(True)
        End If
    End Function

    ''' <summary>
    ''' Запустить бесконечный цикл прослушивания
    ''' </summary>
    Public Async Sub RunListenAsync()
        Await RunListen()
    End Sub
    ''' <summary>
    ''' Запустить бесконечный цикл прослушивания
    ''' </summary>
    ''' <returns></returns>
    Private Async Function RunListen() As Task
        Try
            While isRunListen
                Dim messageReceive As String = Await myPipeClientServer.Listen()

                If messageReceive = COMMAND_STOP Then
                    isRunListen = False
                    Exit Function
                End If

                RaiseEvent DataUpdated(Me, New DataUpdatedEventArgs(Of String)(New NetworkVariableData(Of String)(messageReceive)))
            End While
        Catch ex As Exception
            ' ошибка возникает при закрытии слушающего ожидающего подключения,
            ' которая перехватывается здесь
            Debug.WriteLine("ERROR: {0}", ex.ToString)
            RaiseEvent WriteCompleted(Me, New WriteCompletedEventArgs(ex, False, Me))
        End Try
    End Function

    ''' <summary>
    ''' 2  этап Close
    ''' </summary>
    Public Sub Close()
        If UserControlCommandTarget IsNot Nothing Then
            UserControlCommandTarget.Close()
            UserControlCommandTarget = Nothing
        End If

        isRunListen = False
        myPipeClientServer.Close()
    End Sub
End Class

Public Class DataUpdatedEventArgs(Of TValue)
    Inherits EventArgs
    '
    ' Сводка:
    '     Initializes a new instance of the NationalInstruments.NetworkVariable.DataUpdatedEventArgs`1
    '     class with the specified data.
    '
    ' Параметры:
    '   data:
    '     The NationalInstruments.NetworkVariable.NetworkVariableData`1 that contains information
    '     about the read data.
    '
    ' Исключения:
    '   T:System.ArgumentNullException:
    '     data is null.
    Public Sub New(inData As NetworkVariableData(Of TValue))
        Data = inData
    End Sub

    '
    ' Сводка:
    '     Gets a NationalInstruments.NetworkVariable.NetworkVariableData`1 that contains
    '     information about the read data.
    Public ReadOnly Property Data As NetworkVariableData(Of TValue)
End Class

Public NotInheritable Class NetworkVariableData(Of TValue)
    Implements ISerializable

    Private ReadOnly commandXML As TValue
    Public Sub New(value As TValue)
        HasValue = True
        HasTimeStamp = True
        HasQuality = True
        TimeStamp = Date.Now.ToLocalTime
        Quality = "Good"
        IsQualityGood = True
        HasServerError = False

        commandXML = value
    End Sub

    '
    ' Сводка:
    '     Gets a value indicating whether the value is available.
    Public ReadOnly Property HasValue As Boolean
    '
    ' Сводка:
    '     Gets a value indicating whether the NationalInstruments.NetworkVariable.NetworkVariableData`1.TimeStamp
    '     is available.
    Public ReadOnly Property HasTimeStamp As Boolean
    '
    ' Сводка:
    '     Gets the timestamp of the network variable data.
    '
    ' Исключения:
    '   T:System.InvalidOperationException:
    '     NationalInstruments.NetworkVariable.NetworkVariableData`1.HasTimeStamp is false.
    '
    ' Примечания.
    '     The System.DateTime returned represents time in Coordinated Universal Time (UTC).
    '     To convert to local time, call System.DateTime.ToLocalTime.
    Public ReadOnly Property TimeStamp As Date
    '
    ' Сводка:
    '     Gets a value indicating whether NationalInstruments.NetworkVariable.NetworkVariableData`1.Quality
    '     is available.
    Public ReadOnly Property HasQuality As Boolean
    '
    ' Сводка:
    '     Gets the quality value of the data.
    '
    ' Исключения:
    '   T:System.InvalidOperationException:
    '     NationalInstruments.NetworkVariable.NetworkVariableData`1.HasQuality is false.
    Public ReadOnly Property Quality As String
    '
    ' Сводка:
    '     Gets a value indicating whether the NationalInstruments.NetworkVariable.NetworkVariableData`1.Quality
    '     value is good.
    '
    ' Исключения:
    '   T:System.InvalidOperationException:
    '     NationalInstruments.NetworkVariable.NetworkVariableData`1.HasQuality is false.
    '
    ' Примечания.
    '     The main purpose of this property is to differentiate between NationalInstruments.NetworkVariable.NetworkVariableData`1.Quality
    '     warnings and NationalInstruments.NetworkVariable.NetworkVariableData`1.Quality
    '     errors. If NationalInstruments.NetworkVariable.NetworkVariableData`1.IsQualityGood
    '     returns true, consider the NationalInstruments.NetworkVariable.NetworkVariableData`1.Quality
    '     value a warning, instead of an error.
    Public ReadOnly Property IsQualityGood As Boolean
    '
    ' Сводка:
    '     Gets a value indicating whether NationalInstruments.NetworkVariable.NetworkVariableData`1.ServerError
    '     is available.
    Public ReadOnly Property HasServerError As Boolean
    '
    ' Сводка:
    '     Gets any server or device error associated with the network variable data.
    '
    ' Исключения:
    '   T:System.InvalidOperationException:
    '     NationalInstruments.NetworkVariable.NetworkVariableData`1.HasServerError is false.
    '
    ' Примечания.
    '     Consult your server or device documentation for descriptions of the error codes
    '     returned by this method.
    Public ReadOnly Property ServerError As Long

    Public Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        Throw New NotImplementedException()
    End Sub

    '
    ' Сводка:
    '     Returns the raw value of the network variable.
    '
    ' Возврат:
    '     The raw value of the network variable.
    '
    ' Исключения:
    '   T:System.InvalidOperationException:
    '     NationalInstruments.NetworkVariable.NetworkVariableData`1.HasValue is false.
    '
    ' Примечания.
    '     Due to a limitation in the NationalInstruments.NetworkVariable class library,
    '     string values must be converted from a .NET Unicode string to an ANSI string.
    '     This conversion is done by using the system default code page to map each Unicode
    '     character to an ANSI character. If TValue is System.Object, System.String, or
    '     an array of either type and the value contains unmappable characters, the return
    '     value of this method replaces the unmappable characters with substitution characters.
    '     For more information, see Refer to Unicodemstudio.
    Public Function GetValue() As TValue
        Return commandXML
    End Function
End Class

'
' Сводка:
'     Provides data for the NationalInstruments.NetworkVariable.NetworkVariableWriter`1.WriteCompleted
'     event.
Public Class WriteCompletedEventArgs
    Inherits AsyncCompletedEventArgs

    '
    ' Сводка:
    '     Initializes a new instance of the NationalInstruments.NetworkVariable.WriteCompletedEventArgs
    '     class with the specified error, whether the asynchronous operation is canceled,
    '     and the optional user-supplied state object.
    '
    ' Параметры:
    '   error:
    '     An error, if an error occurs, during the asynchronous operation.
    '
    '   canceled:
    '     A value indicating whether the asynchronous operation is canceled.
    '
    '   userState:
    '     The optional user-supplied state object.
    Public Sub New([error] As Exception, canceled As Boolean, userState As Object)
        MyBase.New([error], canceled, userState)
    End Sub
End Class

'Public Async Function WriteStopAsync() As Task
'    Try
'        Await myPipeClientServer.SendStopAsync()
'    Catch ex As Exception
'        Console.WriteLine("ERROR: {0}", ex.ToString)
'        'Await Task.Run(Function() Console.WriteLine("ERROR: {0}", ex.ToString))
'    End Try
'End Function
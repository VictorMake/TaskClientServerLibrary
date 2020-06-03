Imports System.IO
Imports System.IO.Pipes
Imports System.Threading.Tasks
Imports TaskClientServerLibrary.Clobal

''' <summary>
''' Пара каналов асинхронного чтения и записи команд, 
''' при командном обмене между компьютерами для выполнении на них задач
''' </summary>
Public Class PipeClientServer
    ''' <summary>
    ''' Имя слушающего канала. 
    ''' </summary>
    Public ReadOnly Property NamePiepListen As String
    ''' <summary>
    ''' Имя пишущего канала. 
    ''' </summary>
    Public ReadOnly Property NamePiepSend As String

    Public serverName As String
    Private myPipeServer As NamedPipeServerStream

    Public Sub New(inNamePiepListen As String, inNamePiepSend As String, ByVal inServerName As String)
        NamePiepListen = inNamePiepListen
        NamePiepSend = inNamePiepSend
        serverName = inServerName
    End Sub

    ''' <summary>
    ''' Сервер ждет NamedPipeClientStream объект в дочерний процесс для подключения к нему.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function Listen() As Task(Of String)
        Try
            myPipeServer = New NamedPipeServerStream(NamePiepListen, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous)
            'pipeServer.WaitForConnection()
            Await Task.Factory.FromAsync(AddressOf myPipeServer.BeginWaitForConnection, AddressOf myPipeServer.EndWaitForConnection, Nothing)
            Using reader As StreamReader = New StreamReader(myPipeServer)
                Dim text As String = Await reader.ReadToEndAsync()
                Return text
            End Using
        Catch ex As IOException
            Debug.WriteLine("ERROR: {0}", ex.ToString)
            'Return ($"ERROR: {ex.ToString}")
            ' "Все копии канала заняты."
            If ex.Message.Contains("Все копии канала заняты") Then
                Return COMMAND_STOP
            Else
                Return COMMAND_NOTHING
            End If
            'Finally
            '    myPipeServer.Close()
            '    myPipeServer = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Отправить сообщение по именованному каналу.
    ''' </summary>
    ''' <param name="SendStr"></param>
    ''' <param name="TimeOut"></param>
    ''' <returns></returns>
    Public Async Function SendAsync(ByVal SendStr As String, ByVal Optional TimeOut As Integer = 1000) As Task
        Using pipeStream As NamedPipeClientStream = New NamedPipeClientStream(serverName, NamePiepSend, PipeDirection.Out, PipeOptions.Asynchronous)
            Try
                pipeStream.Connect(TimeOut)
                Console.WriteLine($"[{serverName}] Pipe создал соединение")

                Using sw As StreamWriter = New StreamWriter(pipeStream)
                    Await sw.WriteAsync(SendStr)
                    Await pipeStream.FlushAsync()
                End Using
            Catch e As TimeoutException
                Debug.WriteLine($"ERROR: {e.Message}")
                Throw
            Catch e As IOException
                Debug.WriteLine($"ERROR: {e.Message}")
                Throw
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 3 - этап Close
    ''' </summary>
    Public Sub Close()
        If myPipeServer IsNot Nothing AndAlso myPipeServer.IsConnected Then myPipeServer.Disconnect()

        myPipeServer.Close()
        myPipeServer = Nothing
    End Sub
End Class

'''' <summary>
'''' Сервер ждет NamedPipeClientStream объект в дочерний процесс для подключения к нему.
'''' </summary>
'''' <param name="PipeName"></param>
'''' <returns></returns>
'Public Shared Async Function Listen(ByVal PipeName As String) As Task(Of String)
'    Using pipeServer As NamedPipeServerStream = New NamedPipeServerStream(PipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous)
'        'pipeServer.WaitForConnection()
'        Await Task.Factory.FromAsync(AddressOf pipeServer.BeginWaitForConnection, AddressOf pipeServer.EndWaitForConnection, Nothing)
'        Using reader As StreamReader = New StreamReader(pipeServer)
'            Dim text As String = Await reader.ReadToEndAsync()
'            Return text
'        End Using
'    End Using
'End Function

'''' <summary>
'''' Послать команду остановки для своего же слушающего сервера (NamePiepListen).
'''' </summary>
'''' <param name="TimeOut"></param>
'''' <returns></returns>
'Public Async Function SendStopAsync(ByVal Optional TimeOut As Integer = 100) As Task
'    Using pipeStream As NamedPipeClientStream = New NamedPipeClientStream(".", NamePiepListen, PipeDirection.Out, PipeOptions.Asynchronous)
'        Try
'            pipeStream.Connect(TimeOut)
'            Using sw As StreamWriter = New StreamWriter(pipeStream)
'                Await sw.WriteAsync(COMMAND_STOP)
'                'TODO: Await pipeStream.FlushAsync()
'            End Using

'        Catch e As TimeoutException
'            Console.WriteLine($"ERROR: {e.Message}")
'            Throw
'        Catch e As IOException
'            Console.WriteLine($"ERROR: {e.Message}")
'            Throw
'        End Try
'    End Using
'End Function

'Private Shared Async Function ReadМessageAsync(ByVal s As PipeStream) As Task(Of Byte())
'    Dim ms As MemoryStream = New MemoryStream()
'    Dim buffer As Byte() = New Byte(4095) {}

'    Do
'        ms.Write(buffer, 0, Await s.ReadAsync(buffer, 0, buffer.Length))
'    Loop While Not s.IsMessageComplete

'    Return ms.ToArray()
'End Function
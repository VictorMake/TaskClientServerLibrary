''' <summary>
''' Proxy для для команды на исполнения.
''' Ставится в очередь задач для отправки.
''' </summary>
Public Class NetCommandForTask
    ''' <summary>
    ''' Имя процедуры в форме
    ''' </summary>
    ''' <returns></returns>
    Public Property ProcedureName As String
    ''' <summary>
    ''' Параметры для процедуры
    ''' </summary>
    ''' <returns></returns>
    Public Property Parameters As String()
    ''' <summary>
    ''' Признак того, что эта команда послана как ответ от пришедшей команды,
    ''' для того чтобы передать Index
    ''' </summary>
    ''' <returns></returns>
    Public Property IsResponse As Boolean
    ''' <summary>
    ''' Индекс пришедшей команды, который вставляется вместо генератора,
    ''' для поиска и отметки что от посланной команды пришёл ответ
    ''' </summary>
    ''' <returns></returns>
    Public Property IndexResponse As String

    Public Sub New(inProcedureName As String, ByVal ParamArray inParameters() As String)
        ProcedureName = inProcedureName

        If Not IsNothing(inParameters) Then
            Parameters = inParameters
        End If
    End Sub
End Class
' Декларирование сигнатуры события
Public Delegate Sub CommandSendReceiveCollectionClear(sender As Object, e As AlarmClearCommandEventArgs)
Public Delegate Sub CommandSendReceiveCollectionChange(sender As Object, e As AlarmChangeCommandEventArgs)

Public Class AlarmClearCommandEventArgs
    Inherits EventArgs
End Class

Public Class AlarmChangeCommandEventArgs
    Inherits EventArgs

    Public ReadOnly Property Index As Integer
    Public ReadOnly Property Value As CommandForListViewItem

    Public Sub New(index As Integer, value As CommandForListViewItem)
        MyBase.New()

        Me.Index = index
        Me.Value = value
    End Sub
End Class

''' <summary>
''' Коллекция для полученных или отправленных команд 
''' для отображения на элементе ListView контрола UserControlCommand
''' </summary>
''' <remarks></remarks>
Public Class CommandSendReceiveCollection
    Implements IEnumerable

    ' События
    Public Event Cleared As CommandSendReceiveCollectionClear
    Public Event Clearing As CommandSendReceiveCollectionClear
    Public Event Deleted As CommandSendReceiveCollectionChange
    Public Event Added As CommandSendReceiveCollectionChange

    ' контейнер элементов
    Private Commands As List(Of CommandForListViewItem)

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        ' инициализация коллекции
        Commands = New List(Of CommandForListViewItem)
    End Sub

    ''' <summary>
    ''' Добавить команду в коллекцию
    ''' </summary>
    ''' <param name="сommand">Элемент для добавления</param>
    Public Sub Add(сommand As CommandForListViewItem)
        Commands.Add(сommand)
        OnAdded(Commands.Count - 1, сommand)
    End Sub

    ''' <summary>
    ''' Добавить диапазон команд
    ''' </summary>
    ''' <param name="сommands">Элементы для добавления</param>
    Public Sub AddRange(сommands As CommandForListViewItem())
        For Each itemCommand As CommandForListViewItem In сommands
            Add(itemCommand)
        Next
    End Sub

    ''' <summary>
    ''' Удалить команду
    ''' </summary>
    ''' <param name="сommand">Элемент для удаления</param>
    Public Sub Remove(сommand As CommandForListViewItem)
        Dim index As Integer = Commands.IndexOf(сommand)
        Commands.Remove(сommand)
        OnDeleted(index, сommand)
    End Sub

    ''' <summary>
    ''' Удалить команду по индексу
    ''' </summary>
    ''' <param name="index">Индекс элемента для удаления</param>
    Public Sub Remove(index As Integer)
        Dim command As CommandForListViewItem = DirectCast(Commands(index), CommandForListViewItem)
        Commands.RemoveAt(index)
        OnDeleted(index, command)
    End Sub

    ''' <summary>
    ''' Очистить коллекцию команд
    ''' </summary>
    Public Sub Clear()
        OnTaskSendCollectionClearing()
        Commands.Clear()
        OnTaskSendCollectionClear()
    End Sub

    ''' <summary>
    ''' Вернуть число элементов в коллекции
    ''' </summary>
    Public ReadOnly Property Count() As Integer
        Get
            Return Commands.Count
        End Get
    End Property

    ''' <summary>
    ''' Вставить команду по индексу
    ''' </summary>
    ''' <param name="index">Индекс вставляемого элемента</param>
    ''' <param name="command">Элемент для вставки</param>
    Public Sub Insert(index As Integer, command As CommandForListViewItem)
        Commands.Insert(index, command)
        OnAdded(index, command)
    End Sub

    ''' <summary>
    ''' Вернуть команду по индексу
    ''' </summary>
    Default Public ReadOnly Property Item(index As Integer) As CommandForListViewItem
        Get
            Return TryCast(Commands(index), CommandForListViewItem)
        End Get
    End Property

    ''' <summary>
    ''' Вернуть команду по тексту
    ''' </summary>
    Default Public ReadOnly Property Item(text As String) As CommandForListViewItem
        Get
            For Each itemCommand As CommandForListViewItem In Commands
                If itemCommand.IndexCommamd = text Then
                    Return itemCommand
                End If
            Next
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Вернуть индекс данной команду
    ''' </summary>
    ''' <param name="command">Элемент, чей элемент нужен</param>
    ''' <returns>Индекс элемента</returns>
    Public Function IndexOf(command As CommandForListViewItem) As Integer
        Return Commands.IndexOf(command)
    End Function

    ''' <summary>
    ''' Вернуть IEnumerator коллекции
    ''' </summary>
    ''' <returns>IEnumerator</returns>
    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return Commands.GetEnumerator()
    End Function

    ''' <summary>
    ''' Элемент был добавлен
    ''' </summary>
    ''' <param name="index">Индекс команды</param>
    ''' <param name="command">Команда</param>
    Private Sub OnAdded(index As Integer, command As CommandForListViewItem)
        RaiseEvent Added(Me, New AlarmChangeCommandEventArgs(index, command))
    End Sub

    ''' <summary>
    ''' Элемент был удалён
    ''' </summary>
    ''' <param name="index">Индекс команды</param>
    ''' <param name="command">Команда</param>
    Private Sub OnDeleted(index As Integer, command As CommandForListViewItem)
        RaiseEvent Deleted(Me, New AlarmChangeCommandEventArgs(index, command))
    End Sub

    ''' <summary>
    ''' Коллекция в процессе очищения
    ''' </summary>
    Private Sub OnTaskSendCollectionClearing()
        RaiseEvent Clearing(Me, New AlarmClearCommandEventArgs)
    End Sub

    ''' <summary>
    ''' Коллекция была очищена
    ''' </summary>
    Private Sub OnTaskSendCollectionClear()
        RaiseEvent Cleared(Me, New AlarmClearCommandEventArgs)
    End Sub
End Class

Public Class CommandForListViewItem
    Public Property IDCommamd As String
    Public Property Description As String
    Public Property CommanderID As String
    Public Property IndexCommamd As String
    Public Property Color As Drawing.Color = Drawing.Color.Magenta

    Public Sub New(inIDCommamd As String, inDescription As String, inCommanderID As String, inIndexCommamd As String)
        IDCommamd = inIDCommamd
        Description = inDescription
        CommanderID = inCommanderID
        IndexCommamd = inIndexCommamd
    End Sub
End Class

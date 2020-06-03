Public Class FutureClass
    Public ReadOnly Property Name As Object
    'Добавлен какой-то класс
    Structure MyStructure
        Public ValueOne As Integer
        Public ValueTwo As Boolean
    End Structure

    Public Sub New(name As String)
        Me.Name = name
    End Sub

    Public Function GetName(inNewName As String) As String
        Return Name
    End Function

    Private mValue As String
    Public Property GetValue() As String
        Get
            Return mValue
        End Get
        Set(ByVal value As String)
            mValue = value
        End Set
    End Property

End Class

Public Class FutureClass
    Public ReadOnly Property Name As Object

    'Добавлен какой-то класс
    Structure MyStructure
        Public ValueOne As Integer
        Public ValueTwo As Boolean

        Public Sub New(One As Integer, Two As Boolean)
            ValueOne = One
            ValueTwo = Two
        End Sub
    End Structure

    Private structureProject As MyStructure

    Public Sub New(name As String)
        Me.Name = name
        structureProject = New MyStructure(1, True)
    End Sub

    Public Function GetName(inNewName As String) As String
        Return Name
    End Function

    'Private mValue As String
    'Public Property GetValue() As String
    '    Get
    '        Return mValue
    '    End Get
    '    Set(ByVal value As String)
    '        mValue = value
    '    End Set
    'End Property

End Class

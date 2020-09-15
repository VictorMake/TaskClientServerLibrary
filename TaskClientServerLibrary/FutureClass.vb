Public Class FutureClass
    Public ReadOnly Property Name As Object

    'Добавлен какой-то класс
    ' два изменения 2
    Structure MyStructure
        Public ValueOne As Integer
        Public ValueTwo As Boolean

        Public Sub New(One As Integer, Two As Boolean)
            ValueOne = One
            ValueTwo = Two
        End Sub
    End Structure

    Private structureProject As MyStructure

    Public Enum TypeProject
        Added = 1
        Removed = 2
    End Enum


    Public Sub New(name As String, Optional project As TypeProject = TypeProject.Added)
        Me.Name = name
        structureProject = New MyStructure(1, True)
        mProject = project
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


    Private mProject As TypeProject
    Public ReadOnly Property Project() As TypeProject
        Get
            Return mProject
        End Get
    End Property
End Class

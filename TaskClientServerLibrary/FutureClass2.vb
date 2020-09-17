﻿Public Class FutureClass2
    Public ReadOnly Property Name As Object

    ' Добавлен какой-то класс
    ' Добавил примечание
    ' Делаю новый коммит
    ' два изменения 1
    ' Feature New Test
    ' Commit for new feature
    ' Commit for new feature 2
    ' merge to develop
    ' создана фича для релиза 2
    ' создана фича2 для релиза 2
    ' перед удалением ветки
    ' cherry-pick
    ' Change in next for Rebase 1
    ' Change in next for Rebase 2
    Structure ConflictFirst
        Public ValueOne As Integer
        Public ValueTwo As Boolean

        Public Sub New(One As Integer, Two As Boolean)
            ValueOne = One
            ValueTwo = Two
        End Sub
    End Structure

    Structure ConflictSecond
        Public ValueOne As Integer
        Public ValueTwo As Boolean

        Public Sub New(One As Integer, Two As Boolean)
            ValueOne = One
            ValueTwo = Two
        End Sub
    End Structure


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

Imports System.IO
Imports System.Windows.Forms
Imports TaskClientServerLibrary.Clobal

'<?xml version="1.0" encoding="UTF-8"?>
'<Tasks>
'  <Task Name="Поставить метку" Description="Поставить метку КТ" ProcedureName="НаПослеВыполненияЗапроса" WhatModule="FormMain" Index="123">
'      <Parameter Key="1" Value="0" Type="String" />
'      <Parameter Key="2" Value="10" Type="String" />
'      <Parameter Key="3" Value="20" Type="String" />
'  </Task>
'<Task Name="Сообщение" Description="Послать сообщение на другой компьютер" ProcedureName="Сообщение" WhatModule="FormMain" Index="123">
'    <Parameter Key="1" Value="0" Type="String" Description="Текст посылаемого сообщеня" />
'</Task>
'</Tasks>

''' <summary>
''' Менеджер управления разрешенными задачами сетевого межпроцессного командного обмена
''' </summary>
Public Class ManagerTaskApplication
    Implements IEnumerable

    ''' <summary>
    ''' Коллекция описанных задач и соответствующих реальных процедур
    ''' </summary>
    Private mCollectionsTask As Dictionary(Of String, TaskApplication)
    ''' <summary>
    ''' Полный путь к файлу XML
    ''' </summary>
    Private ReadOnly XmlPathFileTasksClientServer As String
    ''' <summary>
    ''' Имя файла конфигурации задач
    ''' </summary>
    Private Const XmlFileTasks As String = "TasksClientServer.xml"
    ''' <summary>
    ''' Загрузка, навигация и сериализация задач
    ''' </summary>
    Private XDoc As XDocument

    Public Sub New(ByVal pathResource As String)
        mCollectionsTask = New Dictionary(Of String, TaskApplication)
        XmlPathFileTasksClientServer = Path.Combine(pathResource, XmlFileTasks)

        If FileNotExists(XmlPathFileTasksClientServer) Then
            MessageBox.Show($"В каталоге нет файла <{XmlFileTasks}>!", "Запуск библиотеки TaskClientServerLibrary", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            LoadXmlSettingTasksClientServer()
            PopulateCollectionsTask()
        End If
    End Sub

    ''' <summary>
    ''' True - файла нет
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Private Function FileNotExists(fileName As String) As Boolean
        Return Not File.Exists(fileName)
    End Function

    Public ReadOnly Property Tasks() As Dictionary(Of String, TaskApplication)
        Get
            Return mCollectionsTask
        End Get
    End Property

    Default Public Property Item(ByVal indexKey As String) As TaskApplication
        Get
            Return mCollectionsTask.Item(indexKey)
        End Get
        Set(ByVal value As TaskApplication)
            mCollectionsTask.Item(indexKey) = value
        End Set
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Return mCollectionsTask.Count()
        End Get
    End Property

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return mCollectionsTask.GetEnumerator
    End Function

    Public Sub Remove(indexKey As String)
        ' удаление по номеру или имени или объекту?
        ' если целый тип то по плавающему индексу, а если строковый то по ключу
        mCollectionsTask.Remove(indexKey)
    End Sub

    Public Sub Clear()
        mCollectionsTask.Clear()
    End Sub

    Protected Overrides Sub Finalize()
        mCollectionsTask = Nothing
        MyBase.Finalize()
    End Sub

    Public Function Add(ByVal name As String, ByVal description As String, ByVal procedureName As String, ByVal whatModule As String) As TaskApplication
        If Not ContainsKey(name) Then Return Nothing

        Dim tempTask As TaskApplication = New TaskApplication(name, description, procedureName, whatModule)
        mCollectionsTask.Add(name, tempTask)

        Return tempTask
    End Function

    ''' <summary>
    ''' Проверка Наличия
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Private Function ContainsKey(ByVal name As String) As Boolean
        If mCollectionsTask.ContainsKey(name) Then
            MessageBox.Show($"Задача {name} в коллекции уже существует!",
                            "Ошибка добавления новой задачи", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Считать Из Файла Настройки Задач
    ''' </summary>
    Private Sub LoadXmlSettingTasksClientServer()
        Try
            XDoc = XDocument.Load(XmlPathFileTasksClientServer)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Считывание из " & XmlPathFileTasksClientServer, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Заполнить CollectionsTask
    ''' </summary>
    Private Sub PopulateCollectionsTask()
        If XDoc.Root.HasElements Then
            For Each itemTaskXElement As XElement In XDoc.Root.<Task>
                ' добавить задачу в коллекцию
                Dim tempTask As TaskApplication = Me.Add(itemTaskXElement.Attribute(COMMAND_NAME).Value,
                                                         itemTaskXElement.Attribute(COMMAND_DESCRIPTION).Value,
                                                         itemTaskXElement.Attribute(ATTR_PROCEDURE_NAME).Value,
                                                         itemTaskXElement.Attribute(ATTR_WHAT_MODULE).Value)
                If tempTask IsNot Nothing Then
                    For Each itemParameterXElement As XElement In itemTaskXElement.<Parameter>
                        ' добавить параметр для вызываемой процедуры
                        tempTask.Add(CInt(itemParameterXElement.Attribute(COMMAND_KEY).Value),
                                     itemParameterXElement.Attribute(COMMAND_VALUE).Value,
                                     ConvertStringToEnumTypeParam(itemParameterXElement.Attribute(COMMAND_TYPE).Value),
                                     itemParameterXElement.Attribute(COMMAND_DESCRIPTION).Value)
                    Next
                End If
            Next
        End If
    End Sub

    Private Function ConvertStringToEnumTypeParam(ByVal strEnumTypeParam As String) As TypeParam
        Select Case strEnumTypeParam
            Case "String"
                Return TypeParam.String
            Case "Boolean"
                Return TypeParam.Boolean
            Case "DateTime"
                Return TypeParam.DateTime
            Case "Double"
                Return TypeParam.Double
            Case "Int32"
                Return TypeParam.Int32
            Case "Object"
                Return TypeParam.Object
        End Select

        'Dim valuesFormTypeParam As Array = [Enum].GetValues(GetType(TypeParam))
        'Dim tempTypeParam As TypeParam = TypeParam.String ' по умолчанию

        'For I As Integer = 0 To valuesFormTypeParam.Length - 1
        '    If valuesFormTypeParam.GetValue(I).ToString = strEnumTypeParam Then
        '        tempTypeParam = valuesFormTypeParam.GetValue(I)
        '        Exit For
        '    End If
        'Next

        'Return tempTypeParam
    End Function

    'Private Function ConverStringToTypeParam(inNameType) As TypeParam
    '    Dim fi As Reflection.FieldInfo = EnumType.GetField([Enum].GetName(EnumType, value))
    '    Dim dna As DescriptionAttribute = DirectCast(Attribute.GetCustomAttribute(fi, GetType(DescriptionAttribute)), DescriptionAttribute)

    '    If dna IsNot Nothing Then
    '        Return dna.Description
    '    Else
    '        Return value.ToString()
    '    End If
    'End Function

    '''' <summary>
    '''' Получить описание (Description) для перечисления
    '''' </summary>
    '''' <param name="EnumType"></param>
    '''' <param name="value"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function ConvertTo(ByVal EnumType As Type, ByVal value As Object) As String
    '    Dim fi As Reflection.FieldInfo = EnumType.GetField([Enum].GetName(EnumType, value))
    '    Dim dna As DescriptionAttribute = DirectCast(Attribute.GetCustomAttribute(fi, GetType(DescriptionAttribute)), DescriptionAttribute)

    '    If dna IsNot Nothing Then
    '        Return dna.Description
    '    Else
    '        Return value.ToString()
    '    End If
    'End Function

    ''' <summary>
    ''' Класс исполнения задачи из запроса (команды), запрошенного от Client.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TaskApplication
        Implements IEnumerable
        Implements ICloneable

        ''' <summary>
        ''' Имя задачи
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Name() As String
        ''' <summary>
        ''' Описание задачи
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Description() As String
        ''' <summary>
        ''' Вызываемая процедура
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property ProcedureName() As String
        ''' <summary>
        ''' Имя модуля или формы где находится вызываемая процедура
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property WhatModule() As String
        ''' <summary>
        ''' Список параметров вызываемой процедуры
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Parameters() As Dictionary(Of Integer, Parameter)
            Get
                Return mParameters
            End Get
        End Property

        Default Public Property Item(ByVal IndexKey As Integer) As Parameter
            Get
                Return mParameters.Item(IndexKey)
            End Get
            Set(ByVal value As Parameter)
                mParameters.Item(IndexKey) = value
            End Set
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return mParameters.Count()
            End Get
        End Property

        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return mParameters.GetEnumerator
        End Function

        Public Sub Remove(indexKey As Integer)
            ' удаление по номеру или имени или объекту?
            ' если целый тип то по плавающему индексу, а если строковый то по ключу
            mParameters.Remove(indexKey)
        End Sub

        Public Sub Clear()
            mParameters.Clear()
        End Sub

        Protected Overrides Sub Finalize()
            mParameters = Nothing
            MyBase.Finalize()
        End Sub

        Public Sub Add(ByVal number As Integer, ByVal value As String, ByVal type As TypeParam, ByVal description As String)
            If IsContainsKey(number) Then
                mParameters.Add(number, New Parameter(number, value, type, description))
            End If
        End Sub

        Private mParameters As Dictionary(Of Integer, Parameter)

        ''' <summary>
        ''' Создание экземпляра класса исполнения задачи из запроса (команды), запрошенного от Client.
        ''' </summary>
        ''' <param name="name">Имя задачи</param>
        ''' <param name="description">Описание задачи</param>
        ''' <param name="procedureName">Вызываемая процедура</param>
        ''' <param name="whatModule">Имя модуля или формы где находится вызываемая процедура</param>
        Public Sub New(ByVal name As String, ByVal description As String, ByVal procedureName As String, ByVal whatModule As String)
            mParameters = New Dictionary(Of Integer, Parameter)
            Me.Name = name
            Me.Description = description
            Me.ProcedureName = procedureName
            Me.WhatModule = whatModule
        End Sub

        ''' <summary>
        ''' Проверка Наличия
        ''' </summary>
        ''' <param name="number"></param>
        ''' <returns></returns>
        Private Function IsContainsKey(ByVal number As Integer) As Boolean
            If mParameters.ContainsKey(number) Then
                MessageBox.Show($"Parameter с индексом {number.ToString} уже существует!",
                                "Ошибка добавления параметра", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            If number < 1 Then
                MessageBox.Show("Номер параметра должен быть в больше 1!", "Ошибка добавления параметра", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            Return True
        End Function

        Public Overrides Function ToString() As String
            Try
                Return $"Имя:{Me.Name} Описание:{Me.Description} Процедура:{Me.ProcedureName} Класс:{Me.WhatModule}"
            Catch
                Throw New Exception("Не возможно выдать задачу в строковом формате.")
            End Try
        End Function

        ''' <summary>
        ''' Глубокое клонирование
        ''' </summary>
        ''' <returns></returns>
        Public Overridable Function Clone() As Object Implements ICloneable.Clone
            Dim cloneTask As New TaskApplication(Name, Description, ProcedureName, WhatModule)

            For Each itemParameter As Parameter In Me.Parameters.Values
                cloneTask.Add(itemParameter.Number, itemParameter.Value, itemParameter.Type, itemParameter.Description)
            Next

            Return cloneTask
        End Function

        ''' <summary>
        ''' Параметра для исполняемой процедуры в типе её содержащей
        ''' </summary>
        Public Class Parameter
            ''' <summary>
            ''' Порядковый номер параметра
            ''' </summary>
            ''' <returns></returns>
            Public ReadOnly Property Number() As Integer
            ''' <summary>
            ''' Значение параметра
            ''' </summary>
            ''' <returns></returns>
            Public Property Value() As String
            ''' <summary>
            ''' Системный тип параметра
            ''' </summary>
            ''' <returns></returns>
            Public ReadOnly Property Type() As TypeParam
            ''' <summary>
            ''' Описание назначения параметра
            ''' </summary>
            ''' <returns></returns>
            Public ReadOnly Property Description() As String

            Public Sub New(ByVal number As Integer, ByVal value As String, ByVal type As TypeParam, ByVal description As String)
                Me.Number = number
                Me.Value = value
                Me.Type = type
                Me.Description = description
            End Sub
        End Class
    End Class
End Class

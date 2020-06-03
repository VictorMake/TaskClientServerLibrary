Imports System.IO
Imports System.Windows.Forms
Imports System.Xml
Imports TaskClientServerLibrary.Clobal
Imports TaskClientServerLibrary.ManagerTaskApplication

''' <summary>
''' Работа по расшифровке команды полученной от Target
''' </summary>
''' <remarks></remarks>
Public Class XMLConfigCommand
#Region " Declarations "
    Private mDataSet As New DataSet()
    Private xmlDataDoc As XmlDataDocument
    Private ReadOnly whoIsUpdated As WhoIsUpdate
    Private Const DATA_SET_COMMAND As String = "DataSetCommand"
    Private Const KeyIsNoExist As String = "Запрошенный ключ не найден."
    Private Const RootIsNotExist As String = "Корневой узел для запрошенного ключа отсутствует."
#End Region

#Region " Public properties, procedures and enums"
    Public Property DataGridCommand As New DataGridView
    Public Property ManagerTasks As ManagerTaskApplication

    Public Sub New(Optional ByVal optWhoIsUpdated As WhoIsUpdate = WhoIsUpdate.DataView)
        whoIsUpdated = optWhoIsUpdated
        InitializeDataset()
        DataGridCommand.DataSource = GetDataTable()
    End Sub

    Public Overrides Function ToString() As String
        Dim commandXML As String = String.Empty

        Try
            commandXML = mDataSet.GetXml()
        Catch ex As Exception
            Dim CAPTION As String = $"Процедура <{NameOf(ToString)}>  невозможно выдать таблицу в XML формате."
            Dim text As String = ex.Message
            MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'RegistrationEventLog.EventLog_MSG_EXCEPTION($"<{CAPTION}> {text}")
            'Throw New Exception("Невозможно выдать таблицу в XML формате.")
        End Try

        Return commandXML
    End Function

    ''' <summary>
    ''' Загрузить XmlTextReader через строку в формате XML.
    ''' Далее создаётся DataSet -> заполняется через XmlTextReader -> заполняется DataGridView
    ''' </summary>
    ''' <param name="xmlData"></param>
    Public Sub LoadXMLfromString(ByVal xmlData As String)
        '<DataSetCommand>
        '  <Settings>
        '    <Key>NAME</Key>
        '    <Value>Сообщение</Value>
        '    <Type>String</Type>
        '  </Settings>
        '  <Settings>
        '    <Key>DESCRIPTION</Key>
        '    <Value>Послать сообщение на другой компьютер</Value>
        '    <Type>String</Type>
        '  </Settings>
        '  <Settings>
        '    <Key>1 ПАРАМЕТР</Key>
        '    <Value>Привет</Value>
        '    <Type>String</Type>
        '  </Settings>
        '  <Settings>
        '    <Key>INDEX</Key>
        '    <Value>811557888</Value>
        '    <Type>String</Type>
        '  </Settings>
        '</DataSetCommand>

        xmlDataDoc = Nothing
        mDataSet = New DataSet()
        InitializeDataset()
        ' то же работает
        'Dim xmlReaderMemory As XmlReader = XmlReader.Create(New StringReader(xmlData))
        'mDataSet.ReadXml(xmlReaderMemory, XmlReadMode.InferSchema)
        ' то же работает
        'Dim reader As System.IO.StringReader = New System.IO.StringReader(xmlData)

        Using reader As New XmlTextReader(New StringReader(xmlData))
            Try
                mDataSet.ReadXml(reader) ', XmlReadMode.InferSchema)
                DataGridCommand.DataSource = GetDataTable()
            Catch ex As Exception
                Dim CAPTION As String = $"Невозможно загрузить данные из строки  в <{NameOf(LoadXMLfromString)}>."
                Dim text As String = ex.Message
                MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                'RegistrationEventLog.EventLog_MSG_EXCEPTION($"<{CAPTION}> {text}")
            End Try
        End Using
    End Sub

    ''' <summary>
    ''' Добавить запись в таблицу или документ
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="inValue"></param>
    ''' <param name="typeValue"></param>
    Public Sub AddRow(ByVal key As String, ByVal inValue As String, Optional ByVal typeValue As TypeParam = TypeParam.String)
        Dim EditValue As String() = {inValue, typeValue.ToString}

        If whoIsUpdated = WhoIsUpdate.DataView Then
            RowValue(key) = EditValue
        Else
            ValuePathQuery(key) = EditValue
        End If
    End Sub

    ''' <summary>
    ''' Значение записи в таблице или документе
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns></returns>
    Public Function GetRowValue(ByVal key As String) As String()
        Try
            If whoIsUpdated = WhoIsUpdate.DataView Then
                Return RowValue(key)
            Else
                Return ValuePathQuery(key)
            End If
            'MessageBox.Show("Значение = " & Value(0) & "; Тип = " & Value(1), Key, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, Nothing)
        Catch ex As Exception
            Dim caption As String = $"Функция <{NameOf(GetRowValue)}> вызвала ошибку"
            Dim text As String = ex.Message
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'RegistrationEventLog.EventLog_MSG_EXCEPTION($"<{CAPTION}> {text}")
            Return {0, TypeParam.String.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Значение записи в таблице DataSet
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns></returns>
    Private Property RowValue(ByVal key As String) As String()
        Get
            Dim dv As New DataView(mDataSet.Tables(TableCommand)) With {
                .Sort = COMMAND_KEY
            }
            Dim index As Integer = dv.Find(key.ToUpper.Trim) '  Находит строку в DataView по указанному значению ключа сортировки.

            If index > -1 Then
                'Return dv.Item(index)(conValue)
                Return {dv.Item(index)(COMMAND_VALUE), dv.Item(index)(COMMAND_TYPE)}
            Else
                Throw New Exception(KeyIsNoExist)
            End If
        End Get

        Set(ByVal Value As String())
            ' проверить существует ли запись, прежде чем что-либо делать
            Dim dv As New DataView(mDataSet.Tables(TableCommand)) With {
                .Sort = COMMAND_KEY
            }
            'dv.RowFilter = "Key='" & Key.ToUpper.Trim & "'"
            Dim index As Integer = dv.Find(key.ToUpper.Trim)

            If index = -1 Then
                ' запись не найдена, значить добавить строку
                Dim CellValues As String() = {key.ToUpper.Trim, Value(0), Value(1)}
                mDataSet.Tables(TableCommand).Rows.Add(CellValues)
            Else
                ' запись найдена, обновить новыми значениями
                dv(index)(COMMAND_KEY) = key.ToUpper.Trim
                dv(index)(COMMAND_VALUE) = Value(0)
                dv(index)(COMMAND_TYPE) = Value(1)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Значение записи в документе
    ''' </summary>
    ''' <param name="inKey"></param>
    ''' <returns></returns>
    Private Property ValuePathQuery(ByVal inKey As String) As String()
        Get
            If xmlDataDoc Is Nothing Then xmlDataDoc = New XmlDataDocument(mDataSet)

            Dim nodRecord As XmlNode = xmlDataDoc.SelectSingleNode($"/{DATA_SET_COMMAND}/{Clobal.TableCommand}[Key={inKey.ToUpper.Trim}]")

            If nodRecord IsNot Nothing Then
                Return {nodRecord.ChildNodes(1).InnerText, nodRecord.ChildNodes(2).InnerText}
            Else
                Throw New Exception(KeyIsNoExist)
            End If
        End Get

        Set(ByVal Value As String())
            ' проверить существует ли запись, прежде чем что-либо делать
            If xmlDataDoc Is Nothing Then xmlDataDoc = New XmlDataDocument(mDataSet)

            mDataSet.EnforceConstraints = False
            Dim nodRecord As XmlNode = xmlDataDoc.SelectSingleNode($"/{DATA_SET_COMMAND}/{Clobal.TableCommand}[Key={inKey.ToUpper.Trim}]")

            If nodRecord Is Nothing Then
                ' запись не найдена, значить добавить строку
                Dim strXpathQueryRoot As String = "/" & DATA_SET_COMMAND
                Dim nodRoot As XmlNode = xmlDataDoc.SelectSingleNode(strXpathQueryRoot)

                If nodRoot IsNot Nothing Then
                    '<appSettings>
                    '  <Key>1</Key>
                    '  <Value>1</Value>
                    '  <Type>String</Type>
                    '</appSettings>     
                    Dim nodChild As XmlNode = xmlDataDoc.CreateNode(XmlNodeType.Element, Clobal.TableCommand, String.Empty)
                    CreateKey(xmlDataDoc, nodChild, COMMAND_KEY, inKey.ToUpper.Trim)
                    CreateKey(xmlDataDoc, nodChild, COMMAND_VALUE, Value(0))
                    CreateKey(xmlDataDoc, nodChild, COMMAND_TYPE, Value(1))
                    nodRoot.AppendChild(nodChild)
                Else
                    Throw New Exception(RootIsNotExist)
                End If
            Else
                ' запись найдена, обновить новыми значениями
                nodRecord.ChildNodes(0).InnerText = inKey.ToUpper.Trim
                nodRecord.ChildNodes(1).InnerText = Value(0)
                nodRecord.ChildNodes(2).InnerText = Value(1)
            End If
            mDataSet.EnforceConstraints = True
        End Set
    End Property

    Private Sub CreateKey(ByRef xmlDoc As XmlDataDocument, ByRef xmlNodeSection As XmlNode, ByVal name As String, ByVal sValue As String)
        Dim newNodeKey As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, name, String.Empty)
        newNodeKey.InnerText = sValue
        xmlNodeSection.AppendChild(newNodeKey)
    End Sub

    'Public Function KeyCount() As Integer
    '    Return mDataSet.Tables(TableCommand).Rows.Count
    'End Function

    ''' <summary>
    ''' Удалить запись
    ''' </summary>
    ''' <param name="Key"></param>
    Public Sub RemoveKey(ByVal Key As String)
        Try
            If whoIsUpdated = WhoIsUpdate.DataView Then
                Remove(Key)
            Else
                RemovePathQuery(Key)
            End If
        Catch ex As Exception
            Dim text As String = ex.Message
            MessageBox.Show(text, $"<{NameOf(RemoveKey)}> Error:", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'RegistrationEventLog.EventLog_MSG_EXCEPTION($"<{CAPTION}> {text}")
        End Try
    End Sub

    ''' <summary>
    ''' Удалить запись в таблице
    ''' </summary>
    ''' <param name="Key"></param>
    Private Sub Remove(ByVal Key As String)
        Dim dv As New DataView(mDataSet.Tables(TableCommand)) With {
            .Sort = COMMAND_KEY
        }
        Dim index As Integer = dv.Find(Key.ToUpper.Trim)

        If index > -1 Then
            dv.Item(index).Delete()
        Else
            Throw New Exception(KeyIsNoExist)
        End If
    End Sub

    ''' <summary>
    ''' Удалить запись в документе
    ''' </summary>
    ''' <param name="Key"></param>
    Private Sub RemovePathQuery(ByVal Key As String)
        If xmlDataDoc Is Nothing Then
            xmlDataDoc = New XmlDataDocument(mDataSet)
        End If

        mDataSet.EnforceConstraints = False

        Dim strXpathQuery As String = $"/{DATA_SET_COMMAND}/{Clobal.TableCommand}[Key={Key.ToUpper.Trim}]"
        Dim nodRecord As XmlNode = xmlDataDoc.SelectSingleNode(strXpathQuery)

        If nodRecord IsNot Nothing Then
            Dim strXpathQueryRoot As String = "/" & DATA_SET_COMMAND
            Dim nodRoot As XmlNode = xmlDataDoc.SelectSingleNode(strXpathQueryRoot)

            If nodRoot IsNot Nothing Then
                nodRoot.RemoveChild(nodRecord)
            Else
                Throw New Exception(RootIsNotExist)
            End If
        Else
            Throw New Exception(KeyIsNoExist)
        End If
        mDataSet.EnforceConstraints = True
    End Sub

    Public Sub Clear()
        If whoIsUpdated = WhoIsUpdate.XmlDataDocument Then
            ' не работает т.к. надо заново определять DataSet
            'For I As Integer = _ds.Tables(TableCommand).Rows.Count - 1 To 0 Step -1
            '    mDataSetTables(TableCommand).Rows.RemoveAt(I)
            'Next
            'mDataSetEnforceConstraints = False
            'xmlDataDoc.RemoveAll()
            'mDataSetEnforceConstraints = True

            xmlDataDoc = Nothing
            mDataSet = New DataSet()
            InitializeDataset()
            DataGridCommand.DataSource = GetDataTable()
        Else
            mDataSet.Tables(TableCommand).Rows.Clear()
        End If
    End Sub

    Public Function GetDataTable() As DataTable
        Return mDataSet.Tables(TableCommand)
    End Function

    ''' <summary>
    ''' Создать экземпляр задачи, клонировать его из менеджера.
    ''' Заполнить значения параметров полученными по сети
    ''' и вернуть новый экземпляр.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTask() As TaskApplication
        Dim mTask As TaskApplication = ManagerTasks.Tasks(GetRowValue(COMMAND_NAME)(0)).Clone()

        For Each itemRow As DataRow In mDataSet.Tables(TableCommand).Rows
            If itemRow(COMMAND_KEY).ToString.IndexOf(COMMAND_PARAMETER.ToUpper) <> -1 Then
                mTask(Val(itemRow(COMMAND_KEY).ToString)).Value = itemRow(COMMAND_VALUE).ToString
            End If
        Next

        Return mTask
    End Function
#End Region

#Region " Private procedures "
    Private Sub InitializeDataset()
        mDataSet.DataSetName = DATA_SET_COMMAND
        mDataSet.Tables.Add(TableCommand)
        mDataSet.Tables(TableCommand).Columns.Add(COMMAND_KEY, GetType(String))
        mDataSet.Tables(TableCommand).Columns.Add(COMMAND_VALUE, GetType(String))
        mDataSet.Tables(TableCommand).Columns.Add(COMMAND_TYPE, GetType(String))
    End Sub
#End Region
End Class

'пример доступа к записям в таблице посредством запроса из справки -> Performing an XPath Query on a DataSet (ADO.NET)
'    Dim xmlDoc As XmlDataDocument = New XmlDataDocument(DataSet)
'    Dim nodeList As XmlNodeList = xmlDoc.DocumentElement.SelectNodes( "descendant::Customers[*/OrderDetails/ProductID=43]")
'    Dim dataRow As DataRow
'    Dim xmlNode As XmlNode

'For Each xmlNode In nodeList
'  dataRow = xmlDoc.GetRowFromElement(CType(xmlNode, XmlElement))

'  If Not dataRow Is Nothing then Console.WriteLine(xmlRow(0).ToString())
'Next

'Private Const XML_Suffix As String = ".cfg"
'Private _XMLPath As String
'Private _AppPath As String
'Private _ExeName As String

'Public Sub SaveXML()
'    Try
'        '_ds.WriteXml(_XMLPath, XmlWriteMode.WriteSchema)
'        _ds.WriteXml(_XMLPath)
'    Catch
'        Throw New Exception("Невозможно записать в " & _XMLPath & ".")
'    End Try
'End Sub

'Public Property XMLPath() As String
'    Get
'        Return _XMLPath
'    End Get
'    Set(ByVal Value As String)
'        _XMLPath = Value
'    End Set
'End Property

'Public Sub ResetXMLPath()
'    _XMLPath = _AppPath & _ExeName & XML_Suffix
'End Sub
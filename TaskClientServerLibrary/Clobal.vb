Public Class Clobal
    Public Const NamePipe As String = "Pipe"
    Public Const TableCommand As String = "Settings"
    '--- Для COMMAND ---
    Public Const INDEX As String = "Index"
    Public Const COMMAND_DESCRIPTION As String = "Description"
    Public Const COMMAND_NAME As String = "Name"
    Public Const COMMAND_PARAMETER As String = "Параметр"
    Public Const COMMAND_COMMANDER_ID As String = "CommanderID"
    '--- Parameter -----
    Public Const COMMAND_KEY As String = "Key"
    Public Const COMMAND_VALUE As String = "Value"
    Public Const COMMAND_TYPE As String = "Type"
    '--- Для XML ---
    Public Const ATTR_PROCEDURE_NAME As String = "ProcedureName"
    Public Const ATTR_WHAT_MODULE As String = "WhatModule"
    '--- Для Листа ---
    Public Const ID_COMMAND_LV As String = "ID Команды"
    Public Const COMMAND_DESCRIPTION_LV As String = "Описание"
    Public Const COMMANDER_ID_LV As String = "Отправитель"
    Public Const INDEX_COMMAND_LV As String = "Индекс Команды"

    Public Const COMMAND_STOP As String = "Stop"
    Public Const COMMAND_NOTHING As String = "Nothing"

    Public Const CLIENT As String = "Клиент:"
    Public Const SERVER As String = "Сервер"

    Public Const KEY_RICH_TEXT_SERVER As Integer = -1

    Public Enum WhoIsUpdate
        DataView
        XmlDataDocument
    End Enum

    Public Enum TypeParam
        [String]
        [Boolean]
        [DateTime]
        [Double]
        [Int32]
        [Object]
    End Enum

    ' Список вызываемых процедур должен быть описан в XML файле "TasksClientServer.xml"
    Public Const СкажиТекущееВремя As String = "Скажи_текущее_время"
    Public Const УстановиТекущееВремя As String = "Установи_текущее_время"
    Public Const SetPolynomialChannel As String = "Set_Polynomial_Channel"
    Public Const OkSetPolynomialChannel As String = "Ok_Set_Polynomial_Channel"
    Public Const ПоставитьМеткуКТ As String = "Поставить_метку_КТ"
    Public Const ОтветПоставитьМеткуКТ As String = "Ответ_Поставить_метку_КТ"
    Public Const StopClient As String = "Stop_Client"
    Public Const OkStopClient As String = "Ok_Stop_Client"
    Public Const SendMessage As String = "Send_Message"
    Public Const OKSendMessage As String = "Ok_Send_Message"
    Public Const TestConst As String = "TestConst"
End Class

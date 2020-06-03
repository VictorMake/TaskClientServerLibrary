Imports TaskClientServerLibrary

Public Class Form1

    Dim mTasksManager As TasksManager
    Dim mTask As TasksManager.Task

    Dim ПутьРесурсы As String = "G:\DiskD\ПрограммыVBNET\RegistrationНаследование\bin\Ресурсы"

    Private Sub ButtonLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoad.Click
        mTasksManager = New TasksManager(ПутьРесурсы)
        ListBoxTask.Items.Clear()
        For Each mTask As TasksManager.Task In mTasksManager.Tasks.Values
            'ListBoxTask.Items.Add(mTask.ToString)
            ListBoxTask.Items.Add(mTask)
        Next
    End Sub

    Private Sub ButtonChowSelecttion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonChowSelecttion.Click
        If ListBoxTask.SelectedIndex <> -1 Then
            Dim msg As String = vbNullString
            mTask = ListBoxTask.SelectedItem
            For Each mParameter As TasksManager.Task.Parameter In mTask.Parameters.Values
                msg = msg & mParameter.НомерИндекса & vbTab & mParameter.Value & vbTab & mParameter.Type & vbCrLf
            Next
            MessageBox.Show(msg)
        End If
    End Sub

    Private Sub ButtonChowSelecttionByIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonChowSelecttionByIndex.Click
        If ListBoxTask.SelectedIndex <> -1 Then
            Dim msg As String = vbNullString
            mTask = ListBoxTask.SelectedItem
            Dim Key As String
            For I As Integer = 1 To mTask.Count
                Key = I.ToString
                msg = msg & mTask(Key).НомерИндекса & vbTab & mTask(Key).Value & vbTab & mTask(Key).Type & vbCrLf
            Next
            MessageBox.Show(msg)
        End If
    End Sub
End Class

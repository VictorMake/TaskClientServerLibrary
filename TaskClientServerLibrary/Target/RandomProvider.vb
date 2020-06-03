Imports System.Threading

Public NotInheritable Class RandomProvider
    Private Shared seed As Integer = Environment.TickCount
    Private Shared randomWrapper As New ThreadLocal(Of Random)(Function() New Random(Interlocked.Increment(seed)))

    Private Sub New()
    End Sub

    Public Shared Function GetThreadRandom() As Random
        Return randomWrapper.Value
    End Function
End Class

'Dim rnd As Random = RandomProvider.GetThreadRandom()
'Dim value As Integer = rnd.Next(0, 100)

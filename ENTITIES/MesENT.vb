Public Class MesENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vMes As String
    Public Property Mes() As String
        Get
            Return vMes
        End Get
        Set(ByVal value As String)
            vMes = value
        End Set
    End Property

End Class

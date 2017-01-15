Public Class AnoENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vAno As Integer
    Public Property Ano() As Integer
        Get
            Return vAno
        End Get
        Set(ByVal value As Integer)
            vAno = value
        End Set
    End Property

End Class

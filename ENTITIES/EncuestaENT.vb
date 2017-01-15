Public Class EncuestaENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vTema As String
    Public Property Tema() As String
        Get
            Return vTema
        End Get
        Set(ByVal value As String)
            vTema = value
        End Set
    End Property

    Private vFechaVencimiento As DateTime
    Public Property FechaVencimiento() As DateTime
        Get
            Return vFechaVencimiento
        End Get
        Set(ByVal value As DateTime)
            vFechaVencimiento = value
        End Set
    End Property

End Class

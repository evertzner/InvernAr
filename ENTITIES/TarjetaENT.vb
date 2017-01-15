Public Class TarjetaENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vTarjeta As String
    Public Property Tarjeta() As String
        Get
            Return vTarjeta
        End Get
        Set(ByVal value As String)
            vTarjeta = value
        End Set
    End Property

End Class

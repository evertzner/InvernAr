Public Class ChatENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vIdUsuario As Integer
    Public Property IdUsuario() As Integer
        Get
            Return vIdUsuario
        End Get
        Set(ByVal value As Integer)
            vIdUsuario = value
        End Set
    End Property

    Private vMensaje As String
    Public Property Mensaje() As String
        Get
            Return vMensaje
        End Get
        Set(ByVal value As String)
            vMensaje = value
        End Set
    End Property

    Private vFechaHora As DateTime
    Public Property FechaHora() As DateTime
        Get
            Return vFechaHora
        End Get
        Set(ByVal value As DateTime)
            vFechaHora = value
        End Set
    End Property

    Private vLeido As Boolean
    Public Property Leido() As Boolean
        Get
            Return vLeido
        End Get
        Set(ByVal value As Boolean)
            vLeido = value
        End Set
    End Property

    Private vRespuesta As Boolean
    Public Property Respuesta() As Boolean
        Get
            Return vRespuesta
        End Get
        Set(ByVal value As Boolean)
            vRespuesta = value
        End Set
    End Property

End Class

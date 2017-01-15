Public Class ReporteEncuestaENT

    Private vIdEncuesta As Integer
    Public Property IdEncuesta() As Integer
        Get
            Return vIdEncuesta
        End Get
        Set(ByVal value As Integer)
            vIdEncuesta = value
        End Set
    End Property

    Private vIdPregunta As Integer
    Public Property IdPregunta() As Integer
        Get
            Return vIdPregunta
        End Get
        Set(ByVal value As Integer)
            vIdPregunta = value
        End Set
    End Property

    Private vPregunta As String
    Public Property Pregunta() As String
        Get
            Return vPregunta
        End Get
        Set(ByVal value As String)
            vPregunta = value
        End Set
    End Property

    Private vRespuesta As String
    Public Property Respuesta() As String
        Get
            Return vRespuesta
        End Get
        Set(ByVal value As String)
            vRespuesta = value
        End Set
    End Property

    Private vCantidad As Integer
    Public Property Cantidad() As Integer
        Get
            Return vCantidad
        End Get
        Set(ByVal value As Integer)
            vCantidad = value
        End Set
    End Property

End Class

Public Class PreguntaEncuestaENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

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

End Class


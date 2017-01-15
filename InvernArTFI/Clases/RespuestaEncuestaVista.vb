Imports ENTITIES
Imports BLL

Public Class RespuestaEncuestaVista

    Private vRespuestaEncuestaBLL As RespuestaEncuestaBLL
    Public Property RespuestaEncuestaBLL() As RespuestaEncuestaBLL
        Get
            Return vRespuestaEncuestaBLL
        End Get
        Set(ByVal value As RespuestaEncuestaBLL)
            vRespuestaEncuestaBLL = value
        End Set
    End Property

    Private vRespuestaEncuestaENT As RespuestaEncuestaENT
    Public Property RespuestaEncuestaENT() As RespuestaEncuestaENT
        Get
            Return vRespuestaEncuestaENT
        End Get
        Set(ByVal value As RespuestaEncuestaENT)
            vRespuestaEncuestaENT = value
        End Set
    End Property

    Sub New()
        Me.RespuestaEncuestaBLL = New RespuestaEncuestaBLL
        Me.RespuestaEncuestaENT = New RespuestaEncuestaENT
    End Sub

End Class

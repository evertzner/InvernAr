Imports BLL
Imports ENTITIES

Public Class PreguntaEncuestaVista

    Private vPreguntaEncuestaBLL As PreguntaEncuestaBLL
    Public Property PreguntaEncuestaBLL() As PreguntaEncuestaBLL
        Get
            Return vPreguntaEncuestaBLL
        End Get
        Set(ByVal value As PreguntaEncuestaBLL)
            vPreguntaEncuestaBLL = value
        End Set
    End Property

    Private vPreguntaEncuestaENT As PreguntaEncuestaENT
    Public Property PreguntaEncuestaENT() As PreguntaEncuestaENT
        Get
            Return vPreguntaEncuestaENT
        End Get
        Set(ByVal value As PreguntaEncuestaENT)
            vPreguntaEncuestaENT = value
        End Set
    End Property

    Sub New()
        Me.PreguntaEncuestaENT = New PreguntaEncuestaENT
        Me.PreguntaEncuestaBLL = New PreguntaEncuestaBLL
    End Sub

End Class

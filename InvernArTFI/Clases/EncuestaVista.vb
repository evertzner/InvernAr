Imports BLL
Imports ENTITIES

Public Class EncuestaVista

    Private vEncuestaBLL As EncuestaBLL
    Public Property EncuestaBLL() As EncuestaBLL
        Get
            Return vEncuestaBLL
        End Get
        Set(ByVal value As EncuestaBLL)
            vEncuestaBLL = value
        End Set
    End Property

    Private vEncuestaENT As EncuestaENT
    Public Property EncuestaENT() As EncuestaENT
        Get
            Return vEncuestaENT
        End Get
        Set(ByVal value As EncuestaENT)
            vEncuestaENT = value
        End Set
    End Property

    Sub New()
        Me.EncuestaBLL = New EncuestaBLL
        Me.EncuestaENT = New EncuestaENT
    End Sub

End Class

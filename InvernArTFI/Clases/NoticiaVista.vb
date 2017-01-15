Imports BLL
Imports ENTITIES

Public Class NoticiaVista

    Private vNoticiaENT As NoticiaENT
    Public Property NoticiaENT() As NoticiaENT
        Get
            Return vNoticiaENT
        End Get
        Set(ByVal value As NoticiaENT)
            vNoticiaENT = value
        End Set
    End Property

    Private vNoticiaBLL As NoticiaBLL
    Public Property NoticiaBLL() As NoticiaBLL
        Get
            Return vNoticiaBLL
        End Get
        Set(ByVal value As NoticiaBLL)
            vNoticiaBLL = value
        End Set
    End Property

    Sub New()
        Me.NoticiaBLL = New NoticiaBLL
        Me.NoticiaENT = New NoticiaENT
    End Sub

End Class

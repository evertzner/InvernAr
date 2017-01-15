Imports ENTITIES
Imports BLL

Public Class ImagenNoticiaVista

    Private vImagenNoticiaENT As ImagenNoticiaENT
    Public Property ImagenNoticiaENT() As ImagenNoticiaENT
        Get
            Return vImagenNoticiaENT
        End Get
        Set(ByVal value As ImagenNoticiaENT)
            vImagenNoticiaENT = value
        End Set
    End Property

    Private vImagenNoticiaBLL As ImagenNoticiaBLL
    Public Property ImagenNoticiaBLL() As ImagenNoticiaBLL
        Get
            Return vImagenNoticiaBLL
        End Get
        Set(ByVal value As ImagenNoticiaBLL)
            vImagenNoticiaBLL = value
        End Set
    End Property

    Sub New()
        Me.ImagenNoticiaENT = New ImagenNoticiaENT
        Me.ImagenNoticiaBLL = New ImagenNoticiaBLL
    End Sub

End Class

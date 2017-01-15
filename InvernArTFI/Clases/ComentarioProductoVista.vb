Imports ENTITIES
Imports BLL

Public Class ComentarioProductoVista

    Private vComentarioProductoBLL As ComentarioProductoBLL
    Public Property ComentarioProductoBLL() As ComentarioProductoBLL
        Get
            Return vComentarioProductoBLL
        End Get
        Set(ByVal value As ComentarioProductoBLL)
            vComentarioProductoBLL = value
        End Set
    End Property

    Private vComentarioProductoENT As ComentarioProductoENT
    Public Property ComentarioProductoENT() As ComentarioProductoENT
        Get
            Return vComentarioProductoENT
        End Get
        Set(ByVal value As ComentarioProductoENT)
            vComentarioProductoENT = value
        End Set
    End Property

    Sub New()
        Me.ComentarioProductoBLL = New ComentarioProductoBLL
        Me.ComentarioProductoENT = New ComentarioProductoENT
    End Sub

End Class

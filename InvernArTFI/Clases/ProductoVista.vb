Imports ENTITIES
Imports BLL

Public Class ProductoVista

    Private vProductoBLL As ProductoBLL
    Public Property ProductoBLL() As ProductoBLL
        Get
            Return vProductoBLL
        End Get
        Set(ByVal value As ProductoBLL)
            vProductoBLL = value
        End Set
    End Property

    Private vProductoENT As ProductoENT
    Public Property ProductoENT() As ProductoENT
        Get
            Return vProductoENT
        End Get
        Set(ByVal value As ProductoENT)
            vProductoENT = value
        End Set
    End Property

    Sub New()
        Me.ProductoBLL = New ProductoBLL
        Me.ProductoENT = New ProductoENT
    End Sub

End Class

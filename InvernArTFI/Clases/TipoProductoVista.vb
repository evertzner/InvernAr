Imports BLL
Imports ENTITIES

Public Class TipoProductoVista

    Private vTipoProductoENT As TipoProductoENT
    Public Property TipoProductoENT() As TipoProductoENT
        Get
            Return vTipoProductoENT
        End Get
        Set(ByVal value As TipoProductoENT)
            vTipoProductoENT = value
        End Set
    End Property

    Private vTipoProductoBLL As TipoProductoBLL
    Public Property TipoProductoBLL() As TipoProductoBLL
        Get
            Return vTipoProductoBLL
        End Get
        Set(ByVal value As TipoProductoBLL)
            vTipoProductoBLL = value
        End Set
    End Property

    Sub New()
        Me.TipoProductoENT = New TipoProductoENT
        Me.TipoProductoBLL = New TipoProductoBLL
    End Sub

End Class

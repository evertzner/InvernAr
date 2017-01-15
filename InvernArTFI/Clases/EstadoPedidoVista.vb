Imports ENTITIES
Imports BLL

Public Class EstadoPedidoVista

    Private vEstadoPedidoENT As EstadoPedidoENT
    Public Property EstadoPedidoENT() As EstadoPedidoENT
        Get
            Return vEstadoPedidoENT
        End Get
        Set(ByVal value As EstadoPedidoENT)
            vEstadoPedidoENT = value
        End Set
    End Property

    Private vEstadoPedidoBLL As EstadoPedidoBLL
    Public Property EstadoPedidoBLL() As EstadoPedidoBLL
        Get
            Return vEstadoPedidoBLL
        End Get
        Set(ByVal value As EstadoPedidoBLL)
            vEstadoPedidoBLL = value
        End Set
    End Property

    Sub New()
        Me.EstadoPedidoENT = New EstadoPedidoENT
        Me.EstadoPedidoBLL = New EstadoPedidoBLL
    End Sub

End Class

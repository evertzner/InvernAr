Imports ENTITIES
Imports BLL

Public Class PedidoVista

    Private vPedidoENT As PedidoENT
    Public Property PedidoENT() As PedidoENT
        Get
            Return vPedidoENT
        End Get
        Set(ByVal value As PedidoENT)
            vPedidoENT = value
        End Set
    End Property

    Private vPedidoBLL As PedidoBLL
    Public Property PedidoBLL() As PedidoBLL
        Get
            Return vPedidoBLL
        End Get
        Set(ByVal value As PedidoBLL)
            vPedidoBLL = value
        End Set
    End Property

    Sub New()
        Me.PedidoENT = New PedidoENT
        Me.PedidoBLL = New PedidoBLL
    End Sub

End Class

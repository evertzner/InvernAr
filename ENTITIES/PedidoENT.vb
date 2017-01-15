Public Class PedidoENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vIdFactura As Integer
    Public Property IdFactura() As Integer
        Get
            Return vIdFactura
        End Get
        Set(ByVal value As Integer)
            vIdFactura = value
        End Set
    End Property

    Private vEstado As String
    Public Property Estado() As String
        Get
            Return vEstado
        End Get
        Set(ByVal value As String)
            vEstado = value
        End Set
    End Property

    Private vFecha As DateTime
    Public Property Fecha() As DateTime
        Get
            Return vFecha
        End Get
        Set(ByVal value As DateTime)
            vFecha = value
        End Set
    End Property


End Class

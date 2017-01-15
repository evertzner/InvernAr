Public Class FacturaDetalleENT

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

    Private vIdProducto As Integer
    Public Property IdProducto() As Integer
        Get
            Return vIdProducto
        End Get
        Set(ByVal value As Integer)
            vIdProducto = value
        End Set
    End Property

    Private vProductoNombre As String
    Public Property ProductoNombre() As String
        Get
            Return vProductoNombre
        End Get
        Set(ByVal value As String)
            vProductoNombre = value
        End Set
    End Property

    Private vProductoTipo As String
    Public Property ProductoTipo() As String
        Get
            Return vProductoTipo
        End Get
        Set(ByVal value As String)
            vProductoTipo = value
        End Set
    End Property

    Private vCantidad As Integer
    Public Property Cantidad() As Integer
        Get
            Return vCantidad
        End Get
        Set(ByVal value As Integer)
            vCantidad = value
        End Set
    End Property

    Private vPrecioUnitario As Double
    Public Property PrecioUnitario() As Double
        Get
            Return vPrecioUnitario
        End Get
        Set(ByVal value As Double)
            vPrecioUnitario = value
        End Set
    End Property

    Private vPrecio As Double
    Public Property Precio() As Double
        Get
            Return vPrecio
        End Get
        Set(ByVal value As Double)
            vPrecio = value
        End Set
    End Property

End Class

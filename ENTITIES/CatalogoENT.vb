Public Class CatalogoENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
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

    Private vOrden As Integer
    Public Property Orden() As Integer
        Get
            Return vOrden
        End Get
        Set(ByVal value As Integer)
            vOrden = value
        End Set
    End Property

End Class

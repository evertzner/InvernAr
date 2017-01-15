Public Class ProductoCatalogoENT
    Inherits ProductoENT

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

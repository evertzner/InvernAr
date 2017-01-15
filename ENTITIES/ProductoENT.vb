Public Class ProductoENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vCodigo As String
    Public Property Codigo() As String
        Get
            Return vCodigo
        End Get
        Set(ByVal value As String)
            vCodigo = value
        End Set
    End Property

    Private vNombre As String
    Public Property Nombre() As String
        Get
            Return vNombre
        End Get
        Set(ByVal value As String)
            vNombre = value
        End Set
    End Property

    Private vEspecificacion As String
    Public Property Especificacion() As String
        Get
            Return vEspecificacion
        End Get
        Set(ByVal value As String)
            vEspecificacion = value
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

    Private vTipo As String
    Public Property Tipo() As String
        Get
            Return vTipo
        End Get
        Set(ByVal value As String)
            vTipo = value
        End Set
    End Property

    Private vImagen As Byte()
    Public Property Imagen() As Byte()
        Get
            Return vImagen
        End Get
        Set(ByVal value As Byte())
            vImagen = value
        End Set
    End Property

End Class

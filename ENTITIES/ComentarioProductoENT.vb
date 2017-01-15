Public Class ComentarioProductoENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vIdUsuario As Integer
    Public Property IdUsuario() As Integer
        Get
            Return vIdUsuario
        End Get
        Set(ByVal value As Integer)
            vIdUsuario = value
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

    Private vComentario As String
    Public Property Comentario() As String
        Get
            Return vComentario
        End Get
        Set(ByVal value As String)
            vComentario = value
        End Set
    End Property

    Private vValorizacion As Integer
    Public Property Valorizacion() As Integer
        Get
            Return vValorizacion
        End Get
        Set(ByVal value As Integer)
            vValorizacion = value
        End Set
    End Property


    Private vFechaComentado As DateTime
    Public Property FechaComentado() As DateTime
        Get
            Return vFechaComentado
        End Get
        Set(ByVal value As DateTime)
            vFechaComentado = value
        End Set
    End Property

    Private vNombreUsuario As String
    Public Property NombreUsuario() As String
        Get
            Return vNombreUsuario
        End Get
        Set(ByVal value As String)
            vNombreUsuario = value
        End Set
    End Property


End Class

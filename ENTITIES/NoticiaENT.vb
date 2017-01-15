Public Class NoticiaENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vTitulo As String
    Public Property Titulo() As String
        Get
            Return vTitulo
        End Get
        Set(ByVal value As String)
            vTitulo = value
        End Set
    End Property

    Private vContenido As String
    Public Property Contenido() As String
        Get
            Return vContenido
        End Get
        Set(ByVal value As String)
            vContenido = value
        End Set
    End Property

    Private vFechaHora As DateTime
    Public Property FechaHora() As DateTime
        Get
            Return vFechaHora
        End Get
        Set(ByVal value As DateTime)
            vFechaHora = value
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

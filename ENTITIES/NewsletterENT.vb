Public Class NewsletterENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
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

    Private vDescripcion As String
    Public Property Descripcion() As String
        Get
            Return vDescripcion
        End Get
        Set(ByVal value As String)
            vDescripcion = value
        End Set
    End Property

    Private vAsunto As String
    Public Property Asunto() As String
        Get
            Return vAsunto
        End Get
        Set(ByVal value As String)
            vAsunto = value
        End Set
    End Property

    Private vCuerpo As String
    Public Property Cuerpo() As String
        Get
            Return vCuerpo
        End Get
        Set(ByVal value As String)
            vCuerpo = value
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

    Private vFechaHora As DateTime
    Public Property FechaHora() As DateTime
        Get
            Return vFechaHora
        End Get
        Set(ByVal value As DateTime)
            vFechaHora = value
        End Set
    End Property

    Private vCategoria As String
    Public Property Categoria() As String
        Get
            Return vCategoria
        End Get
        Set(ByVal value As String)
            vCategoria = value
        End Set
    End Property

End Class

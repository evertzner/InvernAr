Public Class ImagenNoticiaENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vIdNoticia As Integer
    Public Property IdNoticia() As Integer
        Get
            Return vIdNoticia
        End Get
        Set(ByVal value As Integer)
            vIdNoticia = value
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

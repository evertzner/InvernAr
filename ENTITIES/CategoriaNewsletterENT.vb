Public Class CategoriaNewsletterENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
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

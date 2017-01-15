Public Class SuscripcionNewsletterENT

    Private vCorreoElectronico As String
    Public Property CorreoElectronico() As String
        Get
            Return vCorreoElectronico
        End Get
        Set(ByVal value As String)
            vCorreoElectronico = value
        End Set
    End Property

    Private vCategoria As Integer
    Public Property Categoria() As Integer
        Get
            Return vCategoria
        End Get
        Set(ByVal value As Integer)
            vCategoria = value
        End Set
    End Property

End Class

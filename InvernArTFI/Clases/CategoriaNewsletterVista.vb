Imports ENTITIES
Imports BLL

Public Class CategoriaNewsletterVista

    Private vCategoriaNewsletterENT As CategoriaNewsletterENT
    Public Property CategoriaNewsletterENT() As CategoriaNewsletterENT
        Get
            Return vCategoriaNewsletterENT
        End Get
        Set(ByVal value As CategoriaNewsletterENT)
            vCategoriaNewsletterENT = value
        End Set
    End Property

    Private vCategoriaNewsletterBLL As CategoriaNewsletterBLL
    Public Property CategoriaNewsletterBLL() As CategoriaNewsletterBLL
        Get
            Return vCategoriaNewsletterBLL
        End Get
        Set(ByVal value As CategoriaNewsletterBLL)
            vCategoriaNewsletterBLL = value
        End Set
    End Property

    Sub New()
        Me.CategoriaNewsletterENT = New CategoriaNewsletterENT
        Me.CategoriaNewsletterBLL = New CategoriaNewsletterBLL
    End Sub

End Class

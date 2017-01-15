Imports ENTITIES
Imports BLL

Public Class NewsletterVista

    Private vNewsletterBLL As NewsletterBLL
    Public Property NewsletterBLL() As NewsletterBLL
        Get
            Return vNewsletterBLL
        End Get
        Set(ByVal value As NewsletterBLL)
            vNewsletterBLL = value
        End Set
    End Property

    Private vNewsletterENT As NewsletterENT
    Public Property NewsletterENT() As NewsletterENT
        Get
            Return vNewsletterENT
        End Get
        Set(ByVal value As NewsletterENT)
            vNewsletterENT = value
        End Set
    End Property

    Sub New()
        Me.NewsletterBLL = New NewsletterBLL
        Me.NewsletterENT = New NewsletterENT
    End Sub


End Class

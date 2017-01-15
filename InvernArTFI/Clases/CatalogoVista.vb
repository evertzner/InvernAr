Imports BLL
Imports ENTITIES

Public Class CatalogoVista

    Private vCatalogoBLL As CatalogoBLL
    Public Property CatalogoBLL() As CatalogoBLL
        Get
            Return vCatalogoBLL
        End Get
        Set(ByVal value As CatalogoBLL)
            vCatalogoBLL = value
        End Set
    End Property

    Private vCatalogoENT As CatalogoENT
    Public Property CatalogoENT() As CatalogoENT
        Get
            Return vCatalogoENT
        End Get
        Set(ByVal value As CatalogoENT)
            vCatalogoENT = value
        End Set
    End Property

    Sub New()
        Me.CatalogoBLL = New CatalogoBLL
        Me.CatalogoENT = New CatalogoENT
    End Sub

End Class

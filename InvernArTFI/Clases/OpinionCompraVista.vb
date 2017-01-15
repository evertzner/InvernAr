Imports ENTITIES
Imports BLL

Public Class OpinionCompraVista

    Private vOpinionCompraBLL As OpinionCompraBLL
    Public Property OpinionCompraBLL() As OpinionCompraBLL
        Get
            Return vOpinionCompraBLL
        End Get
        Set(ByVal value As OpinionCompraBLL)
            vOpinionCompraBLL = value
        End Set
    End Property

    Private vOpinionCompraENT As OpinionCompraENT
    Public Property OpinionCompraENT() As OpinionCompraENT
        Get
            Return vOpinionCompraENT
        End Get
        Set(ByVal value As OpinionCompraENT)
            vOpinionCompraENT = value
        End Set
    End Property

    Sub New()
        Me.OpinionCompraENT = New OpinionCompraENT
        Me.OpinionCompraBLL = New OpinionCompraBLL
    End Sub

End Class

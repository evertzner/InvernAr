Imports ENTITIES
Imports BLL

Public Class FacturaVista

    Private vFacturaBLL As FacturaBLL
    Public Property FacturaBLL() As FacturaBLL
        Get
            Return vFacturaBLL
        End Get
        Set(ByVal value As FacturaBLL)
            vFacturaBLL = value
        End Set
    End Property

    Private vFacturaENT As FacturaENT
    Public Property FacturaENT() As FacturaENT
        Get
            Return vFacturaENT
        End Get
        Set(ByVal value As FacturaENT)
            vFacturaENT = value
        End Set
    End Property

    Sub New()
        Me.FacturaENT = New FacturaENT
        Me.FacturaBLL = New FacturaBLL
    End Sub

End Class

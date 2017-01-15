Imports ENTITIES
Imports BLL

Public Class FacturaDetalleVista

    Private vFacturaDetalleENT As FacturaDetalleENT
    Public Property FacturaDetalleENT() As FacturaDetalleENT
        Get
            Return vFacturaDetalleENT
        End Get
        Set(ByVal value As FacturaDetalleENT)
            vFacturaDetalleENT = value
        End Set
    End Property

    Private vFacturaDetalleBLL As FacturaDetalleBLL
    Public Property FacturaDetalleBLL() As FacturaDetalleBLL
        Get
            Return vFacturaDetalleBLL
        End Get
        Set(ByVal value As FacturaDetalleBLL)
            vFacturaDetalleBLL = value
        End Set
    End Property

    Sub New()
        Me.FacturaDetalleENT = New FacturaDetalleENT
        Me.FacturaDetalleBLL = New FacturaDetalleBLL
    End Sub

End Class

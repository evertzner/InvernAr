Imports ENTITIES
Imports BLL

Public Class InstalacionDetalleVista

    Private vInstalacionDetalleENT As InstalacionDetalleENT
    Public Property InstalacionDetalleENT() As InstalacionDetalleENT
        Get
            Return vInstalacionDetalleENT
        End Get
        Set(ByVal value As InstalacionDetalleENT)
            vInstalacionDetalleENT = value
        End Set
    End Property

    Private vInstalacionDetalleBLL As InstalacionDetalleBLL
    Public Property InstalacionDetalleBLL() As InstalacionDetalleBLL
        Get
            Return vInstalacionDetalleBLL
        End Get
        Set(ByVal value As InstalacionDetalleBLL)
            vInstalacionDetalleBLL = value
        End Set
    End Property

    Sub New()
        Me.InstalacionDetalleENT = New InstalacionDetalleENT
        Me.InstalacionDetalleBLL = New InstalacionDetalleBLL
    End Sub

End Class

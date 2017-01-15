Imports ENTITIES
Imports BLL

Public Class ReporteVentasVista

    Private vReporteVentasENT As ReporteVentasENT
    Public Property ReporteVentasENT() As ReporteVentasENT
        Get
            Return vReporteVentasENT
        End Get
        Set(ByVal value As ReporteVentasENT)
            vReporteVentasENT = value
        End Set
    End Property

    Private vReporteVentasBLL As ReporteVentasBLL
    Public Property ReporteVentasBLL() As ReporteVentasBLL
        Get
            Return vReporteVentasBLL
        End Get
        Set(ByVal value As ReporteVentasBLL)
            vReporteVentasBLL = value
        End Set
    End Property

    Sub New()
        Me.ReporteVentasENT = New ReporteVentasENT
        Me.ReporteVentasBLL = New ReporteVentasBLL
    End Sub

End Class

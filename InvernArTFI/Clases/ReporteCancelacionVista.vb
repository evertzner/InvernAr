Imports ENTITIES
Imports BLL

Public Class ReporteCancelacionVista

    Private vReporteCancelacionENT As ReporteCancelacionENT
    Public Property ReporteCancelacionENT() As ReporteCancelacionENT
        Get
            Return vReporteCancelacionENT
        End Get
        Set(ByVal value As ReporteCancelacionENT)
            vReporteCancelacionENT = value
        End Set
    End Property

    Private vReporteCancelacionBLL As ReporteCancelacionBLL
    Public Property ReporteCancelacionBLL() As ReporteCancelacionBLL
        Get
            Return vReporteCancelacionBLL
        End Get
        Set(ByVal value As ReporteCancelacionBLL)
            vReporteCancelacionBLL = value
        End Set
    End Property

    Sub New()
        Me.ReporteCancelacionENT = New ReporteCancelacionENT
        Me.ReporteCancelacionBLL = New ReporteCancelacionBLL
    End Sub

End Class

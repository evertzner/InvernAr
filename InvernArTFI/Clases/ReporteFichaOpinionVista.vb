Imports ENTITIES
Imports BLL

Public Class ReporteFichaOpinionVista

    Private vReporteFichaOpinionENT As ReporteFichaOpinionENT
    Public Property ReporteFichaOpinionENT() As ReporteFichaOpinionENT
        Get
            Return vReporteFichaOpinionENT
        End Get
        Set(ByVal value As ReporteFichaOpinionENT)
            vReporteFichaOpinionENT = value
        End Set
    End Property

    Private vReporteFichaOpinionBLL As ReporteFichaOpinionBLL
    Public Property ReporteFichaOpinionBLL() As ReporteFichaOpinionBLL
        Get
            Return vReporteFichaOpinionBLL
        End Get
        Set(ByVal value As ReporteFichaOpinionBLL)
            vReporteFichaOpinionBLL = value
        End Set
    End Property

    Sub New()
        Me.ReporteFichaOpinionENT = New ReporteFichaOpinionENT
        Me.ReporteFichaOpinionBLL = New ReporteFichaOpinionBLL
    End Sub

End Class

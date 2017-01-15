Imports ENTITIES
Imports BLL

Public Class ReporteEncuestaVista

    Private vReporteEncuestaENT As ReporteEncuestaENT
    Public Property ReporteEncuestaENT() As ReporteEncuestaENT
        Get
            Return vReporteEncuestaENT
        End Get
        Set(ByVal value As ReporteEncuestaENT)
            vReporteEncuestaENT = value
        End Set
    End Property

    Private vReporteEncuestaBLL As ReporteEncuestaBLL
    Public Property ReporteEncuestaBLL() As ReporteEncuestaBLL
        Get
            Return vReporteEncuestaBLL
        End Get
        Set(ByVal value As ReporteEncuestaBLL)
            vReporteEncuestaBLL = value
        End Set
    End Property

    Sub New()
        Me.ReporteEncuestaENT = New ReporteEncuestaENT
        Me.ReporteEncuestaBLL = New ReporteEncuestaBLL
    End Sub

End Class

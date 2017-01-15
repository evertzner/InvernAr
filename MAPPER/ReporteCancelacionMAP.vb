Imports ENTITIES
Imports DAL

Public Class ReporteCancelacionMAP

    Shared Function ReporteCancelacion() As DataSet
        Return Generico.Leer("pReporteCancelacion")
    End Function

End Class

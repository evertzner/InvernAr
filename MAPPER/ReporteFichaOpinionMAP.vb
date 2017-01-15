Imports ENTITIES
Imports DAL

Public Class ReporteFichaOpinionMAP

    Shared Function ReporteFichaOpinion() As DataSet
        Return Generico.Leer("pReporteFichaOpinion")
    End Function

End Class

Imports ENTITIES
Imports MAPPER

Public Class ReporteFichaOpinionBLL
    Dim ReporteFichaOpinionENT As ReporteFichaOpinionENT

    Public Function ReporteFichaOpinion() As List(Of ReporteFichaOpinionENT)
        Dim ReporteOpinion As New List(Of ReporteFichaOpinionENT)
        Dim lector As IDataReader = ReporteFichaOpinionMAP.ReporteFichaOpinion.CreateDataReader
        Do While lector.Read()
            ReporteFichaOpinionENT = New ReporteFichaOpinionENT
            With ReporteFichaOpinionENT
                .Opinion = Convert.ToString(lector("Opinion"))
                .Cuenta = Convert.ToInt32(lector("Cuenta"))
            End With
            ReporteOpinion.Add(ReporteFichaOpinionENT)
        Loop
        lector.Close()
        Return ReporteOpinion
    End Function

End Class

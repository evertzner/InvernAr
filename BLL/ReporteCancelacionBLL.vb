Imports ENTITIES
Imports MAPPER

Public Class ReporteCancelacionBLL
    Dim ReporteCancelacionENT As ReporteCancelacionENT

    Public Function ReporteCancelaciones() As List(Of ReporteCancelacionENT)
        Dim ReporteCancelacion As New List(Of ReporteCancelacionENT)
        Dim lector As IDataReader = ReporteCancelacionMAP.ReporteCancelacion.CreateDataReader
        Do While lector.Read()
            ReporteCancelacionENT = New ReporteCancelacionENT
            With ReporteCancelacionENT
                .Motivo = Convert.ToString(lector("Motivo"))
                .Cuenta = Convert.ToInt32(lector("Cuenta"))
            End With
            ReporteCancelacion.Add(ReporteCancelacionENT)
        Loop
        lector.Close()
        Return ReporteCancelacion
    End Function

End Class

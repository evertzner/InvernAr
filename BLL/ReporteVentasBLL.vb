Imports ENTITIES
Imports MAPPER

Public Class ReporteVentasBLL
    Dim ReporteVentasENT As ReporteVentasENT

    Public Function ReporteVentas() As List(Of ReporteVentasENT)
        Dim ReporteVenta As New List(Of ReporteVentasENT)
        Dim lector As IDataReader = ReporteVentasMAP.ReporteVentas.CreateDataReader
        Do While lector.Read()
            ReporteVentasENT = New ReporteVentasENT
            With ReporteVentasENT
                .Fecha = Convert.ToDateTime(lector("Fecha"))
                .Total = Convert.ToDouble(lector("Total"))
            End With
            ReporteVenta.Add(ReporteVentasENT)
        Loop
        lector.Close()
        Return ReporteVenta
    End Function

End Class

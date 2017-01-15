Imports ENTITIES
Imports DAL

Public Class ReporteVentasMAP

    Shared Function ReporteVentas() As DataSet
        Return Generico.Leer("pReporteVentas")
    End Function

End Class

Imports ENTITIES
Imports DAL

Public Class ReporteEncuestaMAP

    Shared Function ReporteEncuestas(ByRef QueObjeto As Object) As DataSet
        Dim ReporteEncuesta As New ReporteEncuestaENT
        ReporteEncuesta = DirectCast(QueObjeto, ReporteEncuestaENT)
        Dim HT As New Hashtable
        HT.Add("@pIdEncuesta", ReporteEncuesta.IdEncuesta)
        Return Generico.Leer("pReporteEncuestas", HT)
    End Function

End Class

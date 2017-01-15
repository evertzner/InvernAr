Imports ENTITIES
Imports MAPPER

Public Class ReporteEncuestaBLL
    Dim ReporteEncuestaENT As ReporteEncuestaENT

    Public Function ReporteEncuestas(ByRef QueObjeto As Object) As List(Of ReporteEncuestaENT)
        Dim ReporteEncuesta As New List(Of ReporteEncuestaENT)
        Dim lector As IDataReader = ReporteEncuestaMAP.ReporteEncuestas(QueObjeto).CreateDataReader
        Do While lector.Read()
            ReporteEncuestaENT = New ReporteEncuestaENT
            With ReporteEncuestaENT
                .IdEncuesta = Convert.ToInt32(lector("IdEncuesta"))
                .IdPregunta = Convert.ToInt32(lector("IdPregunta"))
                .Pregunta = Convert.ToString(lector("Pregunta"))
                .Respuesta = Convert.ToString(lector("Respuesta"))
                .Cantidad = Convert.ToInt32(lector("Cantidad"))
            End With
            ReporteEncuesta.Add(ReporteEncuestaENT)
        Loop
        lector.Close()
        Return ReporteEncuesta
    End Function

End Class

Imports ENTITIES
Imports MAPPER

Public Class PreguntaEncuestaBLL
    Dim PreguntaEncuestaENT As PreguntaEncuestaENT

    Public Function ListarPreguntas(ByRef QueObjeto As Object) As List(Of PreguntaEncuestaENT)
        Dim ListaPreguntaEncuesta As New List(Of PreguntaEncuestaENT)
        Dim lector As IDataReader = PreguntaEncuestaMAP.ListarPreguntas(QueObjeto).CreateDataReader
        Do While lector.Read()
            PreguntaEncuestaENT = New PreguntaEncuestaENT
            With PreguntaEncuestaENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdEncuesta = Convert.ToInt32(lector("IdEncuesta"))
                .IdPregunta = Convert.ToInt32(lector("IdPregunta"))
                .Pregunta = Convert.ToString(lector("Pregunta"))
            End With
            ListaPreguntaEncuesta.Add(PreguntaEncuestaENT)
        Loop
        lector.Close()
        Return ListaPreguntaEncuesta
    End Function

    Public Sub AgregarPreguntas(ByRef QueObjeto As Object)
        PreguntaEncuestaMAP.AgregarPreguntas(QueObjeto)
    End Sub

    Public Sub ModificarPreguntas(ByRef QueObjeto As Object)
        PreguntaEncuestaMAP.ModificarPreguntas(QueObjeto)
    End Sub

    Public Function EliminarPreguntas(ByRef QueObjeto As Object) As Integer
        Return PreguntaEncuestaMAP.EliminarPreguntas(QueObjeto)
    End Function

End Class

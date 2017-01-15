Imports ENTITIES
Imports DAL

Public Class PreguntaEncuestaMAP

    Shared Function ListarPreguntas(ByRef QueObjeto As Object) As DataSet
        Dim PreguntaEncuesta As New PreguntaEncuestaENT
        PreguntaEncuesta = DirectCast(QueObjeto, PreguntaEncuestaENT)
        Dim HT As New Hashtable
        HT.Add("@pIdEncuesta", PreguntaEncuesta.IdEncuesta)
        Return Generico.Leer("pListarPreguntas", HT)
    End Function

    Shared Sub AgregarPreguntas(ByRef QueObjeto As Object)
        Dim PreguntaEncuesta As New PreguntaEncuestaENT
        PreguntaEncuesta = DirectCast(QueObjeto, PreguntaEncuestaENT)
        Dim HT As New Hashtable
        HT.Add("@pIdEncuesta", PreguntaEncuesta.IdEncuesta)
        HT.Add("@pIdPregunta", PreguntaEncuesta.IdPregunta)
        HT.Add("@pPregunta", PreguntaEncuesta.Pregunta)
        Generico.Escribir("pAgregarPreguntas", HT)
    End Sub

    Shared Sub ModificarPreguntas(ByRef QueObjeto As Object)
        Dim PreguntaEncuesta As New PreguntaEncuestaENT
        PreguntaEncuesta = DirectCast(QueObjeto, PreguntaEncuestaENT)
        Dim HT As New Hashtable
        HT.Add("@pId", PreguntaEncuesta.Id)
        HT.Add("@pIdPregunta", PreguntaEncuesta.IdPregunta)
        HT.Add("@pPregunta", PreguntaEncuesta.Pregunta)
        Generico.Escribir("pModificarPreguntas", HT)
    End Sub

    Shared Function EliminarPreguntas(ByRef QueObjeto As Object) As Integer
        Dim PreguntaEncuesta As New PreguntaEncuestaENT
        PreguntaEncuesta = DirectCast(QueObjeto, PreguntaEncuestaENT)
        Dim HT As New Hashtable
        HT.Add("@pId", PreguntaEncuesta.Id)
        Return Generico.Escribir("pEliminarPreguntas", HT)
    End Function

End Class

Imports ENTITIES
Imports DAL

Public Class RespuestaEncuestaMAP

    Shared Sub Responder(ByRef QueObjeto As Object)
        Dim RespuestaEncuesta As New RespuestaEncuestaENT
        RespuestaEncuesta = DirectCast(QueObjeto, RespuestaEncuestaENT)
        Dim HT As New Hashtable
        HT.Add("@pIdPregunta", RespuestaEncuesta.IdPregunta)
        HT.Add("@pIdEncuesta", RespuestaEncuesta.IdEncuesta)
        HT.Add("@pCorreoElectronico", RespuestaEncuesta.CorreoElectronico)
        HT.Add("@pRespuesta", RespuestaEncuesta.Respuesta)
        Generico.Escribir("pResponder", HT)
    End Sub

End Class

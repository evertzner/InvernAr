Imports ENTITIES
Imports DAL

Public Class EncuestaMAP

    Shared Function ListarEncuestas() As DataSet
        Return Generico.Leer("pListarEncuestas")
    End Function

    Shared Sub NuevaEncuesta(ByRef QueObjeto As Object)
        Dim Encuesta As New EncuestaENT
        Encuesta = DirectCast(QueObjeto, EncuestaENT)
        Dim HT As New Hashtable
        HT.Add("@pTema", Encuesta.Tema)
        HT.Add("@pFechaVencimiento", Encuesta.FechaVencimiento)
        Generico.Escribir("pNuevaEncuesta", HT)
    End Sub

    Shared Sub ModificarEncuesta(ByRef QueObjeto As Object)
        Dim Encuesta As New EncuestaENT
        Encuesta = DirectCast(QueObjeto, EncuestaENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Encuesta.Id)
        HT.Add("@pTema", Encuesta.Tema)
        HT.Add("@pFechaVencimiento", Encuesta.FechaVencimiento)
        Generico.Escribir("pModificarEncuesta", HT)
    End Sub

    Shared Function EliminarEncuesta(ByRef QueObjeto As Object) As Integer
        Dim Encuesta As New EncuestaENT
        Encuesta = DirectCast(QueObjeto, EncuestaENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Encuesta.Id)
        Return Generico.Escribir("pEliminarEncuesta", HT)
    End Function

End Class

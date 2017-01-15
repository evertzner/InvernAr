Imports ENTITIES
Imports DAL

Public Class ChatMAP

    Shared Function ListarTodosLosMensajes() As DataSet
        Return Generico.Leer("pListarTodosLosMensajes")
    End Function

    Shared Function ListarCantidadDeMensajes() As DataSet
        Return Generico.Leer("pListarCantidadDeMensajes")
    End Function

    Shared Function ListarMensajes(ByRef QueObjeto As Object) As DataSet
        Dim Chat As New ChatENT
        Chat = DirectCast(QueObjeto, ChatENT)
        Dim HT As New Hashtable
        HT.Add("@pIdUsuario", Chat.IdUsuario)
        Return Generico.Leer("pListarMensajes", HT)
    End Function

    Shared Sub NuevoMensaje(ByRef QueObjeto As Object)
        Dim Chat As New ChatENT
        Chat = DirectCast(QueObjeto, ChatENT)
        Dim HT As New Hashtable
        HT.Add("@pIdUsuario", Chat.IdUsuario)
        HT.Add("@pMensaje", Chat.Mensaje)
        HT.Add("@pFechaHora", Chat.FechaHora)
        HT.Add("@pRespuesta", Chat.Respuesta)
        Generico.Escribir("pNuevoMensaje", HT)
    End Sub

    Shared Sub LeerMensaje(ByRef QueObjeto As Object)
        Dim Chat As New ChatENT
        Chat = DirectCast(QueObjeto, ChatENT)
        Dim HT As New Hashtable
        HT.Add("@pIdUsuario", Chat.IdUsuario)
        HT.Add("@pRespuesta", Chat.Respuesta)
        Generico.Escribir("pLeerMensaje", HT)
    End Sub

    Shared Function NotificarNuevoMensaje(ByRef QueObjeto As Object, Respuesta As Boolean) As DataSet
        Dim Usuario As New UsuarioENT
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        Dim HT As New Hashtable
        If QueObjeto Is Nothing Then
            HT.Add("@pIdUsuario", DBNull.Value)
        Else
            HT.Add("@pIdUsuario", Usuario.ID)
        End If
        HT.Add("@pRespuesta", Respuesta)

        Return Generico.Leer("pNotificarNuevoMensaje", HT)
    End Function


End Class

Imports ENTITIES
Imports DAL

Public Class PermisoMAP

    Shared Function ConsultaPermisos(ByRef QueObjeto As Object) As DataSet
        Dim Usuario As New UsuarioENT
        Dim HT As New Hashtable
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        If Usuario Is Nothing Then
            HT.Add("@pId", DBNull.Value)
        Else
            HT.Add("@pId", Usuario.ID)
        End If
        Return Generico.Leer("pConsultarPermisos", HT)
    End Function

    Shared Function ListarPermisos() As DataSet
        Return Generico.Leer("pListarPermisos")
    End Function

End Class

Imports ENTITIES
Imports DAL

Public Class BitacoraMAP

    Shared Sub RegistrarBitacora(ByRef QueObjeto As Object)
        Dim Bitacora As New BitacoraENT
        Dim HT As New Hashtable
        Bitacora = DirectCast(QueObjeto, BitacoraENT)
        HT.Add("@pFechaActividad", Bitacora.FechaActividad)
        HT.Add("@pUsuario", Bitacora.Usuario)
        HT.Add("@pActividad", Bitacora.Actividad)
        Generico.Escribir("pRegistrarBitacora", HT)
    End Sub

    Shared Function ConsultarBitacora(ByRef QueObjeto As Object) As DataSet
        Dim Bitacora As New BitacoraENT
        Dim HT As New Hashtable
        Bitacora = DirectCast(QueObjeto, BitacoraENT)
        If Bitacora.Usuario = Nothing Then
            HT.Add("@pUsuario", DBNull.Value)
        Else
            HT.Add("@pUsuario", Bitacora.Usuario)
        End If
        Return Generico.Leer("pConsultarBitacora", HT)
    End Function

    Shared Function ConsultarUsuariosEnBitacora() As DataSet
        Return Generico.Leer("pConsultarUsuariosEnBitacora")
    End Function

End Class

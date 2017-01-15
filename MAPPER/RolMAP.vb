Imports ENTITIES
Imports DAL

Public Class RolMAP

    Shared Function ListarRoles() As DataSet
        Return Generico.Leer("pListarRoles")
    End Function

    Shared Function ListarRolesSinUFC() As DataSet
        Return Generico.Leer("pListarRolesSinUFC")
    End Function

    Shared Function BajaRol(ByRef QueObjeto As Object) As Integer
        Dim Rol As New RolENT
        Rol = DirectCast(QueObjeto, RolENT)
        Dim HT As New Hashtable
        HT.Add("@pCodigo", Rol.Codigo)
        Return Generico.Escribir("pBajaRol", HT)
    End Function

    Shared Sub AltaRol(ByRef QueObjeto As Object)
        Dim Rol As New RolENT
        Rol = DirectCast(QueObjeto, RolENT)
        Dim HT As New Hashtable
        HT.Add("@pCodigo", Rol.Codigo)
        HT.Add("@pNombre", Rol.Nombre)
        Generico.Escribir("pAltaRol", HT)
    End Sub

    Shared Sub AgregarRolPermiso(ByRef QueObjeto As Object)
        Dim Rol As New RolENT
        Rol = DirectCast(QueObjeto, RolENT)
        Dim HT As New Hashtable
        HT.Add("@pCodigoRol", Rol.Codigo)
        For Each PermisoENT In Rol.ListaPermisos
            HT.Remove("@pCodigoPermiso")
            HT.Add("@pCodigoPermiso", PermisoENT.Codigo)
            Generico.Escribir("pAgregarRolPermiso", HT)
        Next
    End Sub

    Shared Sub BorrarRolPermiso(ByRef QueObjeto As Object)
        Dim Rol As New RolENT
        Rol = DirectCast(QueObjeto, RolENT)
        Dim HT As New Hashtable
        HT.Add("@pCodigoRol", Rol.Codigo)
        Generico.Escribir("pBorrarRolPermiso", HT)
    End Sub

    Shared Function ListarRolesPermisos(ByRef QueObjeto As Object) As DataSet
        Dim Rol As New RolENT
        Rol = DirectCast(QueObjeto, RolENT)
        Dim HT As New Hashtable
        HT.Add("@pCodigoRol", Rol.Codigo)
        Return Generico.Leer("pListarRolesPermisos", HT)
    End Function

    Shared Sub ModificarRol(ByRef QueObjeto As Object)
        Dim Rol As New RolENT
        Rol = DirectCast(QueObjeto, RolENT)
        Dim HT As New Hashtable
        HT.Add("@pCodigo", Rol.Codigo)
        HT.Add("@pNombre", Rol.Nombre)
        Generico.Escribir("pModificarRol", HT)
    End Sub



End Class

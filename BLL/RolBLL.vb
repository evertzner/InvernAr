Imports ENTITIES
Imports MAPPER

Public Class RolBLL
    Public Function ListarRoles() As List(Of RolENT)
        Dim ListaRoles As New List(Of RolENT)
        Dim lector As IDataReader = RolMAP.ListarRoles.CreateDataReader
        Do While lector.Read()
            Dim Rol As New RolENT
            Rol.Codigo = Convert.ToString(lector("Codigo"))
            Rol.Nombre = Convert.ToString(lector("Nombre"))
            ListaRoles.Add(Rol)
        Loop
        lector.Close()
        Return ListaRoles
    End Function

    Public Function ListarRolesSinUFC() As List(Of RolENT)
        Dim ListaRoles As New List(Of RolENT)
        Dim lector As IDataReader = RolMAP.ListarRolesSinUFC.CreateDataReader
        Do While lector.Read()
            Dim Rol As New RolENT
            Rol.Codigo = Convert.ToString(lector("Codigo"))
            Rol.Nombre = Convert.ToString(lector("Nombre"))
            ListaRoles.Add(Rol)
        Loop
        lector.Close()
        Return ListaRoles
    End Function

    Public Sub AltaRol(ByRef QueObjeto As Object)
        RolMAP.AltaRol(QueObjeto)
    End Sub

    Public Function BajaRol(ByRef QueObjeto As Object) As Integer
        Return RolMAP.BajaRol(QueObjeto)
    End Function

    Public Sub AgregarRolPermiso(ByRef QueObjeto As Object)
        RolMAP.AgregarRolPermiso(QueObjeto)
    End Sub

    Public Sub BorrarRolPermiso(ByRef QueObjeto As Object)
        RolMAP.BorrarRolPermiso(QueObjeto)
    End Sub

    Public Sub ModificarRol(ByRef QueObjeto As Object)
        RolMAP.ModificarRol(QueObjeto)
    End Sub

    Public Function ListarRolesPermisos(ByRef QueObjeto As Object) As RolENT
        Dim lector As IDataReader = RolMAP.ListarRolesPermisos(QueObjeto).CreateDataReader
        Dim Rol As New RolENT
        Rol.Codigo = DirectCast(QueObjeto, RolENT).Codigo
        Do While lector.Read()
            Dim Permiso As New PermisoENT
            Permiso.Codigo = Convert.ToString(lector("CodigoPermiso"))
            Rol.ListaPermisos.Add(Permiso)
        Loop
        lector.Close()
        Return Rol
    End Function
End Class

Imports ENTITIES
Imports DAL

Public Class UsuarioMAP

    Shared Function ConsultaLogIn(ByRef QueObjeto As Object) As DataSet
        Dim Usuario As New UsuarioENT
        Dim HT As New Hashtable
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        HT.Add("@pCorreoElectronico", Usuario.CorreoElectronico)
        Return Generico.Leer("pConsultaLogIn", HT)
    End Function

    Shared Function Modificacion(ByRef QueObjeto As Object) As Integer
        Dim Usuario As New UsuarioENT
        Dim HT As New Hashtable
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        HT.Add("@pID", Usuario.ID)
        HT.Add("@pDNI", Usuario.DNI)
        HT.Add("@pCUIT", Usuario.CUIT)
        HT.Add("@pNombre", Usuario.Nombre)
        HT.Add("@pApellido", Usuario.Apellido)
        HT.Add("@pCorreoElectronico", Usuario.CorreoElectronico)
        HT.Add("@pDomicilio", Usuario.Domicilio)
        HT.Add("@pLocalidad", Usuario.Localidad)
        HT.Add("@pProvincia", Usuario.Provincia)
        HT.Add("@pTelefono", Usuario.Telefono)
        HT.Add("@pInterno", Usuario.Interno)
        HT.Add("@pTelefonoCelular", Usuario.TelefonoCelular)
        HT.Add("@pContraseña", Usuario.Contraseña)
        HT.Add("@pIntentosFallidos", Usuario.IntentosFallidos)
        HT.Add("@pBloqueado", Usuario.Bloqueado)
        HT.Add("@pValidado", Usuario.Validado)
        HT.Add("@pBaja", Usuario.Baja)
        Return Generico.Escribir("pModificarUsuario", HT)
    End Function

    Shared Sub ModificarContraseña(ByRef QueObjeto As Object)
        Dim Usuario As New UsuarioENT
        Dim HT As New Hashtable
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        HT.Add("@pID", Usuario.ID)
        HT.Add("@pContrasena", Usuario.Contraseña)
        Generico.Escribir("pCambiarPass", HT)
    End Sub

    Shared Function GenerarCodigoContrasena(CorreoElectronico As String, Codigo As Integer) As Integer
        Dim HT As New Hashtable
        HT.Add("@pCorreoElectronico", CorreoElectronico)
        HT.Add("@pCodigo", Codigo)
        Return Generico.Escribir("pGenerarCodigoRecupero", HT)
    End Function

    Shared Function RecuperarContraseña(Codigo As Integer, CorreoElectronico As String, Contrasena As String) As Integer
        Dim HT As New Hashtable
        HT.Add("@pCodigo", Codigo)
        HT.Add("@pCorreoElectronico", CorreoElectronico)
        HT.Add("@pContrasena", Contrasena)
        Return Generico.Escribir("pRecuperarContraseña", HT)
    End Function

    Shared Function NuevoUsuario(ByRef QueObjeto As Object, ByRef QueRol As Object) As Integer
        Dim Usuario As New UsuarioENT
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        Dim Rol As New RolENT
        Rol = DirectCast(QueRol, RolENT)
        Dim HT As New Hashtable
        HT.Add("@pDNI", Usuario.DNI)
        HT.Add("@pCUIT", Usuario.CUIT)
        HT.Add("@pNombre", Usuario.Nombre)
        HT.Add("@pApellido", Usuario.Apellido)
        HT.Add("@pCorreoElectronico", Usuario.CorreoElectronico)
        HT.Add("@pDomicilio", Usuario.Domicilio)
        HT.Add("@pLocalidad", Usuario.Localidad)
        HT.Add("@pProvincia", Usuario.Provincia)
        HT.Add("@pTelefono", Usuario.Telefono)
        HT.Add("@pInterno", Usuario.Interno)
        HT.Add("@pTelefonoCelular", Usuario.TelefonoCelular)
        HT.Add("@pContraseña", Usuario.Contraseña)
        HT.Add("@pCodigoRol", Rol.Codigo)
        Return Generico.Escribir("pNuevoUsuario", HT)
    End Function

    Shared Function ValidarCuenta(CorreoElectronico As String) As Integer
        Dim HT As New Hashtable
        HT.Add("@pCorreoElectronico", CorreoElectronico)
        Return Generico.Escribir("pValidarCuenta", HT)
    End Function

    Shared Function ListarCorreosElectronicos() As DataSet
        Return Generico.Leer("pListarCorreosElectronicos")
    End Function

    Shared Function ListarUsuarios() As DataSet
        Return Generico.Leer("pListarUsuarios")
    End Function

    Shared Function EliminarUsuario(ByRef QueObjeto As Object) As Integer
        Dim Usuario As New UsuarioENT
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Usuario.ID)
        Return Generico.Escribir("pEliminarUsuario", HT)
    End Function

    Shared Sub AgregarUsuarioRol(ByRef QueObjeto As Object)
        Dim Usuario As New UsuarioENT
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Usuario.ID)
        For Each RolENT In Usuario.ListaRoles
            HT.Remove("@pCodigoRol")
            HT.Add("@pCodigoRol", RolENT.Codigo)
            Generico.Escribir("pAgregarUsuarioRol", HT)
        Next
    End Sub

    Shared Sub BorrarUsuarioRol(ByRef QueObjeto As Object)
        Dim Usuario As New UsuarioENT
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Usuario.ID)
        Generico.Escribir("pBorrarUsuarioRol", HT)
    End Sub

    Shared Function ListarUsuariosRoles(ByRef QueObjeto As Object) As DataSet
        Dim Usuario As New UsuarioENT
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Usuario.ID)
        Return Generico.Leer("pListarUsuariosRoles", HT)
    End Function

    Shared Function ListarUsuarioPorId(ByRef QueObjeto As Object) As DataSet
        Dim Usuario As New UsuarioENT
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Usuario.ID)
        Return Generico.Leer("pListarUsuarioPorId", HT)
    End Function

End Class

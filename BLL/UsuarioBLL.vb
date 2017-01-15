Imports System.Security.Cryptography
Imports System.Text
Imports ENTITIES
Imports MAPPER

Public Class UsuarioBLL
    Dim UsuarioENT As UsuarioENT

    Public Function Encriptar(ByVal md5Hash As MD5, ByVal pass As String) As String
        Try
            md5Hash = MD5.Create
            Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(pass))
            Dim sBuilder As New StringBuilder()
            Dim i As Integer
            For i = 0 To data.Length - 1
                sBuilder.Append(data(i).ToString("x2"))
            Next i
            Return sBuilder.ToString()
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function TestEncoding(Contraseña As String, Id As String) As String
        Dim wrapper As New EncriptacionReversibleBLL(Id)
        Dim cipherText As String = wrapper.EncryptData(Contraseña)
        Return cipherText
    End Function

    Public Function TestDecoding(Contraseña As String, Id As String) As String
        Dim wrapper As New EncriptacionReversibleBLL(Id)
        Dim plainText As String = wrapper.DecryptData(Contraseña)
        Return plainText
    End Function

    Public Function IniciarSesion(ByRef QueObjeto As Object, Contraseña As String) As Integer
        'Flag = 0 -->Usuario inexistente
        'Flag = 1 -->Inicio correcto
        'Flag = 2 -->Usuario OK /\ Pass Err
        'Flag = 3 -->Usuario Bloqueado
        'Flag = 4 -->Bloquear usuario
        Dim Flag As Integer = 0
        Dim lector As IDataReader = UsuarioMAP.ConsultaLogIn(QueObjeto).CreateDataReader
        Do While lector.Read()
            UsuarioENT = New UsuarioENT
            With UsuarioENT
                .ID = Convert.ToInt32(lector("Id"))
                .DNI = Convert.ToInt32(lector("DNI"))
                .CUIT = Convert.ToString(lector("CUIT"))
                '.Nombre = Convert.ToString(lector("Nombre"))
                .Nombre = DirectCast(lector("Nombre"), String)
                .Apellido = Convert.ToString(lector("Apellido"))
                .CorreoElectronico = Convert.ToString(lector("CorreoElectronico"))
                .Domicilio = Convert.ToString(lector("Domicilio"))
                .Localidad = Convert.ToString(lector("Localidad"))
                .Provincia = Convert.ToString(lector("Provincia"))
                .Telefono = Convert.ToString(lector("Telefono"))
                .Interno = Convert.ToString(lector("Interno"))
                .TelefonoCelular = Convert.ToString(lector("TelefonoCelular"))
                .Contraseña = Convert.ToString(lector("Contraseña"))
                .IntentosFallidos = Convert.ToInt32(lector("IntentosFallidos"))
                .Bloqueado = Convert.ToBoolean(lector("Bloqueado"))
                .Validado = Convert.ToBoolean(lector("Validado"))
                .Baja = Convert.ToBoolean(lector("Baja"))
            End With
        Loop
        lector.Close()
        For i = 0 To 1
            If UsuarioENT Is Nothing = True Then
                Flag = 0
                Exit For
            ElseIf UsuarioENT.Baja = True Then
                Flag = 0
                Exit For
            ElseIf UsuarioENT.Bloqueado = True Then
                Flag = 3
                Exit For
                'ElseIf Encriptar(MD5.Create, Contraseña) <> UsuarioENT.Contraseña Then
            ElseIf Contraseña <> TestDecoding(UsuarioENT.Contraseña, UsuarioENT.ID) Then
                UsuarioENT.IntentosFallidos -= 1
                If UsuarioENT.IntentosFallidos = -1 Then
                    UsuarioENT.Bloqueado = True
                    Flag = 4
                    UsuarioMAP.Modificacion(UsuarioENT)
                    Exit For
                Else
                    Flag = 2
                    UsuarioMAP.Modificacion(UsuarioENT)
                    Exit For
                End If
            ElseIf Contraseña = TestDecoding(UsuarioENT.Contraseña, UsuarioENT.ID) Then
                If UsuarioENT.Validado = True Then
                    Flag = 1
                    UsuarioENT.IntentosFallidos = 3
                    UsuarioMAP.Modificacion(UsuarioENT)
                    Exit For
                Else
                    Flag = 5
                    Exit For
                End If
            End If
        Next
        Return Flag
    End Function

    Public Function QueUsuario(ByRef QueObjeto As Object) As UsuarioENT
        Dim lector As IDataReader = UsuarioMAP.ConsultaLogIn(QueObjeto).CreateDataReader
        Do While lector.Read()
            UsuarioENT = New UsuarioENT
            With UsuarioENT
                .ID = Convert.ToInt32(lector("Id"))
                .DNI = Convert.ToInt32(lector("DNI"))
                .CUIT = Convert.ToString(lector("CUIT"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .Apellido = Convert.ToString(lector("Apellido"))
                .CorreoElectronico = Convert.ToString(lector("CorreoElectronico"))
                .Domicilio = Convert.ToString(lector("Domicilio"))
                .Localidad = Convert.ToString(lector("Localidad"))
                .Provincia = Convert.ToString(lector("Provincia"))
                .Telefono = Convert.ToString(lector("Telefono"))
                .Interno = Convert.ToString(lector("Interno"))
                .TelefonoCelular = Convert.ToString(lector("TelefonoCelular"))
                .Contraseña = Convert.ToString(lector("Contraseña"))
                .IntentosFallidos = Convert.ToInt32(lector("IntentosFallidos"))
                .Bloqueado = Convert.ToBoolean(lector("Bloqueado"))
                .Validado = Convert.ToBoolean(lector("Validado"))
                .Baja = Convert.ToBoolean(lector("Baja"))
            End With
        Loop
        lector.Close()
        Return UsuarioENT
    End Function

    Public Function ModificarUsuario(ByRef QueObjeto As Object) As Integer
        Try
            Return UsuarioMAP.Modificacion(QueObjeto)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ModificarContraseña(QueObjeto As Object, PassActual As String, PassNueva As String, PassNuevaRepetida As String) As Integer
        'Flag = 0 --> Contraseña actual inválida
        'Flag = 1 --> Se ha modificado la contraseña
        'Flag = 2 --> No coinciden las contraseñas
        Dim UsuarioENT As New UsuarioENT
        UsuarioENT = DirectCast(QueObjeto, UsuarioENT)
        Dim Flag As Integer = 0
        If TestEncoding(PassActual, UsuarioENT.ID) = QueUsuario(UsuarioENT).Contraseña Then
            If PassNueva = PassNuevaRepetida Then
                UsuarioENT.Contraseña = TestEncoding(PassNueva, UsuarioENT.ID)
                UsuarioMAP.ModificarContraseña(UsuarioENT)
                Flag = 1
            Else
                Flag = 2
            End If
        Else
            Flag = 0
        End If
        Return Flag
    End Function

    Public Function GenerarCodigoContrasena(CorreoElectronico As String, Codigo As Integer) As Integer
        Return UsuarioMAP.GenerarCodigoContrasena(CorreoElectronico, Codigo)
    End Function

    Public Function RecuperarContraseña(Codigo As Integer, CorreoElectronico As String, Contrasena As String, ContrasenaRepetida As String) As Integer
        'Flag = 0 --> Codigo o email invalido
        'Flag = 1 --> Se ha modificado la contraseña
        'Flag = 2 --> No coinciden las contraseñas
        Dim Flag As Integer = 0
        Dim Retorno As Integer = 0
        If Contrasena = ContrasenaRepetida Then
            Dim UsuarioENT As New UsuarioENT
            UsuarioENT.CorreoElectronico = CorreoElectronico
            Retorno = UsuarioMAP.RecuperarContraseña(Codigo, CorreoElectronico, TestEncoding(Contrasena, QueUsuario(UsuarioENT).ID))
            'Retorno = UsuarioMAP.RecuperarContraseña(Codigo, CorreoElectronico, Encriptar(MD5.Create, Contrasena))
            If Retorno > 0 Then
                Flag = 1
            Else
                Flag = 0
            End If
        Else
            Flag = 2
        End If
        Return Flag
    End Function

    Public Function NuevoUsuario(ByRef QueObjeto As Object, ByRef QueRol As Object) As Integer
        Dim Flag = 0
        Dim Usuario As New UsuarioENT
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        'Dim ContraseñaAux As String = ""
        'ContraseñaAux = Encriptar(MD5.Create, Usuario.Contraseña)
        'Usuario.Contraseña = ContraseñaAux
        If UsuarioMAP.NuevoUsuario(Usuario, QueRol) > 0 Then
            Dim UsuarioAUX As New UsuarioENT
            UsuarioAUX = QueUsuario(Usuario)
            Dim UsuarioENT As New UsuarioENT
            With UsuarioENT
                .ID = UsuarioAUX.ID
                .Apellido = UsuarioAUX.Apellido
                .Baja = UsuarioAUX.Baja
                .Bloqueado = UsuarioAUX.Bloqueado
                .CorreoElectronico = UsuarioAUX.CorreoElectronico
                .CUIT = UsuarioAUX.CUIT
                .DNI = UsuarioAUX.DNI
                .Domicilio = UsuarioAUX.Domicilio
                .IntentosFallidos = UsuarioAUX.IntentosFallidos
                .Interno = UsuarioAUX.Interno
                .Localidad = UsuarioAUX.Localidad
                .Provincia = UsuarioAUX.Provincia
                .Nombre = UsuarioAUX.Nombre
                .Telefono = UsuarioAUX.Telefono
                .Validado = UsuarioAUX.Validado
                .TelefonoCelular = UsuarioAUX.TelefonoCelular
                .Contraseña = TestEncoding(UsuarioAUX.Contraseña, UsuarioAUX.ID)
            End With
            ModificarUsuario(UsuarioENT)
            Flag = 1
        Else
            Flag = 0
        End If
        Return Flag
    End Function

    Public Function ValidarCuenta(CorreoElectronico As String) As Integer
        Return UsuarioMAP.ValidarCuenta(CorreoElectronico)
    End Function

    Public Function ListarCorreosElectronicos() As List(Of String)
        Dim ListaCorreosElectronicos As New List(Of String)
        Dim lector As IDataReader = UsuarioMAP.ListarCorreosElectronicos.CreateDataReader
        Do While lector.Read()
            Dim CorreoElectronico As String = Convert.ToString(lector("CorreoElectronico"))
            ListaCorreosElectronicos.Add(CorreoElectronico)
        Loop
        lector.Close()
        Return ListaCorreosElectronicos
    End Function

    Public Function DesencriptarCorreoElectronico(CadenaEncriptada As String) As String
        Try
            Dim CorreoElectronico As String = ""
            For Each item As String In ListarCorreosElectronicos()
                Dim hashOfInput As String = Encriptar(MD5.Create, item)
                Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase
                If 0 = comparer.Compare(hashOfInput, CadenaEncriptada) Then
                    CorreoElectronico = item
                End If
            Next
            Return CorreoElectronico
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ListarUsuarios() As List(Of UsuarioENT)
        Dim ListaUsuarios As New List(Of UsuarioENT)
        Dim lector As IDataReader = UsuarioMAP.ListarUsuarios.CreateDataReader
        Do While lector.Read()
            UsuarioENT = New UsuarioENT
            With UsuarioENT
                .ID = Convert.ToInt32(lector("Id"))
                .DNI = Convert.ToInt32(lector("DNI"))
                .CUIT = Convert.ToString(lector("CUIT"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .Apellido = Convert.ToString(lector("Apellido"))
                .CorreoElectronico = Convert.ToString(lector("CorreoElectronico"))
                .Domicilio = Convert.ToString(lector("Domicilio"))
                .Localidad = Convert.ToString(lector("Localidad"))
                .Provincia = Convert.ToString(lector("Provincia"))
                .Telefono = Convert.ToString(lector("Telefono"))
                .Interno = Convert.ToString(lector("Interno"))
                .TelefonoCelular = Convert.ToString(lector("TelefonoCelular"))
                .Contraseña = Convert.ToString(lector("Contraseña"))
                .IntentosFallidos = Convert.ToInt32(lector("IntentosFallidos"))
                .Bloqueado = Convert.ToBoolean(lector("Bloqueado"))
                .Validado = Convert.ToBoolean(lector("Validado"))
                .Baja = Convert.ToBoolean(lector("Baja"))
            End With
            ListaUsuarios.Add(UsuarioENT)
        Loop
        lector.Close()
        Return ListaUsuarios
    End Function

    Public Function EliminarUsuario(ByRef QueObjeto As Object) As Integer
        Return UsuarioMAP.EliminarUsuario(QueObjeto)
    End Function

    Public Sub AgregarUsuarioRol(ByRef QueObjeto As Object)
        UsuarioMAP.AgregarUsuarioRol(QueObjeto)
    End Sub

    Public Sub BorrarUsuarioRol(ByRef QueObjeto As Object)
        UsuarioMAP.BorrarUsuarioRol(QueObjeto)
    End Sub

    Public Function ListarUsuariosRoles(ByRef QueObjeto As Object) As UsuarioENT
        Dim lector As IDataReader = UsuarioMAP.ListarUsuariosRoles(QueObjeto).CreateDataReader
        Dim Usuario As New UsuarioENT
        Usuario.ID = DirectCast(QueObjeto, UsuarioENT).ID
        Do While lector.Read()
            Dim Rol As New RolENT
            Rol.Codigo = Convert.ToString(lector("CodigoRol"))
            Usuario.ListaRoles.Add(Rol)
        Loop
        lector.Close()
        Return Usuario
    End Function

    Public Function ListarUsuarioPorId(ByRef QueObjeto As Object) As List(Of UsuarioENT)
        Dim ListaUsuarios As New List(Of UsuarioENT)
        Dim lector As IDataReader = UsuarioMAP.ListarUsuarioPorId(QueObjeto).CreateDataReader
        Do While lector.Read()
            UsuarioENT = New UsuarioENT
            With UsuarioENT
                .ID = Convert.ToInt32(lector("Id"))
                .DNI = Convert.ToInt32(lector("DNI"))
                .CUIT = Convert.ToString(lector("CUIT"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .Apellido = Convert.ToString(lector("Apellido"))
                .CorreoElectronico = Convert.ToString(lector("CorreoElectronico"))
                .Domicilio = Convert.ToString(lector("Domicilio"))
                .Localidad = Convert.ToString(lector("Localidad"))
                .Provincia = Convert.ToString(lector("Provincia"))
                .Telefono = Convert.ToString(lector("Telefono"))
                .Interno = Convert.ToString(lector("Interno"))
                .TelefonoCelular = Convert.ToString(lector("TelefonoCelular"))
                .Contraseña = Convert.ToString(lector("Contraseña"))
                .IntentosFallidos = Convert.ToInt32(lector("IntentosFallidos"))
                .Bloqueado = Convert.ToBoolean(lector("Bloqueado"))
                .Validado = Convert.ToBoolean(lector("Validado"))
                .Baja = Convert.ToBoolean(lector("Baja"))
            End With
            ListaUsuarios.Add(UsuarioENT)
        Loop
        lector.Close()
        Return ListaUsuarios
    End Function

End Class

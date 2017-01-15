Imports BLL
Imports ENTITIES
Imports System.Security.Cryptography
Imports System.Globalization

Public Class Validacion
    Inherits System.Web.UI.Page
    Dim UsuarioV As New UsuarioVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divValidacionControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(UsuarioV.UsuarioBLL.DesencriptarCorreoElectronico(Request.QueryString("mail")), _
                                                                                    "Validó su cuenta"))
            UsuarioV.UsuarioBLL.ValidarCuenta(UsuarioV.UsuarioBLL.DesencriptarCorreoElectronico(Request.QueryString("mail")))
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

End Class
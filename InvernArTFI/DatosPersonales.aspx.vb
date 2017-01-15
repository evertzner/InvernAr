Imports ENTITIES
Imports BLL
Imports System.Net.Mail
Imports System.IO
Imports System.Globalization

Public Class DatosPersonales
    Inherits System.Web.UI.Page
    Dim UsuarioV As New UsuarioVista
    Dim UsuarioMail As New UsuarioENT
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack And Not Session("Usuario") Is Nothing Then
                CargarFormulario()
            End If
            txtDatosPersonalesCorreoElectronico.Attributes.Remove("readonly")
            txtDatosPersonalesCorreoElectronico.Attributes.Add("readonly", "true")
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Public Sub CargarFormulario()
        Traduccion.TraducirControles(divDatosPersonalesControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
        Dim Usuario As New UsuarioENT
        Usuario = DirectCast(Session("Usuario"), UsuarioENT)
        For Each c As Control In divDatosPersonalesControles.Controls
            If TypeOf (c) Is HtmlInputText Then
                Select Case c.ID
                    Case "txtDatosPersonalesDNI"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.DNI)
                    Case "txtDatosPersonalesCUIT"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.CUIT)
                    Case "txtDatosPersonalesNombre"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.Nombre)
                    Case "txtDatosPersonalesApellido"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.Apellido)
                    Case "txtDatosPersonalesCorreoElectronico"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.CorreoElectronico)
                    Case "txtDatosPersonalesDomicilio"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.Domicilio)
                    Case "txtDatosPersonalesLocalidad"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.Localidad)
                    Case "txtDatosPersonalesProvincia"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.Provincia)
                    Case "txtDatosPersonalesTelefono"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.Telefono)
                    Case "txtDatosPersonalesInterno"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.Interno)
                    Case "txtDatosPersonalesTelefonoCelular"
                        DirectCast(c, HtmlInputText).Attributes.Add("value", Usuario.TelefonoCelular)
                End Select
            End If
        Next
    End Sub

    Private Sub btnDatosPersonalesModificar_ServerClick(sender As Object, e As EventArgs) Handles btnDatosPersonalesModificar.ServerClick
        Try
            Dim Usuario As New UsuarioENT
            Usuario = DirectCast(Session("Usuario"), UsuarioENT)
            Usuario.Nombre = txtDatosPersonalesNombre.Value
            Usuario.Apellido = txtDatosPersonalesApellido.Value
            Usuario.Domicilio = txtDatosPersonalesDomicilio.Value
            Usuario.DNI = txtDatosPersonalesDNI.Value
            Usuario.CUIT = txtDatosPersonalesCUIT.Value
            Usuario.Localidad = txtDatosPersonalesLocalidad.Value
            Usuario.CorreoElectronico = txtDatosPersonalesCorreoElectronico.Value
            Usuario.Interno = txtDatosPersonalesInterno.Value
            Usuario.Telefono = txtDatosPersonalesTelefono.Value
            Usuario.TelefonoCelular = txtDatosPersonalesTelefonoCelular.Value
            UsuarioV.UsuarioBLL.ModificarUsuario(Usuario)
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(Usuario.CorreoElectronico, "Cambió sus datos personales"))
            Response.Redirect("DatosPersonales.aspx")
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnDatosPersonalesCambiarContrasena_ServerClick(sender As Object, e As EventArgs) Handles btnDatosPersonalesCambiarContrasena.ServerClick
        Try
            Dim Flag As Integer = 0
            Dim Usuario As New UsuarioENT
            Usuario = DirectCast(Session("Usuario"), UsuarioENT)
            Flag = UsuarioV.UsuarioBLL.ModificarContraseña(Usuario, txtDatosPersonalesPassActual.Value, txtDatosPersonalesPassNueva.Value, _
                                                           txtDatosPersonalesPassNuevaRepetida.Value)
            If Flag = 1 Then
                divDatosPersonalesAlerta.Visible = True
                divDatosPersonalesAlerta.Attributes.Add("class", "alert alert-success")
                Traduccion.TraducirAlertas(divDatosPersonalesAlerta, divDatosPersonalesAlerta.ID & Flag, DirectCast(Session("Idioma"), CultureInfo))
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(Usuario.CorreoElectronico, "Cambió su contraseña"))
                'divDatosPersonalesAlerta.InnerText = "Contraseña cambiada satisftactoriamente"
                EnviarMail()
            ElseIf Flag = 0 Then
                divDatosPersonalesAlerta.Visible = True
                divDatosPersonalesAlerta.Attributes.Add("class", "alert alert-danger")
                Traduccion.TraducirAlertas(divDatosPersonalesAlerta, divDatosPersonalesAlerta.ID & Flag, DirectCast(Session("Idioma"), CultureInfo))
                'divDatosPersonalesAlerta.InnerText = "Contraseña actual incorrecta"
            ElseIf Flag = 2 Then
                divDatosPersonalesAlerta.Visible = True
                divDatosPersonalesAlerta.Attributes.Add("class", "alert alert-danger")
                Traduccion.TraducirAlertas(divDatosPersonalesAlerta, divDatosPersonalesAlerta.ID & Flag, DirectCast(Session("Idioma"), CultureInfo))
                'divDatosPersonalesAlerta.InnerText = "Las contraseñas nuevas no coinciden"
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub EnviarMail()
        UsuarioMail = DirectCast(Session("Usuario"), UsuarioENT)
        Dim Cliente As New SmtpClient()
        Dim VistaAlternativa As AlternateView
        VistaAlternativa = AlternateView.CreateAlternateViewFromString(PopulateBody(UsuarioMail.Nombre), Nothing, System.Net.Mime.MediaTypeNames.Text.Html)
        Dim Mensaje As New MailMessage()
        Dim Imagen As LinkedResource = New LinkedResource("C:\Users\Esteban\Documents\Visual Studio 2012\Projects\InvernArTFI\InvernArTFI\images\LogoWeb.png", _
                                                          System.Net.Mime.MediaTypeNames.Image.Jpeg)
        Mensaje.To.Add(UsuarioMail.CorreoElectronico)
        Mensaje.Subject = "Cambio de contraseña"
        Mensaje.IsBodyHtml = True
        Imagen.ContentId = "Pic1"
        VistaAlternativa.LinkedResources.Add(Imagen)
        Mensaje.AlternateViews.Add(VistaAlternativa)
        Try
            Cliente.Send(Mensaje)
        Catch ex As Exception
            ' ...
        End Try
    End Sub

    Private Function PopulateBody(Nombre As String) As String
        Dim body As String = String.Empty
        Dim reader As StreamReader = New StreamReader(Server.MapPath("~\Cuerpos de mail\CambioContrasena.html"))
        body = reader.ReadToEnd
        body = body.Replace("{UserName}", Nombre)
        Return body
    End Function

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

End Class
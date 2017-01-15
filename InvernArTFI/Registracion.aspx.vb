Imports BLL
Imports ENTITIES
Imports System.IO
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.Globalization

Public Class Registracion
    Inherits System.Web.UI.Page
    Dim UsuarioV As New UsuarioVista
    Dim BitacoraV As New BitacoraVista
    Dim RolV As New RolVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divRegistracionControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            divRegistracionAlerta.Visible = False
            If Not IsPostBack Then
                Session("AceptaTC") = False
                btnRegistracionConfirmar.Disabled = True
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnRegistracionConfirmar_ServerClick(sender As Object, e As EventArgs) Handles btnRegistracionConfirmar.ServerClick
        Try
            Dim CampoVacio As Boolean = False
            For Each c As Control In divRegistracionControles.Controls
                If TypeOf (c) Is HtmlInputText Then
                    If DirectCast(c, HtmlInputText).Value = "" Then
                        If DirectCast(c, HtmlInputText).ID = "txtRegistracionInterno" Or DirectCast(c, HtmlInputText).ID = "txtRegistracionTelefonoCelular" Then
                            CampoVacio = False
                            Exit For
                        Else
                            CampoVacio = True
                            Exit For
                        End If
                    End If
                End If
            Next
            If CampoVacio = False Then
                If txtRegistracionCorreoElectronico.Value = txtRegistracionCorreoElectronicoRepetido.Value Then
                    RolV.RolENT.Codigo = "UFC"
                    If txtRegistracionPassNueva.Value = txtRegistracionPassNuevaRepetida.Value Then
                        With UsuarioV.UsuarioENT
                            .DNI = txtRegistracionDNI.Value
                            .CUIT = txtRegistracionCUIT.Value
                            .Nombre = txtRegistracionNombre.Value
                            .Apellido = txtRegistracionApellido.Value
                            .CorreoElectronico = txtRegistracionCorreoElectronico.Value
                            .Domicilio = txtRegistracionDomicilio.Value
                            .Localidad = txtRegistracionLocalidad.Value
                            .Provincia = txtRegistracionProvincia.Value
                            .Telefono = txtRegistracionTelefono.Value
                            .Interno = txtRegistracionInterno.Value
                            .TelefonoCelular = txtRegistracionTelefonoCelular.Value
                            .Contraseña = txtRegistracionPassNueva.Value
                        End With
                        If UsuarioV.UsuarioBLL.NuevoUsuario(UsuarioV.UsuarioENT, RolV.RolENT) > 0 Then
                            divRegistracionAlerta.Visible = True
                            divRegistracionAlerta.Attributes.Add("class", "alert alert-success")
                            Traduccion.TraducirAlertas(divRegistracionAlerta, divRegistracionAlerta.ID & "1", DirectCast(Session("Idioma"), CultureInfo))
                            'divRegistracionAlerta.InnerText = "Se ha enviado un mail de confirmación, por favor entre al link dentro del mail para validar su cuenta"
                            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(txtRegistracionCorreoElectronico.Value, _
                                                                                                    "Se registró el usuario " & _
                                                                                                    txtRegistracionCorreoElectronico.Value))
                            EnviarMail(UsuarioV.UsuarioENT.CorreoElectronico, UsuarioV.UsuarioENT.Nombre)
                            Response.Redirect("Inicio.aspx")
                        Else
                            divRegistracionAlerta.Visible = True
                            divRegistracionAlerta.Attributes.Add("class", "alert alert-danger")
                            Traduccion.TraducirAlertas(divRegistracionAlerta, divRegistracionAlerta.ID & "0", DirectCast(Session("Idioma"), CultureInfo))
                            'divRegistracionAlerta.InnerText = "El correo electrónico ya pertenece a un usuario registrado"
                        End If
                    Else
                        divRegistracionAlerta.Visible = True
                        divRegistracionAlerta.Attributes.Add("class", "alert alert-danger")
                        Traduccion.TraducirAlertas(divRegistracionAlerta, divRegistracionAlerta.ID & "2", DirectCast(Session("Idioma"), CultureInfo))
                        'divRegistracionAlerta.InnerText = "Las contraseñas son diferentes"
                    End If
                Else
                    divRegistracionAlerta.Visible = True
                    divRegistracionAlerta.Attributes.Add("class", "alert alert-danger")
                    Traduccion.TraducirAlertas(divRegistracionAlerta, divRegistracionAlerta.ID & "3", DirectCast(Session("Idioma"), CultureInfo))
                    'divRegistracionAlerta.InnerText = "Asegúrese de que confirmó bien el correo electrónico"
                End If
            Else
                divRegistracionAlerta.Visible = True
                divRegistracionAlerta.Attributes.Add("class", "alert alert-danger")
                Traduccion.TraducirAlertas(divRegistracionAlerta, divRegistracionAlerta.ID & "4", DirectCast(Session("Idioma"), CultureInfo))
                'divRegistracionAlerta.InnerText = "No pueden haber campos vacíos"
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub EnviarMail(CorreoElectronico As String, Nombre As String)
        Dim Cliente As New SmtpClient()
        Dim VistaAlternativa As AlternateView
        VistaAlternativa = AlternateView.CreateAlternateViewFromString(PopulateBody(Nombre, UsuarioV.UsuarioBLL.Encriptar(MD5.Create, CorreoElectronico)), _
                                                                       Nothing, System.Net.Mime.MediaTypeNames.Text.Html)
        Dim Mensaje As New MailMessage()
        Dim Imagen As LinkedResource = New LinkedResource("C:\Users\Esteban\Documents\Visual Studio 2012\Projects\InvernArTFI\InvernArTFI\images\LogoWeb.png", _
                                                          System.Net.Mime.MediaTypeNames.Image.Jpeg)
        Mensaje.To.Add(CorreoElectronico)
        Mensaje.Subject = "Bienvenido"
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

    Private Function PopulateBody(Nombre As String, CorreoElectronico As String) As String
        Dim body As String = String.Empty
        Dim Link As String = "http://localhost:1698/Validacion.aspx?mail=" & CorreoElectronico
        Dim reader As StreamReader = New StreamReader(Server.MapPath("~\Cuerpos de mail\NuevoUsuario.html"))
        body = reader.ReadToEnd
        body = body.Replace("{UserName}", Nombre)
        body = body.Replace("{Link}", "<a href=""" & Link & """>" & Link & "</a>")
        Return body
    End Function

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

End Class
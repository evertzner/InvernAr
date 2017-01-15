Imports ENTITIES
Imports BLL
Imports System.IO
Imports System.Net.Mail
Imports System.Threading
Imports System.Globalization
Imports SelectPdf

Public Class Master
    Inherits System.Web.UI.MasterPage
    Dim UsuarioV As New UsuarioVista
    Dim PermisoV As New PermisoVista
    Dim UsuarioMail As New UsuarioENT
    Dim BitacoraV As New BitacoraVista
    Dim MultidiomaV As New MultidiomaVista
    Dim CategoriaNewsletterV As New CategoriaNewsletterVista
    Dim ListaIdiomasTraducidos As New List(Of MultidiomaENT)
    Dim ChatV As New ChatVista
    Dim NewsletterV As New NewsletterVista
    Dim PDF As Stream

    Private Sub Main_Init(sender As Object, e As EventArgs) Handles Main.Init
        Try
            Dim Lista As New List(Of PermisoENT)
            Lista = DirectCast(Session("Permisos"), List(Of PermisoENT))
            Dim AceptarIngreso As Boolean = False
            If Not Lista Is Nothing Then
                For Each permiso In Lista
                    If AceptarIngreso = False Then
                        For Each c As Control In ulMasterControlesMenu.Controls
                            If TypeOf (c) Is HtmlAnchor Then
                                'If permiso.Codigo = c.ID And "/invernar/" & DirectCast(c, HtmlAnchor).HRef.ToLower = Request.Url.LocalPath.ToLower Then
                                If permiso.Codigo = c.ID And "/" & DirectCast(c, HtmlAnchor).HRef.ToLower = Request.Url.LocalPath.ToLower Then
                                    AceptarIngreso = True
                                    Exit For
                                Else
                                    AceptarIngreso = False
                                End If
                            End If
                        Next
                    Else
                        Exit For
                    End If
                Next
                If Not IsPostBack And AceptarIngreso = False Then
                    Response.Redirect("Inicio.aspx")
                End If
            End If
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Main.Load
        Try
            PD.Visible = False
            If Session("UFC") = False Then
                aMasterCarrito.Visible = False
                lnkMasterCompras.Visible = False
                separatorMasterCompras.Visible = False
                lnkMasterCuentaCorriente.Visible = False
                lnkMasterInformacionSensores.Visible = False
                separatorMasterInformacionSensores.Visible = False
            Else
                lnkMasterInformacionSensores.Visible = True
                separatorMasterInformacionSensores.Visible = True
                aMasterCarrito.Visible = True
            End If
            If Session("Carrito") IsNot Nothing Then
                lnkMasterCompras.Visible = True
                separatorMasterCompras.Visible = True
                lnkMasterCuentaCorriente.Visible = True
                ConfigurarCarrito()
            Else
                carrito.Style.Remove("color")
                carrito.Style.Add("color", "white")
            End If
            If Not IsPostBack And Session("Usuario") Is Nothing Then
                aMasterChat.Visible = False
            End If
            divMasterAlerta.Visible = False
            If Session("Usuario") Is Nothing Then
                Session("Permisos") = PermisoV.PermisoBLL.QuePermisos(Nothing)
            End If
            If Not IsPostBack And Not Session("Usuario") Is Nothing Then
                CargarMenu(False)
            ElseIf Not IsPostBack And Not Session("Permisos") Is Nothing Then
                CargarMenu(True)

            End If
            If Session("IndiceIdioma") Is Nothing Then
                Session("Idioma") = Thread.CurrentThread.CurrentUICulture
            End If
            If IsPostBack Then

                ListaIdiomasTraducidos = DirectCast(Session("IdiomasTraducidos"), List(Of MultidiomaENT))
                'Session("Idioma") = ListaIdiomasTraducidos.Item(ddlMasterIdiomas.SelectedIndex).Idioma
            End If
            If Not Session("Usuario") Is Nothing Then
                txtMasterEmail.Visible = False
                txtMasterPassword.Visible = False
                btnMasterIniciarSesion.Visible = False
                btnMasterRegistrarse.Visible = False
                btnMasterOlvidoContrasena.Visible = False
                aMasterChat.Visible = True
            End If
            If Not IsPostBack Then
                Traducir()
                Session("NewsletterCategoriasMaster") = CategoriaNewsletterV.CategoriaNewsletterBLL.ListarCategoriasNewsletter
                ddlMasterCategoria.Items.Clear()
                For Each item In DirectCast(Session("NewsletterCategoriasMaster"), List(Of CategoriaNewsletterENT))
                    ddlMasterCategoria.Items.Add(item.Categoria)
                Next
                ddlMasterCategoria.Items.Insert(0, "")
                ddlMasterCategoria.Items.Insert(1, "Todo")
            End If
            'Traducir()
            OcultarLinks()
            revCorreoElectronicoNewsletter.ErrorMessage = Traduccion.TraducirMensaje(revCorreoElectronico.ID, DirectCast(Session("Idioma"), CultureInfo))
            revCorreoElectronicoRC.ErrorMessage = Traduccion.TraducirMensaje(revCorreoElectronicoRC.ID, DirectCast(Session("Idioma"), CultureInfo))
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub Traducir()
        Dim MultidiomaENT As New MultidiomaENT
        MultidiomaENT.Idioma = Session("Idioma")
        If MultidiomaENT.Idioma.Parent.Name = "" Then
            ddlMasterIdiomas.SelectedValue = MultidiomaENT.Idioma.DisplayName
        Else
            ddlMasterIdiomas.SelectedValue = MultidiomaENT.Idioma.Parent.DisplayName
        End If
        Traduccion.TraducirControles(divMasterControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
        Traduccion.TraducirControles(ulMasterControlesMenu.Controls, DirectCast(Session("Idioma"), CultureInfo))
    End Sub

    Private Sub btnMasterIniciarSesion_ServerClick(sender As Object, e As EventArgs) Handles btnMasterIniciarSesion.ServerClick
        Try
            UsuarioV.UsuarioENT.CorreoElectronico = txtMasterEmail.Value
            Dim Flag As Integer = 0
            Flag = UsuarioV.UsuarioBLL.IniciarSesion(UsuarioV.UsuarioENT, txtMasterPassword.Value)
            If Flag = 1 Then
                Dim UsuarioAux As New UsuarioENT
                UsuarioAux = UsuarioV.UsuarioBLL.QueUsuario(UsuarioV.UsuarioENT)
                Session("UFC") = False
                Session("Usuario") = UsuarioAux
                Session("Permisos") = PermisoV.PermisoBLL.QuePermisos(UsuarioAux)
                Session("RolParaChat") = UsuarioV.UsuarioBLL.ListarUsuariosRoles(UsuarioAux)
                For Each Role In DirectCast(Session("RolParaChat"), UsuarioENT).ListaRoles
                    If Role.Codigo = "UFC" Then
                        Session("UFC") = True
                        Exit For
                    End If
                Next
                txtMasterEmail.Visible = False
                txtMasterPassword.Visible = False
                btnMasterIniciarSesion.Visible = False
                btnMasterRegistrarse.Visible = False
                btnMasterOlvidoContrasena.Visible = False
                divMasterAlerta.Visible = False
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(UsuarioAux.CorreoElectronico, "Inició sesión"))
                'Response.Redirect("Inicio.aspx")
                CargarMenu(False)
            ElseIf Flag = 0 Then
                divMasterAlerta.Visible = True
                Traduccion.TraducirAlertas(divMasterAlerta, divMasterAlerta.ID & Flag, DirectCast(Session("Idioma"), CultureInfo))
                'divMasterAlerta.InnerText = "Usuario inexistente"
            ElseIf Flag = 2 Then
                divMasterAlerta.Visible = True
                Traduccion.TraducirAlertas(divMasterAlerta, divMasterAlerta.ID & Flag, DirectCast(Session("Idioma"), CultureInfo))
                'divMasterAlerta.InnerText = "Contraseña incorrecta"
            ElseIf Flag = 3 Then
                divMasterAlerta.Visible = True
                Traduccion.TraducirAlertas(divMasterAlerta, divMasterAlerta.ID & Flag, DirectCast(Session("Idioma"), CultureInfo))
                'divMasterAlerta.InnerText = "Usuario bloqueado"
            ElseIf Flag = 4 Then
                divMasterAlerta.Visible = True
                Traduccion.TraducirAlertas(divMasterAlerta, divMasterAlerta.ID & Flag, DirectCast(Session("Idioma"), CultureInfo))
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(txtMasterEmail.Value, "Se ha bloqueado el usuario " & txtMasterEmail.Value))
                'divMasterAlerta.InnerText = "Se ha bloqueado al usuario"
            ElseIf Flag = 5 Then
                divMasterAlerta.Visible = True
                Traduccion.TraducirAlertas(divMasterAlerta, divMasterAlerta.ID & Flag, DirectCast(Session("Idioma"), CultureInfo))
                'divMasterAlerta.InnerText = "Aún no se ha validado el usuario, chequee su correo electrónico"
            End If
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Public Sub CargarMenu(Inicio As Boolean)
        For Each c As Control In ulMasterControlesMenu.Controls
            If TypeOf (c) Is HtmlAnchor Then
                c.Visible = False
            End If
        Next
        For Each permiso As PermisoENT In Session("Permisos")
            For Each c As Control In ulMasterControlesMenu.Controls
                If TypeOf (c) Is HtmlAnchor Then
                    If c.ID = permiso.Codigo Then
                        c.Visible = True
                    End If
                End If
            Next
        Next
        If Inicio = False And Not Session("Usuario") Is Nothing Then
            btnMasterUsuario.InnerHtml = DirectCast(Session("Usuario"), UsuarioENT).Nombre & " ▼"
            btnMasterUsuario.Visible = True
            If Session("UFC") = True Then
                'ConfigurarCarrito()
                If ChatV.ChatBLL.NotificarNuevoMensaje(DirectCast(Session("Usuario"), UsuarioENT), True) > 0 Then
                    buzon.Style.Add("color", "orange")
                Else
                    buzon.Style.Add("color", "white")
                End If
            Else
                If ChatV.ChatBLL.NotificarNuevoMensaje(Nothing, False) > 0 Then
                    buzon.Style.Add("color", "orange")
                Else
                    buzon.Style.Add("color", "white")
                End If
            End If
        Else
            btnMasterUsuario.Visible = False
        End If
        For Each item In MultidiomaV.MultidiomaBLL.ListarIdiomas(True)
            ListaIdiomasTraducidos.Add(item)
            ddlMasterIdiomas.Items.Add(item.Idioma.DisplayName)
        Next
        Session("IdiomasTraducidos") = ListaIdiomasTraducidos
        OcultarLinks()
    End Sub

    Private Sub lnkMasterLogOut_ServerClick(sender As Object, e As EventArgs) Handles lnkMasterLogOut.ServerClick
        Try
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, "Cerró sesión"))
            Session("Usuario") = Nothing
            Session("Permisos") = PermisoV.PermisoBLL.QuePermisos(Nothing)
            Session("Carrito") = Nothing
            Session("UFC") = False
            Response.Redirect("Inicio.aspx")
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub OcultarLinks()
        INI.Visible = False
        DP.Visible = False
        REG.Visible = False
        RC.Visible = False
        PD.Visible = False
        CP.Visible = False
        COM.Visible = False
        COMS.Visible = False
        CC.Visible = False
        FAQ.Visible = False
        ISE.Visible = False
    End Sub

    Private Sub btnMasterRecuperarContrasena_ServerClick(sender As Object, e As EventArgs) Handles btnMasterRecuperarContrasena.ServerClick
        Try
            UsuarioV.UsuarioENT.CorreoElectronico = txtMasterEmailRecupero.Value
            Session("UsuarioRecupero") = UsuarioV.UsuarioBLL.QueUsuario(UsuarioV.UsuarioENT)
            If Not DirectCast(Session("UsuarioRecupero"), UsuarioENT) Is Nothing Then
                EnviarMail(txtMasterEmailRecupero.Value)
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(txtMasterEmailRecupero.Value, "El usuario " & _
                                                                                        txtMasterEmailRecupero.Value & " ha solicitado recuperar la contraseña"))
                Response.Redirect("Inicio.aspx")
            Else
                txtMasterEmailRecupero.Value = ""
                divMasterAlerta.Visible = True
                divMasterAlerta.Attributes.Add("class", "alert alert-warning")
                Traduccion.TraducirAlertas(divMasterAlerta, divMasterAlerta.ID, DirectCast(Session("Idioma"), CultureInfo))
                'divMasterAlerta.InnerText = "El correo que ingresó, no pertenece a ningún usuario registrado"
            End If
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub EnviarMail(CorreoElectronico As String)
        UsuarioMail = DirectCast(Session("Usuario"), UsuarioENT)
        Dim Cliente As New SmtpClient()
        Dim VistaAlternativa As AlternateView = AlternateView.CreateAlternateViewFromString(PopulateBody(UsuarioV.UsuarioBLL.TestDecoding(DirectCast(Session("UsuarioRecupero"), UsuarioENT).Contraseña, DirectCast(Session("UsuarioRecupero"), UsuarioENT).ID)), Nothing, System.Net.Mime.MediaTypeNames.Text.Html)
        Dim Mensaje As New MailMessage()
        Dim Imagen As LinkedResource = New LinkedResource("C:\Users\Esteban\Documents\Visual Studio 2012\Projects\InvernArTFI\InvernArTFI\images\LogoWeb.png", _
                                                          System.Net.Mime.MediaTypeNames.Image.Jpeg)
        Mensaje.To.Add(CorreoElectronico)
        Mensaje.Subject = "Recuperar Contraseña"
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

    Private Function PopulateBody(Contraseña As String) As String
        Dim body As String = String.Empty
        Dim reader As StreamReader = New StreamReader(Server.MapPath("~\Cuerpos de mail\RecuperarContrasena.html"))
        body = reader.ReadToEnd
        body = body.Replace("{Contraseña}", Contraseña)
        Return body
    End Function

    Private Sub btnMasterSuscribirseNewsletter_ServerClick(sender As Object, e As EventArgs) Handles btnMasterSuscribirseNewsletter.ServerClick
        Try
            If txtMasterEmailNewsletter.Value <> "" And ddlMasterCategoria.SelectedIndex > 0 Then
                If ddlMasterCategoria.SelectedIndex > 1 Then
                    Dim SuscripcionNewsletterENT As New SuscripcionNewsletterENT
                    SuscripcionNewsletterENT.CorreoElectronico = txtMasterEmailNewsletter.Value
                    SuscripcionNewsletterENT.Categoria = DirectCast(Session("NewsletterCategoriasMaster"), List(Of CategoriaNewsletterENT)).Item(ddlMasterCategoria.SelectedIndex - 2).Id
                    If Not NewsletterV.NewsletterBLL.AltaSuscripcionNewsletter(SuscripcionNewsletterENT) >= 0 Then
                        divMasterAlerta.Visible = True
                        Traduccion.TraducirAlertas(divMasterAlerta, divMasterAlerta.ID & "Newsletter", DirectCast(Session("Idioma"), CultureInfo))
                    Else
                        BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(txtMasterEmailNewsletter.Value, "Se ha suscrito al Newsletter"))
                    End If
                ElseIf ddlMasterCategoria.SelectedIndex = 1 Then
                    'Session("NewsletterCategoriasMaster") = CategoriaNewsletterV.CategoriaNewsletterBLL.ListarCategoriasNewsletter
                    For Each item In DirectCast(Session("NewsletterCategoriasMaster"), List(Of CategoriaNewsletterENT))
                        Dim SuscripcionNewsletterENT As New SuscripcionNewsletterENT
                        SuscripcionNewsletterENT.CorreoElectronico = txtMasterEmailNewsletter.Value
                        SuscripcionNewsletterENT.Categoria = item.Id
                        NewsletterV.NewsletterBLL.BajaSuscripcionNewsletter(SuscripcionNewsletterENT)
                        NewsletterV.NewsletterBLL.AltaSuscripcionNewsletter(SuscripcionNewsletterENT)
                    Next
                End If
            End If
            txtMasterEmailNewsletter.Value = ""
            ddlMasterCategoria.SelectedIndex = 0
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Public Property LblModalTitle() As String
        Get
            Return lblMasterModalMensajeTitulo.InnerText
        End Get
        Set(value As String)
            lblMasterModalMensajeTitulo.InnerText = value
        End Set
    End Property

    Public Property LblModalBody() As String
        Get
            Return lblMasterModalMensajeMensaje.Text
        End Get
        Set(value As String)
            lblMasterModalMensajeMensaje.Text = value
        End Set
    End Property

    Public Property btnModalBoton() As String
        Get
            Return botonNotificacionAceptar.InnerText
        End Get
        Set(value As String)
            botonNotificacionAceptar.InnerText = value
        End Set
    End Property

    Public Sub ConfigurarCarrito()
        If Not Session("Carrito") Is Nothing Then
            carrito.Style.Remove("color")
            carrito.Style.Add("color", "red")
        Else
            carrito.Style.Remove("color")
            carrito.Style.Add("color", "white")
        End If
    End Sub

    Private Sub btnMasterRecuperarContrasenaClose_ServerClick(sender As Object, e As EventArgs) Handles btnMasterRecuperarContrasenaClose.ServerClick
        Try
            Response.Redirect("Inicio.aspx")
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnMasterSuscribirseNewsletterClose_ServerClick(sender As Object, e As EventArgs) Handles btnMasterSuscribirseNewsletterClose.ServerClick
        Try
            Response.Redirect("Inicio.aspx")
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub ddlMasterIdiomas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Session("Traducir") = True
        Session("IndiceIdioma") = ddlMasterIdiomas.SelectedIndex
        Session("Idioma") = ListaIdiomasTraducidos.Item(Session("IndiceIdioma")).Idioma
        Traducir()
        Response.Redirect(Request.Url.AbsolutePath)
    End Sub

End Class
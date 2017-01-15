Imports ENTITIES
Imports BLL
Imports System.Globalization
Imports System.Threading
Imports System.Text.RegularExpressions

Public Class BajaNewsletter
    Inherits System.Web.UI.Page
    'Dim UsuarioV As New UsuarioVista
    Dim NewsletterV As New NewsletterVista
    Dim BitacoraV As New BitacoraVista
    Dim CategoriaNewsletterV As New CategoriaNewsletterVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divBajaNewsletterControles.Controls, Thread.CurrentThread.CurrentUICulture)
            If Not IsPostBack Then
                Session("NewsletterCategoriasBaja") = CategoriaNewsletterV.CategoriaNewsletterBLL.ListarCategoriasNewsletter
                ddlBajaNewsletterCategoria.Items.Clear()
                For Each item In DirectCast(Session("NewsletterCategoriasBaja"), List(Of CategoriaNewsletterENT))
                    ddlBajaNewsletterCategoria.Items.Add(item.Categoria)
                Next
                ddlBajaNewsletterCategoria.Items.Insert(0, "")
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        lblModalTitle = titulo
        lblModalBody = body
        btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

    Public Property lblModalTitle() As String
        Get
            Return lblMasterModalMensajeTitulo.InnerText
        End Get
        Set(value As String)
            lblMasterModalMensajeTitulo.InnerText = value
        End Set
    End Property

    Public Property lblModalBody() As String
        Get
            Return lblMasterModalMensajeMensaje.Text
        End Get
        Set(value As String)
            lblMasterModalMensajeMensaje.Text = value
        End Set
    End Property

    Public Property btnModalBoton() As String
        Get
            Return btnMasterModalCerrar.InnerText
        End Get
        Set(value As String)
            btnMasterModalCerrar.InnerText = value
        End Set
    End Property

    Private Sub btnBajaNewsletterAceptar_ServerClick(sender As Object, e As EventArgs) Handles btnBajaNewsletterAceptar.ServerClick
        Try
            Dim RegCorreoElectronis As New Regex("^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$")
            If txtBajaNewsletterCorreoElectronico.Value <> "" And ddlBajaNewsletterCategoria.SelectedIndex > 0 Then
                If RegCorreoElectronis.Match(txtBajaNewsletterCorreoElectronico.Value).Success = True Then
                    BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(txtBajaNewsletterCorreoElectronico.Value, _
                                                                                           "El usuario se dio de baja de la suscripción al Newsletter"))
                    Dim SuscripcionNewsletterENT As New SuscripcionNewsletterENT
                    SuscripcionNewsletterENT.CorreoElectronico = txtBajaNewsletterCorreoElectronico.Value
                    SuscripcionNewsletterENT.Categoria = DirectCast(Session("NewsletterCategoriasBaja"), List(Of CategoriaNewsletterENT)).Item(ddlBajaNewsletterCategoria.SelectedIndex - 1).Id
                    'NewsletterV.NewsletterBLL.BajaSuscripcionNewsletter(UsuarioV.UsuarioENT)
                    NewsletterV.NewsletterBLL.BajaSuscripcionNewsletter(SuscripcionNewsletterENT)
                    txtBajaNewsletterCorreoElectronico.Value = ""
                    ddlBajaNewsletterCategoria.SelectedIndex = 0
                    Abrirmodal(Traduccion.TraducirMensaje("ModalBajaNewsletterTitulo", Thread.CurrentThread.CurrentUICulture), _
                               Traduccion.TraducirMensaje("ModalBajaNewsletterMensaje", Thread.CurrentThread.CurrentUICulture), _
                               Traduccion.TraducirMensaje("ModalBajaNewsletterBoton", Thread.CurrentThread.CurrentUICulture))
                Else
                    Abrirmodal(Traduccion.TraducirMensaje("Atencion", Thread.CurrentThread.CurrentUICulture), _
                               Traduccion.TraducirMensaje("Mensaje16", Thread.CurrentThread.CurrentUICulture), _
                               Traduccion.TraducirMensaje("botonNotificacionAceptar", Thread.CurrentThread.CurrentUICulture))
                End If
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", Thread.CurrentThread.CurrentUICulture), _
                           Traduccion.TraducirMensaje("CamposVacios", Thread.CurrentThread.CurrentUICulture), _
                           Traduccion.TraducirMensaje("botonNotificacionAceptar", Thread.CurrentThread.CurrentUICulture))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

End Class
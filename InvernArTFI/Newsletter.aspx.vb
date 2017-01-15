Imports BLL
Imports ENTITIES
Imports System.Globalization
Imports System.Net.Mail
Imports System.IO

Public Class Newsletter
    Inherits System.Web.UI.Page
    Dim BitacoraV As New BitacoraVista
    Dim NewsletterV As New NewsletterVista
    Dim ListaNewsletter As New List(Of NewsletterENT)
    Dim ListaNewsletterCategoria As New List(Of CategoriaNewsletterENT)
    Dim CategoriaNewsletterV As New CategoriaNewsletterVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvNewsletterNewsletter, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirGrillas(gvNewsletterCategorias, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divNewsletterControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divNewsletterListado.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divNewsletterNewsletter.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divNewsletterCategorias.Controls, DirectCast(Session("Idioma"), CultureInfo))
            'divNewsletterAlerta.Visible = False
            If Not IsPostBack Then
                divNewsletterNewsletter.Style("display") = ""
                divNewsletterListado.Style("display") = "none"
                divNewsletterCategorias.Style("display") = "none"
                Session("NewsletterLista") = Nothing
                BindData()
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub BindData()
        CargarLista()
        CargarGridView()
        LlenarListas()
    End Sub

    Private Sub CargarLista()
        Session("NewsletterLista") = NewsletterV.NewsletterBLL.ListarNewsletter
        Session("NewsletterCategoriasLista") = CategoriaNewsletterV.CategoriaNewsletterBLL.ListarCategoriasNewsletter
    End Sub

    Private Sub CargarGridView()
        gvNewsletterNewsletter.AutoGenerateColumns = False
        ListaNewsletter = DirectCast(Session("NewsletterLista"), List(Of NewsletterENT))
        gvNewsletterNewsletter.DataSource = ListaNewsletter
        gvNewsletterNewsletter.DataBind()
        gvNewsletterCategorias.AutoGenerateColumns = False
        ListaNewsletterCategoria = DirectCast(Session("NewsletterCategoriasLista"), List(Of CategoriaNewsletterENT))
        gvNewsletterCategorias.DataSource = ListaNewsletterCategoria
        gvNewsletterCategorias.DataBind()
    End Sub

    Private Sub LlenarListas()
        ddlNewsletterCategoria.Items.Clear()
        For Each item In CategoriaNewsletterV.CategoriaNewsletterBLL.ListarCategoriasNewsletter
            ddlNewsletterCategoria.Items.Add(item.Categoria)
        Next
        ddlNewsletterCategoria.Items.Insert(0, "")
    End Sub

    Private Function CargarImagen() As Byte()
        Dim byteImage As Byte() = Nothing
        If fuImagen.PostedFile.ContentLength > 0 Then
            byteImage = New [Byte](fuImagen.PostedFile.ContentLength - 1) {}
            fuImagen.PostedFile.InputStream.Read(byteImage, 0, fuImagen.PostedFile.ContentLength)
        End If
        Return byteImage
    End Function

    Private Sub btnNewsletterConfirmar_ServerClick(sender As Object, e As EventArgs) Handles btnNewsletterConfirmar.ServerClick
        Try
            Dim CampoVacio As Boolean = False
            For Each c As Control In divNewsletterNewsletter.Controls
                If TypeOf (c) Is HtmlInputText Then
                    If DirectCast(c, HtmlInputText).Value = "" Then
                        CampoVacio = True
                        Exit For
                    End If
                End If
            Next
            If ddlNewsletterCategoria.SelectedIndex = 0 Then
                CampoVacio = True
            End If
            If CampoVacio = False Then
                With NewsletterV.NewsletterENT
                    .Nombre = txtNewsletterNombre.Value
                    .Descripcion = txtNewsletterDescripcion.Value
                    .Asunto = txtNewsletterAsunto.Value
                    .Cuerpo = txtNewsletterCuerpo.Value
                    .FechaHora = Now
                    .Imagen = CargarImagen()
                    .Categoria = DirectCast(Session("NewsletterCategoriasLista"), List(Of CategoriaNewsletterENT)).Item(ddlNewsletterCategoria.SelectedIndex - 1).Id
                End With
                NewsletterV.NewsletterBLL.NuevoNewsletter(NewsletterV.NewsletterENT)
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Dio de alta el Newsletter """ & NewsletterV.NewsletterENT.Nombre & """"))
                txtNewsletterAsunto.Value = ""
                txtNewsletterCuerpo.Value = ""
                txtNewsletterDescripcion.Value = ""
                txtNewsletterNombre.Value = ""
                ddlNewsletterCategoria.SelectedIndex = 0
                'divNewsletterAlerta.Visible = True
                'divNewsletterAlerta.Attributes.Add("class", "alert alert-success")
                'Traduccion.TraducirAlertas(divNewsletterAlerta, divNewsletterAlerta.ID, DirectCast(Session("Idioma"), CultureInfo))
                ''divUsuariosAlerta.InnerText = "No pueden haber campos vacíos"
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                          Traduccion.TraducirMensaje("Mensaje14", DirectCast(Session("Idioma"), CultureInfo)), _
                          Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                BindData()
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("CamposVacios", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvNewsletterNewsletter_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvNewsletterNewsletter.RowCancelingEdit
        Try
            gvNewsletterNewsletter.EditIndex = -1
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvNewsletterNewsletter_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvNewsletterNewsletter.RowEditing
        Try
            gvNewsletterNewsletter.EditIndex = e.NewEditIndex
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvNewsletterNewsletter_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvNewsletterNewsletter.RowUpdating
        Try
            Dim ListaNewsletter = CType(Session("NewsletterLista"), List(Of NewsletterENT))
            Dim row = gvNewsletterNewsletter.Rows(e.RowIndex)
            CType(row.Cells(3).Controls(5), RequiredFieldValidator).Validate()
            CType(row.Cells(4).Controls(5), RequiredFieldValidator).Validate()
            CType(row.Cells(5).Controls(5), RequiredFieldValidator).Validate()
            CType(row.Cells(6).Controls(5), RequiredFieldValidator).Validate()
            If CType(row.Cells(3).Controls(5), RequiredFieldValidator).IsValid = True And _
                CType(row.Cells(4).Controls(5), RequiredFieldValidator).IsValid = True And _
                CType(row.Cells(5).Controls(5), RequiredFieldValidator).IsValid = True And _
                CType(row.Cells(6).Controls(5), RequiredFieldValidator).IsValid = True Then
                With ListaNewsletter.Item(row.DataItemIndex)
                    .Nombre = (CType((row.Cells(3).Controls(1)), TextBox)).Text
                    .Descripcion = (CType((row.Cells(4).Controls(1)), TextBox)).Text
                    .Asunto = (CType((row.Cells(5).Controls(1)), TextBox)).Text
                    .Cuerpo = (CType((row.Cells(6).Controls(1)), TextBox)).Text
                    .FechaHora = Now
                    .Categoria = DirectCast(Session("NewsletterCategoriasLista"), List(Of CategoriaNewsletterENT)).Item(CType(row.Cells(8).Controls(1), DropDownList).SelectedIndex).Id
                    .Imagen = If(DirectCast(row.Cells(9).Controls(1), FileUpload).PostedFile.ContentLength > 0, CargarModificaImagen(row), ObtnerImagenAnterior(row))
                End With
                Dim NewsletterENT As New NewsletterENT
                With NewsletterENT
                    .Id = ListaNewsletter.Item(row.DataItemIndex).Id
                    .Nombre = ListaNewsletter.Item(row.DataItemIndex).Nombre
                    .Descripcion = ListaNewsletter.Item(row.DataItemIndex).Descripcion
                    .Asunto = ListaNewsletter.Item(row.DataItemIndex).Asunto
                    .Cuerpo = ListaNewsletter.Item(row.DataItemIndex).Cuerpo
                    .FechaHora = ListaNewsletter.Item(row.DataItemIndex).FechaHora
                    .Categoria = ListaNewsletter.Item(row.DataItemIndex).Categoria
                    .Imagen = ListaNewsletter.Item(row.DataItemIndex).Imagen
                End With
                NewsletterV.NewsletterBLL.ModificarNewsletter(NewsletterENT)
                Session("NewsletterLista") = ListaNewsletter
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Modificó el Newsletter " & NewsletterENT.Id))
                gvNewsletterNewsletter.EditIndex = -1
                BindData()
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Function CargarModificaImagen(row As GridViewRow) As Byte()
        Dim byteImage As Byte() = Nothing
        If DirectCast(row.Cells(9).Controls(1), FileUpload).PostedFile.ContentLength > 0 Then
            byteImage = New [Byte](DirectCast(row.Cells(8).Controls(1), FileUpload).PostedFile.ContentLength - 1) {}
            DirectCast(row.Cells(9).Controls(1), FileUpload).PostedFile.InputStream.Read(byteImage, 0, DirectCast(row.Cells(8).Controls(1),  _
                                                                                         FileUpload).PostedFile.ContentLength)
        End If
        Return byteImage
    End Function

    Private Function ObtnerImagenAnterior(row As GridViewRow) As Byte()
        Dim ListaNewsletter = CType(Session("NewsletterLista"), List(Of NewsletterENT))
        Return ListaNewsletter.Item(row.DataItemIndex).Imagen
    End Function

    Private Sub aNewsletterNuevo_ServerClick(sender As Object, e As EventArgs) Handles aNewsletterNuevo.ServerClick
        Try
            divNewsletterNewsletter.Style("display") = ""
            divNewsletterListado.Style("display") = "none"
            divNewsletterCategorias.Style("display") = "none"
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub aNewsletterListado_ServerClick(sender As Object, e As EventArgs) Handles aNewsletterListado.ServerClick
        Try
            divNewsletterNewsletter.Style("display") = "none"
            divNewsletterListado.Style("display") = ""
            divNewsletterCategorias.Style("display") = "none"
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub aNewsletterCategorias_ServerClick(sender As Object, e As EventArgs) Handles aNewsletterCategorias.ServerClick
        Try
            divNewsletterNewsletter.Style("display") = "none"
            divNewsletterListado.Style("display") = "none"
            divNewsletterCategorias.Style("display") = ""
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub EnviarNewsletter_Click(sender As Object, e As EventArgs)
        Try
            Dim idNewsletter = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            Dim ListaNewsletter = CType(Session("NewsletterLista"), List(Of NewsletterENT))
            Dim ListaSuscripciones = NewsletterV.NewsletterBLL.ListarSuscripcionesNewsletter
            For Each Newsletter In ListaNewsletter
                If Newsletter.Id = idNewsletter Then
                    For i = 0 To ListaSuscripciones.Count - 1
                        For Each item In DirectCast(Session("NewsletterCategoriasLista"), List(Of CategoriaNewsletterENT))
                            If item.Categoria = Newsletter.Categoria Then
                                If ListaSuscripciones.Item(i).Categoria = item.Id Then
                                    EnviarMail(Newsletter, NewsletterV.NewsletterBLL.ListarSuscripcionesNewsletter.Item(i).CorreoElectronico, i)
                                End If
                            End If
                        Next
                    Next
                End If
            Next
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub EnviarMail(Newsletter As NewsletterENT, CorreoElectronico As String, Iteracion As Integer)
        Dim Cliente As New SmtpClient()
        Dim VistaAlternativa As AlternateView
        VistaAlternativa = AlternateView.CreateAlternateViewFromString(PopulateBody(Newsletter.Cuerpo), Nothing, System.Net.Mime.MediaTypeNames.Text.Html)
        Dim Mensaje As New MailMessage()
        Dim Imagen As LinkedResource = New LinkedResource("C:\Users\Esteban\Documents\Visual Studio 2012\Projects\InvernArTFI\InvernArTFI\images\LogoWeb.png", _
                                                          System.Net.Mime.MediaTypeNames.Image.Jpeg)
        Dim Archivo As String = "C:\" & Iteracion & ".jpg"
        Mensaje.To.Add(CorreoElectronico)
        Mensaje.Subject = Newsletter.Asunto
        Mensaje.IsBodyHtml = True
        Imagen.ContentId = "Pic1"
        If Not Newsletter.Imagen Is Nothing Then
            File.WriteAllBytes(Archivo, Newsletter.Imagen)
        End If
        Dim Imagen2 As LinkedResource = New LinkedResource(Archivo)
        Imagen2.ContentId = "Pic2"
        VistaAlternativa.LinkedResources.Add(Imagen)
        VistaAlternativa.LinkedResources.Add(Imagen2)
        Mensaje.AlternateViews.Add(VistaAlternativa)
        Try
            Cliente.Send(Mensaje)
            If Not Newsletter.Imagen Is Nothing Then
                My.Computer.FileSystem.DeleteFile(Archivo)
            End If
        Catch ex As Exception
            ' ...
        End Try
    End Sub

    Private Function PopulateBody(Cuerpo As String) As String
        Dim body As String = String.Empty
        Dim Link As String = "http://localhost:1698/BajaNewsletter.aspx"
        Dim reader As StreamReader = New StreamReader(Server.MapPath("~\Cuerpos de mail\Newsletter.html"))
        body = reader.ReadToEnd
        body = body.Replace("{Cuerpo}", Cuerpo)
        body = body.Replace("{Baja}", "<a id=""aMasterPoliticasPrivacidad"" runat=""server"" href=""" & Link & """ target=""_blank"">aquí</a>")
        Return body
    End Function

    Protected Sub btnNewsletterEliminar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim Id = TryCast(sender, ImageButton).CommandArgument
            Dim ListaNewsletter = CType(Session("NewsletterLista"), List(Of NewsletterENT))
            Dim NewsletterENT As New NewsletterENT
            NewsletterENT.Id = Id
            NewsletterV.NewsletterBLL.EliminarNewsletter(NewsletterENT)
            For i = 0 To ListaNewsletter.Count - 1
                If ListaNewsletter.Item(i).Id = Id Then
                    ListaNewsletter.RemoveAt(i)
                    Exit For
                End If
            Next
            If ListaNewsletter.Count = 0 Then
                Session("NewsletterLista") = Nothing
            End If
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Eliminó el Newsletter " & NewsletterENT.Id))
            gvNewsletterNewsletter.AutoGenerateColumns = False
            gvNewsletterNewsletter.DataSource = ListaNewsletter
            gvNewsletterNewsletter.DataBind()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

    Private Sub btnNewsletterCategoriaNueva_ServerClick(sender As Object, e As EventArgs) Handles btnNewsletterCategoriaNueva.ServerClick
        Try
            Dim CampoVacio As Boolean = False
            For Each c As Control In divNewsletterCategorias.Controls
                If TypeOf (c) Is HtmlInputText Then
                    If DirectCast(c, HtmlInputText).Value = "" Then
                        CampoVacio = True
                        Exit For
                    End If
                End If
            Next
            If CampoVacio = False Then
                CategoriaNewsletterV.CategoriaNewsletterENT.Categoria = txtNewsletterNombreCategoria.Value
                CategoriaNewsletterV.CategoriaNewsletterBLL.NuevaCategoriaNewsletter(CategoriaNewsletterV.CategoriaNewsletterENT)
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Dio de alta la Categoría_Newsletter """ & CategoriaNewsletterV.CategoriaNewsletterENT.Categoria & """"))
                txtNewsletterNombreCategoria.Value = ""
                BindData()
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                         Traduccion.TraducirMensaje("Mensaje15", DirectCast(Session("Idioma"), CultureInfo)), _
                         Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("CamposVacios", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvNewsletterCategorias_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvNewsletterCategorias.RowCancelingEdit
        Try
            gvNewsletterCategorias.EditIndex = -1
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvNewsletterCategorias_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvNewsletterCategorias.RowEditing
        Try
            gvNewsletterCategorias.EditIndex = e.NewEditIndex
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvNewsletterCategorias_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvNewsletterCategorias.RowUpdating
        Try
            Dim ListaCategoriaNewsletter = CType(Session("NewsletterCategoriasLista"), List(Of CategoriaNewsletterENT))
            Dim row = gvNewsletterCategorias.Rows(e.RowIndex)
            CType(row.Cells(3).Controls(5), RequiredFieldValidator).Validate()
            If CType(row.Cells(3).Controls(5), RequiredFieldValidator).IsValid = True Then
                With ListaCategoriaNewsletter.Item(row.DataItemIndex)
                    .Categoria = (CType((row.Cells(3).Controls(1)), TextBox)).Text
                End With
                Dim CategoriaNewsletterENT As New CategoriaNewsletterENT
                With CategoriaNewsletterENT
                    .Id = ListaCategoriaNewsletter.Item(row.DataItemIndex).Id
                    .Categoria = ListaCategoriaNewsletter.Item(row.DataItemIndex).Categoria
                End With
                CategoriaNewsletterV.CategoriaNewsletterBLL.ModificarCategoriaNewsletter(CategoriaNewsletterENT)
                Session("NewsletterCategoriasLista") = ListaCategoriaNewsletter
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Modificó la CategoríaNewsletter " & CategoriaNewsletterENT.Id))
                gvNewsletterCategorias.EditIndex = -1
                BindData()
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub btnNewsletterCategoriaEliminar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim Id = TryCast(sender, ImageButton).CommandArgument
            Dim ListaCategorias = CType(Session("NewsletterCategoriasLista"), List(Of CategoriaNewsletterENT))
            Dim CategoriaNewsletterENT As New CategoriaNewsletterENT
            CategoriaNewsletterENT.Id = Id
            If CategoriaNewsletterV.CategoriaNewsletterBLL.EliminarCategoriaNewsletter(CategoriaNewsletterENT) > 0 Then
                For i = 0 To ListaCategorias.Count - 1
                    If ListaCategorias.Item(i).Id = Id Then
                        ListaCategorias.RemoveAt(i)
                        Exit For
                    End If
                Next
                If ListaCategorias.Count = 0 Then
                    Session("NewsletterCategoriasLista") = Nothing
                End If
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Eliminó la categoría " & CategoriaNewsletterENT.Id))
                BindData()
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("Mensaje13", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Public Function ListarNewsletterCategoria() As List(Of String)
        ListarNewsletterCategoria = New List(Of String)
        For Each item In CategoriaNewsletterV.CategoriaNewsletterBLL.ListarCategoriasNewsletter
            ListarNewsletterCategoria.Add(item.Categoria)
        Next
        Return ListarNewsletterCategoria
    End Function

End Class
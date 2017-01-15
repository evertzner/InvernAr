Imports BLL
Imports ENTITIES
Imports System.Globalization

Public Class GestionNoticias
    Inherits System.Web.UI.Page
    Dim BitacoraV As New BitacoraVista
    Dim NoticiaV As New NoticiaVista
    Dim ListaNoticias As New List(Of NoticiaENT)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvGestionNoticiasNoticias, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divGestionNoticiasControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divGestionNoticiasListado.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divGestionNoticiasNoticias.Controls, DirectCast(Session("Idioma"), CultureInfo))
            'divGestionNoticiasAlerta.Visible = False
            If Not IsPostBack Then
                divGestionNoticiasNoticias.Style("display") = ""
                divGestionNoticiasListado.Style("display") = "none"
                Session("NoticiasLista") = Nothing
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
    End Sub

    Private Sub CargarLista()
        Session("NoticiasLista") = NoticiaV.NoticiaBLL.ListarNoticias
    End Sub

    Private Sub CargarGridView()
        gvGestionNoticiasNoticias.AutoGenerateColumns = False
        ListaNoticias = DirectCast(Session("NoticiasLista"), List(Of NoticiaENT))
        gvGestionNoticiasNoticias.DataSource = ListaNoticias
        gvGestionNoticiasNoticias.DataBind()
    End Sub

    Private Function CargarImagen() As Byte()
        Dim byteImage As Byte() = Nothing
        If fuImagen.PostedFile.ContentLength > 0 Then
            byteImage = New [Byte](fuImagen.PostedFile.ContentLength - 1) {}
            fuImagen.PostedFile.InputStream.Read(byteImage, 0, fuImagen.PostedFile.ContentLength)
        End If
        Return byteImage
    End Function

    Private Sub btnGestionNoticiasConfirmar_ServerClick(sender As Object, e As EventArgs) Handles btnGestionNoticiasConfirmar.ServerClick
        Try
            Dim CampoVacio As Boolean = False
            For Each c As Control In divGestionNoticiasNoticias.Controls
                If TypeOf (c) Is HtmlInputText Then
                    If DirectCast(c, HtmlInputText).Value = "" Then
                        CampoVacio = True
                        Exit For
                    End If
                End If
            Next
            If CampoVacio = False Then
                With NoticiaV.NoticiaENT
                    .Titulo = txtGestionNoticiasTitulo.Value
                    .Contenido = txtGestionNoticiasContenido.InnerText
                    .FechaHora = Now
                    .Imagen = CargarImagen()
                End With
                NoticiaV.NoticiaBLL.NuevaNoticia(NoticiaV.NoticiaENT)
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Dio de alta la noticia """ & NoticiaV.NoticiaENT.Titulo & """"))
                txtGestionNoticiasContenido.InnerText = ""
                txtGestionNoticiasTitulo.Value = ""
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

    Private Sub gvGestionNoticiasNoticias_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvGestionNoticiasNoticias.RowCancelingEdit
        Try
            gvGestionNoticiasNoticias.EditIndex = -1
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvGestionNoticiasNoticias_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvGestionNoticiasNoticias.RowEditing
        Try
            gvGestionNoticiasNoticias.EditIndex = e.NewEditIndex
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvGestionNoticiasNoticias_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvGestionNoticiasNoticias.RowUpdating
        Try
            Dim ListaNoticias = CType(Session("NoticiasLista"), List(Of NoticiaENT))
            Dim row = gvGestionNoticiasNoticias.Rows(e.RowIndex)
            CType(row.Cells(3).Controls(5), RequiredFieldValidator).Validate()
            CType(row.Cells(4).Controls(5), RequiredFieldValidator).Validate()
            If CType(row.Cells(3).Controls(5), RequiredFieldValidator).IsValid = True And CType(row.Cells(4).Controls(5), RequiredFieldValidator).IsValid = True Then
                With ListaNoticias.Item(row.DataItemIndex)
                    .Titulo = (CType((row.Cells(3).Controls(1)), TextBox)).Text
                    .Contenido = (CType((row.Cells(4).Controls(1)), TextBox)).Text
                    .FechaHora = Now
                    .Imagen = If(DirectCast(row.Cells(6).Controls(1), FileUpload).PostedFile.ContentLength > 0, CargarModificaImagen(row), ObtnerImagenAnterior(row))
                End With
                Dim NoticiaENT As New NoticiaENT
                With NoticiaENT
                    .Id = ListaNoticias.Item(row.DataItemIndex).Id
                    .Titulo = ListaNoticias.Item(row.DataItemIndex).Titulo
                    .Contenido = ListaNoticias.Item(row.DataItemIndex).Contenido
                    .FechaHora = ListaNoticias.Item(row.DataItemIndex).FechaHora
                    .Imagen = ListaNoticias.Item(row.DataItemIndex).Imagen
                End With
                NoticiaV.NoticiaBLL.ModificarNoticia(NoticiaENT)
                Session("NoticiasLista") = ListaNoticias
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Modificó la noticia " & NoticiaENT.Id))
                gvGestionNoticiasNoticias.EditIndex = -1
                gvGestionNoticiasNoticias.AutoGenerateColumns = False
                gvGestionNoticiasNoticias.DataSource = ListaNoticias
                gvGestionNoticiasNoticias.DataBind()
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
        If DirectCast(row.Cells(6).Controls(1), FileUpload).PostedFile.ContentLength > 0 Then
            byteImage = New [Byte](DirectCast(row.Cells(6).Controls(1), FileUpload).PostedFile.ContentLength - 1) {}
            DirectCast(row.Cells(6).Controls(1), FileUpload).PostedFile.InputStream.Read(byteImage, 0, DirectCast(row.Cells(6).Controls(1),  _
                                                                                         FileUpload).PostedFile.ContentLength)
        End If
        Return byteImage
    End Function

    Private Function ObtnerImagenAnterior(row As GridViewRow) As Byte()
        Dim ListaNoticias = CType(Session("NoticiasLista"), List(Of NoticiaENT))
        Return ListaNoticias.Item(row.DataItemIndex).Imagen
    End Function

    Private Sub aGestionNoticiasNueva_ServerClick(sender As Object, e As EventArgs) Handles aGestionNoticiasNueva.ServerClick
        Try
            divGestionNoticiasNoticias.Style("display") = ""
            divGestionNoticiasListado.Style("display") = "none"
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub aGestionNoticiasListado_ServerClick(sender As Object, e As EventArgs) Handles aGestionNoticiasListado.ServerClick
        Try
            divGestionNoticiasNoticias.Style("display") = "none"
            divGestionNoticiasListado.Style("display") = ""
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub btnGestionNoticiasEliminar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim Id = TryCast(sender, ImageButton).CommandArgument
            Dim ListaNoticias = CType(Session("NoticiasLista"), List(Of NoticiaENT))
            Dim NoticiaENT As New NoticiaENT
            NoticiaENT.Id = Id
            NoticiaV.NoticiaBLL.EliminarNoticia(NoticiaENT)
            For i = 0 To ListaNoticias.Count - 1
                If ListaNoticias.Item(i).Id = Id Then
                    ListaNoticias.RemoveAt(i)
                    Exit For
                End If
            Next
            If ListaNoticias.Count = 0 Then
                Session("NoticiasLista") = Nothing
            End If
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Eliminó la noticia " & NoticiaENT.Id))
            gvGestionNoticiasNoticias.AutoGenerateColumns = False
            gvGestionNoticiasNoticias.DataSource = ListaNoticias
            gvGestionNoticiasNoticias.DataBind()
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

End Class
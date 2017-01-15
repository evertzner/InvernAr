Imports BLL
Imports ENTITIES
Imports System.Globalization

Public Class DetalleProducto
    Inherits System.Web.UI.Page
    Dim ComentarioProductoV As New ComentarioProductoVista
    Dim ProductoCatalogoENT As ProductoCatalogoENT
    Dim Lista As New List(Of ComentarioProductoENT)
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvDetalleProductoComentarios, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divDetalleProductoControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divDetalleProductoComentar.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                hiddenValorizacion.Value = 0
                If Request.QueryString("Id") > 0 Then
                    Session("ProductoId") = Request.QueryString("Id")
                    CargarProducto()
                Else
                    Response.Redirect("Inicio.aspx")
                End If
            End If
            If Session("Usuario") Is Nothing Then
                divDetalleProductoComentar.Visible = False
            Else
                divDetalleProductoComentar.Visible = True
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub CargarProducto()
        Dim Lista = DirectCast(Session("CatalogoLista"), List(Of ProductoCatalogoENT))
        For Each Producto In Lista
            If Producto.Id = Session("ProductoId") Then
                ProductoCatalogoENT = New ProductoCatalogoENT
                ProductoCatalogoENT = Producto
                Exit For
            End If
        Next
        Dim containerCatalogo = New HtmlGenericControl("DIV")
        containerCatalogo.Attributes.Add("class", "preview-panel")
        Dim imagen = New Image()
        imagen.ImageUrl = CargarImagen(ProductoCatalogoENT.Imagen)
        imagen.CssClass = "img-responsive"
        containerCatalogo.Controls.Add(imagen)
        divDetalleProductoContenedor.Controls.Add(containerCatalogo)
        Dim TituloEncabezado = New HtmlGenericControl("DIV")
        TituloEncabezado.Attributes.Add("class", "panel-heading")
        Dim TituloLiteral = New Literal
        TituloLiteral.Text += "<h3 class=""panel-title"">" & ProductoCatalogoENT.Nombre & "</h3>"
        TituloEncabezado.Controls.Add(TituloLiteral)
        divDetalleProductoDetalles.Controls.Add(TituloEncabezado)
        Dim Texto As String = ""
        For i = 1 To 3
            Dim CuerpoDetalles = New HtmlGenericControl("DIV")
            CuerpoDetalles.Attributes.Add("class", "panel-body")
            If i = 1 Then
                CuerpoDetalles.InnerText = "Especificación: " & ProductoCatalogoENT.Especificacion
            ElseIf i = 2 Then
                CuerpoDetalles.InnerText = "TIpo: " & ProductoCatalogoENT.Tipo
            ElseIf i = 3 Then
                CuerpoDetalles.InnerText = "Precio Unitario: " & ProductoCatalogoENT.PrecioUnitario
            End If
            divDetalleProductoDetalles.Controls.Add(CuerpoDetalles)
        Next
        BindData()
    End Sub

    Public Function CargarImagen(imgBytes As Byte()) As String
        Dim base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length)
        Return "data:image/jpeg;base64," + base64String
    End Function

    Sub BindData()
        CargarLista()
        CargarGridView()
        MostrarRating()
    End Sub

    Private Sub CargarLista()
        Dim ProductoCatalogoENT As New ProductoCatalogoENT
        ProductoCatalogoENT.Id = Session("ProductoId")
        Session("ComentariosProducto") = Nothing
        Session("ComentariosProducto") = ComentarioProductoV.ComentarioProductoBLL.ListarComentarios(ProductoCatalogoENT)
    End Sub

    Private Sub CargarGridView()
        gvDetalleProductoComentarios.AutoGenerateColumns = False
        Lista = DirectCast(Session("ComentariosProducto"), List(Of ComentarioProductoENT))
        gvDetalleProductoComentarios.DataSource = Lista
        gvDetalleProductoComentarios.DataBind()
    End Sub

    Private Sub MostrarRating()
        Dim ListaComentarios = CType(Session("ComentariosProducto"), List(Of ComentarioProductoENT))
        For i = 0 To gvDetalleProductoComentarios.Rows.Count - 1
            For Each Control In gvDetalleProductoComentarios.Rows(i).Cells(5).Controls
                If TypeOf (Control) Is Label Then
                    If DirectCast(Control, Label).ID.Substring(8, 1) <= ListaComentarios.Item(i).Valorizacion Then
                        DirectCast(Control, Label).Style.Add("color", "#5cb85c")
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub btnDetalleProductoComentar_ServerClick(sender As Object, e As EventArgs) Handles btnDetalleProductoComentar.ServerClick
        Try
            If txtDetalleProductoComentario.Value <> "" Then
                With ComentarioProductoV.ComentarioProductoENT
                    .IdProducto = Session("ProductoId")
                    .IdUsuario = DirectCast(Session("Usuario"), UsuarioENT).ID
                    .Comentario = txtDetalleProductoComentario.Value
                    .Valorizacion = hiddenValorizacion.Value
                    .FechaComentado = Now
                End With
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                       "Realizó un comentario sobre el producto " & _
                                                                                       ComentarioProductoV.ComentarioProductoENT.IdProducto))
                ComentarioProductoV.ComentarioProductoBLL.AgregarComentario(ComentarioProductoV.ComentarioProductoENT)
                txtDetalleProductoComentario.Value = ""
                BindData()
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("CamposVacios", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvDetalleProductoComentarios_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvDetalleProductoComentarios.PageIndexChanging
        Try
            gvDetalleProductoComentarios.PageIndex = e.NewPageIndex
            CargarGridView()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
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
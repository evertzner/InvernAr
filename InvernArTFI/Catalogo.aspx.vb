Imports BLL
Imports ENTITIES
Imports System.Globalization
Imports System.Text

Public Class Catalogo
    Inherits System.Web.UI.Page
    Dim CatalogoV As New CatalogoVista
    Dim Lista As New List(Of ProductoCatalogoENT)
    Dim TipoProductoV As New TipoProductoVista
    Dim ProductoV As New ProductoVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divCatalogoControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            'divCatalogoAlerta.Visible = False
            If Not IsPostBack Then
                BindData()
                Session("CatalogoListaFiltrada") = Nothing
                Session("Comparar") = Nothing
                Session("ProductosAComparar") = Nothing
            End If
            If IsPostBack Then
                CargarCatalogo()
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub BindData()
        LlenarListas()
        CargarCatalogo()
    End Sub

    Private Sub LlenarListas()
        ddlCatalogoTipo.Items.Clear()
        For Each item In TipoProductoV.TipoProductoBLL.ListarTipoProducto
            ddlCatalogoTipo.Items.Add(item.Tipo)
        Next
        ddlCatalogoTipo.Items.Insert(0, "")
        With ddlOrdenarPor.Items
            .Add("")
            .Add("Nombre A-Z")
            .Add("Nombre Z-A")
            .Add("Precio ascendente")
            .Add("Precio descendete")
        End With
    End Sub

    Private Sub CargarLista()
        Session("CatalogoLista") = CatalogoV.CatalogoBLL.ListarProductosCatalogo(ProductoV.ProductoENT)
    End Sub

    Public Function CargarImagen(imgBytes As Byte()) As String
        Dim base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length)
        Return "data:image/jpeg;base64," + base64String
    End Function

    Private Sub CargarCatalogo()
        divCatalogoContenedor.InnerHtml = ""
        If Session("CatalogoListaFiltrada") Is Nothing Then
            CargarLista()
            Lista = CatalogoV.CatalogoBLL.ListarProductosCatalogo(ProductoV.ProductoENT)
        Else
            Lista = DirectCast(Session("CatalogoListaFiltrada"), List(Of ProductoCatalogoENT))
        End If
        For Each Producto In Lista
            Dim containerCatalogo = New HtmlGenericControl("DIV")
            containerCatalogo.Attributes.Add("class", "preview-panel")
            Dim imagen = New Image()
            imagen.ImageUrl = CargarImagen(Producto.Imagen)
            imagen.CssClass = "img-responsive"
            containerCatalogo.Controls.Add(imagen)
            Dim containerDescripcion = New HtmlGenericControl("DIV")
            containerDescripcion.Attributes.Add("class", "preview-panel-content")
            Dim literal = New Literal()
            Dim literalText = "<h3>" & Producto.Nombre & "</h3>"
            literalText += "<p>" & Producto.Especificacion & "</p>"
            literal.Text = literalText
            containerDescripcion.Controls.Add(literal)
            containerCatalogo.Controls.Add(containerDescripcion)
            Dim containerComparar = New HtmlGenericControl("DIV")
            Dim literal2 = New Literal()
            Dim literalText2 As String = ""
            If Session("UFC") = True Then
                Dim buttonComprar As New HtmlInputButton()
                buttonComprar.Attributes.Add("class", "btn btn-primary botonComprar")
                buttonComprar.Attributes.Add("UseSubmitBehavior", "False")
                buttonComprar.Value = "Comprar"
                AddHandler buttonComprar.ServerClick, AddressOf ButtonComprar_Click
                buttonComprar.Attributes.Add("id", Producto.Id.ToString())
                containerComparar.Controls.Add(buttonComprar)
                literalText2 += "<p style=""color:white;"">$ " & Producto.PrecioUnitario & "</p>"
                literalText2 += "<a id=" & Producto.Id & " href=""DetalleProducto.aspx?Id=" & Producto.Id & _
                    """ class=""btn btn-default glyphicon glyphicon-eye-open""></a>"
            Else
                literalText2 += "<p style=""color:white;"">$ " & Producto.PrecioUnitario & "</p>"
                literalText2 += "<a id=" & Producto.Id & " href=""DetalleProducto.aspx?Id=" & Producto.Id & _
                    """ class=""btn btn-default glyphicon glyphicon-eye-open""></a>"
            End If
            literalText2 += "<div style=""color:white;"">Comparar<input id=""chk" & Producto.Id & _
                """type=""checkbox"" runat=""server"" onclick=""mostrar(this);"" /></div>"
            literal2.Text = literalText2
            containerComparar.Controls.Add(literal2)
            containerCatalogo.Controls.Add(containerComparar)
            divCatalogoContenedor.Controls.Add(containerCatalogo)
        Next
    End Sub

    Private Sub ButtonComprar_Click(sender As Object, e As EventArgs)
        Try
            CargarCatalogo()
            Dim Id As Integer = Integer.Parse(DirectCast(sender, HtmlInputButton).Attributes("id"))
            Dim ListaProductosAComprar As New List(Of FacturaDetalleENT)
            If Not Session("Carrito") Is Nothing Then
                For Each Producto In DirectCast(Session("Carrito"), List(Of FacturaDetalleENT))
                    ListaProductosAComprar.Add(Producto)
                Next
            End If
            CargarLista()
            For Each Producto In Lista
                If Producto.Id = Id Then
                    Dim ProductoComprar As New FacturaDetalleENT
                    ProductoComprar.IdProducto = Producto.Id
                    ProductoComprar.PrecioUnitario = Producto.PrecioUnitario
                    ProductoComprar.ProductoNombre = Producto.Nombre
                    ProductoComprar.Cantidad = 1
                    ListaProductosAComprar.Add(ProductoComprar)
                    Session("Carrito") = ListaProductosAComprar
                End If
            Next
            Response.Redirect("Catalogo.aspx")
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnCatalogoComparar_ServerClick(sender As Object, e As EventArgs) Handles btnCatalogoComparar.ServerClick
        Try
            Dim Lista As New List(Of ProductoCatalogoENT)
            Dim ProductoCatalogoENT As ProductoCatalogoENT
            Dim ListaString As New List(Of String)
            Dim Seleccionados As Integer = 0
            Dim CheckBoxIds() As String
            Dim Texto1 As String = hiddenProductos.Value.Replace("chk", " ")
            CheckBoxIds = Texto1.Split(" ")
            Dim Texto As String = ""
            For Each item In CheckBoxIds
                ListaString.Add(item)
            Next
            ListaString.RemoveAt(0)
            For Each item In ListaString
                '    Texto += item
                ProductoCatalogoENT = New ProductoCatalogoENT
                ProductoCatalogoENT.Id = item
                Lista.Add(ProductoCatalogoENT)
            Next
            Session("ProductosAComparar") = Lista
            If hiddenCuenta.Value = "true" Then
                Response.Redirect("CompararProductos.aspx")
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("divCatalogoAlerta", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                'divCatalogoAlerta.Visible = True
                'divCatalogoAlerta.Attributes.Add("class", "alert alert-danger")
                'Traduccion.TraducirAlertas(divCatalogoAlerta, divCatalogoAlerta.ID, DirectCast(Session("Idioma"), CultureInfo))
                ''divUsuariosAlerta.InnerText = "Debe seleccionar al menos 2 productos"
            End If
            Session("Filtro") = Nothing
            BindData()
            hiddenProductos.Value = ""
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnCatalogoFiltrar_ServerClick(sender As Object, e As EventArgs) Handles btnCatalogoFiltrar.Click
        Try
            CargarLista()
            Lista.Clear()
            If ddlCatalogoTipo.SelectedIndex = 0 And ddlOrdenarPor.SelectedIndex = 0 Then
                Session("CatalogoListaFiltrada") = Nothing
                Lista = DirectCast(Session("CatalogoLista"), List(Of ProductoCatalogoENT))
                'Response.Redirect("Catalogo.aspx")
            ElseIf ddlCatalogoTipo.SelectedIndex > 0 And ddlOrdenarPor.SelectedIndex = 0 Then
                For Each item In DirectCast(Session("CatalogoLista"), List(Of ProductoCatalogoENT))
                    If item.Tipo = ddlCatalogoTipo.SelectedValue Then
                        Lista.Add(item)
                    End If
                Next
                'Session("CatalogoListaFiltrada") = Lista
                'CargarCatalogo()
            ElseIf ddlCatalogoTipo.SelectedIndex = 0 And ddlOrdenarPor.SelectedIndex > 0 Then
                If ddlOrdenarPor.SelectedIndex = 1 Then
                    Lista = DirectCast(Session("CatalogoLista"), List(Of ProductoCatalogoENT)).OrderBy(Function(x) x.Nombre).ToList
                ElseIf ddlOrdenarPor.SelectedIndex = 2 Then
                    Lista = DirectCast(Session("CatalogoLista"), List(Of ProductoCatalogoENT)).OrderBy(Function(x) x.Nombre).Reverse.ToList
                ElseIf ddlOrdenarPor.SelectedIndex = 3 Then
                    Lista = DirectCast(Session("CatalogoLista"), List(Of ProductoCatalogoENT)).OrderBy(Function(x) x.PrecioUnitario).ToList
                ElseIf ddlOrdenarPor.SelectedIndex = 4 Then
                    Lista = DirectCast(Session("CatalogoLista"), List(Of ProductoCatalogoENT)).OrderBy(Function(x) x.PrecioUnitario).Reverse.ToList
                End If
                'Session("CatalogoListaFiltrada") = Lista
                'CargarCatalogo()
            ElseIf ddlCatalogoTipo.SelectedIndex > 0 And ddlOrdenarPor.SelectedIndex > 0 Then
                For Each item In DirectCast(Session("CatalogoLista"), List(Of ProductoCatalogoENT))
                    If item.Tipo = ddlCatalogoTipo.SelectedValue Then
                        Lista.Add(item)
                    End If
                Next
                If ddlOrdenarPor.SelectedIndex = 1 Then
                    Lista = Lista.OrderBy(Function(x) x.Nombre).ToList
                ElseIf ddlOrdenarPor.SelectedIndex = 2 Then
                    Lista = Lista.OrderBy(Function(x) x.Nombre).Reverse.ToList
                ElseIf ddlOrdenarPor.SelectedIndex = 3 Then
                    Lista = Lista.OrderBy(Function(x) x.PrecioUnitario).ToList
                ElseIf ddlOrdenarPor.SelectedIndex = 4 Then
                    Lista = Lista.OrderBy(Function(x) x.PrecioUnitario).Reverse.ToList
                End If
                'Session("CatalogoListaFiltrada") = Lista
                'CargarCatalogo()
            End If
            Dim ListaAux As New List(Of ProductoCatalogoENT)
            If txtPrecioMayorA.Value <> "" And txtPrecioMenorA.Value = "" Then
                For Each item In Lista
                    If item.PrecioUnitario >= txtPrecioMayorA.Value Then
                        ListaAux.Add(item)
                    End If
                Next
                Lista = ListaAux
            ElseIf txtPrecioMayorA.Value = "" And txtPrecioMenorA.Value <> "" Then
                For Each item In Lista
                    If item.PrecioUnitario <= txtPrecioMenorA.Value Then
                        ListaAux.Add(item)
                    End If
                Next
                Lista = ListaAux
            ElseIf txtPrecioMayorA.Value <> "" And txtPrecioMenorA.Value <> "" Then
                For Each item In Lista
                    If item.PrecioUnitario >= txtPrecioMayorA.Value And item.PrecioUnitario <= txtPrecioMenorA.Value Then
                        ListaAux.Add(item)
                    End If
                Next
                Lista = ListaAux
            End If
            Session("CatalogoListaFiltrada") = Lista
            CargarCatalogo()
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
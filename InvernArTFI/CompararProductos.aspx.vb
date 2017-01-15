Imports BLL
Imports ENTITIES
Imports System.Globalization

Public Class CompararProductos
    Inherits System.Web.UI.Page
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divCompararProductosControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack And Not Session("ProductosAComparar") Is Nothing Then
                CargarProducto()
                Session("ProductosAComparar") = Nothing
            Else
                Response.Redirect("Inicio.aspx")
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub CargarProducto()
        Dim ListaProductosAComparar = DirectCast(Session("ProductosAComparar"), List(Of ProductoCatalogoENT))
        Dim ListaCatalogo = DirectCast(Session("CatalogoLista"), List(Of ProductoCatalogoENT))
        Dim Cantidad As Integer = 1
        For Each Producto In ListaCatalogo
            For Each ProductoAComparar In ListaProductosAComparar
                If Producto.Id = ProductoAComparar.Id Then
                    Dim containerCatalogo = New HtmlGenericControl("DIV")
                    containerCatalogo.Attributes.Add("class", "preview-panel")
                    Dim imagen = New Image()
                    imagen.ImageUrl = CargarImagen(Producto.Imagen)
                    imagen.CssClass = "img-responsive"
                    containerCatalogo.Controls.Add(imagen)
                    If Cantidad = 1 Then
                        divCompararProductosImagenProducto1.Controls.Add(containerCatalogo)
                    ElseIf Cantidad = 2 Then
                        divCompararProductosImagenProducto2.Controls.Add(containerCatalogo)
                    ElseIf Cantidad = 3 Then
                        divCompararProductosImagenProducto3.Controls.Add(containerCatalogo)
                    End If
                    Dim TituloEncabezado = New HtmlGenericControl("DIV")
                    TituloEncabezado.Attributes.Add("class", "panel-heading")
                    Dim TituloLiteral = New Literal
                    TituloLiteral.Text += "<h3 class=""panel-title"">" & Producto.Nombre & "</h3>"
                    TituloEncabezado.Controls.Add(TituloLiteral)
                    If Cantidad = 1 Then
                        divCompararProductosProducto1.Controls.Add(TituloEncabezado)
                    ElseIf Cantidad = 2 Then
                        divCompararProductosProducto2.Controls.Add(TituloEncabezado)
                    ElseIf Cantidad = 3 Then
                        divCompararProductosProducto3.Controls.Add(TituloEncabezado)
                    End If
                    Dim Texto As String = ""
                    For i = 1 To 3
                        Dim CuerpoDetalles = New HtmlGenericControl("DIV")
                        CuerpoDetalles.Attributes.Add("class", "panel-body")
                        If i = 1 Then
                            CuerpoDetalles.InnerText = "Especificacion: " & Producto.Especificacion
                        ElseIf i = 2 Then
                            CuerpoDetalles.InnerText = "Tipo: " & Producto.Tipo
                        ElseIf i = 3 Then
                            CuerpoDetalles.InnerText = "Precio Unitario: " & Producto.PrecioUnitario
                        End If
                        If Cantidad = 1 Then
                            divCompararProductosProducto1.Controls.Add(CuerpoDetalles)
                        ElseIf Cantidad = 2 Then
                            divCompararProductosProducto2.Controls.Add(CuerpoDetalles)
                        ElseIf Cantidad = 3 Then
                            divCompararProductosProducto3.Controls.Add(CuerpoDetalles)
                        End If
                    Next
                    Cantidad += 1
                End If
            Next
        Next
    End Sub

    Public Function CargarImagen(imgBytes As Byte()) As String
        Dim base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length)
        Return "data:image/jpeg;base64," + base64String
    End Function

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

End Class
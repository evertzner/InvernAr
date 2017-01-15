Imports ENTITIES
Imports BLL
Imports System.Globalization

Public Class GestionCatalogo
    Inherits System.Web.UI.Page
    Dim CatalogoV As New CatalogoVista
    Dim ProductoV As New ProductoVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvGestionCatalogoProductos, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divGestionCatalogoControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                BindData(New ProductoENT)
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub BindData(Filtro As ProductoENT)
        For i = 0 To gvGestionCatalogoProductos.Rows.Count - 1
            CType(gvGestionCatalogoProductos.Rows(i).Cells(0).Controls(1), CheckBox).Checked = False
        Next
        CargarListaRoles(Filtro)
        CargarGridViewRoles()
    End Sub

    Private Sub CargarListaRoles(Filtro As ProductoENT)
        Session("ProductosCatalogo") = CatalogoV.CatalogoBLL.ListarProductosCatalogo(Filtro)
        Session("ListaProductos") = ProductoV.ProductoBLL.ListarProductos
    End Sub

    Private Sub CargarGridViewRoles()
        Dim ListaProductos As New List(Of ProductoENT)
        gvGestionCatalogoProductos.AutoGenerateColumns = False
        ListaProductos = DirectCast(Session("ListaProductos"), List(Of ProductoENT))
        gvGestionCatalogoProductos.DataSource = ListaProductos
        gvGestionCatalogoProductos.DataBind()
        For Each Producto In DirectCast(Session("ProductosCatalogo"), List(Of ProductoCatalogoENT))
            For i = 0 To gvGestionCatalogoProductos.Rows.Count - 1
                If ListaProductos.Item(i).Id = Producto.Id Then
                    CType(gvGestionCatalogoProductos.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True
                    CType(gvGestionCatalogoProductos.Rows(i).Cells(8).Controls(1), TextBox).Text = Producto.Orden
                End If
            Next
        Next
    End Sub

    Private Sub btnGestionCatalogoAceptarCambios_ServerClick(sender As Object, e As EventArgs) Handles btnGestionCatalogoAceptarCambios.ServerClick
        Try
            Dim CamposVacios As Boolean = False
            For i = 0 To gvGestionCatalogoProductos.Rows.Count - 1
                If CType(gvGestionCatalogoProductos.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    If CType(gvGestionCatalogoProductos.Rows(i).Cells(8).Controls(1), TextBox).Text = "" Then
                        CamposVacios = True
                        Exit For
                    Else
                        CamposVacios = False
                    End If
                End If
            Next
            Dim ListaProductosCatalogo As New List(Of ProductoCatalogoENT)
            Dim Producto As New ProductoENT
            ListaProductosCatalogo = DirectCast(Session("ProductosCatalogo"), List(Of ProductoCatalogoENT))
            If CamposVacios = False Then
                For i = 0 To ListaProductosCatalogo.Count - 1
                    CatalogoV.CatalogoBLL.EliminarProductoCatalogo(ListaProductosCatalogo.Item(i))
                Next
                For i = 0 To gvGestionCatalogoProductos.Rows.Count - 1
                    If CType(gvGestionCatalogoProductos.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                        Producto.Codigo = gvGestionCatalogoProductos.Rows(i).Cells(2).Text
                        CatalogoV.CatalogoBLL.AgregarProductoCatalogo(Producto, CType(gvGestionCatalogoProductos.Rows(i).Cells(8).Controls(1), TextBox).Text)
                    End If
                Next
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Modificó el catálogo"))
                BindData(New ProductoENT)
                divGestionCatalagoAlerta.Visible = False
            Else
                divGestionCatalagoAlerta.Visible = True
                divGestionCatalagoAlerta.Attributes.Add("class", "alert alert-danger")
                Traduccion.TraducirAlertas(divGestionCatalagoAlerta, divGestionCatalagoAlerta.ID, DirectCast(Session("Idioma"), CultureInfo))
                'divUsuariosAlerta.InnerText = "Campos orden vacíos"
            End If
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
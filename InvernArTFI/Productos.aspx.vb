Imports BLL
Imports ENTITIES
Imports System.Globalization

Public Class Productos
    Inherits System.Web.UI.Page
    Dim ProductoV As New ProductoVista
    Dim Lista As New List(Of ProductoENT)
    Dim BitacoraV As New BitacoraVista
    Dim TipoProductoV As New TipoProductoVista
    Dim SensorLimiteV As New SensorLimiteVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvProductosProductos, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divProductosControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divProductosAlta.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divProductosListado.Controls, DirectCast(Session("Idioma"), CultureInfo))
            'divProductosAlerta.Visible = False
            If Not IsPostBack Then
                divProductosAlta.Style("display") = ""
                divProductosListado.Style("display") = "none"
                Session("ProductosLista") = Nothing
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
        Session("ProductosLista") = ProductoV.ProductoBLL.ListarProductos
    End Sub

    Private Sub CargarGridView()
        gvProductosProductos.AutoGenerateColumns = False
        Lista = DirectCast(Session("ProductosLista"), List(Of ProductoENT))
        gvProductosProductos.DataSource = Lista
        gvProductosProductos.DataBind()
    End Sub

    Private Sub LlenarListas()
        ddlProductosTipo.Items.Clear()
        For Each item In TipoProductoV.TipoProductoBLL.ListarTipoProducto
            ddlProductosTipo.Items.Add(item.Tipo)
        Next
        ddlProductosTipo.Items.Insert(0, "")
    End Sub

    Private Sub btnProductosConfirmar_ServerClick(sender As Object, e As EventArgs) Handles btnProductosConfirmar.ServerClick
        Try
            Dim CampoVacio As Boolean = False
            For Each c As Control In divProductosAlta.Controls
                If TypeOf (c) Is HtmlInputText Then
                    If DirectCast(c, HtmlInputText).Value = "" Then
                        CampoVacio = True
                        Exit For
                    End If
                End If
            Next
            If ddlProductosTipo.SelectedIndex = 0 Then
                CampoVacio = True
            End If
            If CampoVacio = False Then
                With ProductoV.ProductoENT
                    .Codigo = txtProductosCodigo.Value
                    .Nombre = txtProductosNombre.Value
                    .Especificacion = txtProductosEspecificacion.Value
                    .PrecioUnitario = txtProductosPrecioUnitario.Value
                    .Tipo = ddlProductosTipo.SelectedValue
                    .Imagen = CargarImagen()
                End With
                If ProductoV.ProductoBLL.AgregarProducto(ProductoV.ProductoENT) > 0 Then
                    BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                            "Ha creado el producto " & ProductoV.ProductoENT.Codigo))
                    Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                         Traduccion.TraducirMensaje("divProductosAlerta1", DirectCast(Session("Idioma"), CultureInfo)), _
                         Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                    'divProductosAlerta.Visible = True
                    'divProductosAlerta.Attributes.Add("class", "alert alert-success")
                    ''BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(txtProductosCodigo.Value, "Se cargó el producto " & txtProductosCodigo.Value))
                    'Traduccion.TraducirAlertas(divProductosAlerta, divProductosAlerta.ID & "1", DirectCast(Session("Idioma"), CultureInfo))
                    ''divUsuariosAlerta.InnerText = "Se ha creado el usuario"
                    LimpiarCampos()
                Else
                    Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                       Traduccion.TraducirMensaje("divProductosAlerta0", DirectCast(Session("Idioma"), CultureInfo)), _
                       Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                    'divProductosAlerta.Visible = True
                    'divProductosAlerta.Attributes.Add("class", "alert alert-danger")
                    'Traduccion.TraducirAlertas(divProductosAlerta, divProductosAlerta.ID & "0", DirectCast(Session("Idioma"), CultureInfo))
                    ''divUsuariosAlerta.InnerText = "El codigo ya pertenece a un usuario registrado"
                End If

            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                       Traduccion.TraducirMensaje("divProductosAlerta2", DirectCast(Session("Idioma"), CultureInfo)), _
                       Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                'divProductosAlerta.Visible = True
                'divProductosAlerta.Attributes.Add("class", "alert alert-danger")
                'Traduccion.TraducirAlertas(divProductosAlerta, divProductosAlerta.ID & "2", DirectCast(Session("Idioma"), CultureInfo))
                ''divUsuariosAlerta.InnerText = "No pueden haber campos vacíos"
            End If
            BindData()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Function CargarImagen() As Byte()
        Dim byteImage As Byte() = Nothing
        If fuImagen.PostedFile.ContentLength > 0 Then
            byteImage = New [Byte](fuImagen.PostedFile.ContentLength - 1) {}
            fuImagen.PostedFile.InputStream.Read(byteImage, 0, fuImagen.PostedFile.ContentLength)
        End If
        Return byteImage
    End Function

    Private Sub gvProductosProductos_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvProductosProductos.RowCancelingEdit
        Try
            gvProductosProductos.EditIndex = -1
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvProductosProductos_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvProductosProductos.RowEditing
        Try
            gvProductosProductos.EditIndex = e.NewEditIndex
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvProductosProductos_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvProductosProductos.RowUpdating
        Try
            Dim ListaProductos = CType(Session("ProductosLista"), List(Of ProductoENT))
            Dim row = gvProductosProductos.Rows(e.RowIndex)
            With ListaProductos.Item(row.DataItemIndex)
                .Codigo = (CType((row.Cells(3).Controls(1)), TextBox)).Text
                .Nombre = (CType((row.Cells(4).Controls(1)), TextBox)).Text
                .Especificacion = (CType((row.Cells(5).Controls(1)), TextBox)).Text
                .PrecioUnitario = (CType((row.Cells(6).Controls(1)), TextBox)).Text
                .Tipo = (CType(row.Cells(7).Controls(1), DropDownList).SelectedValue)
                .Imagen = If(DirectCast(row.Cells(8).Controls(1), FileUpload).PostedFile.ContentLength > 0, CargarModificaImagen(row), ObtnerImagenAnterior(row))
            End With
            Dim ProductoENT As New ProductoENT
            With ProductoENT
                .Id = ListaProductos.Item(row.DataItemIndex).Id
                .Codigo = ListaProductos.Item(row.DataItemIndex).Codigo
                .Nombre = ListaProductos.Item(row.DataItemIndex).Nombre
                .Especificacion = ListaProductos.Item(row.DataItemIndex).Especificacion
                .PrecioUnitario = ListaProductos.Item(row.DataItemIndex).PrecioUnitario
                .Tipo = ListaProductos.Item(row.DataItemIndex).Tipo
                .Imagen = ListaProductos.Item(row.DataItemIndex).Imagen
            End With
            ProductoV.ProductoBLL.ModificarProducto(ProductoENT)
            Session("ProductosLista") = ListaProductos
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Ha modificado el producto " & ProductoENT.Id))
            gvProductosProductos.EditIndex = -1
            gvProductosProductos.AutoGenerateColumns = False
            gvProductosProductos.DataSource = ListaProductos
            gvProductosProductos.DataBind()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Function CargarModificaImagen(row As GridViewRow) As Byte()
        Dim byteImage As Byte() = Nothing
        If DirectCast(row.Cells(8).Controls(1), FileUpload).PostedFile.ContentLength > 0 Then
            byteImage = New [Byte](DirectCast(row.Cells(8).Controls(1), FileUpload).PostedFile.ContentLength - 1) {}
            DirectCast(row.Cells(8).Controls(1), FileUpload).PostedFile.InputStream.Read(byteImage, 0, DirectCast(row.Cells(8).Controls(1),  _
                                                                                         FileUpload).PostedFile.ContentLength)
        End If
        Return byteImage
    End Function

    Private Function ObtnerImagenAnterior(row As GridViewRow) As Byte()
        Dim ListaProductos = CType(Session("ProductosLista"), List(Of ProductoENT))
        Return ListaProductos.Item(row.DataItemIndex).Imagen
    End Function

    Private Sub aProductosAlta_ServerClick(sender As Object, e As EventArgs) Handles aProductosAlta.ServerClick
        Try
            divProductosAlta.Style("display") = ""
            divProductosListado.Style("display") = "none"
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub aProductosListado_ServerClick(sender As Object, e As EventArgs) Handles aProductosListado.ServerClick
        Try
            divProductosAlta.Style("display") = "none"
            divProductosListado.Style("display") = ""
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub LimpiarCampos()
        txtProductosCodigo.Value = ""
        txtProductosEspecificacion.Value = ""
        txtProductosNombre.Value = ""
        txtProductosPrecioUnitario.Value = ""
        ddlProductosTipo.SelectedIndex = 0
    End Sub

    Protected Sub btnAsignacionRolesEliminarRol_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim Id = TryCast(sender, ImageButton).CommandArgument
            Dim ListaProductos = CType(Session("ProductosLista"), List(Of ProductoENT))
            Dim ProductoENT As New ProductoENT
            ProductoENT.Id = Id
            ProductoV.ProductoBLL.EliminarProducto(ProductoENT)
            For i = 0 To ListaProductos.Count - 1
                If ListaProductos.Item(i).Id = Id Then
                    ListaProductos.RemoveAt(i)
                    Exit For
                End If
            Next
            If ListaProductos.Count = 0 Then
                Session("ProductosLista") = Nothing
            End If
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Ha eliminado el producto " & ProductoENT.Id))
            gvProductosProductos.AutoGenerateColumns = False
            gvProductosProductos.DataSource = ListaProductos
            gvProductosProductos.DataBind()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Public Function ListarTipoProducto() As List(Of TipoProductoENT)
        Return TipoProductoV.TipoProductoBLL.ListarTipoProducto
    End Function

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

End Class
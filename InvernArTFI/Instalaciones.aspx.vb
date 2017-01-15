Imports ENTITIES
Imports BLL
Imports System.Globalization

Public Class Instalaciones
    Inherits System.Web.UI.Page
    Dim InstalacionV As New InstalacionVista
    Dim InstalacionDetalleV As New InstalacionDetalleVista
    Dim BitacoraV As New BitacoraVista
    Dim UsuarioV As New UsuarioVista
    Dim ProductosV As New ProductoVista
    Dim SensorV As New SensorVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvInstalacionesInstalaciones, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirGrillas(gvInstalacionesInstalacionDetalle, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divInstalacionesControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divInstalacionesInstalaciones.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divInstalacionesInstalacionDetalle.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divInstalacionesAlta.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divInstalacionesListado.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                LimpiarCampos()
                divInstalacionesAlta.Style("display") = ""
                divInstalacionesListado.Style("display") = "none"
                Session("Fila") = Nothing
                divInstalacionesInstalacionDetalle.Style("display") = "none"
                BindData()
            End If
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
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

    Private Sub LlenarListas()
        ddlInstalacionesProductos.Items.Clear()
        For Each item In DirectCast(Session("Productos"), List(Of ProductoENT))
            ddlInstalacionesProductos.Items.Add(item.Codigo)
        Next
        ddlInstalacionesProductos.Items.Insert(0, "")
    End Sub

    Private Sub CargarLista()
        Session("Productos") = ProductosV.ProductoBLL.ListarProductos
        Session("Instalaciones") = InstalacionV.InstalacionBLL.ListarInstalaciones
    End Sub

    Private Sub CargarGridView()
        gvInstalacionesInstalaciones.AutoGenerateColumns = False
        gvInstalacionesInstalaciones.DataSource = DirectCast(Session("Instalaciones"), List(Of InstalacionENT))
        gvInstalacionesInstalaciones.DataBind()
    End Sub

    Private Sub gvInstalacionesInstalaciones_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvInstalacionesInstalaciones.PageIndexChanging
        Try
            gvInstalacionesInstalaciones.PageIndex = e.NewPageIndex
            CargarGridView()
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvInstalacionesInstalaciones_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gvInstalacionesInstalaciones.SelectedIndexChanging
        Try
            divInstalacionesInstalacionDetalle.Style("display") = ""
            If Not Session("Fila") Is Nothing Then
                gvInstalacionesInstalaciones.Rows(Session("Fila")).Style.Remove("background-color")
            End If
            Dim ListaInstalaciones = CType(Session("Instalaciones"), List(Of InstalacionENT))
            InstalacionDetalleV.InstalacionDetalleENT.IdInstalacion = ListaInstalaciones.Item(gvInstalacionesInstalaciones.Rows(e.NewSelectedIndex).DataItemIndex).Id
            Session("InstalacionDetalle") = InstalacionDetalleV.InstalacionDetalleBLL.ListarInstalacionesDetalle(InstalacionDetalleV.InstalacionDetalleENT)
            Session("Fila") = e.NewSelectedIndex
            Session("InstalacionSeleccionada") = InstalacionDetalleV.InstalacionDetalleENT.IdInstalacion
            Dim ListaProductos As New List(Of ProductoENT)
            Dim Existe As Boolean = False
            For Each Producto In DirectCast(Session("Productos"), List(Of ProductoENT))
                For Each ProductoInstalacion In DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
                    If Producto.Id = ProductoInstalacion.IdProducto Then
                        Existe = True
                        Exit For
                    End If
                Next
                If Existe = False Then
                    ListaProductos.Add(Producto)
                End If
                Existe = False
            Next
            ddlInstalacionesProductos.Items.Clear()
            For Each item In ListaProductos
                ddlInstalacionesProductos.Items.Add(item.Codigo)
            Next
            ddlInstalacionesProductos.Items.Insert(0, "")
            gvInstalacionesInstalaciones.Rows(Session("Fila")).Style.Add("background-color", "#449d44")
            gvInstalacionesInstalacionDetalle.AutoGenerateColumns = False
            gvInstalacionesInstalacionDetalle.DataSource = DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
            gvInstalacionesInstalacionDetalle.DataBind()
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exSeleccionarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub RealizarInstalacion_Click(sender As Object, e As EventArgs)
        Try
            Dim IdInstalacion As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            Dim ListaInstalaciones = CType(Session("Instalaciones"), List(Of InstalacionENT))
            For Each Instalacion In ListaInstalaciones
                If Instalacion.Id = IdInstalacion Then
                    If Instalacion.Realizado = True Then
                        AbrirModal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                         Traduccion.TraducirMensaje("Mensaje11", DirectCast(Session("Idioma"), CultureInfo)), _
                         Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                    Else
                        InstalacionV.InstalacionENT.Id = IdInstalacion
                        InstalacionV.InstalacionENT.FechaDeRealizacion = Now
                        InstalacionV.InstalacionBLL.RealizarInstalacion(InstalacionV.InstalacionENT)
                        AbrirModal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                                   Traduccion.TraducirMensaje("Mensaje10", DirectCast(Session("Idioma"), CultureInfo)), _
                                   Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                    End If
                End If
            Next
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    'Private Sub AbrirModal(titulo As String, body As String, boton As String)
    '    lblMasterModalMensajeTitulo.InnerText = titulo
    '    lblMasterModalMensajeMensaje.Text = body
    '    botonNotificacionAceptar.InnerText = boton
    '    ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalInstalacionesMensaje", "$('#modalInstalacionesMensaje').modal({backdrop: 'static', keyboard: false});", True)
    'End Sub

    Private Sub AbrirModal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

    'Private Sub botonNotificacionAceptar_ServerClick(sender As Object, e As EventArgs) Handles botonNotificacionAceptar.ServerClick
    '    Try
    '        Response.Redirect("Instalaciones.aspx")
    '    Catch ex As Exception
    '        AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
    '                  Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
    '                  Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
    '        BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
    '    End Try
    'End Sub

    Private Sub aInstalacionesAlta_ServerClick(sender As Object, e As EventArgs) Handles aInstalacionesAlta.ServerClick
        Try
            divInstalacionesAlta.Style("display") = ""
            divInstalacionesListado.Style("display") = "none"
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub aInstalacionesListado_ServerClick(sender As Object, e As EventArgs) Handles aInstalacionesListado.ServerClick
        Try
            divInstalacionesAlta.Style("display") = "none"
            divInstalacionesListado.Style("display") = ""
            Session("Fila") = Nothing
            divInstalacionesInstalacionDetalle.Style("display") = "none"
            BindData()
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnInstalacionesNuevo_ServerClick(sender As Object, e As EventArgs) Handles btnInstalacionesNuevo.ServerClick
        Try
            If txtInstalacionesIdCliente.Value <> "" And txtInstalacionesFechaDeSolicitud.Value <> "" And txtInstalacionesDatosDeContacto.Value <> "" And _
               txtInstalacionesDomicilioDeInstalacion.Value <> "" Then
                Dim UsuarioExiste As Boolean = False
                UsuarioV.UsuarioENT.ID = txtInstalacionesIdCliente.Value
                For Each Usuario In UsuarioV.UsuarioBLL.ListarUsuarioPorId(UsuarioV.UsuarioENT)
                    If Usuario.ID = txtInstalacionesIdCliente.Value Then
                        UsuarioExiste = True
                        Exit For
                    End If
                Next
                If UsuarioExiste = True Then
                    With InstalacionV.InstalacionENT
                        .IdCliente = txtInstalacionesIdCliente.Value
                        .FechaDeSolicitud = txtInstalacionesFechaDeSolicitud.Value
                        .DatosDeContacto = txtInstalacionesDatosDeContacto.Value
                        .DomicilioDeInstalacion = txtInstalacionesDomicilioDeInstalacion.Value
                        .Observaciones = txtInstalacionesObservaciones.Value
                    End With
                    InstalacionV.InstalacionBLL.AgregarInstalacion(InstalacionV.InstalacionENT)
                    BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                            "Agregó una nueva instalación"))

                    Response.Redirect("Instalaciones.aspx")
                Else
                    AbrirModal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                   Traduccion.TraducirMensaje("Mensaje19", DirectCast(Session("Idioma"), CultureInfo)), _
                   Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                End If
                LimpiarCampos()
            Else
                AbrirModal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                   Traduccion.TraducirMensaje("CamposVacios", DirectCast(Session("Idioma"), CultureInfo)), _
                   Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub btnInstalacionesEliminar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim Id = TryCast(sender, ImageButton).CommandArgument
            Dim ListaInstalaciones = CType(Session("Instalaciones"), List(Of InstalacionENT))
            Dim InstalacionENT As New InstalacionENT
            InstalacionENT.Id = Id
            If InstalacionV.InstalacionBLL.EliminarInstalacion(InstalacionENT) > 0 Then
                For i = 0 To ListaInstalaciones.Count - 1
                    If ListaInstalaciones.Item(i).Id = Id Then
                        ListaInstalaciones.RemoveAt(i)
                        Exit For
                    End If
                Next
                If ListaInstalaciones.Count = 0 Then
                    Session("Instalaciones") = Nothing
                End If
                Session("Fila") = Nothing
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Eliminó la instalación Nº" & InstalacionENT.Id))
                Session("Instalaciones") = ListaInstalaciones
                BindData()
            End If
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub btnInstalacionesProductoEliminar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim Id = TryCast(sender, ImageButton).CommandArgument
            Dim ListaInstalacionesDetalle = CType(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
            Dim InstalacionDetalleENT As New InstalacionDetalleENT
            InstalacionDetalleENT.Id = Id
            If InstalacionDetalleV.InstalacionDetalleBLL.EliminarInstalacionDetalle(InstalacionDetalleENT) > 0 Then
                For i = 0 To ListaInstalacionesDetalle.Count - 1
                    If ListaInstalacionesDetalle.Item(i).Id = Id Then
                        ListaInstalacionesDetalle.RemoveAt(i)
                        Exit For
                    End If
                Next
                If ListaInstalacionesDetalle.Count = 0 Then
                    Session("InstalacionDetalle") = Nothing
                End If
                Session("Fila") = Nothing
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Eliminó la instalación detalle Nº" & InstalacionDetalleENT.Id))
                Session("InstalacionDetalle") = ListaInstalacionesDetalle
            End If
            Dim ListaProductos As New List(Of ProductoENT)
            Dim Existe As Boolean = False
            For Each Producto In DirectCast(Session("Productos"), List(Of ProductoENT))
                For Each ProductoInstalacion In DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
                    If Producto.Id = ProductoInstalacion.IdProducto Then
                        Existe = True
                        Exit For
                    End If
                Next
                If Existe = False Then
                    ListaProductos.Add(Producto)
                End If
                Existe = False
            Next
            ddlInstalacionesProductos.Items.Clear()
            For Each item In ListaProductos
                ddlInstalacionesProductos.Items.Add(item.Codigo)
            Next
            ddlInstalacionesProductos.Items.Insert(0, "")
            gvInstalacionesInstalacionDetalle.AutoGenerateColumns = False
            gvInstalacionesInstalacionDetalle.DataSource = DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
            gvInstalacionesInstalacionDetalle.DataBind()
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnInstalacionesProductosNuevo_ServerClick(sender As Object, e As EventArgs) Handles btnInstalacionesProductosNuevo.ServerClick
        Try
            If ddlInstalacionesProductos.SelectedIndex > 0 And txtInstalacionesCantidadProductos.Value <> "" Then
                With InstalacionDetalleV.InstalacionDetalleENT
                    .IdInstalacion = Session("InstalacionSeleccionada")
                    For Each Producto In DirectCast(Session("Productos"), List(Of ProductoENT))
                        If Producto.Codigo = ddlInstalacionesProductos.SelectedValue Then
                            .IdProducto = Producto.Id
                            .Producto = Producto.Nombre
                            If Producto.Tipo = "Sensor" Then
                                With SensorV.SensorENT
                                    .IdInstalacion = Session("InstalacionSeleccionada")
                                    .IdProducto = Producto.Id
                                End With
                                For i = 0 To txtInstalacionesCantidadProductos.Value - 1
                                    SensorV.SensorBLL.AgregarSensores(SensorV.SensorENT)
                                Next
                            End If
                        End If
                    Next
                    .Cantidad = txtInstalacionesCantidadProductos.Value

                End With
                InstalacionDetalleV.InstalacionDetalleBLL.AgregarInstalacionDetalle(InstalacionDetalleV.InstalacionDetalleENT)
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Agregó un nuevo producto a la instalación Nº " & InstalacionDetalleV.InstalacionDetalleENT.IdInstalacion))
                Session("InstalacionDetalle") = InstalacionDetalleV.InstalacionDetalleBLL.ListarInstalacionesDetalle(InstalacionDetalleV.InstalacionDetalleENT)
                gvInstalacionesInstalacionDetalle.AutoGenerateColumns = False
                gvInstalacionesInstalacionDetalle.DataSource = DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
                gvInstalacionesInstalacionDetalle.DataBind()
                Dim ListaProductos As New List(Of ProductoENT)
                Dim Existe As Boolean = False
                For Each Producto In DirectCast(Session("Productos"), List(Of ProductoENT))
                    For Each ProductoInstalacion In DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
                        If Producto.Id = ProductoInstalacion.IdProducto Then
                            Existe = True
                            Exit For
                        End If
                    Next
                    If Existe = False Then
                        ListaProductos.Add(Producto)
                    End If
                    Existe = False
                Next
                ddlInstalacionesProductos.Items.Clear()
                For Each item In ListaProductos
                    ddlInstalacionesProductos.Items.Add(item.Codigo)
                Next
                ddlInstalacionesProductos.Items.Insert(0, "")
                LimpiarCampos()
            Else
                AbrirModal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                   Traduccion.TraducirMensaje("CamposVacios", DirectCast(Session("Idioma"), CultureInfo)), _
                   Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvInstalacionesInstalaciones_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvInstalacionesInstalaciones.RowCancelingEdit
        Try
            gvInstalacionesInstalaciones.EditIndex = -1
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvInstalacionesInstalaciones_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvInstalacionesInstalaciones.RowEditing
        Try
            gvInstalacionesInstalaciones.EditIndex = e.NewEditIndex
            divInstalacionesInstalacionDetalle.Style("display") = "none"
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvInstalacionesInstalaciones_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvInstalacionesInstalaciones.RowUpdating
        Try
            Dim ListaInstalaciones = CType(Session("Instalaciones"), List(Of InstalacionENT))
            Dim row = gvInstalacionesInstalaciones.Rows(e.RowIndex)
            With ListaInstalaciones.Item(row.DataItemIndex)
                .Id = DirectCast(Session("Instalaciones"), List(Of InstalacionENT))(gvInstalacionesInstalaciones.Rows(e.RowIndex).DataItemIndex).Id
                .DatosDeContacto = CType(row.Cells(5).Controls(1), TextBox).Text
                .DomicilioDeInstalacion = CType(row.Cells(6).Controls(1), TextBox).Text
                .Observaciones = CType(row.Cells(7).Controls(1), TextBox).Text
            End With
            Dim InstalacionENT As New InstalacionENT
            With InstalacionENT
                .Id = ListaInstalaciones.Item(row.DataItemIndex).Id
                .DatosDeContacto = ListaInstalaciones.Item(row.DataItemIndex).DatosDeContacto
                .DomicilioDeInstalacion = ListaInstalaciones.Item(row.DataItemIndex).DomicilioDeInstalacion
                .Observaciones = ListaInstalaciones.Item(row.DataItemIndex).Observaciones
            End With
            InstalacionV.InstalacionBLL.ModificarInstalacion(InstalacionENT)
            gvInstalacionesInstalaciones.EditIndex = -1
            BindData()
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Modificó la instalación Nº " & InstalacionENT.Id))
            LimpiarCampos()
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvInstalacionesInstalacionDetalle_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvInstalacionesInstalacionDetalle.PageIndexChanging
        Try
            gvInstalacionesInstalacionDetalle.PageIndex = e.NewPageIndex
            CargarGridView()
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvInstalacionesInstalacionDetalle_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvInstalacionesInstalacionDetalle.RowCancelingEdit
        Try
            gvInstalacionesInstalacionDetalle.EditIndex = -1
            gvInstalacionesInstalacionDetalle.AutoGenerateColumns = False
            gvInstalacionesInstalacionDetalle.DataSource = DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
            gvInstalacionesInstalacionDetalle.DataBind()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvInstalacionesInstalacionDetalle_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvInstalacionesInstalacionDetalle.RowEditing
        Try
            gvInstalacionesInstalacionDetalle.EditIndex = e.NewEditIndex
            gvInstalacionesInstalacionDetalle.AutoGenerateColumns = False
            gvInstalacionesInstalacionDetalle.DataSource = DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
            gvInstalacionesInstalacionDetalle.DataBind()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvInstalacionesInstalacionDetalle_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvInstalacionesInstalacionDetalle.RowUpdating
        Try
            Dim ListaInstalacionDetalle = CType(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
            Dim row = gvInstalacionesInstalacionDetalle.Rows(e.RowIndex)
            With ListaInstalacionDetalle.Item(row.DataItemIndex)
                .Id = DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))(gvInstalacionesInstalacionDetalle.Rows(e.RowIndex).DataItemIndex).Id
                .IdProducto = DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))(gvInstalacionesInstalacionDetalle.Rows(e.RowIndex).DataItemIndex).IdProducto
                .Cantidad = CType(row.Cells(6).Controls(1), TextBox).Text
            End With
            Dim InstalacionDetalleENT As New InstalacionDetalleENT
            With InstalacionDetalleENT
                .Id = ListaInstalacionDetalle.Item(row.DataItemIndex).Id
                .IdProducto = ListaInstalacionDetalle.Item(row.DataItemIndex).IdProducto
                .Cantidad = ListaInstalacionDetalle.Item(row.DataItemIndex).Cantidad
            End With
            For Each Producto In DirectCast(Session("Productos"), List(Of ProductoENT))
                If Producto.Id = InstalacionDetalleENT.IdProducto Then
                    If Producto.Tipo = "Sensor" Then
                        With SensorV.SensorENT
                            .IdInstalacion = Session("InstalacionSeleccionada")
                            .IdProducto = InstalacionDetalleENT.IdProducto
                        End With
                        SensorV.SensorBLL.EliminarSensores(SensorV.SensorENT)
                        For i = 0 To InstalacionDetalleENT.Cantidad - 1
                            SensorV.SensorBLL.AgregarSensores(SensorV.SensorENT)
                        Next
                    End If
                End If
            Next
            InstalacionDetalleV.InstalacionDetalleBLL.ModificarInstalacionDetallte(InstalacionDetalleENT)
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Modificó el producto " & InstalacionDetalleENT.IdProducto & _
                                                                                    " de la instalacion Nº " & Session("InstalacionSeleccionada")))
            LimpiarCampos()
            gvInstalacionesInstalacionDetalle.EditIndex = -1
            Session("InstalacionDetalle") = InstalacionDetalleV.InstalacionDetalleBLL.ListarInstalacionesDetalle(InstalacionDetalleV.InstalacionDetalleENT)
            gvInstalacionesInstalacionDetalle.AutoGenerateColumns = False
            gvInstalacionesInstalacionDetalle.DataSource = DirectCast(Session("InstalacionDetalle"), List(Of InstalacionDetalleENT))
            gvInstalacionesInstalacionDetalle.DataBind()
        Catch ex As Exception
            AbrirModal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub LimpiarCampos()
        ddlInstalacionesProductos.SelectedIndex = 0
        txtInstalacionesCantidadProductos.Value = ""
        txtInstalacionesDatosDeContacto.Value = ""
        txtInstalacionesDomicilioDeInstalacion.Value = ""
        txtInstalacionesFechaDeSolicitud.Value = ""
        txtInstalacionesIdCliente.Value = ""
        txtInstalacionesObservaciones.Value = ""
    End Sub
End Class
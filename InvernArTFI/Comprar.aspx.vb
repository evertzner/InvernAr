Imports BLL
Imports ENTITIES
Imports System.Globalization
Imports SelectPdf
Imports System.Net.Mail
Imports System.IO

Public Class Comprar
    Inherits System.Web.UI.Page
    Dim FacturaDetalleV As New FacturaDetalleVista
    Dim FacturaV As New FacturaVista
    Dim TarjetaV As New TarjetaVista
    Dim NotaCreditoV As New NotaCreditoVista
    Dim CuentaCorrienteV As New CuentaCorrienteVista
    Dim UsuarioV As New UsuarioVista
    Dim OpinionCompraV As New OpinionCompraVista
    Dim PedidoV As New PedidoVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvComprarCarrito, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divComprarControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divComprarMedioPago.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divComprarCarrito.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divComprarVerificarTarjeta.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divComprarNotaCredito.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack And Session("Carrito") IsNot Nothing Then
                divComprarMedioPago.Style("display") = "none"
                divComprarVerificarTarjeta.Style("display") = "none"
                divComprarNotaCredito.Style("display") = "none"
                BindData()
            End If
            If Session("Carrito") Is Nothing Then
                divComprarCarrito.Style("display") = "none"
                divComprarMedioPago.Style("display") = "none"
                divComprarVerificarTarjeta.Style("display") = "none"
                divComprarNotaCredito.Style("display") = "none"
            Else
                divComprarCarrito.Style("display") = ""
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
        Dim Existe As Boolean = False
        Dim ListaFacturaDetalle As New List(Of FacturaDetalleENT)
        For Each Producto In DirectCast(Session("Carrito"), List(Of FacturaDetalleENT))
            If Not ListaFacturaDetalle.Count = 0 Then
                For Each FacturaDetalle In ListaFacturaDetalle
                    If Producto.IdProducto = FacturaDetalle.IdProducto Then
                        FacturaDetalle.Cantidad += 1
                        Existe = True
                    End If
                Next
                If Existe = False Then
                    ListaFacturaDetalle.Add(Producto)
                End If
            Else
                ListaFacturaDetalle.Add(Producto)
            End If
            Existe = False
        Next
        For Each FacturaDetalle In ListaFacturaDetalle
            FacturaDetalle.Precio = FacturaDetalle.PrecioUnitario * FacturaDetalle.Cantidad
        Next
        Session("Carrito") = ListaFacturaDetalle
        Session("Anos") = TarjetaV.TarjetaBLL.ListarAnos
        Session("Meses") = TarjetaV.TarjetaBLL.ListarMeses
        Session("Tarjetas") = TarjetaV.TarjetaBLL.ListarTarjetas
        Session("TarjetasRegistradas") = TarjetaV.TarjetaBLL.ValidarTarjeta
        NotaCreditoV.NotaCreditoENT.IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
        Session("NotasCredito") = NotaCreditoV.NotaCreditoBLL.ListarNotasCredito(NotaCreditoV.NotaCreditoENT)
    End Sub

    Private Sub CargarGridView()
        gvComprarCarrito.AutoGenerateColumns = False
        gvComprarCarrito.DataSource = DirectCast(Session("Carrito"), List(Of FacturaDetalleENT))
        gvComprarCarrito.DataBind()
        Session("Total") = Nothing
        For Each Producto In DirectCast(Session("Carrito"), List(Of FacturaDetalleENT))
            Session("Total") += Producto.Precio
        Next
        lblComprarTotal.InnerText = "Total: " & Session("Total")
    End Sub

    Private Sub LlenarListas()
        ddlComprarMedioPago.Items.Clear()
        ddlComprarAno.Items.Clear()
        ddlComprarMes.Items.Clear()
        ddlComprarTarjeta.Items.Clear()
        For Each Ano In DirectCast(Session("Anos"), List(Of AnoENT))
            ddlComprarAno.Items.Add(Ano.Ano)
        Next
        For Each Mes In DirectCast(Session("Meses"), List(Of MesENT))
            ddlComprarMes.Items.Add(Mes.Mes)
        Next
        For Each Tarjeta In DirectCast(Session("Tarjetas"), List(Of TarjetaENT))
            ddlComprarTarjeta.Items.Add(Tarjeta.Tarjeta)
        Next
        For Each NotaCredito In DirectCast(Session("NotasCredito"), List(Of NotaCreditoENT))
            ddlComprarMedioPago.Items.Add("Nota de credito Nº " & NotaCredito.Id & " Saldo - " & NotaCredito.Saldo)
        Next
        ddlComprarMedioPago.Items.Insert(0, "")
        ddlComprarMedioPago.Items.Insert(1, "Tarjeta de crédito")
        ddlComprarAno.Items.Insert(0, "")
        ddlComprarMes.Items.Insert(0, "")
        ddlComprarTarjeta.Items.Insert(0, "")
    End Sub

    Private Sub gvComprarCarrito_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvComprarCarrito.PageIndexChanging
        Try
            gvComprarCarrito.PageIndex = e.NewPageIndex
            gvComprarCarrito.AutoGenerateColumns = False
            CargarGridView()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvComprarCarrito_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvComprarCarrito.RowCancelingEdit
        Try
            gvComprarCarrito.EditIndex = -1
            CargarGridView()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvComprarCarrito_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvComprarCarrito.RowEditing
        Try
            gvComprarCarrito.EditIndex = e.NewEditIndex
            CargarGridView()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvComprarCarrito_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvComprarCarrito.RowUpdating
        Try
            Dim row = gvComprarCarrito.Rows(e.RowIndex)
            Dim ListaFacturaDetalle = CType(Session("Carrito"), List(Of FacturaDetalleENT))
            ListaFacturaDetalle.Item(row.DataItemIndex).Cantidad = (CType((row.Cells(7).Controls(1)), TextBox)).Text
            For Each FacturaDetalle In ListaFacturaDetalle
                FacturaDetalle.Precio = FacturaDetalle.PrecioUnitario * FacturaDetalle.Cantidad
            Next
            Session("Carrito") = ListaFacturaDetalle
            gvComprarCarrito.EditIndex = -1
            CargarGridView()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvComprarCarrito_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvComprarCarrito.RowDeleting
        Try
            Dim ListaFacturaDetalle = CType(Session("Carrito"), List(Of FacturaDetalleENT))
            ListaFacturaDetalle.RemoveAt(e.RowIndex)
            If ListaFacturaDetalle.Count > 0 Then
                Session("Carrito") = ListaFacturaDetalle
                CargarGridView()
            Else
                Session("Carrito") = Nothing
                Response.Redirect("Catalogo.aspx")
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnComprarSiguiente1_ServerClick(sender As Object, e As EventArgs) Handles btnComprarSiguiente1.ServerClick
        Try
            divComprarMedioPago.Style("display") = ""
            btnComprarSiguiente1.Style("display") = "none"
            LimpiarCampos(1)
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnComprarAnterior2_ServerClick(sender As Object, e As EventArgs) Handles btnComprarAnterior2.ServerClick
        Try
            divComprarMedioPago.Style("display") = "none"
            divComprarVerificarTarjeta.Style("display") = "none"
            divComprarNotaCredito.Style("display") = "none"
            btnComprarSiguiente1.Style("display") = ""
            LimpiarCampos(1)
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnComprarSiguiente2_ServerClick(sender As Object, e As EventArgs) Handles btnComprarSiguiente2.ServerClick
        Try
            Dim ListaNotasCredito = CType(Session("NotasCredito"), List(Of NotaCreditoENT))
            If ddlComprarMedioPago.SelectedIndex = 1 Then
                divComprarVerificarTarjeta.Style("display") = ""
                btnComprarAnterior3.Style("display") = ""
                btnComprarComprar3.Style("display") = ""
                btnComprarAnterior2.Style("display") = "none"
                btnComprarSiguiente2.Style("display") = "none"
                ddlComprarMedioPago.Attributes.Add("disabled", "disabled")
            ElseIf ddlComprarMedioPago.SelectedIndex > 1 Then
                divComprarNotaCredito.Style("display") = ""
                btnComprarAnterior4.Style("display") = ""
                btnComprarComprar4.Style("display") = ""
                btnComprarAnterior2.Style("display") = "none"
                btnComprarSiguiente2.Style("display") = "none"
                Dim NotaCredito As New NotaCreditoENT
                NotaCredito = ListaNotasCredito.Item(ddlComprarMedioPago.SelectedIndex - 2)
                Session("NotaCreditoElegida") = NotaCredito
                If Session("Total") > NotaCredito.Saldo Then
                    'Session("Total") -= NotaCredito.Saldo
                    lblComprarNotaCredito.InnerText = "Usted seleccionó la Nota de Crédito Nº " & NotaCredito.Id & " con un Saldo de " & NotaCredito.Saldo & _
                        ". Deberá abonar un total de $" & Session("Total") - NotaCredito.Saldo & " con tarjeta de crédito u otra nota de débito."
                Else
                    lblComprarNotaCredito.InnerText = "Usted seleccionó la Nota de Crédito Nº " & NotaCredito.Id & " con un Saldo de " & NotaCredito.Saldo
                End If
                ddlComprarMedioPago.Attributes.Add("disabled", "disabled")
            ElseIf ddlComprarMedioPago.SelectedIndex = 0 Then
                divComprarNotaCredito.Style("display") = "none"
                divComprarVerificarTarjeta.Style("display") = "none"
                btnComprarAnterior2.Style("display") = ""
                btnComprarSiguiente2.Style("display") = ""
                ddlComprarMedioPago.Attributes.Remove("disabled")
            End If
            LimpiarCampos(2)
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnComprarAnterior3_ServerClick(sender As Object, e As EventArgs) Handles btnComprarAnterior3.ServerClick
        Try
            divComprarVerificarTarjeta.Style("display") = "none"
            btnComprarAnterior2.Style("display") = ""
            btnComprarSiguiente2.Style("display") = ""
            LimpiarCampos(2)
            ddlComprarMedioPago.Attributes.Remove("disabled")
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnComprarComprar3_ServerClick(sender As Object, e As EventArgs) Handles btnComprarComprar3.ServerClick
        Try
            Dim CampoVacio As Boolean = False
            For Each c As Control In divComprarVerificarTarjeta.Controls
                If TypeOf (c) Is HtmlInputText Then
                    If DirectCast(c, HtmlInputText).Value = "" Then
                        CampoVacio = True
                        Exit For
                    End If
                End If
            Next
            If CampoVacio = False Then
                If ddlComprarTarjeta.SelectedValue <> "" And ddlComprarAno.SelectedValue <> "" And ddlComprarMes.SelectedValue <> "" Then
                    If ValidarTarjeta() = True Then
                        Dim NumeroFactura As Integer
                        If Session("Continuacion") = False Then
                            With FacturaV.FacturaENT
                                .IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
                                .Total = Session("Total")
                                .Fecha = Now
                            End With
                            FacturaV.FacturaBLL.ComprarFactura(FacturaV.FacturaENT)
                            NumeroFactura = FacturaV.FacturaBLL.SeleccionarNumeroFactura
                        Else
                            NumeroFactura = Session("IdFactura")
                        End If
                        With CuentaCorrienteV.CuentaCorrienteENT
                            .IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
                            .IdFactura = NumeroFactura
                            .IdNotaCredito = Nothing
                            .Motivo = Nothing
                            .Fecha = Now
                            .Debito = Session("Total")
                            .Credito = Nothing
                        End With
                        CuentaCorrienteV.CuentaCorrienteBLL.RegistrarCuentaCorriente(CuentaCorrienteV.CuentaCorrienteENT)
                        For Each Producto In DirectCast(Session("Carrito"), List(Of FacturaDetalleENT))
                            With FacturaDetalleV.FacturaDetalleENT
                                .IdFactura = NumeroFactura
                                .IdProducto = Producto.IdProducto
                                .Precio = Producto.Precio
                                .Cantidad = Producto.Cantidad
                            End With
                            FacturaDetalleV.FacturaDetalleBLL.ComprarDetalleFactura(FacturaDetalleV.FacturaDetalleENT)
                        Next
                        With UsuarioV.UsuarioENT
                            .CUIT = DirectCast(Session("Usuario"), UsuarioENT).CUIT
                            .Nombre = DirectCast(Session("Usuario"), UsuarioENT).Nombre
                            .Apellido = DirectCast(Session("Usuario"), UsuarioENT).Apellido
                            .CorreoElectronico = DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico
                            .Domicilio = DirectCast(Session("Usuario"), UsuarioENT).Domicilio
                            .Localidad = DirectCast(Session("Usuario"), UsuarioENT).Localidad
                            .Provincia = DirectCast(Session("Usuario"), UsuarioENT).Provincia
                        End With
                        With PedidoV.PedidoENT
                            .IdFactura = NumeroFactura
                            .Estado = "Iniciado"
                            .Fecha = Now
                        End With
                        PedidoV.PedidoBLL.ModificarPedido(PedidoV.PedidoENT)
                        BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                                "Realizó una compra con tarjeta de crédito"))
                        ArmarPDF(NumeroFactura, CuentaCorrienteV.CuentaCorrienteENT.Fecha, Session("Total"), UsuarioV.UsuarioENT.Apellido & ", " & _
                                 UsuarioV.UsuarioENT.Nombre, UsuarioV.UsuarioENT.CUIT, UsuarioV.UsuarioENT.CorreoElectronico, UsuarioV.UsuarioENT.Domicilio, _
                                 UsuarioV.UsuarioENT.Localidad, UsuarioV.UsuarioENT.Provincia)
                        Session("Continuacion") = False
                        Session("Total") = Nothing
                        LimpiarCampos(2)
                        Session("Carrito") = Nothing
                        AbrirmodalOpinion(Traduccion.TraducirMensaje("modalComprarFichaOpinionTitle", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("lblmodalComprarFichaOpinionDificultad", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("lblmodalComprarFichaOpinionDiseño", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("lblmodalComprarFichaOpinionRetorno", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("btnComprarModalAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                    Else
                        Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("ComprobarDatosTarjeta", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                    End If
                Else
                    Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("ListasDesplegables", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                End If
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

    Private Sub btnComprarAnterior4_ServerClick(sender As Object, e As EventArgs) Handles btnComprarAnterior4.ServerClick
        Try
            divComprarNotaCredito.Style("display") = "none"
            btnComprarAnterior2.Style("display") = ""
            btnComprarSiguiente2.Style("display") = ""
            LimpiarCampos(2)
            ddlComprarMedioPago.Attributes.Remove("disabled")
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnComprarComprar4_ServerClick(sender As Object, e As EventArgs) Handles btnComprarComprar4.ServerClick
        Try
            Session("Continuacion") = False
            With FacturaV.FacturaENT
                .IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
                .Total = Session("Total")
                .Fecha = Now
            End With
            FacturaV.FacturaBLL.ComprarFactura(FacturaV.FacturaENT)
            Dim NumeroFactura As Integer = FacturaV.FacturaBLL.SeleccionarNumeroFactura
            For Each Producto In DirectCast(Session("Carrito"), List(Of FacturaDetalleENT))
                With FacturaDetalleV.FacturaDetalleENT
                    .IdFactura = NumeroFactura
                    .IdProducto = Producto.IdProducto
                    .Precio = Producto.Precio
                    .Cantidad = Producto.Cantidad
                End With
                FacturaDetalleV.FacturaDetalleBLL.ComprarDetalleFactura(FacturaDetalleV.FacturaDetalleENT)
            Next
            Dim NotaCredito As New NotaCreditoENT
            NotaCredito = Session("NotaCreditoElegida")
            If Session("Total") <= NotaCredito.Saldo Then
                With NotaCreditoV.NotaCreditoENT
                    .Id = NotaCredito.Id
                    .Saldo = Session("Total")
                End With
                NotaCreditoV.NotaCreditoBLL.ActualizarNotaCredito(NotaCreditoV.NotaCreditoENT)
                If Session("Total") = NotaCredito.Saldo Then
                    With CuentaCorrienteV.CuentaCorrienteENT
                        .IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
                        .IdFactura = NumeroFactura
                        .IdNotaCredito = NotaCredito.Id
                        .Motivo = NotaCredito.Motivo
                        .Fecha = Now
                        .Debito = Session("Total")
                        .Credito = Nothing
                    End With
                    CuentaCorrienteV.CuentaCorrienteBLL.RegistrarCuentaCorriente(CuentaCorrienteV.CuentaCorrienteENT)
                ElseIf Session("Total") < NotaCredito.Saldo Then
                    With CuentaCorrienteV.CuentaCorrienteENT
                        .IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
                        .IdFactura = NumeroFactura
                        .IdNotaCredito = NotaCredito.Id
                        .Fecha = Now
                        .Debito = Session("Total")
                        .Credito = Nothing
                    End With
                    CuentaCorrienteV.CuentaCorrienteBLL.RegistrarCuentaCorriente(CuentaCorrienteV.CuentaCorrienteENT)
                    With CuentaCorrienteV.CuentaCorrienteENT
                        .IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
                        .IdFactura = NumeroFactura
                        .IdNotaCredito = NotaCredito.Id
                        .Fecha = Now
                        .Debito = Nothing
                        .Credito = NotaCredito.Saldo - Session("Total")
                    End With
                    CuentaCorrienteV.CuentaCorrienteBLL.RegistrarCuentaCorriente(CuentaCorrienteV.CuentaCorrienteENT)
                End If
                With UsuarioV.UsuarioENT
                    .CUIT = DirectCast(Session("Usuario"), UsuarioENT).CUIT
                    .Nombre = DirectCast(Session("Usuario"), UsuarioENT).Nombre
                    .Apellido = DirectCast(Session("Usuario"), UsuarioENT).Apellido
                    .CorreoElectronico = DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico
                    .Domicilio = DirectCast(Session("Usuario"), UsuarioENT).Domicilio
                    .Localidad = DirectCast(Session("Usuario"), UsuarioENT).Localidad
                    .Provincia = DirectCast(Session("Usuario"), UsuarioENT).Provincia
                End With
                With PedidoV.PedidoENT
                    .IdFactura = NumeroFactura
                    .Estado = "Iniciado"
                    .Fecha = Now
                End With
                PedidoV.PedidoBLL.ModificarPedido(PedidoV.PedidoENT)
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Realizó una compra con nota de crédito"))
                ArmarPDF(NumeroFactura, CuentaCorrienteV.CuentaCorrienteENT.Fecha, Session("Total"), UsuarioV.UsuarioENT.Apellido & ", " & _
                                 UsuarioV.UsuarioENT.Nombre, UsuarioV.UsuarioENT.CUIT, UsuarioV.UsuarioENT.CorreoElectronico, UsuarioV.UsuarioENT.Domicilio, _
                                 UsuarioV.UsuarioENT.Localidad, UsuarioV.UsuarioENT.Provincia)
                Session("Total") = Nothing
                Session("Carrito") = Nothing
                AbrirmodalOpinion(Traduccion.TraducirMensaje("modalComprarFichaOpinionTitle", DirectCast(Session("Idioma"), CultureInfo)), _
                            Traduccion.TraducirMensaje("lblmodalComprarFichaOpinionDificultad", DirectCast(Session("Idioma"), CultureInfo)), _
                            Traduccion.TraducirMensaje("lblmodalComprarFichaOpinionDiseño", DirectCast(Session("Idioma"), CultureInfo)), _
                            Traduccion.TraducirMensaje("lblmodalComprarFichaOpinionRetorno", DirectCast(Session("Idioma"), CultureInfo)), _
                            Traduccion.TraducirMensaje("btnComprarModalAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            Else
                With NotaCreditoV.NotaCreditoENT
                    .Id = NotaCredito.Id
                    .Saldo = NotaCredito.Saldo
                End With
                NotaCreditoV.NotaCreditoBLL.ActualizarNotaCredito(NotaCreditoV.NotaCreditoENT)
                With CuentaCorrienteV.CuentaCorrienteENT
                    .IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
                    .IdFactura = NumeroFactura
                    .IdNotaCredito = NotaCredito.Id
                    .Fecha = Now
                    .Debito = Session("Total")
                    .Credito = Nothing
                End With
                Session("Continuacion") = True
                Session("IdFactura") = NumeroFactura
                CuentaCorrienteV.CuentaCorrienteBLL.RegistrarCuentaCorriente(CuentaCorrienteV.CuentaCorrienteENT)
                Session("Total") = Session("Total") - NotaCredito.Saldo
                btnComprarAnterior4.Style("display") = "none"
                btnComprarComprar4.Style("display") = "none"
                BindData()
                ddlComprarMedioPago.Attributes.Remove("disabled")
                divComprarMedioPago.Style("display") = ""
                btnComprarAnterior2.Style("display") = ""
                btnComprarSiguiente2.Style("display") = ""
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub LimpiarCampos(Paso As Integer)
        Select Case Paso
            Case 1
                txtComprarCodigo.Value = ""
                txtComprarNombre.Value = ""
                txtComprarNumero.Value = ""
                ddlComprarAno.SelectedIndex = 0
                ddlComprarMedioPago.SelectedIndex = 0
                ddlComprarMes.SelectedIndex = 0
                ddlComprarTarjeta.SelectedIndex = 0
            Case 2
                txtComprarCodigo.Value = ""
                txtComprarNombre.Value = ""
                txtComprarNumero.Value = ""
                ddlComprarAno.SelectedIndex = 0
                ddlComprarMes.SelectedIndex = 0
                ddlComprarTarjeta.SelectedIndex = 0
        End Select
    End Sub

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

    Private Function ValidarTarjeta() As Boolean
        Dim Valido As Boolean
        'Dim Condicion As Integer = 0
        Dim ListaTarjetas = CType(Session("Tarjetas"), List(Of TarjetaENT))
        Dim ListaTarjetasRegistradas = CType(Session("TarjetasRegistradas"), List(Of TarjetaRegistradaENT))
        Dim ListaAnos = CType(Session("Anos"), List(Of AnoENT))
        Dim ListaMeses = CType(Session("Meses"), List(Of MesENT))
        Dim Tarjeta As New TarjetaENT
        Dim TarjetaRegistrada As New TarjetaRegistradaENT
        If ListaTarjetas.Item(ddlComprarTarjeta.SelectedIndex - 1).Id = 1 Or ListaTarjetas.Item(ddlComprarTarjeta.SelectedIndex - 1).Id = 2 Then
            If txtComprarNumero.Value.Length = 16 Then
                If txtComprarCodigo.Value.Length = 3 Then
                    For Each Item In ListaTarjetasRegistradas
                        If Item.Numero = txtComprarNumero.Value Then
                            TarjetaRegistrada = Item
                            Exit For
                        End If
                    Next
                    If (TarjetaRegistrada.AnoVencimiento >= Now.Year) Or (TarjetaRegistrada.AnoVencimiento = Now.Year And _
                                                                          TarjetaRegistrada.MesVencimiento >= Now.Month) Then
                        If TarjetaRegistrada.CodigoSeguridad = txtComprarCodigo.Value And _
                            TarjetaRegistrada.AnoVencimiento = ListaAnos.Item(ddlComprarAno.SelectedIndex - 1).Ano And _
                            TarjetaRegistrada.MesVencimiento = ListaMeses.Item(ddlComprarMes.SelectedIndex - 1).Mes.Substring(0, 2) And _
                            TarjetaRegistrada.IdTarjeta = ListaTarjetas.Item(ddlComprarMes.SelectedIndex - 1).Id Then
                            Valido = True
                        Else
                            Valido = False
                        End If
                    Else
                        Valido = False
                    End If
                Else
                    Valido = False
                End If
            Else
                Valido = False
            End If
        ElseIf ListaTarjetas.Item(ddlComprarTarjeta.SelectedIndex - 1).Id = 3 Then
            If txtComprarNumero.Value.Length = 15 Then
                If txtComprarCodigo.Value.Length = 4 Then
                    For Each Item In ListaTarjetasRegistradas
                        If Item.Numero = txtComprarNumero.Value Then
                            TarjetaRegistrada = Item
                            Exit For
                        End If
                    Next
                    If TarjetaRegistrada.CodigoSeguridad = txtComprarCodigo.Value And _
                        TarjetaRegistrada.AnoVencimiento = ListaAnos.Item(ddlComprarAno.SelectedIndex - 1).Ano And _
                        TarjetaRegistrada.MesVencimiento = ListaMeses.Item(ddlComprarMes.SelectedIndex - 1).Mes.Substring(0, 2) And _
                        TarjetaRegistrada.IdTarjeta = ListaTarjetas.Item(ddlComprarMes.SelectedIndex - 1).Id Then
                        Valido = True
                    Else
                        Valido = False
                    End If
                Else
                    Valido = False
                End If
            Else
                Valido = False
            End If
        End If
        Return Valido
    End Function

    Protected Sub ArmarPDF(IdFactura As Integer, Fecha As DateTime, Total As Double, NombreCompleto As String, _
                           CUIT As String, CorreoElectronico As String, Domicilio As String, Localidad As String, Provincia As String)
        ' read parameters from the webpage
        Dim pdf_page_size As String = "A4"
        Dim pageSize As PdfPageSize = DirectCast([Enum].Parse(GetType(PdfPageSize), pdf_page_size, True), PdfPageSize)

        Dim pdf_orientation As String = "Portrait"
        Dim pdfOrientation As PdfPageOrientation = DirectCast([Enum].Parse(GetType(PdfPageOrientation), pdf_orientation, True), PdfPageOrientation)

        Dim webPageWidth As Integer = 1024

        Dim webPageHeight As Integer = 0

        ' instantiate a html to pdf converter object
        Dim converter As New HtmlToPdf()

        ' set converter options
        converter.Options.PdfPageSize = pageSize
        converter.Options.PdfPageOrientation = pdfOrientation
        converter.Options.WebPageWidth = webPageWidth
        converter.Options.WebPageHeight = webPageHeight

        ' create a new pdf document converting an url
        'Dim doc As PdfDocument = converter.ConvertUrl(url)

        Dim TEXTO As String = ""
        For Each Producto In DirectCast(Session("Carrito"), List(Of FacturaDetalleENT))
            TEXTO += "<tr><td class=""td"" style=""width:400px;"">" & Producto.ProductoNombre & _
                "</td><td class=""td"" style=""width:160px;"">" & Producto.PrecioUnitario & _
                "</td><td class=""td"" style=""width:150px;"">" & Producto.Cantidad & _
                "</td><td class=""td"" style=""width:160px;"">" & Producto.Precio & "</td></tr>"
        Next

        Dim doc As PdfDocument = converter.ConvertHtmlString(PopulateBody(IdFactura, TEXTO, Fecha, Total, NombreCompleto, CUIT, CorreoElectronico, Domicilio, _
                                                                          Localidad, Provincia))

        ' save pdf document
        Dim memoryStream As New MemoryStream()
        doc.Save(memoryStream)
        Dim bytes As Byte() = memoryStream.ToArray()
        memoryStream.Close()
        'doc.Save(Response, False, "Factura.pdf")
        ' close pdf document
        doc.Close()
        'EnviarMail(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, bytes)
        EnviarMail("esteban.vertzner@gmail.com", bytes)

    End Sub

    Sub EnviarMail(CorreoElectronico As String, Bytes As Byte())
        Dim UsuarioMail = DirectCast(Session("Usuario"), UsuarioENT)
        Dim Cliente As New SmtpClient()
        Dim Mensaje As New MailMessage()
        Mensaje.Attachments.Add(New Attachment(New MemoryStream(Bytes), "Factura.pdf"))
        Mensaje.To.Add(CorreoElectronico)
        Mensaje.Subject = "Factura"
        Mensaje.IsBodyHtml = True
        Try
            Cliente.Send(Mensaje)
        Catch ex As Exception
            ' ...
        End Try
    End Sub

    Private Function PopulateBody(IdFactura As Integer, FacturaDetalle As String, Fecha As DateTime, Total As Double, NombreCompleto As String, _
                                  CUIT As String, CorreoElectronico As String, Domicilio As String, Localidad As String, Provincia As String) As String
        Dim body As String = String.Empty
        Dim reader As StreamReader = New StreamReader(Server.MapPath("~\Cuerpos de mail\FacturaAdjunto.html"))
        body = reader.ReadToEnd
        body = body.Replace("{IdFactura}", IdFactura)
        body = body.Replace("{FacturaDetalle}", FacturaDetalle)
        body = body.Replace("{Fecha}", Fecha)
        body = body.Replace("{Total}", Total)
        body = body.Replace("{NombreCompleto}", NombreCompleto)
        body = body.Replace("{CUIT}", CUIT)
        body = body.Replace("{CorreoElectronico}", CorreoElectronico)
        body = body.Replace("{Domicilio}", Domicilio)
        body = body.Replace("{Localidad}", Localidad)
        body = body.Replace("{Provincia}", Provincia)
        Return body
    End Function

    Private Sub AbrirmodalOpinion(titulo As String, dificultad As String, diseño As String, retorno As String, boton As String)
        modalComprarFichaOpinionTitle.InnerText = titulo
        lblmodalComprarFichaOpinionDificultad.InnerText = dificultad
        lblmodalComprarFichaOpinionDiseño.InnerText = diseño
        lblmodalComprarFichaOpinionRetorno.InnerText = retorno
        btnComprarModalAceptar.InnerText = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalComprarFichaOpinion", "$('#modalComprarFichaOpinion').modal({backdrop: 'static', keyboard: false});", True)
    End Sub

    Private Sub btnComprarModalAceptar_ServerClick(sender As Object, e As EventArgs) Handles btnComprarModalAceptar.ServerClick
        Try
            OpinionCompraV.OpinionCompraENT.IdUsuario = DirectCast(Session("Usuario"), UsuarioENT).ID
            For Each c In cuerpo.Controls
                If TypeOf (c) Is RadioButton Then
                    If DirectCast(c, RadioButton).Checked = True Then
                        If DirectCast(c, RadioButton).ID.Substring(5, 1) = 1 Then
                            OpinionCompraV.OpinionCompraENT.Dificultad = DirectCast(c, RadioButton).Text
                        ElseIf DirectCast(c, RadioButton).ID.Substring(5, 1) = 2 Then
                            OpinionCompraV.OpinionCompraENT.Diseño = DirectCast(c, RadioButton).Text
                        ElseIf DirectCast(c, RadioButton).ID.Substring(5, 1) = 3 Then
                            OpinionCompraV.OpinionCompraENT.Retorno = DirectCast(c, RadioButton).Text
                        End If
                    End If
                End If
            Next
            OpinionCompraV.OpinionCompraBLL.AgregarOpinionCompra(OpinionCompraV.OpinionCompraENT)
            Response.Redirect("Catalogo.aspx")
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnModalClose_ServerClick(sender As Object, e As EventArgs) Handles btnModalClose.ServerClick
        Try
            Response.Redirect("Catalogo.aspx")
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub
End Class
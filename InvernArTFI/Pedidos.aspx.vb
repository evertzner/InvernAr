Imports ENTITIES
Imports BLL
Imports System.Globalization
Imports SelectPdf
Imports System.Net.Mail
Imports System.IO

Public Class Pedidos
    Inherits System.Web.UI.Page
    Dim EstadoPedidoV As New EstadoPedidoVista
    Dim PedidoV As New PedidoVista
    Dim BitacoraV As New BitacoraVista
    Dim FacturaV As New FacturaVista
    Dim FacturaDetalleV As New FacturaDetalleVista
    Dim CuentaCorrienteV As New CuentaCorrienteVista
    Dim UsuarioV As New UsuarioVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvPedidosPedidos, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirGrillas(gvPedidosFacturaDetalle, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divPedidosFacturaDetalle.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                BindData(New PedidoENT)
                Session("Fila") = Nothing
                divPedidosFacturaDetalle.Style("display") = "none"
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Public Function ListarEstados() As List(Of EstadoPedidoENT)
        Return EstadoPedidoV.EstadoPedidoBLL.ListarEstadoPedido
    End Function

    Sub BindData(Filtro As PedidoENT)
        CargarLista(Filtro)
        CargarGridView()
    End Sub

    Private Sub CargarLista(Filtro As PedidoENT)
        Session("PedidosLista") = PedidoV.PedidoBLL.ListarPedidos(Filtro, False)
    End Sub

    Private Sub CargarGridView()
        gvPedidosPedidos.AutoGenerateColumns = False
        gvPedidosPedidos.DataSource = DirectCast(Session("PedidosLista"), List(Of PedidoENT))
        gvPedidosPedidos.DataBind()
    End Sub

    Private Sub gvPedidosPedidos_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvPedidosPedidos.RowCancelingEdit
        Try
            gvPedidosPedidos.EditIndex = -1
            BindData(New PedidoENT)
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvPedidosPedidos_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvPedidosPedidos.RowEditing
        Try
            gvPedidosPedidos.EditIndex = e.NewEditIndex
            BindData(New PedidoENT)
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvPedidosPedidos_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvPedidosPedidos.RowUpdating
        Try
            Dim EstadoRepetido As Boolean = False
            Dim EstadoIniciadoCancelado As Boolean = False
            Dim ListaPedidos = CType(Session("PedidosLista"), List(Of PedidoENT))
            Dim EstadoAnterior As String = ""
            Dim row = gvPedidosPedidos.Rows(e.RowIndex)
            EstadoAnterior = ListaPedidos.Item(row.DataItemIndex).Estado
            If (CType(row.Cells(3).Controls(1), DropDownList).SelectedValue) = EstadoAnterior Then
                EstadoRepetido = True
            End If

            If EstadoRepetido = False Then
                With ListaPedidos.Item(row.DataItemIndex)
                    .Estado = (CType(row.Cells(3).Controls(1), DropDownList).SelectedValue)
                End With
            Else
                gvPedidosPedidos.EditIndex = -1
                BindData(New PedidoENT)
                Exit Sub
            End If
            Dim PedidoENT As New PedidoENT
            With PedidoENT
                .Id = ListaPedidos.Item(row.DataItemIndex).Id
                .IdFactura = ListaPedidos.Item(row.DataItemIndex).IdFactura
                .Estado = ListaPedidos.Item(row.DataItemIndex).Estado
                .Fecha = Now
            End With
            For i = 0 To ListaPedidos.Count - 1
                If PedidoENT.IdFactura = ListaPedidos.Item(i).IdFactura Then
                    If i = e.RowIndex Then
                        If PedidoENT.Estado = "Iniciado" And EstadoAnterior = "Iniciado" Then
                            EstadoIniciadoCancelado = True
                            Exit For
                        ElseIf PedidoENT.Estado = "Cancelado" And EstadoAnterior = "Cancelado" Then
                            EstadoIniciadoCancelado = True
                            Exit For
                        End If
                    Else
                        If PedidoENT.Estado = "Iniciado" And ListaPedidos.Item(i).Estado = "Iniciado" Then
                            EstadoIniciadoCancelado = True
                            Exit For
                        ElseIf PedidoENT.Estado = "Cancelado" And ListaPedidos.Item(i).Estado = "Cancelado" Then
                            EstadoIniciadoCancelado = True
                            Exit For
                        End If
                    End If
                End If
            Next
            If EstadoIniciadoCancelado = False Then
                If PedidoENT.Estado = "Cancelado" Then
                    CancelarPedido(PedidoENT.IdFactura, Now, "Cancelado por un empleado")
                End If
                PedidoV.PedidoBLL.ModificarPedido(PedidoENT)
                Session("PedidosLista") = ListaPedidos
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Modificó el estado del pedido " & PedidoENT.Id))
                gvPedidosPedidos.EditIndex = -1
                BindData(New PedidoENT)
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                         Traduccion.TraducirMensaje("Mensaje8", DirectCast(Session("Idioma"), CultureInfo)), _
                         Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvPedidosPedidos_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvPedidosPedidos.PageIndexChanging
        Try
            gvPedidosPedidos.PageIndex = e.NewPageIndex
            CargarGridView()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try

    End Sub

    Private Sub gvPedidosPedidos_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gvPedidosPedidos.SelectedIndexChanging
        Try
            divPedidosFacturaDetalle.Style("display") = ""
            If Not Session("Fila") Is Nothing Then
                gvPedidosPedidos.Rows(Session("Fila")).Style.Remove("background-color")
            End If
            Dim ListaPedidos = CType(Session("PedidosLista"), List(Of PedidoENT))
            FacturaDetalleV.FacturaDetalleENT.IdFactura = ListaPedidos.Item(gvPedidosPedidos.Rows(e.NewSelectedIndex).DataItemIndex).IdFactura
            Session("FacturaDetalle") = FacturaDetalleV.FacturaDetalleBLL.ListarFacturasDetalle(FacturaDetalleV.FacturaDetalleENT)
            Session("Fila") = e.NewSelectedIndex
            gvPedidosPedidos.Rows(Session("Fila")).Style.Add("background-color", "#449d44")
            gvPedidosFacturaDetalle.AutoGenerateColumns = False
            gvPedidosFacturaDetalle.DataSource = DirectCast(Session("FacturaDetalle"), List(Of FacturaDetalleENT))
            gvPedidosFacturaDetalle.DataBind()
            PedidoV.PedidoENT.IdFactura = FacturaDetalleV.FacturaDetalleENT.IdFactura
            Dim ListaPedido As New List(Of PedidoENT)
            ListaPedido = PedidoV.PedidoBLL.ListarPedidos(PedidoV.PedidoENT, False)
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exSeleccionarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub CancelarPedido(IdFactura As Integer, Fecha As DateTime, Motivo As String)
        Try
            Dim ListaDatos As New List(Of String)
            ListaDatos = PedidoV.PedidoBLL.CancelarPedido(IdFactura, Fecha, Motivo)
            Dim IdNotaCredito As Integer = CInt(ListaDatos.Item(0))
            Dim IdCliente As Integer = CInt(ListaDatos.Item(1))
            Dim Total As Double = CDbl(ListaDatos.Item(2))
            Dim CorreoElectronico As String = ListaDatos.Item(3)
            With CuentaCorrienteV.CuentaCorrienteENT
                .IdCliente = IdCliente
                .IdFactura = IdFactura
                .IdNotaCredito = IdNotaCredito
                .Motivo = Motivo
                .Fecha = Fecha
                .Debito = Nothing
                .Credito = Total
            End With
            CuentaCorrienteV.CuentaCorrienteBLL.RegistrarCuentaCorriente(CuentaCorrienteV.CuentaCorrienteENT)
            UsuarioV.UsuarioENT.CorreoElectronico = CorreoElectronico
            Dim UsuarioENT As New UsuarioENT
            UsuarioENT = UsuarioV.UsuarioBLL.QueUsuario(UsuarioV.UsuarioENT)
            With UsuarioV.UsuarioENT
                .CUIT = UsuarioENT.CUIT
                .Nombre = UsuarioENT.Nombre
                .Apellido = UsuarioENT.Apellido
                .Domicilio = UsuarioENT.Domicilio
                .Localidad = UsuarioENT.Localidad
                .Provincia = UsuarioENT.Provincia
            End With
            divPedidosFacturaDetalle.Style("display") = "none"
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Canceló la factura Nº " & CuentaCorrienteV.CuentaCorrienteENT.IdFactura))
            ArmarPDF(IdNotaCredito, IdFactura, Fecha, Total, UsuarioV.UsuarioENT.Apellido & ", " & UsuarioV.UsuarioENT.Nombre, UsuarioV.UsuarioENT.CUIT, _
                     UsuarioV.UsuarioENT.CorreoElectronico, UsuarioV.UsuarioENT.Domicilio, UsuarioV.UsuarioENT.Localidad, UsuarioV.UsuarioENT.Provincia)
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub ArmarPDF(IdNotaCredito As Integer, IdFactura As Integer, Fecha As DateTime, Saldo As Double, NombreCompleto As String, CUIT As String, _
                           CorreoElectronico As String, Domicilio As String, Localidad As String, Provincia As String)
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

        Dim doc As PdfDocument = converter.ConvertHtmlString(PopulateBody(IdNotaCredito, IdFactura, Fecha, Saldo, NombreCompleto, CUIT, CorreoElectronico, _
                                                                          Domicilio, Localidad, Provincia))

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
        Mensaje.Attachments.Add(New Attachment(New MemoryStream(Bytes), "NotaCredito.pdf"))
        Mensaje.To.Add(CorreoElectronico)
        Mensaje.Subject = "Cancelación de factura"
        Mensaje.IsBodyHtml = True
        Try
            Cliente.Send(Mensaje)
        Catch ex As Exception
            ' ...
        End Try
    End Sub

    Private Function PopulateBody(IdNotaCredito As Integer, IdFactura As Integer, Fecha As DateTime, Saldo As Double, NombreCompleto As String, CUIT As String, _
                                 CorreoElectronico As String, Domicilio As String, Localidad As String, Provincia As String) As String
        Dim body As String = String.Empty
        Dim reader As StreamReader = New StreamReader(Server.MapPath("~\Cuerpos de mail\NotaCreditoAdjunto.html"))
        body = reader.ReadToEnd
        body = body.Replace("{IdNotaCredito}", IdNotaCredito)
        body = body.Replace("{Fecha}", Fecha)
        body = body.Replace("{NombreCompleto}", NombreCompleto)
        body = body.Replace("{CUIT}", CUIT)
        body = body.Replace("{CorreoElectronico}", CorreoElectronico)
        body = body.Replace("{Domicilio}", Domicilio)
        body = body.Replace("{Localidad}", Localidad)
        body = body.Replace("{Provincia}", Provincia)
        body = body.Replace("{IdFactura}", IdFactura)
        body = body.Replace("{Saldo}", Saldo)
        Return body
    End Function

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

End Class
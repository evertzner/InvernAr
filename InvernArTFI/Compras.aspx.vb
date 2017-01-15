Imports ENTITIES
Imports BLL
Imports System.Globalization
Imports SelectPdf
Imports System.Net.Mail
Imports System.IO

Public Class Compras
    Inherits System.Web.UI.Page
    Dim FacturaV As New FacturaVista
    Dim FacturaDetalleV As New FacturaDetalleVista
    Dim NotaCreditoV As New NotaCreditoVista
    Dim CuentaCorrienteV As New CuentaCorrienteVista
    Dim UsuarioV As New UsuarioVista
    Dim PedidoV As New PedidoVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvComprasFacturaDetalle, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirGrillas(gvComprasFacturas, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divComprasControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divComprasFacturaDetalle.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divComprasFacturas.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                Session("Fila") = Nothing
                divComprasFacturaDetalle.Style("display") = "none"
                BindData()
            End If
        Catch ex As Exception
            AbrirmodalMensaje(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
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
        FacturaV.FacturaENT.IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
        Session("Facturas") = FacturaV.FacturaBLL.ListarFacturas(FacturaV.FacturaENT)
    End Sub

    Private Sub CargarGridView()
        gvComprasFacturas.AutoGenerateColumns = False
        gvComprasFacturas.DataSource = DirectCast(Session("Facturas"), List(Of FacturaENT))
        gvComprasFacturas.DataBind()
    End Sub

    Private Sub gvComprasFacturas_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gvComprasFacturas.SelectedIndexChanging
        Try
            divComprasFacturaDetalle.Style("display") = ""
            If Not Session("Fila") Is Nothing Then
                gvComprasFacturas.Rows(Session("Fila")).Style.Remove("background-color")
            End If
            Dim ListaFacturas = CType(Session("Facturas"), List(Of FacturaENT))
            FacturaDetalleV.FacturaDetalleENT.IdFactura = ListaFacturas.Item(gvComprasFacturas.Rows(e.NewSelectedIndex).DataItemIndex).Id
            Session("FacturaDetalle") = FacturaDetalleV.FacturaDetalleBLL.ListarFacturasDetalle(FacturaDetalleV.FacturaDetalleENT)
            Session("Fila") = e.NewSelectedIndex
            gvComprasFacturas.Rows(Session("Fila")).Style.Add("background-color", "#449d44")
            gvComprasFacturaDetalle.AutoGenerateColumns = False
            gvComprasFacturaDetalle.DataSource = DirectCast(Session("FacturaDetalle"), List(Of FacturaDetalleENT))
            gvComprasFacturaDetalle.DataBind()
            PedidoV.PedidoENT.IdFactura = FacturaDetalleV.FacturaDetalleENT.IdFactura
            lblComprasSeguimiento.InnerText = "Estado del pedido: "
            Dim ListaPedido As New List(Of PedidoENT)
            ListaPedido = PedidoV.PedidoBLL.ListarPedidos(PedidoV.PedidoENT, True)
            Dim UltimoIndice As Integer = ListaPedido.Count - 1
            For i = 0 To UltimoIndice
                If i = UltimoIndice Then
                    lblComprasSeguimiento.InnerText += ListaPedido.Item(i).Estado
                Else
                    lblComprasSeguimiento.InnerText += ListaPedido.Item(i).Estado & "-->"
                End If
            Next
        Catch ex As Exception
            AbrirmodalMensaje(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exSeleccionarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub CancelarFactura_Click(sender As Object, e As EventArgs)
        Try
            Session("IdFactura") = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            Dim ListaFacturas = CType(Session("Facturas"), List(Of FacturaENT))
            For Each Factura In ListaFacturas
                If Factura.Id = Session("IdFactura") Then
                    If Factura.Cancelada = True Then
                        AbrirmodalMensaje(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                          Traduccion.TraducirMensaje("Mensaje9", DirectCast(Session("Idioma"), CultureInfo)), _
                          Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                    Else
                        Abrirmodal(Traduccion.TraducirMensaje("modalComprasCancelarFacturaTitle", DirectCast(Session("Idioma"), CultureInfo)), _
                                   Traduccion.TraducirMensaje("lblmodalComprasFichaOpinionMotivo", DirectCast(Session("Idioma"), CultureInfo)), _
                                   Traduccion.TraducirMensaje("btnComprasModalAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                    End If
                End If
            Next
        Catch ex As Exception
            AbrirmodalMensaje(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub Abrirmodal(titulo As String, motivo As String, boton As String)
        modalComprasCancelarFacturaTitle.InnerText = titulo
        lblmodalComprasFichaOpinionMotivo.InnerText = motivo
        btnComprasModalAceptar.InnerText = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "ModalComprasCancelarFactura", "$('#ModalComprasCancelarFactura').modal({backdrop: 'static', keyboard: false});", True)
    End Sub

    Private Sub AbrirmodalMensaje(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

    Private Sub btnComprasModalAceptar_ServerClick(sender As Object, e As EventArgs) Handles btnComprasModalAceptar.ServerClick
        Try
            Dim ListaFacturas = CType(Session("Facturas"), List(Of FacturaENT))
            For i = 0 To ListaFacturas.Count - 1
                If Session("IdFactura") = ListaFacturas.Item(i).Id Then
                    FacturaV.FacturaENT.Total = ListaFacturas.Item(i).Total
                End If
            Next
            With NotaCreditoV.NotaCreditoENT
                .IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
                .Fecha = Now
                .Saldo = FacturaV.FacturaENT.Total
                For Each c In cuerpo.Controls
                    If TypeOf (c) Is RadioButton Then
                        If DirectCast(c, RadioButton).Checked = True Then
                            If DirectCast(c, RadioButton).ID.Substring(5, 1) = 1 Then
                                .Motivo = DirectCast(c, RadioButton).Text
                            End If
                        End If
                    End If
                Next
            End With
            NotaCreditoV.NotaCreditoBLL.NuevaNotaCredito(NotaCreditoV.NotaCreditoENT)
            With CuentaCorrienteV.CuentaCorrienteENT
                .IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
                .IdFactura = Session("IdFactura")
                .IdNotaCredito = NotaCreditoV.NotaCreditoBLL.SeleccionarNumeroNotaCredito
                .Motivo = NotaCreditoV.NotaCreditoENT.Motivo
                .Fecha = Now
                .Debito = Nothing
                .Credito = FacturaV.FacturaENT.Total
            End With
            With UsuarioV.UsuarioENT
                .CUIT = DirectCast(Session("Usuario"), UsuarioENT).CUIT
                .Nombre = DirectCast(Session("Usuario"), UsuarioENT).Nombre
                .Apellido = DirectCast(Session("Usuario"), UsuarioENT).Apellido
                .CorreoElectronico = DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico
                .Domicilio = DirectCast(Session("Usuario"), UsuarioENT).Domicilio
                .Localidad = DirectCast(Session("Usuario"), UsuarioENT).Localidad
                .Provincia = DirectCast(Session("Usuario"), UsuarioENT).Provincia
            End With
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Canceló la factura Nº " & CuentaCorrienteV.CuentaCorrienteENT.IdFactura))
            ArmarPDF(CuentaCorrienteV.CuentaCorrienteENT.IdNotaCredito, Session("IdFactura"), CuentaCorrienteV.CuentaCorrienteENT.Fecha, FacturaV.FacturaENT.Total, _
                     UsuarioV.UsuarioENT.Apellido & ", " & UsuarioV.UsuarioENT.Nombre, UsuarioV.UsuarioENT.CUIT, UsuarioV.UsuarioENT.CorreoElectronico, _
                     UsuarioV.UsuarioENT.Domicilio, UsuarioV.UsuarioENT.Localidad, UsuarioV.UsuarioENT.Provincia)
            CuentaCorrienteV.CuentaCorrienteBLL.RegistrarCuentaCorriente(CuentaCorrienteV.CuentaCorrienteENT)
            FacturaV.FacturaENT.Id = Session("IdFactura")
            FacturaV.FacturaBLL.CancelarFactura(FacturaV.FacturaENT)
            With PedidoV.PedidoENT
                .IdFactura = Session("IdFactura")
                .Estado = "Cancelado"
                .Fecha = Now
            End With
            PedidoV.PedidoBLL.ModificarPedido(PedidoV.PedidoENT)
            Response.Redirect("Compras.aspx")
        Catch ex As Exception
            AbrirmodalMensaje(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
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

    Private Sub gvComprasFacturas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvComprasFacturas.PageIndexChanging
        Try
            gvComprasFacturas.PageIndex = e.NewPageIndex
            CargarGridView()
        Catch ex As Exception
            AbrirmodalMensaje(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

End Class
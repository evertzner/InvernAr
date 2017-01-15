Imports ENTITIES
Imports BLL
Imports System.Globalization
Imports SelectPdf
Imports System.IO

Public Class CuentaCorriente
    Inherits System.Web.UI.Page
    Dim CuentaCorrienteV As New CuentaCorrienteVista
    Dim FacturaV As New FacturaVista
    Dim FacturaDetalleV As New FacturaDetalleVista
    Dim NotaCreditoV As New NotaCreditoVista
    Dim UsuarioENT As UsuarioENT
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvCuentaCorriente, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divCuentaCorrienteControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
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
        CuentaCorrienteV.CuentaCorrienteENT.IdCliente = DirectCast(Session("Usuario"), UsuarioENT).ID
        Session("CuentaCorriente") = CuentaCorrienteV.CuentaCorrienteBLL.ListarCuentaCorriente(CuentaCorrienteV.CuentaCorrienteENT)
    End Sub

    Private Sub CargarGridView()
        gvCuentaCorriente.AutoGenerateColumns = False
        gvCuentaCorriente.DataSource = DirectCast(Session("CuentaCorriente"), List(Of CuentaCorrienteENT))
        gvCuentaCorriente.DataBind()
    End Sub

    Private Sub gvCuentaCorriente_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvCuentaCorriente.PageIndexChanging
        Try
            gvCuentaCorriente.PageIndex = e.NewPageIndex
            CargarGridView()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub CargarUsuario()
        UsuarioENT = New UsuarioENT
        With UsuarioENT
            .ID = DirectCast(Session("Usuario"), UsuarioENT).ID
            .Apellido = DirectCast(Session("Usuario"), UsuarioENT).Apellido
            .Nombre = DirectCast(Session("Usuario"), UsuarioENT).Nombre
            .Localidad = DirectCast(Session("Usuario"), UsuarioENT).Localidad
            .Provincia = DirectCast(Session("Usuario"), UsuarioENT).Provincia
            .Domicilio = DirectCast(Session("Usuario"), UsuarioENT).Domicilio
            .CUIT = DirectCast(Session("Usuario"), UsuarioENT).CUIT
            .CorreoElectronico = DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico
        End With
    End Sub

    Protected Sub IdFactura_Click(sender As Object, e As EventArgs)
        Try
            Dim IdFactura = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            CargarUsuario()
            FacturaV.FacturaENT.IdCliente = UsuarioENT.ID
            FacturaDetalleV.FacturaDetalleENT.IdFactura = IdFactura
            Dim FacturaENT As New FacturaENT
            For Each Factura In FacturaV.FacturaBLL.ListarFacturas(FacturaV.FacturaENT)
                If Factura.Id = IdFactura Then
                    FacturaENT = Factura
                End If
            Next
            Dim FacturaDetalleENT As New List(Of FacturaDetalleENT)
            FacturaDetalleENT = FacturaDetalleV.FacturaDetalleBLL.ListarFacturasDetalle(FacturaDetalleV.FacturaDetalleENT)
            Dim TEXTO As String = ""
            For Each Producto In FacturaDetalleENT
                TEXTO += "<tr><td class=""td"" style=""width:400px;"">" & Producto.ProductoNombre & _
                    "</td><td class=""td"" style=""width:160px;"">" & Producto.PrecioUnitario & _
                    "</td><td class=""td"" style=""width:150px;"">" & Producto.Cantidad & _
                    "</td><td class=""td"" style=""width:160px;"">" & Producto.Precio & "</td></tr>"
            Next
            ArmarPDF(0, IdFactura, FacturaENT.Fecha, FacturaENT.Total, UsuarioENT.Apellido & ", " & UsuarioENT.Nombre, UsuarioENT.CUIT, _
                     UsuarioENT.CorreoElectronico, UsuarioENT.Domicilio, UsuarioENT.Localidad, UsuarioENT.Provincia, "Factura", TEXTO)
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub IdNotaCredito_Click(sender As Object, e As EventArgs)
        Try
            Dim IdCuentaCorriente = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            CargarUsuario()
            Dim CuentaCorrienteENT As New CuentaCorrienteENT
            For Each CuentaCorriente In DirectCast(Session("CuentaCorriente"), List(Of CuentaCorrienteENT))
                If CuentaCorriente.Id = IdCuentaCorriente Then
                    CuentaCorrienteENT = CuentaCorriente
                End If
            Next
            If CuentaCorrienteENT.IdNotaCredito > 0 Then
                ArmarPDF(CuentaCorrienteENT.IdNotaCredito, CuentaCorrienteENT.IdFactura, CuentaCorrienteENT.Fecha, CuentaCorrienteENT.Credito + _
                         CuentaCorrienteENT.Debito, UsuarioENT.Apellido & ", " & UsuarioENT.Nombre, UsuarioENT.CUIT, UsuarioENT.CorreoElectronico, _
                         UsuarioENT.Domicilio, UsuarioENT.Localidad, UsuarioENT.Provincia, "NotaCredito", "")
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub ArmarPDF(IdNotaCredito As Integer, IdFactura As Integer, Fecha As DateTime, Saldo As Double, NombreCompleto As String, CUIT As String, _
                           CorreoElectronico As String, Domicilio As String, Localidad As String, Provincia As String, QueDocumento As String, Texto As String)
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
        Dim doc As New PdfDocument
        If QueDocumento = "NotaCredito" Then
            doc = converter.ConvertHtmlString(PopulateBodyNotaCredito(IdNotaCredito, IdFactura, Fecha, Saldo, NombreCompleto, CUIT, CorreoElectronico, _
                                                                              Domicilio, Localidad, Provincia))
        ElseIf QueDocumento = "Factura" Then
            doc = converter.ConvertHtmlString(PopulateBodyFactura(IdFactura, Texto, Fecha, Saldo, NombreCompleto, CUIT, CorreoElectronico, Domicilio, _
                                                                          Localidad, Provincia))
        End If
        ' save pdf document
        doc.Save(Response, False, "Factura.pdf")
        ' close pdf document
        doc.Close()
    End Sub

    Private Function PopulateBodyNotaCredito(IdNotaCredito As Integer, IdFactura As Integer, Fecha As DateTime, Saldo As Double, NombreCompleto As String, _
                                             CUIT As String, CorreoElectronico As String, Domicilio As String, Localidad As String, Provincia As String) _
                                         As String
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

    Private Function PopulateBodyFactura(IdFactura As Integer, FacturaDetalle As String, Fecha As DateTime, Total As Double, NombreCompleto As String, _
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

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

End Class
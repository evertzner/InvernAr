Imports ENTITIES
Imports BLL
Imports System.Globalization
Imports SelectPdf
Imports System.Net.Mail
Imports System.IO

Public Class ConfiguracionInvernadero
    Inherits System.Web.UI.Page
    Dim ProductoV As New ProductoVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarLista()
            End If
            CargarCuadroHerramientas()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub DescargarPresupuesto_Click(sender As Object, e As EventArgs) Handles DescargarPresupuesto.Click
        Try
            If Not hiddenComponentes.Value = "" Then
                GenerarPresupuesto()
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                                              Traduccion.TraducirMensaje("Mensaje12", DirectCast(Session("Idioma"), CultureInfo)), _
                                              Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub CargarLista()
        Dim ListaProductos As New List(Of ProductoENT)
        For Each Producto In ProductoV.ProductoBLL.ListarProductos
            If Producto.Tipo <> "Kit" Then
                ListaProductos.Add(Producto)
            End If
        Next
        Session("ProductosLista") = ListaProductos
    End Sub

    Private Sub CargarCuadroHerramientas()
        For Each Producto In DirectCast(Session("ProductosLista"), List(Of ProductoENT))
            Dim ProductoDiv = New HtmlGenericControl("DIV")
            ProductoDiv.ID = "drag" & Producto.Id
            ProductoDiv.Style.Add(HtmlTextWriterStyle.Height, "100px")
            ProductoDiv.Style.Add(HtmlTextWriterStyle.Width, "100px")
            ProductoDiv.Style.Add("background-size", "100% 100%")
            ProductoDiv.Style.Add("float", "left")
            ProductoDiv.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url(data:image/jpeg;base64," & Convert.ToBase64String(Producto.Imagen) & ")")
            ProductoDiv.Attributes.Add("class", "drag")
            ProductoDiv.Attributes.Add("idProducto", Producto.Id.ToString())
            ProductoDiv.Attributes.Add("precio", Producto.PrecioUnitario.ToString())
            ProductoDiv.Attributes.Add("especificacion", Producto.Especificacion.ToString())
            ProductoDiv.Attributes.Add("title", Producto.Nombre.ToString() & " - " & Producto.Especificacion.ToString() & vbCrLf & "$" & Producto.PrecioUnitario.ToString())
            Herramientas.Controls.Add(ProductoDiv)
        Next
    End Sub

    Private Sub GenerarPresupuesto()
        Dim Posible As Integer = 0
        Dim Existe As Boolean = False
        Dim ListaFacturaDetalle As New List(Of FacturaDetalleENT)
        Dim Lista As New List(Of FacturaDetalleENT)
        Dim FacturaDetalleENT As FacturaDetalleENT
        Dim ListaString As New List(Of String)
        Dim ProductosId() As String
        ProductosId = hiddenComponentes.Value.Split(";")
        For Each item In ProductosId
            ListaString.Add(item)
        Next
        ListaString.RemoveAt(ListaString.Count - 1)
        For Each item In ListaString
            FacturaDetalleENT = New FacturaDetalleENT
            FacturaDetalleENT.IdProducto = item
            Lista.Add(FacturaDetalleENT)
        Next
        Dim ListaFD As New List(Of FacturaDetalleENT)
        For Each Producto In DirectCast(Session("ProductosLista"), List(Of ProductoENT))
            Dim FD As New FacturaDetalleENT
            FD.IdProducto = Producto.Id
            FD.PrecioUnitario = Producto.PrecioUnitario
            FD.ProductoNombre = Producto.Nombre
            FD.ProductoTipo = Producto.Tipo
            ListaFD.Add(FD)
        Next
        For i = 0 To Lista.Count - 1
            For Each Producto In ListaFD
                If Lista.Item(i).IdProducto = Producto.IdProducto Then
                    If Not ListaFacturaDetalle.Count = 0 Then
                        For Each FacturaDetalle In ListaFacturaDetalle
                            If Producto.IdProducto = FacturaDetalle.IdProducto Then
                                FacturaDetalle.Cantidad += 1
                                Existe = True
                            End If
                        Next
                        If Existe = False Then
                            Producto.Cantidad = 1
                            ListaFacturaDetalle.Add(Producto)
                        End If
                    Else
                        Producto.Cantidad = 1
                        ListaFacturaDetalle.Add(Producto)
                    End If
                    Existe = False
                End If
            Next
        Next
        Dim Total As Double = 0
        Dim TotalActuadores As Integer = 0
        Dim TotalAnimaciones As Integer = 0
        Dim TotalArduinos As Integer = 0
        For Each FacturaDetalle In ListaFacturaDetalle
            FacturaDetalle.Precio = FacturaDetalle.PrecioUnitario * FacturaDetalle.Cantidad
            Total += FacturaDetalle.Precio
            If FacturaDetalle.ProductoTipo = "Actuador" Then
                TotalActuadores += FacturaDetalle.Cantidad * 4
            ElseIf FacturaDetalle.ProductoTipo = "Animacion" Then
                TotalAnimaciones += FacturaDetalle.Cantidad
            ElseIf FacturaDetalle.ProductoTipo = "Arduino" Then
                TotalArduinos += FacturaDetalle.Cantidad
            End If
        Next

        If TotalActuadores / TotalAnimaciones >= 1 Then
            Posible = 0
        ElseIf TotalActuadores / TotalAnimaciones < 1 Then
            Posible = 1
        End If
        If Posible = 0 And TotalArduinos > 0 Then
            Dim ListaFacturaDetalleAux = New List(Of FacturaDetalleENT)
            For Each FacturaDetalle In ListaFacturaDetalle
                If FacturaDetalle.Precio > 0 Then
                    ListaFacturaDetalleAux.Add(FacturaDetalle)
                End If
            Next
            ArmarPDF(Now, Total, DirectCast(Session("Usuario"), UsuarioENT).Apellido & ", " & DirectCast(Session("Usuario"), UsuarioENT).Nombre, _
                     DirectCast(Session("Usuario"), UsuarioENT).CUIT, DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                     DirectCast(Session("Usuario"), UsuarioENT).Domicilio, DirectCast(Session("Usuario"), UsuarioENT).Localidad, _
                     DirectCast(Session("Usuario"), UsuarioENT).Provincia, ListaFacturaDetalleAux)
        ElseIf Posible = 1 Then
            Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                       Traduccion.TraducirMensaje("Mensaje17", DirectCast(Session("Idioma"), CultureInfo)), _
                       Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
        ElseIf TotalArduinos < 1 Then
            Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("Mensaje18", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
        End If
    End Sub

    Protected Sub ArmarPDF(Fecha As DateTime, Total As Double, NombreCompleto As String, CUIT As String, CorreoElectronico As String, Domicilio As String, _
                           Localidad As String, Provincia As String, ListaFacturaDetalle As List(Of FacturaDetalleENT))
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
        For Each Producto In ListaFacturaDetalle
            TEXTO += "<tr><td class=""td"" style=""width:400px;"">" & Producto.ProductoNombre & _
                "</td><td class=""td"" style=""width:160px;"">" & Producto.PrecioUnitario & _
                "</td><td class=""td"" style=""width:150px;"">" & Producto.Cantidad & _
                "</td><td class=""td"" style=""width:160px;"">" & Producto.Precio & "</td></tr>"
        Next

        Dim doc As PdfDocument = converter.ConvertHtmlString(PopulateBody(TEXTO, Fecha, Total, NombreCompleto, CUIT, CorreoElectronico, Domicilio, _
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
        Mensaje.Attachments.Add(New Attachment(New MemoryStream(Bytes), "Presupuesto.pdf"))
        Mensaje.To.Add(CorreoElectronico)
        Mensaje.Subject = "Presupuesto"
        Mensaje.IsBodyHtml = True
        Try
            Cliente.Send(Mensaje)
        Catch ex As Exception
            ' ...
        End Try
    End Sub

    Private Function PopulateBody(FacturaDetalle As String, Fecha As DateTime, Total As Double, NombreCompleto As String, _
                                  CUIT As String, CorreoElectronico As String, Domicilio As String, Localidad As String, Provincia As String) As String
        Dim body As String = String.Empty
        Dim reader As StreamReader = New StreamReader(Server.MapPath("~\Cuerpos de mail\PresupuestoAdjunto.html"))
        body = reader.ReadToEnd
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
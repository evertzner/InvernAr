Imports BLL
Imports ENTITIES
Imports System.Globalization
Imports SelectPdf
Imports System.IO

Public Class ReporteVentas
    Inherits System.Web.UI.Page
    Dim ReporteVentasV As New ReporteVentasVista
    Dim Lista As New List(Of ReporteVentasENT)
    Dim Lista2 As New List(Of ReporteVentasENT)
    Dim ListaFiltrada As New List(Of ReporteVentasFiltradaENT)
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divUsuariosControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                Session("ReporteVentasFiltrado") = Nothing
                Session("ReporteVentas") = ReporteVentasV.ReporteVentasBLL.ReporteVentas
                MostrarGrafico()
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub MostrarGrafico()
        Dim ListaRep = Nothing
        If Session("ReporteVentasFiltrado") Is Nothing Then
            ListaRep = CType(Session("ReporteVentas"), List(Of ReporteVentasENT))
        Else
            ListaRep = CType(Session("ReporteVentasFiltrado"), List(Of ReporteVentasFiltradaENT))
        End If
        Dim x As String() = New String(ListaRep.Count - 1) {}
        Dim y As Decimal() = New Decimal(ListaRep.Count - 1) {}
        For i As Integer = 0 To ListaRep.Count - 1
            x(i) = ListaRep.Item(i).Fecha
            y(i) = Convert.ToInt32(ListaRep.Item(i).Total)
        Next
        BarChart1.Series.Add(New AjaxControlToolkit.BarChartSeries() With {.Data = y, .Name = "Ventas"})
        BarChart1.CategoriesAxis = String.Join(",", x)
        BarChart1.ChartTitle = "Ventas"
        BarChart1.Visible = True
    End Sub

    Protected Sub ArmarPDF()
        ' read parameters from the webpage
        Dim pdf_page_size As String = "A4"
        Dim pageSize As PdfPageSize = DirectCast([Enum].Parse(GetType(PdfPageSize), pdf_page_size, True), PdfPageSize)

        Dim pdf_orientation As String = "Landscape"
        Dim pdfOrientation As PdfPageOrientation = DirectCast([Enum].Parse(GetType(PdfPageOrientation), pdf_orientation, True), PdfPageOrientation)

        Dim webPageWidth As Integer = 1000

        Dim webPageHeight As Integer = 0

        ' instantiate a html to pdf converter object
        Dim converter As New HtmlToPdf()

        ' set converter options
        converter.Options.PdfPageSize = pageSize
        converter.Options.PdfPageOrientation = pdfOrientation
        converter.Options.MarginTop = 10
        converter.Options.MarginLeft = 10
        converter.Options.MarginRight = 10
        converter.Options.WebPageWidth = webPageWidth
        converter.Options.WebPageHeight = webPageHeight

        ' create a new pdf document converting an url
        'Dim doc As PdfDocument = converter.ConvertUrl(url)
        Dim Texto As String = ""
        Texto += "<html><head>" & _
                    "<style>#Main_BarChart1__ParentDiv {width: 1150px;}" & _
                    "#SeriesAxis {color: red;fill: #232323;fill-opacity: 1;font-family: Arial,Helvetica,sans-serif;font-size: 11px;}" & _
        "path {fill: #6c1e83;fill-opacity: 1;stroke: #cc5300;stroke-linecap: square;stroke-linejoin: round;stroke-opacity: 1;stroke-width: 1;}" & _
        "#LegendText {fill: #232323;fill-opacity: 1;font-family: Arial,Helvetica,sans-serif;font-size: 11px;}" & _
        "#ValueAxis {fill: #232323;fill-opacity: 1;font-family: Arial,Helvetica,sans-serif;font-size: 11px;}" & _
        "#ChartTitle {fill: #711737;fill-opacity: 1;font-family: Arial,Helvetica,sans-serif;font-size: 16px;}" & _
        "#LegendArea {fill: none;fill-opacity: 1;stroke-linecap: square;stroke-linejoin: round;stroke-opacity: 1;stroke-width: 0;}" & _
        "</style></head><body>" & hiddenHTML.Value & "</body></html>"


        Dim doc As PdfDocument = converter.ConvertHtmlString(Texto)

        ' save pdf document
        'Dim memoryStream As New MemoryStream()
        'doc.Save(memoryStream)
        'Dim bytes As Byte() = memoryStream.ToArray()
        'memoryStream.Close()
        doc.Save(Response, False, "ReporteVentas.pdf")
        ' close pdf document
        doc.Close()
    End Sub

    Private Sub btnreportesexportarpdf_serverclick(sender As Object, e As EventArgs) Handles btnReportesExportarPDF.ServerClick
        Try
            ArmarPDF()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnReporteVentasFiltrar_ServerClick(sender As Object, e As EventArgs) Handles btnReporteVentasFiltrar.ServerClick
        Try
            Lista.Clear()
            If txtReporteVentasFechaDesde.Value = "" And txtReporteVentasFechaHasta.Value = "" Then
                Session("ReporteVentasFiltrado") = Nothing
                Session("ReporteVentas") = ReporteVentasV.ReporteVentasBLL.ReporteVentas
            Else
                For Each item In DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT))
                    If txtReporteVentasFechaDesde.Value <> "" And txtReporteVentasFechaHasta.Value = "" Then
                        If item.Fecha >= txtReporteVentasFechaDesde.Value Then
                            Lista.Add(item)
                        End If
                    ElseIf txtReporteVentasFechaDesde.Value = "" And txtReporteVentasFechaHasta.Value <> "" Then
                        If item.Fecha <= txtReporteVentasFechaHasta.Value Then
                            Lista.Add(item)
                        End If
                    ElseIf txtReporteVentasFechaDesde.Value <> "" And txtReporteVentasFechaHasta.Value <> "" Then
                        If item.Fecha >= txtReporteVentasFechaDesde.Value And item.Fecha <= txtReporteVentasFechaHasta.Value Then
                            Lista.Add(item)
                        End If
                    End If
                Next
                Session("ReporteVentasFiltrado") = Nothing
                Session("ReporteVentas") = Nothing
                Session("ReporteVentas") = Lista
            End If
            If rbReporteVentasDiario.Checked = True Then
                Lista2.Clear()
                For i = 0 To DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Count - 1
                    Dim ReporteVentasENT As New ReporteVentasENT
                    ReporteVentasENT.Fecha = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha
                    ReporteVentasENT.Total = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Total
                    Lista2.Add(ReporteVentasENT)
                Next
                Session("ReporteVentas") = Nothing
                Session("ReporteVentas") = Lista2
                MostrarGrafico()
            ElseIf rbReporteVentasAnual.Checked = True Then
                ListaFiltrada.Clear()
                For i = 0 To DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Count - 1
                    Dim ReporteVentasFiltradaENT As New ReporteVentasFiltradaENT
                    If i = 0 Then
                        ReporteVentasFiltradaENT.Fecha = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year
                        ReporteVentasFiltradaENT.Total = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Total
                        ListaFiltrada.Add(ReporteVentasFiltradaENT)
                    Else
                        If DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year = _
                            DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i - 1).Fecha.Year Then
                            For j = 0 To ListaFiltrada.Count - 1
                                If ListaFiltrada.Item(j).Fecha = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year Then
                                    ListaFiltrada.Item(j).Total += DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Total
                                End If
                            Next
                        Else
                            ReporteVentasFiltradaENT.Fecha = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year
                            ReporteVentasFiltradaENT.Total = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Total
                            ListaFiltrada.Add(ReporteVentasFiltradaENT)
                        End If
                    End If
                Next
                Session("ReporteVentasFiltrado") = Nothing
                Session("ReporteVentasFiltrado") = ListaFiltrada
                MostrarGrafico()
            ElseIf rbReporteVentasMensual.Checked = True Then
                ListaFiltrada.Clear()
                For i = 0 To DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Count - 1
                    Dim ReporteVentasFiltradaENT As New ReporteVentasFiltradaENT
                    If i = 0 Then
                        ReporteVentasFiltradaENT.Fecha = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Month & "/" & _
                        DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year
                        ReporteVentasFiltradaENT.Total = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Total
                        ListaFiltrada.Add(ReporteVentasFiltradaENT)
                    Else
                        If DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Month & "/" & _
                        DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year = _
                            DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i - 1).Fecha.Month & "/" & _
                        DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i - 1).Fecha.Year Then
                            For j = 0 To ListaFiltrada.Count - 1
                                If ListaFiltrada.Item(j).Fecha = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Month & "/" & _
                        DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year Then
                                    ListaFiltrada.Item(j).Total += DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Total
                                End If
                            Next
                        Else
                            ReporteVentasFiltradaENT.Fecha = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Month & "/" & _
                        DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year
                            ReporteVentasFiltradaENT.Total = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Total
                            ListaFiltrada.Add(ReporteVentasFiltradaENT)
                        End If
                    End If
                Next
                Session("ReporteVentasFiltrado") = Nothing
                Session("ReporteVentasFiltrado") = ListaFiltrada
                MostrarGrafico()
            ElseIf rbReporteVentasSemanal.Checked = True Then
                ListaFiltrada.Clear()
                For i = 0 To DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Count - 1
                    Dim ReporteVentasFiltradaENT As New ReporteVentasFiltradaENT
                    If i = 0 Then
                        ReporteVentasFiltradaENT.Fecha = "Semana " & _
                            Math.Ceiling(DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.DayOfYear / 7) & " - " & _
                           DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year
                        ReporteVentasFiltradaENT.Total = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Total
                        ListaFiltrada.Add(ReporteVentasFiltradaENT)
                    Else
                        If "Semana " & _
                            Math.Ceiling(DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.DayOfYear / 7) & " - " & _
                           DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year =
                            "Semana " & _
                            Math.Ceiling(DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i - 1).Fecha.DayOfYear / 7) & " - " & _
                           DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i - 1).Fecha.Year Then
                            For j = 0 To ListaFiltrada.Count - 1
                                If ListaFiltrada.Item(j).Fecha = "Semana " & _
                            Math.Ceiling(DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.DayOfYear / 7) & " - " & _
                           DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year Then
                                    ListaFiltrada.Item(j).Total += DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Total
                                End If
                            Next
                        Else
                            ReporteVentasFiltradaENT.Fecha = "Semana " & _
                            Math.Ceiling(DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.DayOfYear / 7) & " - " & _
                           DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Fecha.Year
                            ReporteVentasFiltradaENT.Total = DirectCast(Session("ReporteVentas"), List(Of ReporteVentasENT)).Item(i).Total
                            ListaFiltrada.Add(ReporteVentasFiltradaENT)
                        End If
                    End If
                Next
                Session("ReporteVentasFiltrado") = Nothing
                Session("ReporteVentasFiltrado") = ListaFiltrada
                MostrarGrafico()
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
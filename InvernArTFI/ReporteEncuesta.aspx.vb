﻿Imports BLL
Imports ENTITIES
Imports System.Globalization
Imports SelectPdf
Imports System.IO

Public Class ReporteEncuesta
    Inherits System.Web.UI.Page
    Dim EncuestasV As New EncuestaVista
    Dim ReporteEncuestaV As New ReporteEncuestaVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divUsuariosControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                ddlReportesEncuestas.Enabled = False
                btnReportesExportarPDF.Attributes.Add("disabled", "")
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub LlenarListas()
        btnReportesExportarPDF.Attributes.Add("disabled", "")
        ddlReportesEncuestas.Enabled = True
        ddlReportesEncuestas.Items.Clear()
        Session("Encuestas") = EncuestasV.EncuestaBLL.ListarEncuestas
        For Each item In DirectCast(Session("Encuestas"), List(Of EncuestaENT))
            If rbReporteEncuestaNoVencido.Checked = True Then
                If Now.Date < item.FechaVencimiento.Date Then
                    ddlReportesEncuestas.Items.Add(item.Tema)
                End If
            ElseIf rbReporteEncuestaVencido.Checked = True Then
                If Now.Date >= item.FechaVencimiento.Date Then
                    ddlReportesEncuestas.Items.Add(item.Tema)
                End If
            ElseIf rbReporteEncuestaTodo.Checked = True Then
                ddlReportesEncuestas.Items.Add(item.Tema)
            End If
        Next
        ddlReportesEncuestas.Items.Insert(0, "")
    End Sub

    Protected Sub ddlReportesEncuestas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            btnReportesExportarPDF.Attributes.Remove("disabled")
            If ddlReportesEncuestas.SelectedIndex > 0 Then
                ReporteEncuestaV.ReporteEncuestaENT.IdEncuesta = DirectCast(Session("Encuestas"), List(Of EncuestaENT)).Item(ddlReportesEncuestas.SelectedIndex - 1).Id
            End If
            Session("ReporteEncuesta") = ReporteEncuestaV.ReporteEncuestaBLL.ReporteEncuestas(ReporteEncuestaV.ReporteEncuestaENT)
            Dim Lista = CType(Session("ReporteEncuesta"), List(Of ReporteEncuestaENT))
            Dim x As String() = New String(Lista.Count - 1) {}
            Dim y As Decimal() = New Decimal(Lista.Count - 1) {}
            For i As Integer = 0 To Lista.Count - 1
                x(i) = Lista.Item(i).IdPregunta & " - " & Lista.Item(i).Respuesta
                y(i) = Convert.ToInt32(Lista.Item(i).Cantidad)
            Next
            BarChart1.Series.Add(New AjaxControlToolkit.BarChartSeries() With {.Data = y, .Name = String.Format("{0}", ddlReportesEncuestas.SelectedItem.Value)})
            BarChart1.CategoriesAxis = String.Join(",", x)
            BarChart1.ChartTitle = String.Format("Encuesta: {0}", ddlReportesEncuestas.SelectedItem.Value)
            BarChart1.Visible = ddlReportesEncuestas.SelectedItem.Value <> ""
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exSeleccionarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
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
        doc.Save(Response, False, "ReporteEncuesta.pdf")
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

    Private Sub btnReporteEncuestaCargarEncuestas_ServerClick(sender As Object, e As EventArgs) Handles btnReporteEncuestaCargarEncuestas.ServerClick
        Try
            LlenarListas()
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
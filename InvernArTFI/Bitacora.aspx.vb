Imports ENTITIES
Imports BLL
Imports System.Globalization

Public Class Bitacora
    Inherits System.Web.UI.Page
    Dim BitacoraV As New BitacoraVista
    Dim Lista As New List(Of BitacoraENT)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvBitacoraBitacora, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divBitacoraControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                Lista.Clear()
                Session("BitacoraLista") = Nothing
                Session("BitacoraFiltrada") = Nothing
                CargarLista()
                CargarGridView()
                LlenarListas()
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub CargarGridView()
        gvBitacoraBitacora.AutoGenerateColumns = False
        Lista = DirectCast(Session("BitacoraLista"), List(Of BitacoraENT))
        gvBitacoraBitacora.DataSource = Lista
        gvBitacoraBitacora.DataBind()
    End Sub

    Private Sub CargarLista()
        Session("BitacoraLista") = BitacoraV.BitacoraBLL.ConsultarBitacora(BitacoraV.BitacoraENT)
    End Sub

    Private Sub LlenarListas()
        ddlBitacoraUsuarios.Items.Clear()
        For Each item In BitacoraV.BitacoraBLL.ConsultarUsuariosEnBitacora
            ddlBitacoraUsuarios.Items.Add(item.CorreoElectronico)
        Next
        ddlBitacoraUsuarios.Items.Insert(0, "")
    End Sub

    Private Sub btnBitacoraFiltrar_ServerClick(sender As Object, e As EventArgs) Handles btnBitacoraFiltrar.ServerClick
        Try
            Lista.Clear()
            If ddlBitacoraUsuarios.SelectedValue = "" And txtBitacoraFechaDesde.Value = "" And txtBitacoraFechaHasta.Value = "" Then
                Response.Redirect("Bitacora.aspx")
            Else
                For Each item In DirectCast(Session("BitacoraLista"), List(Of BitacoraENT))
                    If item.Usuario = ddlBitacoraUsuarios.SelectedValue And txtBitacoraFechaDesde.Value = "" And txtBitacoraFechaHasta.Value = "" Then
                        Lista.Add(item)
                    ElseIf txtBitacoraFechaDesde.Value <> "" And txtBitacoraFechaHasta.Value = "" Then
                        If item.FechaActividad >= txtBitacoraFechaDesde.Value Then
                            Lista.Add(item)
                        End If
                    ElseIf txtBitacoraFechaDesde.Value = "" And txtBitacoraFechaHasta.Value <> "" Then
                        If item.FechaActividad <= txtBitacoraFechaHasta.Value Then
                            Lista.Add(item)
                        End If
                    ElseIf txtBitacoraFechaDesde.Value <> "" And txtBitacoraFechaHasta.Value <> "" Then
                        If item.FechaActividad >= txtBitacoraFechaDesde.Value And item.FechaActividad <= txtBitacoraFechaHasta.Value Then
                            Lista.Add(item)
                        End If
                    End If
                Next
                Session("BitacoraFiltrada") = Nothing
                Session("BitacoraFiltrada") = Lista
                gvBitacoraBitacora.PageIndex = 0
                gvBitacoraBitacora.AutoGenerateColumns = False
                gvBitacoraBitacora.DataSource = Lista
                gvBitacoraBitacora.DataBind()
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvBitacoraBitacora_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvBitacoraBitacora.PageIndexChanging
        Try
            gvBitacoraBitacora.PageIndex = e.NewPageIndex
            gvBitacoraBitacora.AutoGenerateColumns = False
            If Session("BitacoraFiltrada") Is Nothing Then
                gvBitacoraBitacora.DataSource = DirectCast(Session("BitacoraLista"), List(Of BitacoraENT))
            Else
                gvBitacoraBitacora.DataSource = DirectCast(Session("BitacoraFiltrada"), List(Of BitacoraENT))
            End If
            gvBitacoraBitacora.DataBind()
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
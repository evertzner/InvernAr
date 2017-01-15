Imports ENTITIES
Imports BLL
Imports System.Globalization

Public Class Sensores
    Inherits System.Web.UI.Page
    Dim SensorLimiteV As New SensorLimiteVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvSensores, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divSensoresControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
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
        Session("SensoresLimiteLista") = SensorLimiteV.SensorLimiteBLL.ListarSensoresLimites
    End Sub

    Private Sub CargarGridView()
        gvSensores.AutoGenerateColumns = False
        gvSensores.DataSource = DirectCast(Session("SensoresLimiteLista"), List(Of SensorLimiteENT))
        gvSensores.DataBind()
    End Sub

    Private Sub gvSensores_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvSensores.PageIndexChanging
        Try
            gvSensores.PageIndex = e.NewPageIndex
            CargarGridView()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvSensores_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvSensores.RowCancelingEdit
        Try
            gvSensores.EditIndex = -1
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvSensores_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvSensores.RowEditing
        Try
            gvSensores.EditIndex = e.NewEditIndex
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvSensores_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvSensores.RowUpdating
        Try
            Dim ListaSensoresLimite = CType(Session("SensoresLimiteLista"), List(Of SensorLimiteENT))
            Dim row = gvSensores.Rows(e.RowIndex)
            With ListaSensoresLimite.Item(row.DataItemIndex)
                .Id = DirectCast(Session("SensoresLimiteLista"), List(Of SensorLimiteENT))(gvSensores.Rows(e.RowIndex).DataItemIndex).Id
                .LimiteMinimoAlerta = CType(row.Cells(4).Controls(1), TextBox).Text
                .LimiteMaximoAlerta = CType(row.Cells(5).Controls(1), TextBox).Text
            End With
            Dim SensorLimiteENT As New SensorLimiteENT
            With SensorLimiteENT
                .Id = ListaSensoresLimite.Item(row.DataItemIndex).Id
                .LimiteMinimoAlerta = ListaSensoresLimite.Item(row.DataItemIndex).LimiteMinimoAlerta
                .LimiteMaximoAlerta = ListaSensoresLimite.Item(row.DataItemIndex).LimiteMaximoAlerta
            End With
            If SensorLimiteENT.LimiteMinimoAlerta < SensorLimiteENT.LimiteMaximoAlerta Then
                SensorLimiteV.SensorLimiteBLL.ActualizarSensorLimite(SensorLimiteENT)
                gvSensores.EditIndex = -1
                BindData()
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Modificó los límites del sensor " & SensorLimiteENT.Id))
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                               Traduccion.TraducirMensaje("Mensaje20", DirectCast(Session("Idioma"), CultureInfo)), _
                               Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
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
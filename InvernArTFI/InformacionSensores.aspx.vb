Imports ENTITIES
Imports BLL
Imports System.Globalization

Public Class InformacionSensores
    Inherits System.Web.UI.Page
    Dim SensorV As New SensorVista
    Dim UsuarioV As New UsuarioVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvSensores, DirectCast(Session("Idioma"), CultureInfo))
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
        UsuarioV.UsuarioENT.ID = DirectCast(Session("Usuario"), UsuarioENT).ID
        Session("SensoresLista") = SensorV.SensorBLL.ListarSensores(UsuarioV.UsuarioENT)
    End Sub

    Private Sub CargarGridView()
        gvSensores.AutoGenerateColumns = False
        gvSensores.DataSource = DirectCast(Session("SensoresLista"), List(Of SensorENT))
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim ListaSensores As New List(Of SensorENT)
            For Each Sensor In DirectCast(Session("SensoresLista"), List(Of SensorENT))
                Sensor.ValorSensado = CInt(Math.Floor(((Sensor.LimiteMaximoAlerta + (Sensor.LimiteMaximoAlerta * 0.10000000000000001)) - _
                                                       (Sensor.LimiteMinimoAlerta + (Sensor.LimiteMinimoAlerta * 0.10000000000000001)) + 1) * Rnd())) + _
                                           (Sensor.LimiteMinimoAlerta + (Sensor.LimiteMinimoAlerta * 0.10000000000000001))
                If Sensor.ValorSensado < Sensor.LimiteMinimoAlerta Or Sensor.ValorSensado > Sensor.LimiteMaximoAlerta Then
                    Sensor.Estado = "Alerta"
                Else
                    Sensor.Estado = "Funcionando"
                End If
                ListaSensores.Add(Sensor)
            Next
            gvSensores.AutoGenerateColumns = False
            gvSensores.DataSource = ListaSensores
            gvSensores.DataBind()
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
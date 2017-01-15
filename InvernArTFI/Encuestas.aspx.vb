Imports BLL
Imports ENTITIES
Imports System.Globalization
Imports System.Text

Public Class Encuestas
    Inherits System.Web.UI.Page
    Dim PreguntaEncuestaV As New PreguntaEncuestaVista
    Dim RespuestaEncuestaV As New RespuestaEncuestaVista
    Dim ListaEncuestas As New List(Of EncuestaENT)
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divEncuestasEncuesta.Controls, DirectCast(Session("Idioma"), CultureInfo))
            'Traduccion.TraducirControles(modalEncuestasResponder.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                btnEncuestasResponder.Attributes.Add("disabled", "disabled")
                LlenarListas()
                OcultarControles()
                divEncuestasAlerta.Visible = False
            End If
        Catch ex As Exception
            AbrirmodalMaster(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub OcultarControles()
        divEncuestasPreguntas.Visible = False
        For Each c As Control In divEncuestasPreguntas.Controls
            If TypeOf (c) Is HtmlGenericControl Then
                If c.ID.StartsWith("Label") Then
                    c.Visible = False
                End If
            End If
            If TypeOf (c) Is RadioButton Then
                If c.ID.StartsWith("Radio") Then
                    c.Visible = False
                End If
            End If
        Next
    End Sub

    Private Sub LlenarListas()
        ddlEncuestasEncuestas.Items.Clear()
        For Each item In RespuestaEncuestaV.RespuestaEncuestaBLL.ListarEncuestasCliente()
            ListaEncuestas.Add(item)
        Next
        For Each item In ListaEncuestas
            If Now.Date < item.FechaVencimiento.Date Then
                ddlEncuestasEncuestas.Items.Add(item.Tema)
            End If
        Next
        ddlEncuestasEncuestas.Items.Insert(0, "")
        Session("ListaEncuestasAResponder") = ListaEncuestas
    End Sub

    Private Sub btnEncuestasConsultar_ServerClick(sender As Object, e As EventArgs) Handles btnEncuestasConsultar.ServerClick
        Try
            If ddlEncuestasEncuestas.SelectedIndex > 0 Then
                btnEncuestasResponder.Attributes.Remove("disabled")
                OcultarControles()
                divEncuestasPreguntas.Visible = True
                Session("EncuestaAResponder") = DirectCast(Session("ListaEncuestasAResponder"), List(Of EncuestaENT)).Item(ddlEncuestasEncuestas.SelectedIndex - 1)
                PreguntaEncuestaV.PreguntaEncuestaENT.IdEncuesta = DirectCast(Session("EncuestaAResponder"), EncuestaENT).Id
                Session("PreguntasAResponder") = PreguntaEncuestaV.PreguntaEncuestaBLL.ListarPreguntas(PreguntaEncuestaV.PreguntaEncuestaENT)
                For Each item In DirectCast(Session("PreguntasAResponder"), List(Of PreguntaEncuestaENT))
                    Pregunta(item)
                Next
            Else
                btnEncuestasResponder.Attributes.Add("disabled", "disabled")
                OcultarControles()
            End If
        Catch ex As Exception
            AbrirmodalMaster(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub Pregunta(PreguntaEncuesta As PreguntaEncuestaENT)
        For Each c As Control In divEncuestasPreguntas.Controls
            If TypeOf (c) Is HtmlGenericControl Then
                If c.ID.StartsWith("Label") Then
                    If c.ID.Remove(0, 5) = PreguntaEncuesta.IdPregunta Then
                        DirectCast(c, HtmlGenericControl).InnerText = PreguntaEncuesta.Pregunta
                        c.Visible = True
                    End If
                End If
            End If
            If TypeOf (c) Is RadioButton Then
                If c.ID.StartsWith("Radio") Then
                    If c.ID.Remove(0, 5).Length = 3 Then
                        If c.ID.Remove(0, 5).Remove(2, 1) = PreguntaEncuesta.IdPregunta Then
                            c.Visible = True
                            If DirectCast(c, RadioButton).Text = "Bajo" Then DirectCast(c, RadioButton).Checked = True
                        End If
                    Else
                        If c.ID.Remove(0, 5).Remove(1, 1) = PreguntaEncuesta.IdPregunta Then
                            c.Visible = True
                            If DirectCast(c, RadioButton).Text = "Bajo" Then DirectCast(c, RadioButton).Checked = True
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub btnEncuestasResponder_ServerClick(sender As Object, e As EventArgs) Handles btnEncuestasResponder.Click
        Try
            Dim CamposVacios As Boolean = False
            If Session("Usuario") IsNot Nothing Then
                RespuestaEncuestaV.RespuestaEncuestaENT.CorreoElectronico = DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico
            Else
                Abrirmodal(Traduccion.TraducirMensaje("modalEncuestasResponderTitle", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("txtEncuestasModalCorreoElectronico", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("btnEncuestasModalAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                Exit Sub
            End If
            For Each c In divEncuestasPreguntas.Controls
                If TypeOf (c) Is RadioButton Then
                    If DirectCast(c, RadioButton).Visible = True Then
                        If DirectCast(c, RadioButton).Checked = True Then
                            RespuestaEncuestaV.RespuestaEncuestaENT.IdEncuesta = DirectCast(Session("EncuestaAResponder"), EncuestaENT).Id
                            If c.ID.Remove(0, 5).Length = 3 Then
                                RespuestaEncuestaV.RespuestaEncuestaENT.IdPregunta = c.ID.Remove(0, 5).Remove(2, 1)
                            Else
                                RespuestaEncuestaV.RespuestaEncuestaENT.IdPregunta = c.ID.Remove(0, 5).Remove(1, 1)
                            End If
                            RespuestaEncuestaV.RespuestaEncuestaENT.Respuesta = DirectCast(c, RadioButton).Text
                            RespuestaEncuestaV.RespuestaEncuestaBLL.Responder(RespuestaEncuestaV.RespuestaEncuestaENT)
                        End If
                    End If
                End If
            Next
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(RespuestaEncuestaV.RespuestaEncuestaENT.CorreoElectronico, _
                                                                                                   "Respondío la encuesta " & _
                                                                                                   RespuestaEncuestaV.RespuestaEncuestaENT.IdEncuesta))
            Response.Redirect("Encuestas.aspx")
        Catch ex As Exception
            AbrirmodalMaster(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub Abrirmodal(titulo As String, cajatexto As String, boton As String)
        modalEncuestasResponderTitle.InnerText = titulo
        txtEncuestasModalCorreoElectronico.Attributes.Add("placeholder", cajatexto)
        btnEncuestasModalAceptar.InnerText = boton
        revCorreoElectronico.ErrorMessage = Traduccion.TraducirMensaje(revCorreoElectronico.ID, DirectCast(Session("Idioma"), CultureInfo))
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalEncuestasResponder", "$('#modalEncuestasResponder').modal({backdrop: 'static', keyboard: false});", True)
    End Sub

    Private Sub btnEncuestasModalAceptar_ServerClick(sender As Object, e As EventArgs) Handles btnEncuestasModalAceptar.ServerClick
        Try
            If txtEncuestasModalCorreoElectronico.Value <> "" Then
                RespuestaEncuestaV.RespuestaEncuestaENT.CorreoElectronico = txtEncuestasModalCorreoElectronico.Value
                For Each c In divEncuestasPreguntas.Controls
                    If TypeOf (c) Is RadioButton Then
                        If DirectCast(c, RadioButton).Visible = True Then
                            If DirectCast(c, RadioButton).Checked = True Then
                                RespuestaEncuestaV.RespuestaEncuestaENT.IdEncuesta = DirectCast(Session("EncuestaAResponder"), EncuestaENT).Id
                                If c.ID.Remove(0, 5).Length = 3 Then
                                    RespuestaEncuestaV.RespuestaEncuestaENT.IdPregunta = c.ID.Remove(0, 5).Remove(2, 1)
                                Else
                                    RespuestaEncuestaV.RespuestaEncuestaENT.IdPregunta = c.ID.Remove(0, 5).Remove(1, 1)
                                End If
                                RespuestaEncuestaV.RespuestaEncuestaENT.Respuesta = DirectCast(c, RadioButton).Text
                                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(RespuestaEncuestaV.RespuestaEncuestaENT.CorreoElectronico, _
                                                                                                        "Respondío la encuesta " & _
                                                                                                        RespuestaEncuestaV.RespuestaEncuestaENT.IdEncuesta))
                                RespuestaEncuestaV.RespuestaEncuestaBLL.Responder(RespuestaEncuestaV.RespuestaEncuestaENT)
                            End If
                        End If
                    End If
                Next
                Response.Redirect("Encuestas.aspx")
            End If
        Catch ex As Exception
            AbrirmodalMaster(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnEncuestasModalCerrar_ServerClick(sender As Object, e As EventArgs) Handles btnEncuestasModalCerrar.ServerClick
        Try
            Response.Redirect("Encuestas.aspx")
        Catch ex As Exception
            AbrirmodalMaster(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub AbrirmodalMaster(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

End Class
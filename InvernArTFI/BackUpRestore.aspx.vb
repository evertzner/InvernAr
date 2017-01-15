Imports ENTITIES
Imports BLL
Imports System.Globalization

Public Class BackUpRestore
    Inherits System.Web.UI.Page
    Dim BackUpRestoreV As New BackUpRestoreVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divBRControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnBRRealizarBackUp_Click(sender As Object, e As EventArgs) Handles btnBRRealizarBackUp.Click
        Try
            If txtBRDestino.Value <> "" Then
                BackUpRestoreV.BackUpRestoreENT.Localizacion = txtBRDestino.Value
                BackUpRestoreV.BackUpRestoreBLL.RealizarBackUp(BackUpRestoreV.BackUpRestoreENT)
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Realizó un BackUp"))
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("MensajeBackUp", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                LimpiarCampos()
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("CamposVacios", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnBRRealizarRestore_Click(sender As Object, e As EventArgs) Handles btnBRRealizarRestore.Click
        Try
            If fuArchivo.Value <> "" Then
                BackUpRestoreV.BackUpRestoreENT.Localizacion = fuArchivo.Value
                BackUpRestoreV.BackUpRestoreBLL.RealizarRestore(BackUpRestoreV.BackUpRestoreENT)
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                       "Realizó un Restore"))
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("MensajeRestore", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                LimpiarCampos()
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("CamposVacios", DirectCast(Session("Idioma"), CultureInfo)), _
                             Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub LimpiarCampos()
        txtBRDestino.Value = ""
    End Sub

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

End Class
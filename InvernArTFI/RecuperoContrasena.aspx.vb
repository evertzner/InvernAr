Imports BLL
Imports ENTITIES
Imports System.Globalization

Public Class RecuperoContrasena
    Inherits System.Web.UI.Page
    Dim UsuarioV As New UsuarioVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       Try
        Traduccion.TraducirControles(divRecuperoContrasenaControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
        divRecuperoContrasenaAlerta.Visible = False
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnRecuperoContrasenaConfirmar_ServerClick(sender As Object, e As EventArgs) Handles btnRecuperoContrasenaConfirmar.ServerClick
        Try
            'Flag = 0 --> Codigo o email invalido
            'Flag = 1 --> Se ha modificado la contraseña
            'Flag = 2 --> No coinciden las contraseñas
            Dim Flag As Integer
            If txtRecuperoContrasenaCodigo.Value <> "" And txtRecuperoContrasenaCorreoElectronico.Value <> "" And txtRecuperoContrasenaPassNueva.Value <> "" And _
                txtRecuperoContrasenaPassNuevaRepetida.Value <> "" Then
                Flag = UsuarioV.UsuarioBLL.RecuperarContraseña(txtRecuperoContrasenaCodigo.Value, txtRecuperoContrasenaCorreoElectronico.Value, _
                                                               txtRecuperoContrasenaPassNueva.Value, txtRecuperoContrasenaPassNuevaRepetida.Value)
                If Flag = 1 Then
                    BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(txtRecuperoContrasenaCorreoElectronico.Value, "Recuperó su contraseña"))
                    Response.Redirect("Inicio.aspx")
                ElseIf Flag = 0 Then
                    divRecuperoContrasenaAlerta.Visible = True
                    divRecuperoContrasenaAlerta.Attributes.Add("class", "alert alert-danger")
                    Traduccion.TraducirAlertas(divRecuperoContrasenaAlerta, divRecuperoContrasenaAlerta.ID & Flag, DirectCast(Session("Idioma"), CultureInfo))
                    'divRecuperoContrasenaAlerta.InnerText = "Verifique los datos, Código o Email inválido"
                ElseIf Flag = 2 Then
                    divRecuperoContrasenaAlerta.Visible = True
                    divRecuperoContrasenaAlerta.Attributes.Add("class", "alert alert-danger")
                    Traduccion.TraducirAlertas(divRecuperoContrasenaAlerta, divRecuperoContrasenaAlerta.ID & Flag, DirectCast(Session("Idioma"), CultureInfo))
                    'divRecuperoContrasenaAlerta.InnerText = "Las contraseñas no coinciden"
                End If
            Else
                divRecuperoContrasenaAlerta.Visible = True
                divRecuperoContrasenaAlerta.Attributes.Add("class", "alert alert-danger")
                Traduccion.TraducirAlertas(divRecuperoContrasenaAlerta, divRecuperoContrasenaAlerta.ID, DirectCast(Session("Idioma"), CultureInfo))
                'divRecuperoContrasenaAlerta.InnerText = "Los campos no pueden estar vacíos"
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
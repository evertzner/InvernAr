Imports BLL
Imports ENTITIES
Imports System.Globalization

Public Class PerfilUsuario
    Inherits System.Web.UI.Page
    Dim ListaRoles As New List(Of RolENT)
    Dim ListaUsuarios As New List(Of UsuarioENT)
    Dim RolV As New RolVista
    Dim UsuarioV As New UsuarioVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvPerfilUsuarioRoles, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirGrillas(gvPerfilUsuarioUsuarios, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divPerfilUsuarioControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            divPerfilUsuarioAlerta.Visible = False
            If Not IsPostBack Then
                Session("Fila") = Nothing
                Session("UsuariosLista") = Nothing
                Session("RolesLista") = Nothing
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
        For i = 0 To gvPerfilUsuarioRoles.Rows.Count - 1
            CType(gvPerfilUsuarioRoles.Rows(i).Cells(0).Controls(1), CheckBox).Checked = False
        Next
        CargarListaUsuarios()
        CargarGridViewUsuarios()
        CargarListaRoles()
        CargarGridViewRoles()
    End Sub

    Private Sub CargarListaUsuarios()
        Session("UsuariosLista") = UsuarioV.UsuarioBLL.ListarUsuarios
    End Sub

    Private Sub CargarGridViewUsuarios()
        gvPerfilUsuarioUsuarios.AutoGenerateColumns = False
        ListaUsuarios = DirectCast(Session("UsuariosLista"), List(Of UsuarioENT))
        gvPerfilUsuarioUsuarios.DataSource = ListaUsuarios
        gvPerfilUsuarioUsuarios.DataBind()
    End Sub

    Private Sub CargarListaRoles()
        Session("RolesLista") = RolV.RolBLL.ListarRoles
    End Sub

    Private Sub CargarGridViewRoles()
        gvPerfilUsuarioRoles.AutoGenerateColumns = False
        ListaRoles = DirectCast(Session("RolesLista"), List(Of RolENT))
        gvPerfilUsuarioRoles.DataSource = ListaRoles
        gvPerfilUsuarioRoles.DataBind()
    End Sub

    Private Sub gvPerfilUsuarioUsuarios_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gvPerfilUsuarioUsuarios.SelectedIndexChanging
        Try
            If Not Session("Fila") Is Nothing Then
                gvPerfilUsuarioUsuarios.Rows(Session("Fila")).Style.Remove("background-color")
            End If
           
            For i = 0 To gvPerfilUsuarioRoles.Rows.Count - 1
                CType(gvPerfilUsuarioRoles.Rows(i).Cells(0).Controls(1), CheckBox).Checked = False
            Next
            Dim ListaUsuarios = CType(Session("UsuariosLista"), List(Of UsuarioENT))
            Session("UsuarioACambiar") = UsuarioV.UsuarioBLL.ListarUsuariosRoles(ListaUsuarios.Item(gvPerfilUsuarioUsuarios.Rows(e.NewSelectedIndex).DataItemIndex))
            For Each RolENT In DirectCast(Session("UsuarioACambiar"), UsuarioENT).ListaRoles
                For i = 0 To gvPerfilUsuarioRoles.Rows.Count - 1
                    If RolENT.Codigo = gvPerfilUsuarioRoles.Rows(i).Cells(1).Text Then
                        CType(gvPerfilUsuarioRoles.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True
                    End If
                Next
            Next
            Session("UsuarioElegidoId") = ListaUsuarios.Item(gvPerfilUsuarioUsuarios.Rows(e.NewSelectedIndex).DataItemIndex).ID
            Session("UsuarioElegidoCorreoElectronico") = ListaUsuarios.Item(gvPerfilUsuarioUsuarios.Rows(e.NewSelectedIndex).DataItemIndex).CorreoElectronico
            Session("Fila") = e.NewSelectedIndex
            gvPerfilUsuarioUsuarios.Rows(Session("Fila")).Style.Add("background-color", "#449d44")
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exSeleccionarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnPerfilUsuarioModificarAsignacion_ServerClick(sender As Object, e As EventArgs) Handles btnPerfilUsuarioModificarAsignacion.ServerClick
        Try
            Dim UsuarioENT As New UsuarioENT
            Dim AP As Boolean = False
            UsuarioENT.ID = Session("UsuarioElegidoId")
            For i = 0 To gvPerfilUsuarioRoles.Rows.Count - 1
                If CType(gvPerfilUsuarioRoles.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    Dim RolENT As New RolENT
                    RolENT.Codigo = gvPerfilUsuarioRoles.Rows(i).Cells(1).Text
                    If RolENT.Codigo = "AP" Then
                        AP = True
                    End If
                    UsuarioENT.ListaRoles.Add(RolENT)
                End If
            Next
            Session("CantidadAP") = 0
            For Each Usuario In DirectCast(Session("UsuariosLista"), List(Of UsuarioENT))
                For Each Rol In UsuarioV.UsuarioBLL.ListarUsuariosRoles(Usuario).ListaRoles
                    If Rol.Codigo = "AP" Then
                        Session("CantidadAP") += 1
                    End If
                Next
            Next
            Dim CantidadAP As Integer = Session("CantidadAP")
            If AP = False And CantidadAP < 2 Then
                '    For Each RolENT In DirectCast(Session("UsuarioACambiar"), UsuarioENT).ListaRoles
                '        If RolENT.Codigo = "AP" Then
                '            UsuarioENT.ListaRoles.Add(RolENT)
                '        End If
                '    Next
                'Else
                Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                     Traduccion.TraducirMensaje("Mensaje6", DirectCast(Session("Idioma"), CultureInfo)), _
                     Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                BindData()
                Exit Sub
            End If
            UsuarioV.UsuarioBLL.BorrarUsuarioRol(UsuarioENT)
            UsuarioV.UsuarioBLL.AgregarUsuarioRol(UsuarioENT)
            Session("UsuarioElegidoId") = Nothing
            Session("UsuarioElegidoCorreoElectronico") = Nothing
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Modificó la asignacion de perfiles del usuario " & UsuarioENT.ID))
            BindData()
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
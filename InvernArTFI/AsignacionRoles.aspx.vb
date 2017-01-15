Imports BLL
Imports ENTITIES
Imports System.Globalization

Public Class AsignacionRoles
    Inherits System.Web.UI.Page
    Dim ListaRoles As New List(Of RolENT)
    Dim ListaPermisos As New List(Of PermisoENT)
    Dim RolV As New RolVista
    Dim PermisoV As New PermisoVista
    Dim ListaIdiomasTraducidos As New List(Of MultidiomaENT)
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvAsignacionRolesPermisos, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirGrillas(gvAsignacionRolesRoles, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divAsignacionRolesControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            divAsignacionRolesAlerta.Visible = False
            If Not IsPostBack Then
                Session("PermisosLista") = Nothing
                Session("RolesLista") = Nothing
                Session("Fila") = Nothing
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
        For i = 0 To gvAsignacionRolesPermisos.Rows.Count - 1
            CType(gvAsignacionRolesPermisos.Rows(i).Cells(0).Controls(1), CheckBox).Checked = False
        Next
        CargarListaRoles()
        CargarGridViewRoles()
        CargarListaPermisos()
        CargarGridViewPermisos()
    End Sub

    Private Sub CargarListaRoles()
        Session("RolesLista") = RolV.RolBLL.ListarRoles
    End Sub

    Private Sub CargarGridViewRoles()
        gvAsignacionRolesRoles.AutoGenerateColumns = False
        ListaRoles = DirectCast(Session("RolesLista"), List(Of RolENT))
        gvAsignacionRolesRoles.DataSource = ListaRoles
        gvAsignacionRolesRoles.DataBind()
    End Sub

    Private Sub CargarListaPermisos()
        Session("PermisosLista") = PermisoV.PermisoBLL.ListarPermisos
    End Sub

    Private Sub CargarGridViewPermisos()
        gvAsignacionRolesPermisos.AutoGenerateColumns = False
        ListaPermisos = DirectCast(Session("PermisosLista"), List(Of PermisoENT))
        gvAsignacionRolesPermisos.DataSource = ListaPermisos
        gvAsignacionRolesPermisos.DataBind()
    End Sub

    Private Sub gvAsignacionRolesRoles_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gvAsignacionRolesRoles.SelectedIndexChanging
        Try
            If Not Session("Fila") Is Nothing Then
                gvAsignacionRolesRoles.Rows(Session("Fila")).Style.Remove("background-color")
            End If
            For i = 0 To gvAsignacionRolesPermisos.Rows.Count - 1
                CType(gvAsignacionRolesPermisos.Rows(i).Cells(0).Controls(1), CheckBox).Checked = False
            Next
            Dim ListaRoles = CType(Session("RolesLista"), List(Of RolENT))
            Session("Rol") = RolV.RolBLL.ListarRolesPermisos(ListaRoles.Item(gvAsignacionRolesRoles.Rows(e.NewSelectedIndex).DataItemIndex))
            For Each PermisoENT In DirectCast(Session("Rol"), RolENT).ListaPermisos
                For i = 0 To gvAsignacionRolesPermisos.Rows.Count - 1
                    If PermisoENT.Codigo = gvAsignacionRolesPermisos.Rows(i).Cells(1).Text Then
                        CType(gvAsignacionRolesPermisos.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True
                    End If
                Next
            Next
            Session("RolElegido") = ListaRoles.Item(gvAsignacionRolesRoles.Rows(e.NewSelectedIndex).DataItemIndex).Codigo
            Session("Fila") = e.NewSelectedIndex
            gvAsignacionRolesRoles.Rows(Session("Fila")).Style.Add("background-color", "#449d44")
            For i = 0 To gvAsignacionRolesPermisos.Rows.Count - 1
                If gvAsignacionRolesPermisos.Rows(i).Cells(1).Text = "AR" And CType(gvAsignacionRolesPermisos.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    gvAsignacionRolesPermisos.Rows(i).Cells(0).Enabled = False
                Else
                    gvAsignacionRolesPermisos.Rows(i).Cells(0).Enabled = True
                End If
            Next
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exSeleccionarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvAsignacionRolesRoles_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvAsignacionRolesRoles.RowCancelingEdit
        Try
            gvAsignacionRolesRoles.EditIndex = -1
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvAsignacionRolesRoles_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvAsignacionRolesRoles.RowEditing
        Try
            gvAsignacionRolesRoles.EditIndex = e.NewEditIndex
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvAsignacionRolesRoles_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAsignacionRolesRoles.RowUpdating
        Try
            Dim ListaRoles = CType(Session("RolesLista"), List(Of RolENT))
            Dim row = gvAsignacionRolesRoles.Rows(e.RowIndex)
            With ListaRoles.Item(row.DataItemIndex)
                .Codigo = row.Cells(2).Text
                .Nombre = CType(row.Cells(3).Controls(1), TextBox).Text
            End With
            Dim RolENT As New RolENT
            With RolENT
                .Codigo = ListaRoles.Item(row.DataItemIndex).Codigo
                .Nombre = ListaRoles.Item(row.DataItemIndex).Nombre
            End With
            RolV.RolBLL.ModificarRol(RolENT)
            gvAsignacionRolesRoles.EditIndex = -1
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Modificó el rol " & ListaRoles.Item(row.DataItemIndex).Codigo))
            BindData()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnAsignacionRolesNuevo_ServerClick(sender As Object, e As EventArgs) Handles btnAsignacionRolesNuevo.ServerClick
        Try
            Dim CampoVacio As Boolean = False
            For Each c As Control In divAsignacionRolesControles.Controls
                If TypeOf (c) Is HtmlInputText Then
                    If DirectCast(c, HtmlInputText).Value = "" Then
                        CampoVacio = True
                        Exit For
                    End If
                End If
            Next
            If CampoVacio = False Then
                Dim MismoNombre As Boolean = False
                Dim RolENT As New RolENT
                RolENT.Codigo = txtAsignacionRolesCodigo.Value
                RolENT.Nombre = txtAsignacionRolesNombre.Value
                For Each Rol In DirectCast(Session("RolesLista"), List(Of RolENT))
                    If Rol.Nombre = RolENT.Nombre Then
                        MismoNombre = True
                        Exit For
                    End If
                Next
                If MismoNombre = False Then
                    RolV.RolBLL.AltaRol(RolENT)
                    BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                            "Dio de alta el rol " & RolENT.Codigo))
                Else
                    Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                              Traduccion.TraducirMensaje("Mensaje4", DirectCast(Session("Idioma"), CultureInfo)), _
                              Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                End If
                txtAsignacionRolesCodigo.Value = ""
                txtAsignacionRolesNombre.Value = ""
                BindData()
                LimpiarCampos()
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("CamposVacios", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnAsignacionRolesModificarAsignacion_ServerClick(sender As Object, e As EventArgs) Handles btnAsignacionRolesModificarAsignacion.ServerClick
        Try
            Dim RolENT As New RolENT
            RolENT.Codigo = Session("RolElegido")
            For i = 0 To gvAsignacionRolesPermisos.Rows.Count - 1
                If CType(gvAsignacionRolesPermisos.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    Dim PermisoENT As New PermisoENT
                    PermisoENT.Codigo = gvAsignacionRolesPermisos.Rows(i).Cells(1).Text
                    RolENT.ListaPermisos.Add(PermisoENT)
                End If
            Next
            RolV.RolBLL.BorrarRolPermiso(RolENT)
            RolV.RolBLL.AgregarRolPermiso(RolENT)
            Session("RolElegido") = Nothing
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Cambió la asignación del rol " & RolENT.Codigo))
            BindData()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub btnAsignacionRolesEliminarRol_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim Codigo = TryCast(sender, ImageButton).CommandArgument
            Dim ListaRoles = CType(Session("RolesLista"), List(Of RolENT))
            Dim RolENT As New RolENT
            RolENT.Codigo = Codigo
            If RolENT.Codigo = "AP" Then
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("Mensaje21", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            Else
                If RolV.RolBLL.BajaRol(RolENT) > 0 Then
                    For i = 0 To ListaRoles.Count - 1
                        If ListaRoles.Item(i).Codigo = Codigo Then
                            ListaRoles.RemoveAt(i)
                            Exit For
                        End If
                    Next
                    If ListaRoles.Count = 0 Then
                        Session("RolesLista") = Nothing
                    End If
                    BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                            "Eliminó el rol " & RolENT.Codigo))
                    BindData()
                Else
                    Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                               Traduccion.TraducirMensaje("Mensaje7", DirectCast(Session("Idioma"), CultureInfo)), _
                               Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                End If
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
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

    Sub LimpiarCampos()
        txtAsignacionRolesCodigo.Value = ""
        txtAsignacionRolesNombre.Value = ""
    End Sub

End Class
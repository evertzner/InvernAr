Imports BLL
Imports ENTITIES
Imports System.Security.Cryptography
Imports System.Globalization

Public Class SeguridadUsuarios
    Inherits System.Web.UI.Page
    Dim UsuarioV As New UsuarioVista
    Dim Lista As New List(Of UsuarioENT)
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvSeguridadUsuariosUsuarios, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divSeguridadUsuariosControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                Session("UsuarioLista") = Nothing
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
        Session("UsuarioLista") = UsuarioV.UsuarioBLL.ListarUsuarios
    End Sub

    Private Sub CargarGridView()
        gvSeguridadUsuariosUsuarios.AutoGenerateColumns = False
        Lista = DirectCast(Session("UsuarioLista"), List(Of UsuarioENT))
        gvSeguridadUsuariosUsuarios.DataSource = Lista
        gvSeguridadUsuariosUsuarios.DataBind()
    End Sub

    Private Sub gvSeguridadUsuariosUsuarios_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvSeguridadUsuariosUsuarios.RowCancelingEdit
        Try
            gvSeguridadUsuariosUsuarios.EditIndex = -1
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvSeguridadUsuariosUsuarios_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvSeguridadUsuariosUsuarios.RowEditing
        Try
            gvSeguridadUsuariosUsuarios.EditIndex = e.NewEditIndex
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvSeguridadUsuariosUsuarios_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvSeguridadUsuariosUsuarios.RowUpdating
        Try
            Dim ListaUsuarios = CType(Session("UsuarioLista"), List(Of UsuarioENT))
            Dim row = gvSeguridadUsuariosUsuarios.Rows(e.RowIndex)
            If Not (CType(row.Cells(13).Controls(1), TextBox).Text = "" And CType(row.Cells(17).Controls(0), CheckBox).Checked = False) Then
                With ListaUsuarios.Item(row.DataItemIndex)
                    .CorreoElectronico = CType(row.Cells(6).Controls(1), TextBox).Text
                    .Contraseña = CType(row.Cells(13).Controls(1), TextBox).Text
                    .IntentosFallidos = CType(row.Cells(14).Controls(1), TextBox).Text
                    .Bloqueado = CType(row.Cells(15).Controls(0), CheckBox).Checked
                    .Validado = CType(row.Cells(16).Controls(0), CheckBox).Checked
                    .Baja = CType(row.Cells(17).Controls(0), CheckBox).Checked
                End With
                Dim UsuarioENT As New UsuarioENT
                With UsuarioENT
                    .ID = ListaUsuarios.Item(row.DataItemIndex).ID
                    .DNI = ListaUsuarios.Item(row.DataItemIndex).DNI
                    .CUIT = ListaUsuarios.Item(row.DataItemIndex).CUIT
                    .Nombre = ListaUsuarios.Item(row.DataItemIndex).Nombre
                    .Apellido = ListaUsuarios.Item(row.DataItemIndex).Apellido
                    .CorreoElectronico = ListaUsuarios.Item(row.DataItemIndex).CorreoElectronico
                    .Domicilio = ListaUsuarios.Item(row.DataItemIndex).Domicilio
                    .Localidad = ListaUsuarios.Item(row.DataItemIndex).Localidad
                    .Provincia = ListaUsuarios.Item(row.DataItemIndex).Provincia
                    .Telefono = ListaUsuarios.Item(row.DataItemIndex).Telefono
                    .Interno = ListaUsuarios.Item(row.DataItemIndex).Interno
                    .TelefonoCelular = ListaUsuarios.Item(row.DataItemIndex).TelefonoCelular
                    .Contraseña = UsuarioV.UsuarioBLL.TestEncoding(ListaUsuarios.Item(row.DataItemIndex).Contraseña, ListaUsuarios.Item(row.DataItemIndex).ID)
                    .IntentosFallidos = ListaUsuarios.Item(row.DataItemIndex).IntentosFallidos
                    .Bloqueado = ListaUsuarios.Item(row.DataItemIndex).Bloqueado
                    .Validado = ListaUsuarios.Item(row.DataItemIndex).Validado
                    .Baja = ListaUsuarios.Item(row.DataItemIndex).Baja
                End With
                If UsuarioV.UsuarioBLL.ModificarUsuario(UsuarioENT) > 0 Then
                    BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                            "Ha modificado los datos del usuario " & UsuarioENT.ID))
                    gvSeguridadUsuariosUsuarios.EditIndex = -1
                    gvSeguridadUsuariosUsuarios.AutoGenerateColumns = False
                    gvSeguridadUsuariosUsuarios.DataSource = ListaUsuarios
                    gvSeguridadUsuariosUsuarios.DataBind()
                Else
                    Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                          Traduccion.TraducirMensaje("Mensaje5", DirectCast(Session("Idioma"), CultureInfo)), _
                          Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                End If
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                           "Debe especificar una contraseña", _
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
Imports BLL
Imports ENTITIES
Imports System.IO
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.Globalization

Public Class Usuarios
    Inherits System.Web.UI.Page
    Dim UsuarioV As New UsuarioVista
    Dim BitacoraV As New BitacoraVista
    Dim RolV As New RolVista
    Dim Lista As New List(Of UsuarioENT)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvUsuariosUsuarios, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divUsuariosControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divUsuariosAlta.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divUsuariosListado.Controls, DirectCast(Session("Idioma"), CultureInfo))
            'divUsuariosAlerta.Visible = False
            If Not IsPostBack Then
                divUsuariosAlta.Style("display") = ""
                divUsuariosListado.Style("display") = "none"
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
        LlenarListas()
    End Sub

    Private Sub CargarLista()
        Session("UsuarioLista") = UsuarioV.UsuarioBLL.ListarUsuarios
    End Sub

    Private Sub CargarGridView()
        gvUsuariosUsuarios.AutoGenerateColumns = False
        For Each Usuario In DirectCast(Session("UsuarioLista"), List(Of UsuarioENT))
            If Not Usuario.Baja = True Then
                Lista.Add(Usuario)
            End If
        Next
        gvUsuariosUsuarios.DataSource = Lista
        gvUsuariosUsuarios.DataBind()
    End Sub

    Private Sub LlenarListas()
        ddlUsuariosRoles.Items.Clear()
        For Each item In RolV.RolBLL.ListarRolesSinUFC
            ddlUsuariosRoles.Items.Add(item.Codigo)
        Next
        ddlUsuariosRoles.Items.Insert(0, "")
    End Sub

    Private Sub btnUsuariosConfirmar_ServerClick(sender As Object, e As EventArgs) Handles btnUsuariosConfirmar.ServerClick
        Try
            Dim CampoVacio As Boolean = False
            For Each c As Control In divUsuariosAlta.Controls
                If TypeOf (c) Is HtmlInputText Then
                    If DirectCast(c, HtmlInputText).Value = "" Then
                        If DirectCast(c, HtmlInputText).ID = "txtUsuariosInterno" Or DirectCast(c, HtmlInputText).ID = "txtUsuariosTelefonoCelular" Then
                            CampoVacio = False
                            Exit For
                        Else
                            CampoVacio = True
                            Exit For
                        End If
                    End If
                End If
            Next
            If ddlUsuariosRoles.SelectedValue <> "" Then
                If CampoVacio = False Then
                    If txtUsuariosCorreoElectronico.Value = txtUsuariosCorreoElectronicoRepetido.Value Then
                        RolV.RolENT.Codigo = ddlUsuariosRoles.SelectedValue
                        If txtUsuariosPassNueva.Value = txtUsuariosPassNuevaRepetida.Value Then
                            With UsuarioV.UsuarioENT
                                .DNI = txtUsuariosDNI.Value
                                .CUIT = txtUsuariosCUIT.Value
                                .Nombre = txtUsuariosNombre.Value
                                .Apellido = txtUsuariosApellido.Value
                                .CorreoElectronico = txtUsuariosCorreoElectronico.Value
                                .Domicilio = txtUsuariosDomicilio.Value
                                .Localidad = txtUsuariosLocalidad.Value
                                .Provincia = txtUsuariosProvincia.Value
                                .Telefono = txtUsuariosTelefono.Value
                                .Interno = txtUsuariosInterno.Value
                                .TelefonoCelular = txtUsuariosTelefonoCelular.Value
                                .Contraseña = txtUsuariosPassNueva.Value
                            End With
                            If UsuarioV.UsuarioBLL.NuevoUsuario(UsuarioV.UsuarioENT, RolV.RolENT) > 0 Then
                                'divUsuariosAlerta.Visible = True
                                'divUsuariosAlerta.Attributes.Add("class", "alert alert-success")
                                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                                        "Registró al usuario " & UsuarioV.UsuarioENT.CorreoElectronico))

                                If RolV.RolENT.Codigo = "UFC" Then
                                    Abrirmodal(Traduccion.TraducirMensaje("MensajeUsuariosNotificacion", DirectCast(Session("Idioma"), CultureInfo)), _
                                               Traduccion.TraducirMensaje("divUsuariosAlerta1a", DirectCast(Session("Idioma"), CultureInfo)), _
                                               Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))

                                    'Traduccion.TraducirAlertas(divUsuariosAlerta, divUsuariosAlerta.ID & "1a", DirectCast(Session("Idioma"), CultureInfo))
                                    'divUsuariosAlerta.InnerText = "Se ha enviado un mail de confirmación, por favor entre al link dentro del mail para validar su cuenta"
                                    EnviarMail(UsuarioV.UsuarioENT.CorreoElectronico, UsuarioV.UsuarioENT.Nombre)
                                Else
                                    Abrirmodal(Traduccion.TraducirMensaje("MensajeUsuariosNotificacion", DirectCast(Session("Idioma"), CultureInfo)), _
                                              Traduccion.TraducirMensaje("divUsuariosAlerta1b", DirectCast(Session("Idioma"), CultureInfo)), _
                                              Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))

                                    'Traduccion.TraducirAlertas(divUsuariosAlerta, divUsuariosAlerta.ID & "1b", DirectCast(Session("Idioma"), CultureInfo))
                                    'divUsuariosAlerta.InnerText = "Se ha creado el usuario"
                                End If
                                LimpiarCampos()
                                BindData()
                            Else
                                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("divUsuariosAlerta0", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                                'divUsuariosAlerta.Visible = True
                                'divUsuariosAlerta.Attributes.Add("class", "alert alert-danger")
                                'Traduccion.TraducirAlertas(divUsuariosAlerta, divUsuariosAlerta.ID & "0", DirectCast(Session("Idioma"), CultureInfo))
                                ''divUsuariosAlerta.InnerText = "El correo electrónico ya pertenece a un usuario registrado"
                            End If
                        Else
                            Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                         Traduccion.TraducirMensaje("divUsuariosAlerta2", DirectCast(Session("Idioma"), CultureInfo)), _
                         Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                            'divUsuariosAlerta.Visible = True
                            'divUsuariosAlerta.Attributes.Add("class", "alert alert-danger")
                            'Traduccion.TraducirAlertas(divUsuariosAlerta, divUsuariosAlerta.ID & "2", DirectCast(Session("Idioma"), CultureInfo))
                            ''divUsuariosAlerta.InnerText = "Las contraseñas son diferentes"
                        End If
                    Else
                        Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                        Traduccion.TraducirMensaje("divUsuariosAlerta3", DirectCast(Session("Idioma"), CultureInfo)), _
                        Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                        'divUsuariosAlerta.Visible = True
                        'divUsuariosAlerta.Attributes.Add("class", "alert alert-danger")
                        'Traduccion.TraducirAlertas(divUsuariosAlerta, divUsuariosAlerta.ID & "3", DirectCast(Session("Idioma"), CultureInfo))
                        ''divUsuariosAlerta.InnerText = "Asegúrese de que confirmó bien el correo electrónico"
                    End If
                Else
                    Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                        Traduccion.TraducirMensaje("divUsuariosAlerta4", DirectCast(Session("Idioma"), CultureInfo)), _
                        Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                    'divUsuariosAlerta.Visible = True
                    'divUsuariosAlerta.Attributes.Add("class", "alert alert-danger")
                    'Traduccion.TraducirAlertas(divUsuariosAlerta, divUsuariosAlerta.ID & "4", DirectCast(Session("Idioma"), CultureInfo))
                    ''divUsuariosAlerta.InnerText = "No pueden haber campos vacíos"
                End If
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                        Traduccion.TraducirMensaje("divUsuariosAlerta5", DirectCast(Session("Idioma"), CultureInfo)), _
                        Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                'divUsuariosAlerta.Visible = True
                'divUsuariosAlerta.Attributes.Add("class", "alert alert-danger")
                'Traduccion.TraducirAlertas(divUsuariosAlerta, divUsuariosAlerta.ID & "5", DirectCast(Session("Idioma"), CultureInfo))
                ''divUsuariosAlerta.InnerText = "Debe seleccionar un rol"
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub LimpiarCampos()
        txtUsuariosApellido.Value = ""
        txtUsuariosCorreoElectronico.Value = ""
        txtUsuariosCorreoElectronicoRepetido.Value = ""
        txtUsuariosCUIT.Value = ""
        txtUsuariosDNI.Value = ""
        txtUsuariosDomicilio.Value = ""
        txtUsuariosInterno.Value = ""
        txtUsuariosLocalidad.Value = ""
        txtUsuariosNombre.Value = ""
        txtUsuariosPassNueva.Value = ""
        txtUsuariosPassNuevaRepetida.Value = ""
        txtUsuariosProvincia.Value = ""
        txtUsuariosTelefono.Value = ""
        txtUsuariosTelefonoCelular.Value = ""
        ddlUsuariosRoles.SelectedIndex = 0
    End Sub

    Sub EnviarMail(CorreoElectronico As String, Nombre As String)
        Dim Cliente As New SmtpClient()
        Dim VistaAlternativa As AlternateView
        VistaAlternativa = AlternateView.CreateAlternateViewFromString(PopulateBody(Nombre, UsuarioV.UsuarioBLL.Encriptar(MD5.Create, CorreoElectronico)), _
                                                                       Nothing, System.Net.Mime.MediaTypeNames.Text.Html)
        Dim Mensaje As New MailMessage()
        Dim Imagen As LinkedResource = New LinkedResource("C:\Users\Esteban\Documents\Visual Studio 2012\Projects\InvernArTFI\InvernArTFI\images\LogoWeb.png", _
                                                          System.Net.Mime.MediaTypeNames.Image.Jpeg)
        Mensaje.To.Add(CorreoElectronico)
        Mensaje.Subject = "Bienvenido"
        Mensaje.IsBodyHtml = True
        Imagen.ContentId = "Pic1"
        VistaAlternativa.LinkedResources.Add(Imagen)
        Mensaje.AlternateViews.Add(VistaAlternativa)
        Try
            Cliente.Send(Mensaje)
        Catch ex As Exception
            ' ...
        End Try
    End Sub

    Private Function PopulateBody(Nombre As String, CorreoElectronico As String) As String
        Dim body As String = String.Empty
        Dim Link As String = "http://localhost:1698/Validacion.aspx?mail=" & CorreoElectronico
        Dim reader As StreamReader = New StreamReader(Server.MapPath("~\Cuerpos de mail\NuevoUsuario.html"))
        body = reader.ReadToEnd
        body = body.Replace("{UserName}", Nombre)
        body = body.Replace("{Link}", "<a href=""" & Link & """>" & Link & "</a>")
        Return body
    End Function

    Private Sub gvUsuariosUsuarios_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvUsuariosUsuarios.RowCancelingEdit
        Try
            gvUsuariosUsuarios.EditIndex = -1
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvUsuariosUsuarios_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvUsuariosUsuarios.RowEditing
        Try
            gvUsuariosUsuarios.EditIndex = e.NewEditIndex
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvUsuariosUsuarios_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvUsuariosUsuarios.RowUpdating
        Try
            Dim ListaUsuarios = CType(Session("UsuarioLista"), List(Of UsuarioENT))
            Dim row = gvUsuariosUsuarios.Rows(e.RowIndex)
            With ListaUsuarios.Item(row.DataItemIndex)
                .DNI = (CType((row.Cells(3).Controls(1)), TextBox)).Text
                .CUIT = (CType((row.Cells(4).Controls(1)), TextBox)).Text
                .Nombre = (CType((row.Cells(5).Controls(1)), TextBox)).Text
                .Apellido = (CType((row.Cells(6).Controls(1)), TextBox)).Text
                .CorreoElectronico = (CType((row.Cells(7).Controls(1)), TextBox)).Text
                .Domicilio = (CType((row.Cells(8).Controls(1)), TextBox)).Text
                .Localidad = (CType((row.Cells(9).Controls(1)), TextBox)).Text
                .Provincia = (CType((row.Cells(10).Controls(1)), TextBox)).Text
                .Telefono = (CType((row.Cells(11).Controls(1)), TextBox)).Text
                .Interno = (CType((row.Cells(12).Controls(1)), TextBox)).Text
                .TelefonoCelular = (CType((row.Cells(13).Controls(1)), TextBox)).Text
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
                .Contraseña = ListaUsuarios.Item(row.DataItemIndex).Contraseña
                .IntentosFallidos = ListaUsuarios.Item(row.DataItemIndex).IntentosFallidos
                .Bloqueado = ListaUsuarios.Item(row.DataItemIndex).Bloqueado
                .Validado = ListaUsuarios.Item(row.DataItemIndex).Validado
            End With
            UsuarioV.UsuarioBLL.ModificarUsuario(UsuarioENT)
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Modificó al usuario " & UsuarioENT.CorreoElectronico))
            gvUsuariosUsuarios.EditIndex = -1
            Session("UsuarioLista") = ListaUsuarios
            CargarGridView()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub aUsuariosAlta_ServerClick(sender As Object, e As EventArgs) Handles aUsuariosAlta.ServerClick
        Try
            divUsuariosAlta.Style("display") = ""
            divUsuariosListado.Style("display") = "none"
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub aUsuariosListado_ServerClick(sender As Object, e As EventArgs) Handles aUsuariosListado.ServerClick
        Try
            divUsuariosAlta.Style("display") = "none"
            divUsuariosListado.Style("display") = ""
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
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

    Protected Sub btnUsuariosEliminar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim Id = TryCast(sender, ImageButton).CommandArgument
            Dim ListaUsuarios = CType(Session("UsuarioLista"), List(Of UsuarioENT))
            Dim UsuarioENT As New UsuarioENT
            UsuarioENT.ID = Id
            If UsuarioV.UsuarioBLL.EliminarUsuario(UsuarioENT) > 0 Then
                For i = 0 To ListaUsuarios.Count - 1
                    If ListaUsuarios.Item(i).ID = Id Then
                        ListaUsuarios.RemoveAt(i)
                        Exit For
                    End If
                Next
                If ListaUsuarios.Count = 0 Then
                    Session("UsuarioLista") = Nothing
                End If
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Eliminó al usuario " & UsuarioENT.ID))
                Session("UsuarioLista") = ListaUsuarios
                CargarGridView()
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("Mensaje5", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvUsuariosUsuarios_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvUsuariosUsuarios.PageIndexChanging
        Try
            gvUsuariosUsuarios.PageIndex = e.NewPageIndex
            CargarGridView()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

End Class
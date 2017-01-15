Imports ENTITIES
Imports BLL
Imports System.Globalization

Public Class Chat
    Inherits System.Web.UI.Page
    Dim ChatV As New ChatVista
    Dim Respuesta As Boolean = True
    Dim UFC As Boolean
    Dim UsuarioSeleccionado As Integer
    Dim Receptor As String = ""
    Dim Emisor As String = ""
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divChatCantidadNoLeidos.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divChatChat.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                Session("Fila") = Nothing
                If Session("UFC") = True Then
                    divChatChat.Visible = True
                    Respuesta = False
                    ChatV.ChatENT.IdUsuario = DirectCast(Session("Usuario"), UsuarioENT).ID
                    ChatV.ChatENT.Respuesta = True
                    ChatV.ChatBLL.LeerMensaje(ChatV.ChatENT)
                    divChatCantidadNoLeidos.Visible = False
                    CargarLista()
                    CargarChat()
                Else
                    divChatChat.Visible = False
                    Respuesta = True
                    divChatCantidadNoLeidos.Visible = True
                    Session("ChatsTotales") = ChatV.ChatBLL.ListarCantidadDeMensajes
                    gvChatChats.AutoGenerateColumns = False
                    gvChatChats.DataSource = DirectCast(Session("ChatsTotales"), List(Of ChatCantidadENT))
                    gvChatChats.DataBind()
                End If
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
    End Sub

    Private Sub CargarLista()
        If Session("UFC") = True Then
            Session("Chat") = ChatV.ChatBLL.ListarMensajes(ChatV.ChatENT)
        Else
            ChatV.ChatENT.IdUsuario = Session("UsuarioSeleccionado")
            Session("Chat") = ChatV.ChatBLL.ListarMensajes(ChatV.ChatENT)
        End If

    End Sub

    Private Sub CargarChat()
        Dim Iterador As Integer = 0
        Dim Chat As String = ""
        ulChatVentana.InnerHtml = ""
        For Each Mensaje In DirectCast(Session("Chat"), List(Of ChatENT))
            If (Session("UFC") = False And Mensaje.Respuesta = True) Or (Session("UFC") = True And Mensaje.Respuesta = False) Then
                Chat += "<li class=""left clearfix"">" & _
                            "<div class=""chat-body clearfix"">" & _
                                "<div class=""header"">" & _
                                    "<small class=""pull-right text-muted"">" & _
                                        "<span class=""glyphicon glyphicon-time""></span>" & Mensaje.FechaHora & "</small>" & _
                                "</div>" & _
                                "</br><div class=""pull-right""><p>" & Mensaje.Mensaje & "</p></div>" & _
                            "</div>" & _
                        "</li>"
            Else
                Chat += "<li class=""right clearfix"">" & _
                            "<div class=""chat-body clearfix"">" & _
                                "<div class=""header"">" & _
                                    "<small class=""pull-left text-muted"">" & _
                                        "<span class=""glyphicon glyphicon-time""></span>" & Mensaje.FechaHora & "</small>" & _
                                "</div>" & _
                                "</br><div class=""pull-left""><p>" & Mensaje.Mensaje & "</p></div>" & _
                            "</div>" & _
                        "</li>"
            End If
        Next
        ulChatVentana.InnerHtml = Chat
    End Sub

    Private Sub gvChatChats_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gvChatChats.SelectedIndexChanging
        Try
            If Not Session("Fila") Is Nothing Then
                gvChatChats.Rows(Session("Fila")).Style.Remove("background-color")
            End If
            Session("UsuarioSeleccionado") = DirectCast(Session("ChatsTotales"), List(Of ChatCantidadENT))(gvChatChats.Rows(e.NewSelectedIndex).DataItemIndex).IdUsuario
            Session("Fila") = e.NewSelectedIndex
            gvChatChats.Rows(Session("Fila")).Style.Add("background-color", "#449d44")
            ChatV.ChatENT.IdUsuario = Session("UsuarioSeleccionado")
            ChatV.ChatENT.Respuesta = False
            ChatV.ChatBLL.LeerMensaje(ChatV.ChatENT)
            divChatChat.Visible = True
            CargarLista()
            CargarChat()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exSeleccionarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnChatEnviar_ServerClick(sender As Object, e As EventArgs) Handles btnChatEnviar.Click
        Try
            If Session("UFC") = False Then
                ChatV.ChatENT.IdUsuario = Session("UsuarioSeleccionado")
                ChatV.ChatENT.Mensaje = txtChatMensaje.Value
                ChatV.ChatENT.FechaHora = Now
                ChatV.ChatENT.Respuesta = True
                ChatV.ChatBLL.NuevoMensaje(ChatV.ChatENT)
            Else
                ChatV.ChatENT.IdUsuario = DirectCast(Session("Usuario"), UsuarioENT).ID
                ChatV.ChatENT.Mensaje = txtChatMensaje.Value
                ChatV.ChatENT.FechaHora = Now
                ChatV.ChatENT.Respuesta = False
                ChatV.ChatBLL.NuevoMensaje(ChatV.ChatENT)
            End If
            CargarLista()
            CargarChat()
            txtChatMensaje.Value = ""
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
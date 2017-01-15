Imports BLL
Imports ENTITIES
Imports System.Globalization

Public Class GestionEncuestas
    Inherits System.Web.UI.Page
    Dim EncuestaV As New EncuestaVista
    Dim PreguntaEncuestaV As New PreguntaEncuestaVista
    Dim ListaEncuesta As New List(Of EncuestaENT)
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvGestionEncuestasEncuestas, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirGrillas(gvGestionEncuestasPreguntas, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divGestionEncuestasControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                Session("Fila") = Nothing
            End If
            If Not IsPostBack And Session("EncuestaEliminada") = True Then
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                              Traduccion.TraducirMensaje("MensajeEncuestaEliminada", DirectCast(Session("Idioma"), CultureInfo)), _
                              Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
            If Not IsPostBack Then
                divGestionEncuestasAlerta.Visible = False
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
        CargarListaEncuestas()
        CargarGridViewEncuestas()
    End Sub

    Private Sub CargarListaEncuestas()
        Session("EncuestaLista") = EncuestaV.EncuestaBLL.ListarEncuestas
    End Sub

    Private Sub CargarGridViewEncuestas()
        gvGestionEncuestasEncuestas.AutoGenerateColumns = False
        ListaEncuesta = DirectCast(Session("EncuestaLista"), List(Of EncuestaENT))
        gvGestionEncuestasEncuestas.DataSource = ListaEncuesta
        gvGestionEncuestasEncuestas.DataBind()
    End Sub

    Private Sub gvGestionEncuestasEncuestas_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles gvGestionEncuestasEncuestas.SelectedIndexChanging
        Try
            If Not Session("Fila") Is Nothing Then
                gvGestionEncuestasEncuestas.Rows(Session("Fila")).Style.Remove("background-color")
            End If
            Session("EncuestaElegida") = DirectCast(Session("EncuestaLista"), List(Of EncuestaENT))(gvGestionEncuestasEncuestas.Rows(e.NewSelectedIndex).DataItemIndex)
            Session("Fila") = e.NewSelectedIndex
            gvGestionEncuestasEncuestas.Rows(Session("Fila")).Style.Add("background-color", "#449d44")
            CargarListaPreguntas()
            CargarGridViewPreguntas()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exSeleccionarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub CargarListaPreguntas()
        Dim PreguntaEncuesta As New PreguntaEncuestaENT
        PreguntaEncuesta.IdEncuesta = DirectCast(Session("EncuestaElegida"), EncuestaENT).Id
        Session("PreguntasEncuesta") = PreguntaEncuestaV.PreguntaEncuestaBLL.ListarPreguntas(PreguntaEncuesta)
    End Sub

    Private Sub CargarGridViewPreguntas()
        gvGestionEncuestasPreguntas.AutoGenerateColumns = False
        gvGestionEncuestasPreguntas.DataSource = DirectCast(Session("PreguntasEncuesta"), List(Of PreguntaEncuestaENT))
        gvGestionEncuestasPreguntas.DataBind()
    End Sub

    Private Sub gvGestionEncuestasEncuestas_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvGestionEncuestasEncuestas.RowCancelingEdit
        Try
            gvGestionEncuestasEncuestas.EditIndex = -1
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvGestionEncuestasEncuestas_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvGestionEncuestasEncuestas.RowEditing
        Try
            gvGestionEncuestasEncuestas.EditIndex = e.NewEditIndex
            BindData()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvGestionEncuestasEncuestas_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvGestionEncuestasEncuestas.RowUpdating
        Try
            Dim ListaEncuesta = CType(Session("EncuestaLista"), List(Of EncuestaENT))
            Dim row = gvGestionEncuestasEncuestas.Rows(e.RowIndex)
            With ListaEncuesta.Item(row.DataItemIndex)
                .Id = DirectCast(Session("EncuestaLista"), List(Of EncuestaENT))(gvGestionEncuestasEncuestas.Rows(e.RowIndex).DataItemIndex).Id
                .Tema = CType(row.Cells(3).Controls(1), TextBox).Text
                .FechaVencimiento = CType(row.Cells(4).Controls(1), HtmlInputText).Value
            End With
            Dim EncuestaENT As New EncuestaENT
            With EncuestaENT
                .Id = ListaEncuesta.Item(row.DataItemIndex).Id
                .Tema = ListaEncuesta.Item(row.DataItemIndex).Tema
                .FechaVencimiento = ListaEncuesta.Item(row.DataItemIndex).FechaVencimiento
            End With
            EncuestaV.EncuestaBLL.ModificarEncuesta(EncuestaENT)
            gvGestionEncuestasEncuestas.EditIndex = -1
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Modificó el tema de la encuesta " & EncuestaENT.Id))
            LimpiarCampos()
            BindData()
            Session("EncuestaEliminada") = False
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvGestionEncuestasPreguntas_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvGestionEncuestasPreguntas.RowCancelingEdit
        Try
            gvGestionEncuestasPreguntas.EditIndex = -1
            CargarListaPreguntas()
            CargarGridViewPreguntas()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvGestionEncuestasPreguntas_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvGestionEncuestasPreguntas.RowEditing
        Try
            gvGestionEncuestasPreguntas.EditIndex = e.NewEditIndex
            CargarListaPreguntas()
            CargarGridViewPreguntas()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvGestionEncuestasPreguntas_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvGestionEncuestasPreguntas.RowUpdating
        Try
            Dim ListaPreguntasEncuesta = CType(Session("PreguntasEncuesta"), List(Of PreguntaEncuestaENT))
            Dim row = gvGestionEncuestasPreguntas.Rows(e.RowIndex)
            With ListaPreguntasEncuesta.Item(row.DataItemIndex)
                .Id = DirectCast(Session("PreguntasEncuesta"), List(Of PreguntaEncuestaENT))(gvGestionEncuestasPreguntas.Rows(e.RowIndex).DataItemIndex).Id
                .IdEncuesta = DirectCast(Session("PreguntasEncuesta"), List(Of PreguntaEncuestaENT))(gvGestionEncuestasPreguntas.Rows(e.RowIndex).DataItemIndex).IdEncuesta
                .IdPregunta = CType(row.Cells(4).Controls(1), TextBox).Text
                .Pregunta = CType(row.Cells(5).Controls(1), TextBox).Text
            End With
            Dim PreguntaEncuestaENT As New PreguntaEncuestaENT
            With PreguntaEncuestaENT
                .Id = ListaPreguntasEncuesta.Item(row.DataItemIndex).Id
                .IdEncuesta = ListaPreguntasEncuesta.Item(row.DataItemIndex).IdEncuesta
                .IdPregunta = ListaPreguntasEncuesta.Item(row.DataItemIndex).IdPregunta
                .Pregunta = ListaPreguntasEncuesta.Item(row.DataItemIndex).Pregunta
            End With
            PreguntaEncuestaV.PreguntaEncuestaBLL.ModificarPreguntas(PreguntaEncuestaENT)
            CargarListaPreguntas()
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Modificó la pregunta Nº " & PreguntaEncuestaENT.Id & _
                                                                                    " de la encuesta Nº " & PreguntaEncuestaENT.IdEncuesta))
            LimpiarCampos()
            gvGestionEncuestasPreguntas.EditIndex = -1
            gvGestionEncuestasPreguntas.AutoGenerateColumns = False
            gvGestionEncuestasPreguntas.DataSource = DirectCast(Session("PreguntasEncuesta"), List(Of PreguntaEncuestaENT))
            gvGestionEncuestasPreguntas.DataBind()
            Session("EncuestaEliminada") = False
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnGestionEncuestasEncuesta_ServerClick(sender As Object, e As EventArgs) Handles btnGestionEncuestasEncuesta.ServerClick
        Try
            If txtGestionEncuestasEncuesta.Value <> "" And txtGestionEncuestasFechaVencimiento.Value <> "" Then
                EncuestaV.EncuestaENT.Tema = txtGestionEncuestasEncuesta.Value
                EncuestaV.EncuestaENT.FechaVencimiento = txtGestionEncuestasFechaVencimiento.Value
                EncuestaV.EncuestaBLL.NuevaEncuesta(EncuestaV.EncuestaENT)
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Creó una nueva encuesta"))
                Session("EncuestaEliminada") = False
                Response.Redirect("GestionEncuestas.aspx")
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

    Private Sub btnGestionEncuestasPregunta_ServerClick(sender As Object, e As EventArgs) Handles btnGestionEncuestasPregunta.ServerClick
        Try
            Dim Seleccionado As Boolean = False
            Dim EncuestaSeleccionadaId As Integer = 0
            For i = 0 To gvGestionEncuestasEncuestas.Rows.Count - 1
                If gvGestionEncuestasEncuestas.Rows(i).Style("background-color") = "#449d44" Then
                    EncuestaSeleccionadaId = DirectCast(Session("EncuestaLista"), List(Of EncuestaENT))(gvGestionEncuestasEncuestas.Rows(i).DataItemIndex).Id
                    Seleccionado = True
                    Exit For
                Else
                    Seleccionado = False
                End If
            Next
            If Seleccionado = True Then
                If txtGestionEncuestaNumeroPregunta.Value <> "" And txtGestionEncuestasPregunta.Value <> "" Then
                    Dim PreguntaEncuesta As New PreguntaEncuestaENT
                    PreguntaEncuesta.IdEncuesta = DirectCast(Session("EncuestaElegida"), EncuestaENT).Id
                    'If PreguntaEncuestaV.PreguntaEncuestaBLL.ListarPreguntas(PreguntaEncuesta).Count < 10 Then
                    PreguntaEncuestaV.PreguntaEncuestaENT.IdEncuesta = EncuestaSeleccionadaId
                    PreguntaEncuestaV.PreguntaEncuestaENT.IdPregunta = txtGestionEncuestaNumeroPregunta.Value
                    PreguntaEncuestaV.PreguntaEncuestaENT.Pregunta = txtGestionEncuestasPregunta.Value
                    PreguntaEncuestaV.PreguntaEncuestaBLL.AgregarPreguntas(PreguntaEncuestaV.PreguntaEncuestaENT)
                    LimpiarCampos()
                    CargarListaPreguntas()
                    CargarGridViewPreguntas()
                    BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                            "Cargó una pregunta para la encuesta Nº " & _
                                                                                            PreguntaEncuestaV.PreguntaEncuestaENT.IdEncuesta))
                    LimpiarCampos()
                    Session("EncuestaEliminada") = False
                Else
                    Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                       Traduccion.TraducirMensaje("CamposVacios", DirectCast(Session("Idioma"), CultureInfo)), _
                       Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                End If
            Else
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                          Traduccion.TraducirMensaje("divGestionEncuestasAlerta", DirectCast(Session("Idioma"), CultureInfo)), _
                          Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Sub LimpiarCampos()
        txtGestionEncuestaNumeroPregunta.Value = ""
        txtGestionEncuestasEncuesta.Value = ""
        txtGestionEncuestasPregunta.Value = ""
        txtGestionEncuestasFechaVencimiento.Value = ""
    End Sub

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

    Protected Sub btnEncuestasEliminar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim Id = TryCast(sender, ImageButton).CommandArgument
            Dim ListaEncuesta = CType(Session("EncuestaLista"), List(Of EncuestaENT))
            Dim EncuestaENT As New EncuestaENT
            EncuestaENT.Id = Id
            If EncuestaV.EncuestaBLL.EliminarEncuesta(EncuestaENT) > 0 Then
                For i = 0 To ListaEncuesta.Count - 1
                    If ListaEncuesta.Item(i).Id = Id Then
                        ListaEncuesta.RemoveAt(i)
                        Exit For
                    End If
                Next
                If ListaEncuesta.Count = 0 Then
                    Session("EncuestaLista") = Nothing
                End If
                Session("Fila") = Nothing
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Eliminó la encuesta Nº" & EncuestaENT.Id))
                Session("EncuestaLista") = ListaEncuesta
                Session("EncuestaEliminada") = True
            Else
                Session("EncuestaEliminada") = False
            End If
            Response.Redirect("GestionEncuestas.aspx")
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Protected Sub btnEncuestasPreguntasEliminar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim Id = TryCast(sender, ImageButton).CommandArgument
            Dim ListaPreguntasEncuesta = CType(Session("PreguntasEncuesta"), List(Of PreguntaEncuestaENT))
            Dim PreguntaEncuestaENT As New PreguntaEncuestaENT
            PreguntaEncuestaENT.Id = Id
            If PreguntaEncuestaV.PreguntaEncuestaBLL.EliminarPreguntas(PreguntaEncuestaENT) > 0 Then
                For i = 0 To ListaPreguntasEncuesta.Count - 1
                    If ListaPreguntasEncuesta.Item(i).Id = Id Then
                        Session("EncuestaPregunta") = ListaPreguntasEncuesta.Item(i)
                        ListaPreguntasEncuesta.RemoveAt(i)
                        Exit For
                    End If
                Next
                If ListaPreguntasEncuesta.Count = 0 Then
                    Session("PreguntasEncuesta") = Nothing
                End If
                'Session("Fila") = Nothing
                CargarListaPreguntas()
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Eliminó la pregunta Nº " & PreguntaEncuestaENT.Id & _
                                                                                        " de la encuesta Nº " & _
                                                                                        DirectCast(Session("EncuestaPregunta"), PreguntaEncuestaENT).IdEncuesta))
                Session("EncuestaLista") = ListaEncuesta
                Abrirmodal(Traduccion.TraducirMensaje("Atencion", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("MensajePreguntaEliminada", DirectCast(Session("Idioma"), CultureInfo)), _
                           Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
                CargarListaEncuestas()
                gvGestionEncuestasPreguntas.EditIndex = -1
                gvGestionEncuestasPreguntas.AutoGenerateColumns = False
                gvGestionEncuestasPreguntas.DataSource = DirectCast(Session("PreguntasEncuesta"), List(Of PreguntaEncuestaENT))
                gvGestionEncuestasPreguntas.DataBind()
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

End Class
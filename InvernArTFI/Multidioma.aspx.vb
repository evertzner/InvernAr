Imports BLL
Imports ENTITIES
Imports System.Globalization

Public Class Multidioma
    Inherits System.Web.UI.Page
    Dim MultidiomaV As New MultidiomaVista
    Dim ListaMultidioma As New List(Of MultidiomaENT)
    Dim ListaMultidiomaNuevo As New List(Of MultidiomaENT)
    Dim ListaIdiomasTraducidos As New List(Of MultidiomaENT)
    Dim ListaIdiomasNoTraducidos As New List(Of MultidiomaENT)
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirGrillas(gvMultidiomaNuevo, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirGrillas(gvMultidiomaMultidioma, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divMultidiomaControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divMultidiomaAdministrar.Controls, DirectCast(Session("Idioma"), CultureInfo))
            Traduccion.TraducirControles(divMultidiomaNuevo.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                divMultidiomaAdministrar.Style("display") = ""
                divMultidiomaNuevo.Style("display") = "none"
                Session("ListaMultidioma") = Nothing
                Session("ListaMultidiomaNuevo") = Nothing
                Session("IdiomasTraducidos") = Nothing
                Session("IdiomasNoTraducidos") = Nothing
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
        LlenarListas()
    End Sub

    Private Sub LlenarListas()
        ddlMultidiomaIdiomasTraducidos.Items.Clear()
        For Each item In MultidiomaV.MultidiomaBLL.ListarIdiomas(True)
            ListaIdiomasTraducidos.Add(item)
            ddlMultidiomaIdiomasTraducidos.Items.Add(item.Idioma.DisplayName)
        Next
        Session("IdiomasTraducidos") = ListaIdiomasTraducidos
        ddlMultidiomaIdiomasTraducidos.Items.Insert(0, "")

        ddlMultidiomaIdiomasNoTraducidos.Items.Clear()
        For Each item In MultidiomaV.MultidiomaBLL.ListarIdiomas(False)
            ListaIdiomasNoTraducidos.Add(item)
            ddlMultidiomaIdiomasNoTraducidos.Items.Add(item.Idioma.DisplayName)
        Next
        Session("IdiomasNoTraducidos") = ListaIdiomasNoTraducidos
        ddlMultidiomaIdiomasNoTraducidos.Items.Insert(0, "")
    End Sub

    Private Sub CargarListaMultidioma()
        Dim MultidiomaENT As New MultidiomaENT
        Dim IdiomasTraducidos = CType(Session("IdiomasTraducidos"), List(Of MultidiomaENT))
        Dim Item = ddlMultidiomaIdiomasTraducidos.SelectedIndex - 1
        MultidiomaENT.Idioma = IdiomasTraducidos.Item(Item).Idioma
        Session("ListaMultidioma") = MultidiomaV.MultidiomaBLL.ListarTraduccion(MultidiomaENT)
    End Sub

    Private Sub CargarGridViewMultidioma()
        gvMultidiomaMultidioma.AutoGenerateColumns = False
        ListaMultidioma = DirectCast(Session("ListaMultidioma"), List(Of MultidiomaENT))
        gvMultidiomaMultidioma.DataSource = ListaMultidioma
        gvMultidiomaMultidioma.DataBind()
    End Sub

    Private Sub CargarListaMultidiomaNuevo()
        Dim MultidiomaENT As New MultidiomaENT
        Dim IdiomasNoTraducidos = CType(Session("IdiomasNoTraducidos"), List(Of MultidiomaENT))
        Dim Item = ddlMultidiomaIdiomasNoTraducidos.SelectedIndex + 1
        MultidiomaENT.Idioma = IdiomasNoTraducidos.Item(Item).Idioma
        Session("ListaMultidiomaNuevo") = MultidiomaV.MultidiomaBLL.ListarTraduccion(MultidiomaENT)
    End Sub

    Private Sub CargarGridViewMultidiomaNuevo()
        gvMultidiomaNuevo.AutoGenerateColumns = False
        ListaMultidiomaNuevo = DirectCast(Session("ListaMultidiomaNuevo"), List(Of MultidiomaENT))
        gvMultidiomaNuevo.DataSource = ListaMultidiomaNuevo
        gvMultidiomaNuevo.DataBind()
        For Each row As GridViewRow In gvMultidiomaNuevo.Rows
            If row.Cells(2).Controls.OfType(Of TextBox)().ToList().Count > 0 Then
                row.Cells(2).Controls.OfType(Of TextBox)().FirstOrDefault().Visible = True
            End If
        Next
    End Sub

    Private Sub aMultidiomaAdministrar_ServerClick(sender As Object, e As EventArgs) Handles aMultidiomaAdministrar.ServerClick
        Try
            divMultidiomaAdministrar.Style("display") = ""
            divMultidiomaNuevo.Style("display") = "none"
            Response.Redirect("Multidioma.aspx")
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub aMultidiomaNuevo_ServerClick(sender As Object, e As EventArgs) Handles aMultidiomaNuevo.ServerClick
        Try
            divMultidiomaAdministrar.Style("display") = "none"
            divMultidiomaNuevo.Style("display") = ""
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvMultidiomaMultidioma_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvMultidiomaMultidioma.RowCancelingEdit
        Try
            gvMultidiomaMultidioma.EditIndex = -1
            CargarListaMultidioma()
            CargarGridViewMultidioma()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvMultidiomaMultidioma_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvMultidiomaMultidioma.RowEditing
        Try
            gvMultidiomaMultidioma.EditIndex = e.NewEditIndex
            CargarListaMultidioma()
            CargarGridViewMultidioma()
        Catch ex As Exception
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub gvMultidiomaMultidioma_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvMultidiomaMultidioma.RowUpdating
        Try
            Dim ListaMultidioma = CType(Session("ListaMultidioma"), List(Of MultidiomaENT))
            Dim row = gvMultidiomaMultidioma.Rows(e.RowIndex)
            Dim IdiomasTraducidos = CType(Session("IdiomasTraducidos"), List(Of MultidiomaENT))
            Dim Item = ddlMultidiomaIdiomasTraducidos.SelectedIndex - 1
            With ListaMultidioma.Item(row.DataItemIndex)
                .Idioma = IdiomasTraducidos.Item(Item).Idioma
                .Etiqueta = ListaMultidioma.Item(row.DataItemIndex).Etiqueta
                .Traduccion = (CType((row.Cells(3).Controls(0)), TextBox)).Text
            End With
            Dim MultidiomaENT As New MultidiomaENT
            With MultidiomaENT
                .Idioma = IdiomasTraducidos.Item(Item).Idioma
                .Etiqueta = ListaMultidioma.Item(row.DataItemIndex).Etiqueta
                .Traduccion = ListaMultidioma.Item(row.DataItemIndex).Traduccion
            End With
            MultidiomaV.MultidiomaBLL.ModificarTraduccion(MultidiomaENT)
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                    "Modificó el idioma " & MultidiomaENT.Idioma.Name))

            gvMultidiomaMultidioma.EditIndex = -1
            gvMultidiomaMultidioma.AutoGenerateColumns = False
            gvMultidiomaMultidioma.DataSource = ListaMultidioma
            gvMultidiomaMultidioma.DataBind()
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exActualizarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnMultidiomaConsultarTraduccion_ServerClick(sender As Object, e As EventArgs) Handles btnMultidiomaConsultarTraduccion.ServerClick
        Try
            If ddlMultidiomaIdiomasTraducidos.SelectedIndex > 0 Then
                CargarListaMultidioma()
                CargarGridViewMultidioma()
            Else
                Response.Redirect("Multidioma.aspx")
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exRealizarOperacion", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnMultidiomaEliminar_Click(sender As Object, e As EventArgs) Handles btnMultidiomaEliminar.Click
        Try
            If ddlMultidiomaIdiomasTraducidos.SelectedIndex > 0 Then
                Dim IdiomasTraducidos = CType(Session("IdiomasTraducidos"), List(Of MultidiomaENT))
                Dim Item = ddlMultidiomaIdiomasTraducidos.SelectedIndex - 1
                Dim MultidiomaENT As New MultidiomaENT
                MultidiomaENT.Idioma = IdiomasTraducidos.Item(Item).Idioma
                MultidiomaV.MultidiomaBLL.EliminarTraduccion(MultidiomaENT)
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Eliminó el idioma " & MultidiomaENT.Idioma.Name))
                Response.Redirect("Multidioma.aspx")
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exEliminarRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnMultidiomaConsultarTraduccionNueva_ServerClick(sender As Object, e As EventArgs) Handles btnMultidiomaConsultarTraduccionNueva.ServerClick
        Try
            If ddlMultidiomaIdiomasNoTraducidos.SelectedIndex > 0 Then
                CargarListaMultidiomaNuevo()
                CargarGridViewMultidiomaNuevo()
            Else
                Response.Redirect("Multidioma.aspx")
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCrearRegistro", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub btnMultidiomaAceptarCambios_Click(sender As Object, e As EventArgs) Handles btnMultidiomaAceptarCambios.Click
        Try
            If ddlMultidiomaIdiomasNoTraducidos.SelectedIndex > 0 Then
                Dim ListaMultidiomaNuevo = CType(Session("ListaMultidiomaNuevo"), List(Of MultidiomaENT))
                Dim IdiomasNoTraducidos = CType(Session("IdiomasNoTraducidos"), List(Of MultidiomaENT))
                Dim Item = ddlMultidiomaIdiomasNoTraducidos.SelectedIndex - 1
                For Each row As GridViewRow In gvMultidiomaNuevo.Rows
                    With ListaMultidiomaNuevo.Item(row.DataItemIndex)
                        .Idioma = IdiomasNoTraducidos.Item(Item).Idioma
                        .Etiqueta = ListaMultidiomaNuevo.Item(row.DataItemIndex).Etiqueta
                        .Traduccion = (CType((row.Cells(2).Controls(1)), TextBox)).Text
                    End With
                    Dim MultidiomaENT As New MultidiomaENT
                    With MultidiomaENT
                        .Idioma = IdiomasNoTraducidos.Item(Item).Idioma
                        .Etiqueta = ListaMultidiomaNuevo.Item(row.DataItemIndex).Etiqueta
                        .Traduccion = ListaMultidiomaNuevo.Item(row.DataItemIndex).Traduccion
                    End With
                    Session("Multidioma") = MultidiomaENT
                    MultidiomaV.MultidiomaBLL.AgregarTraduccion(MultidiomaENT)
                Next
                BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos(DirectCast(Session("Usuario"), UsuarioENT).CorreoElectronico, _
                                                                                        "Agregó el idioma " & _
                                                                                        DirectCast(Session("Multidioma"), MultidiomaENT).Idioma.Name))
                gvMultidiomaNuevo.AutoGenerateColumns = False
                gvMultidiomaNuevo.DataSource = ListaMultidiomaNuevo
                gvMultidiomaNuevo.DataBind()
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
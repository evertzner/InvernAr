Imports BLL
Imports ENTITIES
Imports System.Globalization

Public Class Noticias
    Inherits System.Web.UI.Page
    Dim NoticiaV As New NoticiaVista
    Dim BitacoraV As New BitacoraVista

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Traduccion.TraducirControles(divNoticiasControles.Controls, DirectCast(Session("Idioma"), CultureInfo))
            If Not IsPostBack Then
                CargarLista()
                CargarNoticias()
            End If
        Catch ex As Exception
            Abrirmodal(Traduccion.TraducirMensaje("Error", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("exCargarPagina", DirectCast(Session("Idioma"), CultureInfo)), _
                      Traduccion.TraducirMensaje("botonNotificacionAceptar", DirectCast(Session("Idioma"), CultureInfo)))
            BitacoraV.BitacoraBLL.RegistrarBitacora(BitacoraV.BitacoraBLL.Atributos("Excepción", ex.Message))
        End Try
    End Sub

    Private Sub CargarLista()
        Session("NoticiasLista") = NoticiaV.NoticiaBLL.ListarNoticias
    End Sub

    Private Sub CargarNoticias()
        divNoticiasContenedor.InnerHtml = ""
        Dim ListaNoticas As New List(Of NoticiaENT)
        ListaNoticas = DirectCast(Session("NoticiasLista"), List(Of NoticiaENT))
        For Each Noticia In ListaNoticas
            ArmarNoticias(Noticia)
        Next
    End Sub

    Public Function CargarImagen(imgBytes As Byte()) As String
        Dim base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length)
        Return "data:image/jpeg;base64," + base64String
    End Function

    Private Sub ArmarNoticias(Noticia As NoticiaENT)
        Dim divFilaMadre = New HtmlGenericControl("DIV")
        divFilaMadre.Attributes.Add("class", "row")
        Dim divFila = New HtmlGenericControl("DIV")
        divFila.Attributes.Add("class", "row")
        Dim divTitulo = New HtmlGenericControl("DIV")
        divTitulo.Attributes.Add("class", "col-md-6")
        Dim divImagen = New HtmlGenericControl("DIV")
        divImagen.Attributes.Add("class", "col-md-8")
        Dim divContenido = New HtmlGenericControl("DIV")
        divContenido.Attributes.Add("class", "col-md-8")
        Dim Titulo = New Literal
        Titulo.Text = "<strong>" & Noticia.Titulo & "</strong>"
        divFila = New HtmlGenericControl("DIV")
        divFila.Attributes.Add("class", "row")
        divTitulo.Controls.Add(Titulo)
        divFila.Controls.Add(divTitulo)
        divFilaMadre.Controls.Add(divFila)
        If Not Noticia.Imagen Is Nothing Then
            Dim Imagen = New Image()
            Imagen.ImageUrl = CargarImagen(Noticia.Imagen)
            Imagen.CssClass = "img-responsive"
            divFila = New HtmlGenericControl("DIV")
            divFila.Attributes.Add("class", "row")
            divImagen.Controls.Add(Imagen)
            divFila.Controls.Add(divImagen)
            divFilaMadre.Controls.Add(divFila)
        End If
        Dim Contenido = New Literal
        Contenido.Text = Noticia.Contenido & "<br/><hr/>"
        divFila = New HtmlGenericControl("DIV")
        divFila.Attributes.Add("class", "row")
        divContenido.Controls.Add(Contenido)
        divFila.Controls.Add(divContenido)
        divFilaMadre.Controls.Add(divFila)
        divNoticiasContenedor.Controls.Add(divFilaMadre)
    End Sub

    Private Sub Abrirmodal(titulo As String, body As String, boton As String)
        Master.LblModalTitle = titulo
        Master.LblModalBody = body
        Master.btnModalBoton = boton
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "modalMensaje", "$('#modalMensaje').modal();", True)
    End Sub

End Class
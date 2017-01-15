Imports ENTITIES
Imports System.Threading
Imports System.Globalization

Public Class Traduccion
    Inherits System.Web.UI.Page
    'Private Shared ReadOnly vInstancia As New Traduccion
    'Public Shared Function Traduccion() As Traduccion
    '    Return vInstancia
    'End Function
    'Public Sub New()

    'End Sub

    'Public Shared Sub TraducirControles(ByVal Form As form, Optional ByVal DataGridView As DataGridView = Nothing, Optional ByVal MenuToolStrip As MenuStrip = Nothing)
    Public Shared Sub TraducirControles(Controles As ControlCollection, Idioma As CultureInfo)
        Dim ListaEtiquetas As New List(Of MultidiomaENT)
        Dim MultidiomaV As New MultidiomaVista
        'MultidiomaV.MultidiomaENT.Idioma = Thread.CurrentThread.CurrentUICulture
        MultidiomaV.MultidiomaENT.Idioma = Idioma
        ListaEtiquetas.AddRange(MultidiomaV.MultidiomaBLL.ListarTraduccion(MultidiomaV.MultidiomaENT))
        For Each item In ListaEtiquetas
            For Each c As Control In Controles
                If TypeOf (c) Is Button Then
                    If c.ID = item.Etiqueta Then
                        DirectCast(c, Button).Text = item.Traduccion
                    End If
                End If
                If TypeOf (c) Is HtmlGenericControl Then
                    If c.ID = item.Etiqueta Then
                        DirectCast(c, HtmlGenericControl).InnerText = item.Traduccion
                    End If
                End If
                If TypeOf (c) Is HtmlButton Then
                    If c.ID = item.Etiqueta Then
                        DirectCast(c, HtmlButton).InnerText = item.Traduccion
                    End If
                End If
                If TypeOf (c) Is HtmlInputGenericControl Then
                    If c.ID = item.Etiqueta Then
                        DirectCast(c, HtmlInputGenericControl).Attributes.Add("placeholder", item.Traduccion)
                    End If
                End If
                If TypeOf (c) Is HtmlInputText Then
                    If c.ID = item.Etiqueta Then
                        DirectCast(c, HtmlInputText).Attributes.Add("placeholder", item.Traduccion)
                    End If
                End If
                If TypeOf (c) Is HtmlInputFile Then
                    If c.ID = item.Etiqueta Then
                        DirectCast(c, HtmlInputFile).Attributes.Add("placeholder", item.Traduccion)
                    End If
                End If
                If TypeOf (c) Is HtmlInputPassword Then
                    If c.ID = item.Etiqueta Then
                        DirectCast(c, HtmlInputPassword).Attributes.Add("placeholder", item.Traduccion)
                    End If
                End If
                If TypeOf (c) Is HtmlAnchor Then
                    If c.ID = item.Etiqueta Then
                        DirectCast(c, HtmlAnchor).InnerText = item.Traduccion
                    End If
                End If
                If TypeOf (c) Is RegularExpressionValidator Then
                    If c.ID = item.Etiqueta Then
                        DirectCast(c, RegularExpressionValidator).ErrorMessage = item.Traduccion
                    End If
                End If
            Next
        Next
    End Sub

    Public Shared Sub TraducirAlertas(Alerta As HtmlGenericControl, AlertaAux As String, Idioma As CultureInfo)
        Dim ListaEtiquetas As New List(Of MultidiomaENT)
        Dim MultidiomaV As New MultidiomaVista
        MultidiomaV.MultidiomaENT.Idioma = Idioma
        ListaEtiquetas.AddRange(MultidiomaV.MultidiomaBLL.ListarTraduccion(MultidiomaV.MultidiomaENT))
        For Each item In ListaEtiquetas
            If AlertaAux = item.Etiqueta Then
                Alerta.InnerText = item.Traduccion
            End If
        Next
    End Sub

    Public Shared Sub TraducirGrillas(Grilla As WebControls.GridView, Idioma As CultureInfo)
        Dim ListaEtiquetas As New List(Of MultidiomaENT)
        Dim MultidiomaV As New MultidiomaVista
        MultidiomaV.MultidiomaENT.Idioma = Idioma
        ListaEtiquetas.AddRange(MultidiomaV.MultidiomaBLL.ListarTraduccion(MultidiomaV.MultidiomaENT))
        For Each item In ListaEtiquetas
            For Each Columna In Grilla.Columns
                If TypeOf (Columna) Is BoundField Then
                    If Columna.DataField = item.Etiqueta Then
                        Columna.HeaderText = item.Traduccion
                    End If
                End If
            Next
        Next
    End Sub

    Public Shared Function TraducirMensaje(ByVal Codigo As String, Idioma As CultureInfo, Optional ByVal Traducir As Boolean = True) As String
        Dim MultidiomaV As New MultidiomaVista
        Dim ListaEtiquetas As New List(Of MultidiomaENT)

        If Traducir = True Then
            MultidiomaV.MultidiomaENT.Idioma = Idioma
        Else
            MultidiomaV.MultidiomaENT.Idioma = CultureInfo.GetCultureInfo("es")
        End If
        ListaEtiquetas.AddRange(MultidiomaV.MultidiomaBLL.ListarTraduccion(MultidiomaV.MultidiomaENT))
        Dim Traduccion As String = ""
        For Each item In ListaEtiquetas
            If Codigo = item.Etiqueta Then
                Traduccion = item.Traduccion
            End If
        Next
        Return Traduccion
    End Function

End Class

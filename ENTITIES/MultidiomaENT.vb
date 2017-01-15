Imports System.Globalization

Public Class MultidiomaENT

    Private vIdioma As CultureInfo
    Public Property Idioma() As CultureInfo
        Get
            Return vIdioma
        End Get
        Set(ByVal value As CultureInfo)
            vIdioma = value
        End Set
    End Property

    Private vEtiqueta As String
    Public Property Etiqueta() As String
        Get
            Return vEtiqueta
        End Get
        Set(ByVal value As String)
            vEtiqueta = value
        End Set
    End Property

    Private vTraduccion As String
    Public Property Traduccion() As String
        Get
            Return vTraduccion
        End Get
        Set(ByVal value As String)
            vTraduccion = value
        End Set
    End Property

End Class

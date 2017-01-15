Public Class RolENT

    Private vCodigo As String
    Public Property Codigo() As String
        Get
            Return vCodigo
        End Get
        Set(ByVal value As String)
            vCodigo = value
        End Set
    End Property

    Private vNombre As String
    Public Property Nombre() As String
        Get
            Return vNombre
        End Get
        Set(ByVal value As String)
            vNombre = value
        End Set
    End Property

    Private vListaPermisos As New List(Of PermisoENT)
    Public Property ListaPermisos() As List(Of PermisoENT)
        Get
            Return vListaPermisos
        End Get
        Set(ByVal value As List(Of PermisoENT))
            vListaPermisos = value
        End Set
    End Property



End Class

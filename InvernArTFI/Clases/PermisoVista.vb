Imports BLL
Imports ENTITIES

Public Class PermisoVista

    Private vPermisoENT As PermisoENT
    Public Property PermisoENT() As PermisoENT
        Get
            Return vPermisoENT
        End Get
        Set(ByVal value As PermisoENT)
            vPermisoENT = value
        End Set
    End Property

    Private vPermisoBLL As PermisoBLL
    Public Property PermisoBLL() As PermisoBLL
        Get
            Return vPermisoBLL
        End Get
        Set(ByVal value As PermisoBLL)
            vPermisoBLL = value
        End Set
    End Property

    Sub New()
        Me.PermisoENT = New PermisoENT
        Me.PermisoBLL = New PermisoBLL
    End Sub

End Class

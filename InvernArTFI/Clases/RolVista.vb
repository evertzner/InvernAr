Imports BLL
Imports ENTITIES

Public Class RolVista

    Private vRolENT As RolENT
    Public Property RolENT() As RolENT
        Get
            Return vRolENT
        End Get
        Set(ByVal value As RolENT)
            vRolENT = value
        End Set
    End Property

    Private vRolBLL As RolBLL
    Public Property RolBLL() As RolBLL
        Get
            Return vRolBLL
        End Get
        Set(ByVal value As RolBLL)
            vRolBLL = value
        End Set
    End Property

    Sub New()
        Me.RolBLL = New RolBLL
        Me.RolENT = New RolENT
    End Sub
End Class

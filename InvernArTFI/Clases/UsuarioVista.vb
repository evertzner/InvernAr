Imports BLL
Imports ENTITIES

Public Class UsuarioVista

    Private vUsuarioENT As UsuarioENT
    Public Property UsuarioENT() As UsuarioENT
        Get
            Return vUsuarioENT
        End Get
        Set(ByVal value As UsuarioENT)
            vUsuarioENT = value
        End Set
    End Property

    Private vUsuarioBLL As UsuarioBLL
    Public Property UsuarioBLL() As UsuarioBLL
        Get
            Return vUsuarioBLL
        End Get
        Set(ByVal value As UsuarioBLL)
            vUsuarioBLL = value
        End Set
    End Property

    Sub New()
        Me.UsuarioENT = New UsuarioENT
        Me.UsuarioBLL = New UsuarioBLL
    End Sub

End Class

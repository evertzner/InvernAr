Imports ENTITIES
Imports BLL

Public Class InstalacionVista

    Private vInstalacionENT As InstalacionENT
    Public Property InstalacionENT() As InstalacionENT
        Get
            Return vInstalacionENT
        End Get
        Set(ByVal value As InstalacionENT)
            vInstalacionENT = value
        End Set
    End Property

    Private vInstalacionBLL As InstalacionBLL
    Public Property InstalacionBLL() As InstalacionBLL
        Get
            Return vInstalacionBLL
        End Get
        Set(ByVal value As InstalacionBLL)
            vInstalacionBLL = value
        End Set
    End Property

    Sub New()
        Me.InstalacionENT = New InstalacionENT
        Me.InstalacionBLL = New InstalacionBLL
    End Sub

End Class

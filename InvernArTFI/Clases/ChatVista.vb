Imports ENTITIES
Imports BLL

Public Class ChatVista

    Private vChatBLL As ChatBLL
    Public Property ChatBLL() As ChatBLL
        Get
            Return vChatBLL
        End Get
        Set(ByVal value As ChatBLL)
            vChatBLL = value
        End Set
    End Property

    Private vChatENT As ChatENT
    Public Property ChatENT() As ChatENT
        Get
            Return vChatENT
        End Get
        Set(ByVal value As ChatENT)
            vChatENT = value
        End Set
    End Property

    Sub New()
        Me.ChatBLL = New ChatBLL
        Me.ChatENT = New ChatENT
    End Sub

End Class

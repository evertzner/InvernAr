Imports BLL
Imports ENTITIES

Public Class NotaCreditoVista

    Private vNotaCreditoENT As NotaCreditoENT
    Public Property NotaCreditoENT() As NotaCreditoENT
        Get
            Return vNotaCreditoENT
        End Get
        Set(ByVal value As NotaCreditoENT)
            vNotaCreditoENT = value
        End Set
    End Property

    Private vNotaCreditoBLL As NotaCreditoBLL
    Public Property NotaCreditoBLL() As NotaCreditoBLL
        Get
            Return vNotaCreditoBLL
        End Get
        Set(ByVal value As NotaCreditoBLL)
            vNotaCreditoBLL = value
        End Set
    End Property

    Sub New()
        Me.NotaCreditoENT = New NotaCreditoENT
        Me.NotaCreditoBLL = New NotaCreditoBLL
    End Sub

End Class

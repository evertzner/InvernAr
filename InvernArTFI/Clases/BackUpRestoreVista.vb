Imports ENTITIES
Imports BLL

Public Class BackUpRestoreVista

    Private vBackUpRestoreBLL As BackUpRestoreBLL
    Public Property BackUpRestoreBLL() As BackUpRestoreBLL
        Get
            Return vBackUpRestoreBLL
        End Get
        Set(ByVal value As BackUpRestoreBLL)
            vBackUpRestoreBLL = value
        End Set
    End Property

    Private vBackUpRestoreENT As BackUpRestoreENT
    Public Property BackUpRestoreENT() As BackUpRestoreENT
        Get
            Return vBackUpRestoreENT
        End Get
        Set(ByVal value As BackUpRestoreENT)
            vBackUpRestoreENT = value
        End Set
    End Property

    Sub New()
        Me.BackUpRestoreBLL = New BackUpRestoreBLL
        Me.BackUpRestoreENT = New BackUpRestoreENT
    End Sub

End Class

Imports BLL
Imports ENTITIES

Public Class MultidiomaVista

    Private vMultidiomaENT As MultidiomaENT
    Public Property MultidiomaENT() As MultidiomaENT
        Get
            Return vMultidiomaENT
        End Get
        Set(ByVal value As MultidiomaENT)
            vMultidiomaENT = value
        End Set
    End Property

    Private vMultidiomaBLL As MultidiomaBLL
    Public Property MultidiomaBLL() As MultidiomaBLL
        Get
            Return vMultidiomaBLL
        End Get
        Set(ByVal value As MultidiomaBLL)
            vMultidiomaBLL = value
        End Set
    End Property

    Sub New()
        Me.MultidiomaENT = New MultidiomaENT
        Me.MultidiomaBLL = New MultidiomaBLL
    End Sub

End Class

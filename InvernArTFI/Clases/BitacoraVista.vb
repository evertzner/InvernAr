Imports ENTITIES
Imports BLL

Public Class BitacoraVista

    Private vBitacoraENT As BitacoraENT
    Public Property BitacoraENT() As BitacoraENT
        Get
            Return vBitacoraENT
        End Get
        Set(ByVal value As BitacoraENT)
            vBitacoraENT = value
        End Set
    End Property

    Private vBitacoraBLL As BitacoraBLL
    Public Property BitacoraBLL() As BitacoraBLL
        Get
            Return vBitacoraBLL
        End Get
        Set(ByVal value As BitacoraBLL)
            vBitacoraBLL = value
        End Set
    End Property

    Sub New()
        Me.BitacoraBLL = New BitacoraBLL
        Me.BitacoraENT = New BitacoraENT
    End Sub

End Class

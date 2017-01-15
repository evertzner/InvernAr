Public Class ChatCantidadENT

    Private vIdUsuario As Integer
    Public Property IdUsuario() As Integer
        Get
            Return vIdUsuario
        End Get
        Set(ByVal value As Integer)
            vIdUsuario = value
        End Set
    End Property

    Private vCorreoElectronico As String
    Public Property CorreoElectronico() As String
        Get
            Return vCorreoElectronico
        End Get
        Set(ByVal value As String)
            vCorreoElectronico = value
        End Set
    End Property

    Private vNoLeido As Integer
    Public Property NoLeido() As Integer
        Get
            Return vNoLeido
        End Get
        Set(ByVal value As Integer)
            vNoLeido = value
        End Set
    End Property

End Class

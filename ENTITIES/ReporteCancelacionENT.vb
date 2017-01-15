Public Class ReporteCancelacionENT

    Private vMotivo As String
    Public Property Motivo() As String
        Get
            Return vMotivo
        End Get
        Set(ByVal value As String)
            vMotivo = value
        End Set
    End Property

    Private vCuenta As Integer
    Public Property Cuenta() As Integer
        Get
            Return vCuenta
        End Get
        Set(ByVal value As Integer)
            vCuenta = value
        End Set
    End Property

End Class

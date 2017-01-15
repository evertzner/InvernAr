Public Class NotaCreditoENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vIdCliente As Integer
    Public Property IdCliente() As Integer
        Get
            Return vIdCliente
        End Get
        Set(ByVal value As Integer)
            vIdCliente = value
        End Set
    End Property

    Private vFecha As DateTime
    Public Property Fecha() As DateTime
        Get
            Return vFecha
        End Get
        Set(ByVal value As DateTime)
            vFecha = value
        End Set
    End Property

    Private vSaldo As Double
    Public Property Saldo() As Double
        Get
            Return vSaldo
        End Get
        Set(ByVal value As Double)
            vSaldo = value
        End Set
    End Property

    Private vMotivo As String
    Public Property Motivo() As String
        Get
            Return vMotivo
        End Get
        Set(ByVal value As String)
            vMotivo = value
        End Set
    End Property

End Class

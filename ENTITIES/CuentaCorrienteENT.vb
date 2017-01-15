Public Class CuentaCorrienteENT

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

    Private vIdFactura As Integer
    Public Property IdFactura() As Integer
        Get
            Return vIdFactura
        End Get
        Set(ByVal value As Integer)
            vIdFactura = value
        End Set
    End Property

    Private vIdNotaCredito As Integer
    Public Property IdNotaCredito() As Integer
        Get
            Return vIdNotaCredito
        End Get
        Set(ByVal value As Integer)
            vIdNotaCredito = value
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

    Private vDebito As Double
    Public Property Debito() As Double
        Get
            Return vDebito
        End Get
        Set(ByVal value As Double)
            vDebito = value
        End Set
    End Property

    Private vCredito As Double
    Public Property Credito() As Double
        Get
            Return vCredito
        End Get
        Set(ByVal value As Double)
            vCredito = value
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

End Class

Public Class FacturaENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
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

    Private vIdCliente As Integer
    Public Property IdCliente() As Integer
        Get
            Return vIdCliente
        End Get
        Set(ByVal value As Integer)
            vIdCliente = value
        End Set
    End Property

    Private vTotal As Double
    Public Property Total() As Double
        Get
            Return vTotal
        End Get
        Set(ByVal value As Double)
            vTotal = value
        End Set
    End Property

    Private vCancelada As Boolean
    Public Property Cancelada() As Boolean
        Get
            Return vCancelada
        End Get
        Set(ByVal value As Boolean)
            vCancelada = value
        End Set
    End Property

End Class

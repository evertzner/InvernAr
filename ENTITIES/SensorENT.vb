Public Class SensorENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vIdInstalacion As Integer
    Public Property IdInstalacion() As Integer
        Get
            Return vIdInstalacion
        End Get
        Set(ByVal value As Integer)
            vIdInstalacion = value
        End Set
    End Property

    Private vIdProducto As Integer
    Public Property IdProducto() As Integer
        Get
            Return vIdProducto
        End Get
        Set(ByVal value As Integer)
            vIdProducto = value
        End Set
    End Property

    Private vNombre As String
    Public Property Nombre() As String
        Get
            Return vNombre
        End Get
        Set(ByVal value As String)
            vNombre = value
        End Set
    End Property

    Private vValorSensado As Double
    Public Property ValorSensado() As Double
        Get
            Return vValorSensado
        End Get
        Set(ByVal value As Double)
            vValorSensado = value
        End Set
    End Property

    Private vEstado As String
    Public Property Estado() As String
        Get
            Return vEstado
        End Get
        Set(ByVal value As String)
            vEstado = value
        End Set
    End Property

    Private vLimiteMinimoAlerta As Double
    Public Property LimiteMinimoAlerta() As Double
        Get
            Return vLimiteMinimoAlerta
        End Get
        Set(ByVal value As Double)
            vLimiteMinimoAlerta = value
        End Set
    End Property

    Private vLimiteMaximoAlerta As Double
    Public Property LimiteMaximoAlerta() As Double
        Get
            Return vLimiteMaximoAlerta
        End Get
        Set(ByVal value As Double)
            vLimiteMaximoAlerta = value
        End Set
    End Property

End Class

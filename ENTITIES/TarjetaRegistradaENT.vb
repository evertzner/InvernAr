Public Class TarjetaRegistradaENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
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

    Private vNumero As String
    Public Property Numero() As String
        Get
            Return vNumero
        End Get
        Set(ByVal value As String)
            vNumero = value
        End Set
    End Property

    Private vCodigoSeguridad As String
    Public Property CodigoSeguridad() As String
        Get
            Return vCodigoSeguridad
        End Get
        Set(ByVal value As String)
            vCodigoSeguridad = value
        End Set
    End Property

    Private vMesVencimiento As String
    Public Property MesVencimiento() As String
        Get
            Return vMesVencimiento
        End Get
        Set(ByVal value As String)
            vMesVencimiento = value
        End Set
    End Property

    Private vAnoVencimiento As String
    Public Property AnoVencimiento() As String
        Get
            Return vAnoVencimiento
        End Get
        Set(ByVal value As String)
            vAnoVencimiento = value
        End Set
    End Property

    Private vIdTarjeta As Integer
    Public Property IdTarjeta() As Integer
        Get
            Return vIdTarjeta
        End Get
        Set(ByVal value As Integer)
            vIdTarjeta = value
        End Set
    End Property

    Private vBaja As Boolean
    Public Property Baja() As Boolean
        Get
            Return vBaja
        End Get
        Set(ByVal value As Boolean)
            vBaja = value
        End Set
    End Property

End Class

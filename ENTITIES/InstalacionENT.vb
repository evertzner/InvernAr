Public Class InstalacionENT

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

    Private vFechaDeSolicitud As DateTime
    Public Property FechaDeSolicitud() As DateTime
        Get
            Return vFechaDeSolicitud
        End Get
        Set(ByVal value As DateTime)
            vFechaDeSolicitud = value
        End Set
    End Property

    Private vDatosDeContacto As String
    Public Property DatosDeContacto() As String
        Get
            Return vDatosDeContacto
        End Get
        Set(ByVal value As String)
            vDatosDeContacto = value
        End Set
    End Property

    Private vDomicilioDeInstalacion As String
    Public Property DomicilioDeInstalacion() As String
        Get
            Return vDomicilioDeInstalacion
        End Get
        Set(ByVal value As String)
            vDomicilioDeInstalacion = value
        End Set
    End Property

    Private vObservaciones As String
    Public Property Observaciones() As String
        Get
            Return vObservaciones
        End Get
        Set(ByVal value As String)
            vObservaciones = value
        End Set
    End Property

    Private vFechaDeRealizacion As DateTime
    Public Property FechaDeRealizacion() As DateTime
        Get
            Return vFechaDeRealizacion
        End Get
        Set(ByVal value As DateTime)
            vFechaDeRealizacion = value
        End Set
    End Property

    Private vRealizado As Boolean
    Public Property Realizado() As Boolean
        Get
            Return vRealizado
        End Get
        Set(ByVal value As Boolean)
            vRealizado = value
        End Set
    End Property

End Class

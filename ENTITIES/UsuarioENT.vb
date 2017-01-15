Public Class UsuarioENT

    Private vID As Integer
    Public Property ID() As Integer
        Get
            Return vID
        End Get
        Set(ByVal value As Integer)
            vID = value
        End Set
    End Property

    Private vDNI As Integer
    Public Property DNI() As Integer
        Get
            Return vDNI
        End Get
        Set(ByVal value As Integer)
            vDNI = value
        End Set
    End Property

    Private vCUIT As String
    Public Property CUIT() As String
        Get
            Return vCUIT
        End Get
        Set(ByVal value As String)
            vCUIT = value
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

    Private vApellido As String
    Public Property Apellido() As String
        Get
            Return vApellido
        End Get
        Set(ByVal value As String)
            vApellido = value
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

    Private vDomicilio As String
    Public Property Domicilio() As String
        Get
            Return vDomicilio
        End Get
        Set(ByVal value As String)
            vDomicilio = value
        End Set
    End Property

    Private vLocalidad As String
    Public Property Localidad() As String
        Get
            Return vLocalidad
        End Get
        Set(ByVal value As String)
            vLocalidad = value
        End Set
    End Property

    Private vProvincia As String
    Public Property Provincia() As String
        Get
            Return vProvincia
        End Get
        Set(ByVal value As String)
            vProvincia = value
        End Set
    End Property

    Private vTelefono As String
    Public Property Telefono() As String
        Get
            Return vTelefono
        End Get
        Set(ByVal value As String)
            vTelefono = value
        End Set
    End Property

    Private vInterno As String
    Public Property Interno() As String
        Get
            Return vInterno
        End Get
        Set(ByVal value As String)
            vInterno = value
        End Set
    End Property

    Private vTelefonoCelular As String
    Public Property TelefonoCelular() As String
        Get
            Return vTelefonoCelular
        End Get
        Set(ByVal value As String)
            vTelefonoCelular = value
        End Set
    End Property

    Private vContraseña As String
    Public Property Contraseña() As String
        Get
            Return vContraseña
        End Get
        Set(ByVal value As String)
            vContraseña = value
        End Set
    End Property

    Private vIntentosFallidos As Integer
    Public Property IntentosFallidos() As Integer
        Get
            Return vIntentosFallidos
        End Get
        Set(ByVal value As Integer)
            vIntentosFallidos = value
        End Set
    End Property

    Private vBloqueado As Boolean
    Public Property Bloqueado() As Boolean
        Get
            Return vBloqueado
        End Get
        Set(ByVal value As Boolean)
            vBloqueado = value
        End Set
    End Property

    Private vValidado As Boolean
    Public Property Validado() As Boolean
        Get
            Return vValidado
        End Get
        Set(ByVal value As Boolean)
            vValidado = value
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

    Private vListaRoles As New List(Of RolENT)
    Public Property ListaRoles() As List(Of RolENT)
        Get
            Return vListaRoles
        End Get
        Set(ByVal value As List(Of RolENT))
            vListaRoles = value
        End Set
    End Property

End Class

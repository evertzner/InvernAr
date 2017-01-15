Public Class BitacoraENT
    Private vID As Integer
    Public Property ID() As Integer
        Get
            Return vID
        End Get
        Set(ByVal value As Integer)
            vID = value
        End Set
    End Property

    Private vFechaActividad As DateTime
    Public Property FechaActividad() As DateTime
        Get
            Return vFechaActividad
        End Get
        Set(ByVal value As DateTime)
            vFechaActividad = value
        End Set
    End Property

    Private vUsuario As String
    Public Property Usuario() As String
        Get
            Return vUsuario
        End Get
        Set(ByVal value As String)
            vUsuario = value
        End Set
    End Property

    Private vActividad As String
    Public Property Actividad() As String
        Get
            Return vActividad
        End Get
        Set(ByVal value As String)
            vActividad = value
        End Set
    End Property
End Class

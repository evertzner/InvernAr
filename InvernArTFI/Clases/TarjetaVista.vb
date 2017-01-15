Imports BLL
Imports ENTITIES

Public Class TarjetaVista

    Private vAnoENT As AnoENT
    Public Property AnoENT() As AnoENT
        Get
            Return vAnoENT
        End Get
        Set(ByVal value As AnoENT)
            vAnoENT = value
        End Set
    End Property

    Private vMesENT As MesENT
    Public Property MesENT() As MesENT
        Get
            Return vMesENT
        End Get
        Set(ByVal value As MesENT)
            vMesENT = value
        End Set
    End Property

    Private vTarjetaENT As TarjetaENT
    Public Property TarjetaENT() As TarjetaENT
        Get
            Return vTarjetaENT
        End Get
        Set(ByVal value As TarjetaENT)
            vTarjetaENT = value
        End Set
    End Property

    Private vTarjetaRegistradaENT As TarjetaRegistradaENT
    Public Property TarjetaRegistradaENT() As TarjetaRegistradaENT
        Get
            Return vTarjetaRegistradaENT
        End Get
        Set(ByVal value As TarjetaRegistradaENT)
            vTarjetaRegistradaENT = value
        End Set
    End Property

    Private vTarjetaBLL As TarjetaBLL
    Public Property TarjetaBLL() As TarjetaBLL
        Get
            Return vTarjetaBLL
        End Get
        Set(ByVal value As TarjetaBLL)
            vTarjetaBLL = value
        End Set
    End Property

    Sub New()
        Me.AnoENT = New AnoENT
        Me.MesENT = New MesENT
        Me.TarjetaENT = New TarjetaENT
        Me.TarjetaBLL = New TarjetaBLL
        Me.TarjetaRegistradaENT = New TarjetaRegistradaENT
    End Sub

End Class

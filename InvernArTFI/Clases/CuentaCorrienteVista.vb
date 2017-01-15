Imports ENTITIES
Imports BLL

Public Class CuentaCorrienteVista

    Private vCuentaCorrienteENT As CuentaCorrienteENT
    Public Property CuentaCorrienteENT() As CuentaCorrienteENT
        Get
            Return vCuentaCorrienteENT
        End Get
        Set(ByVal value As CuentaCorrienteENT)
            vCuentaCorrienteENT = value
        End Set
    End Property

    Private vCuentaCorrienteBLL As CuentaCorrienteBLL
    Public Property CuentaCorrienteBLL() As CuentaCorrienteBLL
        Get
            Return vCuentaCorrienteBLL
        End Get
        Set(ByVal value As CuentaCorrienteBLL)
            vCuentaCorrienteBLL = value
        End Set
    End Property

    Sub New()
        Me.CuentaCorrienteENT = New CuentaCorrienteENT
        Me.CuentaCorrienteBLL = New CuentaCorrienteBLL
    End Sub

End Class

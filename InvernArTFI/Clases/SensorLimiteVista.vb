Imports ENTITIES
Imports BLL

Public Class SensorLimiteVista

    Private vSensorLimiteENT As SensorLimiteENT
    Public Property SensorLimiteENT() As SensorLimiteENT
        Get
            Return vSensorLimiteENT
        End Get
        Set(ByVal value As SensorLimiteENT)
            vSensorLimiteENT = value
        End Set
    End Property

    Private vSensorLimiteBLL As SensorLimiteBLL
    Public Property SensorLimiteBLL() As SensorLimiteBLL
        Get
            Return vSensorLimiteBLL
        End Get
        Set(ByVal value As SensorLimiteBLL)
            vSensorLimiteBLL = value
        End Set
    End Property

    Sub New()
        Me.SensorLimiteENT = New SensorLimiteENT
        Me.SensorLimiteBLL = New SensorLimiteBLL
    End Sub

End Class

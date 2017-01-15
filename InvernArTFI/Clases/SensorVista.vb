Imports ENTITIES
Imports BLL

Public Class SensorVista

    Private vSensorENT As SensorENT
    Public Property SensorENT() As SensorENT
        Get
            Return vSensorENT
        End Get
        Set(ByVal value As SensorENT)
            vSensorENT = value
        End Set
    End Property

    Private vSensorBLL As SensorBLL
    Public Property SensorBLL() As SensorBLL
        Get
            Return vSensorBLL
        End Get
        Set(ByVal value As SensorBLL)
            vSensorBLL = value
        End Set
    End Property

    Sub New()
        Me.SensorENT = New SensorENT
        Me.SensorBLL = New SensorBLL
    End Sub

End Class

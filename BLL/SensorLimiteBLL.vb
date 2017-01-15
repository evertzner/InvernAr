Imports ENTITIES
Imports MAPPER

Public Class SensorLimiteBLL
    Dim SensorLimiteENT As SensorLimiteENT

    Public Sub ActualizarSensorLimite(ByRef QueObjeto As Object)
        SensorLimiteMAP.ActualizarSensorLimite(QueObjeto)
    End Sub

    Public Sub AgregarSensorLimite(ByRef QueObjeto As Object)
        SensorLimiteMAP.AgregarSensorLimite(QueObjeto)
    End Sub

    Public Sub EliminarSensorLimite(ByRef QueObjeto As Object)
        SensorLimiteMAP.EliminarSensorLimite(QueObjeto)
    End Sub

    Public Function ListarSensoresLimites() As List(Of SensorLimiteENT)
        Dim ListaSensoresLimite As New List(Of SensorLimiteENT)
        Dim lector As IDataReader = SensorLimiteMAP.ListarSensoresLimites.CreateDataReader
        Do While lector.Read()
            SensorLimiteENT = New SensorLimiteENT
            With SensorLimiteENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdProducto = Convert.ToInt32(lector("IdProducto"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .LimiteMaximoAlerta = Convert.ToDouble(lector("LimiteMaximoAlerta"))
                .LimiteMinimoAlerta = Convert.ToDouble(lector("LimiteMinimoAlerta"))
            End With
            ListaSensoresLimite.Add(SensorLimiteENT)
        Loop
        lector.Close()
        Return ListaSensoresLimite
    End Function

End Class

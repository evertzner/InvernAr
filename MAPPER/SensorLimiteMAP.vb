Imports ENTITIES
Imports DAL

Public Class SensorLimiteMAP

    Shared Sub ActualizarSensorLimite(ByRef QueObjeto As Object)
        Dim SensorLimite As New SensorLimiteENT
        SensorLimite = DirectCast(QueObjeto, SensorLimiteENT)
        Dim HT As New Hashtable
        HT.Add("@pId", SensorLimite.Id)
        HT.Add("@pLimiteMinimoAlerta", SensorLimite.LimiteMinimoAlerta)
        HT.Add("@pLimiteMaximoAlerta", SensorLimite.LimiteMaximoAlerta)
        Generico.Escribir("pActualizarSensorLimite", HT)
    End Sub

    Shared Sub AgregarSensorLimite(ByRef QueObjeto As Object)
        Dim SensorLimite As New SensorLimiteENT
        SensorLimite = DirectCast(QueObjeto, SensorLimiteENT)
        Dim HT As New Hashtable
        HT.Add("@pIdProducto", SensorLimite.IdProducto)
        Generico.Escribir("pAgregarSensorLimite", HT)
    End Sub

    Shared Sub EliminarSensorLimite(ByRef QueObjeto As Object)
        Dim SensorLimite As New SensorLimiteENT
        SensorLimite = DirectCast(QueObjeto, SensorLimiteENT)
        Dim HT As New Hashtable
        HT.Add("@pIdProducto", SensorLimite.IdProducto)
        Generico.Escribir("pEliminarSensorLimite", HT)
    End Sub

    Shared Function ListarSensoresLimites() As DataSet
        Return Generico.Leer("pListarSensoresLimites")
    End Function

End Class

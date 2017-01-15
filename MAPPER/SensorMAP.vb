Imports ENTITIES
Imports DAL

Public Class SensorMAP

    Shared Sub ActualizarSensores(ByRef QueObjeto As Object)
        Dim Sensor As New SensorENT
        Sensor = DirectCast(QueObjeto, SensorENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Sensor.Id)
        HT.Add("@pValorSensado", Sensor.ValorSensado)
        HT.Add("@pEstado", Sensor.Estado)
        Generico.Escribir("pActualizarSensores", HT)
    End Sub

    Shared Sub AgregarSensores(ByRef QueObjeto As Object)
        Dim Sensor As New SensorENT
        Sensor = DirectCast(QueObjeto, SensorENT)
        Dim HT As New Hashtable
        HT.Add("@pIdInstalacion", Sensor.IdInstalacion)
        HT.Add("@pIdProducto", Sensor.IdProducto)
        Generico.Escribir("pAgregarSensores", HT)
    End Sub

    Shared Sub EliminarSensores(ByRef QueObjeto As Object)
        Dim Sensor As New SensorENT
        Sensor = DirectCast(QueObjeto, SensorENT)
        Dim HT As New Hashtable
        HT.Add("@pIdInstalacion", Sensor.IdInstalacion)
        HT.Add("@pIdProducto", Sensor.IdProducto)
        Generico.Escribir("pEliminarSensores", HT)
    End Sub

    Shared Function ListarSensores(ByRef QueObjeto As Object) As DataSet
        Dim Usuario As New UsuarioENT
        Usuario = DirectCast(QueObjeto, UsuarioENT)
        Dim HT As New Hashtable
        HT.Add("@pIdCliente", Usuario.ID)
        Return Generico.Leer("pListarSensores", HT)
    End Function

End Class
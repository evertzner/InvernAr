Imports ENTITIES
Imports MAPPER

Public Class SensorBLL
    Dim SensorENT As SensorENT

    Public Sub ActualizarSensores(ByRef QueObjeto As Object)
        SensorMAP.ActualizarSensores(QueObjeto)
    End Sub

    Public Sub AgregarSensores(ByRef QueObjeto As Object)
        SensorMAP.AgregarSensores(QueObjeto)
    End Sub

    Public Sub EliminarSensores(ByRef QueObjeto As Object)
        SensorMAP.EliminarSensores(QueObjeto)
    End Sub

    Public Function ListarSensores(ByRef QueObjeto As Object) As List(Of SensorENT)
        Dim ListaSensores As New List(Of SensorENT)
        Dim lector As IDataReader = SensorMAP.ListarSensores(QueObjeto).CreateDataReader
        Do While lector.Read()
            SensorENT = New SensorENT
            With SensorENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdInstalacion = Convert.ToInt32(lector("IdInstalacion"))
                .IdProducto = Convert.ToInt32(lector("IdProducto"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .ValorSensado = Convert.ToDouble(lector("ValorSensado"))
                .Estado = Convert.ToString(lector("Estado"))
                .LimiteMinimoAlerta = Convert.ToDouble(lector("LimiteMinimoAlerta"))
                .LimiteMaximoAlerta = Convert.ToDouble(lector("LimiteMaximoAlerta"))
            End With
            ListaSensores.Add(SensorENT)
        Loop
        lector.Close()
        Return ListaSensores
    End Function

End Class

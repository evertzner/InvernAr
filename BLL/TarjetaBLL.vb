Imports ENTITIES
Imports MAPPER

Public Class TarjetaBLL

    Public Function ListarAnos() As List(Of AnoENT)
        Dim ListaAnos As New List(Of AnoENT)
        Dim lector As IDataReader = TarjetaMAP.ListarAnos.CreateDataReader
        Do While lector.Read()
            Dim AnoENT As New AnoENT
            With AnoENT
                .Id = Convert.ToInt32(lector("Id"))
                .Ano = Convert.ToInt32(lector("Ano"))
            End With
            ListaAnos.Add(AnoENT)
        Loop
        lector.Close()
        Return ListaAnos
    End Function

    Public Function ListarMeses() As List(Of MesENT)
        Dim ListaMeses As New List(Of MesENT)
        Dim lector As IDataReader = TarjetaMAP.ListarMeses.CreateDataReader
        Do While lector.Read()
            Dim MesENT As New MesENT
            With MesENT
                .Id = Convert.ToInt32(lector("Id"))
                .Mes = Convert.ToString(lector("Mes"))
            End With
            ListaMeses.Add(MesENT)
        Loop
        lector.Close()
        Return ListaMeses
    End Function

    Public Function ListarTarjetas() As List(Of TarjetaENT)
        Dim ListaTarjetas As New List(Of TarjetaENT)
        Dim lector As IDataReader = TarjetaMAP.ListarTarjetas.CreateDataReader
        Do While lector.Read()
            Dim TarjetaENT As New TarjetaENT
            With TarjetaENT
                .Id = Convert.ToInt32(lector("Id"))
                .Tarjeta = Convert.ToString(lector("Tarjeta"))
            End With
            ListaTarjetas.Add(TarjetaENT)
        Loop
        lector.Close()
        Return ListaTarjetas
    End Function

    Public Function ValidarTarjeta() As List(Of TarjetaRegistradaENT)
        Dim ListaTarjetasRegistradas As New List(Of TarjetaRegistradaENT)
        Dim lector As IDataReader = TarjetaMAP.ValidarTarjeta.CreateDataReader
        Do While lector.Read()
            Dim TarjetaRegistradaENT As New TarjetaRegistradaENT
            With TarjetaRegistradaENT
                .Id = Convert.ToInt32(lector("Id"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .Numero = Convert.ToString(lector("Numero"))
                .CodigoSeguridad = Convert.ToString(lector("CodigoSeguridad"))
                .MesVencimiento = Convert.ToString(lector("MesVencimiento"))
                .AnoVencimiento = Convert.ToString(lector("AnoVencimiento"))
                .IdTarjeta = Convert.ToInt32(lector("IdTarjeta"))
                .Baja = Convert.ToBoolean(lector("Baja"))
            End With
            ListaTarjetasRegistradas.Add(TarjetaRegistradaENT)
        Loop
        lector.Close()
        Return ListaTarjetasRegistradas
    End Function

End Class

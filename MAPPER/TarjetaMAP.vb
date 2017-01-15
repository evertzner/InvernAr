Imports ENTITIES
Imports DAL

Public Class TarjetaMAP

    Shared Function ListarAnos() As DataSet
        Return Generico.Leer("pListarAnos")
    End Function

    Shared Function ListarMeses() As DataSet
        Return Generico.Leer("pListarMeses")
    End Function

    Shared Function ListarTarjetas() As DataSet
        Return Generico.Leer("pListarTarjetas")
    End Function

    Shared Function ValidarTarjeta() As DataSet
        Return Generico.Leer("pValidarTarjeta")
    End Function

End Class

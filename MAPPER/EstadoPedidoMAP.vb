Imports ENTITIES
Imports DAL

Public Class EstadoPedidoMAP

    Shared Function ListarEstadoPedido() As DataSet
        Return Generico.Leer("pListarEstadoPedido")
    End Function

End Class

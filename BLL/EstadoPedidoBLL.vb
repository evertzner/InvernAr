Imports ENTITIES
Imports MAPPER

Public Class EstadoPedidoBLL
    Dim EstadoPedidoENT As EstadoPedidoENT

    Public Function ListarEstadoPedido() As List(Of EstadoPedidoENT)
        Dim ListaEstadoPedido As New List(Of EstadoPedidoENT)
        Dim lector As IDataReader = EstadoPedidoMAP.ListarEstadoPedido.CreateDataReader
        Do While lector.Read()
            EstadoPedidoENT = New EstadoPedidoENT
            With EstadoPedidoENT
                .Estado = Convert.ToString(lector("Estado"))
            End With
            ListaEstadoPedido.Add(EstadoPedidoENT)
        Loop
        lector.Close()
        Return ListaEstadoPedido
    End Function

End Class

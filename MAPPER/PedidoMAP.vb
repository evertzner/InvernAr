Imports ENTITIES
Imports DAL

Public Class PedidoMAP

    Shared Sub ModificarPedido(ByRef QueObjeto As Object)
        Dim Pedido As New PedidoENT
        Pedido = DirectCast(QueObjeto, PedidoENT)
        Dim HT As New Hashtable
        HT.Add("@pIdFactura", Pedido.IdFactura)
        HT.Add("@pEstado", Pedido.Estado)
        HT.Add("@pFecha", Pedido.Fecha)
        Generico.Escribir("pModificarPedido", HT)
    End Sub

    Shared Function ListarPedidos(ByRef QueObjeto As Object, Omitir As Boolean) As DataSet
        Dim Pedido As New PedidoENT
        Pedido = DirectCast(QueObjeto, PedidoENT)
        Dim HT As New Hashtable
        HT.Add("@pIdFactura", Pedido.IdFactura)
        HT.Add("@pOmitir", Omitir)
        Return Generico.Leer("pListarPedidos", HT)
    End Function

    Shared Function CancelarPedido(IdFactura As Integer, Fecha As DateTime, Motivo As String) As DataSet
        Dim HT As New Hashtable
        HT.Add("@pIdFactura", IdFactura)
        HT.Add("@pFecha", Fecha)
        HT.Add("@pMotivo", Motivo)
        Return Generico.Leer("pCancelarPedido", HT)
    End Function

End Class

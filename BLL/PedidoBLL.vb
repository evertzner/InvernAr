Imports ENTITIES
Imports MAPPER

Public Class PedidoBLL
    Dim PedidoENT As PedidoENT

    Public Function ListarPedidos(ByRef QueObjeto As Object, Omitir As Boolean) As List(Of PedidoENT)
        Dim ListaPedidos As New List(Of PedidoENT)
        Dim lector As IDataReader = PedidoMAP.ListarPedidos(QueObjeto, Omitir).CreateDataReader
        Do While lector.Read()
            PedidoENT = New PedidoENT
            With PedidoENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdFactura = Convert.ToInt32(lector("IdFactura"))
                .Estado = Convert.ToString(lector("Estado"))
                .Fecha = Convert.ToDateTime(lector("Fecha"))
            End With
            ListaPedidos.Add(PedidoENT)
        Loop
        lector.Close()
        Return ListaPedidos
    End Function

    Public Sub ModificarPedido(ByRef QueObjeto As Object)
        PedidoMAP.ModificarPedido(QueObjeto)
    End Sub

    Public Function CancelarPedido(IdFactura As Integer, Fecha As DateTime, Motivo As String) As List(Of String)
        Dim ListaDatos As New List(Of String)
        Dim lector As IDataReader = PedidoMAP.CancelarPedido(IdFactura, Fecha, Motivo).CreateDataReader
        Do While lector.Read()
            Dim Dato As String
            Dato = Convert.ToInt32(lector("IdNotaCredito"))
            ListaDatos.Add(Dato)
            Dato = Convert.ToInt32(lector("IdCliente"))
            ListaDatos.Add(Dato)
            Dato = Convert.ToDouble(lector("Total"))
            ListaDatos.Add(Dato)
            Dato = Convert.ToString(lector("CorreoElectronico"))
            ListaDatos.Add(Dato)
        Loop
        lector.Close()
        Return ListaDatos
    End Function

End Class

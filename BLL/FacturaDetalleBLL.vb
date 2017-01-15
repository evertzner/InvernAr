Imports MAPPER
Imports ENTITIES

Public Class FacturaDetalleBLL

    Public Sub ComprarDetalleFactura(ByRef QueObjeto As Object)
        FacturaDetalleMAP.ComprarDetalleFactura(QueObjeto)
    End Sub

    Public Function ListarFacturasDetalle(ByRef QueObjeto As Object) As List(Of FacturaDetalleENT)
        Dim ListaFacturasDetalle As New List(Of FacturaDetalleENT)
        Dim lector As IDataReader = FacturaDetalleMAP.ListarFacturasDetalle(QueObjeto).CreateDataReader
        Do While lector.Read()
            Dim FacturaDetalleENT As New FacturaDetalleENT
            With FacturaDetalleENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdFactura = Convert.ToInt32(lector("IdFactura"))
                .IdProducto = Convert.ToInt32(lector("IdProducto"))
                .ProductoNombre = Convert.ToString(lector("Nombre"))
                .ProductoTipo = Convert.ToString(lector("Tipo"))
                .Cantidad = Convert.ToInt32(lector("Cantidad"))
                .PrecioUnitario = Convert.ToDouble(lector("PrecioUnitario"))
                .Precio = Convert.ToDouble(lector("Precio"))
            End With
            ListaFacturasDetalle.Add(FacturaDetalleENT)
        Loop
        lector.Close()
        Return ListaFacturasDetalle
    End Function

End Class

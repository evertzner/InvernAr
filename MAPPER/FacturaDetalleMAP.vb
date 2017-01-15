Imports DAL
Imports ENTITIES

Public Class FacturaDetalleMAP

    Shared Sub ComprarDetalleFactura(ByRef QueObjeto As Object)
        Dim FacturaDetalle As New FacturaDetalleENT
        FacturaDetalle = DirectCast(QueObjeto, FacturaDetalleENT)
        Dim HT As New Hashtable
        HT.Add("@pIdFactura", FacturaDetalle.IdFactura)
        HT.Add("@pIdProducto", FacturaDetalle.IdProducto)
        HT.Add("@pCantidad", FacturaDetalle.Cantidad)
        HT.Add("@pPrecio", FacturaDetalle.Precio)
        Generico.Escribir("pComprarDetalleFactura", HT)
    End Sub

    Shared Function ListarFacturasDetalle(ByRef QueObjeto As Object) As DataSet
        Dim FacturaDetalle As New FacturaDetalleENT
        FacturaDetalle = DirectCast(QueObjeto, FacturaDetalleENT)
        Dim HT As New Hashtable
        HT.Add("@pIdFactura", FacturaDetalle.IdFactura)
        Return Generico.Leer("pListarFacturasDetalle", HT)
    End Function

End Class

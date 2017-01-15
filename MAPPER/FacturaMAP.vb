Imports DAL
Imports ENTITIES

Public Class FacturaMAP

    Shared Sub ComprarFactura(ByRef QueObjeto As Object)
        Dim Factura As New FacturaENT
        Factura = DirectCast(QueObjeto, FacturaENT)
        Dim HT As New Hashtable
        HT.Add("@pFecha", Factura.Fecha)
        HT.Add("@pIdCliente", Factura.IdCliente)
        HT.Add("@pTotal", Factura.Total)
        Generico.Escribir("pComprarFactura", HT)
    End Sub

    Shared Sub ModificarFactura(ByRef QueObjeto As Object)
        Dim Factura As New FacturaENT
        Factura = DirectCast(QueObjeto, FacturaENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Factura.Id)
        HT.Add("@pTotal", Factura.Total)
        Generico.Escribir("pModificarFactura", HT)
    End Sub

    Shared Function SeleccionarNumeroFactura() As DataSet
        Return Generico.Leer("pSeleccionarNumeroFactura")
    End Function

    Shared Sub CancelarFactura(ByRef QueObjeto As Object)
        Dim Factura As New FacturaENT
        Factura = DirectCast(QueObjeto, FacturaENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Factura.Id)
        Generico.Escribir("pCancelarFactura", HT)
    End Sub

    Shared Function ListarFacturas(ByRef QueObjeto As Object)
        Dim Factura As New FacturaENT
        Factura = DirectCast(QueObjeto, FacturaENT)
        Dim HT As New Hashtable
        HT.Add("@pIdCliente", Factura.IdCliente)
        Return Generico.Leer("pListarFacturas", HT)
    End Function

End Class

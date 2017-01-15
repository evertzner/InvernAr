Imports ENTITIES
Imports DAL

Public Class NotaCreditoMAP

    Shared Function ListarNotasCredito(ByRef QueObjeto As Object) As DataSet
        Dim NotaCredito As New NotaCreditoENT
        NotaCredito = DirectCast(QueObjeto, NotaCreditoENT)
        Dim HT As New Hashtable
        HT.Add("@pIdCliente", NotaCredito.IdCliente)
        Return Generico.Leer("pListarNotasCredito", HT)
    End Function

    Shared Sub ActualizarNotaCredito(ByRef QueObjeto As Object)
        Dim NotaCredito As New NotaCreditoENT
        NotaCredito = DirectCast(QueObjeto, NotaCreditoENT)
        Dim HT As New Hashtable
        HT.Add("@pId", NotaCredito.Id)
        HT.Add("@pImporte", NotaCredito.Saldo)
        Generico.Escribir("pActualizarNotaCredito", HT)
    End Sub

    Shared Sub NuevaNotaCredito(ByRef QueObjeto As Object)
        Dim NotaCredito As New NotaCreditoENT
        NotaCredito = DirectCast(QueObjeto, NotaCreditoENT)
        Dim HT As New Hashtable
        HT.Add("@pIdCliente", NotaCredito.IdCliente)
        HT.Add("@pFecha", NotaCredito.Fecha)
        HT.Add("@pSaldo", NotaCredito.Saldo)
        HT.Add("@pMotivo", NotaCredito.Motivo)
        Generico.Escribir("pNuevaNotaCredito", HT)
    End Sub

    Shared Function SeleccionarNumeroNotaCredito() As DataSet
        Return Generico.Leer("pSeleccionarNumeroNotaCredito")
    End Function

End Class

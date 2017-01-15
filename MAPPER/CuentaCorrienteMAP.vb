Imports ENTITIES
Imports DAL

Public Class CuentaCorrienteMAP

    Shared Sub RegistrarCuentaCorriente(ByRef QueObjeto As Object)
        Dim CuentaCorriente As New CuentaCorrienteENT
        CuentaCorriente = DirectCast(QueObjeto, CuentaCorrienteENT)
        Dim HT As New Hashtable
        HT.Add("@pIdCliente", CuentaCorriente.IdCliente)
        HT.Add("@pIdFactura", CuentaCorriente.IdFactura)
        HT.Add("@pIdNotaCredito", CuentaCorriente.IdNotaCredito)
        HT.Add("@pMotivo", CuentaCorriente.Motivo)
        HT.Add("@pDebito", CuentaCorriente.Debito)
        HT.Add("@pCredito", CuentaCorriente.Credito)
        HT.Add("@pFecha", CuentaCorriente.Fecha)
        Generico.Escribir("pRegistrarCuentaCorriente", HT)
    End Sub

    Shared Function ListarCuentaCorriente(ByRef QueObjeto As Object) As DataSet
        Dim CuentaCorriente As New CuentaCorrienteENT
        CuentaCorriente = DirectCast(QueObjeto, CuentaCorrienteENT)
        Dim HT As New Hashtable
        HT.Add("@pIdCliente", CuentaCorriente.IdCliente)
        Return Generico.Leer("pListarCuentaCorriente", HT)
    End Function

End Class

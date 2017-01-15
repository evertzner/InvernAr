Imports ENTITIES
Imports MAPPER

Public Class CuentaCorrienteBLL

    Public Sub RegistrarCuentaCorriente(ByRef QueObjeto As Object)
        CuentaCorrienteMAP.RegistrarCuentaCorriente(QueObjeto)
    End Sub

    Public Function ListarCuentaCorriente(ByRef QueObjeto As Object) As List(Of CuentaCorrienteENT)
        Dim ListaCuentaCorriente As New List(Of CuentaCorrienteENT)
        Dim lector As IDataReader = CuentaCorrienteMAP.ListarCuentaCorriente(QueObjeto).CreateDataReader
        Do While lector.Read()
            Dim CuentaCorrienteENT As New CuentaCorrienteENT
            With CuentaCorrienteENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdCliente = Convert.ToInt32(lector("IdCliente"))
                .IdFactura = Convert.ToInt32(lector("IdFactura"))
                .IdNotaCredito = Convert.ToInt32(lector("IdNotaCredito"))
                .Motivo = Convert.ToString(lector("Motivo"))
                .Debito = Convert.ToDouble(lector("Debito"))
                .Credito = Convert.ToDouble(lector("Credito"))
                .Fecha = Convert.ToDateTime(lector("Fecha"))
            End With
            ListaCuentaCorriente.Add(CuentaCorrienteENT)
        Loop
        lector.Close()
        Return ListaCuentaCorriente
    End Function

End Class

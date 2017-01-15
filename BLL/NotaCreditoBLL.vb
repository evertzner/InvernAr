Imports ENTITIES
Imports MAPPER

Public Class NotaCreditoBLL

    Public Function ListarNotasCredito(ByRef QueObjeto As Object) As List(Of NotaCreditoENT)
        Dim ListaNotasCredito As New List(Of NotaCreditoENT)
        Dim lector As IDataReader = NotaCreditoMAP.ListarNotasCredito(QueObjeto).CreateDataReader
        Do While lector.Read()
            Dim NotaCreditoENT As New NotaCreditoENT
            With NotaCreditoENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdCliente = Convert.ToInt32(lector("IdCliente"))
                .Fecha = Convert.ToDateTime(lector("Fecha"))
                .Saldo = Convert.ToDouble(lector("Saldo"))
                .Motivo = Convert.ToString(lector("Motivo"))
            End With
            ListaNotasCredito.Add(NotaCreditoENT)
        Loop
        lector.Close()
        Return ListaNotasCredito
    End Function

    Public Sub ActualizarNotaCredito(ByRef QueObjeto As Object)
        NotaCreditoMAP.ActualizarNotaCredito(QueObjeto)
    End Sub

    Public Sub NuevaNotaCredito(ByRef QueObjeto As Object)
        NotaCreditoMAP.NuevaNotaCredito(QueObjeto)
    End Sub

    Public Function SeleccionarNumeroNotaCredito() As Integer
        Dim IdNotaCredito As Integer = 0
        Dim lector As IDataReader = NotaCreditoMAP.SeleccionarNumeroNotaCredito.CreateDataReader
        Do While lector.Read()
            IdNotaCredito = Convert.ToInt32(lector("Id"))
        Loop
        lector.Close()
        Return IdNotaCredito
    End Function

End Class

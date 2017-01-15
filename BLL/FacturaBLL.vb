Imports MAPPER
Imports ENTITIES

Public Class FacturaBLL

    Public Sub ComprarFactura(ByRef QueObjeto As Object)
        FacturaMAP.ComprarFactura(QueObjeto)
    End Sub

    Public Sub ModificarFactura(ByRef QueObjeto As Object)
        FacturaMAP.ModificarFactura(QueObjeto)
    End Sub

    Public Function SeleccionarNumeroFactura() As Integer
        Dim IdFactura As Integer = 0
        Dim lector As IDataReader = FacturaMAP.SeleccionarNumeroFactura.CreateDataReader
        Do While lector.Read()
            IdFactura = Convert.ToInt32(lector("Id"))
        Loop
        lector.Close()
        Return IdFactura
    End Function

    Public Function ListarFacturas(ByRef QueObjeto As Object) As List(Of FacturaENT)
        Dim ListaFacturas As New List(Of FacturaENT)
        Dim lector As IDataReader = FacturaMAP.ListarFacturas(QueObjeto).CreateDataReader
        Do While lector.Read()
            Dim FacturaENT As New FacturaENT
            With FacturaENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdCliente = Convert.ToInt32(lector("IdCliente"))
                .Fecha = Convert.ToDateTime(lector("Fecha"))
                .Total = Convert.ToDouble(lector("Total"))
                .Cancelada = Convert.ToBoolean(lector("Cancelada"))
            End With
            ListaFacturas.Add(FacturaENT)
        Loop
        lector.Close()
        Return ListaFacturas
    End Function

    Public Sub CancelarFactura(ByRef QueObjeto As Object)
        FacturaMAP.CancelarFactura(QueObjeto)
    End Sub

End Class

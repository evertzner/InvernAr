Imports ENTITIES
Imports MAPPER

Public Class TipoProductoBLL
    Dim TipoProductoENT As TipoProductoENT

    Public Function ListarTipoProducto() As List(Of TipoProductoENT)
        Dim ListaTipoProducto As New List(Of TipoProductoENT)
        Dim lector As IDataReader = TipoProductoMAP.ListarTipoProducto.CreateDataReader
        Do While lector.Read()
            TipoProductoENT = New TipoProductoENT
            With TipoProductoENT
                .Tipo = Convert.ToString(lector("Tipo"))
            End With
            ListaTipoProducto.Add(TipoProductoENT)
        Loop
        lector.Close()
        Return ListaTipoProducto
    End Function

End Class

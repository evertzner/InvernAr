Imports ENTITIES
Imports MAPPER

Public Class ProductoBLL
    Dim ProductoENT As ProductoENT

    Public Function ListarProductos() As List(Of ProductoENT)
        Dim ListaProductos As New List(Of ProductoENT)
        Dim lector As IDataReader = ProductoMAP.ListarProductos.CreateDataReader
        Do While lector.Read()
            ProductoENT = New ProductoENT
            With ProductoENT
                .Id = Convert.ToInt32(lector("Id"))
                .Codigo = Convert.ToString(lector("Codigo"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .Especificacion = Convert.ToString(lector("Especificacion"))
                .PrecioUnitario = Convert.ToDouble(lector("PrecioUnitario"))
                .Tipo = Convert.ToString(lector("Tipo"))
                If lector("Imagen") Is DBNull.Value Then
                    .Imagen = Nothing
                Else
                    .Imagen = DirectCast(lector("Imagen"), Byte())
                End If

            End With
            ListaProductos.Add(ProductoENT)
        Loop
        lector.Close()
        Return ListaProductos
    End Function

    Public Function AgregarProducto(ByRef QueObjeto As Object) As Integer
        Dim Flag As Integer = 0
        If ProductoMAP.AgregarProducto(QueObjeto) > 0 Then
            Flag = 1
        Else
            Flag = 0
        End If
        Return Flag
    End Function

    Public Function ModificarProducto(ByRef QueObjeto As Object) As Integer
        Dim Flag As Integer = 0
        If ProductoMAP.ModificarProducto(QueObjeto) > 0 Then
            Flag = 1
        Else
            Flag = 0
        End If
        Return Flag
    End Function

    Public Sub EliminarProducto(ByRef QueObjeto As Object)
        ProductoMAP.EliminarProducto(QueObjeto)
    End Sub

End Class

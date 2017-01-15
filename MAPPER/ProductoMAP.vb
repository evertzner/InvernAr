Imports ENTITIES
Imports DAL

Public Class ProductoMAP

    Shared Function ListarProductos() As DataSet
        Return Generico.Leer("pListarProductos")
    End Function

    Shared Function AgregarProducto(ByRef QueObjeto As Object) As Integer
        Dim Producto As New ProductoENT
        Producto = DirectCast(QueObjeto, ProductoENT)
        Dim HT As New Hashtable
        HT.Add("@pCodigo", Producto.Codigo)
        HT.Add("@pNombre", Producto.Nombre)
        HT.Add("@pEspecificacion", Producto.Especificacion)
        HT.Add("@pPrecioUnitario", Producto.PrecioUnitario)
        HT.Add("@pTipo", Producto.Tipo)
        If Producto.Imagen Is Nothing Then
            HT.Add("@pImagen", New SqlTypes.SqlBytes)
        Else
            HT.Add("@pImagen", Producto.Imagen)
        End If
        Return Generico.Escribir("pAgregarProducto", HT)
    End Function

    Shared Function ModificarProducto(ByRef QueObjeto As Object) As Integer
        Dim Producto As New ProductoENT
        Producto = DirectCast(QueObjeto, ProductoENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Producto.Id)
        HT.Add("@pCodigo", Producto.Codigo)
        HT.Add("@pNombre", Producto.Nombre)
        HT.Add("@pEspecificacion", Producto.Especificacion)
        HT.Add("@pPrecioUnitario", Producto.PrecioUnitario)
        HT.Add("@pTipo", Producto.Tipo)
        If Producto.Imagen Is Nothing Then
            HT.Add("@pImagen", New SqlTypes.SqlBytes)
        Else
            HT.Add("@pImagen", Producto.Imagen)
        End If
        Return Generico.Escribir("pModificarProducto", HT)
    End Function

    Shared Sub EliminarProducto(ByRef QueObjeto As Object)
        Dim Producto As New ProductoENT
        Producto = DirectCast(QueObjeto, ProductoENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Producto.Id)
        Generico.Escribir("pEliminarProducto", HT)
    End Sub

End Class

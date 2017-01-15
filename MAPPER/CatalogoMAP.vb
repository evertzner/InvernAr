Imports ENTITIES
Imports DAL

Public Class CatalogoMAP

    Shared Function ListarCatalogo() As DataSet
        Return Generico.Leer("pListarCatalogo")
    End Function

    Shared Function ListarProductosCatalogo(ByRef QueObjeto As Object) As DataSet
        Dim Producto As New ProductoENT
        Producto = DirectCast(QueObjeto, ProductoENT)
        Dim HT As New Hashtable
        HT.Add("@pTipo", Producto.Tipo)
        Return Generico.Leer("pListarProductosCatalogo", HT)
    End Function

    Shared Function AgregarProductoCatalogo(ByRef QueObjeto As Object, Orden As Integer) As Integer
        Dim Producto As New ProductoENT
        Producto = DirectCast(QueObjeto, ProductoENT)
        Dim HT As New Hashtable
        HT.Add("@pCodigo", Producto.Codigo)
        HT.Add("@pOrden", Orden)
        Return Generico.Escribir("pAgregarProductoCatalogo", HT)
    End Function

    Shared Sub ModificarOrdenCatalogo(ByRef QueObjeto As Object)
        Dim Catalogo As New CatalogoENT
        Catalogo = DirectCast(QueObjeto, CatalogoENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Catalogo.Id)
        HT.Add("@pOrden", Catalogo.Orden)
        Generico.Escribir("pModificarOrdenCatalogo", HT)
    End Sub

    Shared Sub EliminarProductoCatalogo(ByRef QueObjeto As Object)
        Dim ProductoCatalogo As New ProductoCatalogoENT
        ProductoCatalogo = DirectCast(QueObjeto, ProductoCatalogoENT)
        Dim HT As New Hashtable
        HT.Add("@pIdProducto", ProductoCatalogo.Id)
        Generico.Escribir("pEliminarProductoCatalogo", HT)
    End Sub

End Class

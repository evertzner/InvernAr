Imports ENTITIES
Imports MAPPER

Public Class CatalogoBLL
    Dim ProductoCatalogoENT As ProductoCatalogoENT
    Dim CatalogoENT As CatalogoENT

    Public Function ListarCatalogo() As List(Of CatalogoENT)
        Dim ListaCatalogo As New List(Of CatalogoENT)
        Dim lector As IDataReader = CatalogoMAP.ListarCatalogo.CreateDataReader
        Do While lector.Read()
            CatalogoENT = New CatalogoENT
            With CatalogoENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdProducto = Convert.ToInt32(lector("IdProducto"))
                .Orden = Convert.ToInt32(lector("Orden"))
            End With
            ListaCatalogo.Add(CatalogoENT)
        Loop
        lector.Close()
        Return ListaCatalogo
    End Function

    Public Function ListarProductosCatalogo(ByRef QueObjeto As Object) As List(Of ProductoCatalogoENT)
        Dim ListaProductosCatalogo As New List(Of ProductoCatalogoENT)
        Dim lector As IDataReader = CatalogoMAP.ListarProductosCatalogo(QueObjeto).CreateDataReader
        Do While lector.Read()
            ProductoCatalogoENT = New ProductoCatalogoENT
            With ProductoCatalogoENT
                .Id = Convert.ToInt32(lector("Id"))
                .Codigo = Convert.ToString(lector("Codigo"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .Especificacion = Convert.ToString(lector("Especificacion"))
                .PrecioUnitario = Convert.ToDouble(lector("PrecioUnitario"))
                .Tipo = Convert.ToString(lector("Tipo"))
                .Imagen = DirectCast(lector("Imagen"), Byte())
                .Orden = Convert.ToInt32(lector("Orden"))
            End With
            ListaProductosCatalogo.Add(ProductoCatalogoENT)
        Loop
        lector.Close()
        Return ListaProductosCatalogo
    End Function

    Public Function AgregarProductoCatalogo(ByRef QueObjeto As Object, Orden As Integer) As Integer
        Dim Flag As Integer = 0
        If CatalogoMAP.AgregarProductoCatalogo(QueObjeto, Orden) < 0 Then
            Flag = 0
        Else
            Flag = 1
        End If
        Return Flag
    End Function

    Public Sub ModificarOrdenCatalogo(ByRef QueObjeto As Object)
        CatalogoMAP.ModificarOrdenCatalogo(QueObjeto)
    End Sub

    Public Sub EliminarProductoCatalogo(ByRef QueObjeto As Object)
        CatalogoMAP.EliminarProductoCatalogo(QueObjeto)
    End Sub

End Class

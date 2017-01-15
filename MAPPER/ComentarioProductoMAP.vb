Imports ENTITIES
Imports DAL

Public Class ComentarioProductoMAP

    Shared Function ListarComentarios(ByRef QueObjeto As Object) As DataSet
        Dim ProductoCatalogoENT As New ProductoCatalogoENT
        ProductoCatalogoENT = DirectCast(QueObjeto, ProductoCatalogoENT)
        Dim HT As New Hashtable
        HT.Add("@pIdProducto", ProductoCatalogoENT.Id)
        Return Generico.Leer("pListarComentarios", HT)
    End Function

    Shared Sub AgregarComentario(ByRef QueObjeto As Object)
        Dim ComentarioProducto As New ComentarioProductoENT
        ComentarioProducto = DirectCast(QueObjeto, ComentarioProductoENT)
        Dim HT As New Hashtable
        HT.Add("@pIdUsuario", ComentarioProducto.IdUsuario)
        HT.Add("@pIdProducto", ComentarioProducto.IdProducto)
        HT.Add("@pComentario", ComentarioProducto.Comentario)
        HT.Add("@pValorizacion", ComentarioProducto.Valorizacion)
        HT.Add("@pFechaComentado", ComentarioProducto.FechaComentado)
        Generico.Escribir("pAgregarComentario", HT)
    End Sub

End Class

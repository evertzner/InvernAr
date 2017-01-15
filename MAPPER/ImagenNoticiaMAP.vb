Imports ENTITIES
Imports DAL

Public Class ImagenNoticiaMAP

    Shared Function ListarImagenNoticia(ByRef QueObjeto As Object) As DataSet
        Dim ImagenNoticia As New ImagenNoticiaENT
        ImagenNoticia = DirectCast(QueObjeto, ImagenNoticiaENT)
        Dim HT As New Hashtable
        HT.Add("@pIdNoticia", ImagenNoticia.IdNoticia)
        Return Generico.Leer("pListarImagenNoticia", HT)
    End Function

    Shared Sub NuevaImagenNoticia(ByRef QueObjeto As Object)
        Dim ImagenNoticia As New ImagenNoticiaENT
        ImagenNoticia = DirectCast(QueObjeto, ImagenNoticiaENT)
        Dim HT As New Hashtable
        HT.Add("@pIdNoticia", ImagenNoticia.IdNoticia)
        HT.Add("@pImagen", ImagenNoticia.Imagen)
        Generico.Escribir("pNuevaImagenNoticia", HT)
    End Sub

    Shared Sub EliminarImagenNoticia(ByRef QueObjeto As Object)
        Dim ImagenNoticia As New ImagenNoticiaENT
        ImagenNoticia = DirectCast(QueObjeto, ImagenNoticiaENT)
        Dim HT As New Hashtable
        HT.Add("@pId", ImagenNoticia.Id)
        Generico.Escribir("pEliminarImagenNoticia", HT)
    End Sub

End Class

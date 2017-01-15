Imports ENTITIES
Imports DAL

Public Class NoticiaMAP

    Shared Function ListarNoticias() As DataSet
        Return Generico.Leer("pListarNoticias")
    End Function

    Shared Sub NuevaNoticia(ByRef QueObjeto As Object)
        Dim Noticia As New NoticiaENT
        Noticia = DirectCast(QueObjeto, NoticiaENT)
        Dim HT As New Hashtable
        HT.Add("@pTitulo", Noticia.Titulo)
        HT.Add("@pContenido", Noticia.Contenido)
        HT.Add("@pFechaHora", Noticia.FechaHora)
        If Noticia.Imagen Is Nothing Then
            HT.Add("@pImagen", New SqlTypes.SqlBytes)
        Else
            HT.Add("@pImagen", Noticia.Imagen)
        End If
        Generico.Escribir("pNuevaNoticia", HT)
    End Sub

    Shared Sub ModificarNoticia(ByRef QueObjeto As Object)
        Dim Noticia As New NoticiaENT
        Noticia = DirectCast(QueObjeto, NoticiaENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Noticia.Id)
        HT.Add("@pTitulo", Noticia.Titulo)
        HT.Add("@pContenido", Noticia.Contenido)
        HT.Add("@pFechaHora", Noticia.FechaHora)
        If Noticia.Imagen Is Nothing Then
            HT.Add("@pImagen", New SqlTypes.SqlBytes)
        Else
            HT.Add("@pImagen", Noticia.Imagen)
        End If
        Generico.Escribir("pModificarNoticia", HT)
    End Sub

    Shared Sub EliminarNoticia(ByRef QueObjeto As Object)
        Dim Noticia As New NoticiaENT
        Noticia = DirectCast(QueObjeto, NoticiaENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Noticia.Id)
        Generico.Escribir("pEliminarNoticia", HT)
    End Sub

End Class

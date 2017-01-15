Imports ENTITIES
Imports DAL

Public Class CategoriaNewsletterMAP

    Shared Function ListarCategoriasNewsletter() As DataSet
        Return Generico.Leer("pListarCategoriasNewsletter")
    End Function

    Shared Function EliminarCategoriaNewsletter(ByRef QueObjeto As Object) As Integer
        Dim CategoriaNewsletter As New CategoriaNewsletterENT
        CategoriaNewsletter = DirectCast(QueObjeto, CategoriaNewsletterENT)
        Dim HT As New Hashtable
        HT.Add("@pId", CategoriaNewsletter.Id)
        Return Generico.Escribir("pEliminarCategoriaNewsletter", HT)
    End Function

    Shared Sub ModificarCategoriaNewsletter(ByRef QueObjeto As Object)
        Dim CategoriaNewsletter As New CategoriaNewsletterENT
        CategoriaNewsletter = DirectCast(QueObjeto, CategoriaNewsletterENT)
        Dim HT As New Hashtable
        HT.Add("@pId", CategoriaNewsletter.Id)
        HT.Add("@pCategoria", CategoriaNewsletter.Categoria)
        Generico.Escribir("pModificarCategoriaNewsletter", HT)
    End Sub

    Shared Sub NuevaCategoriaNewsletter(ByRef QueObjeto As Object)
        Dim CategoriaNewsletter As New CategoriaNewsletterENT
        CategoriaNewsletter = DirectCast(QueObjeto, CategoriaNewsletterENT)
        Dim HT As New Hashtable
        HT.Add("@pCategoria", CategoriaNewsletter.Categoria)
        Generico.Escribir("pNuevaCategoriaNewsletter", HT)
    End Sub

End Class

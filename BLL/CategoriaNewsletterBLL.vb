Imports ENTITIES
Imports MAPPER

Public Class CategoriaNewsletterBLL
    Dim CategoriaNewsletterENT As CategoriaNewsletterENT

    Public Sub NuevaCategoriaNewsletter(ByRef QueObjeto As Object)
        CategoriaNewsletterMAP.NuevaCategoriaNewsletter(QueObjeto)
    End Sub

    Public Sub ModificarCategoriaNewsletter(ByRef QueObjeto As Object)
        CategoriaNewsletterMAP.ModificarCategoriaNewsletter(QueObjeto)
    End Sub

    Public Function EliminarCategoriaNewsletter(ByRef QueObjeto As Object) As Integer
        Return CategoriaNewsletterMAP.EliminarCategoriaNewsletter(QueObjeto)
    End Function

    Public Function ListarCategoriasNewsletter() As List(Of CategoriaNewsletterENT)
        Dim ListaCategoriasNewsletter As New List(Of CategoriaNewsletterENT)
        Dim lector As IDataReader = CategoriaNewsletterMAP.ListarCategoriasNewsletter.CreateDataReader
        Do While lector.Read()
            CategoriaNewsletterENT = New CategoriaNewsletterENT
            With CategoriaNewsletterENT
                .Id = Convert.ToInt32(lector("Id"))
                .Categoria = Convert.ToString(lector("Categoria"))
            End With
            ListaCategoriasNewsletter.Add(CategoriaNewsletterENT)
        Loop
        lector.Close()
        Return ListaCategoriasNewsletter
    End Function

End Class

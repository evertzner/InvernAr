Imports ENTITIES
Imports DAL

Public Class NewsletterMAP

    Shared Function AltaSuscripcionNewsletter(ByRef QueObjeto As Object) As Integer
        Dim SuscripcionNewsletter As New SuscripcionNewsletterENT
        SuscripcionNewsletter = DirectCast(QueObjeto, SuscripcionNewsletterENT)
        Dim HT As New Hashtable
        HT.Add("@pCorreoElectronico", SuscripcionNewsletter.CorreoElectronico)
        HT.Add("@pCategoria", SuscripcionNewsletter.Categoria)
        Return Generico.Escribir("pAltaSuscripcionNewsletter", HT)
    End Function

    Shared Sub BajaSuscripcionNewsletter(ByRef QueObjeto As Object)
        Dim SuscripcionNewsletter As New SuscripcionNewsletterENT
        SuscripcionNewsletter = DirectCast(QueObjeto, SuscripcionNewsletterENT)
        Dim HT As New Hashtable
        HT.Add("@pCorreoElectronico", SuscripcionNewsletter.CorreoElectronico)
        HT.Add("@pCategoria", SuscripcionNewsletter.Categoria)
        Generico.Escribir("pBajaSuscripcionNewsletter", HT)
    End Sub

    Shared Function ListarSuscripcionesNewsletter() As DataSet
        Return Generico.Leer("pListarSuscripcionesNewsletter")
    End Function

    Shared Sub NuevoNewsletter(ByRef QueObjeto As Object)
        Dim Newsletter As New NewsletterENT
        Newsletter = DirectCast(QueObjeto, NewsletterENT)
        Dim HT As New Hashtable
        HT.Add("@pNombre", Newsletter.Nombre)
        HT.Add("@pDescripcion", Newsletter.Descripcion)
        HT.Add("@pAsunto", Newsletter.Asunto)
        HT.Add("@pCuerpo", Newsletter.Cuerpo)
        If Newsletter.Imagen Is Nothing Then
            HT.Add("@pImagen", New SqlTypes.SqlBytes)
        Else
            HT.Add("@pImagen", Newsletter.Imagen)
        End If
        HT.Add("@pFechaHora", Newsletter.FechaHora)
        HT.Add("@pCategoria", Newsletter.Categoria)
        Generico.Escribir("pNuevoNewsletter", HT)
    End Sub

    Shared Sub ModificarNewsletter(ByRef QueObjeto As Object)
        Dim Newsletter As New NewsletterENT
        Newsletter = DirectCast(QueObjeto, NewsletterENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Newsletter.Id)
        HT.Add("@pNombre", Newsletter.Nombre)
        HT.Add("@pDescripcion", Newsletter.Descripcion)
        HT.Add("@pAsunto", Newsletter.Asunto)
        HT.Add("@pCuerpo", Newsletter.Cuerpo)
        If Newsletter.Imagen Is Nothing Then
            HT.Add("@pImagen", New SqlTypes.SqlBytes)
        Else
            HT.Add("@pImagen", Newsletter.Imagen)
        End If
        HT.Add("@pFechaHora", Newsletter.FechaHora)
        HT.Add("@pCategoria", Newsletter.Categoria)
        Generico.Escribir("pModificarNewsletter", HT)
    End Sub

    Shared Sub EliminarNewsletter(ByRef QueObjeto As Object)
        Dim Newsletter As New NewsletterENT
        Newsletter = DirectCast(QueObjeto, NewsletterENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Newsletter.Id)
        Generico.Escribir("pEliminarNewsletter", HT)
    End Sub

    Shared Function ListarNewsletter() As DataSet
        Return Generico.Leer("pListarNewsletter")
    End Function

End Class

Imports MAPPER
Imports ENTITIES

Public Class NewsletterBLL
    Dim NewsletterENT As NewsletterENT

    Public Function AltaSuscripcionNewsletter(ByRef QueObjeto As Object) As Integer
        Return NewsletterMAP.AltaSuscripcionNewsletter(QueObjeto)
    End Function

    Public Sub BajaSuscripcionNewsletter(ByRef QueObjeto As Object)
        NewsletterMAP.BajaSuscripcionNewsletter(QueObjeto)
    End Sub

    Public Function ListarSuscripcionesNewsletter() As List(Of SuscripcionNewsletterENT)
        Dim ListaSuscripcionesNewsletter As New List(Of SuscripcionNewsletterENT)
        Dim lector As IDataReader = NewsletterMAP.ListarSuscripcionesNewsletter.CreateDataReader
        Do While lector.Read()
            Dim SuscripcionNewsletterENT As New SuscripcionNewsletterENT
            With SuscripcionNewsletterENT
                .CorreoElectronico = Convert.ToString(lector("CorreoElectronico"))
                .Categoria = Convert.ToInt32(lector("Categoria"))
            End With
            ListaSuscripcionesNewsletter.Add(SuscripcionNewsletterENT)
        Loop
        lector.Close()
        Return ListaSuscripcionesNewsletter
    End Function

    Public Sub NuevoNewsletter(ByRef QueObjeto As Object)
        NewsletterMAP.NuevoNewsletter(QueObjeto)
    End Sub

    Public Sub ModificarNewsletter(ByRef QueObjeto As Object)
        NewsletterMAP.ModificarNewsletter(QueObjeto)
    End Sub

    Public Sub EliminarNewsletter(ByRef QueObjeto As Object)
        NewsletterMAP.EliminarNewsletter(QueObjeto)
    End Sub

    Public Function ListarNewsletter() As List(Of NewsletterENT)
        Dim ListaNewsletter As New List(Of NewsletterENT)
        Dim lector As IDataReader = NewsletterMAP.ListarNewsletter.CreateDataReader
        Do While lector.Read()
            NewsletterENT = New NewsletterENT
            With NewsletterENT
                .Id = Convert.ToInt32(lector("Id"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .Descripcion = Convert.ToString(lector("Descripcion"))
                .Asunto = Convert.ToString(lector("Asunto"))
                .Cuerpo = Convert.ToString(lector("Cuerpo"))
                If lector("Imagen") Is DBNull.Value Then
                    .Imagen = Nothing
                Else
                    .Imagen = DirectCast(lector("Imagen"), Byte())
                End If
                .FechaHora = Convert.ToDateTime(lector("FechaHora"))
                .Categoria = Convert.ToString(lector("Categoria"))
            End With
            ListaNewsletter.Add(NewsletterENT)
        Loop
        lector.Close()
        Return ListaNewsletter
    End Function

End Class

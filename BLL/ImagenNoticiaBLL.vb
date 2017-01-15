Imports ENTITIES
Imports MAPPER

Public Class ImagenNoticiaBLL
    Dim ImagenNoticiaENT As ImagenNoticiaENT

    Public Function ListarImagenNoticia(ByRef QueObjeto As Object) As List(Of ImagenNoticiaENT)
        Dim ListaImagenNoticia As New List(Of ImagenNoticiaENT)
        Dim lector As IDataReader = ImagenNoticiaMAP.ListarImagenNoticia(QueObjeto).CreateDataReader
        Do While lector.Read()
            ImagenNoticiaENT = New ImagenNoticiaENT
            With ImagenNoticiaENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdNoticia = Convert.ToUInt32(lector("IdNoticia"))
                .Imagen = DirectCast(lector("Imagen"), Byte())
            End With
            ListaImagenNoticia.Add(ImagenNoticiaENT)
        Loop
        lector.Close()
        Return ListaImagenNoticia
    End Function

    Public Sub NuevaImagenNoticia(ByRef QueObjeto As Object)
        ImagenNoticiaMAP.NuevaImagenNoticia(QueObjeto)
    End Sub

    Public Sub EliminarImagenNoticia(ByRef QueObjeto As Object)
        ImagenNoticiaMAP.EliminarImagenNoticia(QueObjeto)
    End Sub

End Class

Imports MAPPER
Imports ENTITIES

Public Class NoticiaBLL
    Dim NoticiaENT As NoticiaENT

    Public Function ListarNoticias() As List(Of NoticiaENT)
        Dim ListaNoticias As New List(Of NoticiaENT)
        Dim lector As IDataReader = NoticiaMAP.ListarNoticias.CreateDataReader
        Do While lector.Read()
            NoticiaENT = New NoticiaENT
            With NoticiaENT
                .Id = Convert.ToInt32(lector("Id"))
                .Titulo = Convert.ToString(lector("Titulo"))
                .Contenido = Convert.ToString(lector("Contenido"))
                .FechaHora = Convert.ToDateTime(lector("FechaHora"))
                If lector("Imagen") Is DBNull.Value Then
                    .Imagen = Nothing
                Else
                    .Imagen = DirectCast(lector("Imagen"), Byte())
                End If

            End With
            ListaNoticias.Add(NoticiaENT)
        Loop
        lector.Close()
        Return ListaNoticias
    End Function

    Public Sub NuevaNoticia(ByRef QueObjeto As Object)
        NoticiaMAP.NuevaNoticia(QueObjeto)
    End Sub

    Public Sub ModificarNoticia(ByRef QueObjeto As Object)
        NoticiaMAP.ModificarNoticia(QueObjeto)
    End Sub

    Public Sub EliminarNoticia(ByRef QueObjeto As Object)
        NoticiaMAP.EliminarNoticia(QueObjeto)
    End Sub

End Class

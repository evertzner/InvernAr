Imports ENTITIES
Imports MAPPER

Public Class ComentarioProductoBLL
    Dim ComentarioProductoENT As New ComentarioProductoENT
    Dim ListaComentarios As New List(Of ComentarioProductoENT)

    Public Function ListarComentarios(ByRef QueObjeto As Object) As List(Of ComentarioProductoENT)
        Dim lector As IDataReader = ComentarioProductoMAP.ListarComentarios(QueObjeto).CreateDataReader
        Do While lector.Read()
            ComentarioProductoENT = New ComentarioProductoENT
            With ComentarioProductoENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdProducto = Convert.ToInt32(lector("IdProducto"))
                .IdUsuario = Convert.ToInt32(lector("IdUsuario"))
                .Comentario = Convert.ToString(lector("Comentario"))
                .Valorizacion = Convert.ToInt32(lector("Valorizacion"))
                .FechaComentado = Convert.ToString(lector("FechaComentado"))
                .NombreUsuario = Convert.ToString(lector("Nombre"))
            End With
            ListaComentarios.Add(ComentarioProductoENT)
        Loop
        lector.Close()
        Return ListaComentarios
    End Function

    Public Sub AgregarComentario(ByRef QueObjeto As Object)
        ComentarioProductoMAP.AgregarComentario(QueObjeto)
    End Sub

End Class

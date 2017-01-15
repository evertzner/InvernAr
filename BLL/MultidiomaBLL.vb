Imports ENTITIES
Imports MAPPER

Public Class MultidiomaBLL

    Public Sub AgregarTraduccion(ByRef QueObjeto As Object)
        MultidiomaMAP.AgregarTraduccion(QueObjeto)
    End Sub

    Public Sub ModificarTraduccion(ByRef QueObjeto As Object)
        MultidiomaMAP.ModificarTraduccion(QueObjeto)
    End Sub

    Public Sub EliminarTraduccion(ByRef QueObjeto As Object)
        MultidiomaMAP.EliminarTraduccion(QueObjeto)
    End Sub

    Public Function ListarTraduccion(ByRef QueObjeto As Object) As List(Of MultidiomaENT)
        Dim lista As New List(Of MultidiomaENT)
        Try
            Dim lector As IDataReader = MultidiomaMAP.ListarTraduccion(QueObjeto).CreateDataReader
            Do While lector.Read()
                Dim MultidiomaENT As New MultidiomaENT
                MultidiomaENT.Etiqueta = Convert.ToString(lector("Etiqueta"))
                MultidiomaENT.Traduccion = Convert.ToString(lector("Traduccion"))
                lista.Add(MultidiomaENT)
            Loop
            lector.Close()
        Catch ex As Exception
            Throw
        End Try
        Return lista
    End Function

    Public Function ListarIdiomas(ByVal Booleano As Boolean) As List(Of MultidiomaENT)
        Dim MultidiomaENT As New MultidiomaENT
        Dim lista As New List(Of MultidiomaENT)
        Try
            Dim lector As IDataReader = MultidiomaMAP.ListarIdiomas(Booleano).CreateDataReader
            Do While lector.Read()
                MultidiomaENT = New MultidiomaENT
                MultidiomaENT.Idioma = New System.Globalization.CultureInfo(Convert.ToString(lector("Idioma")))
                lista.Add(MultidiomaENT)
            Loop
            lector.Close()
        Catch ex As Exception
            Throw
        End Try
        Return lista
    End Function

End Class

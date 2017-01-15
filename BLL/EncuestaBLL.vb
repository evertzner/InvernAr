Imports MAPPER
Imports ENTITIES

Public Class EncuestaBLL
    Dim EncuestaENT As EncuestaENT

    Public Function ListarEncuestas() As List(Of EncuestaENT)
        Dim ListaEncuesta As New List(Of EncuestaENT)
        Dim lector As IDataReader = EncuestaMAP.ListarEncuestas.CreateDataReader
        Do While lector.Read()
            EncuestaENT = New EncuestaENT
            With EncuestaENT
                .Id = Convert.ToInt32(lector("Id"))
                .Tema = Convert.ToString(lector("Tema"))
                .FechaVencimiento = Convert.ToDateTime(lector("FechaVencimiento"))
            End With
            ListaEncuesta.Add(EncuestaENT)
        Loop
        lector.Close()
        Return ListaEncuesta
    End Function

    Public Sub NuevaEncuesta(ByRef QueObjeto As Object)
        EncuestaMAP.NuevaEncuesta(QueObjeto)
    End Sub

    Public Sub ModificarEncuesta(ByRef QueObjeto As Object)
        EncuestaMAP.ModificarEncuesta(QueObjeto)
    End Sub

    Public Function EliminarEncuesta(ByRef QueObjeto As Object) As Integer
        Return EncuestaMAP.EliminarEncuesta(QueObjeto)
    End Function

End Class

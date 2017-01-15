Imports ENTITIES
Imports MAPPER

Public Class RespuestaEncuestaBLL
    Dim EncuestaENT As EncuestaENT

    Public Sub Responder(ByRef QueObjeto As Object)
        RespuestaEncuestaMAP.Responder(QueObjeto)
    End Sub

    Public Function ListarEncuestasCliente() As List(Of EncuestaENT)
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
End Class

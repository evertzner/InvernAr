Imports ENTITIES
Imports MAPPER

Public Class InstalacionBLL

    Public Sub AgregarInstalacion(ByRef QueObjeto As Object)
        InstalacionMAP.AgregarInstalacion(QueObjeto)
    End Sub

    Public Sub RealizarInstalacion(ByRef QueObjeto As Object)
        InstalacionMAP.RealizarInstalacion(QueObjeto)
    End Sub

    Public Function ListarInstalaciones() As List(Of InstalacionENT)
        Dim ListaInstalaciones As New List(Of InstalacionENT)
        Dim lector As IDataReader = InstalacionMAP.ListarInstalaciones.CreateDataReader
        Do While lector.Read()
            Dim InstalacionENT As New InstalacionENT
            With InstalacionENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdCliente = Convert.ToInt32(lector("IdCliente"))
                .FechaDeSolicitud = Convert.ToDateTime(lector("FechaDeSolicitud"))
                .DatosDeContacto = Convert.ToString(lector("DatosDeContacto"))
                .DomicilioDeInstalacion = Convert.ToString(lector("DomicilioDeInstalacion"))
                .Observaciones = Convert.ToString(lector("Observaciones"))
                .FechaDeRealizacion = Convert.ToDateTime(lector("FechaDeRealizacion"))
                .Realizado = Convert.ToBoolean(lector("Realizado"))
            End With
            ListaInstalaciones.Add(InstalacionENT)
        Loop
        lector.Close()
        Return ListaInstalaciones
    End Function

    Public Sub ModificarInstalacion(ByRef QueObjeto As Object)
        InstalacionMAP.ModificarInstalacion(QueObjeto)
    End Sub

    Public Function EliminarInstalacion(ByRef QueObjeto As Object) As Integer
        Return InstalacionMAP.EliminarInstalacion(QueObjeto)
    End Function

End Class

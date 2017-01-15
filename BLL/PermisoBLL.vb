Imports ENTITIES
Imports MAPPER

Public Class PermisoBLL
    Dim PermisoENT As PermisoENT
    Dim ListaPermisos As New List(Of PermisoENT)

    Public Function QuePermisos(QueObjeto As Object) As List(Of PermisoENT)
        Dim lector As IDataReader = PermisoMAP.ConsultaPermisos(QueObjeto).CreateDataReader
        Do While lector.Read()
            PermisoENT = New PermisoENT
            With PermisoENT
                .Codigo = Convert.ToString(lector("Codigo"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .Descripcion = Convert.ToString(lector("Descripcion"))
            End With
            ListaPermisos.Add(PermisoENT)
        Loop
        lector.Close()
        Return ListaPermisos
    End Function

    Public Function ListarPermisos() As List(Of PermisoENT)
        Dim lector As IDataReader = PermisoMAP.ListarPermisos.CreateDataReader
        Do While lector.Read()
            PermisoENT = New PermisoENT
            With PermisoENT
                .Codigo = Convert.ToString(lector("Codigo"))
                .Nombre = Convert.ToString(lector("Nombre"))
                .Descripcion = Convert.ToString(lector("Descripcion"))
            End With
            ListaPermisos.Add(PermisoENT)
        Loop
        lector.Close()
        Return ListaPermisos
    End Function

End Class

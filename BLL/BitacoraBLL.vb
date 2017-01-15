Imports ENTITIES
Imports MAPPER

Public Class BitacoraBLL
    Dim BitacoraENT As BitacoraENT
    Dim ListaBitacora As New List(Of BitacoraENT)
    Dim UsuarioENT As UsuarioENT
    Dim ListaUsuarios As New List(Of UsuarioENT)

    Public Sub RegistrarBitacora(ByRef QueObjeto As Object)
        Try
            BitacoraMAP.RegistrarBitacora(QueObjeto)
        Catch ex As Exception

        End Try
    End Sub

    Public Function Atributos(Usuario As String, Actividad As String) As BitacoraENT
        Dim Bitacora As New BitacoraENT
        Bitacora.FechaActividad = Now
        Bitacora.Usuario = Usuario
        Bitacora.Actividad = Actividad
        Return Bitacora
    End Function

    Public Function ConsultarBitacora(ByRef QueObjeto As Object) As List(Of BitacoraENT)
        Dim lector As IDataReader = BitacoraMAP.ConsultarBitacora(QueObjeto).CreateDataReader
        Do While lector.Read()
            BitacoraENT = New BitacoraENT
            With BitacoraENT
                .ID = Convert.ToInt32(lector("Id"))
                .FechaActividad = Convert.ToDateTime(lector("FechaActividad"))
                .Usuario = Convert.ToString(lector("Usuario"))
                .Actividad = Convert.ToString(lector("Actividad"))
            End With
            ListaBitacora.Add(BitacoraENT)
        Loop
        lector.Close()
        Return ListaBitacora
    End Function

    Public Function ConsultarUsuariosEnBitacora() As List(Of UsuarioENT)
        Dim lector As IDataReader = BitacoraMAP.ConsultarUsuariosEnBitacora.CreateDataReader
        Do While lector.Read()
            UsuarioENT = New UsuarioENT
            UsuarioENT.CorreoElectronico = Convert.ToString(lector("Usuario"))
            ListaUsuarios.Add(UsuarioENT)
        Loop
        lector.Close()
        Return ListaUsuarios
    End Function



End Class

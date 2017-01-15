Imports ENTITIES
Imports MAPPER

Public Class ChatBLL
    Dim ChatENT As ChatENT
    Dim ChatCantidadENT As ChatCantidadENT

    Public Function ListarTodosLosMensajes() As List(Of ChatENT)
        Dim ListaChat As New List(Of ChatENT)
        Dim lector As IDataReader = ChatMAP.ListarTodosLosMensajes.CreateDataReader
        Do While lector.Read()
            ChatENT = New ChatENT
            With ChatENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdUsuario = Convert.ToInt32(lector("IdUsuario"))
                .Mensaje = Convert.ToString(lector("Mensaje"))
                .FechaHora = Convert.ToDateTime(lector("FechaHora"))
                .Leido = Convert.ToBoolean(lector("Leido"))
                .Respuesta = Convert.ToBoolean(lector("Respuesta"))
            End With
            ListaChat.Add(ChatENT)
        Loop
        lector.Close()
        Return ListaChat
    End Function

    Public Function ListarCantidadDeMensajes() As List(Of ChatCantidadENT)
        Dim ListaChatCantidad As New List(Of ChatCantidadENT)
        Dim lector As IDataReader = ChatMAP.ListarCantidadDeMensajes.CreateDataReader
        Do While lector.Read()
            ChatCantidadENT = New ChatCantidadENT
            With ChatCantidadENT
                .IdUsuario = Convert.ToInt32(lector("IdUsuario"))
                .CorreoElectronico = Convert.ToString(lector("CorreoElectronico"))
                .NoLeido = Convert.ToInt32(lector("NoLeido"))
            End With
            ListaChatCantidad.Add(ChatCantidadENT)
        Loop
        lector.Close()
        Return ListaChatCantidad
    End Function

    Public Function ListarMensajes(ByRef QueObjeto As Object) As List(Of ChatENT)
        Dim ListaChat As New List(Of ChatENT)
        Dim lector As IDataReader = ChatMAP.ListarMensajes(QueObjeto).CreateDataReader
        Do While lector.Read()
            ChatENT = New ChatENT
            With ChatENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdUsuario = Convert.ToInt32(lector("IdUsuario"))
                .Mensaje = Convert.ToString(lector("Mensaje"))
                .FechaHora = Convert.ToDateTime(lector("FechaHora"))
                .Leido = Convert.ToBoolean(lector("Leido"))
                .Respuesta = Convert.ToBoolean(lector("Respuesta"))
            End With
            ListaChat.Add(ChatENT)
        Loop
        lector.Close()
        Return ListaChat
    End Function

    Public Sub NuevoMensaje(ByRef QueObjeto As Object)
        ChatMAP.NuevoMensaje(QueObjeto)
    End Sub

    Public Sub LeerMensaje(ByRef QueObjeto As Object)
        ChatMAP.LeerMensaje(QueObjeto)
    End Sub

    Public Function NotificarNuevoMensaje(ByRef QueObjeto As Object, Respuesta As Boolean) As Integer
        Dim Flag As Integer = 0
        Dim lector As IDataReader = ChatMAP.NotificarNuevoMensaje(QueObjeto, Respuesta).CreateDataReader
        Do While lector.Read()
            Flag = lector(0)
        Loop
        Return Flag
    End Function

End Class

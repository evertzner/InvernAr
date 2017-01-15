Imports System.Data.SqlClient
Imports System.Configuration

Public Class Generico

    'Shared Function Conectar() As SqlConnection
    '    Dim Conexion As New SqlConnection("Data Source=.;Initial Catalog=InvernAr;Integrated Security=True")
    '    Return Conexion
    'End Function

    Shared Function Conectar(Optional ByVal BBDD As String = "") As SqlConnection

        'Dim conString As String = ConfigurationManager.ConnectionStrings("RemisSQL").ConnectionString
        'Dim conStringMaster As String = ConfigurationManager.ConnectionStrings("RemisMasterSQL").ConnectionString

        'Dim ConexionMaster As SqlConnection = New SqlConnection(conStringMaster)
        'Dim Conexion As SqlConnection = New SqlConnection(conString)

        If BBDD = "master" Then
            Dim Conexion As New SqlConnection(ConfigurationManager.ConnectionStrings("Master").ConnectionString)
            Return Conexion
        Else
            Dim Conexion As New SqlConnection(ConfigurationManager.ConnectionStrings("InvernAr").ConnectionString)
            Return Conexion
        End If

    End Function


    Shared Function Escribir(ByVal SP As String, Optional HT As Hashtable = Nothing, Optional BBDD As String = "") As Integer
        Dim Comando As New SqlCommand
        Dim Retorno As Integer = 0
        Comando.Connection = Conectar(BBDD)
        Comando.CommandType = CommandType.StoredProcedure
        Comando.CommandText = SP
        If Not HT Is Nothing Then
            For i = 0 To HT.Count - 1
                Comando.Parameters.AddWithValue(CStr(HT.Keys(i)), IIf(HT.Values(i) Is Nothing, DBNull.Value, HT.Values(i)))
            Next
        End If
        If Comando.Connection.State = ConnectionState.Closed Then
            Comando.Connection.Open()
        End If

        Retorno = Comando.ExecuteNonQuery

        Comando.Connection.Close()
        Return Retorno
    End Function

    Shared Function Leer(ByVal SP As String, Optional HT As Hashtable = Nothing, Optional BBDD As String = "") As DataSet
        Dim DS As New DataSet
        Dim Comando As New SqlCommand
        Dim Adaptador As New SqlDataAdapter
        Adaptador.SelectCommand = Comando
        Comando.Connection = Conectar(BBDD)
        Comando.CommandType = CommandType.StoredProcedure
        Comando.CommandText = SP
        If Not HT Is Nothing Then
            For i = 0 To HT.Count - 1
                Comando.Parameters.AddWithValue(CStr(HT.Keys(i)), IIf(HT.Values(i) Is Nothing, DBNull.Value, HT.Values(i)))
            Next
        End If
        Adaptador.Fill(DS, "Tabla")
        Return DS
    End Function

End Class

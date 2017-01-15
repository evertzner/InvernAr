Public Class OpinionCompraENT

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vIdUsuario As Integer
    Public Property IdUsuario() As Integer
        Get
            Return vIdUsuario
        End Get
        Set(ByVal value As Integer)
            vIdUsuario = value
        End Set
    End Property

    Private vDificultad As String
    Public Property Dificultad() As String
        Get
            Return vDificultad
        End Get
        Set(ByVal value As String)
            vDificultad = value
        End Set
    End Property

    Private vDiseño As String
    Public Property Diseño() As String
        Get
            Return vDiseño
        End Get
        Set(ByVal value As String)
            vDiseño = value
        End Set
    End Property

    Private vRetorno As String
    Public Property Retorno() As String
        Get
            Return vRetorno
        End Get
        Set(ByVal value As String)
            vRetorno = value
        End Set
    End Property

End Class

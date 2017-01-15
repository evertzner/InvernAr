Public Class BackUpRestoreENT

    Private vLocalizacion As String
    Public Property Localizacion() As String
        Get
            Return vLocalizacion
        End Get
        Set(ByVal value As String)
            vLocalizacion = value
        End Set
    End Property

End Class

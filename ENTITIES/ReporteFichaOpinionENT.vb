Public Class ReporteFichaOpinionENT

    Private vOpinion As String
    Public Property Opinion() As String
        Get
            Return vOpinion
        End Get
        Set(ByVal value As String)
            vOpinion = value
        End Set
    End Property

    Private vCuenta As Integer
    Public Property Cuenta() As Integer
        Get
            Return vCuenta
        End Get
        Set(ByVal value As Integer)
            vCuenta = value
        End Set
    End Property

End Class

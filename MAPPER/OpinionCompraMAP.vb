Imports ENTITIES
Imports DAL

Public Class OpinionCompraMAP

    Shared Sub AgregarOpinionCompra(ByRef QueObjeto As Object)
        Dim OpinionCompra As New OpinionCompraENT
        OpinionCompra = DirectCast(QueObjeto, OpinionCompraENT)
        Dim HT As New Hashtable
        HT.Add("@pIdUsuario", OpinionCompra.IdUsuario)
        HT.Add("@pDificultad", OpinionCompra.Dificultad)
        HT.Add("@pDiseño", OpinionCompra.Diseño)
        HT.Add("@pRetorno", OpinionCompra.Retorno)
        Generico.Escribir("pAgregarOpinionCompra", HT)
    End Sub

End Class

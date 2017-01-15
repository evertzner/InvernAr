Imports ENTITIES
Imports DAL

Public Class BackUpRestoreMAP

    Shared Sub RealizarBackUp(ByRef QueObjeto As Object)
        Dim BackUp As New BackUpRestoreENT
        BackUp = DirectCast(QueObjeto, BackUpRestoreENT)
        Dim HT As New Hashtable
        HT.Add("@pLocalizacion", BackUp.Localizacion)
        Generico.Escribir("pBackUp", HT, "master")
    End Sub

    Shared Sub RealizarRestore(ByRef QueObjeto As Object)
        Dim Restore As New BackUpRestoreENT
        Restore = DirectCast(QueObjeto, BackUpRestoreENT)
        Dim HT As New Hashtable
        HT.Add("@pLocalizacion", Restore.Localizacion)
        Generico.Escribir("pRestore", HT, "master")
    End Sub

End Class

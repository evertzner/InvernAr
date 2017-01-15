Imports ENTITIES
Imports MAPPER

Public Class BackUpRestoreBLL

    Public Sub RealizarBackUp(ByRef QueObjeto As Object)
        BackUpRestoreMAP.RealizarBackUp(QueObjeto)
    End Sub

    Public Sub RealizarRestore(ByRef QueObjeto As Object)
        BackUpRestoreMAP.RealizarRestore(QueObjeto)
    End Sub

End Class

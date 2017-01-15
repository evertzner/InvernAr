Imports ENTITIES
Imports DAL

Public Class InstalacionDetalleMAP

    Shared Sub AgregarInstalacionDetalle(ByRef QueObjeto As Object)
        Dim InstalacionDetalle As New InstalacionDetalleENT
        InstalacionDetalle = DirectCast(QueObjeto, InstalacionDetalleENT)
        Dim HT As New Hashtable
        HT.Add("@pIdInstalacion", InstalacionDetalle.IdInstalacion)
        HT.Add("@pIdProducto", InstalacionDetalle.IdProducto)
        HT.Add("@pProducto", InstalacionDetalle.Producto)
        HT.Add("@pCantidad", InstalacionDetalle.Cantidad)
        Generico.Escribir("pAgregarInstalacionDetalle", HT)
    End Sub

    Shared Function ListarInstalacionesDetalle(ByRef QueObjeto As Object) As DataSet
         Dim InstalacionDetalle As New InstalacionDetalleENT
        InstalacionDetalle = DirectCast(QueObjeto, InstalacionDetalleENT)
        Dim HT As New Hashtable
        HT.Add("@pIdInstalacion", InstalacionDetalle.IdInstalacion)
        Return Generico.Leer("pListarInstalacionesDetalle", HT)
    End Function

    Shared Function EliminarInstalacionDetalle(ByRef QueObjeto As Object) As Integer
        Dim InstalacionDetalle As New InstalacionDetalleENT
        InstalacionDetalle = DirectCast(QueObjeto, InstalacionDetalleENT)
        Dim HT As New Hashtable
        HT.Add("@pId", InstalacionDetalle.Id)
        Return Generico.Escribir("pEliminarInstalacionDetalle", HT)
    End Function

    Shared Sub ModificarInstalacionDetalle(ByRef QueObjeto As Object)
        Dim InstalacionDetalle As New InstalacionDetalleENT
        InstalacionDetalle = DirectCast(QueObjeto, InstalacionDetalleENT)
        Dim HT As New Hashtable
        HT.Add("@pId", InstalacionDetalle.Id)
        HT.Add("@pCantidad", InstalacionDetalle.Cantidad)
        Generico.Escribir("pModificarInstalacionDetalle", HT)
    End Sub

End Class

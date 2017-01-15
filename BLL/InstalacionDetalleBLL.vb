Imports ENTITIES
Imports MAPPER

Public Class InstalacionDetalleBLL

    Public Sub AgregarInstalacionDetalle(ByRef QueObjeto As Object)
        InstalacionDetalleMAP.AgregarInstalacionDetalle(QueObjeto)
    End Sub

    Public Function ListarInstalacionesDetalle(ByRef QueObjeto As Object) As List(Of InstalacionDetalleENT)
        Dim ListaInstalacionesDetalle As New List(Of InstalacionDetalleENT)
        Dim lector As IDataReader = InstalacionDetalleMAP.ListarInstalacionesDetalle(QueObjeto).CreateDataReader
        Do While lector.Read()
            Dim InstalacionDetalleENT As New InstalacionDetalleENT
            With InstalacionDetalleENT
                .Id = Convert.ToInt32(lector("Id"))
                .IdInstalacion = Convert.ToInt32(lector("IdInstalacion"))
                .IdProducto = Convert.ToInt32(lector("IdProducto"))
                .Producto = Convert.ToString(lector("Producto"))
                .Cantidad = Convert.ToInt32(lector("Cantidad"))
            End With
            ListaInstalacionesDetalle.Add(InstalacionDetalleENT)
        Loop
        lector.Close()
        Return ListaInstalacionesDetalle
    End Function

    Public Sub ModificarInstalacionDetallte(ByRef QueObjeto As Object)
        InstalacionDetalleMAP.ModificarInstalacionDetalle(QueObjeto)
    End Sub

    Public Function EliminarInstalacionDetalle(ByRef QueObjeto As Object) As Integer
        Return InstalacionDetalleMAP.EliminarInstalacionDetalle(QueObjeto)
    End Function

End Class

Imports ENTITIES
Imports DAL

Public Class TipoProductoMAP

    Shared Function ListarTipoProducto() As DataSet
        Return Generico.Leer("pListarTipoProducto")
    End Function

End Class

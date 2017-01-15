Imports ENTITIES
Imports DAL

Public Class InstalacionMAP

    Shared Sub AgregarInstalacion(ByRef QueObjeto As Object)
        Dim Instalacion As New InstalacionENT
        Instalacion = DirectCast(QueObjeto, InstalacionENT)
        Dim HT As New Hashtable
        HT.Add("@pIdCliente", Instalacion.IdCliente)
        HT.Add("@pFechaDeSolicitud", Instalacion.FechaDeSolicitud)
        HT.Add("@pDatosDeContacto", Instalacion.DatosDeContacto)
        HT.Add("@pDomicilioDeInstalacion", Instalacion.DomicilioDeInstalacion)
        HT.Add("@pObservaciones", Instalacion.Observaciones)
        Generico.Escribir("pAgregarInstalacion", HT)
    End Sub

    Shared Sub RealizarInstalacion(ByRef QueObjeto As Object)
        Dim Instalacion As New InstalacionENT
        Instalacion = DirectCast(QueObjeto, InstalacionENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Instalacion.Id)
        HT.Add("@pFechaDeRealizacion", Instalacion.FechaDeRealizacion)
        Generico.Escribir("pRealizarInstalacion", HT)
    End Sub

    Shared Function ListarInstalaciones() As DataSet
        Return Generico.Leer("pListarInstalaciones")
    End Function

    Shared Function EliminarInstalacion(ByRef QueObjeto As Object) As Integer
        Dim Instalacion As New InstalacionENT
        Instalacion = DirectCast(QueObjeto, InstalacionENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Instalacion.Id)
        Return Generico.Escribir("pEliminarInstalacion", HT)
    End Function

    Shared Sub ModificarInstalacion(ByRef QueObjeto As Object)
        Dim Instalacion As New InstalacionENT
        Instalacion = DirectCast(QueObjeto, InstalacionENT)
        Dim HT As New Hashtable
        HT.Add("@pId", Instalacion.Id)
        HT.Add("@pDatosDeContacto", Instalacion.DatosDeContacto)
        HT.Add("@pDomicilioDeInstalacion", Instalacion.DomicilioDeInstalacion)
        HT.Add("@pObservaciones", Instalacion.Observaciones)
        Generico.Escribir("pModificarInstalacion", HT)
    End Sub

End Class

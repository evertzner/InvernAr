Imports System.Globalization
Imports DAL
Imports ENTITIES

Public Class MultidiomaMAP

    Shared Sub AgregarTraduccion(ByRef QueObjeto As Object)
        Dim Multidioma As New MultidiomaENT
        Multidioma = DirectCast(QueObjeto, MultidiomaENT)
        Dim HT As New Hashtable
        HT.Add("@pIdioma", Multidioma.Idioma.Name)
        HT.Add("@pEtiqueta", Multidioma.Etiqueta)
        If Multidioma.Traduccion = Nothing Then
            HT.Add("@pTraduccion", DBNull.Value)
        Else
            HT.Add("@pTraduccion", Multidioma.Traduccion)
        End If
        Generico.Escribir("pAgregarTraduccion", HT)
    End Sub

    Shared Sub ModificarTraduccion(ByRef QueObjeto As Object)
        Dim Multidioma As New MultidiomaENT
        Multidioma = DirectCast(QueObjeto, MultidiomaENT)
        Dim HT As New Hashtable
        HT.Add("@pIdioma", Multidioma.Idioma.Name)
        HT.Add("@pEtiqueta", Multidioma.Etiqueta)
         If Multidioma.Traduccion = Nothing Then
            HT.Add("@pTraduccion", DBNull.Value)
        Else
            HT.Add("@pTraduccion", Multidioma.Traduccion)
        End If
        Generico.Escribir("pModificarTraduccion", HT)
    End Sub

    Shared Sub EliminarTraduccion(ByRef QueObjeto As Object)
        Dim Multidioma As New MultidiomaENT
        Multidioma = DirectCast(QueObjeto, MultidiomaENT)
        Dim HT As New Hashtable
        HT.Add("@pIdioma", Multidioma.Idioma.Name)
        Generico.Escribir("pEliminarTraduccion", HT)
    End Sub

    Shared Function ListarTraduccion(ByRef QueObjeto As Object) As DataSet
        Dim Multidioma As New MultidiomaENT
        Multidioma = DirectCast(QueObjeto, MultidiomaENT)
        Dim HT As New Hashtable
        If Multidioma.Idioma.Name = Nothing Then
            HT.Add("@pIdioma", DBNull.Value)
        Else
            If Multidioma.Idioma.Parent.Name = "" Then
                HT.Add("@pIdioma", Multidioma.Idioma.Name)
            Else
                'HT.Add("@pIdioma", "en")
                HT.Add("@pIdioma", Multidioma.Idioma.Parent.Name)
            End If
        End If
        Return Generico.Leer("pListarTraduccion", HT)
    End Function

    Shared Function ListarIdiomas(ByVal Booleano As Boolean) As DataSet
        If Booleano = False Then
            Return Generico.Leer("pListarIdiomasNoTraducidos")
        Else
            Return Generico.Leer("pListarIdiomasTraducidos")
        End If
    End Function

End Class

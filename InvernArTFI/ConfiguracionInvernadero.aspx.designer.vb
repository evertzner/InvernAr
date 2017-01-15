'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class ConfiguracionInvernadero

    '''<summary>
    '''Control hiddenComponentes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hiddenComponentes As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control DescargarPresupuesto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DescargarPresupuesto As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control Lienzo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lienzo As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control Herramientas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Herramientas As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Propiedad Master.
    '''</summary>
    '''<remarks>
    '''Propiedad generada automáticamente.
    '''</remarks>
    Public Shadows ReadOnly Property Master() As InvernArTFI.Master
        Get
            Return CType(MyBase.Master, InvernArTFI.Master)
        End Get
    End Property
End Class

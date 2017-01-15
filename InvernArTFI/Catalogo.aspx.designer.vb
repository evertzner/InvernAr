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


Partial Public Class Catalogo

    '''<summary>
    '''Control hiddenCuenta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hiddenCuenta As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control hiddenProductos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hiddenProductos As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control divCatalogoControles.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents divCatalogoControles As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control lblCatalogoTipo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCatalogoTipo As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control ddlCatalogoTipo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlCatalogoTipo As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control lblOrdenarPor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblOrdenarPor As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control ddlOrdenarPor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlOrdenarPor As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control lblPrecioMayorA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPrecioMayorA As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control txtPrecioMayorA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrecioMayorA As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control lblPrecioMenorA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPrecioMenorA As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control txtPrecioMenorA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrecioMenorA As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control btnCatalogoFiltrar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnCatalogoFiltrar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnCatalogoComparar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnCatalogoComparar As Global.System.Web.UI.HtmlControls.HtmlButton

    '''<summary>
    '''Control revPrecioMayorA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents revPrecioMayorA As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control revPrecioMenorA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents revPrecioMenorA As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control UpdatePanel2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel2 As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control divCatalogoContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents divCatalogoContenedor As Global.System.Web.UI.HtmlControls.HtmlGenericControl

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

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Validacion.aspx.vb" Inherits="InvernArTFI.Validacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta http-equiv="REFRESH" content="5;Inicio.aspx" />
    <title>InvernAr</title>
    <link rel="stylesheet" href="Content/jumbotron.css" />
    <link href="Content/narrow-jumbotron.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <div class="header clearfix">
            <a class="navbar-brand" href="Inicio.aspx">
                <img alt="Brand" src="images/LogoWeb.png" /></a>
        </div>
        <div id="divValidacionControles" runat="server" class="jumbotron">
            <h1 id="lblValidacionTitulo" runat="server" class="display-3">Bienvenido/a</h1>
            <p id="txtValidacionMensaje" runat="server" class="lead">Su cuenta ha sido validada, usted será redireccionado para que pueda iniciar sesión, en caso de no ser redireccionado, pulse el botón de abajo.</p>
            <p><a class="btn btn-lg btn-success" href="Inicio.aspx" role="button" id="btnValidacionInicio" runat="server">Inicio</a></p>
        </div>
        <footer class="footer">
            <p>© Invernar 2016</p>
        </footer>
    </div>
</body>
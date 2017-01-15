<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Inicio.aspx.vb" Inherits="InvernArTFI.Inicio" MasterPageFile="~/Master.Master" %>

<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<link rel="stylesheet" href="/Content/inicio.css" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
	</head>
	<body>
		<div id="divInicioContenedor" class="container">
			<div class="InicioContenedor">
				<span id="InvernAr">InvernAr</span>
			</div>
			<div class="InvernaderosAutomatizadosTexto">
				<asp:Label runat="server" ID="lblInicioInvernaderosAutomatizados">Invernaderos Automatizados</asp:Label>
			</div>
		</div>

		<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
			<!-- Indicators -->
			<ol class="carousel-indicators">
				<li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
				<li data-target="#carousel-example-generic" data-slide-to="1"></li>
				<li data-target="#carousel-example-generic" data-slide-to="2"></li>
				<li data-target="#carousel-example-generic" data-slide-to="3"></li>
			</ol>

			<!-- Wrapper for slides -->
			<div class="carousel-inner" role="listbox">
				<div class="item active">
					<img src="images/Invernadero 01.jpg" alt="Invernadero">
				</div>
				<div class="item">
					<img src="images/Invernadero 02.jpg" alt="Invernadero">
				</div>
				<div class="item">
					<img src="images/Invernadero 03.jpg" alt="Invernadero">
				</div>
				<div class="item">
					<img src="images/Invernadero 04.jpg" alt="Invernadero">
				</div>
			</div>
			<!-- Controls -->
			<a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
				<span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
				<span class="sr-only">Previous</span>
			</a>
			<a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
				<span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
				<span class="sr-only">Next</span>
			</a>
		</div>

		<div id="contenido" class="container">
			<div id="NovedadesContainer" class="col-md-6">
				<div class="encabezado">
					<asp:Label runat="server" ID="NovedadesHeaderLbl">Novedades</asp:Label>
				</div>
				<p>
					<asp:Label runat="server" ID="NovedadesBodyLbl">La más actualizada información sobre nuestra participación en el mercado agrícola</asp:Label>
				</p>
				<a href="#" id="NovedadesBtn" class="btn btn-success" role="button" runat="server">Descubre las novedades</a>
			</div>
			<div id="NuestrosServiciosContainer" class="col-md-5">
				<div class="encabezado">
					<asp:Label runat="server" ID="NuestrosServiciosHeaderLbl">Catálogo</asp:Label>
				</div>
				<p>
					<asp:Label runat="server" ID="NuestrosServiciosBodyLbl">Entra a nuestro catalogo y encuentra lo que estás buscando.</asp:Label>
				</p>
				<a href="Catalogo.aspx" id="ServiciosBtn" class="btn btn-success" role="button" runat="server">Nuestro catálogo</a>
			</div>
		</div>
	</body>

	</html>
</asp:Content>

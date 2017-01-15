<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Noticias.aspx.vb" Inherits="InvernArTFI.Noticias" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
	</head>
	<body>
		<div id="divNoticiasControles" runat="server" class="container">
			<div id="divNoticiasContenedor" runat="server" class="vertical-center-row">
			</div>
			<hr />
			<br />
		</div>
	</body>
	</html>
</asp:Content>

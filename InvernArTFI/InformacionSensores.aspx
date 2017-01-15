<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InformacionSensores.aspx.vb" Inherits="InvernArTFI.InformacionSensores" MasterPageFile="~/Master.Master" %>

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
		<div id="divInformacionSensoresControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-12">
						<asp:Timer ID="Timer1" runat="server" Interval="1000"></asp:Timer>
						<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
							<ContentTemplate>
								<asp:GridView ID="gvSensores" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="20">
									<Columns>
										<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" ReadOnly="true" Visible="false" />
										<asp:BoundField DataField="IdInstalacion" HeaderText="IdInstalacion" SortExpression="IdInstalacion" ReadOnly="true" />
										<asp:BoundField DataField="IdProducto" HeaderText="IdProducto" SortExpression="IdProducto" ReadOnly="true" />
										<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" ReadOnly="true" />
										<asp:BoundField DataField="ValorSensado" HeaderText="ValorSensado" SortExpression="ValorSensado" ReadOnly="true" />
										<asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ReadOnly="true" />
										<asp:BoundField DataField="LimiteMinimoAlerta" HeaderText="LimiteMinimoAlerta" SortExpression="LimiteMinimoAlerta" ReadOnly="true" />
										<asp:BoundField DataField="LimiteMaximoAlerta" HeaderText="LimiteMaximoAlerta" SortExpression="LimiteMaximoAlerta" ReadOnly="true" />
									</Columns>
								</asp:GridView>
							</ContentTemplate>
							<Triggers>
								<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
							</Triggers>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>

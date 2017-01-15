<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Chat.aspx.vb" Inherits="InvernArTFI.Chat" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<link href="Content/chat.css" rel="stylesheet" />
		<title></title>
	</head>
	<body>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
			<ContentTemplate>
				<div id="divChatControles" runat="server" class="container">
					<div class="row">
						<div class="col-md-5">
							<div id="divChatChat" runat="server">
								<div class="panel panel-primary">
									<div class="panel-heading" id="accordion">
										<span class="glyphicon glyphicon-comment"></span>Chat
									</div>
									<div class="panel-collapse" id="collapseOne">
										<div class="panel-body">
											<ul id="ulChatVentana" runat="server" class="chat">
											</ul>
										</div>
										<div class="panel-footer">
											<div class="input-group">
												<input id="txtChatMensaje" type="text" runat="server" class="form-control input-sm" />
												<span class="input-group-btn">
													<asp:Button class="btn btn-warning btn-sm" ID="btnChatEnviar" runat="server" />
												</span>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div id="divChatCantidadNoLeidos" runat="server" class="scrolling-table-container">

								<asp:GridView ID="gvChatChats" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="10">
									<Columns>
										<asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario" Visible="False" />
										<asp:BoundField DataField="CorreoElectronico" HeaderText="Correo Electronico" SortExpression="CorreoElectronico">
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" />
										</asp:BoundField>
										<asp:BoundField DataField="NoLeido" HeaderText="NoLeido" SortExpression="NoLeido">
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" />
										</asp:BoundField>
										<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/15_Tick_16x16.png" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center" />
									</Columns>
								</asp:GridView>
							</div>
						</div>
					</div>
				</div>
			</ContentTemplate>
			<Triggers>
				<asp:AsyncPostBackTrigger ControlID="btnChatEnviar" EventName="Click" />
			</Triggers>
		</asp:UpdatePanel>
	</body>
	</html>
</asp:Content>

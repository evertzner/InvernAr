<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Sensores.aspx.vb" Inherits="InvernArTFI.Sensores" MasterPageFile="~/Master.Master" %>

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
		<div id="divSensoresControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-12">
						<asp:GridView ID="gvSensores" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="20">
							<Columns>
								<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
									<ItemStyle Wrap="False" />
								</asp:CommandField>
								<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" ReadOnly="true" Visible="false" />
								<asp:BoundField DataField="IdProducto" HeaderText="IdProducto" SortExpression="IdProducto" ReadOnly="true" />
								<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" ReadOnly="true" />
								<asp:TemplateField HeaderText="LimiteMinimoAlerta">
									<EditItemTemplate>
										<asp:TextBox ID="txtLimiteMinimoAlerta" runat="server" Text='<%# Bind("LimiteMinimoAlerta")%>'></asp:TextBox>
										<asp:RegularExpressionValidator ID="revLimiteMinimoAlerta" ErrorMessage="*" runat="server"
											ControlToValidate="txtLimiteMinimoAlerta"
											ValidationExpression="^([0-9]{1,5}(,?)[0-9]{0,2})$"
											ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
										<asp:RequiredFieldValidator ID="rfvLimiteMinimoAlerta" runat="server" ErrorMessage="*" ControlToValidate="txtLimiteMinimoAlerta"
											ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
									</EditItemTemplate>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# Bind("LimiteMinimoAlerta")%>' ID="lblLimiteMinimoAlerta"></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="LimiteMaximoAlerta">
									<EditItemTemplate>
										<asp:TextBox ID="txtLimiteMaximoAlerta" runat="server" Text='<%# Bind("LimiteMaximoAlerta")%>'></asp:TextBox>
										<asp:RegularExpressionValidator ID="revLimiteMaximoAlerta" ErrorMessage="*" runat="server"
											ControlToValidate="txtLimiteMaximoAlerta"
											ValidationExpression="^([0-9]{1,5}(,?)[0-9]{0,2})$"
											ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
										<asp:RequiredFieldValidator ID="rfvLimiteMaximoAlerta" runat="server" ErrorMessage="*" ControlToValidate="txtLimiteMaximoAlerta"
											ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
									</EditItemTemplate>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# Bind("LimiteMaximoAlerta")%>' ID="lblLimiteMaximoAlerta"></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>

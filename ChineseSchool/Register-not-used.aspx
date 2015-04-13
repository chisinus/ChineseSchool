<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register-not-used.aspx.cs" Inherits="ChineseSchool.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label runat="server" ID="ctrlMessage" CssClass="Error"></asp:Label>
    <div>
        <asp:Label runat="server">First Name: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlFirstname" MaxLength ="30" />
    </div>
    <div>
        <asp:Label ID="Label1" runat="server">Last Name: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlLastname" MaxLength ="30" />
    </div>
    <div>
        <asp:Label ID="Label2" runat="server">Email Address: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlEmail" MaxLength ="100" />
    </div>
    <div>
        <asp:Label ID="Label3" runat="server">Confirm Email Address: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlEmail2" MaxLength ="100" />
    </div>
    <div>
        <asp:Label ID="Label4" runat="server">Contact Phone: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlPhone" MaxLength ="30" />
        <asp:TextBox runat="server" ID="ctrlPhoneExt" MaxLength ="10" />
    </div>
    <div>
        <asp:Label ID="Label6" runat="server">Address1: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlAddress1" MaxLength ="100" />
    </div>
    <div>
        <asp:Label ID="Label5" runat="server">Address2: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlAddress2" MaxLength ="100" />
    </div>
    <div>
        <asp:Label ID="Label8" runat="server">City: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlCity" MaxLength ="30" />
    </div>
    <div>
        <asp:Label ID="Label9" runat="server">State: </asp:Label>
        <asp:DropDownList runat="server" ID="ctrlState" >
            <asp:ListItem Text="Select One" Value="0" />
            <asp:ListItem Text="New Jersey" Value="1" />
        </asp:DropDownList>
    </div>
    <div>
        <asp:Label ID="Label10" runat="server">Postal Code: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlPostalCode" MaxLength ="5" />
    </div>
    <div>
        <asp:Label ID="Label7" runat="server">Password: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlPassword" MaxLength ="20" />
    </div>
    <div>
        <asp:Label ID="Label11" runat="server">Confirm Password: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlPassword2" MaxLength ="20" />
    </div>
    <div>
        <asp:Button runat="server" ID="ctrlSubmit" OnClick="ctrlSubmit_Click" Text="Submit" />
        <asp:Button runat="server" ID="ctrlCancel" OnClick="ctrlCancel_Click" Text="Cancel" />
    </div>
    </form>
</body>
</html>

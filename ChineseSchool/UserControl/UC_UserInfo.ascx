<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_UserInfo.ascx.cs" Inherits="ChineseSchool.UserControl.UC_UserInfo" %>
<div class="SectionTitle">Your Information</div>
<div>
    <asp:Label ID="Label1" runat="server">First Name: </asp:Label>
    <asp:Label runat="server" ID="ctrlFirstname" />
</div>
<div>
    <asp:Label ID="Label2" runat="server">Last Name: </asp:Label>
    <asp:Label runat="server" ID="ctrlLastname" />
</div>
<div>
    <asp:Label ID="Label3" runat="server">Email Address: </asp:Label>
    <asp:Label runat="server" ID="ctrlEmail" />
</div>
<div>
    <asp:Label ID="Label5" runat="server">Contact Phone: </asp:Label>
    <asp:Label runat="server" ID="ctrlPhone" /> - <asp:Label runat="server" ID="ctrlPhoneExt" />
</div>
<div class="SectionTitle">Address Information</div>
<div>
    <asp:Label ID="Label6" runat="server">Address1: </asp:Label>
    <asp:Label runat="server" ID="ctrlAddress1" />
</div>
<div>
    <asp:Label ID="Label7" runat="server">Address2: </asp:Label>
    <asp:Label runat="server" ID="ctrlAddress2" />
</div>
<div>
    <asp:Label ID="Label8" runat="server">City: </asp:Label>
    <asp:Label runat="server" ID="ctrlCity" />
</div>
<div>
    <asp:Label ID="Label9" runat="server">State: </asp:Label>
    <asp:Label runat="server" ID="ctrlState" />
</div>
<div>
    <asp:Label ID="Label10" runat="server" >Postal Code: </asp:Label>
    <asp:Label runat="server" ID="ctrlPostalCode" />
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_UserInfo.ascx.cs" Inherits="ChineseSchool.UserControl.UC_UserInfo" %>
<div class="SectionTitle">Your Information</div>
<div>
    <p class="editor-label">First Name: </p>
    <asp:Label runat="server" ID="ctrlFirstname" CssClass="display-text" />
</div>
<div>
    <p class="editor-label">Last Name: </p> 
    <asp:Label runat="server" ID="ctrlLastname" CssClass="display-text" />
</div>
<div>
        <p class="editor-label">Email Address: </p>
    <asp:Label runat="server" ID="ctrlEmail" CssClass="display-text" />
</div>
<div>
    <p class="editor-label">Contact Phone: </p>
    <asp:Label runat="server" ID="ctrlPhone" CssClass="display-text" />
</div>
<div class="SectionTitle">Address Information</div>
<div>
    <p class="editor-label">Address1: </p>
    <asp:Label runat="server" ID="ctrlAddress1" CssClass="display-text" />
</div>
<div>
        <p class="editor-label">Address2: </p>
    <asp:Label runat="server" ID="ctrlAddress2" CssClass="display-text" />
</div>
<div>
    <p class="editor-label">City: </p>
    <asp:Label runat="server" ID="ctrlCity" CssClass="display-text" />
</div>
<div>
    <p class="editor-label">State: </p>
    <asp:Label runat="server" ID="ctrlState" CssClass="display-text" />
</div>
<div>
    <p class="editor-label">Postal Code: </p>
    <asp:Label runat="server" ID="ctrlPostalCode" CssClass="display-text" />
</div>

<asp:Panel ID="ctrlVolunteerPanel" runat="server">
    <div class="SectionTitle">Volunteer</div>
    <div style="margin-left:100px"><asp:PlaceHolder ID="ctrlVolunteerPH" runat="server" /></div>
</asp:Panel>


<%@ Page Title="" Language="C#" MasterPageFile="~/Includes/ChineseSchool.Master" AutoEventWireup="true" CodeBehind="Reg_Account.aspx.cs" Inherits="ChineseSchool.Reg_Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="ctrlMessage" CssClass="ErrorMessage"></asp:Label>
    <div class="PageTitle">Create Account</div>
    <div class="SectionTitle">Personal Information</div>
    <div>
        <asp:Label ID="Label1" runat="server">First Name: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlFirstname" MaxLength ="30" />
    </div>
    <div>
        <asp:Label ID="Label2" runat="server">Last Name: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlLastname" MaxLength ="30" />
    </div>
    <div>
        <asp:Label ID="Label3" runat="server">Email Address: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlEmail" MaxLength ="100" />
    </div>
    <div>
        <asp:Label ID="Label4" runat="server">Confirm Email Address: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlEmail2" MaxLength ="100" />
    </div>
    <div>
        <asp:Label ID="Label5" runat="server">Contact Phone: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlPhone" MaxLength ="30" />
        <asp:TextBox runat="server" ID="ctrlPhoneExt" MaxLength ="10" />
    </div>
    <div class="SectionTitle">Address Information</div>
    <div>
        <asp:Label ID="Label6" runat="server">Address1: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlAddress1" MaxLength ="100" />
    </div>
    <div>
        <asp:Label ID="Label7" runat="server">Address2: </asp:Label>
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
            <asp:ListItem Text="New York" Value="2" />
        </asp:DropDownList>
    </div>
    <div>
        <asp:Label ID="Label10" runat="server" >Postal Code: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlPostalCode" MaxLength ="5" />
    </div>
    <div class="SectionTitle">Login Information</div>
    <div>
        <asp:Label ID="Label11" runat="server">Password: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlPassword" MaxLength ="20" TextMode="Password" />
    </div>
    <div>
        <asp:Label ID="Label12" runat="server">Confirm Password: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlPassword2" MaxLength ="20" TextMode="Password" />
    </div>
    <div>
        <asp:Button runat="server" ID="ctrlSubmit" OnClick="ctrlSubmit_Click" Text="Next" />
        <asp:Button runat="server" ID="ctrlCancel" OnClick="ctrlCancel_Click" Text="Cancel" />
    </div>
</asp:Content>

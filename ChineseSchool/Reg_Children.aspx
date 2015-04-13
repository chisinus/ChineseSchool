<%@ Page Title="" Language="C#" MasterPageFile="~/Includes/ChineseSchool.Master" AutoEventWireup="true" CodeBehind="Reg_Children.aspx.cs" Inherits="ChineseSchool.Reg_Children" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="ctrlMessage" CssClass="Error"></asp:Label>
    <div class="PageTitle">Create Account</div>
    <div class="SectionTitle">Child Information</div>
    <div>
        <asp:Label ID="Label1" runat="server">Child First Name: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlFirstname" MaxLength ="30" />
    </div>
    <div>
        <asp:Label ID="Label2" runat="server">Child Last Name: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlLastname" MaxLength ="30" />
    </div>
    <div>
        <asp:Label ID="Label9" runat="server">Gender: </asp:Label>
        <asp:DropDownList runat="server" ID="ctrlGender" >
            <asp:ListItem Text="Select One" Value="0" />
            <asp:ListItem Text="Boy" Value="1" />
            <asp:ListItem Text="Girl" Value="2" />
        </asp:DropDownList>
    </div>
    <div>
        <asp:Label ID="Label4" runat="server">Year of Birth: </asp:Label>
        <asp:TextBox runat="server" ID="ctrlYOB" MaxLength ="4" />
    </div>    
    <div>
        <asp:Label ID="Label3" runat="server">Class/Grade: </asp:Label>
        <asp:DropDownList runat="server" ID="ctrlClass" />
    </div>
    <div>
        <asp:Button runat="server" ID="ctrlSubmit" OnClick="ctrlSubmit_Click" Text="Submit" />
        <asp:Button runat="server" ID="ctrlCancel" OnClick="ctrlCancel_Click" Text="Cancel" />
    </div>
</asp:Content>

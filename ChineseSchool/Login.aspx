<%@ Page Title="" Language="C#" MasterPageFile="~/Includes/ChineseSchool.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ChineseSchool.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label runat="server" ID="ctrlMessage" CssClass="ErrorMessage" />
        <br /><br />
        Email Address: <asp:TextBox runat="server" ID="ctrlEmail" />
        <br /><br />
        Password: <asp:TextBox runat="server" ID="ctrlPassword" TextMode="Password" />
        <br /><br />
        <asp:Button runat="server" ID="ctrlLogin" OnClick="ctrlLogin_Click" Text="Log in" />
        <asp:Button runat="server" ID="ctrlRegister" OnClick="ctrlRegister_Click" Text="Register" />
        <asp:Button runat="server" ID="ctrlExport" OnClick="ctrlExport_Click" Text="Export" />
    </div>
</asp:Content>
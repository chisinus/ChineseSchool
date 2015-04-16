<%@ Page Title="" Language="C#" MasterPageFile="~/Includes/ChineseSchool.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ChineseSchool.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="ctrlMessage" CssClass="ErrorMessage" />
    <br /><br />
    <p class="editor-label">Email Address: </p> <asp:TextBox runat="server" ID="ctrlEmail" CssClass="editor-field" />
    <br />
    <p class="editor-label">Password: </p><asp:TextBox runat="server" ID="ctrlPassword" TextMode="Password" CssClass="editor-field" />
    <br /><br /><br />
    <asp:Button runat="server" ID="ctrlLogin" OnClick="ctrlLogin_Click" Text="Log in" style="margin-right:20px" />
    <asp:Button runat="server" ID="ctrlRegister" OnClick="ctrlRegister_Click" Text="Register" style="float:right; margin-right:400px" />
</asp:Content>
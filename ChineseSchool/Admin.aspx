<%@ Page Title="" Language="C#" MasterPageFile="~/Includes/ChineseSchool.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ChineseSchool.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button runat="server" ID="ctrlExport" OnClick="ctrlExport_Click" Text="Export" />
    <br />
    <asp:Button runat="server" ID="ctrlLogout" OnClick="ctrlLogout_Click" Text="Logout" />
</asp:Content>

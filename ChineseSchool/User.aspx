<%@ Page Title="" Language="C#" MasterPageFile="~/Includes/ChineseSchool.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="ChineseSchool.User" %>
<%@ Register Src="~/UserControl/UC_UserInfo.ascx" TagPrefix="uc1" TagName="UC_UserInfo" %>
<%@ Register Src="~/UserControl/UC_ChildList.ascx" TagPrefix="uc2" TagName="UC_ChildList" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button runat="server" ID="ctrlCancel" OnClick="ctrlEdit_Click" Text="Edit" CssClass="button" />
    <asp:Button runat="server" ID="Button1" OnClick="ctrlLogout_Click" Text="Logout" CssClass="button" />

    <uc1:UC_UserInfo runat="server" id="ctrlUserInfo" />
    <uc2:UC_ChildList runat="server" id="ctrlChildren" />
</asp:Content>

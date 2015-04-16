<%@ Page Title="" Language="C#" MasterPageFile="~/Includes/ChineseSchool.Master" AutoEventWireup="true" CodeBehind="Reg_Children.aspx.cs" Inherits="ChineseSchool.Reg_Children" %>

<%@ Register Src="~/UserControl/UC_ChildList.ascx" TagPrefix="uc1" TagName="UC_ChildList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">Create Account - Add Children</div>
    
    <uc1:UC_ChildList runat="server" ID="ctrlChildren" />

    <div class="SectionTitle">Child Information</div>
    <asp:Label runat="server" ID="ctrlMessage" CssClass="Error"></asp:Label>
    <div class="required">
        <p class="editor-label">Child First Name: </p>
        <asp:TextBox runat="server" ID="ctrlFirstname" MaxLength ="30" CssClass="editor-field" />
    </div>
    <div class="required">
        <p class="editor-label">Child Last Name: </p>
        <asp:TextBox runat="server" ID="ctrlLastname" MaxLength ="30" CssClass="editor-field" />
    </div>
    <div>
        <p class="editor-label">Gender: </p>
        <asp:DropDownList runat="server" ID="ctrlGender"  CssClass="editor-field" >
            <asp:ListItem Text="Select One" Value="0" />
            <asp:ListItem Text="Boy" Value="1" />
            <asp:ListItem Text="Girl" Value="2" />
        </asp:DropDownList>
    </div>
    <div>
        <p class="editor-label">Year of Birth: </p>
        <asp:TextBox runat="server" ID="ctrlYOB" MaxLength ="4" CssClass="editor-field" />
    </div>    
    <div class="required">
        <p class="editor-label">Class/Grade: </p>
        <asp:DropDownList runat="server" ID="ctrlClass" CssClass="editor-field" />
    </div>
    <asp:Button runat="server" ID="ctrlAdd" OnClick="ctrlAdd_Click" Text="Add" style="margin-right:400px; float:right; margin-top:2em;" CssClass="button" />
    <div style="margin-top:2em" class="required">
        <p> Required field</p>
    </div>
    <div style="margin-top:3em;">
        <asp:Button runat="server" ID="ctrlCancel" OnClick="ctrlCancel_Click" Text="Cancel" CssClass="button" />
        <asp:Button runat="server" ID="ctrlSubmit" OnClick="ctrlSubmit_Click" Text="Finish" CssClass="button" style="margin-right:400px;float:right" />
    </div>
</asp:Content>

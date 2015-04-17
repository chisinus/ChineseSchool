<%@ Page Title="" Language="C#" MasterPageFile="~/Includes/ChineseSchool.Master" AutoEventWireup="true" CodeBehind="Reg_Other.aspx.cs" Inherits="ChineseSchool.Reg_Other" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="SectionTitle">Volunteer</div>
    <div>
        <p class="editor-label">We would like to volunteer:</p>
        <asp:CheckBox runat="server" ID="ctrlAdmin" Text="Administrator"  />
        <asp:CheckBox runat="server" ID="CheckBox1" Text="Class Parent" />
        <asp:CheckBox runat="server" ID="CheckBox2" Text="Others" />
    </div>

    <div class="SectionTitle">Emergency Contact</div>
    <div class="required">
        <p class="editor-label">Name: </p>
        <asp:TextBox runat="server" ID="ctrlECName" MaxLength ="100" CssClass="editor-field" />
    </div>
    <div class="required">
        <p class="editor-label">Phone: </p>
        <asp:TextBox runat="server" ID="ctrlECPhone" MaxLength ="20" CssClass="editor-field" />
    </div>
    <div style="margin-top:2em" class="required">
        <p> Required field</p>
    </div>
    <div style="margin-top:2em">
        <asp:Button runat="server" ID="ctrlCancel" OnClick="ctrlCancel_Click" Text="Cancel" CssClass="button" />
        <asp:Button runat="server" ID="ctrlSubmit" OnClick="ctrlSubmit_Click" Text="Next" style="float:right;margin-right:170px" CssClass="button" />
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Includes/ChineseSchool.Master" AutoEventWireup="true" CodeBehind="Reg_Account.aspx.cs" Inherits="ChineseSchool.Reg_Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="ctrlMessage" CssClass="ErrorMessage"></asp:Label>
    <div class="PageTitle">Create Account</div>
    <div class="SectionTitle">Personal Information</div>
    <div class="required">
        <p class="editor-label">First Name: </p>
        <asp:TextBox runat="server" ID="ctrlFirstname" MaxLength ="30" CssClass="editor-field" />
    </div>
    <div class="required">
        <p class="editor-label">Last Name: </p>
        <asp:TextBox runat="server" ID="ctrlLastname" MaxLength ="30" CssClass="editor-field" />
    </div>
    <div class="required">
        <p class="editor-label">Email Address: </p>
        <asp:TextBox runat="server" ID="ctrlEmail" MaxLength ="100" CssClass="editor-field" />
    </div>
    <div class="required">
        <p class="editor-label">Confirm Email Address: </p>
        <asp:TextBox runat="server" ID="ctrlEmail2" MaxLength ="100" CssClass="editor-field" />
    </div>
    <div>
        <p class="editor-label">Contact Phone: </p>
        <asp:TextBox runat="server" ID="ctrlPhone" MaxLength ="30" CssClass="editor-field" />
        <asp:TextBox runat="server" ID="ctrlPhoneExt" MaxLength ="10" CssClass="editor-field" />
    </div>
    <div class="SectionTitle">Address Information</div>
    <div>
        <p class="editor-label">Address1: </p>
        <asp:TextBox runat="server" ID="ctrlAddress1" MaxLength ="100" CssClass="editor-field" Width="400px" />
    </div>
    <div>
        <p class="editor-label">Address2: </p>
        <asp:TextBox runat="server" ID="ctrlAddress2" MaxLength ="100" CssClass="editor-field" Width="400px" />
    </div>
    <div>
        <p class="editor-label">City: </p>
        <asp:TextBox runat="server" ID="ctrlCity" MaxLength ="30" CssClass="editor-field" />
    </div>
    <div>
        <p class="editor-label">State: </p>
        <asp:DropDownList runat="server" ID="ctrlState"  CssClass="editor-field">
            <asp:ListItem Text="Select One" Value="0" />
            <asp:ListItem Text="New Jersey" Value="1" />
            <asp:ListItem Text="New York" Value="2" />
        </asp:DropDownList>
    </div>
    <div>
        <p class="editor-label">Postal Code: </p>
        <asp:TextBox runat="server" ID="ctrlPostalCode" MaxLength ="5" CssClass="editor-field" />
    </div>
    <div class="SectionTitle">Login Information</div>
    <div class="required">
        <p class="editor-label required">Password: </p>
        <asp:TextBox runat="server" ID="ctrlPassword" MaxLength ="20"  TextMode="Password" CssClass="editor-field" />
    </div>
    <div class="required">
        <p class="editor-label">Confirm Password: </p>
        <asp:TextBox runat="server" ID="ctrlPassword2" MaxLength ="20"  TextMode="Password" CssClass="editor-field" />
    </div>
    <div style="margin-top:2em" class="required">
        <p> Required field</p>
    </div>
    <div style="margin-top:2em">
        <asp:Button runat="server" ID="ctrlCancel" OnClick="ctrlCancel_Click" Text="Cancel" CssClass="button" />
        <asp:Button runat="server" ID="ctrlSubmit" OnClick="ctrlSubmit_Click" Text="Next" style="float:right;margin-right:170px" CssClass="button" />
    </div>
    
</asp:Content>

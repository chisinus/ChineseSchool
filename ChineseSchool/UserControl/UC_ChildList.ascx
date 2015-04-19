<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_ChildList.ascx.cs" Inherits="ChineseSchool.UserControl.UC_ChildList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %> 
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>

    <link href="../Styles/Skins/Telerik/Grid.css" rel="stylesheet" />
    <div class="SectionTitle">Children</div>
    <ajax:ToolkitScriptManager ID="ScriptManager" runat="server"></ajax:ToolkitScriptManager>        
    <rad:RadGrid 
        id="ctrlGrid" 
        runat="server" 
        width="700px"
        AutoGenerateColumns="False" 
        GridLines="None" 
        AllowMultiRowSelection="True" 
        AllowPaging="True" 
        AllowSorting="True" 
        PageSize="20"
        EnableEmbeddedSkins="false" 
        Skin="WebBlue"
        AllowFilteringByColumn="false"
        EnableLinqExpressions="false"
        OnItemDataBound="ctrlGrid_ItemDataBound"
        OnDeleteCommand="ctrlGrid_DeleteCommand">
        <ClientSettings>
            <Selecting AllowRowSelect="False" EnableDragToSelectRows="False" />
        </ClientSettings>
        <GroupingSettings CaseSensitive="false"/>
        <mastertableview AllowNaturalSort="False" NoMasterRecordsText="No child added" ClientDataKeyNames="ChildID">
            <Columns>                
                <rad:GridBoundColumn UniqueName="ChildID" DataField="ChildID" HeaderText="Last" HtmlEncode="true" ShowFilterIcon="false" Display="false" >
                    <HeaderStyle Width="1px" />
                </rad:GridBoundColumn>
                <rad:GridBoundColumn UniqueName="ChildLastname" DataField="ChildLastname" HeaderText="Last" HtmlEncode="true" ShowFilterIcon="false" >
                    <HeaderStyle Width="100px" />
                </rad:GridBoundColumn>
                <rad:GridBoundColumn UniqueName="ChildFirstname" DataField="ChildFirstname" HeaderText="First" HtmlEncode="true" ShowFilterIcon="false" >
                    <HeaderStyle Width="100px" />
                </rad:GridBoundColumn>
                <rad:GridBoundColumn UniqueName="Gender" DataField="Gender" HeaderText="Gender" HtmlEncode="true" ShowFilterIcon="false" >
                    <HeaderStyle Width="50px" />
                </rad:GridBoundColumn>
                <rad:GridBoundColumn UniqueName="YOB" DataField="YOB" HeaderText="Year of Birth" HtmlEncode="true" ShowFilterIcon="false" >
                    <HeaderStyle Width="100px" />
                </rad:GridBoundColumn>
                <rad:GridBoundColumn UniqueName="GradeName" DataField="GradeName" HeaderText="Grade" HtmlEncode="true" ShowFilterIcon="false" >
                    <HeaderStyle Width="100px" />
                </rad:GridBoundColumn>
                <rad:GridBoundColumn UniqueName="TeacherName" DataField="TeacherName" HeaderText="Teacher" HtmlEncode="true" ShowFilterIcon="false" >
                    <HeaderStyle Width="200px" />
                </rad:GridBoundColumn>
                <rad:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn" >
                </rad:GridButtonColumn>
            </Columns>
        </mastertableview>
    </rad:RadGrid>

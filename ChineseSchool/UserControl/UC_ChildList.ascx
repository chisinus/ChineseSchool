<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_ChildList.ascx.cs" Inherits="ChineseSchool.UserControl.UC_ChildList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %> 
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>

    <div class="SectionTitle">Children</div>
    <ajax:ToolkitScriptManager ID="ScriptManager" runat="server"></ajax:ToolkitScriptManager>        
    <rad:RadGrid 
        id="ctrlGrid" 
        runat="server" 
        width="700px"
        AutoGenerateColumns="False" 
        GridLines="None" 
        EnableAJAX="True" 
        AllowMultiRowSelection="True" 
        AllowPaging="True" 
        AllowSorting="True" 
        PageSize="20" RadControlsDir="/"
        EnableEmbeddedSkins="false" 
        Skin="WebBlue"
        AllowFilteringByColumn="false"
        EnableLinqExpressions="false">
        <ClientSettings>
            <Selecting AllowRowSelect="False" EnableDragToSelectRows="False" />
        </ClientSettings>
        <GroupingSettings CaseSensitive="false"/>
        <mastertableview AllowNaturalSort="False">
            <Columns>                
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
                <rad:GridBoundColumn UniqueName="ClassName" DataField="ClassName" HeaderText="Class" HtmlEncode="true" ShowFilterIcon="false" >
                    <HeaderStyle Width="100px" />
                </rad:GridBoundColumn>
            </Columns>
        </mastertableview>
    </rad:RadGrid>

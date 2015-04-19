<%@ Page Title="" Language="C#" MasterPageFile="~/Includes/ChineseSchool.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Reg_Children.aspx.cs" Inherits="ChineseSchool.Reg_Children" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %> 
<%@ Register Src="~/UserControl/UC_ChildList.ascx" TagPrefix="uc1" TagName="UC_ChildList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function MedicineQuestionCheckboxChanged(id, checked)
        {
            var ctrl = document.getElementById(id + "Info");
            if (checked) {
                ctrl.style.display = "inline";
            }
            else {
                ctrl.style.display = "none";
                ctrl.value = "";
            }
        }
    </script>
    <div class="PageTitle">Create Account - Add Children</div>
    
    <uc1:UC_ChildList runat="server" ID="ctrlChildren" />

    <div class="SectionTitle">Child Information</div>
    <asp:Label runat="server" ID="ctrlMessage" CssClass="ErrorMessage"></asp:Label>
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
        <p class="editor-label">Grade: </p>
        <asp:DropDownList ID="ctrlGrades" runat="server" CssClass="editor-field" />
        <ajax:CascadingDropDown ID="ctrlGradesCDD" runat="server"
            TargetControlID ="ctrlGrades" 
            PromptText="Please select a grade" 
            LoadingText="Loading grades..."
            ServicePath="CSService.asmx"
            ServiceMethod="GetGrades"
            Category ="Grade" />
    </div>
    <div class="required">
        <p class="editor-label">Teacher: </p>
        <asp:DropDownList ID="ctrlTeachers" runat="server"  CssClass="editor-field" />
        <ajax:CascadingDropDown ID="ctrlTeachersCDD" runat="server"
            ParentControlID="ctrlGrades"
            TargetControlID ="ctrlTeachers" 
            PromptText="Please select a grade" 
            LoadingText="Loading grades..."
            ServicePath="CSService.asmx"
            ServiceMethod="GetTeachersByGrade"
            Category ="Teacher" />
    </div>
    <div class="SectionTitle">Other</div>
    <asp:Label runat="server" ID="ctrlMessage2" CssClass="ErrorMessage"></asp:Label>
    <div>
        <p>1. Has your child had any serious illness, injury, operation or communicable disease since September of last year? Specify please,if yes. </p>
        <div>
            <asp:CheckBox runat="server" ID="ctrlSeriousIllness" CssClass="editor-field" ClientIDMode="Static" OnClick="MedicineQuestionCheckboxChanged(this.id, this.checked);"/>
            <asp:TextBox runat="server" ID="ctrlSeriousIllnessInfo" MaxLength ="250" CssClass="long-field" ClientIDMode="Static" />
        </div>
    </div>
    <div>
        <p>2. Are there any conditions such as Heart Disease, Epilepsy, Diabetes, Asthma or any allergies that the school should be aware of? Specify please, if yes.</p>
        <div>
            <asp:CheckBox runat="server" ID="ctrlHeartDisease" CssClass="editor-field" ClientIDMode="Static" OnClick="MedicineQuestionCheckboxChanged(this.id, this.checked);" />
            <asp:TextBox runat="server" ID="ctrlHeartDiseaseInfo" MaxLength ="250" ClientIDMode="Static" CssClass="long-field" />
        </div>
    </div>
    <div>
        <p>3. Excluding vitamins, does your child take any medication on a regular basis? Specify please, if yes.</p>
        <div>
            <asp:CheckBox runat="server" ID="ctrlMedicine" CssClass="editor-field" ClientIDMode="Static" OnClick="MedicineQuestionCheckboxChanged(this.id, this.checked);"/>
            <asp:TextBox runat="server" ID="ctrlMedicineInfo" MaxLength ="250" CssClass="long-field" ClientIDMode="Static" />
        </div>
    </div>
    <asp:Button runat="server" ID="ctrlAdd" OnClick="ctrlAdd_Click" Text="Add" style="margin-right:0px; float:right; margin-top:2em;" CssClass="button" />
    <div style="margin-top:2em" class="required">
        <p> Required field</p>
    </div>

    <div class="SectionTitle">Tuition and Refund Policy</div>
    Tuition is $450, books $30, registration fee $20.  Registration fee is waived before June 16. In addition, there is a parent-on-duty deposit $50 per student. It is refundable when parents fulfill their duty.  Each additional sibling gets 10% discount off the tuition.
    Cancellation request received by 7/31/2013 (on site or email to lisoc2003@yahoo.com only) will be refunded the amount paid minus a $20 administrative fee for each student.  Cancellation received within the first 4 weeks of school year will be refunded $300 ($270 for siblings). No refund will be issued for any cancellation after 4 weeks of the start of school year.”

    <div class="SectionTitle">Acknowledge</div>
    My child and I have read and agree that we will follow the Code of Conducts of LISOC, and the other school regulations. School website www.lisoc.org provides with the full contents.
    <br /><br />
    
    <asp:Label runat="server" ID="ctrlMessage3" CssClass="ErrorMessage"></asp:Label>
    <asp:CheckBox runat="server" ID="ctrlAck_Rule" /> <span style="color : red">*</span> I agree to obey all LISOC Rules, Policy and Safety guidelines.
    <asp:CheckBox runat="server" ID="ctrlAck_Medical" /> I allow LISOC to release my kids’ medical information when necessary
    <asp:CheckBox runat="server" ID="ctrlAck_Publish" /> I agree LISOC to publish/use my kids’ photo or media for education purpose
    
    <div style="margin-top:3em;">
        <asp:Button runat="server" ID="ctrlCancel" OnClick="ctrlCancel_Click" Text="Cancel" CssClass="button" />
        <asp:Button runat="server" ID="ctrlSubmit" OnClick="ctrlSubmit_Click" Text="Finish" CssClass="button" style="margin-right:0px;float:right" />
    </div>
</asp:Content>

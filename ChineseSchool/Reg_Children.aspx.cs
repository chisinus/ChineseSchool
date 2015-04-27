using ChineseSchool.BusinessLogic;
using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChineseSchool
{
    public partial class Reg_Children : CSPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateOtherSection();

            if (IsPostBack) return;

            UpdateCommentsAndAcknowledgeSection();

            SetChildren(GetCurrentUser().Children);

            ctrlChildren.UpdateUI(GetCurrentUser().Children, true);
        }

        private void UpdateCommentsAndAcknowledgeSection()
        {
            UserData user = GetCurrentUser();
            ctrlComments.Text = user.Comments;

            ctrlAck_Rule.Checked = user.Ack_Rule;
            ctrlAck_Medical.Checked = user.Ack_Medical;
            ctrlAck_Publish.Checked = user.Ack_Publish;

            if (GetEditMode() == CSConstants.EditMode.Edit)
                ctrlAck_RuleSection.Style.Add("display", "none");
        }

        private void UpdateOtherSection()
        {
            ctrlSeriousIllnessInfo.Style.Add("display", ctrlSeriousIllness.Checked?"inline":"none");
            ctrlHeartDiseaseInfo.Style.Add("display", ctrlHeartDisease.Checked?"inline":"none");
            ctrlMedicineInfo.Style.Add("display", ctrlMedicine.Checked?"inline":"none");
        }

        protected void ctrlAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateChildData())
                return;

            ChildData child = new ChildData();

            child.ChildFirstname = ctrlFirstname.Text.Trim();
            child.ChildLastname = ctrlLastname.Text.Trim();
            child.Gender = (CSConstants.Genders)Toolbox.StringToInt(ctrlGender.SelectedValue);
            child.YOB = ctrlYOB.Text.Trim();
            child.PickedClasses.Add(CSAgent.GetClassInfoByGradeAndTeacher(Toolbox.StringToInt(ctrlGrades.SelectedValue), Toolbox.StringToInt(ctrlTeachers.SelectedValue), GetSqlConnection()));
            child.SeriousIllness = ctrlSeriousIllnessInfo.Text.Trim();
            child.HeartDisease = ctrlHeartDiseaseInfo.Text.Trim();
            child.Medicine = ctrlMedicineInfo.Text.Trim();

            List<ChildData> children = GetChildren();
            int id = -1;
            foreach (ChildData c in children)
            {
                if (c.ChildID > id) 
                    id = c.ChildID;
            }

            child.ChildID = id + 1;

            children.Add(child);
            SetChildren(children);

            ctrlChildren.UpdateUI(children, true);

            ClearForm();
        }

        private void ClearForm()
        {
            ctrlMessage.Text = "";
            ctrlFirstname.Text = "";
            ctrlLastname.Text = "";
            ctrlGender.SelectedIndex = 0;
            ctrlYOB.Text = "";
            ctrlGradesCDD.SelectedValue = "0";
            ctrlMedicine.Checked = false;
            ctrlMedicineInfo.Text = "";
            ctrlSeriousIllness.Checked = false;
            ctrlSeriousIllnessInfo.Text = "";
            ctrlHeartDisease.Checked = false;
            ctrlHeartDiseaseInfo.Text = "";

            UpdateOtherSection();
        }

        private bool ValidateChildData()
        {
            if (Toolbox.IsEmpty(ctrlFirstname.Text) || 
                Toolbox.IsEmpty(ctrlLastname.Text) || 
                (Toolbox.StringToInt(ctrlGrades.SelectedValue) <= 0) ||
                (Toolbox.StringToInt(ctrlTeachers.SelectedValue) <= 0))
            {
                ctrlMessage.Text = CSMessage.ERR_RequiredField;
                return false;
            }

            if (!Toolbox.IsEmpty(ctrlYOB.Text))
            {
                int yob = Toolbox.StringToInt(ctrlYOB.Text.Trim());
                if ((yob < 0) || (yob > DateTime.Now.Year))
                {
                    ctrlMessage.Text = "Please enter a valid Year of Birth.";
                    return false;
                }
            }

            if ((ctrlSeriousIllness.Checked && Toolbox.IsEmpty(ctrlSeriousIllnessInfo.Text)) ||
                (ctrlHeartDisease.Checked && Toolbox.IsEmpty(ctrlHeartDiseaseInfo.Text)) ||
                (ctrlMedicine.Checked && Toolbox.IsEmpty(ctrlMedicineInfo.Text)))
            {
                ctrlMessage2.Text = CSMessage.ERR_RequiredField;
                return false;
            }

            return true;
        }

        protected void ctrlSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateFormData()) return;

            UserData user = GetCurrentUser();
            
            string comments = ctrlComments.Text.Trim();
            user.Comments = Toolbox.IsEmpty(comments) ? "" : comments.Substring(0, (comments.Length > 2000) ? 2000 : comments.Length);

            SqlTransaction tran = GetSqlConnection().BeginTransaction();

            if (GetEditMode() == CSConstants.EditMode.New)
            {
                user.UserID = CSAgent.InsertUser(user, GetSqlConnection(), tran);
                if (user.UserID < 0)
                {
                    Toolbox.EndTransaction(tran, false);
                    ctrlMessage.Text = CSMessage.ERR_CompleteRequest;
                    return;
                }
            }

            if (GetEditMode() == CSConstants.EditMode.Edit && 
                !CSAgent.DeleteChildren(user.UserID, GetSqlConnection(), tran))
            {
                ctrlMessage.Text = CSMessage.ERR_CompleteRequest;
                Toolbox.EndTransaction(tran, false);
                return;
            }

            bool result = true;
            List<ChildData> children = GetChildren();
            foreach (ChildData child in children)
            {
                child.ChildID = CSAgent.InsertChild(user.UserID, child, GetSqlConnection(), tran);
                if (child.ChildID < 0)
                {
                    result = false;
                    break;
                }

                if (CSAgent.InsertChildClass(child.ChildID, child.PickedClasses[0].ClassID, GetSqlConnection(), tran) < 0)
                {
                    result = false;
                    break;
                }
            }

            result = result &&
                        CSAgent.UpdateCommentsAndUserAcknowledges(user.UserID, user.Comments, ctrlAck_Rule.Checked, ctrlAck_Medical.Checked, ctrlAck_Publish.Checked, GetSqlConnection(), tran);

            Toolbox.EndTransaction(tran, result);

            if (!result)
            {
                ctrlMessage.Text = CSMessage.ERR_CompleteRequest;
                return;
            }

            SetCurrentUser(CSAgent.GetUserByEmail(GetCurrentUser().Email, GetSqlConnection()));

            if (GetEditMode() == CSConstants.EditMode.New)
                Response.Redirect("Reg_Confirmation.aspx");
            else
                Response.Redirect("User.aspx");
        }

        private bool ValidateFormData()
        {
            if (!ctrlAck_Rule.Checked)
            {
                ctrlMessage3.Text = "Please accept the terms of this agreement.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "window.scrollTo(0, document.body.scrollHeight+30);", true);
                return false;
            }

            return true;
        }

        protected void ctrlCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
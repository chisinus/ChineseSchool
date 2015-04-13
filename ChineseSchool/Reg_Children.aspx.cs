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
            if (IsPostBack) return;

            PopulateGrade();
        }

        private void PopulateGrade()
        {
            List<ClassData> classes = CSAgent.GetClassList(GetSqlConnection());
            if (classes == null) return;

            ctrlClass.Items.Clear();
            ctrlClass.Items.Add(new ListItem("Select One", "0"));
            foreach (ClassData myClass in classes)
            {
                ctrlClass.Items.Add(new ListItem(myClass.ClassName + " / " + myClass.GradeName, myClass.ClassID.ToString()));
            }
        }

        protected void ctrlSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            ChildData child = new ChildData();

            child.ChildFirstname = ctrlFirstname.Text.Trim();
            child.ChildLastname = ctrlLastname.Text.Trim();
            child.Gender = (CSConstants.Genders)Toolbox.StringToInt(ctrlGender.SelectedValue);
            child.YOB = Toolbox.StringToInt(ctrlYOB.Text.Trim());

            UserData user = GetCurrentUser();

            SqlTransaction tran = GetSqlConnection().BeginTransaction();

            child.ChildID = CSAgent.InsertChild(user.UserID, child, GetSqlConnection(), tran);
            bool result = (child.ChildID > 0);
            
            if (result)
            {
                int childClassID = CSAgent.InsertChildClass(child.ChildID, Toolbox.StringToInt(ctrlClass.SelectedValue), GetSqlConnection(), tran);
                result = childClassID > 0;

                if (result)
                {
                    ClassData myClass = new ClassData();
                    myClass.ClassID = childClassID;
                    child.PickedClasses.Add(myClass);
                }
            }

            Toolbox.EndTransaction(tran, result);

            if (!result)
            {
                ctrlMessage.Text = CSMessage.ERR_CompleteRequest;
                return;
            }

            user.Children.Add(child);
            SetCurrentUser(user);

            Response.Redirect("Reg_Confirmation.aspx");
        }

        private bool ValidateData()
        {
            if (Toolbox.IsEmpty(ctrlFirstname.Text) || Toolbox.IsEmpty(ctrlLastname.Text) || (ctrlClass.SelectedIndex == 0))
            {
                ctrlMessage.Text = CSMessage.ERR_RequiredField;
                return false;
            }

            int yob = Toolbox.StringToInt(ctrlYOB.Text.Trim());
            if (yob < 0)
            {
                ctrlMessage.Text = "Please enter a valid Year of Birth.";
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
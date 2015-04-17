using ChineseSchool.BusinessLogic;
using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChineseSchool
{
    public partial class Login : CSPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            Session.Clear();
        }

        protected void ctrlLogin_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            UserData ud = CSAgent.GetUserByEmail(ctrlEmail.Text.Trim(), GetSqlConnection());
            if (ud.Error || (ud.UserID == 0) || (ud.Password != ctrlPassword.Text.Trim()))
            {
                ctrlMessage.Text = CSMessage.ERR_Login_Failed;
                return;
            }

            //UserData ud = new UserData();

            //ud.Password = "1";
            //ud.Firstname = "first";
            //ud.Lastname = "last";
            //ud.Email = "aaa@email.com";
            //ud.Phone = "4121234567";
            //ud.Phone_Ext = "ext";
            //ud.Address1 = "address 111111111111";
            //ud.Address2 = "address 2222222222222";
            //ud.City = "city";
            //ud.State = new StateData { StateID = 1, StateName="New Jersey" };
            //ud.PostalCode = "12345";
            //ud.UserType = CSConstants.UserTypes.User;

            //List<ClassData> classes = new List<ClassData>();
            //classes.Add(new ClassData { ClassID=1, ClassName="Class 1", GradeID=1, GradeName="Grade 1"});
            //classes.Add(new ClassData { ClassID=2, ClassName="Class 2", GradeID=2, GradeName="Grade 2"});
            //ud.Children.Add(new ChildData {ChildFirstname="child f1", ChildLastname = "child l1", PickedClasses = classes, YOB=2008});
            //ud.Children.Add(new ChildData { ChildFirstname = "child f2", ChildLastname = "child l2", PickedClasses = classes, YOB = 2009 });

            SetCurrentUser(ud);

            Response.Redirect((ud.UserType == CSConstants.UserTypes.Admin)? "Admin.aspx" : "User.aspx");
        }

        private bool ValidateData()
        {
            if (Toolbox.IsEmpty(ctrlEmail.Text) || Toolbox.IsEmpty(ctrlPassword.Text))
            {
                ctrlMessage.Text = CSMessage.ERR_RequiredField;
                return false;
            }

            if (!Toolbox.ValidateFormat_Email(ctrlEmail.Text))
            {
                ctrlMessage.Text = CSMessage.ERR_Format_Invalid_Email;
                return false;
            }

            return true;
        }

        protected void ctrlRegister_Click(object sender, EventArgs e)
        {
            SetEditMode(CSConstants.EditMode.New);
            Response.Redirect("Reg_Account.aspx");
        }
    }
}
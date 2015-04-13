using ChineseSchool.BusinessLogic;
using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChineseSchool
{
    public partial class Register : CSPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        protected void ctrlSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            UserData user = new UserData();

            user.Password = ctrlPassword.Text.Trim();;
            user.Firstname = ctrlFirstname.Text.Trim();
            user.Lastname = ctrlLastname.Text.Trim();
            user.Email = ctrlEmail.Text.Trim();
            user.Phone = ctrlPhone.Text.Trim();;
            user.Phone_Ext = ctrlPhoneExt.Text.Trim();
            user.Address1 = ctrlAddress1.Text.Trim();
            user.Address2 = ctrlAddress2.Text.Trim();
            user.City = ctrlCity.Text.Trim();
            user.State = new StateData { StateID = Toolbox.StringToInt(ctrlState.SelectedValue) };
            user.PostalCode = ctrlPostalCode.Text.Trim();

            user.UserID = CSAgent.InsertUser(user, GetSqlConnection());
            if (user.UserID < 0) 
            {
                ctrlMessage.Text = CSMessage.ERR_CompleteRequest;
                return;
            }

            SetCurrentUser(user);

            Response.Redirect("Children_Add.aspx");
        }

        private bool ValidateData()
        {
            if (Toolbox.IsEmpty(ctrlFirstname.Text) || Toolbox.IsEmpty(ctrlLastname.Text) || Toolbox.IsEmpty(ctrlEmail.Text) || Toolbox.IsEmpty(ctrlEmail2.Text) || Toolbox.IsEmpty(ctrlPassword.Text) || Toolbox.IsEmpty(ctrlPassword2.Text))
            {
                ctrlMessage.Text = CSMessage.ERR_RequiredField;
                return false;
            }

            if (ctrlEmail.Text.Trim() != ctrlEmail2.Text.Trim())
            {
                ctrlMessage.Text = "Email Adress and Confirm Email Address do not match.";
                return false;
            }

            if (ctrlPassword.Text.Trim() != ctrlPassword2.Text.Trim())
            {
                ctrlMessage.Text = "Passwords do not match.";
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
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
    public partial class Reg_Account : CSPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (GetEditMode() == CSConstants.EditMode.Edit)
            {
                UserData user = GetCurrentUser();

                ctrlFirstname.Text = user.Firstname;
                ctrlLastname.Text = user.Lastname;
                ctrlEmail.Text = user.Email;
                ctrlEmail2.Text = user.Email;
                ctrlPhone.Text = user.Phone;
                ctrlPhoneExt.Text = user.Phone_Ext;
                ctrlAddress1.Text = user.Address1;
                ctrlAddress2.Text = user.Address2;
                ctrlCity.Text = user.City;
                ctrlState.SelectedValue = ((int)user.State.StateID).ToString();
                ctrlPostalCode.Text = user.PostalCode;
            }
            else
            {
#if (DEBUG)
                ctrlPassword.Text = "1";
                ctrlPassword2.Text = "1";
                ctrlFirstname.Text = "Firstname";
                ctrlLastname.Text = "Lastname";
                ctrlEmail.Text = "xjtest@yahoo.com";
                ctrlEmail2.Text = "xjtest@yahoo.com";
                ctrlPhone.Text = "4121234567";
                ctrlPhoneExt.Text = "ext1";
                ctrlAddress1.Text = "addr 1";
                ctrlAddress2.Text = "addr 2";
                ctrlCity.Text = "city";
                ctrlState.SelectedIndex = 1;
                ctrlPostalCode.Text = "12345";
#endif
            }
        }

        protected void ctrlSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            UserData user = (GetEditMode() == CSConstants.EditMode.New) ? new UserData() : GetCurrentUser();

            user.Password = ctrlPassword.Text.Trim();
            user.Firstname = ctrlFirstname.Text.Trim();
            user.Lastname = ctrlLastname.Text.Trim();
            user.Email = ctrlEmail.Text.Trim();
            user.Phone = ctrlPhone.Text.Trim();
            user.Phone_Ext = ctrlPhoneExt.Text.Trim();
            user.Address1 = ctrlAddress1.Text.Trim();
            user.Address2 = ctrlAddress2.Text.Trim();
            user.City = ctrlCity.Text.Trim();
            user.State = new StateData { StateID = Toolbox.StringToInt(ctrlState.SelectedValue) };
            user.PostalCode = ctrlPostalCode.Text.Trim();
            user.UserType = CSConstants.UserTypes.User;

            bool result;
            if (GetEditMode() == CSConstants.EditMode.New)
            {
                user.UserID = CSAgent.InsertUser(user, GetSqlConnection());
                result = user.UserID > 0;
            }
            else
            {
                result = CSAgent.UpdateUser(user, GetSqlConnection());
            }

            if (user.UserID < 0)
            {
                ctrlMessage.Text = CSMessage.ERR_CompleteRequest;
                return;
            }

            SetCurrentUser(user);

            Response.Redirect("Reg_Children.aspx");
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
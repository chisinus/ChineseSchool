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
            Response.Redirect("Reg_Account.aspx");
        }
    }
}
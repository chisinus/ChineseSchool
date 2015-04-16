using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChineseSchool
{
    public partial class User : SecurePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            ctrlUserInfo.UpdateUI(GetCurrentUser());
            ctrlChildren.UpdateUI(GetCurrentUser());
        }

        protected void ctrlEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reg_Account.aspx");
        }

        protected void ctrlLogout_Click(object sender, EventArgs e)
        {
            GoToLoginPage();
        }
    }
}
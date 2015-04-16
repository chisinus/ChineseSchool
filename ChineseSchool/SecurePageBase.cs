using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChineseSchool
{
    public class SecurePageBase : CSPageBase
    {
        #region sessoin
        const string SESSION_CURRENTUSER = "CurrentUser";
        protected UserData GetCurrentUser()
        {
            if (Session[SESSION_CURRENTUSER] == null)
                Response.Redirect("Login.aspx");

            return (UserData)Session[SESSION_CURRENTUSER];
        }

        protected void SetCurrentUser(UserData user)
        {
            Session[SESSION_CURRENTUSER] = user;
        }
        #endregion sessoin
    }
}
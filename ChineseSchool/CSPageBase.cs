using ChineseSchool.BusinessLogic;
using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChineseSchool
{
    public class CSPageBase : System.Web.UI.Page
    {
        SqlConnection sqlConnection = null;

        protected SqlConnection GetSqlConnection()
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection = Toolbox.OpenConnection();
            }

            return sqlConnection;
        }

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

        protected void GoToLoginPage()
        {
            Response.Redirect("Login.aspx");
        }
    }
}
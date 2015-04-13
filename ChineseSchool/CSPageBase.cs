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
        protected void SetCurrentUser(UserData ud)
        {
            Session["CurrentUser"] = ud;
        }

        protected UserData GetCurrentUser()
        {
            if (Session["CurrentUser"] == null)
                GoToLoginPage();
            
            return (UserData)Session["CurrentUser"];
        }
        #endregion session

        protected void GoToLoginPage()
        {
            Response.Redirect("Login.aspx");
        }
    }
}
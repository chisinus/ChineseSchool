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

        public SqlConnection GetSqlConnection()
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection = Toolbox.OpenConnection();
            }

            return sqlConnection;
        }

        #region sessoin
        const string SESSION_CURRENTUSER = "CurrentUser";
        const string SESSION_EDITMODE = "EditMode";
        public UserData GetCurrentUser(bool mustHave = true)
        {
            if (Session[SESSION_CURRENTUSER] == null)
            {
                if (mustHave)
                    Response.Redirect("Login.aspx");

                return new UserData();
            }

            return (UserData)Session[SESSION_CURRENTUSER];
        }

        public void SetCurrentUser(UserData user)
        {
            Session[SESSION_CURRENTUSER] = user;
        }

        public CSConstants.EditMode GetEditMode()
        {
            if (Session[SESSION_EDITMODE] == null)
                GoToLoginPage();

            return (CSConstants.EditMode)Session[SESSION_EDITMODE];
        }

        public void SetEditMode(CSConstants.EditMode mode)
        {
            Session[SESSION_EDITMODE] = mode;
        }

        //for edit children
        public void SetChildren(List<ChildData> list)
        {
            Session["Children"] = list;
        }

        public List<ChildData> GetChildren()
        {
            return (List<ChildData>)Session["Children"];
        }

        #endregion sessoin

        protected void GoToLoginPage()
        {
            Response.Redirect("Login.aspx");
        }
    }
}
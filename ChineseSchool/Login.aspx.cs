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
            ctrlMessage.Text = "Not implemented.";
            return;

            if (!ValidateData())
                return;

            UserData ud = CSAgent.GetUserByEmail(ctrlEmail.Text.Trim(), GetSqlConnection());
            if (ud.Error || (ud.UserID == 0) || (ud.Password != ctrlPassword.Text.Trim()))
            {
                ctrlMessage.Text = CSMessage.ERR_Login_Failed;
                return;
            }

            SetCurrentUser(ud);
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

        protected void ctrlExport_Click(object sender, EventArgs e)
        {
            DataTable dataTable = CSAgent.GetUserList(GetSqlConnection());

            StringBuilder builder = new StringBuilder();
            List<string> columnNames = new List<string>();
            List<string> rows = new List<string>();

            foreach (DataColumn column in dataTable.Columns)
            {
                columnNames.Add(column.ColumnName);
            }

            builder.Append(string.Join(",", columnNames.ToArray())).Append("\n");

            foreach (DataRow row in dataTable.Rows)
            {
                List<string> currentRow = new List<string>();

                foreach (DataColumn column in dataTable.Columns)
                {
                    object item = row[column];

                    currentRow.Add(item.ToString());
                }

                rows.Add(string.Join(",", currentRow.ToArray()));
            }

            builder.Append(string.Join("\n", rows.ToArray()));

            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=export.csv");
            Response.Write(builder.ToString());
            Response.End();
        }
    }
}
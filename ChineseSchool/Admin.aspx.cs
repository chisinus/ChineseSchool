using ChineseSchool.BusinessLogic;
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
    public partial class Admin : SecurePageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetCurrentUser();
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

        protected void ctrlLogout_Click(object sender, EventArgs e)
        {
            GoToLoginPage();
        }
    }
}
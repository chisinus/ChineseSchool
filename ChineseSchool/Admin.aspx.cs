﻿using ChineseSchool.BusinessLogic;
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
                    currentRow.Add(CsvQuote(row[column].ToString().Trim()));
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

        private string CsvQuote(string text)
        {
            if (text == null) return string.Empty;

            bool containsQuote = false;
            bool containsComma = false;
            int len = text.Length;
            for (int i = 0; i < len && (!containsComma || !containsQuote); i++)
            {
                char ch = text[i];
                if (ch == '"')
                {
                    containsQuote = true;
                }
                else if (ch == ',')
                {
                    containsComma = true;
                }
            }

            bool mustQuote = containsComma || containsQuote;

            if (containsQuote)
                text = text.Replace("\"", "\"\"");

            if (mustQuote)
                return "\"" + text + "\"";  // Quote the cell and replace embedded quotes with double-quote

            return text;
        }

       protected void ctrlLogout_Click(object sender, EventArgs e)
        {
            GoToLoginPage();
        }
    }
}
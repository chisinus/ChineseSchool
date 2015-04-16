using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ChineseSchool.UserControl
{
    public partial class UC_ChildList : System.Web.UI.UserControl
    {
        public void UpdateUI(UserData user)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ChildLastname", typeof(string));
            dt.Columns.Add("ChildFirstname", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("YOB", typeof(string));
            dt.Columns.Add("GradeName", typeof(string));
            dt.Columns.Add("ClassName", typeof(string));

            foreach (ChildData child in user.Children)
            {
                dt.Rows.Add(child.ChildLastname, child.ChildFirstname, child.Gender.ToString(), child.YOB, child.PickedClasses[0].GradeName, BuildClassString(child.PickedClasses));
            }

            ctrlGrid.DataSource = dt;
            ctrlGrid.Rebind();
        }

        private string BuildClassString(List<ClassData> classList)
        {
            if (classList == null || classList.Count == 0) return "";

            string ret = "";
            foreach (ClassData c in classList)
            {
                ret += ((ret == "") ? "" : @"<br />") + c.ClassName;
            }

            return ret;
        }

        public void ctrlGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;

                LinkButton button = dataItem["DeleteColumn"].Controls[0] as LinkButton;
                button.Attributes["onclick"] = "return confirm('Are you sure you want to delete this child?')";
            }
        }

        protected void ctrlGrid_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            int i = 0;
        }

        protected void ctrlGrid_ItemDeleted(object sender, GridCommandEventArgs e)
        {
            int i = 0;
        }
    }
}
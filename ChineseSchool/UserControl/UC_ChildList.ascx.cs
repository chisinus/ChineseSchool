using ChineseSchool.BusinessLogic;
using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ChineseSchool.UserControl
{
    public partial class UC_ChildList : System.Web.UI.UserControl
    {
        public void UpdateUI(List<ChildData> children, bool showDeleteColumn)
        {
            UpdateGrid(children);
            ctrlGrid.MasterTableView.GetColumn("DeleteColumn").Display = showDeleteColumn;
        }

        private void UpdateGrid(List<ChildData> children)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ChildID", typeof(int));
            dt.Columns.Add("ChildLastname", typeof(string));
            dt.Columns.Add("ChildFirstname", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("YOB", typeof(string));
            dt.Columns.Add("GradeName", typeof(string));
            dt.Columns.Add("TeacherName", typeof(string));

            foreach (ChildData child in children)
            {
                dt.Rows.Add(child.ChildID, child.ChildLastname, child.ChildFirstname, child.Gender.ToString(), child.YOB, child.PickedClasses[0].Grade.GradeName, child.PickedClasses[0].Teacher.TeacherName);
            }

            ctrlGrid.DataSource = dt;
            ctrlGrid.Rebind();

        }

        //private string BuildClassString(List<ClassData> classList)
        //{
        //    if (classList == null || classList.Count == 0) return "";

        //    string ret = "";
        //    foreach (ClassData c in classList)
        //    {
        //        ret += ((ret == "") ? "" : @"<br />") + c.ClassName;
        //    }

        //    return ret;
        //}

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
            GridDataItem item = (GridDataItem)e.Item; 
            int childID = (int)item.GetDataKeyValue("ChildID");    

            CSPageBase page = (CSPageBase)Page;

            //if (page.GetEditMode() == CSConstants.EditMode.Edit)
            //{
            //    SqlTransaction tran = page.GetSqlConnection().BeginTransaction();
            //    bool result = CSAgent.DeleteChild(childID, page.GetSqlConnection(), tran);

            //    Toolbox.EndTransaction(tran, result);

            //    if (result)
            //    {
            //        UserData user = CSAgent.GetUserByEmail(page.GetCurrentUser().Email, page.GetSqlConnection());
            //        page.SetCurrentUser(user);
            //        UpdateGrid(user.Children);
            //    }
            //}
            //else
            //{
                List<ChildData> children = page.GetChildren();
                foreach (ChildData child in children)
                {
                    if (child.ChildID == childID)
                    {
                        children.Remove(child);
                        break;
                    }
                }

                page.SetChildren(children);
                UpdateGrid(children);
            //}
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ChineseSchool
{
    public partial class XJTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerID", typeof(int));
            dt.Columns.Add("ContactName", typeof(string));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("ContactTitle", typeof(string));
            dt.Columns.Add("Phone", typeof(string));

            for (int i = 0; i < 10; i++ )
            {
                dt.Rows.Add(i, "ContactName" + i, "CompanyName" + i, "ContactTitle" + i, "Phone" + i);
            }

            RadGrid1.DataSource = dt;
            RadGrid1.Rebind();
        }

        protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                if (!(e.Item is GridEditFormInsertItem))
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    GridEditManager manager = item.EditManager;
                    GridTextBoxColumnEditor editor = manager.GetColumnEditor("CustomerID") as GridTextBoxColumnEditor;
                    editor.TextBoxControl.Enabled = false;
                }
            }
        }
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {

                e.ExceptionHandled = true;
                SetMessage("Customer cannot be inserted. Reason: " + e.Exception.Message);

            }
            else
            {
                SetMessage("New customer is inserted!");
            }
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(string.Format("<span style='color:red'>{0}</span>", text)));
        }

        private void SetMessage(string message)
        {
            gridMessage = message;
        }

        private string gridMessage = null;

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gridMessage))
            {
                DisplayMessage(gridMessage);
            }
        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridEditableItem)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                //here editedItem.SavedOldValues will be the dictionary which holds the
                //predefined values

                //Prepare new dictionary object
                Hashtable newValues = new Hashtable();
                e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);
                //the newValues instance is the new collection of key -> value pairs
                //with the updated ny the user data
            }
        }
    }
}
using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChineseSchool.UserControl
{
    public partial class UC_UserInfo : System.Web.UI.UserControl
    {
        public void UpdateUI(UserData user)
        {
            ctrlFirstname.Text = user.Firstname;
            ctrlLastname.Text = user.Lastname;
            ctrlEmail.Text = user.Email;
            ctrlPhone.Text = user.Phone + " - " + user.Phone_Ext;
            ctrlAddress1.Text = user.Address1;
            ctrlAddress2.Text = user.Address2;
            ctrlCity.Text = user.City;
            ctrlState.Text = user.State.StateName;
            ctrlPostalCode.Text = user.PostalCode;

            if (user.Volunteers.Count == 0)
            {
                ctrlVolunteerPanel.Visible = false;
                return;
            }

            Literal s = new Literal();
            s.Text = user.Volunteers.Aggregate(string.Empty, (current, next)=>current + @"<BR />" + next.VolunteerDesc, ss=>ss.Substring(6));
            ctrlVolunteerPH.Controls.Add(s);
        }
    }
}
﻿using ChineseSchool.BusinessLogic;
using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChineseSchool
{
    public partial class Reg_Children : CSPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            PopulateGrade();

            ctrlChildren.UpdateUI(GetCurrentUser());
        }

        private void PopulateGrade()
        {
            //List<ClassData> classes = CSAgent.GetClassList(GetSqlConnection());
            //if (classes == null) return;

            //ctrlClass.Items.Clear();
            //ctrlClass.Items.Add(new ListItem("Select One", "0"));
            //foreach (ClassData myClass in classes)
            //{
            //    ctrlClass.Items.Add(new ListItem(myClass.ClassName + " / " + myClass.GradeName, myClass.ClassID.ToString()));
            //}
        }

        protected void ctrlAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
                return;

            ChildData child = new ChildData();

            child.ChildFirstname = ctrlFirstname.Text.Trim();
            child.ChildLastname = ctrlLastname.Text.Trim();
            child.Gender = (CSConstants.Genders)Toolbox.StringToInt(ctrlGender.SelectedValue);
            child.YOB = Toolbox.StringToInt(ctrlYOB.Text.Trim());
            child.PickedClasses.Add(CSAgent.GetClassInfo(Toolbox.StringToInt(ctrlClass.SelectedValue), GetSqlConnection()));

            UserData user = GetCurrentUser();
            user.Children.Add(child);
            SetCurrentUser(user);

            ctrlChildren.UpdateUI(user);
        }

        private bool ValidateData()
        {
            if (Toolbox.IsEmpty(ctrlFirstname.Text) || Toolbox.IsEmpty(ctrlLastname.Text) || (ctrlClass.SelectedIndex == 0))
            {
                ctrlMessage.Text = CSMessage.ERR_RequiredField;
                return false;
            }

            int yob = Toolbox.StringToInt(ctrlYOB.Text.Trim());
            if (yob < 0)
            {
                ctrlMessage.Text = "Please enter a valid Year of Birth.";
                return false;
            }

            return true;
        }

        protected void ctrlSubmit_Click(object sender, EventArgs e)
        {
            UserData user = GetCurrentUser();

            if (user.Children.Count == 0) Response.Redirect("Reg_Confirmation.aspx");

            SqlTransaction tran = GetSqlConnection().BeginTransaction();

            bool result = true;
            foreach (ChildData child in user.Children)
            {
                child.ChildID = CSAgent.InsertChild(user.UserID, child, GetSqlConnection(), tran);
                if (child.ChildID < 0)
                {
                    result = false;
                    break;
                }

                if (CSAgent.InsertChildClass(child.ChildID, child.PickedClasses[0].ClassID, GetSqlConnection(), tran) > 0)
                {
                    result = false;
                    break;
                }
            }
            
            Toolbox.EndTransaction(tran, result);

            if (!result)
            {
                ctrlMessage.Text = CSMessage.ERR_CompleteRequest;
                return;
            }

            SetCurrentUser(user);
        }

        protected void ctrlCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
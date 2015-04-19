using ChineseSchool.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChineseSchool.BusinessObjects
{
    public class UserData : CSObject, ICSObjectFromDB, ICSObjectContainer
    {
        #region properties
        public int UserID { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Phone_Ext { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public StateData State { get; set; }
        public string PostalCode { get; set; }
        public CSConstants.UserTypes UserType { get; set; }
        public string EMGContact { get; set; }
        public string EMGPhone { get; set; }
        public bool Ack_Rule { get; set; }
        public bool Ack_Medical { get; set; }
        public bool Ack_Publish { get; set; }

        public List<ChildData> Children { get; set; }
        public List<VolunteerData> Volunteers { get; set; }
        #endregion properties

        #region constructors
        public UserData()
        {
            Children = new List<ChildData>();
            State = new StateData();
        }
        #endregion constructors

        public void GetDataFromDB(SqlDataReader reader)
        {
            UserID = Toolbox.GetDBValue<int>(reader, "UserID");
            Password = Toolbox.GetDBValue<string>(reader, "Password");
            Firstname = Toolbox.GetDBValue<string>(reader, "Firstname");
            Lastname = Toolbox.GetDBValue<string>(reader, "Lastname");
            Email  = Toolbox.GetDBValue<string>(reader, "Email");
            Phone  = Toolbox.GetDBValue<string>(reader, "Phone");
            Phone_Ext  = Toolbox.GetDBValue<string>(reader, "Phone_Ext");
            Address1  = Toolbox.GetDBValue<string>(reader, "Address1");
            Address2 = Toolbox.GetDBValue<string>(reader, "Address2");
            City  = Toolbox.GetDBValue<string>(reader, "City");
            State = new StateData(reader);
            PostalCode = Toolbox.GetDBValue<string>(reader, "PostalCode");
            UserType = (CSConstants.UserTypes)Toolbox.GetDBValue<int>(reader, "UserTypeID");
            EMGContact = Toolbox.GetDBValue<string>(reader, "EMGContact");
            EMGPhone = Toolbox.GetDBValue<string>(reader, "EMGPhone");
            Ack_Rule = Toolbox.GetDBValue<bool>(reader, "Ack_Rule");
            Ack_Medical = Toolbox.GetDBValue<bool>(reader, "Ack_Medical");
            Ack_Publish = Toolbox.GetDBValue<bool>(reader, "Ack_Publish");

            AddItem(reader);
        }

        public bool HasSameKeyValue(System.Data.SqlClient.SqlDataReader reader, string comparingFieldName)
        {
            return UserID == Toolbox.GetDBValue<int>(reader, "UserID");
        }

        public void AddItem(System.Data.SqlClient.SqlDataReader reader)
        {
            int childID = Toolbox.GetDBValue<int>(reader, "ChildID", 0);
            if (childID <= 0) return;

            ChildData child = GetChild(childID);
            if (child == null)
                Children.Add(new ChildData(reader));
            else
                child.AddClass(reader);
        }

        private ChildData GetChild(int childID)
        {
            return Children.FirstOrDefault(c => c.ChildID == childID);
        }

        internal bool HasVolunteer(CSConstants.VolunteerTypes volunteerTypes)
        {
            return Volunteers.Find(v => v.VolunteerType == volunteerTypes) != null;
        }
    }
}
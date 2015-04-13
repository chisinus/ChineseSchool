using ChineseSchool.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ChineseSchool.BusinessObjects
{
    public class ChildData
    {
        #region properties
        public int ChildID { get; set; }
        public string ChildFirstname { get; set; }
        public string ChildLastname { get; set; }
        public CSConstants.Genders Gender { get; set; }
        public int YOB { get; set; }

        public List<ClassData> PickedClasses { get; set; }
        #endregion properties

        #region constructor
        public ChildData(SqlDataReader reader)
        {
            ChildID = Toolbox.GetDBValue<int>(reader, "ChildID");
            ChildFirstname = Toolbox.GetDBValue<string>(reader, "ChildFirstname");
            ChildLastname = Toolbox.GetDBValue<string>(reader, "ChildLastname");
            Gender = (CSConstants.Genders)Toolbox.GetDBValue<int>(reader, "GenderID");
            YOB = Toolbox.GetDBValue<int>(reader, "YOB");

            PickedClasses = new List<ClassData>();

            AddClass(reader);
        }

        public ChildData()
        {
            ChildID = 0;

            PickedClasses = new List<ClassData>();
        }
        #endregion constructor

        internal void AddClass(SqlDataReader reader)
        {
            PickedClasses.Add(new ClassData(reader));
        }
    }
}

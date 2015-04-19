using ChineseSchool.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ChineseSchool.BusinessObjects
{
    public class TeacherData : CSObject, ICSObjectFromDB
    {
        #region properties
        public int TeacherID { get; set; }
        public string TeacherName_C { get; set; }
        public string TeacherName_E { get; set; }

        public string TeacherName
        {
            get { return TeacherName_C + " " + TeacherName_E; }
        }
        #endregion properties

        #region constructor
        public TeacherData(SqlDataReader reader)
        {
            GetDataFromDB(reader);
        }

        public TeacherData()
        {
            // TODO: Complete member initialization
        }
        #endregion constructor

        public void GetDataFromDB(SqlDataReader reader)
        {
            TeacherID = Toolbox.GetDBValue<int>(reader, "TeacherID");
            TeacherName_C = Toolbox.GetDBValue<string>(reader, "TeacherName_C");
            TeacherName_E = Toolbox.GetDBValue<string>(reader, "TeacherName_E");
        }
    }
}

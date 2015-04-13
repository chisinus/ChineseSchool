using ChineseSchool.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ChineseSchool.BusinessObjects
{
    public class ClassData : CSObject, ICSObjectFromDB
    {
        #region properties
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int GradeID { get; set; }        //kept just in case class/grade are multi-to-multi. update after figure out grade/class relationship.
        public string GradeName { get; set; }
        #endregion properties

        #region constructor
        public ClassData(SqlDataReader reader)
        {
            GetDataFromDB(reader);
        }

        public ClassData()
        {
            // TODO: Complete member initialization
        }
        #endregion constructor

        public void GetDataFromDB(SqlDataReader reader)
        {
            ClassID = Toolbox.GetDBValue<int>(reader, "ClassID");
            ClassName = Toolbox.GetDBValue<string>(reader, "ClassName");
            GradeID = Toolbox.GetDBValue<int>(reader, "GradeID");
            GradeName = Toolbox.GetDBValue<string>(reader, "GradeName");
        }
    }
}

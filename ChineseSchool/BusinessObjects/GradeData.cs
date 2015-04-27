using ChineseSchool.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ChineseSchool.BusinessObjects
{
    public class GradeData : CSObject, ICSObjectFromDB
    {
        #region properties
        public int GradeID { get; set; }
        public string GradeName { get; set; }
        #endregion properties

        #region constructor
        public GradeData(SqlDataReader reader)
        {
            GetDataFromDB(reader);
        }

        public GradeData()
        {
            // TODO: Complete member initialization
        }
        #endregion constructor

        public void GetDataFromDB(SqlDataReader reader)
        {
            GradeID = Toolbox.GetDBValue<int>(reader, "GradeID");
            GradeName = Toolbox.GetDBValue<string>(reader, "GradeName");
        }
    }
}

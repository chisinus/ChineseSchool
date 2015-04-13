using ChineseSchool.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChineseSchool.BusinessObjects
{
    public class StateData
    {
        #region properties
        public int StateID { get; set; }
        public string StateName { get; set; }
        #endregion properties

        public StateData()
        {
            StateID = 0;
        }

        public StateData(SqlDataReader reader)
        {
            StateID = Toolbox.GetDBValue<int>(reader, "StateID");
            StateName = Toolbox.GetDBValue<string>(reader, "StateName");
        }
    }
}
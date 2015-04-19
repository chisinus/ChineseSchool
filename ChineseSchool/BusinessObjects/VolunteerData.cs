using ChineseSchool.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChineseSchool.BusinessObjects
{
    public class VolunteerData
    {
        public CSConstants.VolunteerTypes VolunteerType { get; set; }
        public string VolunteerDesc { get; set; }

        public VolunteerData(CSConstants.VolunteerTypes vType)
        {
            VolunteerType = vType;
            VolunteerDesc = "";
        }

        public VolunteerData(SqlDataReader reader)
        {
            VolunteerType = (CSConstants.VolunteerTypes)Toolbox.GetDBValue<int>(reader, "VolunteerTypeID");
            VolunteerDesc = Toolbox.GetDBValue<string>(reader, "VolunteerDesc");
        }
    }
}
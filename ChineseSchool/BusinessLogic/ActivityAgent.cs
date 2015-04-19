using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChineseSchool.BusinessLogic
{
    public class ActivityAgent
    {
        public static bool InsertActivity(int userID, CSConstants.ActivityTypes activityTypeID, string activityContent, SqlConnection conn, SqlTransaction tran)
        {
            if ((userID <=0) || (conn == null)) return false;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
                                                    new SqlParameter("@UserID", userID),
													new SqlParameter("@ActivityTypeID", (int)activityTypeID),
													new SqlParameter("@Content", activityContent)
												};

            return Toolbox.WriteDataToDB("InsertActivity", "procSys_InsertActivity", conn, parameters, tran);
        }
    }
}
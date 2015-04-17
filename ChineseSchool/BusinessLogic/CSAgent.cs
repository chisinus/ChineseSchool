using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChineseSchool.BusinessLogic
{
    public class CSAgent
    {
        #region user
        public static UserData GetUserByEmail(string email, SqlConnection conn)
        {
            if (conn == null)
                return new UserData { Error = true };

			SqlParameter[] parameters = new SqlParameter[] 
										{ 
											new SqlParameter("@Email", email)
										};

            return Toolbox.GetDBObject_Container<UserData>("GetUserByEmail", "procUser_GetUserByEmail", conn, parameters);
        }

        public static int InsertUser(UserData user, SqlConnection conn, SqlTransaction tran = null)
        {
            if ((user == null) || (conn == null)) return -1;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
													new SqlParameter("@Password", user.Password),
													new SqlParameter("@Firstname", user.Firstname),
													new SqlParameter("@Lastname", user.Lastname),
													new SqlParameter("@Email", user.Email),
													new SqlParameter("@Phone", user.Phone),
													new SqlParameter("@Phone_Ext", user.Phone_Ext),
													new SqlParameter("@Address1", user.Address1),
													new SqlParameter("@Address2", user.Address2),
													new SqlParameter("@City", user.City),
													new SqlParameter("@StateID", user.State.StateID),
													new SqlParameter("@PostalCode", user.PostalCode),
													new SqlParameter("@NewID", SqlDbType.Int) { Direction= ParameterDirection.Output}
												};

            return Toolbox.WriteDataToDBWithID("InsertUser", "procUser_InsertUser", conn, parameters, tran);
        }

        public static int InsertChild(int userID, ChildData child, SqlConnection conn, SqlTransaction tran = null)
        {
            if ((child == null) || (conn == null)) return -1;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
													new SqlParameter("@UserID", userID),
													new SqlParameter("@ChildFirstname", child.ChildFirstname),
													new SqlParameter("@ChildLastname", child.ChildLastname),
													new SqlParameter("@GenderID", child.Gender),
													new SqlParameter("@YOB", child.YOB),
													new SqlParameter("@NewID", SqlDbType.Int) { Direction= ParameterDirection.Output}
												};

            return Toolbox.WriteDataToDBWithID("InsertChild", "procUser_InsertChild", conn, parameters, tran);
        }

        public static int InsertChildClass(int childID, int classID, SqlConnection conn, SqlTransaction tran = null)
        {
            if ((childID <= 0) || (classID <= 0) || (conn == null)) return -1;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
													new SqlParameter("@ChildID", childID),
													new SqlParameter("@ClassID", classID),
                                                    new SqlParameter("SchoolYear", CSConstants.CURRENTSCHOOLYEAR),
													new SqlParameter("@NewID", SqlDbType.Int) { Direction= ParameterDirection.Output}
												};

            return Toolbox.WriteDataToDBWithID("InsertClass", "procUser_InsertChildClass", conn, parameters, tran);
        }
        #endregion user

        #region other
        public static List<ClassData> GetClassList(SqlConnection conn)
        {
            if (conn == null) return null;

            return Toolbox.GetDBObjects<ClassData>("GetClassList", "procSys_GetClassList", conn);
        }
        #endregion other

        internal static DataTable GetUserList(SqlConnection conn)
        {
            if (conn == null) return null;

            return Toolbox.GetDBDataTable("GetUserList", "procUser_GetUserList", conn);
        }

        internal static ClassData GetClassInfo(int classID, SqlConnection conn)
        {
            if ((classID <= 0) || (conn == null)) return new ClassData { Error = true };

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
													new SqlParameter("@ClassID", classID)
												};

            return Toolbox.GetDBObject<ClassData>("GetClassInfo", "procClass_GetClassInfo", conn, parameters);
        }

        internal static bool UpdateUser(UserData user, SqlConnection conn)
        {
            if ((user == null) || (conn == null)) return false;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
                                                    new SqlParameter("@UserID", user.UserID),
													new SqlParameter("@Password", user.Password),
													new SqlParameter("@Firstname", user.Firstname),
													new SqlParameter("@Lastname", user.Lastname),
													new SqlParameter("@Email", user.Email),
													new SqlParameter("@Phone", user.Phone),
													new SqlParameter("@Phone_Ext", user.Phone_Ext),
													new SqlParameter("@Address1", user.Address1),
													new SqlParameter("@Address2", user.Address2),
													new SqlParameter("@City", user.City),
													new SqlParameter("@StateID", user.State.StateID),
													new SqlParameter("@PostalCode", user.PostalCode)
												};

            return Toolbox.WriteDataToDB("UpdateUser", "procUser_UpdateUser", conn, parameters);
        }

        internal static bool DeleteChildren(int userID, SqlConnection conn, SqlTransaction tran)
        {
            if ((userID <= 0) || (conn==null) || (tran==null)) return false;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
													new SqlParameter("@UserID", userID)
												};

            return Toolbox.WriteDataToDB("DeleteChildren", "procUser_DeleteChildren", conn, parameters, tran);
        }

        internal static bool DeleteChild(int childID, SqlConnection conn, SqlTransaction tran)
        {
            if ((childID <= 0) || (conn == null) || (tran == null)) return false;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
													new SqlParameter("@ChildID", childID)
												};

            return Toolbox.WriteDataToDB("DeleteChild", "procUser_DeleteChild", conn, parameters, tran);
        }
    }
}
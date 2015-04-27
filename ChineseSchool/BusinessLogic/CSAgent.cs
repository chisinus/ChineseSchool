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

        public static int InsertUser(UserData user, SqlConnection conn, SqlTransaction tran)
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
													new SqlParameter("@EMGContact", user.EMGContact),
                                                    new SqlParameter("@EMGPhone", user.EMGPhone),
                                                    new SqlParameter("@Comments", user.Comments),
													new SqlParameter("@NewID", SqlDbType.Int) { Direction= ParameterDirection.Output}
												};

            int userID = Toolbox.WriteDataToDBWithID("InsertUser", "procUser_InsertUser", conn, parameters, tran);
            bool result = userID > 0;

            if (result)
            {
                foreach (VolunteerData v in user.Volunteers)
                {
                    result = InsertUserVolunteer(userID, v.VolunteerType, conn, tran);
                    
                    if (!result) break;
                }

                result = result &&
                         ActivityAgent.InsertActivity(userID, CSConstants.ActivityTypes.CreateUserAccount, "", conn, tran);
            }

            return result ? userID : -1;
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
                                                    new SqlParameter("@SeriousIllness", child.SeriousIllness),
                                                    new SqlParameter("@HeartDisease", child.HeartDisease),
                                                    new SqlParameter("@Medicine", child.Medicine),
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

        internal static ClassData GetClassInfoByGradeAndTeacher(int gradeID, int teacherID, SqlConnection conn)
        {
            if ((gradeID <= 0) || (teacherID <= 0) || (conn == null)) return new ClassData { Error = true };

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
													new SqlParameter("@GradeID", gradeID),
                                                    new SqlParameter("@TeacherID", teacherID)
												};

            return Toolbox.GetDBObject<ClassData>("GetClassInfo", "procClass_GetClassInfoByGradeAndTeacher", conn, parameters);
        }

        internal static bool UpdateUser(UserData user, SqlConnection conn)
        {
            if ((user == null) || (conn == null)) return false;

            SqlTransaction tran = conn.BeginTransaction();

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
                                                    new SqlParameter("@PostalCode", user.PostalCode),
													new SqlParameter("@EMGContact", user.EMGContact),
                                                    new SqlParameter("@EMGPhone", user.EMGPhone)
												};

            bool result = Toolbox.WriteDataToDB("UpdateUser", "procUser_UpdateUser", conn, parameters, tran);
            if (result)
            {
                result = DeleteUserVolunteer(user.UserID, conn, tran);
                if (result)
                {
                    foreach (VolunteerData v in user.Volunteers)
                    {
                        result = InsertUserVolunteer(user.UserID, v.VolunteerType, conn, tran);

                        if (!result) break;
                    }

                    result = result &&
                             ActivityAgent.InsertActivity(user.UserID, CSConstants.ActivityTypes.UpdateuserAccount, "", conn, tran);
                }
            }

            Toolbox.EndTransaction(tran, result);

            return result;
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

        internal static DataTable GetTeachersByGrade(int gradeID)
        {
            SqlConnection conn = Toolbox.OpenConnection();
            if (conn == null) return null;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
													new SqlParameter("@GradeID", gradeID)
												};

            DataTable ret = Toolbox.GetDBDataTable("GetTeachersByGrade", "procSys_GetTeachersByGrade", conn, parameters);

            Toolbox.CloseConnection(conn);

            return ret;
        }

        internal static List<GradeData> GetGrades()
        {
            SqlConnection conn = Toolbox.OpenConnection();
            if (conn == null) return null;

            List<GradeData> ret = Toolbox.GetDBObjects<GradeData>("GetGrades", "procSys_GetGrades", conn);

            Toolbox.CloseConnection(conn);

            return ret;
        }

        private static bool InsertUserVolunteer(int userID, CSConstants.VolunteerTypes volunteerType, SqlConnection conn, SqlTransaction tran)
        {
            if ((conn == null) || (tran == null)) return false;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
                                                    new SqlParameter("@UserID", userID),
													new SqlParameter("@VolunteerTypeID", (int)volunteerType)
												};

            return Toolbox.WriteDataToDB("InsertUserVolunteer", "procUser_InsertVolunteer", conn, parameters, tran);
        }

        private static bool DeleteUserVolunteer(int userID, SqlConnection conn, SqlTransaction tran)
        {
            if ((userID<=0) || (conn == null) || (tran == null)) return false;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
                                                    new SqlParameter("@UserID", userID)
												};

            return Toolbox.WriteDataToDB("DeleteUserVolunteer", "procUser_DeleteVolunteer", conn, parameters, tran);
        }

        internal static bool UpdateCommentsAndUserAcknowledges(int userID, string comments, bool ackRule, bool actMedical, bool actPublish, SqlConnection conn, SqlTransaction tran)
        {
            if ((userID <= 0) || (conn == null) || (tran == null)) return false;

            SqlParameter[] parameters = new SqlParameter[] 
												{ 
                                                    new SqlParameter("@UserID", userID),
                                                    new SqlParameter("@Comments", comments),
                                                    new SqlParameter("@AckRule", ackRule),
                                                    new SqlParameter("@AckMedial", actMedical),
                                                    new SqlParameter("@ActPublish", actPublish)
												};

            return Toolbox.WriteDataToDB("UpdateUserAcknowledges", "procUser_UpdateAcknowledges", conn, parameters, tran);
        }
    }
}
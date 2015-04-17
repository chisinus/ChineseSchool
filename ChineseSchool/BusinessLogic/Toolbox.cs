using ChineseSchool.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace ChineseSchool.BusinessLogic
{
    public static class Toolbox
    {
        public static string LoggingPath = "";

        #region Logging
        public static void Logging(string caller, string msg, Exception e)
        {
            StreamWriter sw = File.CreateText(LoggingPath + "Logging.txt"); // creating file
            sw.Write("Caller: " + caller + "\r\n");
            sw.Write("Message: " + msg + "\r\n");
            sw.Write("Exception: " + e.ToString() + "\r\n");
            sw.Write("==============================");
            sw.Close();
        }
        #endregion logging

        #region utils
        public static bool IsEmpty(string value)
        {
            if (value == null) return true;

            return string.IsNullOrEmpty(value.Trim());
        }

        public static string ToXMLString(string tag, string value)
        {
            return "<" + tag + ">" + value + "</" + tag + ">";
        }

        public static int StringToInt(string value)
        {
            int nRet;

            try
            {
                nRet = Convert.ToInt32(value);
            }
            catch (Exception)
            {
                // Nothing to do
                nRet = -999999;
            }

            return nRet;
        }
        #endregion utils

        #region Database
        public static SqlConnection OpenConnection()
        {
            string connectionString;
            try
            {
                connectionString = WebConfigurationManager.ConnectionStrings["CSConnectionString"].ToString();
            }
            catch
            {
                return null;
            }

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (SqlException sqle)
            {
                connection = null;
            }

            return connection;
        }

        public static void CloseConnection(SqlConnection conn)
        {
            CloseConnection(conn, null, false);
        }

        public static void CloseConnection(SqlConnection conn, SqlTransaction tran, bool succ)
        {
            try
            {
                if (tran != null)
                {
                    if (succ)
                        tran.Commit();
                    else
                        tran.Rollback();
                }

                conn.Close();
            }
            catch (Exception e)
            {
                Logging("EndTransaction", "EndTransaction", e);
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
                conn.Dispose();
            }
        }

        public static void EndTransaction(SqlTransaction tran, bool succ)
        {
            try
            {
                if (tran != null)
                {
                    if (succ)
                        tran.Commit();
                    else
                        tran.Rollback();
                }
            }
            catch (Exception e)
            {
                Logging("EndTransaction", "EndTransaction", e);
            }
            finally
            {
                if (tran != null)
                    tran.Dispose();
            }
        }

        public static bool HasColumn(SqlDataReader reader, string columnName)
        {
            if (reader == null)
                return false;

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase) &&
                    reader.GetValue(i) != DBNull.Value)
                    return true;
            }
            return false;
        }

        public static T GetDBValue<T>(SqlDataReader reader, string column)
        {
            return GetDBValue(reader, column, default(T), true);
        }

        public static T GetDBValue<T>(SqlDataReader reader, string column, T value, bool required = false)
        {
            bool hasColumn = HasColumn(reader, column);
            if (required && !hasColumn)
                throw new Exception(string.Format("Column {0} does not exist.", column));

            if (typeof(T) == typeof(string))
            {
                return hasColumn ? (T)(object)(((string)reader[column]).Trim()) : value;
            }

            return hasColumn ? (T)reader[column] : value;
        }

        public static T GetDBObject<T>(string caller, string spName, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null) where T : CSObject
        {
            List<T> results = GetDBObjects<T>(caller, spName, conn, parameters, tran);
            if (results != null)
                return (results.Count > 0) ? results[0] : (T)Activator.CreateInstance(typeof(T));

            T ret = (T)Activator.CreateInstance(typeof(T));
            ret.Error = true;

            return ret;
        }

        public static T GetDBObject_Container<T>(string caller, string spName, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null, string comparingFieldName = "") where T : CSObject, ICSObjectContainer, ICSObjectFromDB
        {
            List<T> results = GetDBObjects_Container<T>(caller, spName, conn, parameters, tran, comparingFieldName);
            if (results != null)
                return (results.Count > 0) ? results[0] : (T)Activator.CreateInstance(typeof(T));

            T ret = (T)Activator.CreateInstance(typeof(T));
            ret.Error = true;

            return ret;
        }

        public static T GetDBValue<T>(string caller, string spName, string fieldName, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null)
        {
            List<T> results = GetDBValueList<T>(caller, spName, fieldName, conn, parameters, tran);
            if (results != null)
                return (results.Count > 0) ? results[0] : default(T);

            return GetErrorValue<T>();
        }

        private static void EnsureValueType<T>()
        {
            if ((typeof(T) != typeof(string)) && (typeof(T) != typeof(int)) && (typeof(T) != typeof(DateTime)) &&
                (typeof(T) != typeof(bool)) && (typeof(T) != typeof(short)))
                throw new Exception("The value type is not supported.");
        }

        private static T GetErrorValue<T>()
        {
            T ret = default(T);

            if (typeof(T) == typeof(string))
                ret = (T)(object)null;
            else if ((typeof(T) == typeof(int)) || (typeof(T) == typeof(short)))
                ret = (T)(object)-1;
            else if (typeof(T) == typeof(DateTime))
                ret = (T)(object)CSConstants.EMPTYDATE;
            else if (typeof(T) == typeof(bool))
                ret = (T)(object)false;

            return ret;
        }

        public static List<T> GetDBObjects<T>(string caller, string spName, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null)
        {
            EnsureICSObjectFromDB<T>();

            if (conn == null)
                throw new Exception("The connection is null.");

            List<T> ret = new List<T>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (tran != null)
                        cmd.Transaction = tran;

                    if ((parameters != null) && (parameters.Length > 0))
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T o = (T)Activator.CreateInstance(typeof(T));
                            ((ICSObjectFromDB)o).GetDataFromDB(reader);
                            ret.Add(o);
                        }

                        reader.Close();
                        reader.Dispose();
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                ret = null;
                Logging(caller, BuildParameterLogString(parameters), e);
            }

            return ret;
        }

        public static List<T> GetDBObjects_Container<T>(string caller, string spName, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null, string comparingFieldName = "") where T : ICSObjectContainer, ICSObjectFromDB
        {
            EnsureICSObjectFromDB<T>();
            EnsureICSObjectContainer<T>();

            if (conn == null)
                throw new Exception("The connection is null.");

            List<T> ret = new List<T>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (tran != null)
                        cmd.Transaction = tran;

                    if ((parameters != null) && (parameters.Length > 0))
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ICSObjectContainer current = null;
                        while (reader.Read())
                        {
                            if ((current == null) || !current.HasSameKeyValue(reader, comparingFieldName))
                            {
                                if (current != null)
                                    ret.Add((T)current);

                                T o = (T)Activator.CreateInstance(typeof(T));
                                ((ICSObjectFromDB)o).GetDataFromDB(reader);
                                current = (ICSObjectContainer)o;
                            }
                            else
                            {
                                current.AddItem(reader);
                            }
                        }

                        if (current != null)
                            ret.Add((T)current);

                        reader.Close();
                        reader.Dispose();
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                ret = null;
                Logging(caller, BuildParameterLogString(parameters), e);
            }

            return ret;
        }

        public static DataTable GetDBDataTable(string caller, string spName, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null)
        {
            if (conn == null)
                throw new Exception("The connection is null.");

            DataTable ret = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (tran != null)
                        cmd.Transaction = tran;

                    if ((parameters != null) && (parameters.Length > 0))
                        cmd.Parameters.AddRange(parameters);

                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(ret);
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                ret = null;
                Logging(caller, BuildParameterLogString(parameters), e);
            }

            return ret;
        }

        public static List<ListItem> GetDBListItems(string caller, string spName, string nameField, string valueField, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null)
        {
            return GetDBListItems(caller, spName, new string[] { nameField }, " ", new string[] { valueField }, " ", conn, parameters, tran);
        }

        public static List<ListItem> GetDBListItems(string caller, string spName, string[] nameFields, string nameSeperator, string[] valueFields, string valueSeperator, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null)
        {
            if (conn == null)
                throw new Exception("The connection is null.");

            List<ListItem> ret = new List<ListItem>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (tran != null)
                        cmd.Transaction = tran;

                    if ((parameters != null) && (parameters.Length > 0))
                        cmd.Parameters.AddRange(parameters);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListItem item = new ListItem();
                        item.Text = BuildTextString(reader, nameFields, nameSeperator);
                        item.Value = BuildTextString(reader, valueFields, valueSeperator);
                        ret.Add(item);
                    }

                    reader.Close();
                    reader.Dispose();

                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                ret = null;
                Logging(caller, BuildParameterLogString(parameters), e);
            }

            return ret;
        }

        public static List<T> GetDBValueList<T>(string caller, string spName, string fieldName, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null)
        {
            EnsureValueType<T>();

            if (conn == null)
                throw new Exception("The connection is null.");

            List<T> ret = new List<T>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (tran != null)
                        cmd.Transaction = tran;

                    if ((parameters != null) && (parameters.Length > 0))
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (typeof(T) == typeof(string))
                                ret.Add((T)(object)(((string)reader[fieldName]).Trim()));
                            else
                                ret.Add((T)reader[fieldName]);
                        }

                        reader.Close();
                        reader.Dispose();
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                ret = null;
                Logging(caller, BuildParameterLogString(parameters), e);
            }

            return ret;
        }

        private static string BuildTextString(SqlDataReader reader, string[] fields, string seperator)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string field in fields)
            {
                if (!HasColumn(reader, field))
                    throw new Exception(string.Format("Reader column [{0}] does not exist."));

                string value = reader.IsDBNull(reader.GetOrdinal(field)) ? "" : reader[field].ToString();
                if (builder.Length > 0)
                    builder.Append(seperator);
                builder.Append(value);
            }

            return builder.ToString();
        }

        public static bool WriteDataToDB(string caller, string spName, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null)
        {
            if (conn == null)
                throw new Exception("The connection is null.");

            bool ret = true;
            try
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (tran != null)
                        cmd.Transaction = tran;

                    if ((parameters != null) && (parameters.Length > 0))
                        cmd.Parameters.AddRange(parameters);

                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                ret = false;
                Logging(caller, BuildParameterLogString(parameters), e);
            }

            return ret;
        }

        public static int WriteDataToDBWithID(string caller, string spName, SqlConnection conn, SqlParameter[] parameters = null, SqlTransaction tran = null)
        {
            if (conn == null)
                throw new Exception("The connection is null.");

            if ((parameters.Length < 1) || (parameters.Last().Direction != ParameterDirection.Output))
                throw new Exception("The last parameter must be an output parameter.");

            int ret;
            try
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (tran != null)
                        cmd.Transaction = tran;

                    if ((parameters != null) && (parameters.Length > 0))
                        cmd.Parameters.AddRange(parameters);

                    cmd.ExecuteNonQuery();

                    ret = StringToInt(cmd.Parameters[cmd.Parameters.Count - 1].Value.ToString());

                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                ret = -1;
                Logging(caller, BuildParameterLogString(parameters), e);
            }

            return ret;
        }

        public static string BuildParameterLogString(SqlParameter[] parameters)
        {
            if (parameters == null)
                return "";

            StringBuilder builder = new StringBuilder();
            foreach (SqlParameter param in parameters)
            {
                builder.Append(ToXMLString(param.ParameterName.Remove(0, 1), (param.Value == null) ? "" : param.Value.ToString()));
            }

            return builder.ToString();
        }

        private static void EnsureCCBHObject<T>()
        {
            T o = (T)Activator.CreateInstance(typeof(T));

            CSObject o1 = o as CSObject;

            if (o1 == null)
                throw new InvalidCastException("The type of the object you pass in is not CCBHObject.");
        }

        private static void EnsureICSObjectFromDB<T>()
        {
            T o = (T)Activator.CreateInstance(typeof(T));

            ICSObjectFromDB o1 = o as ICSObjectFromDB;

            if (o1 == null)
                throw new InvalidCastException("The type of the object you pass in is not ICSObjectFromDB.");
        }

        private static void EnsureICSObjectContainer<T>()
        {
            T o = (T)Activator.CreateInstance(typeof(T));

            ICSObjectContainer o1 = o as ICSObjectContainer;

            if (o1 == null)
                throw new InvalidCastException("The type of the object you pass in is not ICSObjectFromDB.");
        }
        #endregion Database

        #region validation
        public const string RE_EmailAddr = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public static bool ValidateFormat_Email(string email)
        {
            if (IsEmpty(email))
                return false;

            return Regex.IsMatch(email.Trim(),
                //@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", 
                                 RE_EmailAddr,
                                 RegexOptions.IgnorePatternWhitespace);
        }
        #endregion validation
   }
}
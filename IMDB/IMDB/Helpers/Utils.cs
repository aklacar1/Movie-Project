using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Helpers
{
    public static class Utils
    {
        #region Query Native SQL
        public static Object SqlQueryScalar(this DbContext db, CommandType type, RawSqlString SQL, params object[] parameters)
        {
            var conn = db.Database.GetDbConnection();
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = type;
                    cmd.CommandText = SQL.Format;
                    if (parameters != null && parameters.Length > 0)
                    {
                        if (parameters[0].GetType() == typeof(string))
                        {
                            int i = 0;
                            foreach (var param in parameters)
                            {
                                DbParameter p = cmd.CreateParameter();
                                p.ParameterName = "@p" + i++;
                                p.DbType = DbType.String;
                                p.Value = param;
                                cmd.Parameters.Add(p);
                            }
                        }
                        else
                        {
                            {
                                foreach (SqlParameter item in parameters as SqlParameter[])
                                {
                                    DbParameter p = cmd.CreateParameter();
                                    p.DbType = item.DbType;
                                    p.ParameterName = item.ParameterName;
                                    p.Value = item.Value;
                                    cmd.Parameters.Add(p);
                                }
                            }
                        }
                    }
                    Object o = cmd.ExecuteScalar();
                    return o;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public static DbDataReader SqlQuery(this DbContext db, CommandType type, RawSqlString SQL, params object[] parameters)
        {
            var conn = db.Database.GetDbConnection();
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = type;
                    cmd.CommandText = SQL.Format;
                    if (parameters != null && parameters.Length > 0)
                    {
                        if (parameters[0].GetType() == typeof(string))
                        {
                            int i = 0;
                            foreach (var param in parameters)
                            {
                                DbParameter p = cmd.CreateParameter();
                                p.ParameterName = "@p" + i++;
                                p.DbType = DbType.String;
                                p.Value = param;
                                cmd.Parameters.Add(p);
                            }
                        }
                        else
                        {
                            {
                                foreach (SqlParameter item in parameters as SqlParameter[])
                                {
                                    DbParameter p = cmd.CreateParameter();
                                    p.DbType = item.DbType;
                                    p.ParameterName = item.ParameterName;
                                    p.Value = item.Value;
                                    cmd.Parameters.Add(p);
                                }
                            }
                        }
                    }
                    DbDataReader o = cmd.ExecuteReader();
                    return o;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public static IDictionary<T, U> SqlQuery<T, U>(this DbContext db, CommandType type, RawSqlString SQL, string Key, string Value, params object[] parameters)
        {
            var conn = db.Database.GetDbConnection();
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    #region Set CMD
                    cmd.CommandType = type;
                    cmd.CommandText = SQL.Format;
                    #region Set Params
                    if (parameters != null && parameters.Length > 0)
                    {
                        if (parameters[0].GetType() == typeof(string))
                        {
                            int i = 0;
                            foreach (var param in parameters)
                            {
                                DbParameter p = cmd.CreateParameter();
                                p.ParameterName = "@p" + i++;
                                p.DbType = DbType.String;
                                p.Value = param;
                                cmd.Parameters.Add(p);
                            }
                        }
                        else
                        {
                            {
                                foreach (SqlParameter item in parameters as SqlParameter[])
                                {
                                    DbParameter p = cmd.CreateParameter();
                                    p.DbType = item.DbType;
                                    p.ParameterName = item.ParameterName;
                                    p.Value = item.Value;
                                    cmd.Parameters.Add(p);
                                }
                            }

                        }
                    }
                    #endregion
                    #endregion
                    var rtnList = new Dictionary<T, U>();
                    object val = new object();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rtnList.Add((T)reader[Key], (U)reader[Value]);
                        }
                    }
                    return rtnList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public static ICollection<T> SqlQuery<T>(this DbContext db, CommandType type, RawSqlString SQL, Dictionary<string, string> columnMapping = null, params object[] parameters) where T : new()
        {
            var conn = db.Database.GetDbConnection();
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    #region Set CMD
                    cmd.CommandType = type;
                    cmd.CommandText = SQL.Format;
                    #region Set Params
                    if (parameters != null && parameters.Length > 0)
                    {
                        if (parameters[0].GetType() == typeof(string))
                        {
                            int i = 0;
                            foreach (var param in parameters)
                            {
                                DbParameter p = cmd.CreateParameter();
                                p.ParameterName = "@p" + i++;
                                p.DbType = DbType.String;
                                p.Value = param;
                                cmd.Parameters.Add(p);
                            }
                        }
                        else
                        {
                            {
                                foreach (SqlParameter item in parameters as SqlParameter[])
                                {
                                    DbParameter p = cmd.CreateParameter();
                                    p.DbType = item.DbType;
                                    p.ParameterName = item.ParameterName;
                                    p.Value = item.Value;
                                    cmd.Parameters.Add(p);
                                }
                            }

                        }
                    }
                    #endregion
                    #endregion
                    var rtnList = new List<T>();
                    object val = new object();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rtnList.Add(ParseGenericData<T>(reader, columnMapping));
                        }
                    }
                    return rtnList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public static ICollection<T> SqlQuery<T>(this DbContext db, CommandType type, RawSqlString SQL, params object[] parameters)
        {
            var conn = db.Database.GetDbConnection();
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    #region Set CMD
                    cmd.CommandType = type;
                    cmd.CommandText = SQL.Format;
                    #region Set Params
                    if (parameters != null && parameters.Length > 0)
                    {
                        if (parameters[0].GetType() == typeof(string))
                        {
                            int i = 0;
                            foreach (var param in parameters)
                            {
                                DbParameter p = cmd.CreateParameter();
                                p.ParameterName = "@p" + i++;
                                p.DbType = DbType.String;
                                p.Value = param;
                                cmd.Parameters.Add(p);
                            }
                        }
                        else
                        {
                            {
                                foreach (SqlParameter item in parameters as SqlParameter[])
                                {
                                    DbParameter p = cmd.CreateParameter();
                                    p.DbType = item.DbType;
                                    p.ParameterName = item.ParameterName;
                                    p.Value = item.Value;
                                    cmd.Parameters.Add(p);
                                }
                            }

                        }
                    }
                    #endregion
                    #endregion
                    var rtnList = new List<T>();
                    object val = new object();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rtnList.Add((T)reader.GetValue(0));
                        }
                    }
                    return rtnList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        private static T ParseGenericData<T>(DbDataReader dr, Dictionary<string, string> columnMapping = null) where T : new()
        {
            T model = new T();
            object val = default(object);
            var propts = typeof(T).GetProperties();
            #region Get Column Names To Avoid Errors
            HashSet<String> hs = new HashSet<String>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < dr.FieldCount; i++)
            {
                hs.Add(dr.GetName(i));
            }
            #endregion
            #region Mapping of Columns
            foreach (var l in propts)
            {
                if (hs.Contains(l.Name) || (columnMapping != null && columnMapping.ContainsKey(l.Name) && hs.Contains(columnMapping[l.Name])))
                {
                    val = columnMapping == null ? dr[l.Name] : dr[columnMapping[l.Name]];
                    if (val == DBNull.Value)
                        l.SetValue(model, null);
                    else
                        l.SetValue(model, val);
                }
            }
            #endregion
            return model;
        }
        #endregion
    }
}

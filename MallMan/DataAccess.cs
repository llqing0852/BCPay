using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public delegate void HandleDataReader(DbDataReader reader);

public static class DataAccess
{
    public static void ExecuteReader(string sql, HandleDataReader dataReaderHandler, List<MySqlParameter> parameters = null)
    {
        using (var conn = Get_connection())
        {
            var cmd = new MySqlCommand(sql, conn);

            if (parameters != null && parameters.Count > 0)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            conn.Open();
            dataReaderHandler(cmd.ExecuteReader());
        }
    }

    public static DataTable ExcuteDataTableReader(string sql)
    {
        var dt = new DataTable();
        ExecuteReader(sql, reader =>
        {
            dt.Load(reader);
        });

        return dt;
    }

    public static DataSet ExecuteStoredProcedure(string procedureName, List<MySqlParameter> parameters = null)
    {
        using (var conn = Get_connection())
        {
            var cmd = new MySqlCommand
            {
                CommandText = procedureName,
                CommandType = CommandType.StoredProcedure,
                Connection = conn
            };

            if (parameters != null)
                foreach (var param in parameters)
                    cmd.Parameters.Add(param);

            var adapter = new MySqlDataAdapter(cmd);
            var ds = new DataSet();
            conn.Open();
            adapter.Fill(ds);
            return ds;
        }
    }

    public static int ExecuteStoredProcedureNonQuery(string procedureName, List<MySqlParameter> parameters = null)
    {
        using (var conn = Get_connection())
        {
            var cmd = new MySqlCommand
            {
                CommandText = procedureName,
                CommandType = CommandType.StoredProcedure,
                Connection = conn
            };

            if (parameters != null)
                foreach (var param in parameters)
                    cmd.Parameters.Add(param);
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
    }

    public static T ExecuteScalar<T>(string sql, List<MySqlParameter> parameters = null)
    {
        using (var conn = Get_connection())
        {
            var cmd = new MySqlCommand(sql, conn);

            if (parameters != null && parameters.Count > 0)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            conn.Open();
            var result = cmd.ExecuteScalar();

            if (result is DBNull)
            {
                return default(T);
            }
            return (T)result;
        }
    }

    public static int ExecuteNonQuery(string sql, List<MySqlParameter> parameters = null)
    {
        using (var conn = Get_connection())
        {
            var cmd = new MySqlCommand(sql, conn);

            if (parameters != null && parameters.Count > 0)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            conn.Open();

            return cmd.ExecuteNonQuery();
        }
    }

    private static MySqlConnection Get_connection()
    {
        return new MySqlConnection("server=156.237.190.247;port=13306;database=shopx5;user=payuser;password=P@ssw0rd!2;SslMode=none;");
    }
}
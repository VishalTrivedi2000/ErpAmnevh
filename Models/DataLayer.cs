using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AMNEVH.Models
{
    public class DataLayer
    {
        string connectionString = "";

        #region
        internal DataTable select(string procedure, string connectionString)
        {
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 999999;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(table);
            }
            return table;
        }
        internal DataTable select(SqlParameter[] parameters, string procedure, string connectionString)
        {
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = 999999;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(table);
            }
            return table;
        }
        internal async Task<int> InsertAndGetIdentity(SqlParameter[] parameters, string procedure, SqlConnection connection, SqlTransaction transaction)
        {
            int pk = 0;
            await Task.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand(procedure, connection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 999999;
                    pk = cmd.ExecuteNonQuery();
                }
            }).ConfigureAwait(false);

            return pk;
        }
        public async Task<List<SqlParameter>> GetStoredProcedureParameters(string storedProcedureName, string connectionString)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(storedProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(command);
                    foreach (SqlParameter parameter in command.Parameters)
                    {
                        if (parameter.Direction != ParameterDirection.ReturnValue)
                        {
                            parameters.Add(parameter);
                        }
                    }
                    connection.Close();
                }
            }).ConfigureAwait(false);
            return parameters;
        }
        internal async Task<int> InsertAndGetIdentity(SqlParameter[] parameters, string procedure, string connectionString)
        {
            int pk = 0;
            await Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(procedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);
                        cmd.CommandTimeout = 999999;
                        pk = cmd.ExecuteNonQuery();
                    }
                }
            }).ConfigureAwait(false);

            return pk;
        }
        public List<SqlParameter> GetStoredProcedureParameters(string storedProcedureName, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(storedProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(command);
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (SqlParameter parameter in command.Parameters)
            {
                if (parameter.Direction != ParameterDirection.ReturnValue)
                {
                    parameters.Add(parameter);
                }
            }
            return parameters;
        }
        public List<SqlParameter> GetStoredProcedureParameters(string storedProcedureName, SqlConnection connection, SqlTransaction sqlTransaction)
        {
            SqlCommand command = new SqlCommand(storedProcedureName, connection, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(command);
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (SqlParameter parameter in command.Parameters)
            {
                if (parameter.Direction != ParameterDirection.ReturnValue)
                {
                    parameters.Add(parameter);
                }
            }
            return parameters;
        }
        internal async Task<int> InsertAndGetIdentity(SqlParameter[] parameters, string procedure, SqlConnection connection)
        {
            int pk = 0;
            await Task.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand(procedure, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandTimeout = 999999;
                    pk = cmd.ExecuteNonQuery();
                }
            }).ConfigureAwait(false);

            return pk;
        }
        internal int insertAndGetIdentity(SqlParameter[] parameters, string procedure, SqlConnection connection, SqlTransaction transaction)
        {
            int pk = 0;
            using (SqlCommand cmd = new SqlCommand(procedure, connection, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = 999999;
                pk = cmd.ExecuteNonQuery();
            }
            return pk;
        }
        internal int insertAndGetIdentity(SqlParameter[] parameters, string procedure, SqlConnection connection)
        {
            int pk = 0;
            using (SqlCommand cmd = new SqlCommand(procedure, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = 999999;
                pk = cmd.ExecuteNonQuery();
            }
            return pk;
        }
        #endregion


        public DataLayer() {
            connectionString = ConfigurationManager.AppSettings.Get("AMNEVH");
        }

        internal DataTable getTable(string procedure, SqlParameter[] sp)
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString)) {
                if (conn.State==ConnectionState.Closed)
                {
                    conn.Open();    
                }
                SqlCommand cmd = new SqlCommand(procedure, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sp);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                return dt;
            }
        }

        internal DataTable getTableQ(string query)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                return dt;
            }
        }
    }
}
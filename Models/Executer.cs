using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AMNEVH.Models
{
    public class Paras
    {
        private Para para;
        public Paras(Para para)
        {
            this.para = para;
        }
        internal Para getPara()
        {
            return para;
        }
    }
    public class Para
    {
        public string Key;
        public string Value;
        public Para(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }
    public class Query
    {
        internal int returnKey;
        internal SqlTransaction usedTransaction;
        internal SqlConnection usedConnection;
        internal bool isSuccess;
        internal bool isTransactional;

        public string ErrorMessage { get; internal set; }

        internal void Close()
        {
            this.usedConnection.Close();
        }
        internal void Commit()
        {
            this.usedTransaction.Commit();
        }
        internal void RollBack()
        {
            this.usedTransaction.Rollback();
        }
    }
    public class Executer
    {
        DataLayer dataLayer;
        public Executer()
        {
            dataLayer = new DataLayer();
        }
        public static T getObject<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        internal List<T> select<T>(string procedure, string connectionString)
        {
            List<T> list = new List<T>();
            DataTable dataTable = dataLayer.select(procedure, connectionString);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string[] header = new string[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    header[i] = dataTable.Columns[i].ToString();
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    T temp = getObject<T>();
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        try
                        {
                            if (temp.GetType().GetProperty(header[j]) != null)
                            {
                                temp.GetType().GetProperty(header[j]).SetValue(temp, dataTable.Rows[i][header[j]].ToString());
                            }
                        }
                        catch
                        {
                        }
                    }
                    list.Add(temp);
                }
            }
            return list;
        }
        internal List<T> select<T>(Paras[] paras, string procedure, string connectionString)
        {
            List<T> list = new List<T>();
            if (paras != null && paras.Length > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paras.Length];
                for (int i = 0; i < paras.Length; i++)
                {
                    parameters[i] = new SqlParameter("@" + paras[i].getPara().Key, paras[i].getPara().Value);
                }
                DataTable dataTable = dataLayer.select(parameters, procedure, connectionString);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    string[] header = new string[dataTable.Columns.Count];
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        header[i] = dataTable.Columns[i].ToString();
                    }
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        T temp = getObject<T>();
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            try
                            {
                                if (temp.GetType().GetProperty(header[j]) != null)
                                {
                                    temp.GetType().GetProperty(header[j]).SetValue(temp, dataTable.Rows[i][header[j]].ToString());
                                }
                            }
                            catch
                            {
                            }
                        }
                        list.Add(temp);
                    }
                }
            }
            return list;
        }
        internal async Task<Query> InsertAndGetIdentityAsync<T>(T obj, string procedure, string connectionString)
        {
            Query query = new Query();
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure, connectionString));
            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure, connectionString)).ConfigureAwait(false);
                if (primaryKey != 0)
                {
                    query.returnKey = primaryKey;
                    query.isSuccess = true;
                }
                else
                {
                    query.isSuccess = false;
                }
            }
            return query;
        }
        internal async Task<Query> InsertAndGetIdentityAsync<T>(T obj, string procedure, string connectionString, bool havingTransaction)
        {
            Query query = new Query();
            SqlTransaction transaction = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                await sqlConnection.OpenAsync().ConfigureAwait(false);
            }
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure, sqlConnection));

            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                if (havingTransaction)
                {
                    transaction = sqlConnection.BeginTransaction();
                    int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure, sqlConnection, transaction)).ConfigureAwait(false);
                    if (primaryKey != 0)
                    {
                        query.isTransactional = true;
                        query.returnKey = primaryKey;
                        query.isSuccess = true;
                        query.usedTransaction = transaction;
                        query.usedConnection = sqlConnection;
                    }
                }
                else
                {
                    try
                    {
                        int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure, sqlConnection)).ConfigureAwait(false);

                        if (primaryKey != 0)
                        {
                            query.isTransactional = false;
                            query.returnKey = primaryKey;
                            query.isSuccess = true;
                        }
                    }
                    catch (Exception e)
                    {
                        query.isSuccess = false;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
            return query;
        }
        internal Query insertAndGetIdentity(Paras[] paras, string procedure, string connectionString, bool havingTransaction)
        {
            Query query = new Query();
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                if (havingTransaction)
                {
                    SqlTransaction transaction = connection.BeginTransaction();
                    if (paras != null && paras.Length > 0)
                    {
                        SqlParameter[] parameters = new SqlParameter[paras.Length];
                        for (int i = 0; i < paras.Length; i++)
                        {
                            parameters[i] = new SqlParameter("@" + paras[i].getPara().Key, paras[i].getPara().Value);
                        }
                        int primaryKey = dataLayer.insertAndGetIdentity(parameters, procedure, connection, transaction);
                        if (primaryKey != 0)
                        {
                            query.isTransactional = true;
                            query.returnKey = primaryKey;
                            query.isSuccess = true;
                            query.usedTransaction = transaction;
                            query.usedConnection = connection;
                        }
                    }
                }
                else
                {
                    SqlTransaction transaction = connection.BeginTransaction();
                    if (paras != null && paras.Length > 0)
                    {
                        try
                        {
                            SqlParameter[] parameters = new SqlParameter[paras.Length];
                            for (int i = 0; i < paras.Length; i++)
                            {
                                parameters[i] = new SqlParameter("@" + paras[i].getPara().Key, paras[i].getPara().Value);
                            }
                            int primaryKey = dataLayer.insertAndGetIdentity(parameters, procedure, connection);
                            if (primaryKey != 0)
                            {
                                query.isTransactional = false;
                                query.returnKey = primaryKey;
                                query.isSuccess = true;
                            }
                        }
                        catch (Exception e)
                        {
                            query.isSuccess = false;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
            return query;
        }
        internal async Task<Query> InsertAndGetIdentityAsync<T>(T obj, string procedure, SqlConnection connection, SqlTransaction transaction)
        {
            Query query = new Query();
            List<SqlParameter> paraList = await Task.Run(() => dataLayer.GetStoredProcedureParameters(procedure, connection, transaction));
            if (paraList != null && paraList.Count > 0)
            {
                SqlParameter[] parameters = new SqlParameter[paraList.Count];
                for (int i = 0; i < paraList.Count; i++)
                {
                    if (obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)) != null)
                    {
                        parameters[i] = new SqlParameter(paraList[i].ParameterName, obj.GetType().GetProperty(paraList[i].ParameterName.Substring(1)).GetValue(obj));
                    }
                }
                int primaryKey = await Task.Run(() => dataLayer.InsertAndGetIdentity(parameters, procedure, connection, transaction)).ConfigureAwait(false);
                if (primaryKey != 0)
                {
                    query.isTransactional = true;
                    query.returnKey = primaryKey;
                    query.isSuccess = true;
                    query.usedTransaction = transaction;
                    query.usedConnection = connection;
                }
            }
            else
            {
                query.ErrorMessage = "Paras Are Not Passed";
            }
            return query;
        }
    }
}
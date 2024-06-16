using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AMNEVH.Models
{
    public class BusinessLayer
    {
        DataLayer dataLayer;
        public BusinessLayer() { 
        dataLayer = new DataLayer();
        }
      
        internal DataTable getTable(string procedure, string userName, string password)
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@userName",userName),
                new SqlParameter("@password",password)
            };
            return dataLayer.getTable(procedure,para);
        }
        internal DataTable getTableQ(string query)
        {
          
            return dataLayer.getTableQ(query);
        }
    }
}
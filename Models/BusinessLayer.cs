using AMNEVH.Models.GeoRegionEntities;
using AMNEVH.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace AMNEVH.Models
{
    public class BusinessLayer
    {
        DataLayer dataLayer;
        Executer executer; string AMNEVH;
        public BusinessLayer()
        {
            dataLayer = new DataLayer();
            AMNEVH = ConfigurationManager.AppSettings.Get("AMNEVH");
            executer = new Executer();
        }

        internal dynamic getDepartment()
        {
            DataTable dt = dataLayer.getTableQ("   select DeptID, Department from DepartmentMaster order by Department ");
            return CommonMethods.convertDTToSelectListItem(dt);
        }

        internal dynamic getDesignation(string DeptID)
        {
            DataTable dt = dataLayer.getTableQ("   select DesigID as desigId,Designation as desigName from DesigMaster where DeptID='"+ DeptID + "' order by Designation  ");
            return CommonMethods.convertDTToSelectListItem(dt);
        }

        internal Employee getEmployee(string id)
        {
            Paras[] paras = new Paras[]
        {
                new Paras(new Para("key",id))
        };
            List<Employee> employees = executer.select<Employee>(paras, "SP_GetEmployeeById", this.AMNEVH);
            if (employees != null && employees.Count > 0)
            {
                return employees[0];
            }
            else return null;
        }

        internal HighSchool getHighSchool(string id)
        {
            Paras[] paras = new Paras[]
          {
                new Paras(new Para("key",id))
          };
            List<HighSchool> highSchools = executer.select<HighSchool>(paras, "SP_GetEmployeeHighSchool", this.AMNEVH);
            if (highSchools != null && highSchools.Count > 0)
            {
                return highSchools[0];
            }
            else return null;
        }

        internal List<SelectListItem> getState()
        {
            DataTable dt = dataLayer.getTableQ("with cte as(select  pincode, state,R=ROW_NUMBER() over (partition by state order by state) from pincode_all  )select state as stateId, state as stateName from cte where R=1 order by state ");
            return CommonMethods.convertDTToSelectListItem(dt);
        }

        internal DataTable getTable(string procedure, string userName, string password)
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@userName",userName),
                new SqlParameter("@password",password)
            };
            return dataLayer.getTable(procedure, para);
        }
        internal DataTable getTableQ(string query)
        {

            return dataLayer.getTableQ(query);
        }
    }
}
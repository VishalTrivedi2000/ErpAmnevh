using AMNEVH.Models;
using AMNEVH.Models.GeoRegionEntities;
using AMNEVH.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMNEVH.Controllers
{
    public class AjaxHandlerController : Controller
    {
        // GET: AjaxHandler
        Executer executer;
        string AMNEVH;
        public AjaxHandlerController()
        {
            executer = new Executer();
            AMNEVH = ConfigurationManager.AppSettings.Get("AMNEVH");
        }
        public string GetStateAccCountry(string key)
        {
            Paras[] paras = new Paras[]
            {
                new Paras(new Para("key",key))
            };
            List<State> state = executer.select<State>(paras, "SP_GetStateAccCountry", this.AMNEVH);
            string optionString = CommonMethods.convertListOptionString<State>(state, "stateId", "stateName");
            return optionString;
        }
        public string GetDistrictAccState(string key)
        {
            Paras[] paras = new Paras[]
            {
                new Paras(new Para("key",key))
            };
            List<District> district = executer.select<District>(paras, "SP_GetDistrictAccState", this.AMNEVH);
            string optionString = CommonMethods.convertListOptionString<District>(district, "districtId", "districtName");
            return optionString;
        }

        public string GetDesignationAccDepartment(string key)
        {
            Paras[] paras = new Paras[]
           {
                new Paras(new Para("key",key))
           };
            List<Designation> designations = executer.select<Designation>(paras, "SP_GetDesignationAccDepartment", this.AMNEVH);
            string optionString = CommonMethods.convertListOptionString<Designation>(designations, "desigId", "desigName");
            return optionString;
        }
        public string GetBlockAccDistrict(string key)
        {
            Paras[] paras = new Paras[]
            {
                new Paras(new Para("key",key))
            };
            List<Block> block = executer.select<Block>(paras, "SP_GetBlockAccDistrict", this.AMNEVH);
            string optionString = CommonMethods.convertListOptionString<Block>(block, "blockId", "blockName");
            return optionString;
        }  
        public string GetCityAccState(string key)
        {
            Paras[] paras = new Paras[]
            {
                new Paras(new Para("key",key))
            };
            List<City> cities = executer.select<City>(paras, "SP_GetCityAccState", this.AMNEVH);
            string optionString = CommonMethods.convertListOptionString<City>(cities, "cityId", "cityName");
            return optionString;
        }
    }
}
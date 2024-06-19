using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMNEVH.Models
{
    public class CommonMethods
    {
        public static List<SelectListItem> convertDTToSelectListItem(DataTable dataTable)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "--Select--"
            });

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = dataRow[0].ToString();
                    item.Text = dataRow[1].ToString();
                    list.Add(item);
                }
            }
            return list;
        }
        public static List<SelectListItem> convertDTToSelectListItem(DataTable dataTable, int col1, int col2)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { 
            Value = "0",
            Text = "--Select--"
        });
         
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = dataRow[col1].ToString();
                    item.Text = dataRow[col2].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        internal static dynamic convertListOptionString<T>(List<T> obj, string value, string text, string select = "Select")
        {
            string optionList = "<option value='0'>--" + select + "--</option>";
            if (obj != null && obj.Count > 0)
            {
                if (obj[0].GetType().GetProperty(value) != null && obj[0].GetType().GetProperty(text) != null)
                {

                    for (int i = 0; i < obj.Count; i++)
                    {
                        optionList += "<option value='" + obj[i].GetType().GetProperty(value).GetValue(obj[i]).ToString() + "'>" + obj[i].GetType().GetProperty(text).GetValue(obj[i]).ToString() + "</option>";
                    }
                }
                else
                {
                    throw new Exception("Property are not matched with object");
                }
            }
            return optionList;
        }

        internal static List<SelectListItem> convertListToSelectListItem<T>(List<T> obj, string value, string text)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "0", Text = "--Select--", Selected = true });
            for (int i = 0; i < obj.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = obj[i].GetType().GetProperty(text).GetValue(obj[i]).ToString();
                item.Value = obj[i].GetType().GetProperty(value).GetValue(obj[i]).ToString();
                list.Add(item);
            }
            return list;
        }
    }
}
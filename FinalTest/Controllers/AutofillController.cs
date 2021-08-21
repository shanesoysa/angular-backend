using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using FinalTest.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace FinalTest.Controllers
{
    public class AutofillController : ApiController
    {



        public String Getbrand()
        {
            string query = @"select distinct reference from items";

            DataSet ds = new DataSet(); 
            DataTable table = new DataTable();
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            //string json = JsonConvert.SerializeObject(ds, Formatting.Indented);

            //String[] spearator = { "[" };

            //// using the method 
            //String[] strlist = ds.ToString().Split(spearator,
            //   StringSplitOptions.RemoveEmptyEntries);


            ////  return strlist[0];

            String Seperator = "\"";

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            //JSONString = JSONString.Replace(Seperator, "");
            return JSONString;


            //var JSONString = new StringBuilder();
            //if (table.Rows.Count > 0)
            //{
            //    JSONString.Append("[");
            //    for (int i = 0; i < table.Rows.Count; i++)
            //    {
            //        JSONString.Append("{");
            //        for (int j = 0; j < table.Columns.Count; j++)
            //        {
            //            if (j < table.Columns.Count - 1)
            //            {
            //                JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
            //            }
            //            else if (j == table.Columns.Count - 1)
            //            {
            //                JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
            //            }
            //        }
            //        if (i == table.Rows.Count - 1)
            //        {
            //            JSONString.Append("}");
            //        }
            //        else
            //        {
            //            JSONString.Append("},");
            //        }
            //    }
            //    JSONString.Append("]");
            //}
            //return JSONString.ToString();


        }






    }
}

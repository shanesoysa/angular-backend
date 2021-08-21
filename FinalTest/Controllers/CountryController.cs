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

namespace FinalTest.Controllers
{
    public class CountryController : ApiController
    {


        public HttpResponseMessage Get()
        {
            string query = @"select distinct country from items";
            DataTable table = new DataTable("table");
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);

        }

        public HttpResponseMessage Get(String val)
        {
            string query = @"select * from buyer_rep";
            DataTable table = new DataTable("table");
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);

        }






    }
}

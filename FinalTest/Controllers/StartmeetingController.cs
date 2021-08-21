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
    public class StartmeetingController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"select * from start_meeting";
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




        public string Post(StartMeeting stm)
        {
            try
            {
               

                string query = @"

                  insert into start_meeting(mid, empid1, empid2, empid3, buyerid, rep1, rep2, season, remark, status) 
                  values('" + stm.mid + "', '" + stm.empid1 + "', '" + stm.empid2 + "', '" + stm.empid3 + "', '" + stm.buyerid + "', '" + stm.rep1 + "', '" + stm.rep2 + "', '" + stm.season + "', '" + stm.remark + "', 'T')";
                
                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Meeting  " + stm.mid + " is Sucessfully Added";

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex;
            }
        }
    }
}

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
    public class SeasonController : ApiController
    {
        string UPDquery = "";

        // GET api/<controller>/5
        public HttpResponseMessage Get()
        {
            string query = @"select * from seasons_master;";
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

        // POST api/<controller>
        public String Post(SeasonMaster season)
        {

            try
            {
                string query = @"

                  insert into seasons_master (season_name, season_year)
                    values('" + season.Name + "', '" + season.Year + "')";


                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Season " + season.Name + " Sucessfully Added";

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex;
            }


        }

        // PUT api/<controller>/5
        public string Put(SeasonMaster season)
        {

            try
            {

                string UPDquery= @"UPDATE seasons_master SET  season_name='"+season.Name+"', season_year='"+season.Year+"' WHERE (sem='"+ season.Sem+ "');";
                
                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(UPDquery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Season  " + season.Sem + " is Sucessfully Updated!! ";

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine + UPDquery;
            }



        }

        public string Delete(int Id)
        {
            try
            {

                string Deletequery = @"DELETE FROM seasons_master WHERE sem="+Id+ "";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(Deletequery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                //  return "Season  " + season.Sem + " is Sucessfully Deleted!! ";
                return "season deleted";

            }
            catch (Exception ex)
            {
                // return "Error = " + Environment.NewLine + ex + Environment.NewLine + Deletequery;
                return "season delete error";
            }


        }

    }
}
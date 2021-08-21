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
    public class EmployeemasterController : ApiController
    {

        public HttpResponseMessage Get()
        {
            string query = @"select * from employee_master";
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


        public string Post(Employee rp)
        {
            try
            {
                string query = @"

                  insert into employee_master (name, designation, email, contact, emp_id)
                    values('" + rp.name + "', '" + rp.designation + "', '" + rp.email + "', '" + rp.contact + "', '" + rp.emp_id + "')";


                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Employee " + rp.name + " is Sucessfully Added";

            }catch(Exception ex)
            {
                return "Error = "+Environment.NewLine+ex;
            }
        }


        public string Put(Employee rp)
        {

            try
            {

                string UPDquery = @"UPDATE `bnndb`.`employee_master` SET  `name`='"+rp.name+ "', `designation`='" + rp.designation + "', `email`='" + rp.email + "', `contact`='" + rp.contact + "', `emp_id`='" + rp.emp_id + "' WHERE (`EID`='" + rp.BID + "');";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(UPDquery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Employee   " + rp.BID + " is Sucessfully Updated!! ";

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine;
            }



        }

        public string Delete(int Id)
        {
            try
            {

                string Deletequery = @"DELETE FROM employee_master WHERE EID=" + Id + "";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(Deletequery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "employee deleted " + Id;

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine;
            }


        }
    }
}

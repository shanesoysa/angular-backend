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
    [RoutePrefix("api/supplier")]
    public class SupplierController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"select * from supplier_master";
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


        public string Post(Supplier rp)
        {
            try
            {
                string query = @"

                  INSERT INTO `bnndb`.`supplier_master` (`name`, `address`, `contact`, `email`, `supplier_code`) VALUES ('"+rp.name + "', '"+rp.address + "', '" + rp.contact + "', '" + rp.email + "', '" + rp.supplier_code + "');";


                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Supplier " + rp.name + " is Sucessfully Added";

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex;
            }
        }


        public string Put(Supplier rp)
        {

            try
            {

                string UPDquery = @"UPDATE `bnndb`.`supplier_master` SET  `name`='"+rp.name + "', `address`='" + rp.address + "', `contact`='" + rp.contact + "', `email`='" + rp.email + "', `supplier_code`='" + rp.supplier_code + "' WHERE (`supid`='" + rp.supid + "');";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(UPDquery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Supplier   " + rp.supid + " is Sucessfully Updated!! ";

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

                string Deletequery = @"DELETE FROM supplier_master WHERE supid=" + Id + "";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(Deletequery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "supplier deleted " + Id;

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine;
            }


        }

        [HttpGet]
        [Route("supcode/{uid}")]
        public HttpResponseMessage supcode(int uid)
        {
            string query = @"SELECT
                            supplier_master.supplier_code
                            FROM
                            user_master
                            INNER JOIN supplier_master ON user_master.uname = supplier_master.`name`
                            WHERE
                            user_master.usid = '" + uid+"'";

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
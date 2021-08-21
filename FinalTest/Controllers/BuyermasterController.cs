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
    public class BuyermasterController : ApiController
    {
        public string Post(BuyerMaster bm)
        {
            try
            {
                string query = @"

        insert into buyer_master (designation, buyer_name, brand, address, contact1, contact2, email1, email2)
        values('" + bm.designation + "', '" + bm.buyer_name + "', '" + bm.brand + "', '" + bm.address + "', '" + bm.contact1 + "', '" + bm.contact2 + "', '" + bm.email1 + "', '" + bm.email2 + "')";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Buyer "+bm.buyer_name+" is Sucessfully Added";

            }catch(Exception ex)
            {
                return "Error "+Environment.NewLine+ex;
            }

        }

        public HttpResponseMessage Get()
        {
            string query = @"select * from buyer_master";
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


        public string Put(BuyerMaster bm)
        {

            try
            {
                
                string UPDquery = @"UPDATE `bnndb`.`buyer_master` SET `designation`='"+bm.designation+"', `buyer_name`='"+bm.buyer_name+"', `brand`='"+bm.brand+"', `address`='"+bm.address+"', `contact1`='"+bm.contact1+"', `contact2`='"+bm.contact2+"', `email1`='"+bm.email1+ "', `email2`='" + bm.email2 + "' WHERE (`BID`='" + bm.BID + "')";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(UPDquery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Buyer   " + bm.BID + " is Sucessfully Updated!! ";

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

                string Deletequery = @"DELETE FROM buyer_master WHERE BID=" + Id + "";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(Deletequery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "buyer deleted "+Id;

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine;
            }


        }




    }
}

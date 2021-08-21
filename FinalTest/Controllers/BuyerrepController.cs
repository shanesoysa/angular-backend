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
    public class BuyerrepController : ApiController
    {

        public string Post(Buyerrep rp)
        {
            try
            {
                string query = @"

       insert into buyer_rep (title, rep_name, brand, buyer_name, location, designation, contact, email)
        values('" + rp.title + "', '" + rp.rep_name + "', '" + rp.brand + "', '" + rp.buyer_name + "', '" + rp.location + "', '" + rp.designation + "', '" + rp.contact + "', '" + rp.email + "')";


                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Buyer Rep "+rp.buyer_name+" Sucessfully Added";
            }catch(Exception ex)
            {
                return "error"+Environment.NewLine+ex;
            }
        }

        public HttpResponseMessage Getbrand()
        {
            string query = @"select distinct brand from buyer_rep";

            DataTable table = new DataTable();
            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        public string Put(Buyerrep rp)
        {

            try
            {

                string UPDquery = @"UPDATE `bnndb`.`buyer_rep` SET  `title`='" + rp.title + "', `rep_name`='" + rp.rep_name + "', `brand`='" + rp.brand + "', `buyer_name`='" + rp.buyer_name + "', `location`='" + rp.location + "', `designation`='" + rp.designation + "', `contact`='" + rp.contact + "', `email`='" + rp.email + "' WHERE (`reid`='" + rp.reid + "');";


                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(UPDquery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Buyer Rep   " + rp.reid + " is Sucessfully Updated!! ";

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

                string Deletequery = @"DELETE FROM buyer_rep WHERE reid=" + Id + "";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(Deletequery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Buyer rep deleted " + Id;

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine;
            }


        }



        }
    }

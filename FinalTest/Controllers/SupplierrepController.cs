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
    public class SupplierrepController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"select * from supplier_item";
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


        public string Post(SupplierRep rp)
        {
            try
            {
                string query = @"
                INSERT INTO `bnndb`.`supplier_item` (`supcode`, `construction`, `weave`, `gsm`, `cutwidth`, `composition`, `mecfin`, `chemfin`, `price`, `mill`, `country`, `rep1`, `rep2`, `rep3`, `image_path`, `approve`, `status`, `fabriccode`, `season`) VALUES ('"+ rp.supcode + "', '" + rp.construction + "', '" + rp.weave + " ', '" + rp.gsm + "', '" + rp.cutwidth + "', '" + rp.composition + "', '" + rp.mecfin + "', '" + rp.chemfin + "', '" + rp.price + "', '" + rp.mill + "', '" + rp.country + "', '" + rp.rep1 + "', '" + rp.rep2 + "', '" + rp.rep3 + "', '" + rp.image_path + "', '" + rp.approve + "', '" + rp.status+ "', '" + rp.fabriccode + "', '" + rp.season+ "');";


                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "supplier Item " + rp.supcode + " is Sucessfully Added";

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex;
            }
        }


        public string Put(SupplierRep rp)
        {

            try
            {

                string UPDquery = @"UPDATE `bnndb`.`supplier_item` SET `supcode`='" + rp.supcode + "', `construction`='" + rp.construction + "', `weave`='" + rp.weave + "', `gsm`='" + rp.gsm + "', `cutwidth`='" + rp.cutwidth + "', `composition`='" + rp.composition + "', `mecfin`='" + rp.mecfin + "', `chemfin`='" + rp.chemfin + "', `price`='" + rp.price + "', `mill`='" + rp.mill + "', `country`='" + rp.country + "', `rep1`='" + rp.rep1 + "', `rep2`='" + rp.rep2 + "', `rep3`='" + rp.rep3 + "', `approve`='" + rp.approve + "', `status`='" + rp.status + "', `fabriccode`='" + rp.fabriccode + "', `season`='" + rp.season + "' WHERE (`supid`='" + rp.supid + "');";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(UPDquery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "supplier Item " + rp.supcode + " is Sucessfully Updated!! ";

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

                string Deletequery = @"DELETE FROM supplier_item WHERE supid=" + Id + "";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(Deletequery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "supplier item deleted " + Id;

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine;
            }


        }

    }
}
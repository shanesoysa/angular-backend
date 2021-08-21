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
    [RoutePrefix("api/items")]

    public class ItemsController : ApiController
    {

        string UPDquery = "";
        public string Post(Items itm)
        {
            try
            {
                string query = @"

                  insert into items (reference, construction, weave, gsm, itwidth, composition, finish, price, country, mill, image_path, chemical_finish, Row_No, Rack_No, fds_repo, test_repo, Status, Insert_date, supplier_code, droot, remarks, season)
                    values('" + itm.reference + "', '" + itm.construction + "', '" + itm.weave + "', '" + itm.gsm + "', '" + itm.itwidth + "', '" + itm.composition + "', '" + itm.finish + "', '" + itm.price + "', '" + itm.country + "', '" + itm.mill + "', '" + itm.image_path + "', '" + itm.chemical_finish + "', '" + itm.Row_No + "', '" + itm.Rask_No + "', '" + itm.fds_repo + "', '" + itm.test_repo + "', '" + itm.Status + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt") + "', '" + itm.supplier_code + "', '" + itm.droot + "', '" + itm.remarks + "', '" + itm.season + "')";


                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Fabric  " + itm.reference + " is Sucessfully Added";

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex;
            }
        }

        [HttpPost]
        [Route("sadd")]
        public string SupplierItemAdd(Items itm)
        {
            try
            {
                string query = @"

                  insert into items (sup_fabric_code, construction, weave, gsm, itwidth, composition, finish, price, country, mill, image_path, chemical_finish, Row_No, Rack_No, fds_repo, test_repo, Status, Insert_date, supplier_code, droot, remarks, season)
                    values('" + itm.reference + "', '" + itm.construction + "', '" + itm.weave + "', '" + itm.gsm + "', '" + itm.itwidth + "', '" + itm.composition + "', '" + itm.finish + "', '" + itm.price + "', '" + itm.country + "', '" + itm.mill + "', '" + itm.image_path + "', '" + itm.chemical_finish + "', '" + itm.Row_No + "', '" + itm.Rask_No + "', '" + itm.fds_repo + "', '" + itm.test_repo + "', '" + itm.Status + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt") + "', '" + itm.supplier_code + "', '" + itm.droot + "', '" + itm.remarks + "', '" + itm.season + "')";


                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Fabric  " + itm.reference + " is Sucessfully Added";

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex;
            }
        }


        public string Put(Items itm)
        {

            try
            {
               
                 //UPDquery = @"

                
                 //   update items set reference = '"+itm.reference+"', construction = '"+itm.construction+ "', weave = '" + itm.weave + "', gsm = '" + itm.gsm + "', itwidth = '" + itm.itwidth + "',  composition = '" + itm.composition + "',  finish = '" + itm.finish + "', price = '" + itm.price + "',  country = '" + itm.country + "', mill = '" + itm.mill + "', chemical_finish = '" + itm.chemical_finish + "', Row_No = '" + itm.Row_No + "', Rack_No = '" + itm.Rask_No + "', fds_repo = '" + itm.fds_repo + "', Status = '" + itm.Status + "', supplier_code = '" + itm.supplier_code + "',  droot = '" + itm.droot + "', remarks = '" + itm.remarks + "',  season = '" + itm.season + "' where itid = " + itm.itid + " ";

                //UPDquery = @"update items set reference = 'abcd' where itid = 260 ";



                UPDquery = @"update items set reference = '" + itm.reference + "', construction = '" + itm.construction + "', weave = '" + itm.weave + "', gsm = '" + itm.gsm + "', itwidth = '" + itm.itwidth + "',  composition = '" + itm.composition + "',  finish = '" + itm.finish + "',  country = '" + itm.country + "', mill = '" + itm.mill + "', chemical_finish = '" + itm.chemical_finish + "' where itid = " + itm.itid + " ";


                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(UPDquery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Fabric  " + itm.reference + " is Sucessfully Updated!! ";
               
            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine + UPDquery;
            }


            
        }

        [HttpGet]
        [Route("nasf")]
        public HttpResponseMessage Notaaprovedsupplierfabric()
        {
            string query = @"SELECT
                            items.itid,
                            items.sup_fabric_code,
                            items.reference,
                            items.construction,
                            items.weave,
                            items.gsm,
                            items.itwidth,
                            items.composition,
                            items.finish,
                            items.price,
                            items.country,
                            items.mill,
                            items.image_path,
                            items.chemical_finish,
                            items.Row_No,
                            items.Rack_No,
                            items.fds_repo,
                            items.test_repo,
                            items.`Status`,
                            items.Insert_date,
                            items.supplier_code,
                            items.droot,
                            items.remarks,
                            items.season,
                            supplier_master.`name`,
                            supplier_master.email,
                            supplier_master.contact,
                            supplier_master.address
                            FROM
                            items
                            INNER JOIN supplier_master ON items.supplier_code = supplier_master.supplier_code
                            WHERE
	                            items.`Status` = 's'";

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

        [HttpGet]
        [Route("nasf/{fbcode}")]
        public HttpResponseMessage Notaaprovedfabricsingle(string fbcode)
        {
            string query = @"SELECT
                            items.itid,
                            items.sup_fabric_code,
                            items.reference,
                            items.construction,
                            items.weave,
                            items.gsm,
                            items.itwidth,
                            items.composition,
                            items.finish,
                            items.price,
                            items.country,
                            items.mill,
                            items.image_path,
                            items.chemical_finish,
                            items.Row_No,
                            items.Rack_No,
                            items.fds_repo,
                            items.test_repo,
                            items.`Status`,
                            items.Insert_date,
                            items.supplier_code,
                            items.droot,
                            items.remarks,
                            items.season
                            FROM
                            items
                            WHERE
                            items.sup_fabric_code = '"+ fbcode + "' LIMIT 1";

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

        [HttpPut]
        [Route("nasf/itemupdate")]
        public string Updatesupfab(Items item)
        {

            try
            {


                UPDquery = @"update items set reference = '" + item.reference + "', status = '" + item.Status + "' where sup_fabric_code = '" + item.supplier_fc + "' ";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(UPDquery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Item "+ item.supplier_fc + " Updated";

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine + UPDquery;
            }



        }




        public HttpResponseMessage Get()
        {
            string query = @"select * from items";
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


        public HttpResponseMessage Get(int id)
        {
            string query = @"select * from items where itid = "+id+" limit 1";
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

        public string Delete(int Id)
        {
            try
            {

                string Deletequery = @"DELETE FROM items WHERE itid=" + Id + "";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(Deletequery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "item deleted " + Id;

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine;
            }


        }


        

        [HttpGet]
        [Route("supplierImages/{supcode}")]
        public HttpResponseMessage supplieritemsingle(string supcode)
        {
            string query = @"SELECT
                            items.image_path
                            FROM
                            items
                            WHERE
                            items.supplier_code = '" + supcode + "' AND items.image_path != ''";

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

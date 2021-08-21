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
    [RoutePrefix("api/meetingmaster")]
    public class MeetingmasterController : ApiController
    {
        public string Post(Meetingmaster mt)
        {
            try
            {
                string query = @"

                  insert into meeting_master (name, purpose, location, country, fromdate, todate)
                    values('" + mt.name + "', '" + mt.purpose + "', '" + mt.location + "', '" + mt.country + "', '" + mt.from_date + "', '"+mt.todate+"')";


                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Meeting " + mt.name + " is Sucessfully Added";

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex;
            }
        }



        public HttpResponseMessage Get()
        {
            string query = @"select * from meeting_master;";
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
        [Route("startdetails/{mid}")]
        public HttpResponseMessage startdetails(int mid)
        {
            //string query = @"SELECT
            //                employee_master.`name` AS empname,
            //                employee_master.designation AS empdesignation,
            //                buyer_master.buyer_name AS buyername,
            //                buyer_rep.rep_name AS buerrepname,
            //                buyer_rep.brand,
            //                start_meeting.season,
            //                start_meeting.remark,
            //                start_meeting.mid
            //                FROM
            //                start_meeting
            //                INNER JOIN employee_master ON start_meeting.empid1 = employee_master.EID AND start_meeting.empid2 = employee_master.EID AND start_meeting.empid3 = employee_master.EID
            //                INNER JOIN buyer_master ON start_meeting.buyerid = buyer_master.BID
            //                INNER JOIN buyer_rep ON start_meeting.rep1 = buyer_rep.reid AND start_meeting.rep2 = buyer_rep.reid
            //                WHERE
            //                start_meeting.mid = '"+mid+"'";

            string query = @"SELECT
                                employee_master.`name` AS empname,
                                employee_master.designation AS empdesignation,
	                            buyer_master.buyer_name AS buyername,
	                            buyer_rep.rep_name AS buerrepname,
	                            buyer_rep.brand,
	                            start_meeting.season,
	                            start_meeting.remark,
	                            start_meeting.mid
                            FROM

                                start_meeting
                            LEFT JOIN employee_master ON start_meeting.empid1 = employee_master.EID
                            AND start_meeting.empid2 = employee_master.EID
                            AND start_meeting.empid3 = employee_master.EID
                            LEFT JOIN  buyer_master ON start_meeting.buyerid = buyer_master.BID
                            LEFT JOIN  buyer_rep ON start_meeting.rep1 = buyer_rep.reid
                            AND start_meeting.rep2 = buyer_rep.reid
                            WHERE 
                            start_meeting.mid = '"+mid+"'";

         

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
        [Route("meetingdetails/{mid}")]
        public HttpResponseMessage meetingdetails(int mid)
        {
            string query = @"SELECT
                            start_meeting.remark,
                            meeting_master.mid,
                            meeting_master.`name`,
                            meeting_master.purpose,
                            meeting_master.location,
                            meeting_master.country,
                            meeting_master.fromdate,
                            meeting_master.todate
                            FROM
                            start_meeting
                            INNER JOIN user_master ON user_master.usid = start_meeting.buyerid
                            INNER JOIN meeting_master ON start_meeting.mid = meeting_master.mid
                            WHERE
                            user_master.usid = '"+mid+"'";

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
        [Route("fabricscan/{mid}")]
        public HttpResponseMessage fabricscan(int mid)
        {
            string query = @"SELECT
                            items.image_path,
                            items.itid,
                            fabric_scan.fbsc,
                            fabric_scan.fabric_code,
                            fabric_scan.qty
                            FROM
                            fabric_scan
                            INNER JOIN items ON fabric_scan.fabric_code = items.reference
                            WHERE
                            fabric_scan.mid = '" + mid+"'";

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

        public string Put(Meetingmaster mt)
        {

            try
            {

                string UPDquery = @"UPDATE `bnndb`.`meeting_master` SET `name`='"+mt.name+ "', `purpose`='" + mt.purpose + "', `location`='" + mt.location + "', `country`='" + mt.country + "', `fromdate`='" + mt.from_date + "', `todate`='" + mt.todate + "' WHERE (`mid`='" + mt.mid + "');";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(UPDquery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Employee   " + mt.mid + " is Sucessfully Updated!! ";

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

                string Deletequery = @"DELETE FROM meeting_master WHERE mid=" + Id + "";

                DataTable table = new DataTable();
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                using (var cmd = new MySqlCommand(Deletequery, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "meeting deleted " + Id;

            }
            catch (Exception ex)
            {
                return "Error = " + Environment.NewLine + ex + Environment.NewLine;
            }


        }

     
       




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using FinalTest.Models;
using System.Text;
using System.Security.Cryptography;

namespace FinalTest.Controllers
{
    [RoutePrefix("api/login")]

    public class LoginController : ApiController
    {
        String datasets = "";



        public string Post(LoginUser lgu)
        {
            try
            {
                

                string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {
                    conn.Open();
                    string pw = ComputeHash(lgu.pass);

                    string sql = "select usid, authority, name from user_master where uname = '" + lgu.uname+@"' AND password = '"+ pw + @"' AND status='T' ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        datasets = rdr.GetString("usid") +","+ rdr.GetString("authority") + "," + rdr.GetString("name");
                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                    //return "error" + Environment.NewLine + ex + Environment.NewLine;
                }

                conn.Close();
                return datasets;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public HttpResponseMessage Get()
        {
            string query = @"SELECT
                            user_master.usid,
                            user_master.`name`,
                            user_master.uname,
                            user_master.email,
                            user_master.authority,
                            user_master.`status`
                            FROM
                            user_master
                            ";

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
        [Route("getuser/{id}")]
        public HttpResponseMessage getuser(int id)
        {
            string query = @"SELECT
                            user_master.`name`,
                            user_master.uname,
                            user_master.hint,
                            user_master.email,
                            user_master.authority,
                            user_master.IdentfyNo
                            FROM
                            user_master
                            WHERE
                            user_master.usid = '"+id+"' limit 1";

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
        [Route("changepw")]
        public string changepw(LoginUser lo)

        {
            string pw = ComputeHash(lo.pass);
            string re_pw = ComputeHash(lo.repass);


            if (pw == re_pw)
            {
                try
                {

                    string UPDquery = @"update user_master set password = '" + pw + "' where usid = '" + lo.id + "' ";

                    DataTable table = new DataTable();
                    using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                    using (var cmd = new MySqlCommand(UPDquery, con))
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(table);
                    }

                    return "Password Updated";

                }
                catch (Exception ex)
                {
                    return "Error!";
                }

            }
            else
            {
                return "Password Do not match!";
            }

        }


        [HttpPost]
        [Route("newuser")]
        public string newuser(LoginUser lo)
        {
                try
                {
                    string pw = ComputeHash(lo.pass);

                    string UPDquery = @"INSERT INTO `bnndb`.`user_master` (`name`, `uname`, `password`, `hint`, `email`, `authority`, `status`) VALUES ('"+lo.name+ "', '" + lo.uname + "', '" + pw + "', '" + lo.hint + "', '" + lo.email + "', '" + lo.auth + "', '" + lo.status + "');";

                    DataTable table = new DataTable();
                    using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                    using (var cmd = new MySqlCommand(UPDquery, con))
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(table);
                    }

                    return "New User "+lo.name+" Added";

                }
                catch (Exception ex)
                {
                    return "Error!" + Environment.NewLine + ex + Environment.NewLine;
                }

      

        }


        [HttpPost]
        [Route("updateusr")]
        public string updateusr(LoginUser lo)
        {
            try
            {

                if (lo.pass =="")
                {
                    string UPDquery = @"UPDATE `bnndb`.`user_master` SET `name`='" + lo.name + "', `uname`='" + lo.uname + "', `hint`='" + lo.hint + "', `email`='" + lo.email + "', `authority`='" + lo.auth + "', `status`='" + lo.status + "' WHERE (`usid`='" + lo.id + "');";
                    DataTable table = new DataTable();
                    using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                    using (var cmd = new MySqlCommand(UPDquery, con))
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(table);
                    }

                    return "User " + lo.name + " Updated";

                }
                else
                {
                    string pw = ComputeHash(lo.pass);

                    string UPDquery = @"UPDATE `bnndb`.`user_master` SET `name`='" + lo.name + "', `uname`='" + lo.uname + "', `password`='" + pw + "', `email`='" + lo.email + "', `authority`='" + lo.auth + "', `status`='" + lo.status + "' WHERE (`usid`='" + lo.id + "');";

                    DataTable table = new DataTable();
                    using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
                    using (var cmd = new MySqlCommand(UPDquery, con))
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(table);
                    }

                    return "User " + lo.name + " Updated";

                }

            }
            catch (Exception ex)
            {
                return "Error!" + Environment.NewLine + ex + Environment.NewLine;
            }



        }

        static string ComputeHash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }



    }
}

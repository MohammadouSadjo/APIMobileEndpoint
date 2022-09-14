using APIMobileEndpoint.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace APIMobileEndpoint.Controllers
{
    [ApiController]
    [Route("authentication")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult PostAuthentication([FromBody] LoginInformations loginInfo)
        {
            string query = "select * from clients where login='"+ loginInfo.login +"'";
            string truePassword;
            User user = new User();

            string uid = "root";
            string pwd = "";
            string sqlDataSource = "SERVER=localhost;PORT=3306;" +
                 "DATABASE=agrotech;" +
                 "UID=" + uid + ";PASSWORD=" + pwd;

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();

                    myReader.Read();

                    if (!myReader.HasRows)
                    {
                        myReader.Close();
                        mycon.Close();
                        return NotFound("Not Found");
                    }
                    else
                    {
                        truePassword = (string)myReader["mot_de_passe"];

                        if (truePassword != loginInfo.password)
                        {
                            myReader.Close();
                            mycon.Close();
                            return NotFound("Not Found");
                        }
                        else
                        {
                            user.id = (int)myReader["id_client"];
                            user.nom = (string)myReader["nom"];
                            user.prenom = (string)myReader["prenom"];
                            user.email = (string)myReader["email"];

                            myReader.Close();
                            mycon.Close();
                            return new JsonResult(user);
                        }
                    }

                }
            }

        }

        [HttpPost("loginadmin")]
        public IActionResult PostAuthenticationAdmin([FromBody] LoginInformations loginInfo)
        {
            string query = "select * from administrateurs where login='" + loginInfo.login + "'";
            string truePassword;
            User user = new User();

            string uid = "root";
            string pwd = "";
            string sqlDataSource = "SERVER=localhost;PORT=3306;" +
                 "DATABASE=agrotech;" +
                 "UID=" + uid + ";PASSWORD=" + pwd;

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();

                    myReader.Read();

                    if (!myReader.HasRows)
                    {
                        myReader.Close();
                        mycon.Close();
                        return NotFound("Not Found");
                    }
                    else
                    {
                        truePassword = (string)myReader["mot_de_passe"];

                        if (truePassword != loginInfo.password)
                        {
                            myReader.Close();
                            mycon.Close();
                            return NotFound("Not Found");
                        }
                        else
                        {
                            user.id = (int)myReader["id_administrateur"];
                            user.nom = (string)myReader["nom"];
                            user.prenom = (string)myReader["prenom"];
                            user.email = (string)myReader["email"];
                            user.role = (Boolean)myReader["role"];

                            myReader.Close();
                            mycon.Close();
                            return new JsonResult(user);
                        }
                    }

                }
            }

        }
    }
}

using APIMobileEndpoint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace APIMobileEndpoint.Controllers
{
    [ApiController]
    [Route("authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult PostAuthentication([FromBody] LoginInformations loginInfo)
        {
            string query = "select * from clients where login='"+ loginInfo.login +"'";
            string truePassword;
            User user = new User();

            

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(_configuration.GetConnectionString("MyCon")))
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

            

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(_configuration.GetConnectionString("MyCon")))
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
                            user.role = (string)myReader["role"];

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

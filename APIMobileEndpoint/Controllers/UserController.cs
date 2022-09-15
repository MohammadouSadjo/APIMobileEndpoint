using APIMobileEndpoint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace APIMobileEndpoint.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("getusers")]
        public IActionResult GetUsers()
        {
            string query = "select * from clients";
            List<User> listUser = new List<User>();

            

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(_configuration.GetConnectionString("MyCon")))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        listUser.Add(new User()
                        {
                            id = (int)myReader.GetValue(0),
                            nom = (string)myReader.GetValue(1),
                            prenom = (string)myReader.GetValue(2),
                            email = (string)myReader.GetValue(5)
                        });
                    }

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(listUser);
        }

        [HttpGet("getadmins")]
        public IActionResult GetAdmins()
        {
            string role2 = "admin";
            string query = "select * from administrateurs where role='"+role2+"'";
            List<User> listUser = new List<User>();

            

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(_configuration.GetConnectionString("MyCon")))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        listUser.Add(new User()
                        {
                            id = (int)myReader.GetValue(0),
                            nom = (string)myReader.GetValue(1),
                            prenom = (string)myReader.GetValue(2),
                            email = (string)myReader.GetValue(5),
                            role = (string)myReader.GetValue(6)
                        });
                    }

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(listUser);
        }

        [HttpPost("adduser")]
        public IActionResult AddUser([FromBody] User user)
        {

            string query = "insert into clients(nom,prenom,login,mot_de_passe,email) values('" + user.nom + "', '" + user.prenom + "', '" + user.login + "', '" + user.mot_de_passe + "','"+ user.email +"')";

            

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(_configuration.GetConnectionString("MyCon")))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(user);
        }

    }
}

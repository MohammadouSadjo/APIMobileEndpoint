using APIMobileEndpoint.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace APIMobileEndpoint.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        [HttpGet("getusers")]
        public IActionResult GetUsers()
        {
            string query = "select * from clients";
            List<User> listUser = new List<User>();

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

    }
}

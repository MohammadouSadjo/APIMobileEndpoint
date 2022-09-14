using APIMobileEndpoint.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace APIMobileEndpoint.Controllers
{
    [ApiController]
    [Route("intervenant")]
    public class IntervenantController : Controller
    {
        [HttpGet("getintervenants")]
        public IActionResult GetIntervenant()
        {
            string query = "select * from intervenants";
            List<Intervenant> listIntervenant = new List<Intervenant>();

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
                        listIntervenant.Add(new Intervenant()
                        {
                            id = (int)myReader.GetValue(0),
                            nom = (string)myReader.GetValue(1),
                            prenom = (string)myReader.GetValue(2),
                            telephone = (string)myReader.GetValue(3),
                            email = (string)myReader.GetValue(4)
                        });
                    }

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(listIntervenant);
        }

        [HttpPost("addintervenant")]
        public IActionResult AddIntervenant([FromBody] Intervenant intervenant)
        {

            string query = "insert into intervenants(nom,prenom,telephone,email) values('" + intervenant.nom + "', '" + intervenant.prenom + "', '" + intervenant.telephone + "', '"+ intervenant.email +"')";

            string uid = "root";
            string password = "";
            string sqlDataSource = "SERVER=localhost;PORT=3306;" +
                 "DATABASE=agrotech;" +
                 "UID=" + uid + ";PASSWORD=" + password;

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(intervenant);
        }
    }
}

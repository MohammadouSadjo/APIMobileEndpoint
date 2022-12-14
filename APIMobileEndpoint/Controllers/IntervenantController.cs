using APIMobileEndpoint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace APIMobileEndpoint.Controllers
{
    [ApiController]
    [Route("intervenant")]
    public class IntervenantController : Controller
    {
        private readonly IConfiguration _configuration;

        public IntervenantController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("getintervenants")]
        public IActionResult GetIntervenant()
        {
            string query = "select * from intervenants";
            List<Intervenant> listIntervenant = new List<Intervenant>();

            

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(_configuration.GetConnectionString("MyCon")))
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

            return new JsonResult(intervenant);
        }

        [HttpPut("updateintervenant/{intervenantId}")]
        public IActionResult UpdateIntervenant([FromBody] Intervenant intervenant, int intervenantId)
        {
            string query = "update intervenants set nom='" + intervenant.nom + "',prenom='" + intervenant.prenom + "',telephone='" + intervenant.telephone + "',email='"+ intervenant.email +"' where id_intervenant='" + intervenantId + "'";

            

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

            intervenant.id = intervenantId;
            return new JsonResult(intervenant);
        }

        [HttpDelete("deleteintervenant/{intervenantId}")]
        public IActionResult DeleteIntervenant(int intervenantId)
        {
            string query = "delete from intervenants where id_intervenant='" + intervenantId + "'";

            

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

            return new JsonResult("Deleted");
        }
    }
}

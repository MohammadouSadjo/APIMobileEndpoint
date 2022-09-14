using APIMobileEndpoint.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace APIMobileEndpoint.Controllers
{
    [ApiController]
    [Route("agriculturaltask")]
    public class AgriculturalTaskController : Controller
    {
        [HttpGet("getagriculturaltask")]
        public IActionResult GetAgriculturalTask()
        {
            string query = "select * from tache_agricole";
            List<AgriculturalTask> listAgriculturalTask = new List<AgriculturalTask>();

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
                        listAgriculturalTask.Add(new AgriculturalTask()
                        {
                            id = (int)myReader.GetValue(0),
                            equipmentId = (int)myReader.GetValue(1),
                            intervenantId = (int)myReader.GetValue(2),
                            description = (string)myReader.GetValue(3),
                            dateExecution = (DateTime)myReader.GetValue(4)
                        });
                    }

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(listAgriculturalTask);
        }

        [HttpPost("addagriculturaltask")]
        public IActionResult AddAgriculturalTask([FromBody] AgriculturalTask agriculturalTask)
        {

            string query = "insert into tache_agricole(equipement_id,intervenant_id,description,date_execution) values('" + agriculturalTask.equipmentId + "', '" + agriculturalTask.intervenantId + "', '" + agriculturalTask.description + "', '" + agriculturalTask.dateExecution + "')";

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

            return new JsonResult(agriculturalTask);
        }
    }
}

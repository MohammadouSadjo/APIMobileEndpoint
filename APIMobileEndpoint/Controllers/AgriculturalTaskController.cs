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

            string uid = "sadjo";
            string pwd = "1209*huaweiPhone";
            string sqlDataSource = "SERVER=projectdevmysql.mysql.database.azure.com;PORT=3306;" +
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

            string uid = "sadjo";
            string pwd = "1209*huaweiPhone";
            string sqlDataSource = "SERVER=projectdevmysql.mysql.database.azure.com;PORT=3306;" +
                 "DATABASE=agrotech;" +
                 "UID=" + uid + ";PASSWORD=" + pwd;

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

        [HttpPut("updateagriculturaltask/{agriculturalTaskId}")]
        public IActionResult UpdateAgriculturalTask([FromBody] AgriculturalTask agriculturalTask, int agriculturalTaskId)
        {
            string query = "update tache_agricole set equipement_id='" + agriculturalTask.equipmentId + "',intervenant_id='" + agriculturalTask.intervenantId + "',description='" + agriculturalTask.description + "',date_execution='" + agriculturalTask.dateExecution + "' where id_tache_agricole='" + agriculturalTaskId + "'";

            string uid = "sadjo";
            string pwd = "1209*huaweiPhone";
            string sqlDataSource = "SERVER=projectdevmysql.mysql.database.azure.com;PORT=3306;" +
                 "DATABASE=agrotech;" +
                 "UID=" + uid + ";PASSWORD=" + pwd;

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

            agriculturalTask.id = agriculturalTaskId;
            return new JsonResult(agriculturalTask);
        }

        [HttpDelete("deleteagriculturaltask/{agriculturalTaskId}")]
        public IActionResult DeleteAgriculturalTask(int agriculturalTaskId)
        {
            string query = "delete from tache_agricole where id_tache_agricole='" + agriculturalTaskId + "'";

            string uid = "sadjo";
            string pwd = "1209*huaweiPhone";
            string sqlDataSource = "SERVER=projectdevmysql.mysql.database.azure.com;PORT=3306;" +
                 "DATABASE=agrotech;" +
                 "UID=" + uid + ";PASSWORD=" + pwd;

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

            return new JsonResult("Deleted");
        }
    }
}

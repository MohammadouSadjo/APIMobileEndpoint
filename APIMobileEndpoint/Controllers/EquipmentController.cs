using APIMobileEndpoint.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace APIMobileEndpoint.Controllers
{
    [ApiController]
    [Route("equipment")]
    public class EquipmentController : Controller
    {
        [HttpGet("getequipments/{clientId}")]
        public IActionResult GetEquipments(int clientId)
        {
            string query = "select * from equipements where client_id='" + clientId + "'";
            List<Equipment> listEquipment = new List<Equipment>();

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

                    while(myReader.Read())
                    {
                        listEquipment.Add(new Equipment()
                        {
                            id = (int)myReader.GetValue(0),
                            clientId = (int)myReader.GetValue(1),
                            nom = (string)myReader.GetValue(2),
                            description = (string)myReader.GetValue(3)
                        });
                    }

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(listEquipment);
        }

        [HttpGet("getpositionandmovementtime/{equipmentId}")]
        public IActionResult GetPositionAndMovementTime(int equipmentId)
        {
            string query = "select * from metriques where equipement_id='" + equipmentId + "'";
            Metrique metrique = new Metrique();

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

                    metrique.id = (int)myReader["id_metrique"];
                    metrique.equipmentId = (int)myReader["equipement_id"];
                    metrique.movementTime = (string)myReader["temps_fonctionnement"];
                    metrique.position = (string)myReader["position"];
                    metrique.datetime = (DateTime)myReader["date_heure"];

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(metrique);
        }
    }

    
}

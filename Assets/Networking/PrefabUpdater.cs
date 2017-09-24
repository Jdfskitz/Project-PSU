using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using MySql.Data.Types;

public class PrefabUpdater : PrefabHandler {

private string SQL_DATABASE_NAME = "Prefabs";
private string SQL_USERNAME = "unityroot";
private string SQL_PASSWORD = "!@12QWqw";
private string SQL_PORT = "3306";
private string SQL_HOST = "localhost";
public MySql.Data.MySqlClient.MySqlConnection connection;

private bool k = false;
private string connectionStr = null;

	public void prefabUpdater(GameObject tgo)
	{
		int pID = tgo.GetComponent<CombatHandler>().pID;
		 
        connectionStr = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
        MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(connection);
        
		using(connection = new MySql.Data.MySqlClient.MySqlConnection(connectionStr))
        {
			
		string updateQuery = "UPDATE `Prefabs`.`GameObjects` SET `TransformX`='@TransformX', `TransformY`='@TransformY', `TransformZ`='@TransformZ', `RotateX`='@RotateX', `RotateY`='@RotateY', `RotateZ`='@RotateZ' WHERE `id`='" + 1 + "';";

			Debug.Log("Connection");
				using(MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection))
					{
                        //command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);
                        command.Parameters.AddWithValue("@TransformX", tgo.GetComponent<CombatHandler>().transform.position.x);
                        command.Parameters.AddWithValue("@TransformY", tgo.GetComponent<CombatHandler>().transform.position.y);
                        command.Parameters.AddWithValue("@TransformZ", tgo.GetComponent<CombatHandler>().transform.position.z);

                        command.Parameters.AddWithValue("@RotateX", tgo.GetComponent<CombatHandler>().transform.rotation.x);
                        command.Parameters.AddWithValue("@RotateY", tgo.GetComponent<CombatHandler>().transform.rotation.y);
                        command.Parameters.AddWithValue("@RotateZ", tgo.GetComponent<CombatHandler>().transform.rotation.z);

						Debug.Log("Data Inserted");

						try{
						connection.Open();
                        command.ExecuteNonQuery();      
						}catch(MySql.Data.MySqlClient.MySqlException sqlEx){
				
				}
			}
		}
	}
}


	       //sqlQueue = @"GameObjects";
       /* 
	    connectionStr = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
        using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionStr))
        {
     

        try {
                
            cnn.Open();
            Debug.Log("Success");       
                
                string GameObjectsRead = "SELECT Name,GameObjectID,TransformX,TransformY,TransformZ,RotateX,RotateY,RotateZ,id,isOpen FROM GameObjects";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(GameObjectsRead, cnn);
                MySql.Data.MySqlClient.MySqlDataReader GameObjectsDB = cmd.ExecuteReader();
 
                    while(GameObjectsDB.Read())
                    {
                        int pID = tgo.GetComponent<CombatHandler>().pID;

                        string updateQuery = "UPDATE `Prefabs`.`GameObjects` SET `TransformX`='@TransformX', `TransformY`='@TransformY', `TransformZ`='@TransformZ', `RotateX`='@RotateX', `RotateY`='@RotateY', `RotateZ`='@RotateZ' WHERE `id`='" + pID + "';";
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connection);

                        cmd.Parameters.AddWithValue("@TransformX", tgo.GetComponent<CombatHandler>().transform.position.x);
                        cmd.Parameters.AddWithValue("@TransformY", tgo.GetComponent<CombatHandler>().transform.position.y);
                        cmd.Parameters.AddWithValue("@TransformZ", tgo.GetComponent<CombatHandler>().transform.position.z);

                        cmd.Parameters.AddWithValue("@RotateX", tgo.GetComponent<CombatHandler>().transform.rotation.x);
                        cmd.Parameters.AddWithValue("@RotateY", tgo.GetComponent<CombatHandler>().transform.rotation.y);
                        cmd.Parameters.AddWithValue("@RotateZ", tgo.GetComponent<CombatHandler>().transform.rotation.z);

                        cmd.BeginExecuteNonQuery();        

                        Debug.Log("Data Inserted");
					}

                GameObjectsDB.Close();     
                
            }catch(MySql.Data.MySqlClient.MySqlException sqlEx){
                    Debug.Log("Failed Connection");  
                    cnn.Close();
            }
        }
	}*/


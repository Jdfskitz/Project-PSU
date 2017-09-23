using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabUpdater : PrefabHandler {

private bool k = false;

private string connectionString = null;

	public void prefabUpdater(GameObject tgo)
	{
        connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
        MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);
        using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        {
     

        try {
                if (!k && tgo != null)
                {
                    k = true;
                    cnn.Open();
                    Debug.Log("Update Method");

                        int pID = tgo.GetComponent<CombatHandler>().pID;
                        
                        string updateQuery = "UPDATE GameObjects SET TransformX = @TransformX,TransformY = @TransformY,TransformZ = @TransformZ, RotateX = @RotateX, RotateY = @RotateY, RotateZ = @RotateZ, WHERE id = '" + pID + "' AND isOpen = '1'";

                        cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, cnn);
                        
                        cmd.Parameters.AddWithValue("@TransformX", tgo.GetComponent<CombatHandler>().transform.position.x);
                        cmd.Parameters.AddWithValue("@TransformY", tgo.GetComponent<CombatHandler>().transform.position.y);
                        cmd.Parameters.AddWithValue("@TransformZ", tgo.GetComponent<CombatHandler>().transform.position.z);

                        cmd.Parameters.AddWithValue("@RotateX", tgo.GetComponent<CombatHandler>().transform.rotation.x);
                        cmd.Parameters.AddWithValue("@RotateY", tgo.GetComponent<CombatHandler>().transform.rotation.y);
                        cmd.Parameters.AddWithValue("@RotateZ", tgo.GetComponent<CombatHandler>().transform.rotation.z);

                        cmd.CommandTimeout = 5;

                        cmd.BeginExecuteNonQuery();        
    
                        Debug.Log("Data Inserted");
                    
                }  
                k=false;
                cnn.Close();

            }
            catch(MySql.Data.MySqlClient.MySqlException sqlEx){
                    Debug.Log("Failed Connection");  
                    cnn.Close();
            }

        }
	}
}

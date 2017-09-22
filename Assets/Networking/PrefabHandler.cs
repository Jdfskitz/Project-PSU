﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class PrefabHandler : MonoBehaviour {

public string SQL_DATABASE_NAME = "";
public string SQL_USERNAME = "";
public string SQL_PASSWORD = "";
public string SQL_PORT = "";
public string SQL_HOST = "";

private string connectionString = null;
MySql.Data.MySqlClient.MySqlConnection cnn;
private string goName;
private int goID;
private float goTransformX, goTransformY, goTransformZ, goRotateX, goRotateY, goRotateZ;

public int ServerRefreshRate;

public int updateWaitTimer;
private int goIndex;

private int isOpen;
int i = 0;
bool t;
bool k = false;
int count = 0;
private GameObject go;
    private List<GameObject> goList = new List<GameObject>();

//List<int> allIds = new List<int>();



    public void Start ()
{
        //sqlQueue = @"GameObjects";
        connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
        MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);
        using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        {
     

        try {
                
            cnn.Open();
            Debug.Log("Success");       
                

                //*CREATE TABLE GameObjects (Name VARCHAR(100) NOT NULL, GameObjectID INTEGER, TransformX INTEGER, TransformY INTEGER, TransformZ INTEGER, RotateX INTEGER, RotateY INTEGER, RotateZ INTEGER); */
                //*INSERT INTO GameObjects (Name, GameObjectID, TransformX, TransformY, TransformZ, RotateX, RotateY, RotateZ) VALUES ( "" , , , , , , , ); */
                //*EX: INSERT INTO GameObjects (Name, GameObjectID, TransformX, TransformY, TransformZ, RotateX, RotateY, RotateZ) VALUES ( "Skeleton" , 1, 295, 10, 120, 0, 80, 0); */

                string GameObjectsRead = "SELECT Name,GameObjectID,TransformX,TransformY,TransformZ,RotateX,RotateY,RotateZ,id,isOpen FROM GameObjects";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(GameObjectsRead, cnn);
                MySql.Data.MySqlClient.MySqlDataReader GameObjectsDB = cmd.ExecuteReader();
 
                    while(GameObjectsDB.Read())
                    {
                        goName = (string)GameObjectsDB[0];
                        goID = (int)GameObjectsDB[1];
                        goTransformX = (float)GameObjectsDB[2];
                        goTransformY = (float)GameObjectsDB[3];
                        goTransformZ = (float)GameObjectsDB[4];
                        goRotateX = (float)GameObjectsDB[5];
                        goRotateY = (float)GameObjectsDB[6];
                        goRotateZ = (float)GameObjectsDB[7];
                        goIndex = (int)GameObjectsDB[8];
                        isOpen = (int)GameObjectsDB[9];

                        //allIds.Add((int)GameObjectsDB[1]);

                            Debug.Log("goInfo Read");

                            if(goID == 1 && isOpen == 1)
                            {
                            go = (GameObject)Instantiate(Resources.Load("Skeleton"),new Vector3(goTransformX,goTransformY,goTransformZ),Quaternion.Euler(goRotateX,goRotateY,goRotateZ));
                            go.transform.position = new Vector3(goTransformX,goTransformY,goTransformZ);
                            go.GetComponent<CombatHandler>().pID = goIndex;
                            goList.Add(go);
                            isOpen = 0;
                            Debug.Log("Object Spawned");
                            //Skeleton Spawner
                            }

                }


                GameObjectsDB.Close();     
                
            }catch(MySql.Data.MySqlClient.MySqlException sqlEx){
                    Debug.Log("Failed Connection");  
                    cnn.Close();
            }
        }
}

public void Update ()
{
        //sqlQueue = @"GameObjects";
        connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
        MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);
        using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        {
     

        try {
                if (!k && go != null)
                {
                    k = true;
                    cnn.Open();
                    Debug.Log("Update Method");

                        
                        int pID = go.GetComponent<CombatHandler>().pID;

                    foreach (GameObject go in goList)
                    {
                        string updateQuery = "UPDATE GameObjects SET TransformX = @TransformX,TransformY = @TransformY,TransformZ = @TransformZ WHERE id = " + go.GetComponent<CombatHandler>().pID;

                        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, cnn);

                        //cmd.Parameters.AddWithValue("@id", go.GetComponent<CombatHandler>().pID);
                        cmd.Parameters.AddWithValue("@TransformX", go.GetComponent<CombatHandler>().transform.position.x);
                        cmd.Parameters.AddWithValue("@TransformY", go.GetComponent<CombatHandler>().transform.position.y);
                        cmd.Parameters.AddWithValue("@TransformZ", go.GetComponent<CombatHandler>().transform.position.z);

                        cmd.BeginExecuteNonQuery();

                        //goTransformX = go.GetComponent<CombatHandler>().transform.position.x;
                        //goTransformY = go.GetComponent<CombatHandler>().transform.position.y;
                        //goTransformZ = go.GetComponent<CombatHandler>().transform.position.z;
                        StartCoroutine(WaitFunction());


                        Debug.Log("Data Inserted");

                    }
                }

                cnn.Close();

            }
            catch(MySql.Data.MySqlClient.MySqlException sqlEx){
                    Debug.Log("Failed Connection");  
                    cnn.Close();
            }
        }

    }

 


 IEnumerator WaitFunction()
 {
     yield return new WaitForSeconds(ServerRefreshRate);
     k = false;
 }

}

public class HolderClass
{
    public int goID { get; set; }
}








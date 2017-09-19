using System.Collections;
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

public void Start()
{
connectionString = "Data Source=SQL_HOST,SQL_PORT;Initial Catalog=SQL_DATABASE_NAME;User ID=SQL_USERNAME;Password=SQL_PASSWORD";
cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
}

public void Update()
{
    try {
        cnn.Open();
            //* On Start of Game Instantiate GameObjects */
            //* 1 GameObject for Every Array in SQL */
            //* Add Range Detection to Each Game Object */

            //* If SQLGRAB.ID = 1 Spawn Skeleton */
            //* If SPAWNINFO[i].model == 1; 
            //* GameObject go = (GameObject)Instantiate(Resources.Load("MyPrefab"),new Vector3(SQLGRAB.x,SQLGRAB.y,SQLGRAB.z)
            //*										,Quaternion.Euler(SQLGRAB.rotationx,SQLGRAB.rotationy,SQLGRAB.rotationz)); */
        cnn.Close();
    }
    catch
    {
    }
}


}



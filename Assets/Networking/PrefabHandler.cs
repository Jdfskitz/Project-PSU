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


private string goName;
private int goID;
private int goTransformX;
private int goTransformY;
private int goTransformZ;
private int goRotateX;
private int goRotateY;
private int goRotateZ;



public void Start()
{

connectionString = "Data Source=SQL_HOST,SQL_PORT;Initial Catalog=SQL_DATABASE_NAME;User ID=SQL_USERNAME;Password=SQL_PASSWORD";
cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

    try {
        Debug.Log("Connecting to MySQL");
        cnn.Open();
        MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);

        //*CREATE TABLE GameObjects (Name VARCHAR(100) NOT NULL, GameObjectID INTEGER, TransformY INTEGER, TransformY INTEGER, TransformZ INTEGER, RotateX INTEGER, RotateY INTEGER, RotateZ INTEGER); */



        string sql = "SELECT Name,GameObjectID,TransformX,TransformY,TransformZ,RotateX,RotateY,RotateZ FROM GameObjects";

        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, cnn);

        MySql.Data.MySqlClient.MySqlDataReader goInfo = cmd.ExecuteReader();

        
            while(goInfo.Read())
            {
                goName = (string)goInfo[0];
                goID = (int)goInfo[1];
                goTransformX = (int)goInfo[2];
                goTransformY = (int)goInfo[3];
                goTransformZ = (int)goInfo[4];
                goRotateX = (int)goInfo[5];
                goRotateY = (int)goInfo[6];
                goRotateZ = (int)goInfo[7];
            }

            //Skeleton Spawner
            if(goID == 1)
            {
            GameObject go = (GameObject)Instantiate(Resources.Load("Skeleton"),new Vector3(goTransformX,goTransformY,goTransformZ)
            									,Quaternion.Euler(goRotateX,goRotateY,goRotateZ));
            }
       
        goInfo.Close();
        cnn.Close();
    }
    catch
    {
        Debug.Log("Failed Connection");
    }


}

public void Update()
{

}


}



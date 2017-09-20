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

public Vector3 thisPosition;
private string goName;
private int goID;
public int goTransformX;
public int goTransformY;
public int goTransformZ;
public int goRotateX;
public int goRotateY;
public int goRotateZ;
private int goIndex;

private int isOpen;
private int rowNumber;

private int [] countid;
private string GameObjectsTable = "GameObjects";
private int key;
int i = 0;
int count = 0;
private GameObject go;

public void Start ()
{

connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";

cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

    try {
        Debug.Log("Connecting to MySQL");
        cnn.Open();
        Debug.Log("Success");

        MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);

        //*CREATE TABLE GameObjects (Name VARCHAR(100) NOT NULL, GameObjectID INTEGER, TransformX INTEGER, TransformY INTEGER, TransformZ INTEGER, RotateX INTEGER, RotateY INTEGER, RotateZ INTEGER); */
        //*INSERT INTO GameObjects (Name, GameObjectID, TransformX, TransformY, TransformZ, RotateX, RotateY, RotateZ) VALUES ( "" , , , , , , , ); */
        //*EX: INSERT INTO GameObjects (Name, GameObjectID, TransformX, TransformY, TransformZ, RotateX, RotateY, RotateZ) VALUES ( "Skeleton" , 1, 295, 10, 120, 0, 80, 0); */

        string sql = "SELECT Name,GameObjectID,TransformX,TransformY,TransformZ,RotateX,RotateY,RotateZ,id,isOpen FROM GameObjects";
         
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
                goIndex = (int)goInfo[8];
                isOpen = (int)goInfo[9];
//                count = (int)countCMD.ExecuteScalar();
 
                    if(goID == 1 && isOpen == 1)
                    {
                    go = (GameObject)Instantiate(Resources.Load("Skeleton"),new Vector3(goTransformX,goTransformY,goTransformZ)
                                                        ,Quaternion.Euler(goRotateX,goRotateY,goRotateZ));
                    go.GetComponent<CombatHandler>().pID = goIndex;
                    isOpen = 0;
                }else{      

                    
                }

        } 

         

        //Skeleton Spawner
        goInfo.Close();
        cnn.Close();


    }
    catch(MySql.Data.MySqlClient.MySqlException sqlEx)
    {
        Debug.Log("Failed Connection");
        cnn.Close();
        
    }

   
}


public void Update()
{
    try{
    Debug.Log("Success Update");

    cnn.Open();
    
    string sql = "SELECT Name,GameObjectID,TransformX,TransformY,TransformZ,RotateX,RotateY,RotateZ,id,isOpen FROM GameObjects";

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
                goIndex = (int)goInfo[8];
                isOpen = (int)goInfo[9];
            }

                MySql.Data.MySqlClient.MySqlCommand Update = new MySql.Data.MySqlClient.MySqlCommand("UPDATE `Prefabs`.`GameObjects` SET `TransformX`= '" + go.GetComponent<CombatHandler>().TransformX + "';" ,cnn);

    }

    catch(MySql.Data.MySqlClient.MySqlException)
    {
        Debug.Log("Failure Update");
    }
}

//void Update()
//{
//    go.transform.position = GetComponent<CombatHandler>().thisPosition;
//    string MovementQuery = "UPDATE GameObjects(TransformX,TransformY,TransformZ) VALUES (" + go.transform.position.x + "," + go.transform.position.y + "," + go.transform.position.z + ")";
//    new MySql.Data.MySqlClient.MySqlCommand(MovementQuery,cnn);
//}


/*void Update()
{
connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";

cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

    try {
        Debug.Log("Connecting to MySQL");
        cnn.Open();
        Debug.Log("Success");

        MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);

        string sql = "SELECT Name,GameObjectID,TransformX,TransformY,TransformZ,RotateX,RotateY,RotateZ FROM GameObjects";

        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, cnn);

        MySql.Data.MySqlClient.MySqlDataReader goInfo = cmd.ExecuteReader();





        goInfo.Close();
        cnn.Close();


    }
    catch(MySql.Data.MySqlClient.MySqlException sqlEx)
    {
        Debug.Log("Failed Connection");
        cnn.Close();

    }



}*/

}



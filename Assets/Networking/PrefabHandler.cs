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

public int updateWaitTimer;
private int goIndex;

private int isOpen;
private int rowNumber;

private int [] countid;
private string GameObjectsTable = "GameObjects";
private int key;
int i = 0;
bool t;
int count = 0;
private GameObject go;



public void Start ()
{
    SQLInitializer();


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

                    go.transform.position = new Vector3(goTransformX,goTransformY,goTransformZ);
                    
                    go.GetComponent<CombatHandler>().pID = goIndex;
                    isOpen = 0;

                    }

        } 

         

        //Skeleton Spawner
        goInfo.Close();


    }
    catch(MySql.Data.MySqlClient.MySqlException sqlEx)
    {
        Debug.Log("Failed Connection");
        SQLTermination();
        
    }

   
}


public void Update()
{       
        
        try{
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
        
        //MySql.Data.MySqlClient.MySqlCommand Update = new MySql.Data.MySqlClient.MySqlCommand("UPDATE `Prefabs`.`GameObjects` SET `TransformX`= '" + go.GetComponent<CombatHandler>().TransformX + "' `TransformY`= '" + go.GetComponent<CombatHandler>().TransformY + "' `TransformZ`= '" + go.GetComponent<CombatHandler>().TransformZ + "';" ,cnn);
        cmd.CommandText = "UPDATE `Prefabs`.`GameObjects` SET `TransformX`= '" + go.GetComponent<CombatHandler>().TransformX + "' `TransformY`= '" + go.GetComponent<CombatHandler>().TransformY + "' `TransformZ`= '" + go.GetComponent<CombatHandler>().TransformZ + "';" ;
        
        cmd.ExecuteReader();

        Debug.Log("Prefab position update");
        }catch(MySql.Data.MySqlClient.MySqlException sqlEx){

        }

        /*if(!t)
        {
            StartCoroutine(UpdatePosition());
            t = true;
        }*/




}



	/*public IEnumerator UpdatePosition()
	{
        SQLInitializer();

        yield return new WaitForSeconds(updateWaitTimer);
        MySql.Data.MySqlClient.MySqlCommand Update = new MySql.Data.MySqlClient.MySqlCommand("UPDATE `Prefabs`.`GameObjects` SET `TransformX`= '" + go.GetComponent<CombatHandler>().TransformX + "' `TransformY`= '" + go.GetComponent<CombatHandler>().TransformY + "' `TransformZ`= '" + go.GetComponent<CombatHandler>().TransformZ + "';" ,cnn);
        Debug.Log("Prefab Position Update");
        t = false;
        

    }*/


    public void SQLInitializer()
    {
        connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";

        cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
    }


    public void SQLTermination()
    {
        cnn.Close();
    }

}




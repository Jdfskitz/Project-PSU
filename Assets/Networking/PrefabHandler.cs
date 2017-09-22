using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;


public class PrefabHandler : MonoBehaviour {
public updateMovement updatemovement = new updateMovement();
public string SQL_DATABASE_NAME = "";
public string SQL_USERNAME = "";
public string SQL_PASSWORD = "";
public string SQL_PORT = "";
public string SQL_HOST = "";

private string connectionString = null;
MySql.Data.MySqlClient.MySqlConnection cnn;
private string goName;
private int goID;
public int goTransformX;
public int goTransformY;
public int goTransformZ;
public int goRotateX;
public int goRotateY;
public int goRotateZ;

private int ping;

public int updateWaitTimer;
private int goIndex;

private int isOpen;
int i = 0;
bool t;
bool k = false;
int count = 0;
private GameObject go;
<<<<<<< HEAD
    private List<GameObject> goList = new List<GameObject>();
    private List<int> allIds = new List<int>();
//List<int> allIds = new List<int>();
=======

>>>>>>> parent of d0e19e2... somenetworkchanges



public void Start ()
{
    ping = 5;
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
                        goTransformX = (int)GameObjectsDB[2];
                        goTransformY = (int)GameObjectsDB[3];
                        goTransformZ = (int)GameObjectsDB[4];
                        goRotateX = (int)GameObjectsDB[5];
                        goRotateY = (int)GameObjectsDB[6];
                        goRotateZ = (int)GameObjectsDB[7];
                        goIndex = (int)GameObjectsDB[8];
<<<<<<< HEAD
                        isOpen = (int)GameObjectsDB[9];

                        allIds.Add((int)GameObjectsDB[1]);
=======
                        isOpen = (int)GameObjectsDB[9];                  
                            Debug.Log("goInfo Read");
>>>>>>> parent of d0e19e2... somenetworkchanges

                            if(goID == 1 && isOpen == 1)
                            {
                            go = (GameObject)Instantiate(Resources.Load("Skeleton"),new Vector3(goTransformX,goTransformY,goTransformZ),Quaternion.Euler(goRotateX,goRotateY,goRotateZ));
                            go.transform.position = new Vector3(goTransformX,goTransformY,goTransformZ);
                            go.GetComponent<CombatHandler>().pID = goIndex;
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


 //* START REALTIME UPDATE FUNCTION */
/*public void Update ()
{
        //sqlQueue = @"GameObjects";
        connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
        MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);
        using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        {
     

        try {
<<<<<<< HEAD

 


 


                if (!k && go != null)
                {
                    k = true;
                    cnn.Open();
                    Debug.Log("Update Method");

                        
                        

                    foreach (GameObject thisPrefab in goList)
                    {
                        int pID = thisPrefab.GetComponent<CombatHandler>().pID;

                        //for(int j = 1; j <= goList.Count; j++)
                        //{
                        if(pID == go.GetComponent<CombatHandler>().pID && pID <= goList.Count && pID > 0)
                        {
                        string updateQuery = "UPDATE GameObjects SET TransformX = @TransformX,TransformY = @TransformY,TransformZ = @TransformZ, RotateX = @RotateX, RotateY = @RotateY, RotateZ = @RotateZ, WHERE id = '" + pID + "' AND isOpen = '1'";

                        MySql.Data.MySqlClient.MySqlCommand updatecmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, cnn);

                        //cmd.Parameters.AddWithValue("@id", go.GetComponent<CombatHandler>().pID);
                        
                        updatecmd.Parameters.AddWithValue("@TransformX", thisPrefab.GetComponent<CombatHandler>().transform.position.x);
                        updatecmd.Parameters.AddWithValue("@TransformY", thisPrefab.GetComponent<CombatHandler>().transform.position.y);
                        updatecmd.Parameters.AddWithValue("@TransformZ", thisPrefab.GetComponent<CombatHandler>().transform.position.z);

                        updatecmd.Parameters.AddWithValue("@RotateX", thisPrefab.GetComponent<CombatHandler>().transform.rotation.x);
                        updatecmd.Parameters.AddWithValue("@RotateY", thisPrefab.GetComponent<CombatHandler>().transform.rotation.y);
                        updatecmd.Parameters.AddWithValue("@RotateZ", thisPrefab.GetComponent<CombatHandler>().transform.rotation.z);

                        updatecmd.CommandTimeout = 5;

                        updatecmd.BeginExecuteNonQuery();
                        
                        //goTransformX = go.GetComponent<CombatHandler>().transform.position.x;
                        //goTransformY = go.GetComponent<CombatHandler>().transform.position.y;
                        //goTransformZ = go.GetComponent<CombatHandler>().transform.position.z;
                        
                        

                        Debug.Log("Data Inserted");
                       // }
                        }
                    }
                }  
                //k=false;
                StartCoroutine(WaitFunction());




=======
        if(!k)
        {
            k=true;
            cnn.Open();
            Debug.Log("Update Method");       
                
                string updateQuery = "UPDATE GameObjects SET TransformX = @TransformX,TransformY = @TransformY,TransformZ = @TransformZ WHERE id ="+go.GetComponent<CombatHandler>().pID;

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery,cnn);
                cmd.Parameters.AddWithValue("@TransformX", go.GetComponent<CombatHandler>().transform.position.x);
                cmd.Parameters.AddWithValue("@TransformY", go.GetComponent<CombatHandler>().transform.position.y);
                cmd.Parameters.AddWithValue("@TransformZ", go.GetComponent<CombatHandler>().transform.position.z);
                
                cmd.BeginExecuteNonQuery();
>>>>>>> parent of d0e19e2... somenetworkchanges

                Debug.Log("Data Inserted");
                
                StartCoroutine(WaitFunction());
                cnn.Close();

            }
                
        
                
            }catch(MySql.Data.MySqlClient.MySqlException sqlEx){
                    Debug.Log("Failed Connection");  
                    cnn.Close();
            }

        }

    }*/
//* END REALTIME UPDATE FUNCTION */
 


 IEnumerator WaitFunction()
 {
     yield return new WaitForSeconds(ping);
     k = false;
 }






}

<<<<<<< HEAD
public class HolderClass : PrefabHandler
{
    public int goID { get; set; }
}

=======
>>>>>>> parent of d0e19e2... somenetworkchanges


public class updateMovement
{

public updateMovement updatemovement = new updateMovement();
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
bool k = false;
int count = 0;
private GameObject go;
    private List<GameObject> goList = new List<GameObject>();
    private List<int> allIds = new List<int>();



    public void updater()
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

                        
                        

                    foreach (GameObject thisPrefab in goList)
                    {
                        int pID = thisPrefab.GetComponent<CombatHandler>().pID;

                        //for(int j = 1; j <= goList.Count; j++)
                        //{
                        if(pID == go.GetComponent<CombatHandler>().pID && pID <= goList.Count && pID > 0)
                        {
                        string updateQuery = "UPDATE GameObjects SET TransformX = @TransformX,TransformY = @TransformY,TransformZ = @TransformZ, RotateX = @RotateX, RotateY = @RotateY, RotateZ = @RotateZ, WHERE id = '" + pID + "' AND isOpen = '1'";

                        MySql.Data.MySqlClient.MySqlCommand updatecmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, cnn);

                        //cmd.Parameters.AddWithValue("@id", go.GetComponent<CombatHandler>().pID);
                        
                        updatecmd.Parameters.AddWithValue("@TransformX", thisPrefab.GetComponent<CombatHandler>().transform.position.x);
                        updatecmd.Parameters.AddWithValue("@TransformY", thisPrefab.GetComponent<CombatHandler>().transform.position.y);
                        updatecmd.Parameters.AddWithValue("@TransformZ", thisPrefab.GetComponent<CombatHandler>().transform.position.z);

                        updatecmd.Parameters.AddWithValue("@RotateX", thisPrefab.GetComponent<CombatHandler>().transform.rotation.x);
                        updatecmd.Parameters.AddWithValue("@RotateY", thisPrefab.GetComponent<CombatHandler>().transform.rotation.y);
                        updatecmd.Parameters.AddWithValue("@RotateZ", thisPrefab.GetComponent<CombatHandler>().transform.rotation.z);

                        updatecmd.CommandTimeout = 5;

                        updatecmd.BeginExecuteNonQuery();
                        
                        //goTransformX = go.GetComponent<CombatHandler>().transform.position.x;
                        //goTransformY = go.GetComponent<CombatHandler>().transform.position.y;
                        //goTransformZ = go.GetComponent<CombatHandler>().transform.position.z;
                        
                        

                        Debug.Log("Data Inserted");
                       // }
                        }
                    }
                }  
                //k=false;






                cnn.Close();

            }
            catch(MySql.Data.MySqlClient.MySqlException sqlEx){
                    Debug.Log("Failed Connection");  
                    cnn.Close();
            }

        }   
    }



}




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
public MySql.Data.MySqlClient.MySqlConnection cnn;
public MySql.Data.MySqlClient.MySqlCommand cmd;
public string goName;
public int goID;
public float goTransformX, goTransformY, goTransformZ, goRotateX, goRotateY, goRotateZ;

public int ServerRefreshRate;

public int updateWaitTimer;
public int goIndex;

public int isOpen;
public int i = 0;
public bool t;
public bool k = false;
public int count = 0;
public GameObject go;
    public List<GameObject> goList = new List<GameObject>();
    public List<int> allIds = new List<int>();
//List<int> allIds = new List<int>();



    public void Start ()
{
        //sqlQueue = @"GameObjects";
        connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
        MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);
        using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        {
     

        try {
                
            cnn.Open();
            Debug.Log("Success");       
                
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

                        allIds.Add((int)GameObjectsDB[1]);
                        Debug.Log(allIds + " Loaded");

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
                            Debug.Log(goID + " loaded");
                            }

                }


                GameObjectsDB.Close();     
                
            }catch(MySql.Data.MySqlClient.MySqlException sqlEx){
                    Debug.Log("Failed Connection");  
                    cnn.Close();
            }
        }
}
//*START UPDATE FUNCTION */

/* 
public void Update ()
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

                        MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, cnn);

                        //cmd.Parameters.AddWithValue("@id", go.GetComponent<CombatHandler>().pID);
                        
                        cmd.Parameters.AddWithValue("@TransformX", thisPrefab.GetComponent<CombatHandler>().transform.position.x);
                        cmd.Parameters.AddWithValue("@TransformY", thisPrefab.GetComponent<CombatHandler>().transform.position.y);
                        cmd.Parameters.AddWithValue("@TransformZ", thisPrefab.GetComponent<CombatHandler>().transform.position.z);

                        cmd.Parameters.AddWithValue("@RotateX", thisPrefab.GetComponent<CombatHandler>().transform.rotation.x);
                        cmd.Parameters.AddWithValue("@RotateY", thisPrefab.GetComponent<CombatHandler>().transform.rotation.y);
                        cmd.Parameters.AddWithValue("@RotateZ", thisPrefab.GetComponent<CombatHandler>().transform.rotation.z);

                        cmd.CommandTimeout = 5;

                        cmd.BeginExecuteNonQuery();
                        
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
                cnn.Close();

            }
            catch(MySql.Data.MySqlClient.MySqlException sqlEx){
                    Debug.Log("Failed Connection");  
                    cnn.Close();
            }

        }*/
        
        //*END UPDATE FUNCTION */

    }

 


/* IEnumerator WaitFunction()
 {
     yield return new WaitForSeconds(ServerRefreshRate);
     k = false;
 }*/



public class HolderClass
{
    public int goID { get; set; }
}
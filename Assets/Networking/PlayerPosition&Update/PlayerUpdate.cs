using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class PlayerUpdate : MonoBehaviour {
private string SQL_DATABASE_NAME = "characters", SQL_USERNAME = "unityroot" , SQL_PASSWORD = "!@12QWqw" ,SQL_PORT = "3306";
public string SQL_HOST = "localhost";

public float serverRefreshTime;

private string connectionString = null;
public MySql.Data.MySqlClient.MySqlConnection cnn;
public MySql.Data.MySqlClient.MySqlConnection connectionUpdater;
public MySql.Data.MySqlClient.MySqlConnection connectionRefresh;
public MySql.Data.MySqlClient.MySqlCommand cmd;
public string pName;
public int pID;
public int goID, accID, charID;
private float pTransformX, pTransformY, pTransformZ, pRotateX, pRotateY, pRotateZ;

public float ServerRefreshRate;

public int updateWaitTimer;

public int isOpen;
public int i = 0;
public bool t;
public bool k = false;
private bool startBool = false;
public int count = 0;
public GameObject Player;
    public List<GameObject> PlayerList = new List<GameObject>();
    public List<int> allIds = new List<int>();


//List<int> allIds = new List<int>();

//* DEFINING STRUCTURE FOR NPC POSITION DETECTION BASED ON 5 SECOND REFRESH TIMER*/

public string PlayerName;
public int PlayerID;                //Discover units based on if NPC ID = pID
public float PlayerTransformX, PlayerTransformY, PlayerTransformZ, PlayerRotateX, PlayerRotateY, PlayerRotateZ;
public int PlayerIndex, PlayerOpen;
public Vector3 PlayerTransformPosition, PlayerTransformRotation;


//* END DEFINITIONS FOR NPC POSITION DETECTION AND SOON TO BE STAT DETECTION*/

CharacterSelector charSelector;

    public void Start()
{
	charSelector = GameObject.FindObjectOfType<CharacterSelector>();		
    
        //sqlQueue = @"GameObjects";
        connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
        MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);
        using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        {
        try {
                
            cnn.Open();      
                
                string GameObjectsRead = "SELECT name,charID,AccountID,TransformX,TransformY,TransformZ,RotateX,RotateY,RotateZ,goID,LoggedIn FROM characters";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(GameObjectsRead, cnn);
                MySql.Data.MySqlClient.MySqlDataReader GameObjectsDB = cmd.ExecuteReader();
 
                    while(GameObjectsDB.Read())
                    {
                        pName = (string)GameObjectsDB[0];
                        pTransformX = (float)GameObjectsDB[1];
                        pTransformY = (float)GameObjectsDB[2];
                        pTransformZ = (float)GameObjectsDB[3];
                        pRotateX = (float)GameObjectsDB[4];
                        pRotateY = (float)GameObjectsDB[5];
                        pRotateZ = (float)GameObjectsDB[6];
                        charID = (int)GameObjectsDB[8];
                        isOpen = (int)GameObjectsDB[7];
						goID = (int)GameObjectsDB[9];
						accID = (int)GameObjectsDB[10];

                        allIds.Add((int)GameObjectsDB[1]);

                            Debug.Log("goInfo Read");

                            if(goID == 1 && isOpen == 1 && charID != charSelector.pID)
                            {
                            Player = (GameObject)Instantiate(Resources.Load("GOID1"),new Vector3(pTransformX,pTransformY,pTransformZ),Quaternion.Euler(pRotateX,pRotateY,pRotateZ));
                            Player.transform.position = new Vector3(pTransformX,pTransformY,pTransformZ);
                            PlayerList.Add(Player);
                            isOpen = 0;
                            //Skeleton Spawner
                            Debug.Log(goID + " loaded");
                            }

                            if(goID == 2 && isOpen == 1 && charID != charSelector.pID)
                            {
                            Player = (GameObject)Instantiate(Resources.Load("GOID2"),new Vector3(pTransformX,pTransformY,pTransformZ),Quaternion.Euler(pRotateX,pRotateY,pRotateZ));
                            Player.transform.position = new Vector3(pTransformX,pTransformY,pTransformZ);
                            PlayerList.Add(Player);
                            isOpen = 0;
                            //Skeleton Spawner
                            Debug.Log(goID + " loaded");
                            }

                            if(goID == 3 && isOpen == 1 && charID != charSelector.pID)
                            {
                            Player = (GameObject)Instantiate(Resources.Load("GOID3"),new Vector3(pTransformX,pTransformY,pTransformZ),Quaternion.Euler(pRotateX,pRotateY,pRotateZ));
                            Player.transform.position = new Vector3(pTransformX,pTransformY,pTransformZ);
                            PlayerList.Add(Player);
                            isOpen = 0;
                            //Skeleton Spawner
                            Debug.Log(goID + " loaded");
                            }


                }

                GameObjectsDB.Close();     
                cnn.Close();
            }catch(MySql.Data.MySqlClient.MySqlException sqlEx){
                    cnn.Close();
            }
        }
}

	public void pUpdater(GameObject tgo)
	{
        connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
        using(connectionUpdater = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        {
			try{
                Debug.Log("Attempt Connection");
			    connectionUpdater.Open();
                                   
                        Debug.Log("Connected to update server");

                        string updateQuery = "UPDATE characters SET TransformX=@TransformX, TransformY=@TransformY, TransformZ=@TransformZ, RotateX=@RotateX, RotateY=@RotateY, RotateZ=@RotateZ WHERE CharID=" + charSelector.pID + ";";
                        MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(updateQuery, connectionUpdater);
					
                        command.Parameters.AddWithValue("@TransformX", tgo.GetComponent<PlayerHandler>().transform.position.x);
                        command.Parameters.AddWithValue("@TransformY", tgo.GetComponent<PlayerHandler>().transform.position.y);
                        command.Parameters.AddWithValue("@TransformZ", tgo.GetComponent<PlayerHandler>().transform.position.z);

                        command.Parameters.AddWithValue("@RotateX", tgo.GetComponent<PlayerHandler>().transform.rotation.x);
                        command.Parameters.AddWithValue("@RotateY", tgo.GetComponent<PlayerHandler>().transform.rotation.y);
                        command.Parameters.AddWithValue("@RotateZ", tgo.GetComponent<PlayerHandler>().transform.rotation.z);

                            Debug.Log("Player Data Inserted");

                        command.ExecuteNonQuery();      
                        tgo.transform.position = tgo.GetComponent<PlayerHandler>().transform.position;
                        connectionUpdater.Close();
                }catch(MySql.Data.MySqlClient.MySqlException sqlEx){
					connectionUpdater.Close();
                    Debug.Log("Connection Terminated");

			}
		}
	}


	public void SQLRefresh(GameObject tgo)
	{
        connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
		using(connectionRefresh = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
        {
			try{
			connectionRefresh.Open();
                int pIDb = charSelector.pID;
                string GameObjectsReadb = "SELECT name,CharID,AccountID,TransformX,TransformY,TransformZ,RotateX,RotateY,RotateZ,GoID,LoggedIn FROM characters WHERE CharID =" + charSelector.pID;
                MySql.Data.MySqlClient.MySqlCommand refresh = new MySql.Data.MySqlClient.MySqlCommand(GameObjectsReadb, connectionRefresh);
                MySql.Data.MySqlClient.MySqlDataReader GameObjectsDBb = refresh.ExecuteReader();
 
                    while(GameObjectsDBb.Read())
                    {
                        pName = (string)GameObjectsDBb[0];
                        pTransformX = (float)GameObjectsDBb[1];
                        pTransformY = (float)GameObjectsDBb[2];
                        pTransformZ = (float)GameObjectsDBb[3];
                        pRotateX = (float)GameObjectsDBb[4];
                        pRotateY = (float)GameObjectsDBb[5];
                        pRotateZ = (float)GameObjectsDBb[6];
                        charID = (int)GameObjectsDBb[8];
                        isOpen = (int)GameObjectsDBb[7];
						goID = (int)GameObjectsDBb[9];
						accID = (int)GameObjectsDBb[10];

                        allIds.Add((int)GameObjectsDBb[8]);

                            /*if(pIDb == goID)
                            {
                                tgo.transform.position = new Vector3(goTransformX,goTransformY,goTransformZ);
                                Debug.Log("moved");
                            }*/

                        PlayerName = pName;
                        PlayerID = pID;
                        PlayerTransformX = pTransformX;
                        PlayerTransformY = pTransformY;
                        PlayerTransformZ = pTransformZ;
                        PlayerRotateX = pRotateX;
                        PlayerRotateY = pRotateY;
                        PlayerRotateZ = pRotateZ;
                        PlayerIndex = charID;
                        PlayerOpen = isOpen;
                        PlayerTransformPosition = new Vector3(PlayerTransformX,PlayerTransformY,PlayerTransformZ);
                        PlayerTransformRotation = new Vector3(PlayerRotateX,PlayerRotateY,PlayerRotateZ);
                    }

                    if(!startBool){
                        if(charID == charSelector.pID)
                        {
                        tgo.transform.position = PlayerTransformPosition;
                        }
                    startBool = true;
                    }

                    if(charID != charSelector.pID)
                    {
                        tgo.transform.position = Vector3.Slerp(tgo.transform.position, PlayerTransformPosition, Time.deltaTime * tgo.GetComponent<CombatHandler>().speed);
                        Debug.Log("Moved"+tgo.name);
                    }

                    connectionRefresh.Close();
                }catch(MySql.Data.MySqlClient.MySqlException sqlEx){
					connectionRefresh.Close();
                }

		}

	}

    }

 


/* IEnumerator WaitFunction()
 {
     yield return new WaitForSeconds(ServerRefreshRate);
     k = false;
 }*/



/*public class HolderClass
{
    public int goID { get; set; }
}*/
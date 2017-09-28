using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class CharacterSelector : MonoBehaviour{
public MySql.Data.MySqlClient.MySqlConnection cnn;
public MySql.Data.MySqlClient.MySqlConnection connection;
private string connectionString;

public string SQL_HOST;
private string SQL_DATABASE_NAME = "Prefabs", SQL_USERNAME = "unityroot" , SQL_PASSWORD = "!@12QWqw" ,SQL_PORT = "3306";

			public string playerName;
			public float pTransformX, pTransformY, pTransformZ, pRotateX, pRotateY, pRotateZ;
			public int LoggedIn, pID, goID, accID;

GameObject charSelector;
[SerializeField]
Transform Background;

public Text charNameText;
public string selectedChar;
public string [] nameList; 
public List <int> charSelected;

private int i;

	// Use this for initialization
	void Start () {
		nameList = LoginHandler.instance.charNames.ToArray();
		Debug.Log(nameList.Length);

	foreach(string PlayerName in nameList)
		{
			charSelector = (GameObject)Instantiate(Resources.Load("charSelection"),new Vector3(Screen.width/10,Screen.height/2 + i*60,0),Quaternion.Euler(0,0,0));
			charSelector.GetComponentInChildren<Text>().text = PlayerName;
			//charSelected.Add(i);
			charSelector.GetComponent<OnButtonSelected>().selectedNa = PlayerName;
			charSelector.transform.SetParent(Background);
			i++;
		Button btn = charSelector.GetComponent<Button>();
		btn.onClick.AddListener(delegate{setSelected(playerName);});

		}

		//Instantiate Selection Buttons
	}

	public void characterCreation()
	{
		//on character creation, insert into accounts where accounts == AccountID character Number
		//set Max Character Limit
	}

	public void setSelected(string charID){
	Debug.Log(charID);


//* WORKING HERE, TRYING TO GET THE SCRIPT TO PULL THE STRING FROM THE BUTTON CLICK ON WHAT STRING TO GRAB */


	//selectedChar = charName.ToString();
	selectedChar = charID;
	//selectedChar = charSelector.GetComponent<OnButtonSelected>().selectedNa;
		
	connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
	MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);
	using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
	{
	try {
			
		cnn.Open();
		Debug.Log("Success");       
			
			string ReadPlayer = "SELECT * FROM Characters WHERE name ='" + selectedChar + "';";
			MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(ReadPlayer, cnn);
			MySql.Data.MySqlClient.MySqlDataReader CharactersDB = cmd.ExecuteReader();

				while(CharactersDB.Read())
				{
					playerName = (string)CharactersDB[0];
					pTransformX = (float)CharactersDB[1];
					pTransformY = (float)CharactersDB[2];
					pTransformZ = (float)CharactersDB[3];
					pRotateX = (float)CharactersDB[4];
					pRotateY = (float)CharactersDB[5];
					pRotateZ = (float)CharactersDB[6];
//					LoggedIn = (int)CharactersDB[7];
					pID = (int)CharactersDB[8];
					goID = (int)CharactersDB[9];
					accID = (int)CharactersDB[10];

				}

				Debug.Log(playerName + " is Selected");
				characterCreation();
			CharactersDB.Close();     
			
		}catch(MySql.Data.MySqlClient.MySqlException sqlEx){
				Debug.Log("Failed Connection");  
				cnn.Close();
		}
	}

		//Select from Characters GameObjectID where SelectedNa = name and AccountID = LoginHandler.instance.accID
		//do things

	}
	public void Login(){

		//Load Scene
		//Instantiate GameObject under account.
	}
}

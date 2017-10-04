using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour{

public static CharacterSelector instance;
public MySql.Data.MySqlClient.MySqlConnection cnn;
public MySql.Data.MySqlClient.MySqlConnection connection;
private string connectionString;

public string selectedName;
public int selectedID;
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
public string [] nameList = new string[10]; 
public List <int> charSelected;

private int i;

[SerializeField]
GameObject char1, char2, char3, char4, char5, char6, char7, char8, char9, char10;



	public void Awake(){
		if(instance == null){
			instance = this;
		}else if(instance = this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(instance);
	}


	// Use this for initialization
	void Start () {
		nameList = LoginHandler.instance.charNames.ToArray();
		Debug.Log(nameList.Length);

		Button BtnChar1 = char1.GetComponent<Button>();
		Button BtnChar2 = char2.GetComponent<Button>();
		Button BtnChar3 = char3.GetComponent<Button>();
		Button BtnChar4 = char4.GetComponent<Button>();
		Button BtnChar5 = char5.GetComponent<Button>();
		Button BtnChar6 = char6.GetComponent<Button>();
		Button BtnChar7 = char7.GetComponent<Button>();
		Button BtnChar8 = char8.GetComponent<Button>();
		Button BtnChar9 = char9.GetComponent<Button>();
		Button BtnChar10 = char10.GetComponent<Button>();


	if(nameList != null)
	{
		if(nameList.Length > 0)
		{
		char1.GetComponentInChildren<Text>().text = nameList[0];
		BtnChar1.onClick.AddListener(delegate{clickSelect(1, nameList[0]);});
		}

		if(nameList.Length > 1)
		{
		char2.GetComponentInChildren<Text>().text = nameList[1];
		BtnChar2.onClick.AddListener(delegate{clickSelect(2, nameList[1]);});
		}

		if(nameList.Length > 2)
		{
		char3.GetComponentInChildren<Text>().text = nameList[2];
		BtnChar3.onClick.AddListener(delegate{clickSelect(3, nameList[2]);});
		}

		if(nameList.Length > 3)
		{
		char4.GetComponentInChildren<Text>().text = nameList[3];
		BtnChar4.onClick.AddListener(delegate{clickSelect(4, nameList[3]);});
		}
		if(nameList.Length > 4)
		{
		char5.GetComponentInChildren<Text>().text = nameList[4];
		BtnChar5.onClick.AddListener(delegate{clickSelect(5, nameList[4]);});
		}
		if(nameList.Length > 5)
		{
		char6.GetComponentInChildren<Text>().text = nameList[5];
		BtnChar6.onClick.AddListener(delegate{clickSelect(6, nameList[5]);});
		}
		if(nameList.Length > 6)
		{
		char7.GetComponentInChildren<Text>().text = nameList[6];
		BtnChar7.onClick.AddListener(delegate{clickSelect(7, nameList[6]);});
		}
		if(nameList.Length > 7)
		{
		char8.GetComponentInChildren<Text>().text = nameList[7];
		BtnChar8.onClick.AddListener(delegate{clickSelect(8, nameList[7]);});
		}
		if(nameList.Length > 8)
		{
		char9.GetComponentInChildren<Text>().text = nameList[8];
		BtnChar9.onClick.AddListener(delegate{clickSelect(9, nameList[8]);});
		}
		if(nameList.Length > 9)
		{
		char10.GetComponentInChildren<Text>().text = nameList[9];
		BtnChar10.onClick.AddListener(delegate{clickSelect(10, nameList[9]);});
		}
	}
		
	}

	public void clickSelect(int selected, string name)
	{
		selectedID = selected;
		selectedName = name;
		setSelected();
		
	}

	public void setSelected(){
		
	connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
	MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);
	using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
	{
	try {
			
		cnn.Open();
		Debug.Log("Success");       
			
			string ReadPlayer = "SELECT * FROM Characters WHERE name ='" + selectedName + "';";
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

	public void characterCreation()
	{
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}
	public void Login(){

		//Load Scene
		//Instantiate GameObject under account.
	}
}

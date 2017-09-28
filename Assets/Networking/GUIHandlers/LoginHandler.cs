using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class LoginHandler : MonoBehaviour{

public static LoginHandler instance;
public List<string> charNames;
public MySql.Data.MySqlClient.MySqlConnection cnn;
public MySql.Data.MySqlClient.MySqlConnection connection;
private string connectionString;

public string SQL_HOST;
private string SQL_DATABASE_NAME = "Prefabs", SQL_USERNAME = "unityroot" , SQL_PASSWORD = "!@12QWqw" ,SQL_PORT = "3306";

public string AccountName, PasswordField;
private string accName, passWd;
public int id, AccountID;

					public string charName;
					public float TransformX;
					public float TransformY;
					public float TransformZ;
					public float RotateX;
					public float RotateY;
					public float RotateZ;
					public int loggedIn;
					public int charId;
					public int goID;
					public int charAccountID;



Text PassWord;


Text AccName;

public void Awake(){
	if(instance == null){
		instance = this;
	}else if(instance = this)
	{
		Destroy(gameObject);
	}
	DontDestroyOnLoad(instance);

}
public void UpdateText()
{
	PassWord = GameObject.Find("PassWord").GetComponent<UnityEngine.UI.Text>();
	AccName = GameObject.Find("AccName").GetComponent<UnityEngine.UI.Text>();
	AccountName = AccName.text.ToString();
	PasswordField = PassWord.text.ToString();
	Debug.Log("Account name is " + AccountName + " and the Password is " + PasswordField);
	callLogin(AccountName, PasswordField);
}

public void callLogin(string AccountName, string PasswordField)
{
	connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
	MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);
	using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
	{
	

	try {
			
		cnn.Open();
		Debug.Log("Success");       
			
			string ReadAccounts = "SELECT accName, passWd, id FROM Accounts WHERE accName ='" + AccountName +"';";
			MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(ReadAccounts, cnn);
			MySql.Data.MySqlClient.MySqlDataReader AccountsDB = cmd.ExecuteReader();

				while(AccountsDB.Read())
				{
					accName = (string)AccountsDB[0];
					passWd = (string)AccountsDB[1];
					id = (int)AccountsDB[2];

						Debug.Log("goInfo Read");


			
					if(AccountName == accName && PasswordField == passWd)
					{
							SelectCharacter();

					}else{
						Debug.Log("Incorrect Password or Account Name");
					}

				}

			AccountsDB.Close();     
			
		}catch(MySql.Data.MySqlClient.MySqlException sqlEx){
				Debug.Log("Failed Connection");  
				cnn.Close();
		}
	}
}


public void SelectCharacter()
{
	
	connectionString = "server=" + SQL_HOST + ";" + "database=" + SQL_DATABASE_NAME + ";" + "user=" + SQL_USERNAME + ";" + "password=" + SQL_PASSWORD + ";" + "port=" + SQL_PORT + ";";
	MySql.Data.MySqlClient.MySqlBulkLoader ObjectLoader = new MySql.Data.MySqlClient.MySqlBulkLoader(cnn);
	using(cnn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
	{
	

	try {
			
		cnn.Open();
		Debug.Log("Success");       
			
			string ReadCharacters = "SELECT * FROM Characters WHERE AccountID =" + id;
			MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(ReadCharacters, cnn);
			MySql.Data.MySqlClient.MySqlDataReader CharactersDB = cmd.ExecuteReader();

				while(CharactersDB.Read())
				{

					charName = (string)CharactersDB[0];
					TransformX = (float)CharactersDB[1];
					TransformY = (float)CharactersDB[2];
					TransformZ = (float)CharactersDB[3];
					RotateX = (float)CharactersDB[4];
					RotateY = (float)CharactersDB[5];
					RotateZ = (float)CharactersDB[6];
					//loggedIn = (int)CharactersDB[7];
					charId = (int)CharactersDB[8];
					goID = (int)CharactersDB[9];
					charAccountID = (int)CharactersDB[10];

						Debug.Log("goInfo Read");

						Debug.Log(charName+" is your selected character\n");
						charNames.Add(charName);

				}
			
			CharactersDB.Close();     
			SceneManager.LoadScene("CharacterSelection", LoadSceneMode.Single);
		}catch(MySql.Data.MySqlClient.MySqlException sqlEx){
				Debug.Log("Failed Connection");  
				cnn.Close();
		}
	}

}

}

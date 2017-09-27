using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour{

GameObject charSelector;
[SerializeField]
Transform Background;

public Text charNameText;
public int selectedChar;
public string [] nameList; 
public List <int> charSelected;

	// Use this for initialization
	void Start () {
		nameList = LoginHandler.instance.charNames.ToArray();
		Debug.Log(nameList.Length);

	for(int i = 0; i < nameList.Length; i++)
		{
			charSelector = (GameObject)Instantiate(Resources.Load("charSelection"),new Vector3(Screen.width/10,Screen.height/2 + i*60,0),Quaternion.Euler(0,0,0));
			charSelector.GetComponentInChildren<Text>().text = nameList[i];
			charSelected.Add(i);
			charSelector.GetComponent<OnButtonSelected>().selectedNo = i;
			charSelector.transform.SetParent(Background);
		}
		//Instantiate Selection Buttons
	}

	public void characterCreation()
	{
		//on character creation, insert into accounts where accounts == AccountID character Number
		//set Max Character Limit
	}

	public void setSelected(){
	
		selectedChar = charSelector.GetComponent<OnButtonSelected>().selectedNo;

	}
	public void Login(){
		//do a read, find SelectedNO Where AccountID = charAccountID
		//Select GameObjectID Where SelectedNoID
		
		//Load Scene
		//Instantiate GameObject under account.
	}
}

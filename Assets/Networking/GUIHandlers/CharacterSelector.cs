using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour{

GameObject charSelector;
[SerializeField]
Transform Background;


public string [] nameList; 

	// Use this for initialization
	void Start () {
		nameList = LoginHandler.instance.charNames.ToArray();
		Debug.Log(nameList.Length);

	for(int i = 0; i < nameList.Length; i++)
		{
			charSelector = (GameObject)Instantiate(Resources.Load("charSelection"),new Vector3(Screen.width/10,Screen.height/2 + i*60,0),Quaternion.Euler(0,0,0));
			charSelector.transform.SetParent(Background);
		}
		//Instantiate Selection Buttons
	}

	public void setSelected(){
		//Set int Selected to character ID value in DB
	}
	public void Login(){
		//Load Scene Game
	}
}

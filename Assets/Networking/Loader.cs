using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

CharacterSelector charSelector;
public GameObject player;
public string pName;

	// Use this for initialization
	void Start () {
		
	charSelector = GameObject.FindObjectOfType<CharacterSelector>();		

	switch(charSelector.goID)
	{
		case 1:
			player = (GameObject)Instantiate(Resources.Load("GOID1"),new Vector3(charSelector.pTransformX,charSelector.pTransformY,charSelector.pTransformZ),Quaternion.Euler(charSelector.pRotateX,charSelector.pRotateY,charSelector.pRotateZ));
			pName = charSelector.playerName;
		break;

		case 2:
			player = (GameObject)Instantiate(Resources.Load("GOID2"),new Vector3(charSelector.pTransformX,charSelector.pTransformY,charSelector.pTransformZ),Quaternion.Euler(charSelector.pRotateX,charSelector.pRotateY,charSelector.pRotateZ));
			pName = charSelector.playerName;
		break;

		case 3:
			player = (GameObject)Instantiate(Resources.Load("GOID3"),new Vector3(charSelector.pTransformX,charSelector.pTransformY,charSelector.pTransformZ),Quaternion.Euler(charSelector.pRotateX,charSelector.pRotateY,charSelector.pRotateZ));
			pName = charSelector.playerName;
			break;
	}

	}


}


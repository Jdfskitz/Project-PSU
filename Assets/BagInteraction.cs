using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagInteraction : MonoBehaviour {

	public GameObject menu;
	private bool isShowing;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("b")) {
			isShowing = !isShowing;
			menu.SetActive (isShowing);
		}
	}
}

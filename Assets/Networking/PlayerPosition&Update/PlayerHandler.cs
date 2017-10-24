using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHandler : MonoBehaviour {


	PlayerUpdate pHandler;
	private bool j = false;
	private bool k = false;
	public int serverRefreshTime;
	private float serverWaitTime;
	public int speed = 5;

	void Start () {
		pHandler = GameObject.FindObjectOfType<PlayerUpdate>();
		serverWaitTime = pHandler.ServerRefreshRate;
	}
	
	// Update is called once per frame
	void Update () {
		if(!j)
		{
		j = true;
			pHandler.SQLRefresh(this.gameObject);
			StartCoroutine(serverRefresh(pHandler.serverRefreshTime));
		}

		if(!k)
		{
			k = true;
			pHandler.pUpdater(this.gameObject);
			StartCoroutine(serverWait(serverWaitTime));
		}

	}
	private IEnumerator serverRefresh(float serverRefreshTime)
	{
		yield return new WaitForSeconds(serverRefreshTime);
		j = false;
	}
	private IEnumerator serverWait(float serverWaitTIme)
	{
		yield return new WaitForSeconds(serverWaitTime);
		k = false;
	}
}

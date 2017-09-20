using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIEnemyEventHandler : MonoBehaviour
{

	//Delegates
	public delegate void AIHandler();

	//AIEvents
	public static event AIHandler onAttack;

	public static void startAttack()
	{
		if(onAttack != null)
		{
			onAttack.Invoke();
		}
	}

}



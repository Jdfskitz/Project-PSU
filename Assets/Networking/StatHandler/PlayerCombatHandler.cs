using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatHandler : MonoBehaviour
{

	private bool attack = false;
	void Update(){

	int i = 0;
	string animString;


	if(Input.GetMouseButtonDown(0)){
			i++;
			float timeBetween = 5;

			switch(i){

				case 1:
				animString = "FirstAttack";
				if(!attack){
					attack = true;
					Debug.Log("Swing 1");
					//StartCoroutine(attackAnimWait(animString));
					timeBetween -= Time.deltaTime;
					if(timeBetween <= 0)
						{
						i = 0;
						}
					}
					break;

					case 2:
					if(!attack)
					{
					attack = true;
					animString = "SecondAttack";
					Debug.Log("Swing 2");
					//StartCoroutine(attackAnimWait(animString));
					timeBetween -= Time.deltaTime;
					if(timeBetween <= 0)
						{
						i = 0;
						}
					}
					break;

					case 3:
					if(!attack)
						{
						attack = true;
						animString = "ThirdAttack";
						Debug.Log("Swing 3");
						//StartCoroutine(attackAnimWait(animString));
						i = 0;
						}
					break;

					default:
						Debug.Log("Case Closed");
					break;

				}

			}

		}

/* 
		private IEnumerator attackAnimWait(string animString)
		{
		Anim.play(animString);
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);	
		attack = false;
		}

*/

	}

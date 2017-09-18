using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetection : MonoBehaviour {

	public GameObject [] array;
	private Quaternion _lookRotation;
	Animator anim;

	public float detectRadius = 300f;
	public float meleeAttackRadius = 50f;
	public float speed = .2f;
	public float turnSpeed = 100f;
	public float tempSpeed = .2f;
	public float attackWait = 3;

	private bool t = false;
	private bool satk;
	private bool matk;
	private bool fatk;
	public bool moving = false;
	public bool meleeAttacking = false;
	private bool isWalking;
	private bool isIdle;
	private int i;


	public int AnimationState = 1;

	public int FactionID;


	void Start()
	{
		anim = this.gameObject.GetComponent<Animator> ();
	}

	void Update () {
		
		MoveTowardsDetection();
		idlePhase ();
		walkingPhase ();

		if(meleeAttacking)
		{
			AIEnemyEventHandler.startAttack();
		}
	}



	//DETECTION FOR WALKING TOWARDS

	void MoveTowardsDetection(){
		
		foreach (GameObject go in array){
			
			if(go.tag == "Player")
			{
			float distanceSqr = (transform.position - go.transform.position).sqrMagnitude;

			if (distanceSqr < detectRadius) {
				moving = true;
			} else {
				moving = false;
			}

			if (distanceSqr < meleeAttackRadius) {
				meleeAttacking = true;
				
			} else {
				meleeAttacking = false;
			}
				
			if (moving) {
				_lookRotation = Quaternion.LookRotation (go.transform.position - transform.position);
				transform.position = Vector3.MoveTowards (transform.position, go.transform.position, speed * Time.deltaTime);
				transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, turnSpeed * Time.deltaTime);
				transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
				isWalking = true;
			} else {
				isWalking = false;
			}

			if (meleeAttacking) {
				moving = false;
				speed = 0;
			} else {
				moving = true;
				speed = tempSpeed;
			}

			if(!meleeAttacking && speed == 0)
			{
				isIdle = true;
			}else{
				isIdle = false;
			}
		}
		}
	}

	void OnEnable(){
		AIEnemyEventHandler.onAttack += attackPhase;
	}

	void OnDisable(){
		AIEnemyEventHandler.onAttack -= attackPhase;
	}


	//While not walking or attacking
	void idlePhase()
	{
		if (isIdle && !isWalking && !meleeAttacking) {
			anim.Play ("Idle");
		}
	}

	//While walking and not attacking
	void walkingPhase()
	{
		if (isWalking && !isIdle && !meleeAttacking) {
			anim.Play ("Walk");
		}
	}

	//Atack Coroutine Initialization
	
	void attackPhase()
	{
		
		if(!t)
		{
			anim = this.gameObject.GetComponent<Animator> ();
			StartCoroutine(AttackInitial ());
			t = true;
			
		}else{
			t = true;
		}
	
	}

	//Couroutine Handlers
	public IEnumerator AttackInitial()
	{
			attackStart();
			anim.Play ("Attack");
			//Debug.Log ("IN ANIMATION ATTACK");
			yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);	
			attackMid();
			anim.Play ("attackReady");
			//Debug.Log("IN ANIMATION MIDATTACK");
			yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime+attackWait);
			attackFinish();	
			t = false;
	}
	
	public void attackStart()
	{
		satk = false;

		if(!satk)
		{
			//* insert prior to swing functions here */


			Debug.Log("hitStart");


			//*End Start Attack Input */
			satk = true;
		}
	}

	public void attackMid()
	{
		matk = false;

		if(!matk)
		{
			//* insert Mid attack functions, immediately after the swing */



			Debug.Log("hitMid");



			//*End Mid Attack Input */
			



			matk = true;
		}
	}

	public void attackFinish()
	{
		fatk = false;

		if(!fatk)
		{
			//* insert End Idle Prior to Loop attack functions here. */


			Debug.Log("hitFinish");




			//*End Finish Attack Input */
			fatk = true;
		}
	}
	
}

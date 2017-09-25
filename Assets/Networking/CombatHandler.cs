using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour {

	public GameObject [] array;
	private Quaternion _lookRotation;
	Animator anim;
	public GameObject tgo; 
	//NPCHandler pHandler = new NPCHandler();
	NPCHandler pHandler;
	NPCHandler pStats = new NPCHandler();
	public float detectRadius = 300f;
	public float meleeAttackRadius = 50f;
	public float speed = .2f;
	private float tempSpeed;
	public float turnSpeed = 100f;
	public float attackWait = 3;
	public GameObject target;

	public int serverRefreshTime;

	private float serverWaitTime = .25f;
	public int meleeAttackDamage;
	public int TransformX;
	public int TransformY;
	public int TransformZ;

	private bool t = false;
	private bool satk;
	private bool matk;
	private bool fatk;
	private bool moving = false;
	private bool meleeAttacking = false;
	private bool isWalking;
	private bool isIdle;
	private int i;
	private bool k = false;
	private bool j = false;
	private bool serverRefreshed = false;

	public int pID;
	public Vector3 thisPosition;
	public int AnimationState = 1;

	public int FactionID;
	public int NPCID;



    void Start()
	{
		pHandler = GameObject.FindObjectOfType<NPCHandler>();
		tempSpeed = speed;
		anim = this.gameObject.GetComponent<Animator> ();
	}

	void Update () {
		array = GameObject.FindGameObjectsWithTag("Player");

		MoveTowardsDetection();
		idlePhase ();
		walkingPhase ();
		if(!j)
		{
		j = true;
		//pHandler.SQLRefresh(this.gameObject);
			StartCoroutine(serverRefresh(pHandler.serverRefreshTime));
		}

		if(meleeAttacking)
		{
			AIEnemyEventHandler.startAttack();
		}
	}
	

	//DETECTION FOR WALKING TOWARDS

	void MoveTowardsDetection(){

		foreach (GameObject go in array){

			float distanceSqr = (this.transform.position - go.transform.position).sqrMagnitude;

			if (distanceSqr < detectRadius) {
				moving = true;
		
			if(!k)
			{
				k = true;
				pHandler.NPCUpdater(this.gameObject);
				StartCoroutine(serverWait(serverWaitTime));
			}

			} else {
				moving = false;
			}

			if (distanceSqr < meleeAttackRadius) {
				meleeAttacking = true;
				
			} else {
				meleeAttacking = false;
			}
				
			if (moving) {
				_lookRotation = Quaternion.LookRotation (go.transform.position - this.transform.position);
				transform.position = Vector3.MoveTowards (this.transform.position, go.transform.position, speed * Time.deltaTime);
				
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

	void OnEnable(){
		AIEnemyEventHandler.onAttack += attackPhase;
	}

	void OnDisable(){
		AIEnemyEventHandler.onAttack -= attackPhase;
	}


	//While not walking or attacking
	void idlePhase()
	{
		if (!isWalking && !meleeAttacking) {
			this.anim.Play ("Idle");
		}
	}

	//While walking and not attacking
	void walkingPhase()
	{
		if (isWalking && !meleeAttacking) {
			this.anim.Play ("Walk");



		}

	}

	//Atack Coroutine Initialization
	
	void attackPhase()
	{
	if(meleeAttacking)
		{
			if(!t)
			{
				this.anim = this.gameObject.GetComponent<Animator> ();
				this.StartCoroutine(AttackInitial ());
				t = true;
				
			}
		}
	}

	//Couroutine Handlers
	public IEnumerator AttackInitial()
	{
			attackStart();
			this.anim.Play ("Attack");
			//Debug.Log ("IN ANIMATION ATTACK");
			target = GameObject.FindGameObjectWithTag("Player");
			//target.GetComponent<PlayerStats>().healthPoints -= this.meleeAttackDamage;

			yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);	
			attackMid();
			this.anim.Play ("attackReady");
			//Debug.Log("IN ANIMATION MIDATTACK");
			yield return new WaitForSeconds(attackWait);
			attackFinish();	
			t = false;
	}

	private IEnumerator serverWait(float serverWaitTIme)
	{
		yield return new WaitForSeconds(serverWaitTime);
		k = false;
	}
	private IEnumerator serverRefresh(float serverRefreshTime)
	{
		yield return new WaitForSeconds(serverRefreshTime);
		j = false;
	}
	
	public void attackStart()
	{
		satk = false;

		if(!satk)
		{
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHandler : MonoBehaviour {

	public GameObject [] array;
	private Quaternion _lookRotation;
	Animator anim;

	public float detectRadius = 300f;
	public float attackRadius = 50f;
	public float speed = .2f;
	public float turnSpeed = 100f;
	public float tempSpeed = .2f;
	public float attackWait = 3;

	public bool moving = false;
	public bool attacking = false;
	private bool isWalking;
	private bool isAttacking;

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
		attackPhase ();

	}

	//DETECTION FOR WALKING TOWARDS

	void MoveTowardsDetection(){
		
		foreach (GameObject go in array){
			
			float distanceSqr = (transform.position - go.transform.position).sqrMagnitude;

			if (distanceSqr < detectRadius) {
				moving = true;
			} else {
				moving = false;
			}

			if (distanceSqr < attackRadius) {
				attacking = true;
			} else {
				attacking = false;
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

			if (attacking) {
				moving = false;
				speed = 0;
			} else {
				moving = true;
				speed = tempSpeed;
			}
		}
	}

	//While not walking or attacking
	void idlePhase()
	{
		if (!isWalking && !attacking) {
			anim.Play ("Idle");
		}
	}

	//While walking and not attacking
	void walkingPhase()
	{
		if (isWalking && !attacking) {
			anim.Play ("Walk");
		}
	}

	//Atack Coroutine Initialization
	void attackPhase()
	{
		if (attacking && !moving) {
			if (AnimationState == 1) {
				StartCoroutine (AttackInitial ());
			} else if (AnimationState == 2) {
				StartCoroutine (AttackMid (attackWait));
			}else{
			}
		}
	}

	//Couroutine Handlers
	public IEnumerator AttackInitial()
	{
		anim.Play ("Attack");
		Debug.Log ("IN ANIMATION ATTACK");
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);	
		AnimationState = 2;
	}
		
	public IEnumerator AttackMid(float attackWait)
	{
		anim.Play ("attackReady");
		Debug.Log("IN ANIMATION MIDATTACK");
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime+attackWait);
		AnimationState = 1;
	}
}

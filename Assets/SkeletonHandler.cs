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
	private bool attackDisabled = false;
	private int hit;

	public int FactionID;
	
	void Update () {
		anim = this.gameObject.GetComponent<Animator> ();

		GetInactiveInRadius ();

		idlePhase ();
		walkingPhase ();
		attackPhase ();

	}

	void GetInactiveInRadius(){
		
		foreach (GameObject go in array){
			
			float distanceSqr = (transform.position - go.transform.position).sqrMagnitude;

			if (distanceSqr < detectRadius && !attackDisabled) {
				moving = true;
			} else {
				moving = false;
			}

			if (distanceSqr < attackRadius && !attackDisabled) {
				attacking = true;
			} else {
				attacking = false;
			}


			if (moving && !attackDisabled) {
				_lookRotation = Quaternion.LookRotation (go.transform.position - transform.position);
				transform.position = Vector3.MoveTowards (transform.position, go.transform.position, speed * Time.deltaTime);
				transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, turnSpeed * Time.deltaTime);
				transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
				isWalking = true;

			} else {
				isWalking = false;
			}


			if (attacking || attackDisabled) {
				moving = false;
				speed = 0;


			} else {
				
				moving = true;
				speed = tempSpeed;
			}



		}
	}

	/*
	void attackPhase()
	{
		if (attacking && !moving && !attackDisabled) {
			StartCoroutine (AttackPause (attackWait));
		}
		if (hit) {
			Debug.Log ("hit");
			hit = false;
		}
	}*/ 

	void attackPhase()
	{
		if (attacking && !moving && !attackDisabled) {

			StartCoroutine (AttackPause (attackWait));

		}

	}


	void idlePhase()
	{
		if (!isWalking && !attacking) {
			anim.Play ("Idle");
		}
		if (attackDisabled) {
			anim.Play ("attackReady");
		}
	}

	void walkingPhase()
	{
		if (isWalking && !attacking && !attackDisabled) {
			anim.Play ("Walk");
		}
	}
		
	/*
	public IEnumerator AttackPause(float attackWait)
	{
		hit = true;
		anim.Play ("Attack");
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		attackDisabled = true;
		yield return new WaitForSeconds(attackWait);
		attackDisabled = false;
		attackPhase ();
	}*/

	public IEnumerator AttackPause(float attackWait)
	{
		beginAttack ();
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		midAttack (0);
		yield return new WaitForSeconds (attackWait);
		finishAttack ();
	}

	void beginAttack ()
	{
		anim.Play ("Attack");
	}

	void midAttack(int hit)
	{
		anim.Play ("attackReady");
		attackDisabled = true;
		hit++;
		if (hit == 1) {
			Debug.Log ("hit");
			hit++;
		}

	}
	void finishAttack()
	{
		
		attackDisabled = false;
	}

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimator : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		anim.Play ("Idle");
	}
}

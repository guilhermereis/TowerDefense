﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKingAnimatorController : MonoBehaviour {
    private Animator anim;
    //private WarriorController controller;

    public bool isAttacking = false;
    public float speed = 0f;

    // Use this for initialization
    void Start () {
        anim = (Animator)GetComponent<Animator>();
        //controller = (WarriorController)GetComponentInParent<WarriorController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (anim)
        {
            anim.SetBool("IsAttacking", isAttacking);
            anim.SetFloat("Speed", speed);
        }
    }

    //public void AttackEnd(int i)
    //{
    //    setIsAttacking(false);
    //}

    //public void Hit(int i) {
    //    controller.processHit() ;
    //}
}
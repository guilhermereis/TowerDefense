using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKingAnimatorController : MonoBehaviour {
    private Animator anim;
    //private WarriorController controller;

    public bool isAttacking = false;
    public bool isAttackingCastle = false;
    public bool isDead = false;
    public float speed = 0f;

    // Use this for initialization
    void Start () {
        anim = (Animator)GetComponent<Animator>();
        //controller = (WarriorController)GetComponentInParent<WarriorController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead)
        {
            if (anim)
            {
                anim.SetLayerWeight(1, 0);
                anim.SetBool("Dead", isDead);
                anim.SetBool("IsAttacking", false);
                anim.SetBool("IsAttackingCastle", false);
            }
        }
        else {
            if (anim)
            {
                anim.SetBool("IsAttacking", isAttacking);
                anim.SetBool("IsAttackingCastle", isAttackingCastle);
                anim.SetFloat("Speed", speed);
            }
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

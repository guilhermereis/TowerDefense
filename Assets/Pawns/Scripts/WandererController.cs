﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererController : EnemyController
{
    
    public float attackCountdown = 0f;
    private WandererCharacter character;
    private WandererAnimatorController anim;


	private void Start()
	{
		weight = 1;
        character = GetComponent<WandererCharacter>();
        anim = (WandererAnimatorController)GetComponent<WandererAnimatorController>();
    }

	protected override void Awake()
    {
        base.Awake();
        speed = 1;
        mats = gameObject.GetComponent<Renderer>().materials;
        mats[0] = frozenMaterial;
        originalMaterial = gameObject.GetComponent<Renderer>().materials;
    }

    public override void EnterFrozenTime()
    {
        base.EnterFrozenTime();
        if (originalMaterial != null) { }
            gameObject.GetComponent<Renderer>().materials = mats;
    }

    public override void LeaveFrozenTime()
    {
        base.LeaveFrozenTime();
        if(originalMaterial!= null)
            gameObject.GetComponent<Renderer>().materials= originalMaterial;
    }

    public override void OnMoving()
    {
        base.OnMoving();
    }

    public override void OnIdle()
    {
        base.OnIdle();
    }

    public override void OnBattle()
    {
        base.OnBattle();
        if (target != null)
        {
            if (attackCountdown <= 0)
            {
                anim.setIsAttacking(true);
                target.GetComponent<CastleHealth>().ApplyDamage(character.attack);
                attackCountdown = 1 / character.attackRate;
            }
            attackCountdown -= Time.deltaTime;
        }


    }


    // Update is called once per frame
    protected override void Update () {
        base.Update();
        if (character.isSlow)
        {
            anim.speed = nav.velocity.magnitude * 2f;
            anim.speedMultiplier = 0.3f;

        }
        else
        {
            anim.speed = nav.velocity.magnitude;
            anim.speedMultiplier = 1f;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {

        base.OnTriggerEnter(other);
        if(other.gameObject.tag == "Ally")
        {
            if(other.gameObject.GetComponent<PawnController>().target == this)
            {
                speed = 1;
            
            }
        }else if(other.gameObject.tag == "Castle")
        {
            target = other.gameObject;
            ChangeState(PawnState.Battle);
        }

        //base.OnTriggerEnter(other);
        //if (other.gameObject.tag != gameObject.tag)
        //{
        //    Debug.Log("somebody");
        //    nav.isStopped = true;
        //    ChangeState(PawnState.Battle);

        //}
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerController : PawnController {

	float attackCountdown = 0;
	DestroyerCharacter character;
	

	// Use this for initialization
	void Start () {
		
	}

	protected override void Awake()
	{
		base.Awake();
		character = (DestroyerCharacter)GetComponent<DestroyerCharacter>();
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update();
		speed = 5;
		if (CurrentState == PawnState.Destroying)
		{
            if (nav != null&&target!=null)
            {
                nav.SetDestination(target.transform.position);
                if (IsAtLocation())
                {
                    ChangeState(PawnState.Battle);
                }
            }
		}
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
				
				Debug.DrawLine(transform.position, target.transform.position);
				//we are goint to apply damage to target and if the target is dead, we are going to
				//tell the camp and so the camp can gives another target or we're going back
				if (target.GetComponent<BuildableController>().Damage(character.attack))
				{
					target = null;
				}

				attackCountdown = 1 / character.attackRate;
			}
			
		}
		else
			ChangeState(PawnState.Walking);
	}

	public override void OnMoving()
	{
		base.OnMoving();
	}

	protected override void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other.name);
		
		base.OnTriggerEnter(other);
		if (other.gameObject.tag.Equals("Build") && target == null)
		{
			target = other.gameObject;
			ChangeState(PawnState.Destroying);

		}
	}

	protected override void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);
		target = null;
		ChangeState(PawnState.Walking);
	}


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : EnemyController {

	private WarriorCharacter character;
	public float attackCountdown = 0f;
	private List<GameObject> enemiesInRange;
    private WarriorGoblingAnimatorController anim;

    // Use this for initialization
    void Start () {
		weight = 2;
		enemiesInRange = new List<GameObject>();
		character = (WarriorCharacter)GetComponent<WarriorCharacter>();
        anim = (WarriorGoblingAnimatorController)GetComponentInChildren<WarriorGoblingAnimatorController>();
    }
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
        anim.speed = nav.velocity.magnitude;
        if (target!= null && enemiesInRange.Contains(target))
		{
			ChangeState(PawnState.Battle);
		}
		attackCountdown -= Time.deltaTime;
	}

	public override void OnBattle()
	{
		base.OnBattle();
		nav.isStopped = true;
		
		if (target != null)
		{
			if (attackCountdown <= 0)
			{
				Debug.DrawLine(transform.position, target.transform.position);
                if (target.tag == "Ally")
                {
                    if (target.GetComponent<PawnCharacter>().Damage(character.attack))
                    {
                        enemiesInRange.Remove(target);
                        target.GetComponent<PawnCharacter>().OnDying();
                        target = null;
                    }
                }
                else if (target.tag == "Castle")
                    target.GetComponent<CastleHealth>().ApplyDamage(character.attack);

                attackCountdown = 1 / character.attackRate;
			}

		}
		else
		{
			ChangeState(PawnState.Walking);
		}
	}


	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
		//Debug.Log(other.gameObject.tag);
		//Debug.Assert(other.isTrigger);
		if(other.gameObject.tag == "Ally" && target == null)
		{
			enemiesInRange.Add(other.gameObject);

			target = other.gameObject;
			ChangeState(PawnState.Battle);
		}else if(other.gameObject == target)
		{
			ChangeState(PawnState.Battle);
		}
		else if(other.gameObject.tag == "Ally")
		{
			enemiesInRange.Add(other.gameObject);
			//Debug.Break();
		}else if (other.gameObject.tag == "Castle")
        {
            target = other.gameObject;
            ChangeState(PawnState.Battle);
        }
	}

	protected override void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);
		if (other.gameObject.tag.Equals("Ally"))
		{
			enemiesInRange.Remove(other.gameObject);
            ChangeState(PawnState.Walking);
			//if (target != null)
			//	ChangeState(PawnState.FindTarget);
			//else { }
			////	enemiesInRange.Remove(other.gameObject);

		}
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : EnemyController {

	private WarriorCharacter character;
	public float attackCountdown = 0f;
	
    private WarriorGoblingAnimatorController anim;

    public AudioSource spearAttackSound;

    

    // Use this for initialization
    void Start () {
		weight = 2;
		enemiesInRange = new List<GameObject>();
		character = (WarriorCharacter)GetComponent<WarriorCharacter>();
        anim = (WarriorGoblingAnimatorController)GetComponentInChildren<WarriorGoblingAnimatorController>();
    }

    protected override void Awake()
    {
        base.Awake();
        mats = transform.Find("PrefabWarriorGobling").Find("polySurface1").GetComponent<Renderer>().materials;
        mats[0] = frozenMaterial;
        
        originalMaterial = transform.Find("PrefabWarriorGobling").Find("polySurface1").GetComponent<Renderer>().materials;

    }

    public override void EnterFrozenTime()
    {
        base.EnterFrozenTime();
        if (originalMaterial != null)
            transform.Find("PrefabWarriorGobling").Find("polySurface1").GetComponent<Renderer>().materials = mats;
    }

    public override void LeaveFrozenTime()
    {
        base.LeaveFrozenTime();
        if (originalMaterial != null)
            transform.Find("PrefabWarriorGobling").Find("polySurface1").GetComponent<Renderer>().materials = originalMaterial;
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
  //      if (target!= null && enemiesInRange.Contains(target))
		//{
		//	ChangeState(PawnState.Battle);
		//}
		attackCountdown -= Time.deltaTime;
	}

	public override void OnBattle()
	{
		base.OnBattle();
		nav.isStopped = true;
		
		if (target != null)
		{
            if (target.GetComponent<PawnCharacter>() != null)
                if (target.GetComponent<PawnCharacter>().isDead)
                    ChangeState(PawnState.Walking);

            LookToTarget();
            if (attackCountdown <= 0)
			{
                anim.setIsAttacking(true);
                Debug.DrawLine(transform.position, target.transform.position);
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
        if(other.gameObject.tag == "Ally" && other.GetType()== typeof(CapsuleCollider))
        {
            if (!other.gameObject.GetComponent<PawnCharacter>().isDead)
            {
                if (other.gameObject.GetComponent<PawnController>().target != null && other.gameObject.GetComponent<PawnController>().target == gameObject)
                {
                    enemiesInRange.Add(other.gameObject);

                    if (target == null || target.GetComponent<PawnCharacter>().isDead)
		            {
			            target = other.gameObject;
			            ChangeState(PawnState.Battle);
		            }else if(other.gameObject == target)
		            {
			            ChangeState(PawnState.Battle);
		            }
                }

            }
		    

        }
        else if (other.gameObject.tag == "Castle")
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
           
            if (other.gameObject == target && !target.GetComponent<PawnCharacter>().isDead)
                ChangeState(PawnState.FindTarget);
           
            //	enemiesInRange.Remove(other.gameObject);

        }
    }

    public void processHit() {
        SoundToPlay.PlayAtLocation(spearAttackSound, transform.position, Quaternion.identity,0.1f,5f);
        //Instantiate(spearAttackSound, transform.position, Quaternion.identity);
        if (target.tag == "Ally")
        {
            bool hitted;
            
            if (target.GetComponent<PawnCharacter>().Damage(character.attack,out hitted, DamageType.Blood))
            {
                enemiesInRange.Remove(target);
                target = null;
            }
        }
        else if (target.tag == "Castle")
            target.GetComponent<CastleHealth>().ApplyDamage(character.attack);

        attackCountdown = 1 / character.attackRate;
    }
}

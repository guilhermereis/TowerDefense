using UnityEngine;
using System.Collections.Generic;

public class SimpleSoldierController : PawnController {

    public delegate void DeadSoldierDelegate(SimpleSoldierController deadSoldier);
    public DeadSoldierDelegate deadSoldier;


    [HideInInspector]
	public SoldierCampController camp;

	private SwordsmanAnimatorController anim;
	
	private SimpleSoldierCharacter character;

	

	public float attackCountdown = 0f;

	private AudioSource swordHit;

#region vfx
    public GameObject damagePrefabParticle;
    public GameObject bloodPrefabParticle;
    public GameObject missPrefabParticle;
#endregion
    public void SetTarget(GameObject _target)
    {
        target = _target;
        ChangeState(PawnState.FindTarget);   

    }

    public void ForgetTarget(GameObject _target)
    {
        if(target == _target)
        {
            target = null;
            ChangeState(PawnState.Homing);
        }
    }
	private void Start()
	{
		character = GetComponent<SimpleSoldierCharacter>();

		camp = GetComponentInParent<SoldierCampController>();
		camp.EnemyOutOfReach += ForgetTarget;

		enemiesInRange = new List<GameObject>();
		anim = (SwordsmanAnimatorController)GetComponent<SwordsmanAnimatorController>();
		swordHit = GetComponent<AudioSource>();

		
	}

	protected override void Awake()
    {
        base.Awake();
        currentState = PawnState.Idle;
        homePosition = transform.position;

		
    }
    // Update is called once per frame
    protected override void Update() {
        base.Update();
		anim.speed = nav.velocity.magnitude;
		attackCountdown -= Time.deltaTime;

       
		//if (target != null && enemiesInRange.Contains(target) && !gameObject.GetComponent<PawnCharacter>().isDead){
		//	ChangeState(PawnState.Battle);
		//}
    }

    public override void OnMoving()
    {
        base.OnMoving();
    }

    public override void OnIdle()
    {
        base.OnIdle();
    }

    public override void OnHoming()
    {
        base.OnHoming();
        anim.setIsAttacking(false);
    }

    public override void OnBattle()
    {

        base.OnBattle();

        if (target != null)
        {
            if (!target.GetComponent<PawnCharacter>().isDead)
            {
                if (enemiesInRange.Contains(target))
                {
			        if (attackCountdown <= 0)
			        {
				        anim.setIsAttacking(true);
                        //swordHit.Play();
                        LookToTarget();
				        Debug.DrawLine(transform.position, target.transform.position);
                        
                        //we are goint to apply damage to target and if the target is dead, we are going to
                        //tell the camp and so the camp can gives another target or we're going back home.
                        
                        swordHit.Play();
                        bool hitted;
                        target.GetComponent<PawnCharacter>().Damage(character.attack, out hitted, DamageType.Blood);

                        if (hitted)
                        {
                            Instantiate(damagePrefabParticle, target.transform.position + target.GetComponent<CapsuleCollider>().center, Quaternion.Euler(new Vector3(-90, 0, 0)));
                            Instantiate(bloodPrefabParticle, target.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                        }
                        else
                        {
                            Instantiate(missPrefabParticle, target.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                            //TODO Miss Effect;
                        }
                        
                           
				
					
				        attackCountdown = 1 / character.attackRate;
			        }

                }

            }else
                anim.setIsAttacking(false);

        }
        else
        {
            anim.setIsAttacking(false);
            //ChangeState(PawnState.Homing);
        }
		
    }

    //enemy in range of attack
    protected override void OnTriggerEnter(Collider other)
    {
		
        base.OnTriggerEnter(other);
		Debug.DrawLine(other.gameObject.transform.position, transform.position);
		if (other.GetType() == typeof(CapsuleCollider) && other.gameObject.tag == "Enemy")
		{
			
            enemiesInRange.Add(other.gameObject);
            //Debug.Log("Started Battle");
            //nav.isStopped = true;
            if (other.gameObject == target)
				ChangeState(PawnState.Battle);


		}
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
		if (other.GetType() == typeof(CapsuleCollider))
		{
			if (other.gameObject.tag == "Enemy")
			{
				//Debug.Log("Started Battle");
				nav.isStopped = false;
				enemiesInRange.Remove(other.gameObject);
				if (target != null)
				{
					ChangeState(PawnState.FindTarget);
				}
				else
				{
					ChangeState(PawnState.Homing);
				}

			}

		}
    }
}

using UnityEngine;
using System.Collections.Generic;

public class SimpleSoldierController : PawnController {

	public SoldierCampController camp;

	
	private SimpleSoldierCharacter character;

	private List<GameObject> enemiesInRange;

	public float attackCountdown = 0f;


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

    protected override void Awake()
    {
        base.Awake();
        currentState = PawnState.Idle;
        homePosition = transform.position;
        character = (SimpleSoldierCharacter)GetComponent<SimpleSoldierCharacter>();
		camp = (SoldierCampController)GetComponentInParent<SoldierCampController>();
		enemiesInRange = new List<GameObject>();

    }
    // Update is called once per frame
    protected override void Update() {
        base.Update();
        attackCountdown -= Time.deltaTime;
		if(target != null && enemiesInRange.Contains(target)){
			ChangeState(PawnState.Battle);
		}
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
				Debug.DrawLine(transform.position, target.transform.position);
				//we are goint to apply damage to target and if the target is dead, we are going to
				//tell the camp and so the camp can gives another target or we're going back
				if (target.GetComponent<PawnCharacter>().Damage(character.attack))
				{
					camp.UpdateEnemies(target);
					target = null;
				}
				attackCountdown = 1 / character.attackRate;
			}
        }else
            ChangeState(PawnState.Homing);
		
    }

    //enemy in range of attack
    protected override void OnTriggerEnter(Collider other)
    {

        base.OnTriggerEnter(other);
		Debug.DrawLine(other.gameObject.transform.position, transform.position);

        if (other.gameObject.tag == "Enemy" && other.gameObject == target)
        {
			enemiesInRange.Add(other.gameObject);
            //Debug.Log("Started Battle");
            //nav.isStopped = true;
            ChangeState(PawnState.Battle);

		}
		else if(other.gameObject.tag == "Enemy")
		{
			enemiesInRange.Add(other.gameObject);
			
		}
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

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

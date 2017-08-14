using UnityEngine;
using System.Collections;

public class SimpleSoldierController : PawnController {

    private SimpleSoldierCharacter character;
    public float lastAttack;

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
    }
    // Update is called once per frame
    protected override void Update() {
        base.Update();

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
        //Debug.Log("Battling");

        if (target != null)
        {
           if (Time.time >character.attackRate + lastAttack)
           {
             target.GetComponent<PawnCharacter>().Damage(character.attack);
           }
           lastAttack += Time.time;
        }else
            ChangeState(PawnState.Homing);

           
       

        
    }

    //enemy in range of attack
    protected override void OnTriggerEnter(Collider other)
    {

        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Enemy" && other.gameObject == target)
        {
            Debug.Log("Started Battle");
            //nav.isStopped = true;
            ChangeState(PawnState.Battle);

        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Started Battle");
            nav.isStopped = false;
            if(target != null)
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

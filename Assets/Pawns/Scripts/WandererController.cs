using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererController : PawnController
{
    
    protected override void Awake()
    {
        base.Awake();
        speed = 2;
        
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
        Debug.Log("Battling");
    }


    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}

    protected override void OnTriggerEnter(Collider other)
    {

        base.OnTriggerEnter(other);
        if(other.gameObject.tag == "Ally")
        {
            speed = 1;
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

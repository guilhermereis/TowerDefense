using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorCharacter : PawnCharacter {

	private void Awake()
	{
		attack = 700;
	}

    public override void OnDying()
    {
        base.OnDying();
        WarriorGoblingAnimatorController anim = (WarriorGoblingAnimatorController)GetComponentInChildren<WarriorGoblingAnimatorController>();
        anim.isDead = true;
        Debug.Log("Dyingmotherfucker");
    }

    public void DeathEnd()
    {
        Destroy(gameObject);
    }
}

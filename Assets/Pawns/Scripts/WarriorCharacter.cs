﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorCharacter : PawnCharacter {

	private void Awake()
	{
	}

    public override void Die(DamageType damage)
    {
        base.Die(damage);
        WarriorGoblingAnimatorController anim = (WarriorGoblingAnimatorController)GetComponentInChildren<WarriorGoblingAnimatorController>();
        anim.isDead = true;
    }

    public void DeathEnd()
    {
        PawnDeathAnimation deathScript = (PawnDeathAnimation)GetComponent<PawnDeathAnimation>();
        if (deathScript)
            deathScript.Die(this);
    }

    public override void OnDeathAnimationEnd()
    {
        base.OnDeathAnimationEnd();
        Destroy(gameObject);
    }
}

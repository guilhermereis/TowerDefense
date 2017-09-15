using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKingCharacter : PawnCharacter {

	private void Awake()
	{
		attack = 7000;
	}

    public override void Die()
    {
        base.Die();
        GoblinKingAnimatorController anim = (GoblinKingAnimatorController)GetComponent<GoblinKingAnimatorController>();
        GoblinKingController controller = (GoblinKingController)GetComponent<GoblinKingController>();
        controller.isDead = true;
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

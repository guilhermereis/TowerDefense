using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererCharacter : PawnCharacter {

    public override void Die(DamageType _damageType)
    {
        base.Die( _damageType);
        WandererAnimatorController anim = (WandererAnimatorController)GetComponent<WandererAnimatorController>();
        anim.isDead = true;
    }

    public void DeathEnd() {
        PawnDeathAnimation deathScript = (PawnDeathAnimation)GetComponent<PawnDeathAnimation>();
        if(deathScript)
            deathScript.Die(this);
    }

    public override void OnDeathAnimationEnd()
    {
        base.OnDeathAnimationEnd();
        Destroy(gameObject);
    }
}

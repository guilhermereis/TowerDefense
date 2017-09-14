using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererCharacter : PawnCharacter {

    public override void OnDying()
    {
        base.OnDying();
        WandererAnimatorController anim = (WandererAnimatorController)GetComponent<WandererAnimatorController>();
        anim.isDead = true;
    }

    public void DeathEnd() {
        Destroy(gameObject);
    }

}

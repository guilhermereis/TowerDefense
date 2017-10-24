using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberCharacter : PawnCharacter {

    public GameObject prefabExplosionSound;

    private void Awake()
    {
        attack = 700;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void Die(DamageType _damageType)
    {
        base.Die(_damageType);
        BomberAnimatorController anim = (BomberAnimatorController)GetComponentInChildren<BomberAnimatorController>();
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

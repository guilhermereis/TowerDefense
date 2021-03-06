﻿using UnityEngine;

public class BomberController : EnemyController {
    [HideInInspector]
    public float attackCountdown = 0f;

    private BomberCharacter character;
    public GameObject explosionParticlePrefab;

    private BomberAnimatorController anim;

    private void Start()
    {
        weight = 3;
        character = (BomberCharacter)GetComponent<BomberCharacter>();
        anim = (BomberAnimatorController)GetComponentInChildren<BomberAnimatorController>();
    }

    protected override void Awake()
    {
        base.Awake();
        speed = 2;

        mats = transform.Find("PrefabBomberGoblin").Find("polySurface1").GetComponent<Renderer>().materials;
        mats[0] = frozenMaterial;

        originalMaterial = transform.Find("PrefabBomberGoblin").Find("polySurface1").GetComponent<Renderer>().materials;
    }

    public override void EnterFrozenTime()
    {
        base.EnterFrozenTime();
        if (originalMaterial != null)
            transform.Find("PrefabBomberGoblin").Find("polySurface1").GetComponent<Renderer>().materials = mats;
    }

    public override void LeaveFrozenTime()
    {
        base.LeaveFrozenTime();
        if (originalMaterial != null)
            transform.Find("PrefabBomberGoblin").Find("polySurface1").GetComponent<Renderer>().materials = originalMaterial;
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

    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (character.isSlow)
        {
            anim.speed = nav.velocity.magnitude * 2f;
            anim.speedMultiplier = 0.3f;
            
        }
        else {
            anim.speed = nav.velocity.magnitude;
            anim.speedMultiplier = 1f;
        }
    }

   

    protected override void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Castle")
        { 
            other.gameObject.GetComponent<CastleHealth>().ApplyDamage(character.attack);
            Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);
            //Camera.main.GetComponent<CameraShake>().PlayShake();
            gameObject.GetComponent<PawnCharacter>().exploded = true;
            SoundToPlay.PlayAtLocation(gameObject.GetComponent<BomberCharacter>().prefabExplosionSound, transform.position, Quaternion.identity, 10f);
            //apply damage to itself with total health amount.
            bool hitted;
            character.Damage(character.health,out hitted,DamageType.Explosion);

        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

    }
}

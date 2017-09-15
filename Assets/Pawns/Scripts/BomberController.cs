using UnityEngine;

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
        anim.speed = nav.velocity.magnitude;

    }

   

    protected override void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Castle")
        {
            other.gameObject.GetComponent<CastleHealth>().ApplyDamage(character.attack);
            Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);
            //Camera.main.GetComponent<CameraShake>().PlayShake();
            if (character.Damage(character.health))
            {
                character.Die();
                Instantiate(gameObject.GetComponent<BomberCharacter>().prefabExplosionSound, transform);
            }

        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

    }
}

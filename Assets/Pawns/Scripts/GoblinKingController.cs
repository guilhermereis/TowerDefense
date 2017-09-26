using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKingController : EnemyController {

    private CameraManager cameraManager;
    public GameObject handBone;
    public GameObject soldierThrowPrefab;
    public float attackCountdown = 0f;

    private GoblinKingCharacter character;
   
    private GoblinKingAnimatorController anim;
    private Transform handSocketTransform;

    private bool throwFinished = true;
    public bool isDead = false;

    // Use this for initialization
    void Start() {
        weight = 2;
        enemiesInRange = new List<GameObject>();
        character = (GoblinKingCharacter)GetComponent<GoblinKingCharacter>();
        anim = (GoblinKingAnimatorController)GetComponentInChildren<GoblinKingAnimatorController>();
        handSocketTransform = handBone.transform;
        cameraManager = GameObject.Find("/GameMode/CameraManager").GetComponent<CameraManager>();
    }


    protected override void Awake()
    {
        base.Awake();
        mats = transform.Find("polySurface2").GetComponent<Renderer>().materials;
        mats[0] = frozenMaterial;
        originalMaterial = transform.Find("polySurface2").GetComponent<Renderer>().materials;
    }

    public override void EnterFrozenTime()
    {
        base.EnterFrozenTime();
        if (originalMaterial != null)
            transform.Find("polySurface2").GetComponent<Renderer>().materials = mats;
    }

    public override void LeaveFrozenTime()
    {
        base.LeaveFrozenTime();
        if (originalMaterial != null)
            transform.Find("polySurface2").GetComponent<Renderer>().materials = originalMaterial;
    }



    // Update is called once per frame
    protected override void Update() {
        base.Update();
        if (!isDead)
        {
            anim.speed = nav.velocity.magnitude;
            if (target ? target.gameObject.tag == "Castle" : false)
            {
                throwFinished = false;
                anim.isAttacking = false;
                anim.isAttackingCastle = true;
                ChangeState(PawnState.Battle);
            }
            else {
                if (throwFinished)
                {
                    float value = attackCountdown - Time.deltaTime;
                    attackCountdown = Mathf.Max(0, value);
                    ChangeState(PawnState.Walking);
                }

                if (throwFinished && attackCountdown <= 0)
                {
                    target = GetNewTarget();

                    if (target != null)
                    {
                        attackCountdown = 1f / character.attackRate;
                        throwFinished = false;
                        anim.isAttacking = true;
                        ChangeState(PawnState.Battle);
                    }
                    else
                    {
                        anim.isAttacking = false;
                        ChangeState(PawnState.Walking);
                    }
                }
            }
        }
        else {
            if(nav)
                nav.isStopped = true;
        }
    }

    private GameObject GetNewTarget() {
        GameObject returnTarget = null;

        if (enemiesInRange.Count > 0) {
            returnTarget = enemiesInRange[0];
        }
        return returnTarget;
    }

    public override void OnBattle()
    {
        base.OnBattle();
        nav.isStopped = true;
        if (target != null) {
            LookToTarget();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.GetType() == typeof(CapsuleCollider) && other.gameObject.tag == "Ally")
        {
            enemiesInRange.Add(other.gameObject);
        }
        else if (other.gameObject.tag == "Castle")
        {
            target = other.gameObject;
            ChangeState(PawnState.Battle);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.tag.Equals("Ally"))
        {
            enemiesInRange.Remove(other.gameObject);
            if (target == other.gameObject) {
                target = null;
            }
        }
    }

    public void Punch() {
        if (target.tag == "Castle") {
            target.GetComponent<CastleHealth>().ApplyDamage(character.attack);
            attackCountdown = 1 / character.attackRate;
        }
    }

    public void Grab(string str) {
        switch (str) {
            case "Pick":
                if (target.tag == "Ally")
                {
                    bool hitted; 
                    if (target.GetComponent<PawnCharacter>().Damage(character.attack, out hitted))
                    {
                        enemiesInRange.Remove(target);
                        target.GetComponent<PawnCharacter>().Die();
                        GameObject holdingSoldier = Instantiate(soldierThrowPrefab);
                        Vector3 handPosition = new Vector3(handBone.transform.position.x + 0.103f, handBone.transform.position.y - 0.109f, handBone.transform.position.z - 0.19f);
                        Quaternion handRotation = handBone.transform.rotation * Quaternion.Euler(90f, 0f, 0f);
                        holdingSoldier.transform.SetPositionAndRotation(handPosition, handRotation);
                        FixedJoint joint = holdingSoldier.GetComponentInChildren<FixedJoint>();
                        joint.connectedBody = handBone.GetComponent<Rigidbody>();
                        target = null;
                    }
                }
                break;
            case "End":
                throwFinished = true;
                anim.isAttacking = false;
                break;

            default:
                break;
        }
    }

    public void Step() {
        cameraManager.shakeCamera(0.5f, 10f, 0.05f);
    }

    public void Death(string deathEvent) {
        switch (deathEvent) {
            case "Butt":
                cameraManager.shakeCamera(0.5f, 10f, 0.2f);
                break;
            case "Back":
                cameraManager.shakeCamera(0.5f, 10f, 0.5f);
                break;
        }
    }
}

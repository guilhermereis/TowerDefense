using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKingController : EnemyController {

    private CameraManager cameraManager;
    public GameObject handBone;
    public GameObject soldierThrowPrefab;
    public float attackCountdown = 0f;

    private GoblinKingCharacter character;
    private List<GameObject> enemiesInRange;
    private GoblinKingAnimatorController anim;
    private Transform handSocketTransform;

    private bool throwFinished = true;

    // Use this for initialization
    void Start() {
        weight = 2;
        enemiesInRange = new List<GameObject>();
        character = (GoblinKingCharacter)GetComponent<GoblinKingCharacter>();
        anim = (GoblinKingAnimatorController)GetComponentInChildren<GoblinKingAnimatorController>();
        handSocketTransform = handBone.transform;
        cameraManager = GameObject.Find("/GameMode/CameraManager").GetComponent<CameraManager>();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        anim.speed = nav.velocity.magnitude;

        if (throwFinished) {
            float value = attackCountdown - Time.deltaTime;
            attackCountdown = Mathf.Max(0, value);
            ChangeState(PawnState.Walking);
        }

        if (throwFinished && attackCountdown <= 0) {
            target = GetNewTarget();

            if (target != null)
            {
                attackCountdown = 1f / character.attackRate;
                throwFinished = false;
                anim.isAttacking = true;
                ChangeState(PawnState.Battle);
            }
            else {
                anim.isAttacking = false;
                ChangeState(PawnState.Walking);
            }
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

    public void Grab(string str) {
        switch (str) {
            case "Pick":
                if (target.tag == "Ally")
                {
                    if (target.GetComponent<PawnCharacter>().Damage(character.attack))
                    {
                        enemiesInRange.Remove(target);
                        target.GetComponent<PawnCharacter>().OnDying();
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
        cameraManager.shakeCamera(0.5f, 5f, 1f);
    }
}

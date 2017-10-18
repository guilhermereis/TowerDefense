using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorGoblingAnimatorController : MonoBehaviour {
    private Animator anim;
    private WarriorController controller;
    private WarriorCharacter character;

    public bool isAttacking = false;
    public float speed = 0f;
    public float speedMultiplier = 1f;
    public bool isDead = false;

    public void setIsAttacking(bool newIsAttacking)
    {
        if (newIsAttacking)
        {
            weightLerp = 0.85f;
        }
        else weightLerp = 0f;
        isAttacking = newIsAttacking;
    }

    private float weightLerp = 0f;

    // Use this for initialization
    void Start () {
        anim = (Animator)GetComponent<Animator>();
        controller = (WarriorController)GetComponentInParent<WarriorController>();
        character = (WarriorCharacter)GetComponentInParent<WarriorCharacter>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead) {
            if (anim) {
                anim.SetLayerWeight(1, 0);
                anim.SetBool("Dead", isDead);
            }
        } else {
            if (isAttacking)
            {
                weightLerp += 0.1f * Time.deltaTime;
            }
            weightLerp = Mathf.Clamp(weightLerp, 0f, 1f);

            if (anim)
            {
                anim.SetBool("IsAttacking", isAttacking);
                anim.SetFloat("Speed", speed);
                anim.SetFloat("SpeedMultiplier", speedMultiplier);
                anim.SetLayerWeight(1, weightLerp);
            }
        }
        
    }

    public void AttackEnd(int i)
    {
        setIsAttacking(false);
    }

    public void Hit(int i) {
        controller.processHit() ;
    }

    public void DeathEnd() {
        character.DeathEnd();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererAnimatorController : MonoBehaviour {
    private Animator anim;

    public bool isAttacking = false;
    public bool isDead = false;
    public float speed = 0f;
    public float speedMultiplier = 1f;
    private AudioSource stepAudio;

    public void setIsAttacking(bool newIsAttacking)
    {
        if (newIsAttacking)
        {
            weightLerp = 0.85f;
        }
        else weightLerp = 0.2f;
        isAttacking = newIsAttacking;
    }

    private float weightLerp = 0f;

    // Use this for initialization
    void Start () {
        anim = (Animator)GetComponent<Animator>();
        stepAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead)
        {
            if (anim)
            {
                anim.SetLayerWeight(1, 0);
                anim.SetBool("Dead", isDead);
            }
        }
        else {
            if (isAttacking)
            {
                weightLerp += 0.1f * Time.deltaTime;
            }
            else
            {
                weightLerp -= 0.1f * Time.deltaTime;
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

    public void Step() {
        SoundToPlay.PlayAtLocation(stepAudio, transform.position, Quaternion.identity, 0.15f, 3f);
    }
}

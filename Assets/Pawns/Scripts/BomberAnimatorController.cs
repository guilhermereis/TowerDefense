using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAnimatorController : MonoBehaviour {
    private Animator anim;

    public bool isAttacking = false;
    public float speed = 0f;

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
    }
	
	// Update is called once per frame
	void Update () {
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
            anim.SetLayerWeight(1, weightLerp);
        }
    }
}

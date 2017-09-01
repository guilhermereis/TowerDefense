using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererAnimatorController : MonoBehaviour {
    private Animator anim;

    public bool isAttacking = false;
    public float speed = 0f;

    // Use this for initialization
    void Start () {
        anim = (Animator)GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (anim)
        {
            anim.SetBool("IsAttacking", isAttacking);
            anim.SetFloat("Speed", speed);
        }
    }
}

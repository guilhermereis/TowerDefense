using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererAnimatorController : MonoBehaviour {
    public Animator animInstance;

    public bool isAttacking = false;
    public float speed = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (animInstance)
        {
            animInstance.SetBool("IsAttacking", isAttacking);
            animInstance.SetFloat("Speed", speed / 4f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanAnimatorController : MonoBehaviour {
    public bool isAttacking = false;
    public Animator animInstance;
    private float weightLerp = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("g"))
        {
            isAttacking = true;
            if (animInstance) {
                animInstance.Play("Attack", 1, 0f);
            }
        }
        if (Input.GetKeyUp("g")){
            isAttacking = false;
        }
        
        if (animInstance) {
            animInstance.SetBool("IsAttacking", isAttacking);
            animInstance.SetLayerWeight(1, isAttacking ? 1f:0f);
        }
	}

}

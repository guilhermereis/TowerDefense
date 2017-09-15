using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnDeathAnimation : MonoBehaviour{

	public float deathTargetDepth = 1.5f;
    public float deathFallSpeed = 0.5f;
    private bool dead = false;
    private PawnCharacter delegateCharacter;
    private Vector3 finalPosition;

    public void Die (PawnCharacter delegateCharacter) {
        this.delegateCharacter = delegateCharacter;
        delegateCharacter.GetComponent<Rigidbody>().detectCollisions = false;
        delegateCharacter.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        finalPosition = transform.position + new Vector3(0f, -deathTargetDepth, 0f);
        dead = true;
    }

    public void Update()
    {
        if (dead) {
            float targetHeight = Mathf.Lerp(transform.position.y, finalPosition.y, deathFallSpeed * Time.deltaTime);
            //Debug.Log(targetHeight);
            transform.position = new Vector3(transform.position.x, targetHeight, transform.position.z);

            if (transform.position.y <= -deathTargetDepth)
            {
                dead = false;
                delegateCharacter.OnDeathAnimationEnd();
            }
        }
    }
}

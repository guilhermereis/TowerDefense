using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnDeathAnimation : MonoBehaviour{

	public float deathTargetDepth = 15f;
    public float deathFalSpeed = 2f;
    private bool dead = false;

    public void Die() {
        dead = true;
    }

    public void Update()
    {
        if (dead) {

        }
    }
}

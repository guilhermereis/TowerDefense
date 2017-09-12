using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlowController : TowerController {


    public float slowAmount  = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
        if (attackCooldown <= 0)
        {

            if (enemies.Count>0)
            {

                Fire();
                //lightningLine.enabled = false;
                attackCooldown = 1 / fireRate;

            }
           

        }

        attackCooldown -= Time.deltaTime;
    }


    public override void Fire()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            target = enemies[i];
            if (!target.GetComponent<PawnCharacter>().isSlow && !(target.GetComponent<PawnController>().currentState == PawnController.PawnState.Dead))
            {
                GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
                Arrow newArrow = (Arrow)arrow.GetComponent<Arrow>();
                arrow.transform.parent = transform;
                newArrow.Target = target;
               
            }
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.GetType() == typeof(CapsuleCollider))
        {
            other.gameObject.GetComponent<PawnController>().deadPawn += RemoveDeadEnemy;
            enemies.Add(other.gameObject);
        }
    }
      
    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaCoilController : TowerController {


    public GameObject prefabLightningParticle;
	
    
  

    // Update is called once per frame
    public override void Update () {

        if (attackCooldown <= 0)
        {

            if (enemies.Count >0)
            {
                lightningLine.enabled = true;
                Fire();
                
                attackCooldown = 1 / fireRate;

            }else
                lightningLine.enabled = false;

        }

        attackCooldown -= Time.deltaTime;

    }

    public override void Fire()
    {
        //Debug.Break();
       

        RaycastHit hit;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                //lightningLine.material.mainTextureOffset = new Vector2(0, Time.time);
                target = enemies[i];
                GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
                Arrow newArrow = (Arrow)arrow.GetComponent<Arrow>();
                arrow.transform.parent = transform;
                newArrow.Target = target;
                Instantiate(arrowSoundPrefab, transform.position, Quaternion.identity);
                //Vector3 dir = (target.transform.position + target.GetComponent<CapsuleCollider>().center) - attackPoint.transform.position;
                //if (Physics.Raycast(attackPoint.transform.position, dir.normalized, out hit))
                //{
                //    Debug.Log(hit.collider);
                //    lightningLine.SetPosition(0, attackPoint.transform.position);
                //    lightningLine.SetPosition(1, hit.point);
                //}



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

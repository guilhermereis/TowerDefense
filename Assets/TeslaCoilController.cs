using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaCoilController : TowerController {


    public GameObject prefabLightningParticle;


    void Start()
    {
        SetFireRateAndAttackPower();
    }
    public override int GetSellCostWithInterest()
    {
        return unitBlueprint.withInterest_sellcost;
    }
    // Update is called once per frame
    public override void Update () {

        if (attackCooldown <= 0 && GameController.gameState != GameState.GameOver)
        {

            if (enemies.Count > 0)
            {

                Fire();

                attackCooldown = 1 / getFireRate();
            }
           

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
                
                target = enemies[i];
                GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
                Arrow newArrow = (Arrow)arrow.GetComponent<Arrow>();
                arrow.transform.parent = transform;
                newArrow.Target = target;
                //SoundToPlay.PlaySfx(arrowSoundPrefab,0.125f);
                SoundToPlay.PlayAtLocation(arrowSoundPrefab, transform.position, Quaternion.identity, 0.125f);


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

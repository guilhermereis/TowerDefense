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
        float percent = ((float)unitBlueprint.getRegularSellCost() / (float)unitBlueprint.cost);

        getAttackPowerLVL();
        getFireRateLVL();
        float interest = 0;
        Debug.Log("PERCENT = " + percent);
        Debug.Log("ATTACK POWER LVL = " + getAttackPowerLVL());
        Debug.Log("FIRE RATE LVL = " + getFireRateLVL());
        string tower1name = "PrefabArcherTower1(Clone)";
        string tower2name = "PrefabArcherTower2(Clone)";
        string tower3name = "PrefabArcherTower3(Clone)";
        string towerSlowName = "PrefabArcherTower2Slow(Clone)";
        string towerTeslaName = "PrefabArcherTower2Tesla(Clone)";

        if (gameObject.name == tower1name)
        {
            switch (getAttackPowerLVL())
            {
                case 0: break;
                case 1: { interest += Shop.instance.upgradeT1Ad1price * percent; break; }
                case 2: { interest += Shop.instance.upgradeT1Ad2price * percent; break; }
                case 3: { interest += Shop.instance.upgradeT1Ad3price * percent; break; }
            }
            switch (getFireRateLVL())
            {
                case 0: break;
                case 1: { interest += Shop.instance.upgradeT1As1price * percent; break; }
                case 2: { interest += Shop.instance.upgradeT1As2price * percent; break; }
                case 3: { interest += Shop.instance.upgradeT1As3price * percent; break; }
            }
        }
        else if (gameObject.name == tower2name
                 || gameObject.name == towerSlowName
                 || gameObject.name == towerTeslaName)
        {
            switch (getAttackPowerLVL())
            {
                case 0: break;
                case 1: { interest += Shop.instance.upgradeT2Ad1price * percent; break; }
                case 2: { interest += Shop.instance.upgradeT2Ad2price * percent; break; }
                case 3: { interest += Shop.instance.upgradeT2Ad3price * percent; break; }
            }
            switch (getFireRateLVL())
            {
                case 0: break;
                case 1: { interest += Shop.instance.upgradeT2As1price * percent; break; }
                case 2: { interest += Shop.instance.upgradeT2As2price * percent; break; }
                case 3: { interest += Shop.instance.upgradeT2As3price * percent; break; }
            }
        }
        else if (gameObject.name == tower3name)
        {
            switch (getAttackPowerLVL())
            {
                case 0: break;
                case 1: { interest += Shop.instance.upgradeT3Ad1price * percent; break; }
                case 2: { interest += Shop.instance.upgradeT3Ad2price * percent; break; }
                case 3: { interest += Shop.instance.upgradeT3Ad3price * percent; break; }
            }
            switch (getFireRateLVL())
            {
                case 0: break;
                case 1: { interest += Shop.instance.upgradeT3As1price * percent; break; }
                case 2: { interest += Shop.instance.upgradeT3As2price * percent; break; }
                case 3: { interest += Shop.instance.upgradeT3As3price * percent; break; }
            }
        }

        //Debug.Log("ALGUEM ESTA CHAMANDO ISSO ! INTEREST = " + interest + ", PERCENT = " + percent);

        return unitBlueprint.getRegularSellCost() + Mathf.FloorToInt(interest);
    }
    // Update is called once per frame
    public override void Update () {

        if (attackCooldown <= 0)
        {

            if (enemies.Count >0)
            {
                lightningLine.enabled = true;
                Fire();
               
                attackCooldown = 1 / getFireRate();

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

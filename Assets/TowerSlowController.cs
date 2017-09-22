﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlowController : TowerController {

#region Slow
    public float slowAmount;

    public float SlowAmount
    {
        get
        {
            return slowAmount;
        }

        set
        {
            slowAmount = value;
        }
    }
#endregion
    // Use this for initialization
    void Start()
    {
        SetFireRateAndAttackPower();
        SlowAmount = 5.0f;
    }
    public void SetFireRateAndAttackPower()
    {
        Debug.Log("Just upgraded " + gameObject.name);
        string tower1name = "PrefabArcherTower1(Clone)";
        string tower2name = "PrefabArcherTower2(Clone)";
        string tower3name = "PrefabArcherTower3(Clone)";
        string towerSlowName = "PrefabArcherTower2Slow(Clone)";
        string towerTeslaName = "PrefabArcherTower2Tesla(Clone)";
        int base_ap = 0;
        float base_fr = 0;
        //-----------SET BASE AP AND FR FOR TOWER TYPE-------------
        if (gameObject.name == tower1name)
        {
            base_ap = Tower1BaseAP;
            base_fr = Tower1BaseFR;
        }
        else if (gameObject.name == tower2name
                 || gameObject.name == towerSlowName
                 || gameObject.name == towerTeslaName)
        {
            base_ap = Tower2BaseAP;
            base_fr = Tower2BaseFR;
            SlowAmount = 5.0f;
        }
        else if (gameObject.name == tower3name)
        {
            base_ap = Tower3BaseAP;
            base_fr = Tower3BaseFR;
        }
        //-------------------------------------------------------
        if (attackPowerLVL == 0)
        {
            attackPower = base_ap;
        }
        else if (attackPowerLVL == 1)
        {
            attackPower = base_ap * 1.5f;
        }
        else if (attackPowerLVL == 2)
        {
            attackPower = base_ap * 2.0f;
        }
        else if (attackPowerLVL == 3)
        {
            attackPower = base_ap * 2.5f;
        }
        if (fireRateLVL == 0)
        {
            fireRate = base_fr;
        }
        else if (fireRateLVL == 1)
        {
            fireRate = base_fr * 1.5f;
        }
        else if (fireRateLVL == 2)
        {
            fireRate = base_fr * 2.0f;
        }
        else if (fireRateLVL == 3)
        {
            fireRate = base_fr * 2.5f;
        }

    }
    // Update is called once per frame
    public override void Update () {
        if (attackCooldown <= 0)
        {

            if (enemies.Count>0)
            {

                Fire();
                Debug.Log("THIS TOWERS FIRE RATE IS: " + getFireRate());
                //lightningLine.enabled = false;
                attackCooldown = 1 / getFireRate();

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
                Instantiate(arrowSoundPrefab, transform.position, Quaternion.identity);
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

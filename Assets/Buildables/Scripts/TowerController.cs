﻿using UnityEngine;
using System.Collections.Generic;

public enum TowerAmmo { Arrow,ArrowFire, CannonBall, Lightning}

public class TowerController : BuildableController {
    #region Attack
    [Header("Attack")]
	public GameObject target;
    public float attackCooldown { get; set; }
	public List<GameObject> enemies;
	private float attackPower;
    private float fireRate = 0.7f;
    public int attackPowerLVL = 0;
    public int fireRateLVL = 0;
    #endregion

    [Header("Arrow")]
	public GameObject attackPoint;
	public GameObject arrowPrefab;
    public GameObject arrowSoundPrefab;

    [Header("CannonBall")]
    public GameObject cannonPoint;
    public GameObject cannonballPrefab;

    [Header("Lightning")]
    public GameObject lightningPrefab;
    public LineRenderer lightningLine;

    [Header("Weapon")]
    public TowerAmmo currentAmmo;


    [Header("Build Effect")]
    ParticleSystem buildSmokeEffectPrefab;

   

   

    // Use this for initialization
    public void Start () {
        attackPower = 700;
        Health = 100f;
		Defense = 5f;
		IsUpgradable = true;
		enemies = new List<GameObject>();
        //currentAmmo = TowerAmmo.Arrow;
        

        
       
      
        //Instantiate(buildSmokeEffectPrefab, transform.position, Quaternion.identity);



	}
	
    public void BuildEffect()
    {
        buildSmokeEffectPrefab = GetComponentInChildren<ParticleSystem>();
        buildSmokeEffectPrefab.Play();
    }

	// Update is called once per frame
	public virtual void Update () {
        
        if (attackCooldown <= 0)
		{
          
			if( target != null && !target.gameObject.GetComponent<PawnCharacter>().isDying)
			{

				Fire();
                //lightningLine.enabled = false;
                attackCooldown = 1 / fireRate;

            }
            else
            {
                if (enemies.Count > 0)
                    target = enemies[0];
               
                    
            }
		}
        
        attackCooldown -= Time.deltaTime;

	}
    public void SetFireRateAndAttackPower()
    {

        if (attackPowerLVL == 0)
        {
            attackPower = 100;
        }
        else if (attackPower == 1)
        {
            attackPower = 150;
        }
        else if (attackPower == 2)
        {
            attackPower = 200;
        }
        else if (attackPower == 3)
        {
            attackPower = 250;
        }
        if (fireRateLVL == 0)
        {
            fireRate = 0.7f;
        }
        else if (fireRateLVL == 1)
        {
            fireRate = 1.1f;
        }
        else if (fireRateLVL == 2)
        {
            fireRate = 1.5f;
        }
        else if (fireRateLVL == 3)
        {
            fireRate = 1.9f;
        }
    }

    public void SetFireRateAndAttackPowerByLVL(int _fireRateLVL, int _attackPowerLVL)
    {
        fireRateLVL = _fireRateLVL;
        attackPowerLVL = _attackPowerLVL;
        if (attackPowerLVL == 0)
        {
            attackPower = 100;
        }
        else if (attackPower == 1)
        {
            attackPower = 150;
        }
        else if (attackPower == 2)
        {
            attackPower = 200;
        }
        else if (attackPower == 3)
        {
            attackPower = 250;
        }
        if (fireRateLVL == 0)
        {
            fireRate = 0.7f;
        }
        else if (fireRateLVL == 1)
        {
            fireRate = 1.1f;
        }
        else if (fireRateLVL == 2)
        {
            fireRate = 1.5f;
        }
        else if (fireRateLVL == 3)
        {
            fireRate = 1.9f;
        }
    }

    public virtual void Fire()
	{
        //Debug.DrawLine(attackPoint.transform.position, target.transform.position, Color.blue,2f);
        if(currentAmmo == TowerAmmo.Arrow)
        {
            GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
            if(arrow != null)
            {
                Arrow newArrow = (Arrow)arrow.GetComponent<Arrow>();
                arrow.transform.parent = transform;
                newArrow.Target = target;
                newArrow.TowerAttack = attackPower;

                Instantiate(arrowSoundPrefab, transform.position, Quaternion.identity);
            }
        }
        else if( currentAmmo == TowerAmmo.ArrowFire)
        {
            GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
            if (arrow != null)
            {
                Arrow newArrow = (Arrow)arrow.GetComponent<Arrow>();
                arrow.transform.parent = transform;
                newArrow.Target = target;
                newArrow.attackPower = attackPower;

                Instantiate(arrowSoundPrefab, transform.position, Quaternion.identity);
            }
        }else if( currentAmmo == TowerAmmo.CannonBall)
        {
            //0 right
            //90 south
            //-90 north
            //180 left

            GameObject rCannonBall = Instantiate(cannonballPrefab, cannonPoint.transform.position, Quaternion.identity);
            rCannonBall.transform.parent = transform;

            GameObject dCannonBall = Instantiate(cannonballPrefab, cannonPoint.transform.position, Quaternion.Euler(new Vector3(0,90,0)));
            dCannonBall.transform.parent = transform;

            GameObject uCannonBall = Instantiate(cannonballPrefab, cannonPoint.transform.position, Quaternion.Euler(new Vector3(0, -90, 0)));
            uCannonBall.transform.parent = transform;

            GameObject lCannonBall = Instantiate(cannonballPrefab, cannonPoint.transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
            lCannonBall.transform.parent = transform;
        }
        

		

	}

    //removes dead enemy from current enemies list;
    public void RemoveDeadEnemy(GameObject _enemy)
    {
        enemies.Remove(_enemy);
    }

    public override void OnTriggerEnter(Collider other)
	{
        Debug.Log(other.GetType());

        if (other.gameObject.CompareTag("Enemy") && other.GetType() == typeof(CapsuleCollider))
		{
            other.gameObject.GetComponent<PawnController>().deadPawn += RemoveDeadEnemy;
			enemies.Add(other.gameObject);
            
			if(target == null)
			{
				//int index = enemies.FindIndex(t=> t.GetInstanceID() == other.gameObject.GetInstanceID());
				//enemies.RemoveAt(index);
				target = other.gameObject;
			}
		}
	}

	void ChangeTarget() { }

   

	public override void OnTriggerExit(Collider other)
	{
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
            if (other.gameObject == target && other.GetType() == typeof(CapsuleCollider))
		    {
                if (enemies.Count > 0)
                    target = enemies[0];
                else
				    target = null;
		    }

        }

		
	}
    public float getFireRate()
    {
        return fireRate;
    }
    public float getAttackPower()
    {
        return attackPower;
    }
    public void setFireRate(float _fireRate)
    {
        fireRate = _fireRate;
    }
    public void setAttackPower(float _attackPower)
    {
        attackPower = _attackPower;
    }

    public void setFireRateLVL(int _fireRateLVL)
    {
        fireRateLVL = _fireRateLVL;
    }
    public void setAttackPowerLVL(int _attackPowerLVL)
    {
        attackPowerLVL = _attackPowerLVL;
    }
    public float getFireRateLVL()
    {
        return fireRateLVL;
    }
    public float getAttackPowerLVL()
    {
        return attackPowerLVL;
    }
}

using UnityEngine;
using System.Collections.Generic;

public enum TowerAmmo { Arrow,ArrowFire, CannonBall, Lightning}

public class TowerController : BuildableController {
    #region Attack
    [Header("Attack")]
	public GameObject target;
    public float attackCooldown { get; set; }
	public List<GameObject> enemies;
	protected float attackPower;
    protected float fireRate = 0.7f;
    public int attackPowerLVL = 0;
    public int fireRateLVL = 0;
    private int apLVLreached = 0;
    private int frLVLreached = 0;
    public bool DONE = false;

    //-----------SETTING BASE AP AND FR-----------------
    protected int Tower1BaseAP = 100;
    protected int Tower2BaseAP = 200;
    protected int Tower3BaseAP = 300;

    protected float Tower1BaseFR = 0.7f;
    protected float Tower2BaseFR = 1.4f;
    protected float Tower3BaseFR = 2.1f;
    //--------------------------------------------------

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
        SetFireRateAndAttackPower();
        //Debug.Log("THIS UNIT'S FIRERATE AND ATTACKPOWER: " + fireRate+ ", " + attackPower);
        Health = 100f;
		Defense = 5f;
		IsUpgradable = true;
		enemies = new List<GameObject>();
        //currentAmmo = TowerAmmo.Arrow;





        //Instantiate(buildSmokeEffectPrefab, transform.position, Quaternion.identity);

        DONE = true;

	}
    public override int GetSellCostWithInterest()
    {
        return unitBlueprint.withInterest_sellcost;
    }

    public void BuildEffect()
    {
        Instantiate(buildSoundPrefab);
        buildSmokeEffectPrefab = GetComponentInChildren<ParticleSystem>();
        buildSmokeEffectPrefab.Play();
    }

	// Update is called once per frame
	public virtual void Update () {
        
        if (attackCooldown <= 0)
		{
          
			if( target != null && !target.gameObject.GetComponent<PawnCharacter>().isDead)
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
        float percent = ((float)unitBlueprint.getRegularSellCost() / (float)unitBlueprint.cost);
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
                 ||gameObject.name == towerSlowName
                 ||gameObject.name == towerTeslaName)
        {
            base_ap = Tower2BaseAP;
            base_fr = Tower2BaseFR;
        }
        else if (gameObject.name == tower3name)
        {
            base_ap = Tower3BaseAP;
            base_fr = Tower3BaseFR;
        }
        //-------------------------------------------------------
        if (attackPowerLVL == 0 && fireRateLVL == 0)
        {
            unitBlueprint.withInterest_sellcost = unitBlueprint.sell_cost;
        }

        //-------------------------------------------------------
        if (attackPowerLVL == 0)
        {
            attackPower = base_ap;
        }
        else if (attackPowerLVL == 1)
        {
            if (apLVLreached < 1)
            {
                attackPower = base_ap * 1.5f;
                unitBlueprint.withInterest_sellcost += Mathf.FloorToInt(percent * Shop.instance.upgradeT1Ad1price);
                apLVLreached = 1;
            }
        }
        else if (attackPowerLVL == 2)
        {
            if (apLVLreached < 2)
            {
                attackPower = base_ap * 2.0f;
                unitBlueprint.withInterest_sellcost += Mathf.FloorToInt(percent * Shop.instance.upgradeT1Ad2price);
                apLVLreached = 2;
            }
        }
        else if (attackPowerLVL == 3)
        {
            if (apLVLreached < 3)
            {
                attackPower = base_ap * 2.5f;
                unitBlueprint.withInterest_sellcost += Mathf.FloorToInt(percent * Shop.instance.upgradeT1Ad3price);
                apLVLreached = 3;
            }
        }
        if (fireRateLVL == 0)
        {
            fireRate = base_fr;
        }
        else if (fireRateLVL == 1)
        {
            if (frLVLreached < 1)
            {
                fireRate = base_fr * 1.5f;
                unitBlueprint.withInterest_sellcost += Mathf.FloorToInt(percent * Shop.instance.upgradeT1As1price);
                frLVLreached = 1;
            }
        }
        else if (fireRateLVL == 2)
        {
            if (frLVLreached < 2)
            {
                fireRate = base_fr * 2.0f;
                unitBlueprint.withInterest_sellcost += Mathf.FloorToInt(percent * Shop.instance.upgradeT1As2price);
                frLVLreached = 2;
            }
        }
        else if (fireRateLVL == 3)
        {
            if (frLVLreached < 3)
            {
                fireRate = base_fr * 2.5f;
                unitBlueprint.withInterest_sellcost += Mathf.FloorToInt(percent * Shop.instance.upgradeT1As3price);
                frLVLreached = 3;
            }
        }
    }

    public void SetFireRateAndAttackPowerByLVL(int _fireRateLVL, int _attackPowerLVL)
    {
        fireRateLVL = _fireRateLVL;
        attackPowerLVL = _attackPowerLVL;
        

        string tower1name = "PrefabArcherTower1(Clone)";
        string tower2name = "PrefabArcherTower2(Clone)";
        string tower3name = "PrefabArcherTower3(Clone)";
        int base_ap = 0;
        float base_fr = 0;
        //-----------SET BASE AP AND FR FOR TOWER TYPE-------------
        if (gameObject.name == tower1name)
        {
            base_ap = Tower1BaseAP;
            base_fr = Tower1BaseFR;
        }
        else if (gameObject.name == tower2name)
        {
            base_ap = Tower2BaseAP;
            base_fr = Tower2BaseFR;
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


    public virtual void Fire()
	{
        //Debug.DrawLine(attackPoint.transform.position, target.transform.position, Color.blue,2f);
        if(currentAmmo == TowerAmmo.Arrow)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;

            GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, Quaternion.Euler(0f, rotation.y, 0f));
            if(arrow != null)
            {
                Arrow newArrow = (Arrow)arrow.GetComponent<Arrow>();
                arrow.transform.parent = transform;
                newArrow.Target = target;
                newArrow.TowerAttack = attackPower;

               

                Instantiate(arrowSoundPrefab, transform.position, Quaternion.Euler(0f, rotation.y, 0f));
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
    public int getFireRateLVL()
    {
        return fireRateLVL;
    }
    public int getAttackPowerLVL()
    {
        return attackPowerLVL;
    }
}

using UnityEngine;
using System.Collections.Generic;

public enum TowerAmmo { Arrow,ArrowFire, CannonBall}

public class TowerController : BuildableController {

	[Header("Attack")]
	public GameObject target;
	private float fireRate = 0.7f;
	private float attackCooldown = 0f;
	public List<GameObject> enemies;
	private float attackPower;

	[Header("Arrow")]
	public GameObject attackPoint;
	public GameObject arrowPrefab;
    public GameObject arrowSoundPrefab;

    [Header("CannonBall")]
    public GameObject cannonPoint;
    public GameObject cannonballPrefab;


    [Header("Weapon")]
    public TowerAmmo currentAmmo;

	

	// Use this for initialization
	void Start () {
		Health = 100f;
		Defense = 5f;
		IsUpgradable = true;
		enemies = new List<GameObject>();
        //currentAmmo = TowerAmmo.Arrow;
	}
	
	// Update is called once per frame
	void Update () {

		if (attackCooldown <= 0)
		{
			if( target != null && !target.gameObject.GetComponent<PawnCharacter>().isDying)
			{
				Fire();
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

	void Fire()
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


    void RemoveDeadEnemy(GameObject _enemy)
    {
        enemies.Remove(_enemy);
    }

    public void OnTriggerEnter(Collider other)
	{
        Debug.Log(other.GetType());

		if (other.gameObject.CompareTag("Enemy") && other.GetType() == typeof(BoxCollider))
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

   

	public void OnTriggerExit(Collider other)
	{
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
            if (other.gameObject == target && other.GetType() == typeof(BoxCollider))
		    {
                if (enemies.Count > 0)
                    target = enemies[0];
                else
				    target = null;
		    }

        }

		
	}
}

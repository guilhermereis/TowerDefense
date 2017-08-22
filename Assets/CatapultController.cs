using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultController : BuildableController {

	private List<GameObject> enemies;
	public GameObject target;
	public GameObject bulletPrefab;
	private Transform launchOrigin;
	private float fireRate;
	private float attackPower;
	private float coolDown = 0;

	public float AttackPower
	{
		get
		{
			return attackPower;
		}

		set
		{
			attackPower = value;
		}
	}

	// Use this for initialization
	void Start () {
		fireRate = 0.4f;
		AttackPower = 400f;
		Health = 100f;
		Defense = 5f;
		IsUpgradable = true;
		enemies = new List<GameObject>();
		launchOrigin = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(coolDown <= 0 && enemies.Count > 0)
		{
			LaunchProjectile();
			coolDown = 1 / fireRate;
		}
		coolDown -= Time.deltaTime;
	}

	private void LaunchProjectile()
	{
		Debug.Log(transform.rotation.eulerAngles);
		GameObject projectile = Instantiate(bulletPrefab, launchOrigin.position, transform.rotation);
		projectile.transform.parent = gameObject.transform;
		projectile.transform.localRotation = Quaternion.identity;

	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.isTrigger && other.gameObject.tag == "Enemy")
		{

			enemies.Add(other.gameObject);
			if (target == null)
				target = other.gameObject;

		}
	}


	private void OnTriggerExit(Collider other)
	{
		if (!other.isTrigger  && other.gameObject.tag == "Enemy")
		{
			enemies.Remove(other.gameObject);
			if (target == other.gameObject)
				target = null;

		}
	}

}

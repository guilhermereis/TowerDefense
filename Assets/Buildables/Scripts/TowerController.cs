﻿using UnityEngine;
using System.Collections.Generic;

public class TowerController : BuildableController {

	[Header("Attack")]
	public GameObject target;
	private float fireRate = 0.7f;
	private float attackCooldown = 0f;
	private List<GameObject> enemies;
	private float attackPower;

	[Header("Arrow")]
	public GameObject attackPoint;
	public GameObject arrowPrefab;

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
		AttackPower = 400f;
		Health = 100f;
		Defense = 5f;
		IsUpgradable = true;
		enemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

		if (attackCooldown <= 0)
		{
			if( target != null)
			{
				Fire();
				attackCooldown = 1 / fireRate;

			}
		}

		attackCooldown -= Time.deltaTime;

	}

	void Fire()
	{
		//Debug.DrawLine(attackPoint.transform.position, target.transform.position, Color.blue,2f);
		GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
        Arrow newArrow = (Arrow)arrow.GetComponent<Arrow>();
		newArrow.Target = target;
		

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Enemy") && !other.isTrigger)
		{
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

	private void OnTriggerExit(Collider other)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	private GameObject target;
	private float speed = 4f;
	private float towerAttack;

	public GameObject Target
	{
		get
		{
			return target;
		}

		set
		{
			target = value;
		}
	}

	public float TowerAttack
	{
		get
		{
			return towerAttack;
		}

		set
		{
			towerAttack = value;
		}
	}

	private void Start()
	{
		//towerAttack = GetComponentInParent<TowerController>().AttackPower;
	}

	private void Update()
	{
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }
		if(target == null)
		{
			Destroy(gameObject);
			return;
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Enemy"))
		{
			if(other.gameObject != null && other.gameObject == target)
			{
				if (other.gameObject.GetComponent<PawnCharacter>().Damage(TowerAttack))
				{
					other.gameObject.GetComponent<PawnCharacter>().OnDying();
				}

			}
				Destroy(gameObject);
				return;
		}
	}
}

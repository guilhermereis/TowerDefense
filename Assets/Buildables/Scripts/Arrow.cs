using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile {

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
        attackPower = 100f;
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

	public override void OnTriggerEnter(Collider other)
	{

        if (other.gameObject == target && other.GetType() == typeof(CapsuleCollider) )
        {
            if (other.gameObject.GetComponent<PawnCharacter>().Damage(attackPower))
            {
                other.gameObject.GetComponent<PawnCharacter>().OnDying();
            }

            Destroy(gameObject);
            return;

        }

       
	}
}

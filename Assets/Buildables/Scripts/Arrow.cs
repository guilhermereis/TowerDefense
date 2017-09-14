using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile {

	public GameObject target;
	public float speed = 1f;
	private float towerAttack;
    public GameObject damagePrefabParticle;
    public GameObject bloodPrefabParticle;
    Rigidbody rig;

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

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();

    }

    private void Start()
	{
        //towerAttack = GetComponentInParent<TowerController>().AttackPower;
        //attackPower = 400f;
        
	}

	public virtual void Update()
	{
        if (target != null)
        {
            Vector3 dir = target.GetComponent<CapsuleCollider>().center + target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            //rig.MovePosition(dir.normalized * speed * Time.deltaTime);
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
           
        }
        if (target == null || target.GetComponent<PawnController>().currentState == PawnController.PawnState.Dead)
		{
			Destroy(gameObject);
			return;
		}

	}

	public override void OnTriggerEnter(Collider other)
	{
        //Debug.Log(other);
        if (other.gameObject == target )
        {
            //spawn vfx for damage
            //Instantiate(damagePrefabParticle, target.transform.position + Vector3.up *0.5f, Quaternion.Euler(new Vector3(-90,0,0)));
            Instantiate(damagePrefabParticle, target.transform.position + target.GetComponent<CapsuleCollider>().center, Quaternion.Euler(new Vector3(-90, 0, 0)));
            Instantiate(bloodPrefabParticle, target.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

            if (other.gameObject.GetComponent<PawnCharacter>().Damage(towerAttack))
            {
                GetComponentInParent<TowerController>().enemies.Remove(other.gameObject);
                other.gameObject.GetComponent<PawnCharacter>().OnDying();
            }

            Destroy(gameObject);
            return;

        }
       
	}
}

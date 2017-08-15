using UnityEngine;

public class TowerController : MonoBehaviour {

	public GameObject target;
	private float fireRate = 1f;
	private float attackCooldown = 0f;
	public GameObject attackPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (attackCooldown <= 0)
		{
			Fire();
		}

		attackCooldown -= Time.deltaTime;

	}

	void Fire()
	{
		Debug.DrawLine(attackPoint.transform.position, target.transform.position, Color.blue,2f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Enemy"))
		{
			if(target == null)
			{
				target = other.gameObject;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject == target)
		{
			target = null;
		}

		
	}
}

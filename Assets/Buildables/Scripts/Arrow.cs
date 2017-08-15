using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	private GameObject target;
	private float speed = 30f;

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

	private void Update()
	{
		Vector3 dir = target.transform.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = lookRotation.eulerAngles;
		transform.rotation = Quaternion.Euler(0f,rotation.y,0f);
		transform.Translate(dir.normalized * speed * Time.deltaTime,Space.World);

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Enemy"))
		{
			Destroy(gameObject);
			return;
		}
	}
}
